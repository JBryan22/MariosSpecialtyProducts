using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MariosSpeciality.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Country of Origin")]
        public string CountryOfOrigin { get; set; }

        [Display(Name = "Product Image")]
        public byte[] ProductImg { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DatePosted { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public override bool Equals(object otherProduct)
        {
            var newProduct = otherProduct as Product;
            return this.ProductId.Equals(newProduct.ProductId);
        }
        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode();
        }

        public double AverageRating()
        {
            double rating = 0;
            double totalRating = 0;
            if (Reviews != null)
            {
                int totalReview = Reviews.Count;
                foreach (var review in this.Reviews)
                {
                    totalRating += int.Parse(review.Rating);
                }
                rating = totalRating / totalReview;
            }
            return Math.Round(rating, 1);
        }
    }
}
