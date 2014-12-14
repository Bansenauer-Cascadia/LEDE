using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Repositories
{
    public interface ITaskRatingRepository
    {
        public void SaveCoreRating(CoreRating RatingToSave);

        public void SaveImpactRating(ImpactTypeRating RatingToSave);
    }

    public class TaskRatingRepository : ITaskRatingRepository
    {

        public void SaveCoreRating(CoreRating RatingToSave)
        {
            throw new NotImplementedException();
        }

        public void SaveImpactRating(ImpactTypeRating RatingToSave)
        {
            throw new NotImplementedException();
        }
    }
}
