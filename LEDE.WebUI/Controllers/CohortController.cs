using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Entities;
using LEDE.Domain.Abstract;

namespace LEDE.WebUI.Controllers
{
    public class CohortController : Controller
    {
        private IEnrollmentRepository db;

        public CohortController(IEnrollmentRepository repo)
        {
            this.db = repo;
        }
        // GET: Cohort
        public ActionResult Index(int ProgramCohortID = 0)
        {
            IEnumerable<ProgramCohort> model = db.getCohorts();
            ViewBag.ProgramCohortID = ProgramCohortID;

            return View(model);
        }

        public PartialViewResult CohortUsers(int ProgramCohortID = 0)
        {           
            CohortUsers model = db.getCohortUsers(ProgramCohortID);
            return PartialView(model);
        }

        public ActionResult AddCohortUsers(CohortUsers users)
        {
            db.addCohortUsers(users.getIdsToAdd(), users.ProgramCohortID);
            return RedirectToAction("CohortUsers", new {ProgramCohortID = users.ProgramCohortID });
        }

        public ActionResult RemoveCohortUsers(CohortUsers users)
        {
            db.removeCohortUsers(users.getIdsToRemove(), users.ProgramCohortID);
            return RedirectToAction("CohortUsers", new { ProgramCohortID = users.ProgramCohortID });
        }
    }
}