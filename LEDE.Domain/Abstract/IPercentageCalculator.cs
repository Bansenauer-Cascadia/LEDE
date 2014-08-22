using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Abstract
{
    public interface IPercentageCalculator
    {
        void CalculateSeminarPercentages(SeminarSummary summary);

        void CalculateStudentPercentages(StudentSummary summary); 
    }
}
