using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Entities;
using LEDE.Domain.Concrete;
using LEDE.Domain.Abstract;

namespace LEDE.WebUI.Controllers
{
    //[Authorize(Roles = "Faculty")]
    public class SummaryController : Controller
    {
        private ISummaryRepository db;
        private IPercentageCalculator Calculator;

        public SummaryController(ISummaryRepository repo, IPercentageCalculator calc)
        {
            this.db = repo;
            this.Calculator = calc;
        }

        [AllowAnonymous]
        public ActionResult Seminar(SeminarSummary summary)
        {
            //get dropdown sorted
            SelectList programCohorts = new SelectList(db.getCohorts(), "ProgramCohortID", "Program.ProgramTitle");
            int selectedCohortID = summary.SelectedCohortID == 0 ? Convert.ToInt32(programCohorts.First().Value) : summary.SelectedCohortID;

            //initialize page model 
            SeminarSummary model = db.getCohortTotals(1, 1);
            model.SelectedCohortID = selectedCohortID;
            Calculator.CalculateSeminarPercentages(model);
            model.ProgramCohorts = programCohorts;

            return View(model);
        }

        public ActionResult Student(int UserID, int ProgramCohortID)
        {            
            StudentSummary model = db.getStudentTotals(ProgramCohortID, UserID);
            Calculator.CalculateStudentPercentages(model);
            return View(model);
        }

        public ActionResult Unformatted(int ProgramCohortID = 1, int UserID = 1)
        {
            StudentSummary model = db.getStudentTotals(ProgramCohortID, UserID);
            Calculator.CalculateStudentPercentages(model);
            return View(model);
        }

        public ActionResult Summary(int ProgramCohortID =1, int UserID =1)
        {
            SpreadsheetModel model = db.getSpreadsheetTable(ProgramCohortID, UserID); 
            return View(model); 
        }
    }
}