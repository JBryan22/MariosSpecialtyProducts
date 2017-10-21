using Microsoft.EntityFrameworkCore;

namespace MariosSpeciality.Models
{
    public class MariosDbContext : DbContext
    {
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseMySql
            (@"Server=localhost;Port=8889;database=marios;uid=root;pwd=root;");

    }
}
