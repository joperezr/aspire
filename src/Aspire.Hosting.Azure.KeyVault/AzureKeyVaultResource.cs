// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Hosting.ApplicationModel;
using Azure.Provisioning.KeyVault;
using Azure.Provisioning.Primitives;

namespace Aspire.Hosting.Azure;

/// <summary>
/// A resource that represents an Azure Key Vault.
/// </summary>
/// <param name="name">The name of the resource.</param>
/// <param name="configureInfrastructure">Callback to configure the Azure resources.</param>
public class AzureKeyVaultResource(string name, Action<AzureResourceInfrastructure> configureInfrastructure)
    : AzureProvisioningResource(name, configureInfrastructure), IResourceWithConnectionString, IAzureKeyVaultResource
{
    /// <summary>
    /// The secrets for this Key Vault.
    /// </summary>
    internal List<AzureKeyVaultSecretResource> Secrets { get; } = [];
    /// <summary>
    /// Gets the "vaultUri" output reference for the Azure Key Vault resource.
    /// </summary>
    public BicepOutputReference VaultUri => new("vaultUri", this);

    /// <summary>
    /// Gets the "name" output reference for the Azure Key Vault resource.
    /// </summary>
    public BicepOutputReference NameOutputReference => new("name", this);

    /// <summary>
    /// Gets the connection string template for the manifest for the Azure Key Vault resource.
    /// </summary>
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create($"{VaultUri}");

    BicepOutputReference IAzureKeyVaultResource.VaultUriOutputReference => VaultUri;

    // In run mode, this is set to the secret client used to access the Azure Key Vault.
    internal Func<IAzureKeyVaultSecretReference, CancellationToken, Task<string?>>? SecretResolver { get; set; }

    Func<IAzureKeyVaultSecretReference, CancellationToken, Task<string?>>? IAzureKeyVaultResource.SecretResolver
    {
        get => SecretResolver;
        set => SecretResolver = value;
    }

    /// <summary>
    /// Gets a secret reference for the specified secret name.
    /// </summary>
    /// <param name="secretName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public IAzureKeyVaultSecretReference GetSecret(string secretName)
    {
        ArgumentException.ThrowIfNullOrEmpty(secretName, nameof(secretName));

        return new AzureKeyVaultSecretReference(secretName, this);
    }

    /// <inheritdoc/>
    public override ProvisionableResource AddAsExistingResource(AzureResourceInfrastructure infra)
    {
        var bicepIdentifier = this.GetBicepIdentifier();
        var resources = infra.GetProvisionableResources();
        
        // Check if a KeyVaultService with the same identifier already exists
        var existingStore = resources.OfType<KeyVaultService>().SingleOrDefault(store => store.BicepIdentifier == bicepIdentifier);
        
        if (existingStore is not null)
        {
            return existingStore;
        }
        
        // Create and add new resource if it doesn't exist
        var store = KeyVaultService.FromExisting(bicepIdentifier);
        store.Name = NameOutputReference.AsProvisioningParameter(infra);
        infra.Add(store);
        return store;
    }
}
