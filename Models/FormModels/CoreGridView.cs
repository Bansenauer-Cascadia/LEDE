using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE_MVC.Models.LEDE;

namespace LEDE_MVC.Models.FormModels
{
    public class CoreGridView 
    {
        public int RatingID { get; set; }

        public int CoreTopicID { get; set; }

        public string CoreTopicDesc { get; set; }

        public int? Cscore { get; set; }

        public int? Pscore { get; set; }

        public int? Sscore { get; set; }
    }
}