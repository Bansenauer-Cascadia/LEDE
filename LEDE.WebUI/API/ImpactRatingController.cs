using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.Controllers
{
    public class ImpactDTO
    {
        public int RatingID { get; set; }

        public int VersID { get; set; }

        public int? SScore { get; set; }

        public int? PScore { get; set; }

        public int? LScore { get; set; }
    }

    public class ImpactRatingController : ApiController
    {
        private DbContext db;

        private int FacultyID;

        public ImpactRatingController()
        {
            this.db = new DbContext();
            FacultyID = Int32.Parse(User.Identity.GetUserId());
        }
        // GET api/<controller>/5
        public ImpactDTO Get(int id)
        {
            try
            {
                ImpactTypeRating rating = db.ImpactTypeRatings.First(ir => ir.TaskRating.VersID == id);
                return new ImpactDTO()
                {
                    RatingID = rating.RatingID,
                    VersID = rating.TaskRating.VersID,
                    SScore = rating.Sscore,
                    PScore = rating.Pscore,
                    LScore = rating.Lscore
                };
            }
            catch
            {
                return new ImpactDTO() {VersID = id};
            }
        }

        // POST api/<controller>
        public void Post(ImpactDTO value)
        {
            TaskRating rating = new TaskRating()
            {
                FacultyID = FacultyID,
                ReviewDate = DateTime.Now,
                VersID = value.VersID,
            };

            ImpactTypeRating impactRating = new ImpactTypeRating()
            {
                TaskRating = rating,
                Sscore = value.SScore,
                Pscore = value.PScore,
                Lscore = value.LScore,
            };
            db.TaskRatings.Add(rating);
            db.ImpactTypeRatings.Add(impactRating);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/<controller>/5
        public void Put(ImpactDTO value)
        {
            try
            {
                ImpactTypeRating existingImpactRating = db.ImpactTypeRatings.Find(value.RatingID);
                TaskRating existingTaskRating = db.TaskRatings.Find(value.RatingID);
                existingImpactRating.Sscore = value.SScore;
                existingImpactRating.Pscore = value.PScore;
                existingImpactRating.Lscore = value.LScore;
                existingTaskRating.ReviewDate = DateTime.Now;

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
                ImpactTypeRating impactRatingToDelete = db.ImpactTypeRatings.Find(id);
                db.ImpactTypeRatings.Remove(impactRatingToDelete);
                TaskRating ratingToDelete = db.TaskRatings.Find(id);
                db.TaskRatings.Remove(ratingToDelete);

                db.SaveChanges();
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}