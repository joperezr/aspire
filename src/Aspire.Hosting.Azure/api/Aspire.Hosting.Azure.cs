//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Aspire.Hosting
{
    public static partial class AzureBicepResourceExtensions
    {
        public static ApplicationModel.IResourceBuilder<Azure.AzureBicepResource> AddBicepTemplate(this IDistributedApplicationBuilder builder, string name, string bicepFile) { throw null; }

        public static ApplicationModel.IResourceBuilder<Azure.AzureBicepResource> AddBicepTemplateString(this IDistributedApplicationBuilder builder, string name, string bicepContent) { throw null; }

        public static Azure.BicepOutputReference GetOutput(this ApplicationModel.IResourceBuilder<Azure.AzureBicepResource> builder, string name) { throw null; }

        public static Azure.BicepSecretOutputReference GetSecretOutput(this ApplicationModel.IResourceBuilder<Azure.AzureBicepResource> builder, string name) { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithEnvironment<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, Azure.BicepOutputReference bicepOutputReference)
            where T : ApplicationModel.IResourceWithEnvironment { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithEnvironment<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, Azure.BicepSecretOutputReference bicepOutputReference)
            where T : ApplicationModel.IResourceWithEnvironment { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, ApplicationModel.EndpointReference value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, ApplicationModel.IResourceBuilder<ApplicationModel.IResourceWithConnectionString> value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource> value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, ApplicationModel.ParameterResource value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, ApplicationModel.ReferenceExpression value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, Azure.BicepOutputReference value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, System.Collections.Generic.IEnumerable<string> value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, System.Func<object?> valueCallback)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, string value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, System.Text.Json.Nodes.JsonNode value)
            where T : Azure.AzureBicepResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> WithParameter<T>(this ApplicationModel.IResourceBuilder<T> builder, string name)
            where T : Azure.AzureBicepResource { throw null; }
    }

    public static partial class AzureProvisionerExtensions
    {
        public static IDistributedApplicationBuilder AddAzureProvisioning(this IDistributedApplicationBuilder builder) { throw null; }
    }

    public static partial class AzureProvisioningResourceExtensions
    {
        public static ApplicationModel.IResourceBuilder<Azure.AzureProvisioningResource> AddAzureInfrastructure(this IDistributedApplicationBuilder builder, string name, System.Action<Azure.AzureResourceInfrastructure> configureInfrastructure) { throw null; }

        public static global::Azure.Provisioning.ProvisioningParameter AsProvisioningParameter(this ApplicationModel.EndpointReference endpointReference, Azure.AzureResourceInfrastructure infrastructure, string parameterName) { throw null; }

        public static global::Azure.Provisioning.ProvisioningParameter AsProvisioningParameter(this ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource> parameterResourceBuilder, Azure.AzureResourceInfrastructure infrastructure, string? parameterName = null) { throw null; }

        public static global::Azure.Provisioning.ProvisioningParameter AsProvisioningParameter(this ApplicationModel.ParameterResource parameterResource, Azure.AzureResourceInfrastructure infrastructure, string? parameterName = null) { throw null; }

        public static global::Azure.Provisioning.ProvisioningParameter AsProvisioningParameter(this ApplicationModel.ReferenceExpression expression, Azure.AzureResourceInfrastructure infrastructure, string parameterName) { throw null; }

        public static global::Azure.Provisioning.ProvisioningParameter AsProvisioningParameter(this Azure.BicepOutputReference outputReference, Azure.AzureResourceInfrastructure infrastructure, string? parameterName = null) { throw null; }

        public static ApplicationModel.IResourceBuilder<T> ConfigureInfrastructure<T>(this ApplicationModel.IResourceBuilder<T> builder, System.Action<Azure.AzureResourceInfrastructure> configure)
            where T : Azure.AzureProvisioningResource { throw null; }
    }

    public static partial class AzureResourceExtensions
    {
        public static string GetBicepIdentifier(this ApplicationModel.IAzureResource resource) { throw null; }

        public static ApplicationModel.IResourceBuilder<T> PublishAsConnectionString<T>(this ApplicationModel.IResourceBuilder<T> builder)
            where T : ApplicationModel.IAzureResource, ApplicationModel.IResourceWithConnectionString { throw null; }
    }

    public static partial class ExistingAzureResourceExtensions
    {
        public static ApplicationModel.IResourceBuilder<T> AsExisting<T>(this ApplicationModel.IResourceBuilder<T> builder, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource> nameParameter, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource>? resourceGroupParameter)
            where T : ApplicationModel.IAzureResource { throw null; }

        public static bool IsExisting(this ApplicationModel.IResource resource) { throw null; }

        public static ApplicationModel.IResourceBuilder<T> PublishAsExisting<T>(this ApplicationModel.IResourceBuilder<T> builder, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource> nameParameter, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource>? resourceGroupParameter)
            where T : ApplicationModel.IAzureResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> PublishAsExisting<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, string? resourceGroup)
            where T : ApplicationModel.IAzureResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> RunAsExisting<T>(this ApplicationModel.IResourceBuilder<T> builder, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource> nameParameter, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource>? resourceGroupParameter)
            where T : ApplicationModel.IAzureResource { throw null; }

        public static ApplicationModel.IResourceBuilder<T> RunAsExisting<T>(this ApplicationModel.IResourceBuilder<T> builder, string name, string? resourceGroup)
            where T : ApplicationModel.IAzureResource { throw null; }
    }
}

namespace Aspire.Hosting.ApplicationModel
{
    public partial interface IAzureResource : IResource
    {
        System.Threading.Tasks.TaskCompletionSource? ProvisioningTaskCompletionSource { get; set; }
    }
}

namespace Aspire.Hosting.Azure
{
    public partial class AppIdentityAnnotation : ApplicationModel.IResourceAnnotation
    {
        public AppIdentityAnnotation(IAppIdentityResource identityResource) { }

        public IAppIdentityResource IdentityResource { get { throw null; } }
    }

    public sealed partial class AspireV8ResourceNamePropertyResolver : global::Azure.Provisioning.Primitives.DynamicResourceNamePropertyResolver
    {
        public override global::Azure.Provisioning.BicepValue<string>? ResolveName(global::Azure.Provisioning.ProvisioningBuildOptions options, global::Azure.Provisioning.Primitives.ProvisionableResource resource, global::Azure.Provisioning.Primitives.ResourceNameRequirements requirements) { throw null; }
    }

    public partial class AzureBicepResource : ApplicationModel.Resource, ApplicationModel.IAzureResource, ApplicationModel.IResource
    {
        public AzureBicepResource(string name, string? templateFile = null, string? templateString = null, string? templateResourceName = null) : base(default!) { }

        public System.Collections.Generic.Dictionary<string, object?> Outputs { get { throw null; } }

        public System.Collections.Generic.Dictionary<string, object?> Parameters { get { throw null; } }

        public System.Threading.Tasks.TaskCompletionSource? ProvisioningTaskCompletionSource { get { throw null; } set { } }

        public AzureBicepResourceScope? Scope { get { throw null; } set { } }

        public System.Collections.Generic.Dictionary<string, string?> SecretOutputs { get { throw null; } }

        public virtual BicepTemplateFile GetBicepTemplateFile(string? directory = null, bool deleteTemporaryFileOnDispose = true) { throw null; }

        public virtual string GetBicepTemplateString() { throw null; }

        public virtual void WriteToManifest(Publishing.ManifestPublishingContext context) { }

        public static partial class KnownParameters
        {
            public static readonly string KeyVaultName;
            public static readonly string Location;
            public static readonly string LogAnalyticsWorkspaceId;
            public static readonly string PrincipalId;
            public static readonly string PrincipalName;
            public static readonly string PrincipalType;
            public static readonly string UserPrincipalId;
        }
    }

    public partial class AzureBicepResourceAnnotation : ApplicationModel.IResourceAnnotation
    {
        public AzureBicepResourceAnnotation(AzureBicepResource resource) { }

        public AzureBicepResource Resource { get { throw null; } }
    }

    public sealed partial class AzureBicepResourceScope
    {
        public AzureBicepResourceScope(object resourceGroup) { }

        public object ResourceGroup { get { throw null; } }
    }

    public sealed partial class AzureProvisioningOptions
    {
        public global::Azure.Provisioning.ProvisioningBuildOptions ProvisioningBuildOptions { get { throw null; } }

        public bool SupportsTargetedRoleAssignments { get { throw null; } set { } }
    }

    public partial class AzureProvisioningResource : AzureBicepResource
    {
        public AzureProvisioningResource(string name, System.Action<AzureResourceInfrastructure> configureInfrastructure) : base(default!, default, default, default) { }

        public System.Action<AzureResourceInfrastructure> ConfigureInfrastructure { get { throw null; } }

        public global::Azure.Provisioning.ProvisioningBuildOptions? ProvisioningBuildOptions { get { throw null; } set { } }

        public virtual global::Azure.Provisioning.Primitives.ProvisionableResource AddAsExistingResource(AzureResourceInfrastructure infra) { throw null; }

        public virtual void AddRoleAssignments(IAddRoleAssignmentsContext roleAssignmentContext) { }

        public static T CreateExistingOrNewProvisionableResource<T>(AzureResourceInfrastructure infrastructure, System.Func<string, global::Azure.Provisioning.BicepValue<string>, T> createExisting, System.Func<AzureResourceInfrastructure, T> createNew)
            where T : global::Azure.Provisioning.Primitives.ProvisionableResource { throw null; }

        public override BicepTemplateFile GetBicepTemplateFile(string? directory = null, bool deleteTemporaryFileOnDispose = true) { throw null; }

        public override string GetBicepTemplateString() { throw null; }
    }

    public static partial class AzurePublisherExtensions
    {
        [System.Diagnostics.CodeAnalysis.Experimental("ASPIREAZURE001", UrlFormat = "https://aka.ms/dotnet/aspire/diagnostics#{0}")]
        public static void AddAzurePublisher(this IDistributedApplicationBuilder builder, System.Action<AzurePublisherOptions>? configureOptions = null) { }

        [System.Diagnostics.CodeAnalysis.Experimental("ASPIREAZURE001", UrlFormat = "https://aka.ms/dotnet/aspire/diagnostics#{0}")]
        public static void AddAzurePublisher(this IDistributedApplicationBuilder builder, string name, System.Action<AzurePublisherOptions>? configureOptions = null) { }
    }

    [System.Diagnostics.CodeAnalysis.Experimental("ASPIREAZURE001", UrlFormat = "https://aka.ms/dotnet/aspire/diagnostics#{0}")]
    public sealed partial class AzurePublisherOptions : Publishing.PublishingOptions
    {
    }

    public sealed partial class AzureResourceInfrastructure : global::Azure.Provisioning.Infrastructure
    {
        internal AzureResourceInfrastructure() : base(default!) { }

        public AzureProvisioningResource AspireResource { get { throw null; } }
    }

    public sealed partial class BicepOutputReference : ApplicationModel.IManifestExpressionProvider, ApplicationModel.IValueProvider, ApplicationModel.IValueWithReferences
    {
        public BicepOutputReference(string name, AzureBicepResource resource) { }

        System.Collections.Generic.IEnumerable<object> ApplicationModel.IValueWithReferences.References { get { throw null; } }

        public string Name { get { throw null; } }

        public AzureBicepResource Resource { get { throw null; } }

        public string? Value { get { throw null; } }

        public string ValueExpression { get { throw null; } }

        public System.Threading.Tasks.ValueTask<string?> GetValueAsync(System.Threading.CancellationToken cancellationToken = default) { throw null; }
    }

    public sealed partial class BicepSecretOutputReference : ApplicationModel.IManifestExpressionProvider, ApplicationModel.IValueProvider, ApplicationModel.IValueWithReferences
    {
        public BicepSecretOutputReference(string name, AzureBicepResource resource) { }

        System.Collections.Generic.IEnumerable<object> ApplicationModel.IValueWithReferences.References { get { throw null; } }

        public string Name { get { throw null; } }

        public AzureBicepResource Resource { get { throw null; } }

        public string? Value { get { throw null; } }

        public string ValueExpression { get { throw null; } }

        public System.Threading.Tasks.ValueTask<string?> GetValueAsync(System.Threading.CancellationToken cancellationToken = default) { throw null; }
    }

    public readonly partial struct BicepTemplateFile : System.IDisposable
    {
        private readonly object _dummy;
        private readonly int _dummyPrimitive;
        public BicepTemplateFile(string path, bool deleteFileOnDispose) { }

        public string Path { get { throw null; } }

        public readonly void Dispose() { }
    }

    public partial class DefaultRoleAssignmentsAnnotation : ApplicationModel.IResourceAnnotation
    {
        public DefaultRoleAssignmentsAnnotation(System.Collections.Generic.IReadOnlySet<RoleDefinition> roles) { }

        public System.Collections.Generic.IReadOnlySet<RoleDefinition> Roles { get { throw null; } }
    }

    public sealed partial class ExistingAzureResourceAnnotation : ApplicationModel.IResourceAnnotation
    {
        public ExistingAzureResourceAnnotation(object name, object? resourceGroup = null) { }

        public object Name { get { throw null; } }

        public object? ResourceGroup { get { throw null; } }
    }

    public partial interface IAddRoleAssignmentsContext
    {
        AzureResourceInfrastructure Infrastructure { get; }

        global::Azure.Provisioning.BicepValue<System.Guid> PrincipalId { get; }

        global::Azure.Provisioning.BicepValue<string> PrincipalName { get; }

        global::Azure.Provisioning.BicepValue<global::Azure.Provisioning.Authorization.RoleManagementPrincipalType> PrincipalType { get; }

        System.Collections.Generic.IEnumerable<RoleDefinition> Roles { get; }
    }

    public partial interface IAppIdentityResource
    {
        BicepOutputReference ClientId { get; }

        BicepOutputReference Id { get; }

        BicepOutputReference PrincipalId { get; }

        BicepOutputReference PrincipalName { get; }
    }

    public partial interface IKeyVaultResource : ApplicationModel.IResource, ApplicationModel.IAzureResource
    {
        BicepOutputReference NameOutputReference { get; }

        System.Func<string, System.Threading.CancellationToken, System.Threading.Tasks.Task<string?>>? SecretResolver { get; set; }

        BicepOutputReference VaultUriOutputReference { get; }

        IKeyVaultSecretReference GetSecretReference(string secretName);
    }

    public partial interface IKeyVaultSecretReference : ApplicationModel.IValueProvider, ApplicationModel.IManifestExpressionProvider
    {
        IKeyVaultResource Resource { get; }

        string SecretName { get; }
    }

    public partial interface IResourceWithAzureFunctionsConfig : ApplicationModel.IResource
    {
        void ApplyAzureFunctionsConfiguration(System.Collections.Generic.IDictionary<string, object> target, string connectionName);
    }

    public partial class RoleAssignmentAnnotation : ApplicationModel.IResourceAnnotation
    {
        public RoleAssignmentAnnotation(AzureProvisioningResource target, System.Collections.Generic.IReadOnlySet<RoleDefinition> roles) { }

        public System.Collections.Generic.IReadOnlySet<RoleDefinition> Roles { get { throw null; } }

        public AzureProvisioningResource Target { get { throw null; } }
    }

    public partial struct RoleDefinition : System.IEquatable<RoleDefinition>
    {
        private object _dummy;
        private int _dummyPrimitive;
        public RoleDefinition(string Id, string Name) { }

        public string Id { get { throw null; } set { } }

        public string Name { get { throw null; } set { } }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public readonly void Deconstruct(out string Id, out string Name) { throw null; }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public readonly bool Equals(RoleDefinition other) { throw null; }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public override readonly bool Equals(object obj) { throw null; }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public override readonly int GetHashCode() { throw null; }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public static bool operator ==(RoleDefinition left, RoleDefinition right) { throw null; }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public static bool operator !=(RoleDefinition left, RoleDefinition right) { throw null; }

        [System.Runtime.CompilerServices.CompilerGenerated]
        public override readonly string ToString() { throw null; }
    }
}