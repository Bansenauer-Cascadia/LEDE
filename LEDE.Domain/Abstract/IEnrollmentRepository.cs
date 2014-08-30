using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDE.Domain.Entities;

namespace LEDE.Domain.Abstract
{
    public interface IEnrollmentRepository
    {
        IEnumerable<ProgramCohort> getCohorts();

        IEnumerable<Program> getPrograms();

        IEnumerable<CoreTopic> getCoreTopics();

        IEnumerable<TaskType> getTaskTypes(); 

        IEnumerable<LEDE.Domain.Entities.Task> getTasks();

        void editProgram(Program program);

        void editSeminar(Seminar seminar);

        void editSeminarTask(LEDE.Domain.Entities.Task task);

        void editSeminarCoretopic(CoreTopic coretopic); 

        IEnumerable<Seminar> getSeminars();        

        CohortUsers getCohortUsers(int ProgramCohortID);

        void addCohortUsers(List<int> idsToAdd, int programCohortID);

        void removeCohortUsers(List<int> idsToRemove, int programCohortID);

        void createProgramCohort(ProgramCohort cohort);

        void deleteProgramCohort(int programCohortID);

        void createProgramSeminar(Seminar seminar);

        void createSeminarCoreTopic(CoreTopic coretopic);

        void createSeminarTask(LEDE.Domain.Entities.Task task); 

        void deleteProgramSeminar(int seminarID);

        void deleteSeminarCoreTopic(int coreTopicID);

        void deleteSeminarTask(int taskID);
    }
}

/*
GenericEnrollment<SelectableItem> getProgramCohorts(int ProgramID);

        GenericEnrollment<SelectableItem> getProgramSeminars(int ProgramID);
void addEnrollmentItems(List<int> idsToAdd, int Id, EnrollmentType type);

void removeEnrollmentItems(List<int> idsToRemove, int Id, EnrollmentType type);
 public void addEnrollmentItems(List<int> idsToAdd, int Id, EnrollmentType type)
        {
            if (type == EnrollmentType.ProgramCohort)
            {
                foreach (int id in idsToAdd)
                {
                    db.ProgramCohorts.SingleOrDefault(c => c.ProgramCohortID == id).ProgramID = Id;
                }
            }
            else if (type == EnrollmentType.Seminar)
            {
                foreach (int id in idsToAdd)
                {
                    db.Seminars.SingleOrDefault(s => s.SeminarID == id).ProgramID = Id;
                }
            }
            db.SaveChanges();
        }

        public void removeEnrollmentItems(List<int> idsToRemove, int Id, EnrollmentType type)
        {
            if (type == EnrollmentType.ProgramCohort)
            {
                foreach (int id in idsToRemove)
                {
                    ProgramCohort cohortToRemove = db.ProgramCohorts.Single(c => c.ProgramCohortID == id);
                    db.ProgramCohorts.Remove(cohortToRemove); 
                }
            }
            else if (type == EnrollmentType.Seminar)
            {
                foreach (int id in idsToRemove)
                {
                    Seminar seminarToRemove = db.Seminars.Single(c => c.SeminarID == id);
                    db.Seminars.Remove(seminarToRemove); 
                }
            }
            db.SaveChanges();
        }
 *        public GenericEnrollment<SelectableItem> getProgramCohorts(int ProgramID)
        {
            GenericEnrollment<SelectableItem> model = new GenericEnrollment<SelectableItem>() {Id = ProgramID, Type = EnrollmentType.ProgramCohort};
            model.enrolledItems = db.ProgramCohorts.Where(c => c.ProgramID == ProgramID).
                Select(c => new SelectableItem() {Id = c.ProgramCohortID, 
                    Name = c.AcademicYear + "-" + c.Status}).ToList();
            model.nonEnrolledItems = db.ProgramCohorts.Where(c => c.ProgramID != ProgramID).
                Select(c => new SelectableItem() { Id = c.ProgramCohortID, 
                    Name = c.AcademicYear + "-" + c.Status}).ToList();

            return model; 
        }
 * public GenericEnrollment<SelectableItem> getProgramSeminars(int ProgramID)
        {
            GenericEnrollment<SelectableItem> model = new GenericEnrollment<SelectableItem>()
            {
                Id = ProgramID,
                Type = EnrollmentType.Seminar,
                enrolledItems = db.Seminars.Where(s => s.ProgramID == ProgramID).
                Select(s => new SelectableItem() { Id = s.SeminarID, Name = s.SeminarTitle }).ToList(),
                nonEnrolledItems = db.Seminars.Where(s => s.ProgramID != ProgramID).
                Select(s => new SelectableItem() { Id = s.SeminarID, Name = s.SeminarTitle }).ToList()
            };
            return model;
        }

 * */
