using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ButiciBackend.Models
{
    public class ButiciContext : DbContext
    {
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Butik> Butici { get; set; }
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<SpojAdresa> Adrese { get; set; }
        public DbSet<SpojVelicina> Velicine { get; set; }
        public ButiciContext(DbContextOptions options) : base(options)
        {

        }
       
    }
}
