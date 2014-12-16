using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEDE.Domain.Entities
{
    public class LogAndReadingGraphsDTO
    {
        public int Id { get; set; }

        public int ProgramCohortID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public double? NumHrs { get; set; }

        public int? NumEntries { get; set; }

        public double? PercentHrs { get; set; }

        public int? PercentEntries { get; set; }
    }
}