using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudyMash.API.Models;

namespace StudyMash.API.DataAccess 
{
    public class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options) : base(options)
        {

        }

        public DbSet<City> cities { get; set; }
    }
}
