namespace LEDE.Domain.Entities
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

        public int UserID { get; set; }

        public int? FeedbackDocID { get; set; }

        [ForeignKey("DocumentID")]
        public virtual Document Document { get; set; }

        [ForeignKey("FeedbackDocID")]
        public virtual Document FeedbackDoc { get; set; }

        public virtual InternReflection InternReflection { get; set; }

        public virtual ReadingLogEntry ReadingLogEntry { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<TaskRating> TaskRatings { get; set; }
    }

    public partial class TaskVersion
    {
        public string versionString()
        {
            string text = "V" + Version;
            if (RatingStatus.Trim() == "Pending") text += "*";
            return text;
        }
    }
}
