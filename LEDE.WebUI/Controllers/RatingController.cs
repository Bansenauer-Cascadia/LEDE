using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles="Faculty")]
    public class RatingController : Controller
    {
        private LEDE.Domain.Abstract.IRatingRepository Ratings; 

        public RatingController(LEDE.Domain.Abstract.IRatingRepository repo)
        {
            this.Ratings = repo;
        }

        public PartialViewResult GetIndexData(int SelectedUserID, int ProgramCohortID)
        {
            return PartialView(Ratings.getTaskVersions(SelectedUserID, ProgramCohortID)); 
        }

        public ActionResult Index(bool? UploadVisible, int? VersID, int? userID, int? ProgramCohortID)
        {
            int FacultyID = Convert.ToInt32(User.Identity.GetUserId()); 
            RatingIndexModel model = Ratings.getIndexModel(FacultyID, ProgramCohortID, userID);
            model.UploadVisible = UploadVisible ?? false;
            model.VersID = VersID;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RatingIndexPost post)
        {
            int FacultyID = Convert.ToInt32(User.Identity.GetUserId()); 
            RatingIndexModel model = Ratings.getIndexModel(FacultyID, post.SelectedCohortID, post.SelectedUserID);

            return View(model);
        }

        public ActionResult Download(int DocumentID)
        {            
            Document downloadDoc = Ratings.findDocument((int)DocumentID);            
            FileManager.DownloadDocument(downloadDoc, HttpContext);             

            return RedirectToAction("Index");
        }

        public PartialViewResult Upload(int VersID, int SelectedUserID, string User, string TaskName, int Version, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl; 
            ViewBag.Version = Version;
            ViewBag.SelectedUserID = SelectedUserID; 
            ViewBag.User = User;
            ViewBag.TaskName = TaskName; 
            return PartialView(VersID);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int SelectedUserID, int VersID, string returnUrl)
        {
            
            if (file != null && file.ContentLength > 0)
            {
                Document feedbackUpload = new Document
                {
                    FileName = file.FileName,
                    FileSize = file.ContentLength.ToString(),
                    UploadDate = DateTime.Now,
                    Container = "user" + SelectedUserID
                };               
                
                Ratings.saveFeedback(feedbackUpload, VersID);
                FileManager.UploadDocument(feedbackUpload, file); 
            }
            if(returnUrl != null)
                return Redirect("~" + returnUrl);
            else
                return RedirectToAction("Index", new {returnUrl});
        }

        public ActionResult Rate(int VersID)
        {
            RatingViewModel model = Ratings.getRatingModel(VersID);

            return View(model);
        }

        
        [HttpPost]
        public ActionResult Header(RatingViewModel post)
        {
            return View("Rate", Ratings.getRatingModel(post.VersID));
        }

        [HttpPost]
        public ActionResult Reflection(RatingViewModel post)
        {
            if (ModelState.IsValid)
            {
                if (post.TaskVersion.ReadingLogEntry != null)
                    Ratings.EditReading(post.VersID, post.TaskVersion.ReadingLogEntry.NumEntries);
                else if (post.TaskVersion.InternReflection != null)
                    Ratings.EditReflection(post.VersID, post.TaskVersion.InternReflection.NumHrs);
                return View("Rate", Ratings.getRatingModel(post.VersID));
            }
            else
            {
                RatingViewModel model = Ratings.getRatingModel(post.VersID);
                model.TaskVersion.InternReflection = post.TaskVersion.InternReflection;
                model.TaskVersion.ReadingLogEntry = post.TaskVersion.ReadingLogEntry;
                return View("Rate", model);
            }
        }

        [HttpPost]
        public ActionResult Task(RatingViewModel post)
        {
            if (Request.IsAjaxRequest())
            {
                ///this should prove usefulll!
            }
            if (ModelState.IsValid)
            {
                post.Rating.FacultyID = Convert.ToInt32(User.Identity.GetUserId()); 
                switch (post.SubmitCommand)
                {
                    case "Save":
                        Ratings.saveTaskRating(post.Rating, post.VersID);
                        break;
                }
                return View("Rate", Ratings.getRatingModel(post.VersID));
            }
            else
            {
                RatingViewModel model = Ratings.getRatingModel(post.VersID);
                model.Rating.TaskCoreRatings = post.Rating.TaskCoreRatings;
                return View("Rate", model);
            }
        }

        [HttpPost]
        public ActionResult Other(RatingViewModel post)
        {
            RatingViewModel model;

            if (ModelState.IsValid)
            {
                switch (post.SubmitCommand)
                {
                    case "Save":
                        post.Rating.FacultyID = Convert.ToInt32(User.Identity.GetUserId()); 
                        Ratings.saveTaskRating(post.Rating, post.VersID);
                        model = Ratings.getRatingModel(post.VersID);
                        break;
                    case "Add":
                        model = Ratings.getRatingModel(post.VersID);
                        model.OtherVisible = true;
                        break;
                    default:
                        model = Ratings.getRatingModel(post.VersID);
                        break;
                }
                return View("Rate", model); 
            }
            else
            {
                model = Ratings.getRatingModel(post.VersID); 
                return View("Rate", model); 
            }
        }

        [HttpPost]
        public ActionResult Impact(RatingViewModel post)
        {
            RatingViewModel model;

            if (ModelState.IsValid)
            {
                switch (post.SubmitCommand)
                {
                    case "Save":
                        post.Rating.FacultyID = Convert.ToInt32(User.Identity.GetUserId()); 
                        Ratings.saveImpactRating(post.Rating, post.VersID);
                        model = Ratings.getRatingModel(post.VersID);
                        break;
                    case "Add":
                        model = Ratings.getRatingModel(post.VersID);
                        model.ImpactVisible = true;
                        break;
                    default:
                        model = Ratings.getRatingModel(post.VersID);
                        break;
                }
                return View("Rate", model);
            }
            else
            {
                model = Ratings.getRatingModel(post.VersID);
                model.Rating.ImpactRating = post.Rating.ImpactRating;
                return View("Rate", model);
            }
        }

        public ActionResult Clear(int VersID, int RatingID)
        {
            if(VersID > 0)
                Ratings.deleteTaskRating(RatingID);
            return View("Rate", Ratings.getRatingModel(VersID));
        }
    }
}