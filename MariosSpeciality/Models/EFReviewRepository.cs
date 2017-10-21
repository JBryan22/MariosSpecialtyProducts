using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MariosSpeciality.Models
{
    public class EFReviewRepository : IReviewRepository
    {
        private MariosDbContext db;

        public EFReviewRepository(MariosDbContext connection = null)
        {
            if (connection == null)
            {
                db = new MariosDbContext();
            }
            else
            {
                db = connection;
            }

        }
        public IQueryable<Review> Reviews => db.Reviews;

        public bool Edit(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool Remove(Review review)
        {
            db.Remove(review);
            db.SaveChanges();
            return true;
        }

        public bool Save(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return true;
        }

        public void DeleteAll()
        {
            db.RemoveRange(db.Reviews.ToList());
            db.SaveChanges();
        }

    }
}
