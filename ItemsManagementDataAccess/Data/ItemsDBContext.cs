using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemsManagementDataAccess.Models;

namespace ItemsManagementDataAccess.Data
{
    public class ItemsDBContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public ItemsDBContext()
        {

        }
        public ItemsDBContext(string connectionString) : base(new DbContextOptionsBuilder<ItemsDBContext>().UseSqlServer(connectionString).Options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            optionBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity => { entity.Property(e => e.Id).IsRequired(); });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 1, Name ="Default Item1", Description = "Item 1" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 2, Name = "Default Item2" , Description = "Item 2" });
        }
    }
}
