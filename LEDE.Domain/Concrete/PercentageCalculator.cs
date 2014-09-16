using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Concrete
{
    public class PercentageCalculator : IPercentageCalculator
    {
        public void CalculateSeminarPercentages(SeminarSummary summary)
        {
            double max = summary.MaxTotal; 

            foreach (RatingQuery total in summary.TotalsList)
            {
                total.CPercentage = 100 * (total.CScore ?? 0) / max;
                total.SPercentage = 100 * (total.SScore ?? 0) / max;
                total.PPercentage = 100 * (total.PScore ?? 0) / max; 
            }
        }

        public void CalculateStudentPercentages(StudentSummary summary)
        {
            double maxTotal = summary.MaxTotal;
            double maxCount = summary.MaxCount;

            foreach (RatingQuery total in summary.RatingsList)
            {
                total.CPercentage = 100 * (total.CScore ?? 0)/ maxTotal;
                total.SPercentage = 100 * (total.SScore ?? 0)/ maxTotal;
                total.PPercentage = 100 * (total.PScore ?? 0)/ maxTotal;

                total.OnePercentage = 100 * total.OneCount / maxCount;
                total.TwoPercentage = 100 * total.TwoCount / maxCount;
                total.ThreePercentage = 100 * total.ThreeCount / maxCount; 
            }
        }
    }
}