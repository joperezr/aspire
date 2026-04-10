// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Hex1b.Input;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 2: C# (.NET) project lifecycle tests.
/// Validates that aspire new → build → run → stop works end-to-end
/// using the published CLI from a channel with a non-polyglot starter template.
/// </summary>
public sealed class Tier2_DotNetProjectLifecycleTests
{
    [Fact]
    public async Task New_Build_Run_Stop_StarterProject()
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

        // Create a new starter project using aspire new
        await auto.TypeAsync("aspire new");
        await auto.EnterAsync();

        // Wait for template selection and pick Starter App (first option)
        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("> Starter App").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(60),
            description: "template selection list (> Starter App)");
        await auto.EnterAsync();

        // Project name prompt
        await auto.WaitUntilTextAsync("Project name", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("ChannelValidationApp");
        await auto.EnterAsync();

        // Output path prompt — accept default
        await auto.WaitUntilTextAsync("Output path", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        // URLs prompt — accept default
        await auto.WaitUntilTextAsync("URLs", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        // Redis cache prompt — decline (no Docker guarantee on all platforms)
        await auto.WaitUntilTextAsync("Redis", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("n");
        await auto.EnterAsync();

        // Test project prompt — decline
        await auto.WaitUntilTextAsync("test project", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("n");
        await auto.EnterAsync();

        // Handle agent init prompt (decline)
        await auto.DeclineAgentInitPromptAsync(counter, TimeSpan.FromMinutes(3));

        // Build the created project
        await auto.TypeAsync("dotnet build ChannelValidationApp");
        await auto.EnterAsync();
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(3));

        // Run the project with aspire run
        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "ChannelValidationApp"), counter);

        await auto.TypeAsync("aspire run");
        await auto.EnterAsync();

        // Wait for the app to start
        await auto.WaitUntilAsync(s =>
        {
            if (s.ContainsText("Select an AppHost to use:"))
            {
                throw new InvalidOperationException(
                    "Unexpected apphost selection prompt — multiple apphosts incorrectly detected.");
            }
            return s.ContainsText("Press CTRL+C to stop the AppHost and exit.");
        }, timeout: TimeSpan.FromMinutes(3), description: "Press CTRL+C message (aspire run started)");

        // Stop with Ctrl+C
        await auto.Ctrl().KeyAsync(Hex1bKey.C);
        await auto.WaitForSuccessPromptAsync(counter, TimeSpan.FromMinutes(1));

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
