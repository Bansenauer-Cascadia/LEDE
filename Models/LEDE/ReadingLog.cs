namespace LEDE_MVC.Models.LEDE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReadingLogEntry")]
    public partial class ReadingLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VersID { get; set; }

        [Required]
        [StringLength(100)]
        public string ReadingTitle { get; set; }

        public int PageNum { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime SubmitDate { get; set; }

        public virtual TaskVersion TaskVersion { get; set; }
    }
}
