using EmployeesApp.Models;
using EmployeesApp.Repository;
using EmployeesApp.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesApp.Controllers;
[Route("[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _repo;
    private readonly AccountNumberValidation _validation;
    public EmployeesController(IEmployeeService repo)
    {
        _repo = repo;
        _validation = new AccountNumberValidation();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetEmployees()
    {
        var employees = _repo.GetAll();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public ActionResult<Employee> GetEmployeeById(Guid id)
    {
        Employee? employee = _repo.GetEmployee(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!_validation.IsValid(employee.AccountNumber!))
        {
            ModelState.AddModelError("AccountNumber", "Account Number is invalid");
            return BadRequest(ModelState);
        }
        _repo.CreateEmployee(employee);
        return CreatedAtAction("GetEmployeeById", new { id = employee.Id }, employee);
    }
}