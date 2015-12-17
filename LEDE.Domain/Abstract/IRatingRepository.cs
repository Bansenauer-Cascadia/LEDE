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

        RatingIndexModel getIndexModel(int FacultyID, int? ProgramCohortID, int? userID);

        Document findDocument(int documentID);

        Document findDocumentByVersID(int VersID);

        void saveFeedback(Document feedbackDoc, int versID);

        void deleteTaskRating(int ratingID);

        void deleteImpactRating(int ratingID);

        RatingViewModel getRatingModel(int versID);

        void EditReflection(int versID, double numHrs);

        void EditReading(int versID, int numEntries);

        IEnumerable<TaskVersion> getTaskVersions(int userID, int ProgramCohortID);

        //Fetch Data For Rating Partials
        TaskVersion getTaskVersion(int versID);

        RatingViewModel getTaskRatings(int versID);

        RatingViewModel getOtherRatings(int versID);

        RatingViewModel getImpactRatings(int versID);

        SelectList getCoreDrop(int VersID);

        //function for saving finished rating
        void saveCompleteRating(RatingViewModel model);
    }
}
