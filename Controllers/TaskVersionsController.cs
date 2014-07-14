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
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LEDE_MVC.Controllers
{
    public class TaskVersionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected static string accountName = "ledeportal";
        protected static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";
 
        private static void UploadFile(int userID, UploadViewModel model, HttpPostedFileBase file)
        {
            string containerString = "user-" + userID;
            
            string versionid = "1";

            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer studentContainer = client.GetContainerReference(containerString);
                studentContainer.CreateIfNotExists();

                CloudBlockBlob blob = studentContainer.GetBlockBlobReference(model.TaskID + "-" + versionid);
                using (Stream fileStream = file.InputStream)
                {
                    blob.UploadFromStream(fileStream);
                }
                var reader = blob.OpenRead();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);               
            }

        }

        // GET: TaskVersions
        public ActionResult Index()
        {            
            var viewModel = new UploadViewModel();
            int id = Convert.ToInt32(User.Identity.GetUserId()); 

            var taskVersions = db.TaskVersions.Include(t => t.Document).Include(t => t.Task).Include(t => t.User).
                Include(t => t.FeedbackDocument).Where(t => t.ID == id);  
            viewModel.TaskVersions = taskVersions;

            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskName");
            ViewBag.User = User.Identity.GetUserId();
           
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include="TaskID")]UploadViewModel viewModel, HttpPostedFileBase file)
        {
            
            int id = Convert.ToInt32(User.Identity.GetUserId()); 
            viewModel.TaskVersions = db.TaskVersions.Include(t => t.Document).Include(t => t.Task).
                Include(t => t.User).Include(t => t.FeedbackDocument).Where(t => t.TaskID == viewModel.TaskID).
                Where(t => t.ID == id);
            
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskName");

            if (file != null && file.ContentLength > 0)
            {
                UploadFile(id, viewModel, file); 
            }

            return View(viewModel); 
        }

        // GET: TaskVersions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskVersion taskVersion = db.TaskVersions.Find(id);
            if (taskVersion == null)
            {
                return HttpNotFound();
            }
            return View(taskVersion);
        }

        // GET: TaskVersions/Create
        public ActionResult Create()
        {
            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "FileName");
            ViewBag.VersID = new SelectList(db.InternReflections, "VersID", "VersID");
            ViewBag.VersID = new SelectList(db.ReadingLogs, "VersID", "ReadingTitle");
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskCode");
            ViewBag.UserID = new SelectList(db.Users, "Id", "UniversityID");
            return View();
        }

        // POST: TaskVersions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VersID,TaskID,DocumentID,Version,RatingStatus,FeedbackDocID,UserID")] TaskVersion taskVersion)
        {
            if (ModelState.IsValid)
            {
                db.TaskVersions.Add(taskVersion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "FileName", taskVersion.DocumentID);
            ViewBag.VersID = new SelectList(db.InternReflections, "VersID", "VersID", taskVersion.VersID);
            ViewBag.VersID = new SelectList(db.ReadingLogs, "VersID", "ReadingTitle", taskVersion.VersID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskCode", taskVersion.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UniversityID", taskVersion.ID);
            return View(taskVersion);
        }

        // GET: TaskVersions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskVersion taskVersion = db.TaskVersions.Find(id);
            if (taskVersion == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "FileName", taskVersion.DocumentID);
            ViewBag.VersID = new SelectList(db.InternReflections, "VersID", "VersID", taskVersion.VersID);
            ViewBag.VersID = new SelectList(db.ReadingLogs, "VersID", "ReadingTitle", taskVersion.VersID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskCode", taskVersion.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UniversityID", taskVersion.ID);
            return View(taskVersion);
        }

        // POST: TaskVersions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VersID,TaskID,DocumentID,Version,RatingStatus,FeedbackDocID,UserID")] TaskVersion taskVersion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskVersion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentID = new SelectList(db.Documents, "DocumentID", "FileName", taskVersion.DocumentID);
            ViewBag.VersID = new SelectList(db.InternReflections, "VersID", "VersID", taskVersion.VersID);
            ViewBag.VersID = new SelectList(db.ReadingLogs, "VersID", "ReadingTitle", taskVersion.VersID);
            ViewBag.TaskID = new SelectList(db.Tasks, "TaskID", "TaskCode", taskVersion.TaskID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "UniversityID", taskVersion.ID);
            return View(taskVersion);
        }

        // GET: TaskVersions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskVersion taskVersion = db.TaskVersions.Find(id);
            if (taskVersion == null)
            {
                return HttpNotFound();
            }
            return View(taskVersion);
        }

        // POST: TaskVersions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskVersion taskVersion = db.TaskVersions.Find(id);
            db.TaskVersions.Remove(taskVersion);
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
