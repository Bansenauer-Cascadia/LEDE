using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Concrete;
using LEDE.Domain.Entities;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

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

        public PartialViewResult GetIndexData(int UserID)
        {
            return PartialView(Ratings.getTaskVersions(UserID)); 
        }

        public ActionResult Index(bool? UploadVisible, int? VersID, int? userID)
        {
            RatingIndexModel model = Ratings.getIndexModel(userID);
            model.UploadVisible = UploadVisible ?? false;
            model.VersID = VersID;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RatingIndexPost post)
        {
            RatingIndexModel model = Ratings.getIndexModel(post.SelectedUserID);

            return View(model);
        }

        public ActionResult Download(int DocumentID, int UserID)
        {
            Document downloadDoc = Ratings.findDocument(DocumentID);

            FileManager.DownloadDocument(downloadDoc, HttpContext);             

            return RedirectToAction("Index");
        }

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

            return RedirectToAction("Index");
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
            if (ModelState.IsValid)
            {
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