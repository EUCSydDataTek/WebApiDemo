using EmployeesApp.Models;
using System.Net;
using System.Net.Http.Json;

namespace EmployeesApp.IntegrationTests;

public class EmployeesControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public EmployeesControllerIntegrationTests(TestingWebAppFactory<Program> factory)
        => _client = factory.CreateClient();

    [Fact]
    public async Task GetEmployees_WhenCalled_ReturnsEmployees()
    {
        // Act
        var response = await _client.GetAsync("/Employees");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("Mark", responseString);
        Assert.Contains("Evelin", responseString);
    }

    [Fact]
    public async Task GetEmployeeById_WhenCalled_ReturnsEmployee()
    {
        // Arrange
        var employee = new Employee()
        {
            Name = "New Employee",
            Age = 25,
            AccountNumber = "214-5874986532-21"
        };
        var response = await _client.PostAsJsonAsync("/Employees", employee);
        employee = await response.Content.ReadFromJsonAsync<Employee>();

        // Act
        var result = await _client.GetAsync($"/Employees/{employee!.Id}");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("New Employee", responseString);
    }

    [Fact]
    public async Task Create_WhenPOSTExecuted_ReturnsWithCreatedEmployee()
    {
        // Arrange
        var employee = new Employee()
        {
            Name = "New Employee",
            Age = 25,
            AccountNumber = "214-5874986532-21"
        };

        // Act 
        var response = await _client.PostAsJsonAsync("/Employees", employee);

        response.EnsureSuccessStatusCode();

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("New Employee", responseString);
        Assert.Contains("214-5874986532-21", responseString);
    }

    [Fact]
    public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
    {
        // Arrange
        var employee = new Employee()
        {
            Name = "New Employee",
            Age = 25
        };

        // Act 
        var response = await _client.PostAsJsonAsync("/Employees", employee);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}