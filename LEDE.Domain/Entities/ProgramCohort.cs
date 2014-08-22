namespace LEDE.Domain.Entities
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
        }

        public int ProgramCohortID { get; set; }

        public int ProgramID { get; set; }

        [Required]
        [StringLength(50)]
        public string AcademicYear { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual ICollection<CohortEnrollment> CohortEnrollments { get; set; }

        public virtual Program Program { get; set; }
    }
}
