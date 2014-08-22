using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Abstract; 

namespace LEDE.Domain.Entities
{
    public class RatingViewModel
    {
        public RatingViewModel() { }

        //header
        public int VersID { get; set; }
        public TaskVersion TaskVersion { get; set; }
        public SelectList VersionDrop { get; set; }

        //other
        public bool OtherVisible { get; set; }
        public SelectList CoreDrop { get; set; }

        //impact
        public bool ImpactVisible { get; set; }

        public string SubmitCommand { get; set; }

        //ratings in db
        public CompleteRating Rating { get; set; }
    }

    public enum postingPanel
    {
        Header,
        Reflection,
        Task,
        Other,
        Impact
    }

    public class RatingIndexModel
    {            
        public IEnumerable<SelectListItem> Candidates { get; set; }

        public int SelectedUserID { get; set; }

        public bool UploadVisible { get; set; }

        public int? VersID { get; set; }
    }

    public class CompleteRating
    {
        public int VersID { get; set; }

        public int FacultyID { get; set; }

        public List<CoreRating> TaskCoreRatings { get; set; }

        public List<CoreRating> OtherCoreRatings { get; set; }

        public ImpactTypeRating ImpactRating { get; set; }
    }

    public class RatingIndexPost
    {
        public string Command { get; set; }

        public int? SelectedUserID { get; set; }
    }

    public class CandidateDrop
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}