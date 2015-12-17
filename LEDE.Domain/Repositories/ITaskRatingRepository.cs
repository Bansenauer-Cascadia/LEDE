using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;

namespace LEDE.Domain.Repositories
{
    public interface ITaskRatingRepository
    {
        int CreateTaskRating(TaskRating RatingToCreate);

        void UpdateTaskRating(TaskRating RatingToUpdate);
    }

    public class TaskRatingRepository : ITaskRatingRepository
    {
        private DbContext db; 

        public TaskRatingRepository() {
            this.db = new DbContext(); 
        }       

        public int CreateTaskRating(TaskRating RatingToCreate)
        {
            db.TaskRatings.Add(RatingToCreate);
            db.SaveChanges();
            return RatingToCreate.RatingID; 
        }

        public void UpdateTaskRating(TaskRating RatingToUpdate)
        {
            TaskRating OldRating = db.TaskRatings.Find(RatingToUpdate.RatingID);
            OldRating.FacultyID = RatingToUpdate.FacultyID;
            OldRating.ReviewDate = RatingToUpdate.ReviewDate;
            db.SaveChanges();
        }        
    }
}
