using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LEDE.Domain.Entities
{
    public class CandidateIndexModel
    {
        public CandidateIndexModel()
        {
            UploadVisible = false; 
        }
        public IEnumerable<TaskVersion> taskVersions { get; set; }

        public IEnumerable<SelectListItem> Tasks { get; set; }

        public int TaskID { get; set; }

        public IEnumerable<SelectListItem> Cohorts { get; set; }

        public int ProgramCohortID { get; set; }

        public bool UploadVisible { get; set; }
    }

    public class CandidateSummaryRow
    {
        public Task Task { get; set; }

        public IEnumerable<TaskVersion> candidateSubmissions { get; set; }
    }

    public class CohortDropDown
    {
        public int ProgramCohortID { get; set; }

        public SelectList UserCohorts { get; set; }
    }

    public class FacultySummaryModel
    {
        public IEnumerable<Task> CohortTasks { get; set; }

        public List<FacultySummaryRow> Rows { get; set; }

        public int ProgramCohortID { get; set; }
    }

    public class FacultySummaryRow
    {
        public User Candidate { get; set; }

        public List<LatestVersion> LatestVersions { get; set; }
    }

    public class LatestVersion
    {
        public int? VersID { get; set; }

        public int? Version { get; set; }

        public int? TaskID { get; set; }

        public string RatingStatus { get; set; }
    }

    public class SummaryTaskVersion
    {
        public int Version { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }

        public string Status { get; set; }
    }
}