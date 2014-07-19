namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        public Document()
        {
            TaskVersions = new HashSet<TaskVersion>();
            TaskVersions1 = new HashSet<TaskVersion>();
        }

        public int DocumentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        [StringLength(25)]
        public string FileSize { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime UploadDate { get; set; }

        public virtual ICollection<TaskVersion> TaskVersions { get; set; }

        public virtual ICollection<TaskVersion> TaskVersions1 { get; set; }
    }
}
