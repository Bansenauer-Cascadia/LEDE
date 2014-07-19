namespace ECSEL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InternReflection")]
    public partial class InternReflection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VersID { get; set; }

        public double NumHrs { get; set; }

        public virtual TaskVersion TaskVersion { get; set; }
    }
}
