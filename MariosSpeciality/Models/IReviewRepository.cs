using System.Linq;

namespace MariosSpeciality.Models
{
   public interface IReviewRepository
    {
        IQueryable<Review> Reviews { get; }
        bool Save(Review review);
        bool Edit(Review review);
        bool Remove(Review review);
    }
}
