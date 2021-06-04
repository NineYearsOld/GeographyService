using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Models;
using GeographyService.Models.Entities;
using GeographyService.Models.Entities.Mappings;

namespace GeographyService.Data
{
    public class GeoContext: DbContext
    {
        public DbSet<Continent> Continents { get; set; }
        public DbSet<River> Rivers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<RiverMapping> RiverMappings { get; set; }
        public DbSet <CountryMapping> CountryMappings { get; set; }
        public DbSet <CityMapping> CityMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-3DP97NFE\\SQLEXPRESS;Initial Catalog=GeoDB;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityMapping>().HasKey(cim => new { cim.CityId, cim.CountryId });

            modelBuilder.Entity<CountryMapping>().HasKey(com => new { com.CountryId, com.ContinentId });

            modelBuilder.Entity<RiverMapping>().HasKey(rim => new { rim.CountryId, rim.RiverId });
        }
    }
}
