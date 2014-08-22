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

            foreach (StudentTotal total in summary.TotalsList)
            {
                total.CPercentage = 100 * total.CTotal / max;
                total.SPercentage = 100 * total.STotal / max;
                total.PPercentage = 100 * total.PTotal / max; 
            }
        }

        public void CalculateStudentPercentages(StudentSummary summary)
        {
            double max = summary.MaxTotal;

            foreach (CoreTotal total in summary.RatingsList)
            {
                total.CPercentage = 100 * total.CTotal / max;
                total.SPercentage = 100 * total.STotal / max;
                total.PPercentage = 100 * total.PTotal / max;
            }
        }
    }
}