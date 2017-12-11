using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TicTacToe.Data;

namespace TicTacToe.ConsoleApp.Configuration
{
    public class TicTacToeDbContextFactory : IDesignTimeDbContextFactory<TicTacToeDbContext>
    {
        public TicTacToeDbContext CreateDbContext(string[] args = null)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                   .AddJsonFile("appsettings.json")
                                                   .Build();

            var builder = new DbContextOptionsBuilder<TicTacToeDbContext>();
            var connectionString = config.GetConnectionString("RemoteConnection");
            builder.UseSqlServer(connectionString);

            return new TicTacToeDbContext(builder.Options);
        }
    }
}