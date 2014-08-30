using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Abstract
{
    public interface ISummaryRepository
    {
        SeminarSummary getCohortTotals (int cohortID, int userID);

        IEnumerable<ProgramCohort> getCohorts();

        StudentSummary getStudentTotals(int cohortID, int userID); 
    }
}
