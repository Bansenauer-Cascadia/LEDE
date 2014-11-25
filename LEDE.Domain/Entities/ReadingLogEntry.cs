namespace LEDE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReadingLogEntry")]
    public partial class ReadingLogEntry
    {
        [Key, ForeignKey("TaskVersion")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VersID { get; set; }

        public int NumEntries { get; set; }

        public virtual TaskVersion TaskVersion { get; set; }
    }
}
