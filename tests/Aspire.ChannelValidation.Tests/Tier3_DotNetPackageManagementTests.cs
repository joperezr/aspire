// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 3: C# (.NET) package management tests.
/// Validates that aspire add and aspire update work correctly with packages from the channel feed.
/// </summary>
public sealed class Tier3_DotNetPackageManagementTests
{
    [Fact]
    public async Task Add_Integration_And_Update_Packages()
    {
        var repoRoot = ChannelValidationHelpers.GetRepoRoot();
        var channel = ChannelValidationHelpers.GetChannel();
        var workspace = ChannelValidationHelpers.CreateTempWorkspace();

        using var terminal = ChannelValidationHelpers.CreateTestTerminal();
        var pendingRun = terminal.RunAsync(TestContext.Current.CancellationToken);

        var counter = new SequenceCounter();
        var auto = new Hex1bTerminalAutomator(terminal, defaultTimeout: TimeSpan.FromSeconds(500));

        await auto.PrepareShellEnvironmentAsync(counter);
        await auto.InstallCliFromChannelAsync(repoRoot, channel, counter);
        await auto.AddCliToPathAsync(counter);
        await auto.ChangeDirectoryAsync(workspace, counter);

        // Create a starter project (no Redis, no test project)
        await auto.TypeAsync("aspire new");
        await auto.EnterAsync();

        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("> Starter App").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(60),
            description: "template selection");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("Project name", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("PkgTestApp");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("Output path", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("URLs", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("Redis", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("n");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("test project", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("n");
        await auto.EnterAsync();

        await auto.DeclineAgentInitPromptAsync(counter, TimeSpan.FromMinutes(3));

        // Navigate into the project
        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "PkgTestApp"), counter);

        // Run aspire add to add a hosting integration
        // Use PostgreSQL as it doesn't require Docker for just adding the package
        await auto.TypeAsync("aspire add Aspire.Hosting.PostgreSQL");
        await auto.EnterAsync();

        // Wait for the add command to prompt for AppHost selection or complete
        var appHostPromptShown = false;
        await auto.WaitUntilAsync(s =>
        {
            // It may ask which project is the AppHost
            if (s.ContainsText("AppHost"))
            {
                appHostPromptShown = true;
                return true;
            }
            // Or it may succeed directly
            return new CellPatternSearcher()
                .FindPattern(counter.Value.ToString())
                .RightText(" OK] $ ")
                .Search(s).Count > 0;
        }, timeout: TimeSpan.FromMinutes(2), description: "aspire add AppHost prompt or completion");

        // If prompted for AppHost, select the first option
        if (appHostPromptShown)
        {
            await auto.EnterAsync();
        }

        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(2));

        // Run aspire update to verify package resolution from channel feed
        await auto.TypeAsync("aspire update");
        await auto.EnterAsync();

        // Update must succeed — use fail-fast to catch feed resolution errors
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(3));

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
