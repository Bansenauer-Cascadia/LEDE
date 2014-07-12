using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LEDE_MVC.Models.LEDE;
using System.Web.Mvc; 


namespace LEDE_MVC.Models
{
    public class UploadViewModel
    {
        public IEnumerable<TaskVersion> TaskVersions { get; set; }

        public int TaskID { get; set; }  
    }
}