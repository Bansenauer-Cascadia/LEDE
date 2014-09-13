using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using System.Web.Mvc;

namespace LEDE.Domain.Abstract
{
    public interface IRatingRepository
    {
        void saveTaskRating(CompleteRating taskRating, int VersID);

        void saveImpactRating(CompleteRating impactRating, int VersID); 

        RatingIndexModel getIndexModel(int? userID);

        Document findDocument(int documentID);

        Document findDocumentByVersID(int VersID);

        void saveFeedback(Document feedbackDoc, int versID);

        void deleteTaskRating(int ratingID);

        RatingViewModel getRatingModel(int versID);

        void EditReflection(int versID, double numHrs);

        void EditReading(int versID, int numEntries);

        IEnumerable<TaskVersion> getTaskVersions(int userID);
    }
}
