using MariosSpeciality.Models;
using Microsoft.EntityFrameworkCore;

namespace MarioSpecialityTests.Models
{
    public class TestDbContext : MariosDbContext
    {
        public override DbSet<Product> Products { get; set; }
        public override DbSet<Review> Reviews { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySql
    (@"Server=localhost;Port=8889;database=marios_test;uid=root;pwd=root;");

    }
}
