using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LEDE.Domain.Entities;
using LEDE.Domain.Abstract;

namespace LEDE.WebUI.Controllers
{
    [Authorize(Roles="ECSEL Admin, Super Admin, LEDE Admin")]
    public class EnrollmentController : Controller
    {
        private IEnrollmentRepository db;

        public EnrollmentController(IEnrollmentRepository repo)
        {
            this.db = repo;
        }

        public ActionResult Programs(string DeleteEntity, int DeleteID = 0, int SelectedItemID = 0)
        {
            //Handle Deletion for non-ajax requests 
            if (DeleteEntity != null)
            {
                if (DeleteEntity == "Cohort")
                    db.deleteProgramCohort(DeleteID);
                else if (DeleteEntity == "Seminar")
                    db.deleteProgramSeminar(DeleteID); 
            }

            ListViewModel model = new ParentViewModel()
            {
                Items = db.getPrograms().Select(p => new ListItem() {Id = p.ProgramID, Name = p.ProgramTitle }),
                editAction = "EditProgram",
                Entity = "Program",
                ChildEntities = new List<ChildEntity>()
                {
                    new ChildEntity(){Entity = "Cohort", Action="ProgramCohorts"},
                    new ChildEntity(){Entity = "Seminar", Action="ProgramSeminars"}
                },
                SelectedItemID = SelectedItemID, 
            };
            return View("ListPanel",model);
        }
        

        public ActionResult EditProgram(int Id)
        {
            Program model = db.getPrograms().Single(p => p.ProgramID == Id); 
            return View(model); 
        }

        [HttpPost]
        public ActionResult EditProgram(Program post)
        {
            db.editProgram(post);
            return RedirectToAction("Programs"); 
        }

        public PartialViewResult ProgramItems(int ParentID, string delete, int Id = 0)
        {
            if (delete == "Cohort")
                db.deleteProgramCohort(Id);
            else if (delete == "Seminar")
                db.deleteProgramSeminar(Id);

            ListViewModel cohorts = null;
            ListViewModel seminars = null; 

            if(delete == "Cohort" || delete == null)
            {
                cohorts = new ListViewModel()
                {
                    Items = db.getCohorts().Where(pc => pc.ProgramID == ParentID).
                    Select(pc => new ListItem() { Id = pc.ProgramCohortID, Name = pc.Program.ProgramTitle + " " + pc.AcademicYear }),
                    Entity = "Cohort",
                    createAction = "createProgramCohort",
                    deleteAction = "ProgramItems",
                    editAction = "Index"
                };
            }
            
            if(delete == "Seminar" || delete == null)
            {
                seminars = new ListViewModel()
                {
                    Items = db.getSeminars().Where(s => s.ProgramID == ParentID).
                    Select(s => new ListItem() { Id = s.SeminarID, Name = s.SeminarTitle }),
                    Entity = "Seminar",
                    createAction = "createProgramSeminar",
                    deleteAction = "ProgramItems",
                    editAction = "Seminars",
                };         
            }              
            
            ChildViewModel model = new ChildViewModel()
            {
                ParentID = ParentID,
                Items = new List<ListViewModel>() { cohorts, seminars }
            };

            return PartialView("ChildPanel", model);
        }        

        public ActionResult createProgramCohort(int Id)
        {
            ProgramCohort model = new ProgramCohort() { ProgramID = Id };
            return View(model);
        }

        [HttpPost]
        public ActionResult createProgramCohort(ProgramCohort post)
        {
            db.createProgramCohort(post);
            return RedirectToAction("Programs", new {SelectedItemID = post.ProgramID});
        }       

        public ActionResult createProgramSeminar(int Id)
        {
            Seminar model = new Seminar() { ProgramID = Id };
            return View(model);
        }

        [HttpPost]
        public ActionResult createProgramSeminar(Seminar post)
        {
            db.createProgramSeminar(post);
            return RedirectToAction("Programs", new { SelectedItemID = post.ProgramID }); 
        }

/////////////////////////////////////////////////
        public ActionResult Seminars(string DeleteEntity, int DeleteID = 0, int SelectedItemID = 0)
        {
            //Handle Deletion for non-ajax requests 
            if (DeleteEntity != null)
            {
                if (DeleteEntity == "CoreTopic")
                    db.deleteSeminarCoreTopic(DeleteID);
                else if (DeleteEntity == "Task")
                    db.deleteSeminarTask(DeleteID);
            }

            ListViewModel model = new ParentViewModel()
            {
                Items = db.getSeminars().Select(s => new ListItem() { Id = s.SeminarID, Name = s.SeminarTitle }),
                editAction = "EditSeminar",
                Entity = "Seminar",
                ChildEntities = new List<ChildEntity>()
                {
                    new ChildEntity(){Entity = "CoreTopic", Action="SeminarCoreTopics"},
                    new ChildEntity(){Entity = "Task", Action="SeminarTasks"}
                },
                SelectedItemID = SelectedItemID,
            };
            return View("ListPanel", model);
        }


        public ActionResult EditSeminar(int Id)
        {
            Seminar model = db.getSeminars().Single(s => s.SeminarID == Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSeminar(Seminar post)
        {
            db.editSeminar(post);
            return RedirectToAction("Seminars");
        }

        public PartialViewResult SeminarItems(int ParentID, string delete, int Id = 0)
        {
            if (delete == "CoreTopic")
                db.deleteSeminarCoreTopic(Id);
            else if (delete == "Task")
                db.deleteSeminarTask(Id);

            ListViewModel coretopics = null;
            ListViewModel tasks = null;

            if (delete == "CoreTopic" || delete == null)
            {
                coretopics = new ListViewModel()
                {
                    Items = db.getCoreTopics().Where(ct => ct.SeminarID == ParentID).
                    Select(ct => new ListItem() { Id = ct.CoreTopicID, Name = ct.CoreTopicNum + " " + ct.CoreTopicDesc}),
                    Entity = "CoreTopic",
                    createAction = "createSeminarCoreTopic",
                    deleteAction = "SeminarItems",
                    editAction = "editCoreTopic"
                };
            }

            if (delete == "Task" || delete == null)
            {
                tasks = new ListViewModel()
                {
                    Items = db.getTasks().Where(t => t.SeminarID == ParentID).
                    Select(t => new ListItem() { Id = t.TaskID, Name = t.TaskCode + ": " + t.TaskName }),
                    Entity = "Task",
                    createAction = "createSeminarTask",
                    deleteAction = "SeminarItems",
                    editAction = "editTask",
                };
            }

            ChildViewModel model = new ChildViewModel()
            {
                ParentID = ParentID,
                Items = new List<ListViewModel>() { coretopics, tasks }
            };

            return PartialView("ChildPanel", model);
        }

        public ActionResult createSeminarCoreTopic(int Id)
        {
            CoreTopic model = new CoreTopic() { SeminarID = Id, ModifyDate = DateTime.Now };
            return View(model);
        }

        [HttpPost]
        public ActionResult createSeminarCoreTopic(CoreTopic post)
        {
            if (post.CoreTopicID > 0)
                db.editSeminarCoretopic(post);
            else
                db.createSeminarCoreTopic(post);
            return RedirectToAction("Seminars", new { SelectedItemID = post.SeminarID });
        }

        public ActionResult createSeminarTask(int Id)
        {
            Task model = new Task() { SeminarID = Id };
            ViewBag.TaskTypeID = new SelectList(db.getTaskTypes(),"TaskTypeID","TaskTypeDescription");
            return View(model);
        }

        [HttpPost]
        public ActionResult createSeminarTask(Task post)
        {
            if (post.TaskID > 0)
                db.editSeminarTask(post);
            else
                db.createSeminarTask(post);

            return RedirectToAction("Seminars", new { SelectedItemID = post.SeminarID });
        }

        public ActionResult editTask(int Id)
        {
            Task model = db.getTasks().Single(t => t.TaskID == Id);
            ViewBag.TaskTypeID = new SelectList(db.getTaskTypes(), "TaskTypeID", "TaskTypeDescription");
            return View("createSeminarTask",model); 
        }

        public ActionResult editCoreTopic(int Id)
        {
            CoreTopic model = db.getCoreTopics().Single(ct => ct.CoreTopicID == Id);
            model.ModifyDate = DateTime.Now; 
            return View("createSeminarCoreTopic", model);
        }

/////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Index(string Command, CohortUsers users = null, int ProgramCohortID = 0)
        {
            if (Command == "AddUsers")
                db.addCohortUsers(users.getIdsToAdd(), users.ProgramCohortID);
            else if (Command == "RemoveUsers")
                db.removeCohortUsers(users.getIdsToRemove(), users.ProgramCohortID);

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
            return RedirectToAction("CohortUsers", new { ProgramCohortID = users.ProgramCohortID });
        }

        public ActionResult RemoveCohortUsers(CohortUsers users)
        {
            db.removeCohortUsers(users.getIdsToRemove(), users.ProgramCohortID);
            return RedirectToAction("CohortUsers", new { ProgramCohortID = users.ProgramCohortID });
        }
    }
}
