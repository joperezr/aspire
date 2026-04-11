// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Hex1b.Input;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 2: TypeScript (polyglot) project lifecycle tests.
/// Validates that aspire new → run → stop works end-to-end using the published CLI
/// from a channel with a TypeScript starter template. This exercises the fundamentally
/// different polyglot execution model.
/// </summary>
public sealed class Tier2_TypeScriptProjectLifecycleTests
{
    [Fact]
    public async Task New_Run_Stop_TypeScriptStarterProject()
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

        // Create a TypeScript empty AppHost project using non-interactive mode
        await auto.TypeAsync("aspire new aspire-ts-empty-apphost --name TsValidationApp --non-interactive");
        await auto.EnterAsync();
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(3));

        // Run the TypeScript project with aspire run
        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "TsValidationApp"), counter);

        await auto.TypeAsync("aspire run --non-interactive");
        await auto.EnterAsync();

        // Wait for the app to start
        await auto.WaitUntilAsync(s =>
        {
            return s.ContainsText("Press CTRL+C to stop the AppHost and exit.");
        }, timeout: TimeSpan.FromMinutes(5), description: "Press CTRL+C message (aspire run started)");

        // Stop with Ctrl+C
        await auto.Ctrl().KeyAsync(Hex1bKey.C);
        await auto.WaitForSuccessPromptAsync(counter, TimeSpan.FromMinutes(1));

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
