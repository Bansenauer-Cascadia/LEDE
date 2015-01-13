using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using LEDE.Domain.Abstract;
using LEDE.Domain.Repositories;
using Microsoft.AspNet.Identity;
using LEDE.WebUI.DTOs;

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles = "Faculty, LEDE Admin, ECSEL Admin, Super Admin")]
    public class ReportController : Controller
    {
        private ISummaryRepository db;

        private IPercentageCalculator Calculator;

        private ICohortTotalsRepository CohortTotals;

        private int FacultyID; 

        public ReportController(ISummaryRepository repo, IPercentageCalculator calc, ICohortTotalsRepository CohortTotals)
        {
            this.db = repo;
            this.Calculator = calc;
            this.CohortTotals = CohortTotals;            
        }

        public ActionResult Seminar(int? ProgramCohortID)
        {            
            SelectList userCohorts = ProgramCohortsForUser(ProgramCohortID);
            int selectedCohortID = ProgramCohortID ?? Convert.ToInt32(userCohorts.SelectedValue);
            ViewBag.Cohorts = userCohorts;

            IEnumerable<RatingQuery> model = db.getCohortTotals(selectedCohortID);
            return View(model);
        }

        public ActionResult CohortHours(int? ProgramCohortID)
        {            
            SelectList userCohorts = ProgramCohortsForUser(ProgramCohortID);
            int selectedCohortID = ProgramCohortID ?? Convert.ToInt32(userCohorts.SelectedValue);
            ViewBag.Cohorts = userCohorts;

            IEnumerable<LogAndReadingGraphsDTO> model = CohortTotals.GetHoursTotals(selectedCohortID);
            return View(model);
        }

        public SelectList ProgramCohortsForUser(int? ProgramCohortID)
        {
            int FacultyID; 
            if (User.IsInRole("Super Admin") || User.IsInRole("LEDE Admin") || User.IsInRole("ECSEL Admin")) FacultyID = 0;
            else FacultyID = Int32.Parse(User.Identity.GetUserId());
            IEnumerable<ProgramCohort> FacultyCohorts = db.getCohorts(FacultyID);
            int selectedProgramCohortID = ProgramCohortID ?? FacultyCohorts.First().ProgramCohortID;

            SelectList programCohorts = new SelectList(FacultyCohorts, "ProgramCohortID",
                "Program.ProgramTitle", selectedProgramCohortID);
            return programCohorts;
        }

        public ActionResult Student(int? UserID, int? ProgramCohortID)
        {
            int userID = UserID ?? Convert.ToInt32(User.Identity.GetUserId());
            int programCohortID = ProgramCohortID ?? db.getStudentCohortID(userID);
            StudentSummary model = db.getStudentTotals(programCohortID, userID);
            Calculator.CalculateStudentPercentages(model);
            return View(model);
        }

        public ActionResult Summary(SummaryModel post)
        {
            int? SelectedCohortID = post.ProgramCohortID == 0 ? (int?)null : post.ProgramCohortID;
            SummaryModel model = db.getSummaryCohorts(Convert.ToInt32(User.Identity.GetUserId()), SelectedCohortID);
            db.getSummaryCandidates(model);

            return View(model);
        }

        public PartialViewResult SummarySpreadsheet(int ProgramCohortID, int UserID)
        {
            SpreadsheetModel model = db.getSpreadsheetTable(ProgramCohortID, UserID);
            return PartialView(model);
        }
     
    }
}