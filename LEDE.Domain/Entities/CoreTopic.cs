namespace LEDE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoreTopic")]
    public partial class CoreTopic
    {
        public CoreTopic()
        {
            CoreRatings = new HashSet<CoreRating>();
        }

        public int CoreTopicID { get; set; }

        public decimal CoreTopicNum { get; set; }

        [Required]
        public string CoreTopicDesc { get; set; }

        public int SeminarID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ModifyDate { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public virtual ICollection<CoreRating> CoreRatings { get; set; }

        public virtual Seminar Seminar { get; set; }
    }
}
