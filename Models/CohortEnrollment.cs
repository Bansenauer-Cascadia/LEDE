namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CohortEnrollment")]
    public partial class CohortEnrollment
    {
        [Key]
        public int CohortEventID { get; set; }

        public int ProgramCohortID { get; set; }

        [Required]
        [StringLength(25)]
        public string AcademicYear { get; set; }

        [Required]
        [StringLength(10)]
        public string Quarter { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public int UserID { get; set; }

        public virtual ProgramCohort ProgramCohort { get; set; }

        public virtual User User { get; set; }
    }
}
