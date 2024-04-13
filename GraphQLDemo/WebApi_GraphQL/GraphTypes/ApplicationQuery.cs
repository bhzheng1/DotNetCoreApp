using GraphQL.Types;

namespace WebApi_GraphQL.GraphTypes
{
    public class ApplicationQuery : ObjectGraphType
    {
        public ApplicationQuery()
        {
            Name = "query";
            Field<EmployeeGroupGraphType>("employeeGroup").Resolve(context => new { });
        }
    }
}