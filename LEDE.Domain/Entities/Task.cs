namespace LEDE.Domain.Entities
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
        [Display(Name="Task Code")]
        public string TaskCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name="Task Name")]
        public string TaskName { get; set; }

        public int SeminarID { get; set; }

        [Display(Name="Task Type")]
        public int? TaskTypeID { get; set; }

        public bool SumScores { get; set; }

        public virtual Seminar Seminar { get; set; }

        public virtual TaskType TaskType { get; set; }

        public virtual ICollection<TaskVersion> TaskVersions { get; set; }
    }
}
