namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Program")]
    public partial class Program
    {
        public Program()
        {
            ProgramCohorts = new HashSet<ProgramCohort>();
        }

        public int ProgramID { get; set; }

        [Required]
        public string ProgramTitle { get; set; }

        public virtual ICollection<ProgramCohort> ProgramCohorts { get; set; }
    }
}
