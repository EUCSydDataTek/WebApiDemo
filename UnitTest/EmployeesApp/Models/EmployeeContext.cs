using Microsoft.EntityFrameworkCore;

namespace EmployeesApp.Models;

public class EmployeeContext(DbContextOptions<EmployeeContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData
        (
            new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Mark",
                    AccountNumber = "123-3452134543-32",
                    Age = 30
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    Name = "Evelin",
                    AccountNumber = "123-9384613085-55",
                    Age = 28
                }
        );
    }
}

