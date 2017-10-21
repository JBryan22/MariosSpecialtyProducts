using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace MariosSpeciality.Models
{

    public class EFProductRepository : IProductRepository
    {
        private MariosDbContext db;
        public EFProductRepository(MariosDbContext connection = null)
        {
            if (connection == null)
            {
                this.db = new MariosDbContext();
            }
            else
            {
                this.db = connection;
            }
        }
        public IQueryable<Product> Products => db.Products;

        public bool Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool Remove(Product product)
        {
            db.Remove(product);
            db.SaveChanges();
            return true;
        }

        public bool Save(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return true;
        }

        public void DeletAll()
        {
            db.Products.RemoveRange(db.Products.ToList());
            //db.Database.ExecuteSqlCommand("DELETE FROM [Products]");
            //db.Database.ExecuteSqlCommand("ALTER TABLE[Products] AUTO_INCREMENT = 1");
            db.SaveChanges();
        }
    }
}
