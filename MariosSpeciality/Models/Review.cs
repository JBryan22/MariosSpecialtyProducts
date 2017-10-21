using System.ComponentModel.DataAnnotations;

namespace MariosSpeciality.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string Author { get; set; }
        public string ContntBody { get; set; }
        public int Rating { get; set; }
        public byte[] AuthorImg { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public override bool Equals(object otherReview)
        {
            var newReview = otherReview as Review;
            return this.ReviewId.Equals(newReview.ReviewId);
        }
        public override int GetHashCode()
        {
            return this.ReviewId.GetHashCode();
        }
    }
}