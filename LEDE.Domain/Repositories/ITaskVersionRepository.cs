using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using LEDE.Domain.Abstract;

namespace LEDE.Domain.Repositories
{
    public interface ITaskVersionRepository
    {
        TaskVersion Find(int VersID);

        List<CoreRating> GetTaskVersionCoreRatings(int VersID);

        int GetTaskVersionSeminarID(int VersID);

        int GetTaskVersionProgramID(int VersID);
    }

    public class TaskVersionRepository : ITaskVersionRepository
    {
        private DbContext db;

        public TaskVersion Find(int VersID)
        {
            return db.TaskVersions.SingleOrDefault(v => v.VersID == VersID);
        }

        public TaskVersionRepository()
        {
            this.db = new DbContext(); 
        }
        public List<CoreRating> GetTaskVersionCoreRatings(int VersID)
        {
            return db.CoreRatings.Where(r => r.TaskRating.TaskVersion.VersID == VersID).ToList(); 
        }

        public int GetTaskVersionSeminarID(int VersID)
        {
            return db.TaskVersions.Find(VersID).Task.SeminarID;
        }

        public int GetTaskVersionProgramID(int VersID)
        {
            return db.TaskVersions.Find(VersID).Task.Seminar.ProgramID;
        }
    }
}
