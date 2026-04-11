// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Aspire.Tests.Shared;
using Hex1b;
using Hex1b.Automation;

namespace Aspire.ChannelValidation.Tests.Helpers;

/// <summary>
/// Helpers for channel validation smoke tests. Provides cross-platform terminal creation,
/// CLI installation from published channels, and environment detection utilities.
/// </summary>
internal static class ChannelValidationHelpers
{

    /// <summary>
    /// Creates a cross-platform Hex1b terminal suitable for channel validation tests.
    /// On Linux/macOS uses bash, on Windows uses pwsh.exe via hex1bpty.exe proxy.
    /// </summary>
    internal static Hex1bTerminal CreateTestTerminal(
        int width = 160,
        int height = 48,
        [CallerMemberName] string testName = "")
    {
        var recordingPath = Aspire.Tests.Shared.Hex1bTestHelpers.GetTestResultsRecordingPath(testName, "channel-validation");

        var builder = Hex1bTerminal.CreateBuilder()
            .WithHeadless()
            .WithDimensions(width, height)
            .WithAsciinemaRecording(recordingPath);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            builder.WithPtyProcess("pwsh.exe", ["-NoProfile", "-NoLogo"]);
        }
        else
        {
            builder.WithPtyProcess("/bin/bash", ["--norc"]);
        }

        return builder.Build();
    }

    /// <summary>
    /// Prepares the terminal shell environment with prompt counting for sequence detection.
    /// Sets up the [N OK/ERR] $ prompt pattern used by Hex1b helpers.
    /// </summary>
    internal static async Task PrepareShellEnvironmentAsync(
        this Hex1bTerminalAutomator auto,
        Aspire.Tests.Shared.SequenceCounter counter)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            // Wait for PowerShell prompt
            await auto.WaitUntilTextAsync("PS ", timeout: TimeSpan.FromSeconds(30));
            await auto.WaitAsync(500);

            // Set up prompt counting for PowerShell
            // Use $? to detect cmdlet/script failures (not just $LASTEXITCODE for native processes)
            const string pwshPromptSetup = """
                $global:CMDCOUNT = 0; function prompt { $s = $?; $global:CMDCOUNT++; "[$global:CMDCOUNT $(if($s){'OK'}else{"ERR:$LASTEXITCODE"})] $ " }
                """;
            await auto.TypeAsync(pwshPromptSetup);
            await auto.EnterAsync();
            await auto.WaitForSuccessPromptAsync(counter);

            // Disable telemetry
            await auto.TypeAsync("$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'; $env:DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'");
            await auto.EnterAsync();
            await auto.WaitForSuccessPromptAsync(counter);
        }
        else
        {
            // Wait for bash prompt
            await auto.WaitUntilTextAsync("$ ", timeout: TimeSpan.FromSeconds(30));
            await auto.WaitAsync(500);

            // Set up prompt counting for bash
            const string bashPromptSetup = "CMDCOUNT=0; PROMPT_COMMAND='s=$?;((CMDCOUNT++));PS1=\"[$CMDCOUNT $([ $s -eq 0 ] && echo OK || echo ERR:$s)] \\$ \"'";
            await auto.TypeAsync(bashPromptSetup);
            await auto.EnterAsync();
            await auto.WaitForSuccessPromptAsync(counter);

            // Disable telemetry
            await auto.TypeAsync("export DOTNET_CLI_TELEMETRY_OPTOUT=1 DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1");
            await auto.EnterAsync();
            await auto.WaitForSuccessPromptAsync(counter);
        }
    }

    /// <summary>
    /// Returns true if running on Linux (used to gate Docker-dependent tests).
    /// </summary>
    internal static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    /// <summary>
    /// Creates a temporary working directory for project creation tests.
    /// </summary>
    internal static string CreateTempWorkspace()
    {
        var dir = Directory.CreateTempSubdirectory("aspire-channel-validation-");
        return dir.FullName;
    }

    /// <summary>
    /// Changes the shell's working directory to the given path.
    /// </summary>
    internal static async Task ChangeDirectoryAsync(
        this Hex1bTerminalAutomator auto,
        string path,
        Aspire.Tests.Shared.SequenceCounter counter)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            await auto.TypeAsync($"Set-Location '{path}'");
        }
        else
        {
            await auto.TypeAsync($"cd '{path}'");
        }

        await auto.EnterAsync();
        await auto.WaitForSuccessPromptAsync(counter);
    }

    /// <summary>
    /// Exits the shell session gracefully.
    /// </summary>
    internal static async Task ExitShellAsync(this Hex1bTerminalAutomator auto)
    {
        await auto.TypeAsync("exit");
        await auto.EnterAsync();
    }
}
