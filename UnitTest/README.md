

Ref: [Unit Testing with xUnit in ASP.NET Core](https://code-maze.com/aspnetcore-unit-testing-xunit/)



Ref: [Testing Controllers with Unit Tests and Moq in ASP.NET Core](https://code-maze.com/unit-testing-controllers-aspnetcore-moq/)

## Unit Testing af EmployeesController klassen med Mocking af Repository

1.	Test for GetEmployees method:
    - Ensure it returns the correct type.
    - Ensure it returns the correct number of employees.
2.	Test for GetEmployeeById method:
    - Ensure it returns NotFound when the employee does not exist.
    - Ensure it returns Ok with the correct employee when the employee exists.
3.	Test for Create method:
    - Ensure it returns BadRequest when the model state is invalid.
    - Ensure it returns BadRequest when the account number is invalid.
    - Ensure it returns CreatedAtAction when the employee is created successfully.


## Integration Test

Ref: [Integration Testing in ASP.NET Core](https://code-maze.com/aspnet-core-integration-testing/)

Ref: [Testing Using Testcontainers for .NET and Docker](https://code-maze.com/csharp-testing-using-testcontainers-for-net-and-docker/)