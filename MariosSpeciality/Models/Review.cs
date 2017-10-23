using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MariosSpeciality.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [MaxLength(250), MinLength(50)]
        public string ContentBody { get; set; }

        [Required]
        public string Rating { get; set; }
        public byte[] AuthorImg { get; set; }

        [DisplayFormat(DataFormatString = "0:F")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]//to assign current time stamp
        public DateTime PostedDate { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public override bool Equals(object otherReview)
        {
            var newReview = otherReview as Review;
            return ReviewId.Equals(newReview.ReviewId);
        }
        public override int GetHashCode()
        {
            return ReviewId.GetHashCode();
        }
    }
}