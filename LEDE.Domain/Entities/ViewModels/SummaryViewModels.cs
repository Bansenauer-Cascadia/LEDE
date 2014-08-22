using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEDE.Domain.Entities
{
    public class SeminarSummary
    {
        public IEnumerable<StudentTotal> TotalsList { get; set; }

        public ProgramCohort Cohort { get; set; }

        public int MaxTotal { get; set; }

        public IEnumerable<SelectListItem> ProgramCohorts { get; set; }
        public int SelectedCohortID { get; set; } 
    }

    public class StudentSummary
    {
        public IEnumerable<CoreTotal> RatingsList { get; set; }

        public User User { get; set; }

        public int MaxTotal { get; set; }
    }

    public class StudentTotal : RatingTotal
    {
        public User User { get; set; }
    }

    public class CoreTotal : RatingTotal
    {
        public CoreTopic CoreTopic { get; set; }
    }

    public class RatingTotal
    {
        public int CTotal { get; set; }
        public double CPercentage { get; set; }

        public int STotal { get; set; }
        public double SPercentage { get; set; }

        public int PTotal { get; set; }
        public double PPercentage { get; set; }
    }
}