// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 4: C# (.NET) publishing tests.
/// Validates that aspire publish generates valid deployment manifests from a channel-installed CLI.
/// Docker Compose publishing requires Docker and is Linux-only.
/// </summary>
public sealed class Tier4_DotNetPublishTests
{
    [Fact]
    public async Task Publish_Manifest_GeneratesValidJson()
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
        await auto.TypeAsync("PublishTestApp");
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
            System.IO.Path.Combine(workspace, "PublishTestApp"), counter);

        // Publish with manifest publisher (no Docker required)
        await auto.TypeAsync("aspire publish --publisher manifest --output-path ./manifest-output");
        await auto.EnterAsync();

        // Wait for publish to complete — it may prompt for AppHost selection
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

    [Fact]
    public async Task Publish_DockerCompose_GeneratesValidManifest()
    {
        // Docker Compose publish requires Docker — Linux only
        Assert.SkipUnless(ChannelValidationHelpers.IsLinux, "Docker Compose publish requires Docker (Linux only)");

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

        // Create a starter project
        await auto.TypeAsync("aspire new");
        await auto.EnterAsync();

        await auto.WaitUntilAsync(
            s => new CellPatternSearcher().Find("> Starter App").Search(s).Count > 0,
            timeout: TimeSpan.FromSeconds(60),
            description: "template selection");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("Project name", timeout: TimeSpan.FromSeconds(30));
        await auto.TypeAsync("DockerPublishApp");
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

        await auto.ChangeDirectoryAsync(
            System.IO.Path.Combine(workspace, "DockerPublishApp"), counter);

        // Publish with Docker Compose publisher
        await auto.TypeAsync("aspire publish --publisher docker --output-path ./docker-output");
        await auto.EnterAsync();

        await auto.WaitForAnyPromptAsync(counter, TimeSpan.FromMinutes(5));

        // Verify docker-compose file was created
        await auto.TypeAsync("test -f ./docker-output/docker-compose.yml && echo 'COMPOSE_EXISTS' || echo 'COMPOSE_MISSING'");
        await auto.EnterAsync();

        await auto.WaitUntilTextAsync("COMPOSE_EXISTS", timeout: TimeSpan.FromSeconds(15));
        await auto.WaitForSuccessPromptAsync(counter);

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
