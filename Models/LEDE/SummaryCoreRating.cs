namespace LEDE_MVC.Models.LEDE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SummaryCoreRating")]
    public partial class SummaryCoreRating
    {
        [Key]
        public int SumRatingID { get; set; }

        public int CandidateID { get; set; }

        public int FacultyID { get; set; }

        public int CohortEventID { get; set; }

        public int CoreTopicID { get; set; }

        public int SubjectiveTypeID { get; set; }

        [Required]
        [StringLength(1)]
        public string SubjectiveRating { get; set; }

        public virtual Models.User User { get; set; }

        public virtual Models.User User1 { get; set; }

        public virtual SubjectiveType SubjectiveType { get; set; }
    }
}
