//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Aspire.Azure.Storage.Blobs
{
    public sealed partial class AzureStorageBlobsSettings
    {
        public string? ConnectionString { get { throw null; } set { } }

        public global::Azure.Core.TokenCredential? Credential { get { throw null; } set { } }

        public bool DisableHealthChecks { get { throw null; } set { } }

        public bool DisableTracing { get { throw null; } set { } }

        public System.Uri? ServiceUri { get { throw null; } set { } }
    }
}

namespace Microsoft.Extensions.Hosting
{
    public static partial class AspireBlobStorageExtensions
    {
        public static void AddAzureBlobClient(this IHostApplicationBuilder builder, string connectionName, System.Action<Aspire.Azure.Storage.Blobs.AzureStorageBlobsSettings>? configureSettings = null, System.Action<global::Azure.Core.Extensions.IAzureClientBuilder<global::Azure.Storage.Blobs.BlobServiceClient, global::Azure.Storage.Blobs.BlobClientOptions>>? configureClientBuilder = null) { }

        public static void AddKeyedAzureBlobClient(this IHostApplicationBuilder builder, string name, System.Action<Aspire.Azure.Storage.Blobs.AzureStorageBlobsSettings>? configureSettings = null, System.Action<global::Azure.Core.Extensions.IAzureClientBuilder<global::Azure.Storage.Blobs.BlobServiceClient, global::Azure.Storage.Blobs.BlobClientOptions>>? configureClientBuilder = null) { }
    }
}