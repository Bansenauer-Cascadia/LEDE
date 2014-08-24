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

        public SelectList Tasks { get; set; }

        public int TaskID { get; set; }

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

}