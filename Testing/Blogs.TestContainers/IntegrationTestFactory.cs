using BackendData;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blogs.TestContainers;
public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string Database = "TestDatabase"; // Use a different database name
    private const string Username = "sa";
    private const string Password = "yourStrong(!)Password";
    private const ushort MsSqlPort = 1433;

    private readonly IContainer _mssqlContainer;

    public IntegrationTestFactory()
    {
        // Default: mcr.microsoft.com/mssql/server:2019-CU18-ubuntu-20.04
        //_mssqlContainer = new MsSqlBuilder().Build();

        _mssqlContainer = new ContainerBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPortBinding(MsSqlPort, true)
        .WithEnvironment("ACCEPT_EULA", "Y")
        .WithEnvironment("SQLCMDUSER", Username)
        .WithEnvironment("SQLCMDPASSWORD", Password)
        .WithEnvironment("MSSQL_SA_PASSWORD", Password)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
        .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var host = _mssqlContainer.Hostname;
        var port = _mssqlContainer.GetMappedPublicPort(MsSqlPort);

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer($"Server={host},{port};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True;MultipleActiveResultSets = true"));
        });
    }

    public async Task InitializeAsync()
    {
        await _mssqlContainer.StartAsync();

        // Create the test database
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _mssqlContainer.DisposeAsync();
    }
}
