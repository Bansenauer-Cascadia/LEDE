namespace LEDE.Domain.Entities
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

        public int UserID { get; set; }

        public virtual ProgramCohort ProgramCohort { get; set; }

        public virtual User User { get; set; }
    }
}
