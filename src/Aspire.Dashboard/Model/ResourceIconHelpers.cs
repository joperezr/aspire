// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Aspire.Dashboard.Model;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FluentUI.AspNetCore.Components;
using Icons = Microsoft.FluentUI.AspNetCore.Components.Icons;

internal static class ResourceIconHelpers
{
    /// <summary>
    /// Maps a resource to a default icon.
    /// </summary>
    public static Icon GetIconForResource(ResourceViewModel resource, IconSize desiredSize, IconVariant desiredVariant = IconVariant.Filled)
    {
        var icon = resource.ResourceType switch
        {
            KnownResourceTypes.Executable => IconResolver.ResolveIconName("SettingsCogMultiple", desiredSize, desiredVariant),
            KnownResourceTypes.Project => IconResolver.ResolveIconName("CodeCircle", desiredSize, desiredVariant),
            KnownResourceTypes.Container => IconResolver.ResolveIconName("Box", desiredSize, desiredVariant),
            KnownResourceTypes.Parameter => IconResolver.ResolveIconName("Settings", desiredSize, desiredVariant),
            KnownResourceTypes.ConnectionString => IconResolver.ResolveIconName("PlugConnectedSettings", desiredSize, desiredVariant),
            KnownResourceTypes.ExternalService => IconResolver.ResolveIconName("CloudArrowUp", desiredSize, desiredVariant),
            string t when t.Contains("database", StringComparison.OrdinalIgnoreCase) => IconResolver.ResolveIconName("Database", desiredSize, desiredVariant),
            _ => IconResolver.ResolveIconName("SettingsCogMultiple", desiredSize, desiredVariant),
        };

        if (icon == null)
        {
            throw new InvalidOperationException($"Couldn't resolve resource icon for {resource.Name}.");
        }

        return icon;
    }

    public static (Icon? icon, Color color) GetHealthStatusIcon(HealthStatus? healthStatus)
    {
        return healthStatus switch
        {
            HealthStatus.Healthy => (new Icons.Filled.Size16.Heart(), Color.Success),
            HealthStatus.Degraded => (new Icons.Filled.Size16.HeartBroken(), Color.Warning),
            HealthStatus.Unhealthy => (new Icons.Filled.Size16.HeartBroken(), Color.Error),
            _ => (new Icons.Regular.Size16.CircleHint(), Color.Info)
        };
    }
}
