using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Repositories;
using LEDE.Domain.Entities;
using LEDE.Domain.Entities.DTOs;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.Controllers
{
    public class CoreRatingController : ApiController
    {
        private ITaskVersionRepository TaskVersions;

        private IProgramRepository Programs;

        private ITaskRatingRepository TaskRatings;

        private ICoreRatingRepository CoreRatings;

        private int FacultyID; 

        public CoreRatingController(ITaskVersionRepository TaskVersions, IProgramRepository Programs, 
            ITaskRatingRepository TaskRatings, ICoreRatingRepository CoreRatings)
        {
            this.TaskVersions = TaskVersions;
            this.Programs = Programs;
            this.TaskRatings = TaskRatings;
            this.CoreRatings = CoreRatings;             
        }

        public CoreRatingController()
        {
            this.TaskVersions = new TaskVersionRepository();
            this.Programs = new ProgramRepository();
            this.CoreRatings = new CoreRatingRepository();
            this.TaskRatings = new TaskRatingRepository();
            this.FacultyID = Convert.ToInt32(User.Identity.GetUserId());
        }

        // GET api/<controller>/5
        public List<CoreRatingDTO> Get(int Id)
        {
            int VersID = Id;
            int TVProgramID = TaskVersions.GetTaskVersionProgramID(VersID);
            List<CoreRating> TVCoreRatings = TaskVersions.GetTaskVersionCoreRatings(VersID);
            List<CoreTopic> TVCoreTopics = Programs.GetProgramCoreTopics(TVProgramID);

            List<CoreRatingDTO> CoreTopicRatings = new List<CoreRatingDTO>();

            foreach (CoreTopic topic in TVCoreTopics)
            {
                CoreRating rating;
                try
                {
                    rating = TVCoreRatings.Single(r => r.CoreTopicID == topic.CoreTopicID);
                }
                catch
                {
                    rating = new CoreRating(); 
                }
                CoreRatingDTO dto = new CoreRatingDTO(topic, rating);
                dto.VersID = VersID; 
                CoreTopicRatings.Add(dto); 
            }

            return CoreTopicRatings;
        }

        // POST api/<controller>
        public void Post(CoreRatingDTO RatingToCreate)
        {
            this.FacultyID = Int32.Parse(User.Identity.GetUserId()); 
            TaskRating NewTaskRating = new TaskRating()
            {
                FacultyID = FacultyID,
                ReviewDate = DateTime.Now,
                VersID = RatingToCreate.VersID
            };
            int NewRatingID = TaskRatings.CreateTaskRating(NewTaskRating);

            CoreRating NewCoreRating = new CoreRating()
            {
                RatingID = NewRatingID,
                CoreTopicID = RatingToCreate.CoreTopicID,
                Cscore = RatingToCreate.CScore,
                Sscore = RatingToCreate.SScore,
                Pscore = RatingToCreate.PScore
            };
            CoreRatings.CreateCoreRating(NewCoreRating);
        }

        // PUT api/<controller>/5
        public void Put(CoreRatingDTO RatingToSave)
        {
            this.FacultyID = Int32.Parse(User.Identity.GetUserId()); 
            TaskRating UpdatedTaskRating = new TaskRating()
            {
                RatingID = RatingToSave.RatingID,
                FacultyID = FacultyID,
                VersID = RatingToSave.VersID,
                ReviewDate = DateTime.Now
            };
            TaskRatings.UpdateTaskRating(UpdatedTaskRating);

            CoreRating UpdatedCoreRating = new CoreRating()
            {
                RatingID = RatingToSave.RatingID,
                Cscore = RatingToSave.CScore,
                Sscore = RatingToSave.SScore,
                Pscore = RatingToSave.PScore,
            };
            CoreRatings.UpdateCoreRating(UpdatedCoreRating);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            CoreRatings.DeleteCoreRating(id); 
        }        
    }
}