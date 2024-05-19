using Domain.AggregateModels;

namespace Project.Application;

public class SeedData
{
    public static Domain.AggregateModels.Project GetProject()
    {
        var customer = new Customer() { CompanyName = "test"};
        var project = new Project(1, "Project1", DateTime.Now, customer);
        var employees = GetEmployees();
        project.Members.
        return project;
    }

    private static IList<Employee> GetEmployees()
    {
        return new List<Employee>()
        {
        new Employee(1,"Employee 1", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 2", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 3", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 4", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 5", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 6", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 7", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 8", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 9", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 10", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        new Employee(1,"Employee 11", DateTime.Now.AddMicroseconds(-13), Guid.NewGuid()),
        };
    }
}