namespace LEDE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {

        public int DocumentID { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        public string Container { get; set; }

        [Required]
        [StringLength(50)]
        public string Blob { get; set; }

        [Required]
        [StringLength(25)]
        public string FileSize { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime UploadDate { get; set; }
    }
}
