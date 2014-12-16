using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;

namespace LEDE.Domain.Repositories
{
    public interface ICohortTotalsRepository
    {
        IEnumerable<LogAndReadingGraphsDTO> GetHoursTotals(int CohortID);
    }

    public class CohortTotalsRepository : ICohortTotalsRepository
    {
        private DbContext db;

        public CohortTotalsRepository()
        {
            this.db = new DbContext();
        }

        public IEnumerable<LogAndReadingGraphsDTO> GetHoursTotals(int CohortID)
        {
            return db.Database.SqlQuery<LogAndReadingGraphsDTO>("InternLogAndReadingGraphs @p0", new object[] { CohortID }).ToList();
        }

    }
}
