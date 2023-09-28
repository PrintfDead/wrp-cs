using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using WashingtonRP.Structures.Models;

namespace WashingtonRP.Structures
{
    public class WashingtonContext : DbContext
    {
        public WashingtonContext(DbContextOptions<WashingtonContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

            optionsBuilder.UseMySql("server=localhost;user=root;password=;database=washington", serverVersion);
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Trace);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<CharacterModel> Characters { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<InventoryModel> Inventories { get; set; }
        public DbSet<BeltModel> Belts { get; set; }
    }
}
