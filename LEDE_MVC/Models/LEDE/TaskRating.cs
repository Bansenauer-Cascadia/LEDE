namespace LEDE_MVC.Models.LEDE
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskRating")]
    public partial class TaskRating
    {
        [Key]
        public int RatingID { get; set; }

        public int VersID { get; set; }        

        public int FacultyID { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReviewDate { get; set; }
       
        public virtual CoreRating CoreRating { get; set; }

        public virtual Models.User User { get; set; }

        public virtual TaskVersion TaskVersion { get; set; }
    }
}
