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

        // Create a new TypeScript starter project
        await auto.TypeAsync("aspire new");
        await auto.EnterAsync();

        // Wait for template selection and navigate to TypeScript Starter
        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("> Starter App").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(60),
            description: "template selection list");

        // Navigate down to TypeScript Empty AppHost
        // Template order: Starter App, Starter App (React), Starter App (Express/React),
        //                 Starter App (Python/React), Empty AppHost (TypeScript), Empty AppHost, ...
        await auto.DownAsync();
        await auto.DownAsync();
        await auto.DownAsync();
        await auto.DownAsync();

        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("TypeScript").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(10),
            description: "TypeScript template option");
        await auto.EnterAsync();

        // Project name prompt
        await auto.WaitUntilTextAsync("Project name", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("TsValidationApp");
        await auto.EnterAsync();

        // Output path prompt — accept default
        await auto.WaitUntilTextAsync("Output path", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        // URLs prompt — accept default
        await auto.WaitUntilTextAsync("URLs", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        // Handle agent init prompt (decline)
        await auto.DeclineAgentInitPromptAsync(counter, TimeSpan.FromMinutes(3));

        // Run the TypeScript project with aspire run
        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "TsValidationApp"), counter);

        await auto.TypeAsync("aspire run");
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
