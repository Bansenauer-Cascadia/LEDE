using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace LEDE_MVC.Models.LEDE
{
    [Table("TaskType")]
    public partial class TaskType
    {
        public TaskType()
        {
            Tasks = new HashSet<Task>();
        }

        public int TaskTypeID { get; set; }

        [Required]
        [StringLength(25)]
        public string TaskTypeDescription { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}