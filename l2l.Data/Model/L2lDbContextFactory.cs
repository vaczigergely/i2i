using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace l2l.Data.Model
{
    public class L2lDbContextFactory : IDesignTimeDbContextFactory<L2lDbContext>, IDisposable
    {
        private readonly string cn;
        private readonly SqliteConnection connection;

        public bool IsInMemoryDb()
        {
            var cb = new SqlConnectionStringBuilder(cn);
            if (!cb.ContainsKey(GlobalStrings.DataSource))
            {
                throw new ArgumentException("Missing property from connectionstring: Data Source", "ConnectionString");
            }

            return GlobalStrings.SqlMemoryDb.Equals((string)cb[GlobalStrings.DataSource], StringComparison.OrdinalIgnoreCase);
        }

        public L2lDbContextFactory()
        {
            var basePath = Directory.GetCurrentDirectory();
            var environment = Environment.GetEnvironmentVariable(GlobalStrings.AspnetCoreEnvironment);

            var cbuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables();

            var config = cbuilder.Build();
            cn = config.GetConnectionString(GlobalStrings.ConnectionName);

            if (IsInMemoryDb())
            {
                connection = new SqliteConnection(cn);
                connection.Open();
            }
            
        }

        public L2lDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<L2lDbContext>();

            if (IsInMemoryDb())
            {
                builder.UseSqlite(connection);
            }
            else
            {
                builder.UseSqlite(cn);
            }
            
            return new L2lDbContext(builder.Options);
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
            }
        }
    }
}