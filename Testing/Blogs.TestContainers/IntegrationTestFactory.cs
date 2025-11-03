using BackendData;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace Blogs.TestContainers;
public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string Password = "yourStrong(!)Password";

    private readonly MsSqlContainer _mssqlContainer;

    public IntegrationTestFactory()
    {
        // Default: mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04
        _mssqlContainer = new MsSqlBuilder()
                              .WithPassword(Password)
                              .Build();

        #region Detailed Config
        //IContainer container = new ContainerBuilder()
        //.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        //.WithPortBinding(1433, true)
        //.WithEnvironment("ACCEPT_EULA", "Y")
        //.WithEnvironment("SQLCMDUSER", "sa")
        //.WithEnvironment("SQLCMDPASSWORD", Password)
        //.WithEnvironment("MSSQL_SA_PASSWORD", Password)
        //.WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(1433))
        //.Build();
        #endregion
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

            var ConnectionStringBuilder = new SqlConnectionStringBuilder(_mssqlContainer.GetConnectionString());
            ConnectionStringBuilder.InitialCatalog = "TestDB";

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(ConnectionStringBuilder.ConnectionString)
            );
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
