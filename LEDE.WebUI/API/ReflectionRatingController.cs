using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.API
{
    public class ReflectionDTO
    {
        public int VersID { get; set; }

        public double NumHours { get; set; }
    }

    public class ReflectionRatingController : ApiController
    {
        private DbContext db;

        private int FacultyID; 

        public ReflectionRatingController()
        {
            this.FacultyID = Int32.Parse(User.Identity.GetUserId());

            this.db = new DbContext(); 
        }
        // GET api/<controller>/5
        public ReflectionDTO Get(int id)
        {
            try
            {
                InternReflection reflection = db.InternReflections.Find(id);
                return new ReflectionDTO()
                {
                    VersID = reflection.VersID,
                    NumHours = reflection.NumHrs
                };
            }
            catch
            {
                return null; 
            }
        }

        // POST api/<controller>
        public void Post(ReflectionDTO reflection)
        {
            try
            {
                InternReflection newReflection = new InternReflection()
                {
                    VersID = reflection.VersID,
                    NumHrs = reflection.NumHours
                };
                db.InternReflections.Add(newReflection);
                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public void Put(ReflectionDTO reflection)
        {
            try
            {
                InternReflection reflectionToUpdate = db.InternReflections.Find(reflection.VersID);
                reflectionToUpdate.NumHrs = reflection.NumHours;
                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            try
            {
                InternReflection refToDelete = db.InternReflections.Find(id);
                db.InternReflections.Remove(refToDelete);
                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}