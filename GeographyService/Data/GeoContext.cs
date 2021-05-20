using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeographyService.Models;

namespace GeographyService.Data
{
    public class GeoContext: DbContext
    {
        public DbSet<Continent> Continents { get; set; }
        public DbSet<River> Rivers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-3DP97NFE\\SQLEXPRESS;Initial Catalog=GeoDB;Integrated Security=True");
        }
    }
}
