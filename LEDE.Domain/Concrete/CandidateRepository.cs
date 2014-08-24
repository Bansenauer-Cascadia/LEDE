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

        private SelectList getTaskDrop()
        {
            return new SelectList(db.Tasks.Select(t => new { Name = t.TaskCode + t.TaskName, t.TaskID }), "TaskID", "Name");
        }

        public CandidateIndexModel getIndexModel(int UserID, int? TaskID)
        {
            CandidateIndexModel model = new CandidateIndexModel() { };
            model.Tasks = getTaskDrop();

            if (TaskID == null)
            {
                if (model.Tasks.FirstOrDefault() != null)
                    model.TaskID = Convert.ToInt32(model.Tasks.FirstOrDefault().Value);
                else
                    model.TaskID = 0;
            }
            else
                model.TaskID = (int)TaskID;

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
            TaskVersion deleteVersion = deleteDoc.TaskVersions.FirstOrDefault();

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
            if (userEnrollments == null)
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
    }
}