namespace LEDE.Domain.Entities
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
            Seminars = new HashSet<Seminar>();
        }

        public int ProgramID { get; set; }

        [Required]
        [Display(Name= "Program Title")]
        public string ProgramTitle { get; set; }

        [StringLength(50)]
        [Display(Name= "Program Type")]
        public string ProgramType { get; set; }

        public virtual ICollection<ProgramCohort> ProgramCohorts { get; set; }

        public virtual ICollection<Seminar> Seminars { get; set; }
    }
}
