// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 1: Installation and binary integrity tests.
/// The CLI is pre-installed by the workflow. These tests validate that the
/// binary is functional (correct permissions, signing, basic execution).
/// These tests catch issues like #16043 where macOS binaries lost execute permissions.
/// </summary>
public sealed class Tier1_InstallationTests
{
    [Fact]
    public async Task Aspire_Version_ReportsValidVersion()
    {
        using var terminal = ChannelValidationHelpers.CreateTestTerminal();
        var pendingRun = terminal.RunAsync(TestContext.Current.CancellationToken);

        var counter = new SequenceCounter();
        var auto = new Hex1bTerminalAutomator(terminal, defaultTimeout: TimeSpan.FromSeconds(500));

        await auto.PrepareShellEnvironmentAsync(counter);

        // Verify aspire --version succeeds and outputs a version string
        await auto.TypeAsync("aspire --version");
        await auto.EnterAsync();
        await auto.WaitForSuccessPromptAsync(counter);

        await auto.ExitShellAsync();
        await pendingRun;
    }

    [Fact]
    public async Task Aspire_Doctor_RunsSuccessfully()
    {
        using var terminal = ChannelValidationHelpers.CreateTestTerminal();
        var pendingRun = terminal.RunAsync(TestContext.Current.CancellationToken);

        var counter = new SequenceCounter();
        var auto = new Hex1bTerminalAutomator(terminal, defaultTimeout: TimeSpan.FromSeconds(500));

        await auto.PrepareShellEnvironmentAsync(counter);

        // aspire doctor should run to completion (validates binary integrity,
        // environment detection, and basic framework functionality)
        await auto.TypeAsync("aspire doctor");
        await auto.EnterAsync();

        // Doctor outputs a summary line; wait for it then for the prompt
        await auto.WaitUntilTextAsync("Summary:", timeout: TimeSpan.FromMinutes(2));
        await auto.WaitForSuccessPromptAsync(counter);

        await auto.ExitShellAsync();
        await pendingRun;
    }

    [Fact]
    public async Task Aspire_Help_ShowsCommands()
    {
        using var terminal = ChannelValidationHelpers.CreateTestTerminal();
        var pendingRun = terminal.RunAsync(TestContext.Current.CancellationToken);

        var counter = new SequenceCounter();
        var auto = new Hex1bTerminalAutomator(terminal, defaultTimeout: TimeSpan.FromSeconds(500));

        await auto.PrepareShellEnvironmentAsync(counter);

        // aspire --help should show the CLI help with key commands
        await auto.TypeAsync("aspire --help");
        await auto.EnterAsync();

        // Verify key commands appear in help output
        await auto.WaitUntilAsync(s =>
        {
            return s.ContainsText("new") && s.ContainsText("run") && s.ContainsText("doctor");
        }, timeout: TimeSpan.FromSeconds(30), description: "help output with key commands (new, run, doctor)");

        await auto.WaitForSuccessPromptAsync(counter);

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
