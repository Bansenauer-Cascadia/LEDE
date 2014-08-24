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

        public ActionResult Index(bool? UploadVisible, int? DownloadID, int? DeleteID, int? TaskID)
        {            
            if (DownloadID != null)
                FileManager.DownloadDocument(db.findDocument((int)DownloadID), HttpContext);  
            else if(DeleteID != null)
                db.DeleteTask((int)DeleteID);

            int UserID = Convert.ToInt32(User.Identity.GetUserId());
            CandidateIndexModel model = db.getIndexModel(UserID, TaskID);                                                     
            model.UploadVisible = UploadVisible ?? false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CandidateIndexModel post)
        {
            int userID = Convert.ToInt32(User.Identity.GetUserId());
            CandidateIndexModel model = db.getIndexModel(userID, post.TaskID);      
            return View(model); 
        }

        public ActionResult Summary(int ProgramCohortID = 0)
        {
            int UserID = Convert.ToInt32(User.Identity.GetUserId());  
            CohortDropDown model =  db.getCohorts(UserID, ProgramCohortID);
            return View(model);
        }

        public PartialViewResult SummaryTable(int ProgramCohortID)
        {
            int UserID = Convert.ToInt32(User.Identity.GetUserId());  
            IEnumerable<CandidateSummaryRow> model = db.getSummaryModel(UserID, ProgramCohortID);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int taskID)
        {
            if (file != null && file.ContentLength > 0)
            {
                Document uploadDoc = new Document()
                {
                    Container = "user" + User.Identity.GetUserId(),
                    FileName = file.FileName,
                    FileSize = file.ContentLength.ToString(),
                    UploadDate = DateTime.Now,
                };

                db.uploadTask(taskID, Convert.ToInt32(User.Identity.GetUserId()), uploadDoc); 
                FileManager.UploadDocument(uploadDoc, file);
            }           

            return RedirectToAction("Index");
        }

        public ActionResult Score(int VersID)
        {
            RatingViewModel rating = ratings.getRatingModel(VersID); 
            CompleteRating model = rating.Rating;
            ViewBag.Task = rating.TaskVersion.Task.TaskName + " - Version " + rating.TaskVersion.Version; 
            return View(model);
        }
    }
}