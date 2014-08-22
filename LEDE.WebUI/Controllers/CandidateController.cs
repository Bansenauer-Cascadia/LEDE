using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles="Candidate")]
    public class CandidateController : Controller
    {
        private ICandidateRepository db;
        private IRatingRepository ratings; 

        public CandidateController(ICandidateRepository repo, IRatingRepository scoreRepo)
        {
            this.db = repo;
            this.ratings = scoreRepo;
        }

        public ActionResult Index(bool? UploadVisible, int? DownloadID, int? DeleteID)
        {            
            if (DownloadID != null)
                FileManager.DownloadDocument(db.findDocument((int)DownloadID), HttpContext);  
            else if(DeleteID != null)
                db.DeleteTask((int)DeleteID);

            CandidateIndexModel model = db.getIndexModel(1, null);  ///replace with real userID!!                                                   
            model.UploadVisible = UploadVisible ?? false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CandidateIndexModel post)
        {
            CandidateIndexModel model = db.getIndexModel(1, post.TaskID);      ////replace with readl userid
            return View(model); 
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int taskID)
        {
            if (file != null && file.ContentLength > 0)
            {
                Document uploadDoc = new Document()
                {
                    Container = "usertest",//"user" + User.Identity.GetUserId()    ///replace with real id
                    FileName = file.FileName,
                    FileSize = file.ContentLength.ToString(),
                    UploadDate = DateTime.Now,
                };

                db.uploadTask(taskID, 1, uploadDoc); //real userid here
                FileManager.UploadDocument(uploadDoc, file);
            }           

            return RedirectToAction("Index");
        }

        public ActionResult Score(int VersID)
        {
            RatingViewModel rating = ratings.getRatingModel(VersID); 
            CompleteRating model = rating.Rating;
            ViewBag.Task = rating.TaskVersion.Task.TaskName + " - Version " + rating.TaskVersion.Version; int foo = model.OtherCoreRatings.Count(); 
            return View(model);
        }
    }
}