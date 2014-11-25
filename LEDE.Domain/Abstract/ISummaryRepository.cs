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
        SeminarSummary getCohortTotals (int cohortID);

        IEnumerable<ProgramCohort> getCohorts(int FacultyID);

        StudentSummary getStudentTotals(int cohortID, int userID);

        SpreadsheetModel getSpreadsheetTable(int ProgramCohortID, int UserID); 

        SummaryModel getSummaryCohorts(int UserID, int? SelectedCohortID);

        void getSummaryCandidates(SummaryModel model);

        int getStudentCohortID(int UserID);
    }
}
