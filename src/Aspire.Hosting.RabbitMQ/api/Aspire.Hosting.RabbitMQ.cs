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
    public static partial class RabbitMQBuilderExtensions
    {
        public static ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> AddRabbitMQ(this IDistributedApplicationBuilder builder, string name, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource>? userName = null, ApplicationModel.IResourceBuilder<ApplicationModel.ParameterResource>? password = null, int? port = null) { throw null; }

        public static ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> WithDataBindMount(this ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> builder, string source, bool isReadOnly = false) { throw null; }

        public static ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> WithDataVolume(this ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> builder, string? name = null, bool isReadOnly = false) { throw null; }

        public static ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> WithManagementPlugin(this ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> builder, int? port) { throw null; }

        public static ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> WithManagementPlugin(this ApplicationModel.IResourceBuilder<ApplicationModel.RabbitMQServerResource> builder) { throw null; }
    }
}

namespace Aspire.Hosting.ApplicationModel
{
    public partial class RabbitMQServerResource : ContainerResource, IResourceWithConnectionString, IResource, IManifestExpressionProvider, IValueProvider, IValueWithReferences, IResourceWithEnvironment
    {
        public RabbitMQServerResource(string name, ParameterResource? userName, ParameterResource password) : base(default!, default) { }

        public ReferenceExpression ConnectionStringExpression { get { throw null; } }

        public ParameterResource PasswordParameter { get { throw null; } }

        public EndpointReference PrimaryEndpoint { get { throw null; } }

        public ParameterResource? UserNameParameter { get { throw null; } }
    }
}