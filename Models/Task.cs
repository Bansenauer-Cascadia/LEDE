namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        public Task()
        {
            TaskVersions = new HashSet<TaskVersion>();
        }

        public int TaskID { get; set; }

        [Required]
        [StringLength(10)]
        public string TaskCode { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }

        public int SeminarID { get; set; }

        public int? TaskTypeID { get; set; }

        public virtual Seminar Seminar { get; set; }

        public virtual Seminar Seminar1 { get; set; }

        public virtual TaskType TaskType { get; set; }

        public virtual ICollection<TaskVersion> TaskVersions { get; set; }
    }
}
