using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MariosSpeciality.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string CountryOfOrigin { get; set; }
        public byte[] ProductImg { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public override bool Equals(object otherProduct)
        {
            var newProduct = otherProduct as Product;
            return this.ProductId.Equals(newProduct.ProductId);
        }
        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode();
        }
    }
}
