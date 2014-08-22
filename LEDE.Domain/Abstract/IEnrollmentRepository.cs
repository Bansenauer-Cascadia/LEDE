using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities; 

namespace LEDE.Domain.Abstract
{
    public interface IEnrollmentRepository
    {
        IEnumerable<ProgramCohort> getCohorts();

        AllUsers getCohortUsers(int ProgramCohortID);

        void addCohortUsers(List<int> idsToAdd, int programCohortID);

        void removeCohortUsers(List<int> idsToRemove, int programCohortID);
        
    }
}
