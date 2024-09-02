using EmployeesApp.Models;

namespace EmployeesApp.Repository;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll();
    Employee? GetEmployee(Guid id);
    void CreateEmployee(Employee employee);
}