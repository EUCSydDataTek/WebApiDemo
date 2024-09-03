using EmployeesApp.Models;

namespace EmployeesApp.Repository;

public class EmployeeService(EmployeeContext context) : IEmployeeService
{
    public IEnumerable<Employee> GetAll() => context.Employees.ToList();

    public Employee? GetEmployee(Guid id) => context.Employees.SingleOrDefault(e => e.Id.Equals(id));

    public void CreateEmployee(Employee employee)
    {
        context.Add(employee);
        context.SaveChanges();
    }
}
