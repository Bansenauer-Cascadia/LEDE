using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LEDE_MVC.Models;
using LEDE_MVC.Models.LEDE;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace LEDE_MVC.Controllers
{
    public class TaskRatingsController : Controller
    {
        private static List<SelectListItem> getRatingScores()
        {
            List<SelectListItem> ratingScores = new List<SelectListItem>();

            ratingScores.Add(new SelectListItem { Text = "0", Value = "0" });
            ratingScores.Add(new SelectListItem { Text = "1", Value = "1" });
            ratingScores.Add(new SelectListItem { Text = "2", Value = "2" });
            ratingScores.Add(new SelectListItem { Text = "3", Value = "3" });

            return ratingScores;

        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.ID = new SelectList(db.Users.Where(c => c.Roles.Select(r => r.RoleId).Contains(1)), "Id", "LastName");
            //ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskName");
            //ViewBag.VersID = new SelectList(db.TaskVersions.Where(r => r.UserID == 6).Where(r => r.TaskID == 1), "VersID", "Version"); 
            //ViewBag.VersID = new SelectList(db.TaskVersions, "VersID", "Version");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Select([Bind(Include = "TaskID,VersID,Id")]TaskVersion inputTaskVersion)
        {
            if (inputTaskVersion.TaskID > 0 && inputTaskVersion.ID > 0)
            {
                inputTaskVersion.User = db.Users.Single(u => u.Id == inputTaskVersion.ID);
                inputTaskVersion.Task = db.Tasks.Single(t => t.TaskID == inputTaskVersion.TaskID); 
                TempData["taskVersion"] = inputTaskVersion;
                return RedirectToAction("Rating"); 
            }
            var tasks = db.TaskVersions.Where(v => v.ID == inputTaskVersion.ID).
                Select(v => new { v.Task.TaskID, v.Task.TaskName }).Distinct();
            int taskCount = tasks.Count();
            if (taskCount > 0)
                ViewBag.TaskID = new SelectList(tasks, "TaskID", "TaskName");
            else
                ViewBag.TaskID = null;                       
           
            ViewBag.ID = new SelectList(db.Users, "ID", "LastName", inputTaskVersion.ID);

            return View();
        }

        // GET: TaskRatings
        public ActionResult Rating(int? EditRatingID)
        {
            var viewModel = new TaskRatingViewModel();
            viewModel.existingRatings = db.TaskRatings.Include(t => t.CoreRating).Include(t => t.TaskVersion).Include(t => t.User).
                Include(t => t.CoreRating.CoreTopic);

            ViewBag.CoreTopicID = new SelectList(db.CoreTopics, "CoreTopicID", "CoreTopicDesc");
            //ViewBag.KnowTypeID = new SelectList(db.Types.Where(c => c.TypeCat == "K"), "TypeID", "TypeCode");
            ViewBag.RatingScore = getRatingScores();

            if (TempData["taskVersion"] != null)
            {
                TaskVersion taskVersion = (TaskVersion)TempData["taskVersion"];
                ViewBag.ID = new SelectList(db.Users, "ID", "LastName", taskVersion.ID);
                ViewBag.TaskID = new SelectList(db.TaskVersions.Where(v => v.ID == taskVersion.ID).Select(v => new { v.TaskID, v.Task.TaskName }),
                    "TaskID","TaskName",taskVersion.TaskID);                
                TempData["taskVersion"] = taskVersion; 
            }            

            if (EditRatingID != null)
                ViewBag.EditRatingID = EditRatingID;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rating([Bind(Include = "RatingScore, RatingID")]TaskRating inputTaskRating,
            [Bind(Include = "CoreTopicID,KnowTypeID")] CoreRating inputCoreRating)
        {
            if (Request.Form["EditButton"] != null)
            {
                return RedirectToAction("Rating", new { EditRatingID = inputTaskRating.RatingID });
            }
            else if (Request.Form["DeleteButton"] != null)
            {
                var removedTaskRating = db.TaskRatings.First(m => m.RatingID == inputTaskRating.RatingID);
                var removedCoreRating = db.CoreRatings.First(m => m.RatingID == inputTaskRating.RatingID);
                db.CoreRatings.Remove(removedCoreRating);
                db.TaskRatings.Remove(removedTaskRating);
                db.SaveChanges();
                return RedirectToAction("Rating");
            }
            else if (Request.Form["InsertButton"] != null)
            {
                TaskRating taskRating = new TaskRating
                {
                    /*RatingScore = inputTaskRating.RatingScore,
                    FacultyID = Convert.ToInt32(User.Identity.GetUserId()),
                    VersID = 1,
                    ReviewDate = DateTime.Now*/
                };
                CoreRating coreRating = new CoreRating
                {
                    /*TaskRating = taskRating,
                    KnowTypeID = inputCoreRating.KnowTypeID,
                    CoreTopicID = inputCoreRating.CoreTopicID*/
                };
                db.TaskRatings.Add(taskRating);
                db.CoreRatings.Add(coreRating);
                db.SaveChanges();
                return RedirectToAction("Rating");
            }
            else if (Request.Form["SubmitButton"] != null)
            {
                /*var updatedRating = db.TaskRatings.Single(c => c.RatingID == inputTaskRating.RatingID);
                updatedRating.RatingScore = inputTaskRating.RatingScore;
                db.SaveChanges();*/
                return RedirectToAction("Rating");
            }
            return RedirectToAction("Rating");
        }

        // GET: TaskRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskRating taskRating = db.TaskRatings.Find(id);
            if (taskRating == null)
            {
                return HttpNotFound();
            }
            return View(taskRating);
        }

        // GET: TaskRatings/Create
        public ActionResult Create()
        {
            ViewBag.RatingID = new SelectList(db.CoreRatings, "RatingID", "RatingID");
            ViewBag.VersID = new SelectList(db.TaskVersions, "VersID", "RatingStatus");
            ViewBag.FacultyID = new SelectList(db.Users, "Id", "UniversityID");
            return View();
        }

        // POST: TaskRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RatingID,VersID,RatingScore,FacultyID,ReviewDate")] TaskRating taskRating)
        {
            if (ModelState.IsValid)
            {
                db.TaskRatings.Add(taskRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RatingID = new SelectList(db.CoreRatings, "RatingID", "RatingID", taskRating.RatingID);
            ViewBag.VersID = new SelectList(db.TaskVersions, "VersID", "RatingStatus", taskRating.VersID);
            ViewBag.FacultyID = new SelectList(db.Users, "Id", "UniversityID", taskRating.FacultyID);
            return View(taskRating);
        }

        // GET: TaskRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskRating taskRating = db.TaskRatings.Find(id);
            if (taskRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.RatingID = new SelectList(db.CoreRatings, "RatingID", "RatingID", taskRating.RatingID);
            ViewBag.VersID = new SelectList(db.TaskVersions, "VersID", "RatingStatus", taskRating.VersID);
            ViewBag.FacultyID = new SelectList(db.Users, "Id", "UniversityID", taskRating.FacultyID);
            return View(taskRating);
        }

        // POST: TaskRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RatingID,VersID,RatingScore,FacultyID,ReviewDate")] TaskRating taskRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RatingID = new SelectList(db.CoreRatings, "RatingID", "RatingID", taskRating.RatingID);
            ViewBag.VersID = new SelectList(db.TaskVersions, "VersID", "RatingStatus", taskRating.VersID);
            ViewBag.FacultyID = new SelectList(db.Users, "Id", "UniversityID", taskRating.FacultyID);
            return View(taskRating);
        }

        // POST: TaskRatings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "RatingID")] TaskRating taskRating)
        {
            TaskRating rating = db.TaskRatings.Find(taskRating.RatingID);
            db.TaskRatings.Remove(rating);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
