using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;

namespace LEDE.Domain.Repositories
{
    public interface IProgramRepository
    {
        List<CoreTopic> GetProgramCoreTopics(int ProgramID);
    }        

    public class ProgramRepository : IProgramRepository
    {
        private DbContext db; 

        public ProgramRepository()
        {
            this.db = new DbContext(); 
        }
        public List<CoreTopic> GetProgramCoreTopics(int ProgramID)
        {
            return db.CoreTopics.Where(ct=> ct.Seminar.ProgramID == ProgramID).ToList();
        }
        
    }
}
