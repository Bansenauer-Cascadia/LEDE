using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;

namespace LEDE.WebUI.Controllers
{
    public class Modela
    {
        public int VersID { get; set; }
    }
    public class TaskVersionController : ApiController
    {
        private DbContext db; 

        public TaskVersionController()
        {
            this.db = new DbContext();
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public int Get(int id)
        {
            try
            {
                TaskVersion versionRequested = db.TaskVersions.Find(id);
                return versionRequested.Task.SeminarID;
            }
            catch
            {
                return 0;
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int Id)
        {
            try
            {
                TaskVersion versionToUpdate = db.TaskVersions.Find(Id);
                versionToUpdate.RatingStatus = "Complete";
                db.SaveChanges();
            }
            catch
            {
                
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}