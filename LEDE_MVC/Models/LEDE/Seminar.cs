namespace LEDE_MVC.Models.LEDE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seminar")]
    public partial class Seminar
    {
        public Seminar()
        {
            CoreTopics = new HashSet<CoreTopic>();
            Tasks = new HashSet<Task>();
        }

        public int SeminarID { get; set; }

        [Required]
        [StringLength(250)]
        public string SeminarTitle { get; set; }

        public int ProgramCohortID { get; set; }

        public virtual ICollection<CoreTopic> CoreTopics { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        public virtual ProgramCohort ProgramCohort { get; set; }
    }
}
