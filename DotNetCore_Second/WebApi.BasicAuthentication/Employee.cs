using System.Collections.Generic;

namespace WebApi.BasicAuthentication
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Dept { get; set; }
        public int Salary { get; set; }
    }
    public class EmployeeBL
    {
        public List<Employee> GetEmployees()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we hardcoded the data
            List<Employee> empList = new List<Employee>();
            for (int i = 0; i < 10; i++)
            {
                if (i > 5)
                {
                    empList.Add(new Employee()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Dept = "IT",
                        Salary = 1000 + i,
                        Gender = "Male"
                    });
                }
                else
                {
                    empList.Add(new Employee()
                    {
                        ID = i,
                        Name = "Name" + i,
                        Dept = "HR",
                        Salary = 1000 + i,
                        Gender = "Female"
                    });
                }
            }
            return empList;
        }
    }
}
