using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Concrete
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private DbContext db; 

        public EnrollmentRepository()
        {
            db = new DbContext(); 
        }


        //Methods for the program page
        public IEnumerable<Program> getPrograms()
        {
            return db.Programs;
        }
        //end methods for program page
        public IEnumerable<ProgramCohort> getCohorts()
        {
            return db.ProgramCohorts; 
        }

        private IEnumerable<SelectableUser> getEnrolledUsers(int ProgramCohortID)
        {
            int FacultyRoleID = db.Roles.Single(r => r.Name == "Faculty").Id;
            int CandidateRoleID = db.Roles.Single(r => r.Name == "Candidate").Id; 

            return db.CohortEnrollments.Where(e => e.ProgramCohortID == ProgramCohortID && e.User.Roles.FirstOrDefault(r=> r.RoleId == FacultyRoleID || r.RoleId == CandidateRoleID) != null).
                Select(e => new SelectableUser() {Id = e.UserID, FirstName = e.User.FirstName, LastName = e.User.LastName, 
                    Role = e.User.Roles.FirstOrDefault(r=> r.RoleId == CandidateRoleID) == null ? RoleEnum.Faculty : RoleEnum.Candidate});
        }


        public CohortUsers getCohortUsers(int ProgramCohortID)
        {
            CohortUsers users = new CohortUsers();
            users.ProgramCohortID = ProgramCohortID;

            int FacultyRoleID = db.Roles.Single(r => r.Name == "Faculty").Id;
            int CandidateRoleID = db.Roles.Single(r => r.Name == "Candidate").Id;

            foreach (User user in db.Users.Where(u => u.Roles.FirstOrDefault(r => r.RoleId == CandidateRoleID || r.RoleId == FacultyRoleID) != null).
                OrderBy(u=> u.LastName))
            {
                SelectableUser selectableuser = new SelectableUser()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Selected = false,
                    Role = user.Roles.FirstOrDefault(r => r.RoleId == CandidateRoleID) != null ? RoleEnum.Candidate : RoleEnum.Faculty
                };
                if (user.CohortEnrollments.FirstOrDefault(e => e.ProgramCohortID == ProgramCohortID) != null)
                {
                    if (selectableuser.Role == RoleEnum.Candidate)
                        users.EnrolledCandidates.Add(selectableuser);
                    else
                        users.EnrolledFaculty.Add(selectableuser); 
                }
                else
                {
                    if (selectableuser.Role == RoleEnum.Candidate)
                        users.NonEnrolledCandidates.Add(selectableuser);
                    else
                        users.NonEnrolledFaculty.Add(selectableuser); 
                }
            }
            return users; 
        }



        public void addCohortUsers(List<int> idsToAdd, int programCohortID)
        {
            foreach (int id in idsToAdd)
            {
                db.CohortEnrollments.Add(new CohortEnrollment { ProgramCohortID = programCohortID, UserID = id }); 
            }
            db.SaveChanges();
        }

        public void removeCohortUsers(List<int> idsToRemove, int programCohortID)
        {
            foreach (int id in idsToRemove)
            {
                CohortEnrollment removedEnrollment = db.CohortEnrollments.First(e => e.UserID == id && e.ProgramCohortID == programCohortID);
                db.CohortEnrollments.Remove(removedEnrollment); 
            }
            db.SaveChanges(); 
        }

        public IEnumerable<Seminar> getSeminars()
        {
            return db.Seminars;
        }

        public void editProgram(Program program)
        {
            Program programToEdit = db.Programs.Find(program.ProgramID);
            programToEdit.ProgramTitle = program.ProgramTitle;
            programToEdit.ProgramType = program.ProgramType;
            programToEdit = program; 
            db.SaveChanges(); 
        }

        public void editCohort(ProgramCohort cohort)
        {
            ProgramCohort cohortToEdit = db.ProgramCohorts.Find(cohort.ProgramCohortID);
            cohortToEdit.Status = cohort.Status;
            cohortToEdit.AcademicYear = cohort.AcademicYear;
            db.SaveChanges(); 
        }

        public void createProgramCohort(ProgramCohort cohort)
        {
            db.ProgramCohorts.Add(cohort);
            db.SaveChanges();
        }

        public void deleteProgramCohort(int programCohortID)
        {
            ProgramCohort cohortToRemove = db.ProgramCohorts.Find(programCohortID);
            db.ProgramCohorts.Remove(cohortToRemove);
            db.SaveChanges();
        }

        public void createProgramSeminar(Seminar seminar)
        {
            db.Seminars.Add(seminar);
            db.SaveChanges(); 
        }

        public void deleteProgramSeminar(int seminarID)
        {
            Seminar seminarToRemove = db.Seminars.Find(seminarID);
            db.Seminars.Remove(seminarToRemove);
            db.SaveChanges();
        }


        public void deleteSeminarCoreTopic(int coreTopicID)
        {
            CoreTopic deleteTopic = db.CoreTopics.Find(coreTopicID);
            db.CoreTopics.Remove(deleteTopic);
            db.SaveChanges(); 
        }

        public void deleteSeminarTask(int taskID)
        {
            Task deleteTask = db.Tasks.Find(taskID);
            db.Tasks.Remove(deleteTask);
            db.SaveChanges(); 
        }


        public void editSeminar(Seminar seminar)
        {
            Seminar editSeminar = db.Seminars.Find(seminar.SeminarID);
            editSeminar.SeminarTitle = seminar.SeminarTitle;
            db.SaveChanges(); 
        }


        public IEnumerable<CoreTopic> getCoreTopics()
        {
            return db.CoreTopics; 
        }

        public IEnumerable<Task> getTasks()
        {
            return db.Tasks; 
        }


        public void createSeminarCoreTopic(CoreTopic coretopic)
        {
            db.CoreTopics.Add(coretopic);
            db.SaveChanges(); 
        }

        public void createSeminarTask(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges(); 
        }

        public IEnumerable<TaskType> getTaskTypes()
        {
            return db.TaskTypes; 
        }


        public void editSeminarTask(Task task)
        {
            Task editTask = db.Tasks.Find(task.TaskID);
            editTask.TaskTypeID = task.TaskTypeID;
            editTask.TaskCode = task.TaskCode;
            editTask.TaskName = task.TaskName;

            db.SaveChanges(); 
        }

        public void editSeminarCoretopic(CoreTopic coretopic)
        {
            CoreTopic editTopic = db.CoreTopics.Find(coretopic.CoreTopicID);
            editTopic.ModifyDate = coretopic.ModifyDate;
            editTopic.CoreTopicNum = coretopic.CoreTopicNum;
            editTopic.CoreTopicDesc = coretopic.CoreTopicDesc;
            editTopic.Status = coretopic.Status;

            db.SaveChanges(); 
        }
    }
}