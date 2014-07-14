using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE_MVC.Models.LEDE; 

namespace LEDE_MVC.Models
{
    public class TaskRatingViewModel
    {
        public IEnumerable<TaskRating> existingRatings { get; set; }
        public IEnumerable<TaskRating> newRatings { get; set; }
    }
}