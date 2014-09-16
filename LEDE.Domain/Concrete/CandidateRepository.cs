using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;
using System.Web.Mvc;

namespace LEDE.Domain.Concrete
{
    public class CandidateRepository : ICandidateRepository
    {
        private DbContext db;

        public CandidateRepository()
        {
            this.db = new DbContext();
        }

        public IEnumerable<TaskVersion> getTaskVersions(int UserID, int TaskID)
        {
            return db.TaskVersions.Where(v => v.UserID == UserID && v.TaskID == TaskID);
        }

        private IEnumerable<SelectListItem> getTaskDrop(int ProgramCohortID)
        {
            int programID = db.ProgramCohorts.Find(ProgramCohortID).ProgramID; 
            return new SelectList(db.Tasks.Where(t=> t.Seminar.ProgramID == programID).Select(t => new 
            { Name = t.TaskCode + t.TaskName, t.TaskID }), "TaskID", "Name");
        }

        private IEnumerable<SelectListItem> getCohortDrop(int UserID)
        {
            return db.CohortEnrollments.Where(e => e.UserID == UserID).Select(e => new SelectListItem()
            {
                Value = e.ProgramCohortID.ToString(),
                Text = e.ProgramCohort.Program.ProgramTitle + " " + e.ProgramCohort.AcademicYear
            }).OrderBy(e => e.Text); 
        }

        public CandidateIndexModel getIndexModel(int UserID, int? ProgramCohortID, int? TaskID)
        {
            CandidateIndexModel model = new CandidateIndexModel() { };
            model.Cohorts = new SelectList(getCohortDrop(UserID), "Value", "Text", ProgramCohortID);
            model.ProgramCohortID = ProgramCohortID ?? Convert.ToInt32(model.Cohorts.First().Value);
            model.Tasks = getTaskDrop(model.ProgramCohortID);

            model.TaskID = TaskID ?? Convert.ToInt32(model.Tasks.First().Value); 

            model.taskVersions = getTaskVersions(UserID, model.TaskID);

            return model;
        }


        public void uploadTask(int taskID, int userID, Document uploadDoc)
        {
            var taskVersions = db.TaskVersions.Where(v => v.UserID == userID && v.TaskID == taskID);
            TaskVersion uploadTask = new TaskVersion()
            {
                Document = uploadDoc,
                RatingStatus = "Pending",
                TaskID = taskID,
                UserID = userID,
                Version = taskVersions.FirstOrDefault() != null ?
                taskVersions.Max(v => v.Version) + 1 : 1
            };
            uploadDoc.Blob = uploadTask.TaskID + "/" + uploadTask.Version;

            db.Documents.Add(uploadDoc);
            db.TaskVersions.Add(uploadTask);
            db.SaveChanges();
        }


        public Document findDocument(int documentID)
        {
            return db.Documents.Find(documentID);
        }


        public void DeleteTask(int documentID)
        {
            Document deleteDoc = db.Documents.Find(documentID);
            TaskVersion deleteVersion = db.TaskVersions.FirstOrDefault(v => v.DocumentID == documentID);

            db.TaskVersions.Remove(deleteVersion);
            db.Documents.Remove(deleteDoc);
            db.SaveChanges();
        }



        public IEnumerable<CandidateSummaryRow> getSummaryModel(int UserID, int? ProgramCohortID)
        {
            int CohortID = ProgramCohortID ?? db.CohortEnrollments.Where(u => u.UserID == UserID).Max(u => u.ProgramCohortID);
            int programID = db.ProgramCohorts.FirstOrDefault(p => p.ProgramCohortID == CohortID).Program.ProgramID;
            List<CandidateSummaryRow> model = new List<CandidateSummaryRow>();
            IEnumerable<TaskVersion> taskVersions = db.TaskVersions.Where(v => v.UserID == UserID && v.Task.Seminar.ProgramID == programID);

            foreach (Task task in db.Tasks.Where(t => t.Seminar.ProgramID == programID))
            {
                CandidateSummaryRow taskModel = new CandidateSummaryRow()
                {
                    Task = task,
                    candidateSubmissions = taskVersions.Where(v => v.TaskID == task.TaskID).OrderBy(v => v.Version).ToList()
                };
                model.Add(taskModel);
            }

            return model;
        }


        public CohortDropDown getCohorts(int UserID, int ProgramCohortID)
        {
            CohortDropDown model = new CohortDropDown();

            IEnumerable<CohortEnrollment> userEnrollments = db.CohortEnrollments.Where(e => e.UserID == UserID);
            if (!userEnrollments.Any())
                model.ProgramCohortID = 0;
            else
            {
                model.ProgramCohortID = ProgramCohortID == 0 ? userEnrollments.OrderByDescending(e => e.ProgramCohort.AcademicYear).
                    First().ProgramCohortID : ProgramCohortID;
            }

            model.UserCohorts = new SelectList(userEnrollments.Select(e => 
                new { e.ProgramCohortID, ProgramTitle = e.ProgramCohort.Program.ProgramTitle }),
                "ProgramCohortID", "ProgramTitle", ProgramCohortID);

            return model;
        }


        public FacultySummaryModel getFacultySummary(int ProgramCohortID)
        {
            FacultySummaryModel model = new FacultySummaryModel();

            int ProgramID = db.ProgramCohorts.Single(c => c.ProgramCohortID == ProgramCohortID).ProgramID;

            IEnumerable<TaskVersion> maxVersions =
                from v in db.TaskVersions
                where v.Task.Seminar.ProgramID == ProgramID && v.User.CohortEnrollments.Any(e => e.ProgramCohortID == ProgramCohortID)
                group v by new { v.UserID, v.TaskID } into m
                let maxVersion = m.Max(v => v.Version)
                select m.FirstOrDefault(v=> v.Version == maxVersion);

            IEnumerable<Task> tasks = db.Tasks.Where(t => t.Seminar.ProgramID == ProgramID);

            model.MaxVersions = maxVersions;
            model.CohortTasks = tasks;
            model.CohortCandidates = db.Users.Where(user => user.CohortEnrollments.Any(e => e.ProgramCohortID == ProgramCohortID)
                && user.Roles.Any(r=> r.RoleId == 1));
            model.ProgramCohortID = ProgramCohortID; 

            return model;
        }
    }
}