using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECSEL.Models;

namespace ECSEL.Models.FormModels
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