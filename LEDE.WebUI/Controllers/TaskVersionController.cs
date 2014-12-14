using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Repositories;
using LEDE.WebUI.DTOs;
using LEDE.Domain.Entities;

namespace LEDE.WebUI.Controllers
{
    public class TaskVersionController : ApiController
    {
        private ITaskVersionRepository TaskVersions; 

        public TaskVersionController()
        {
            this.TaskVersions = new TaskVersionRepository();
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public TaskVersionDTO Get(int id)
        {
            TaskVersion tv = TaskVersions.Find(id);
            return new TaskVersionDTO(tv); 
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