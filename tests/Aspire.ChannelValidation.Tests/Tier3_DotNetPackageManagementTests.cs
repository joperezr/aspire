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

        // Create a starter project using non-interactive mode
        await auto.TypeAsync("aspire new aspire-starter --name PkgTestApp --non-interactive");
        await auto.EnterAsync();
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(3));

        // Navigate into the project
        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "PkgTestApp"), counter);

        // Run aspire add to add a hosting integration (non-interactive to avoid prompts)
        await auto.TypeAsync("aspire add Aspire.Hosting.PostgreSQL --non-interactive");
        await auto.EnterAsync();
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(2));

        // Run aspire update to verify package resolution from channel feed
        await auto.TypeAsync("aspire update --non-interactive");
        await auto.EnterAsync();

        // Update must succeed — use fail-fast to catch feed resolution errors
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(3));

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
