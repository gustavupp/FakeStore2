using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace FakeStore2.Database
{
    internal class Program
    {

        private static string connectionString = "data source = (localdb)\\MSSQLLocalDB;initial catalog = FakeStore2; integrated security = True; MultipleActiveResultSets=True;App=EntityFramework";
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb.AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.EmbeddedResources()
                .ScanIn(typeof (Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
    }
}
