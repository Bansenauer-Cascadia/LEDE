namespace LEDE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpactTypeRating")]
    public partial class ImpactTypeRating
    {
        [Key, ForeignKey("TaskRating")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RatingID { get; set; }

        
        [Display(Name="Structures Score")]
        [Range(0,3)]
        public int? Sscore { get; set; }

        [Range(0,3)]
        [Display(Name = "Practices Score")]
        public int? Pscore { get; set; }

        [Range(0,3)]
        [Display(Name = "Learning Score")]
        public int? Lscore { get; set; }

        public virtual TaskRating TaskRating { get; set; }

    }
}
