using GraphQL;
using GraphQL.Types;
using WebApi_GraphQL.Services;

namespace WebApi_GraphQL.GraphTypes
{
    public class EmployeeGroupGraphType : ObjectGraphType
    {
        public EmployeeGroupGraphType(IEmployeeService employeeService)
        {
            Field<ListGraphType<EmployeeDetailsType>>("employees").Resolve(context => employeeService.GetEmployees());
            Field<ListGraphType<EmployeeDetailsType>>("employee").Argument<IntGraphType>("id").Resolve(context => employeeService.GetEmployee(context.GetArgument<int>("id")));
        }
    }
}