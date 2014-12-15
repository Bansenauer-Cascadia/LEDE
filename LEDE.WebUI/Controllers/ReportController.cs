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
    //[Authorize(Roles = "Faculty")]
    public class ReportController : Controller
    {
        private ISummaryRepository db;

        private IPercentageCalculator Calculator;

        private IProgramCohortRepository Cohorts;

        public ReportController(ISummaryRepository repo, IPercentageCalculator calc, IProgramCohortRepository Cohorts)
        {
            this.db = repo;
            this.Calculator = calc;
            this.Cohorts = Cohorts; 
        }

        public ActionResult Seminar(SeminarSummary summary)
        {
            int FacultyID; 

            if (User.IsInRole("ECSEL Admin") || User.IsInRole("LEDE Admin") || User.IsInRole("Super Admin"))
            {
                FacultyID = 0;
            }
            else
            {
                FacultyID = Convert.ToInt32(User.Identity.GetUserId());
            }

            //get dropdown sorted
            SelectList programCohorts = new SelectList(db.getCohorts(FacultyID), "ProgramCohortID", "Program.ProgramTitle");
            int selectedCohortID = summary.SelectedCohortID == 0 ? Convert.ToInt32(programCohorts.First().Value) : summary.SelectedCohortID;

            //initialize page model 
            SeminarSummary model = db.getCohortTotals(selectedCohortID);
            model.SelectedCohortID = selectedCohortID;
            Calculator.CalculateSeminarPercentages(model);
            model.ProgramCohorts = programCohorts;

            return View(model);
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

        public ActionResult CohortHours(int ProgramCohortID)
        {
            List<InternReflection> CohortReflections = Cohorts.GetCohortInternReflections(ProgramCohortID);
            List<ReadingLogEntry> CohortEntries = Cohorts.GetCohortReadingLogEntries(ProgramCohortID);
            List<HoursDTO> HoursDTOs = GetHoursDTOs(CohortReflections);
            List<EntryDTO> EntryDTOs = GetEntryDTOs(CohortEntries);

            return View(); 
        }

        public List<HoursDTO> GetHoursDTOs(List<InternReflection> Reflections)
        {
            return Reflections.Select(r => new HoursDTO(r)).ToList();
        }

        public List<EntryDTO> GetEntryDTOs(List<ReadingLogEntry> Entries)
        {
            return Entries.Select(e => new EntryDTO(e)).ToList();            
        }
    }

    public class TotalsManager
    {
        public TotalsManager(int foo)
        {

        }
    }
}