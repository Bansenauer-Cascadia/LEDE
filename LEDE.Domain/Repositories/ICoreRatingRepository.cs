using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities.DTOs;

namespace LEDE.Domain.Repositories
{
    public interface ICoreRatingRepository
    {
        void UpdateCoreRating(CoreRating UpdatedRating);

        void CreateCoreRating(CoreRating RatingToCreate);

        void DeleteCoreRating(int RatingID);
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


        public void DeleteCoreRating(int RatingID)
        {
            CoreRating coreRatingToDelete = db.CoreRatings.Find(RatingID);
            db.CoreRatings.Remove(coreRatingToDelete);

            TaskRating taskRatingToDelete = db.TaskRatings.Find(RatingID);
            db.TaskRatings.Remove(taskRatingToDelete);

            db.SaveChanges(); 
        }
    }
}
