namespace LEDE.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskSeminarCoreTopic")]
    public partial class TaskSeminarCoreTopic
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeminarID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CoreTopicID { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal CoreTopicNum { get; set; }

        [Key]
        [Column(Order = 4)]
        public string CoreTopicDesc { get; set; }
    }
}
