namespace LEDE.Domain.Entities
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
            Raters = new HashSet<Rater>();
            Tasks = new HashSet<Task>();
        }

        public int SeminarID { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name="Seminar Title")]
        public string SeminarTitle { get; set; }

        [Required]
        public int ProgramID { get; set; }

        public virtual ICollection<CoreTopic> CoreTopics { get; set; }

        public virtual Program Program { get; set; }

        public virtual ICollection<Rater> Raters { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
