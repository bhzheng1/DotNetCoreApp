using GraphQL.Types;
using WebApi_GraphQL.Models;

namespace WebApi_GraphQL.GraphTypes;

public class EmployeeDetailsType : ObjectGraphType<EmployeeDetails>
{
    public EmployeeDetailsType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.Age);
        Field(x => x.DeptName);
    }
}