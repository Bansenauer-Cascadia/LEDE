namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskVersion")]
    public partial class TaskVersion
    {
        public TaskVersion()
        {
            TaskRatings = new HashSet<TaskRating>();
        }

        [Key]
        public int VersID { get; set; }

        public int TaskID { get; set; }

        public int DocumentID { get; set; }

        public int Version { get; set; }

        [Required]
        [StringLength(15)]
        public string RatingStatus { get; set; }

        [Column ("UserID")]
        public int ID { get; set; }

        public int? FeedbackDocID { get; set; }

        [ForeignKey("DocumentID")]
        public virtual Document Document { get; set; }

        [ForeignKey("FeedbackDocID")]
        public virtual Document FeedbackDocument { get; set; }

        public virtual InternReflection InternReflection { get; set; }

        public virtual ReadingLog ReadingLogEntry { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<TaskRating> TaskRatings { get; set; }


        public virtual User User { get; set; }
    }
}
