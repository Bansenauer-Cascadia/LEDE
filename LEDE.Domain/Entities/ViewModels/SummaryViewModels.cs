using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEDE.Domain.Entities
{
    public class SeminarSummary
    {
        public IEnumerable<RatingQuery> TotalsList { get; set; }

        public ProgramCohort Cohort { get; set; }

        public int MaxTotal { get; set; }

        public int MaxCount { get; set; }

        public IEnumerable<SelectListItem> ProgramCohorts { get; set; }
        public int SelectedCohortID { get; set; }
    }

    public class StudentTotal : RatingTotal
    {
        public User User { get; set; }
    }

    public class StudentSummary
    {
        public List<RatingQuery> RatingsList { get; set; }

        public int MaxTotal { get; set; }

        public int MaxCount { get; set; }
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

        public int OneCount { get; set; }
        public double OnePercentage { get; set; }

        public int TwoCount { get; set; }
        public double TwoPercentage { get; set; }

        public int ThreeCount { get; set; }
        public double ThreePercentage { get; set; }
    }

    public class RatingQuery 
    {
        public decimal CoreTopicNum { get; set; }

        public string CoreTopicDesc { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public int? CScore { get; set; }
        public double CPercentage { get; set; }

        public int? PScore { get; set; }
        public double PPercentage { get; set; }

        public int? SScore { get;set; }
        public double SPercentage { get; set; }

        public int OneCount { get; set; }
        public double OnePercentage { get; set; }

        public int TwoCount { get; set; }
        public double TwoPercentage { get; set; }

        public int ThreeCount { get; set; }
        public double ThreePercentage { get; set; }
    }

    public class SummaryModel
    {
        public int ProgramCohortID { get; set; }
        public SelectList ProgramCohorts { get; set; }

        public int UserID { get; set; }
        public SelectList Candidates { get; set; }
    }

    public class SpreadsheetModel
    {
        public SpreadsheetTable TableBody { get; set; }

        public IEnumerable<Task> CohortTasks { get; set; }
    }

    public class SpreadsheetTable
    {
        public List<SpreadsheetRow> Rows { get; set; }

    }

    public class SpreadsheetRow
    {
        public List<CoreTopicScore> Scores { get; set; }

        public string CoreTopic { get; set; }
    }
}