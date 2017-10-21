using System.Linq;

namespace MariosSpeciality.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        bool Save(Product product);
        bool Edit(Product product);
        bool Remove(Product product);
    }
}
