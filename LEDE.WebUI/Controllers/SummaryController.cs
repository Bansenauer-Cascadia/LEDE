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
    [Authorize(Roles = "Faculty")]
    public class SummaryController : Controller
    {
        private ISummaryRepository Summary;
        private IPercentageCalculator Calculator;

        public SummaryController(ISummaryRepository repo, IPercentageCalculator calc)
        {
            this.Summary = repo;
            this.Calculator = calc;
        }

        [AllowAnonymous]
        public ActionResult Seminar(SeminarSummary summary)
        {
            //get dropdown sorted
            SelectList programCohorts = new SelectList(Summary.getCohorts(), "ProgramCohortID", "Program.ProgramTitle");
            int selectedCohortID = summary.SelectedCohortID == 0 ? Convert.ToInt32(programCohorts.First().Value) : summary.SelectedCohortID;

            //initialize page model 
            SeminarSummary model = Summary.getCohortTotals(1, 1);
            model.SelectedCohortID = selectedCohortID;
            Calculator.CalculateSeminarPercentages(model);
            model.ProgramCohorts = programCohorts;

            return View(model);
        }

        public ActionResult Student(int UserID, int ProgramCohortID)
        {            
            StudentSummary model = Summary.getStudentTotals(ProgramCohortID, UserID);
            Calculator.CalculateStudentPercentages(model);
            return View(model);
        }

        public ActionResult Unformatted()
        {
            StudentSummary model = Summary.getStudentTotals(1, 1);
            Calculator.CalculateStudentPercentages(model);
            return View(model);
        }
    }
}