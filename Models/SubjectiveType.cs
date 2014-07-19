namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubjectiveType")]
    public partial class SubjectiveType
    {
        public SubjectiveType()
        {
            SummaryCoreRatings = new HashSet<SummaryCoreRating>();
        }

        public int SubjectiveTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string SubjectiveTypeDesc { get; set; }

        [Required]
        [StringLength(1)]
        public string SubjectiveCode { get; set; }

        public virtual ICollection<SummaryCoreRating> SummaryCoreRatings { get; set; }
    }
}
