using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;

namespace LEDE.Domain.Repositories
{
    public interface IProgramCohortRepository
    {
        List<InternReflection> GetCohortInternReflections(int ProgramID);

        List<ReadingLogEntry> GetCohortReadingLogEntries(int ProgramID);
    }

    class ProgramCohortRepository : IProgramCohortRepository
    {
        private DbContext db; 

        public ProgramCohortRepository()
        {
            this.db = new DbContext();
        }

        public List<InternReflection> GetCohortInternReflections(int ProgramID)
        {
            return db.InternReflections.Where(ir => ir.TaskVersion.Task.Seminar.ProgramID == ProgramID).ToList();
        }

        public List<ReadingLogEntry> GetCohortReadingLogEntries(int ProgramID)
        {
            return db.ReadingLogEntries.Where(rl => rl.TaskVersion.Task.Seminar.ProgramID == ProgramID).ToList();
        }
    }
}
