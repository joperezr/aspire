// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 4: TypeScript (polyglot) publishing tests.
/// Validates that aspire publish generates valid deployment manifests for TypeScript projects.
/// </summary>
public sealed class Tier4_TypeScriptPublishTests
{
    [Fact]
    public async Task Publish_Manifest_TypeScriptProject()
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

        // Create a TypeScript Empty AppHost
        await auto.TypeAsync("aspire new");
        await auto.EnterAsync();

        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("> Starter App").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(60),
            description: "template selection");

        // Navigate to TypeScript Empty AppHost
        await auto.DownAsync();
        await auto.DownAsync();
        await auto.DownAsync();
        await auto.DownAsync();

        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("TypeScript").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(10),
            description: "TypeScript template option");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("Project name", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("TsPublishApp");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("Output path", timeout: TimeSpan.FromSeconds(30));
        await auto.EnterAsync();

        await auto.DeclineAgentInitPromptAsync(counter, TimeSpan.FromMinutes(3));

        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "TsPublishApp"), counter);

        // Publish with manifest publisher
        await auto.TypeAsync("aspire publish --publisher manifest --output-path ./manifest-output");
        await auto.EnterAsync();

        await auto.WaitForAnyPromptAsync(counter, TimeSpan.FromMinutes(3));

        // Verify the manifest file was created
        if (ChannelValidationHelpers.IsLinux || System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
        {
            await auto.TypeAsync("test -f ./manifest-output/manifest.json && echo 'MANIFEST_EXISTS' || echo 'MANIFEST_MISSING'");
        }
        else
        {
            await auto.TypeAsync("if (Test-Path ./manifest-output/manifest.json) { echo 'MANIFEST_EXISTS' } else { echo 'MANIFEST_MISSING' }");
        }
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("MANIFEST_EXISTS", timeout: TimeSpan.FromSeconds(15));
        await auto.WaitForSuccessPromptAsync(counter);

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
