using Aromato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

namespace Aromato.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Counter> Counters { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("connections.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("ReleaseConnection");

            optionsBuilder.UseMySql(connectionString);
        }
    }
}
