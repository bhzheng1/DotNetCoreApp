using GraphQL.Types;
using WebApi_GraphQL.GraphTypes;

namespace WebApi_GraphQL
{
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<ApplicationQuery>();
        }
    }
}