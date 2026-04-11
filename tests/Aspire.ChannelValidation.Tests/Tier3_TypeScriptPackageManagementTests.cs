// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.ChannelValidation.Tests.Helpers;
using Aspire.Tests.Shared;
using Hex1b.Automation;
using Xunit;

namespace Aspire.ChannelValidation.Tests;

/// <summary>
/// Tier 3: TypeScript (polyglot) package management tests.
/// Validates that aspire add works correctly for TypeScript AppHost projects.
/// </summary>
public sealed class Tier3_TypeScriptPackageManagementTests
{
    [Fact]
    public async Task Add_Integration_TypeScriptProject()
    {
        var workspace = ChannelValidationHelpers.CreateTempWorkspace();

        using var terminal = ChannelValidationHelpers.CreateTestTerminal();
        var pendingRun = terminal.RunAsync(TestContext.Current.CancellationToken);

        var counter = new SequenceCounter();
        var auto = new Hex1bTerminalAutomator(terminal, defaultTimeout: TimeSpan.FromSeconds(500));

        await auto.PrepareShellEnvironmentAsync(counter);
        await auto.ChangeDirectoryAsync(workspace, counter);

        // Create a TypeScript Empty AppHost using non-interactive mode
        await auto.TypeAsync("aspire new aspire-ts-empty --name TsPkgTestApp --output ./TsPkgTestApp --non-interactive");
        await auto.EnterAsync();
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(3));

        // Navigate into the project
        await auto.ChangeDirectoryAsync("TsPkgTestApp", counter);

        // Run aspire add to add an integration to the TS project
        // Use short name 'redis' — '@aspire/hosting-redis' has @ parsed as response file by System.CommandLine
        await auto.TypeAsync("aspire add redis --non-interactive");
        await auto.EnterAsync();

        // Wait for completion — must succeed
        await auto.WaitForSuccessPromptFailFastAsync(counter, TimeSpan.FromMinutes(2));

        await auto.ExitShellAsync();
        await pendingRun;
    }
}
