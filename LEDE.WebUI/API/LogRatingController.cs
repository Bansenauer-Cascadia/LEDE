using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.Controllers
{
    public class LogDTO
    {
        public int VersID { get; set; }

        public int NumEntries { get; set; }
    }
    public class LogRatingController : ApiController
    {
        private DbContext db;

        public LogRatingController()
        {
            this.db = new DbContext();
        }
        // GET api/<controller>/5
        public LogDTO Get(int id)
        {
            try
            {
                ReadingLogEntry entry = db.ReadingLogEntries.Find(id);
                return new LogDTO()
                {
                    VersID = entry.VersID,
                    NumEntries = entry.NumEntries
                };
            }
            catch
            {
                return InitializeLogIfAppropriate(id);
            }
        }

        private LogDTO InitializeLogIfAppropriate(int VersID)
        {
            try
            {
                TaskVersion versionForReflection = db.TaskVersions.Find(VersID);

                if (versionForReflection.Task.TaskTypeID == 2)
                    return new LogDTO() { VersID = versionForReflection.VersID };
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        // POST api/<controller>
        public void Post(LogDTO log)
        {
            try
            {
                ReadingLogEntry newLog = new ReadingLogEntry()
                {
                    VersID = log.VersID,
                    NumEntries = log.NumEntries
                };
                db.ReadingLogEntries.Add(newLog);
                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public void Put(LogDTO log)
        {
            try
            {
                ReadingLogEntry existingLog = db.ReadingLogEntries.Find(log.VersID);
                existingLog.NumEntries = log.NumEntries;
                db.SaveChanges();
            }
            catch
            {
                Post(log);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            try
            {
                ReadingLogEntry entryToDelete = db.ReadingLogEntries.Find(id);
                db.ReadingLogEntries.Remove(entryToDelete);
                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}