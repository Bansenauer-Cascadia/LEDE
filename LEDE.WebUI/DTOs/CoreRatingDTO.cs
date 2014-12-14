using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Entities;

namespace LEDE.WebUI.DTOs
{
    public class CoreRatingDTO
    {
        public int CoreTopicID;

        public string CoreTopicTitle;

        public int SeminarID;

        public int RatingID;

        public int? CScore;

        public int? SScore;

        public int? PScore;

        public CoreRatingDTO(CoreTopic Topic, CoreRating Rating)
        {
            this.CoreTopicID = Topic.CoreTopicID;
            this.CoreTopicTitle = Topic.CoreTopicNum + " " + Topic.CoreTopicDesc;
            this.SeminarID = Topic.SeminarID; 

            this.RatingID = Rating.RatingID;
            this.CScore = Rating.Cscore;
            this.SScore = Rating.Sscore;
            this.PScore = Rating.Pscore;
        }
    }    
}