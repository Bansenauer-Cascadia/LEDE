namespace LEDE_MVC.Models.LEDE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProgramCohort")]
    public partial class ProgramCohort
    {
        public ProgramCohort()
        {
            CohortEnrollments = new HashSet<CohortEnrollment>();
            Seminars = new HashSet<Seminar>();
        }

        [Key]
        public int ProgramCohortID { get; set; }

        [Required]
        [StringLength(150)]
        public string ProgramCohortDesc { get; set; }

        public int ProgramID { get; set; }

        public virtual ICollection<CohortEnrollment> CohortEnrollments { get; set; }

        public virtual Program Program { get; set; }

        public virtual ICollection<Seminar> Seminars { get; set; }
        
    }
}
