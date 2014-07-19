namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImpactTypeRating")]
    public partial class ImpactTypeRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RatingID { get; set; }

        public int? Sscore { get; set; }

        public int? Pscore { get; set; }

        public int? Lscore { get; set; }

        public virtual TaskRating TaskRating { get; set; }

        public virtual TaskRating TaskRating1 { get; set; }
    }
}
