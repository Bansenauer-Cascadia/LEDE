using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;

namespace LEDE.Domain.Repositories
{
    public interface ICoreRatingRepository
    {
        void UpdateCoreRating(CoreRating UpdatedRating);

        void CreateCoreRating(CoreRating RatingToCreate);
    }

    public class CoreRatingRepository : ICoreRatingRepository
    {
        private DbContext db; 

        public CoreRatingRepository()
        {
            this.db = new DbContext(); 
        }
        public void UpdateCoreRating(CoreRating UpdatedRating)
        {
            CoreRating OldRating = db.CoreRatings.Find(UpdatedRating.RatingID);
            OldRating.Cscore = UpdatedRating.Cscore;
            OldRating.Sscore = UpdatedRating.Sscore;
            OldRating.Pscore = UpdatedRating.Pscore;
            db.SaveChanges();
        }

        public void CreateCoreRating(CoreRating RatingToCreate)
        {
            db.CoreRatings.Add(RatingToCreate);
            db.SaveChanges();
        }
    }
}
