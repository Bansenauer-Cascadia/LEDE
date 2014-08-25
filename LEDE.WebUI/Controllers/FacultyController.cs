using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles="Faculty")]
    public class FacultyController : Controller
    {
        private ICandidateRepository db;

        public FacultyController(ICandidateRepository repo)
        {
            this.db = repo; 
        }
        public ActionResult Summary(int ProgramCohortID = 0)
        {
            int UserID = Convert.ToInt32(User.Identity.GetUserId());
            CohortDropDown model = db.getCohorts(UserID, ProgramCohortID);
            return View(model);
        }

        public PartialViewResult SummaryTable(int ProgramCohortID)
        {
            FacultySummaryModel model = db.getFacultySummary(ProgramCohortID);
            return PartialView(model);
        }
    }
}