using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Repositories;
using LEDE.Domain.Entities;
using LEDE.WebUI.DTOs; 

namespace LEDE.WebUI.Controllers
{
    public class CoreRatingController : ApiController
    {
        private ITaskVersionRepository TaskVersions;

        private IProgramRepository Programs;

        public CoreRatingController(ITaskVersionRepository TaskVersions, IProgramRepository Programs)
        {
            this.TaskVersions = TaskVersions;
            this.Programs = Programs; 
        }

        public CoreRatingController()
        {
            this.TaskVersions = new TaskVersionRepository();
            this.Programs = new ProgramRepository(); 
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
                CoreTopicRatings.Add(dto); 
            }

            return CoreTopicRatings;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }        
    }
}