using EmployeesApp.Models;

namespace EmployeesApp.Repository;

public interface IEmployeeService
{
    IEnumerable<Employee> GetAll();
    Employee? GetEmployee(Guid id);
    void CreateEmployee(Employee employee);
}