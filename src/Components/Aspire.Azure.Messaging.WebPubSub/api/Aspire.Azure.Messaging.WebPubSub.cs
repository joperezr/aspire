//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Aspire.Azure.Messaging.WebPubSub
{
    public sealed partial class AzureMessagingWebPubSubSettings
    {
        public string? ConnectionString { get { throw null; } set { } }

        public global::Azure.Core.TokenCredential? Credential { get { throw null; } set { } }

        public bool DisableHealthChecks { get { throw null; } set { } }

        public bool DisableTracing { get { throw null; } set { } }

        public System.Uri? Endpoint { get { throw null; } set { } }

        public string? HubName { get { throw null; } set { } }
    }
}

namespace Microsoft.Extensions.Hosting
{
    public static partial class AspireWebPubSubExtensions
    {
        public static void AddAzureWebPubSubServiceClient(this IHostApplicationBuilder builder, string connectionName, System.Action<Aspire.Azure.Messaging.WebPubSub.AzureMessagingWebPubSubSettings>? configureSettings = null, System.Action<global::Azure.Core.Extensions.IAzureClientBuilder<global::Azure.Messaging.WebPubSub.WebPubSubServiceClient, global::Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions>>? configureClientBuilder = null) { }

        public static void AddKeyedAzureWebPubSubServiceClient(this IHostApplicationBuilder builder, string connectionName, System.Action<Aspire.Azure.Messaging.WebPubSub.AzureMessagingWebPubSubSettings>? configureSettings = null, System.Action<global::Azure.Core.Extensions.IAzureClientBuilder<global::Azure.Messaging.WebPubSub.WebPubSubServiceClient, global::Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions>>? configureClientBuilder = null) { }

        public static void AddKeyedAzureWebPubSubServiceClient(this IHostApplicationBuilder builder, string connectionName, string serviceKey, System.Action<Aspire.Azure.Messaging.WebPubSub.AzureMessagingWebPubSubSettings>? configureSettings = null, System.Action<global::Azure.Core.Extensions.IAzureClientBuilder<global::Azure.Messaging.WebPubSub.WebPubSubServiceClient, global::Azure.Messaging.WebPubSub.WebPubSubServiceClientOptions>>? configureClientBuilder = null) { }
    }
}