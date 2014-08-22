using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Entities;
using LEDE.Domain.Abstract; 

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles = "ECSEL Admin, LEDE Admin, Super Admin")]
    public class EnrollmentController : Controller
    {
        private IEnrollmentRepository db;

        public EnrollmentController(IEnrollmentRepository repo)
        {
            this.db = repo;
        }

        public ActionResult Index(int? ProgramCohortID)
        {
            EnrollmentViewModel model = new EnrollmentViewModel();
            model.Cohorts = db.getCohorts();
            if (ProgramCohortID != null)
            {
                model.ProgramCohortID = (int)ProgramCohortID; 
                model.Users = db.getCohortUsers((int)ProgramCohortID);
                ProgramCohort viewbagCohort = model.Cohorts.FirstOrDefault(c => c.ProgramCohortID == (int)ProgramCohortID);
                ViewBag.Cohort = viewbagCohort.Program.ProgramTitle + " " + viewbagCohort.AcademicYear; 
            }
                
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(EnrollmentViewModel postback, CohortFunction function)
        {
            switch (function)
            {
                case CohortFunction.Add:
                    db.addCohortUsers(postback.getIdsToAdd(), postback.ProgramCohortID);
                    break;
                case CohortFunction.Remove:
                    db.removeCohortUsers(postback.getIdsToRemove(), postback.ProgramCohortID); 
                    break; 
            }
            return RedirectToAction("Index", "Enrollment", new { ProgramCohortID = postback.ProgramCohortID }); 
        }

        public ActionResult Add()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Remove()
        {
            return RedirectToAction("Index"); 
        }
    }
}