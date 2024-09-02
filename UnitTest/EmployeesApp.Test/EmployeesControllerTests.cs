using EmployeesApp.Controllers;
using EmployeesApp.Models;
using EmployeesApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeesApp.Test;
public class EmployeesControllerTests
{
    private readonly Mock<IEmployeeRepository> _mockRepo;
    private readonly EmployeesController _controller;

    public EmployeesControllerTests()
    {
        _mockRepo = new Mock<IEmployeeRepository>();
        _controller = new EmployeesController(_mockRepo.Object);
    }

    [Fact]
    public void GetEmployees_ActionExecutes_ReturnsOkResultWithEmployees()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetAll())
            .Returns(new List<Employee>() { new Employee(), new Employee() });

        // Act
        var result = _controller.GetEmployees();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var employees = Assert.IsAssignableFrom<IEnumerable<Employee>>(okResult.Value);
        Assert.Equal(2, employees.Count());
    }

    [Fact]
    public void GetEmployeeById_EmployeeNotFound_ReturnsNotFound()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetEmployee(It.IsAny<Guid>()))
            .Returns((Employee)null);

        // Act
        var result = _controller.GetEmployeeById(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void GetEmployeeById_EmployeeFound_ReturnsOkResultWithEmployee()
    {
        // Arrange
        var employee = new Employee { Id = Guid.NewGuid() };
        _mockRepo.Setup(repo => repo.GetEmployee(It.IsAny<Guid>()))
            .Returns(employee);

        // Act
        var result = _controller.GetEmployeeById(employee.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedEmployee = Assert.IsType<Employee>(okResult.Value);
        Assert.Equal(employee.Id, returnedEmployee.Id);
    }

    [Fact]
    public void Create_InvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Name is required");
        var employee = new Employee { Age = 25, AccountNumber = "255-8547963214-41" };

        // Act
        var result = _controller.Create(employee);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }

    [Fact]
    public void Create_InvalidAccountNumber_ReturnsBadRequest()
    {
        // Arrange
        var employee = new Employee { Age = 25, AccountNumber = "invalid-account-number" };

        // Act
        var result = _controller.Create(employee);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }

    [Fact]
    public void Create_ValidModelState_ReturnsCreatedAtAction()
    {
        // Arrange
        var employee = new Employee { Id = Guid.NewGuid(), Age = 25, AccountNumber = "255-8547963214-41" };

        // Act
        var result = _controller.Create(employee);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var createdEmployee = Assert.IsType<Employee>(createdAtActionResult.Value);
        Assert.Equal(employee.Id, createdEmployee.Id);
    }
}
