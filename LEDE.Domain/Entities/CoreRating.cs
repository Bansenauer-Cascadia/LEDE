namespace LEDE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoreRating")]
    public partial class CoreRating
    {
        [Key, ForeignKey("TaskRating")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RatingID { get; set; }

        public int CoreTopicID { get; set; }

        [Range(0,3)]
        [Display(Name="Conceptual Score")]
        public int? Cscore { get; set; }

        [Range(0, 3)]
        [Display(Name="Strategic Score")]
        public int? Sscore { get; set; }

        [Range(0,3)]
        [Display(Name="Personal Score")]
        public int? Pscore { get; set; }

        public virtual TaskRating TaskRating { get; set; }

        public virtual CoreTopic CoreTopic { get; set; }
    }
}
