using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;
using System.Web.Mvc;
namespace LEDE.Domain.Concrete
{
    public class RatingRepository : IRatingRepository
    {
        private DbContext db;

        public RatingRepository(DbContext context)
        {
            this.db = context; 
        }

        public RatingRepository()
        {
            this.db = new DbContext();
        }


        public void saveTaskRating(CompleteRating taskRating, int VersID)
        {
            if (taskRating.TaskCoreRatings != null)
            {
                IEnumerable<CoreRating> validTaskRatings = taskRating.TaskCoreRatings.
                Where(r => r.Cscore != null || r.Pscore != null || r.Sscore != null || r.RatingID > 0);
                if (validTaskRatings != null)
                {
                    foreach (CoreRating rating in validTaskRatings)
                    {
                        this._saveTaskRating(rating, VersID, taskRating.FacultyID);
                    }
                }
            }
            
            if (taskRating.OtherCoreRatings != null)
            {
                foreach (CoreRating rating in taskRating.OtherCoreRatings)
                {
                    this._saveTaskRating(rating, VersID, taskRating.FacultyID);
                }
            }
        }

        private void _saveTaskRating(CoreRating rating, int VersID, int facultyID)
        {
            if (rating.RatingID > 0)
                updateCoreRating(rating);
            else
                insertCoreRating(rating, VersID, facultyID);
        }

        private void insertCoreRating(CoreRating rating, int VersID, int facultyID)
        {
            TaskRating taskRating = new TaskRating() {FacultyID = facultyID, VersID = VersID, ReviewDate = DateTime.Now}; ///ADD Real Faculty ID with LOGIN
            rating.TaskRating = taskRating; rating.CoreTopicID = rating.CoreTopicID == 0 ? rating.CoreTopic.CoreTopicID : rating.CoreTopicID;  
            rating.CoreTopic = null; 
            
            db.TaskRatings.Add(taskRating);
            db.CoreRatings.Add(rating);
            db.TaskVersions.Find(VersID).RatingStatus = "Complete";
            db.SaveChanges();                     
        }

        private void updateCoreRating(CoreRating rating)
        {
            CoreRating original = db.CoreRatings.Find(rating.RatingID);
            original.Cscore = rating.Cscore;
            original.Sscore = rating.Sscore;
            original.Pscore = rating.Pscore;
            db.SaveChanges(); 
        }

        private IEnumerable<CandidateDrop> getCandidates()
        {
            int roleID = db.Roles.FirstOrDefault(r => r.Name == "Candidate").Id;
            return db.Users.Where(u => u.Roles.FirstOrDefault(r => r.RoleId == roleID) != null).
                Select(u => new CandidateDrop() {Id = u.Id, Name = u.LastName + ", " + u.FirstName });
        }


        public IEnumerable<TaskVersion> getTaskVersions(int userID)
        {
            IEnumerable<TaskVersion> taskVersions = db.TaskVersions.Where(v => v.UserID == userID)
                            .ToList();
            return taskVersions;
        }

        RatingIndexModel IRatingRepository.getIndexModel(int? userID)
        {
            RatingIndexModel model = new RatingIndexModel();
            var candidates = getCandidates();
            model.Candidates = new SelectList(candidates, "Id", "Name", userID);
            if(model.Candidates.FirstOrDefault() != null)
                model.SelectedUserID = userID ?? Convert.ToInt32(model.Candidates.FirstOrDefault().Value);

            return model; 
        }

        public Document findDocument(int documentID)
        {
            return db.Documents.Find(documentID); 
        }


        public void saveFeedback(Document feedbackDoc, int versID)
        {
            TaskVersion updateVersion = db.TaskVersions.Find(versID);
            feedbackDoc.Blob = updateVersion.Document.Blob + "feedback";

            db.Documents.Add(feedbackDoc);
            updateVersion.FeedbackDoc = feedbackDoc; 
            db.SaveChanges(); 
        }


        public void deleteTaskRating(int ratingID)
        {
            CoreRating core = db.CoreRatings.Find(ratingID);
            ImpactTypeRating impact = db.ImpactTypeRatings.Find(ratingID);
            TaskRating task = db.TaskRatings.Find(ratingID);

            if (core != null)
                db.CoreRatings.Remove(core);
            else if (impact != null)
                db.ImpactTypeRatings.Remove(impact);
            if(task!= null)
                db.TaskRatings.Remove(task);

            db.SaveChanges();
        }


        public RatingViewModel getRatingModel(int versID)
        {
            RatingViewModel model = new RatingViewModel() 
            {
                Rating = getTaskRating(versID),
                VersionDrop = getVersionDrop(versID),
                CoreDrop = getCoreDrop(versID),
                TaskVersion = getTaskVersion(versID),
                VersID = versID
            };

            return model;
        }

        private TaskVersion getTaskVersion(int versID)
        {
            TaskVersion taskVersion = db.TaskVersions.Find(versID);

            return taskVersion; 
        }

        private SelectList getVersionDrop(int versid)
        {
            TaskVersion version = db.TaskVersions.Find(versid);
            int UserID = version.UserID;
            int TaskID = version.TaskID;
            return new SelectList(db.TaskVersions.Where(v => v.TaskID == TaskID && v.UserID == UserID), "VersID", "Version", versid);
        }


        private SelectList getCoreDrop(int VersID)
        {
            int programID = db.TaskVersions.Find(VersID).Task.Seminar.ProgramID;
            IEnumerable<CoreTopic> ratedtopics = db.CoreRatings.Where(r => r.TaskRating.VersID == VersID).Select(r => r.CoreTopic);
            int seminarID = db.TaskVersions.Find(VersID).Task.Seminar.SeminarID;
            var coretopics = db.CoreTopics.Where(c => c.Seminar.SeminarID != seminarID && c.Seminar.ProgramID == programID).Except(ratedtopics).
                Select(c => new { Name = c.CoreTopicNum + ": " + c.CoreTopicDesc, c.CoreTopicID });
            return new SelectList(coretopics, "CoreTopicID", "Name");
        }

        private CompleteRating getTaskRating(int versID)
        {
            int seminarID = db.TaskVersions.Find(versID).Task.SeminarID;
            List<CoreRating> taskRatings = new List<CoreRating>();
            List<CoreRating> otherRatings = db.CoreRatings.Include(r => r.TaskRating).Include(r => r.CoreTopic).
                Where(r => r.TaskRating.VersID == versID && r.CoreTopic.SeminarID != seminarID)
                .OrderBy(r=> r.CoreTopic.CoreTopicNum).ToList();
            IEnumerable<CoreRating> existingTaskRatings = db.CoreRatings.Include(r => r.TaskRating).Include(r => r.CoreTopic).
                Where(r => r.TaskRating.VersID == versID && r.CoreTopic.SeminarID == seminarID);

            foreach (CoreTopic topic in db.CoreTopics.Where(c => c.SeminarID == seminarID).OrderBy(c=> c.CoreTopicNum))
            {
                CoreRating existingTopicRating = existingTaskRatings.FirstOrDefault(c => c.CoreTopicID == topic.CoreTopicID);
                if (existingTopicRating == null)
                    taskRatings.Add(new CoreRating { RatingID = -1, CoreTopic = topic });
                else
                    taskRatings.Add(existingTopicRating); 
            }

            CompleteRating taskRating = new CompleteRating()
            {
                TaskCoreRatings = taskRatings,
                OtherCoreRatings = otherRatings,
                ImpactRating = db.ImpactTypeRatings.FirstOrDefault(r => r.TaskRating.VersID == versID)
            };

            return taskRating;
        }


        public void EditReflection(int versID, double numHrs)
        {
            InternReflection reflection = db.InternReflections.Find(versID);
            if (reflection == null)
            {
                reflection = new InternReflection()
                {
                    NumHrs = numHrs,
                    VersID = versID
                };
                db.InternReflections.Add(reflection);
            }
            else
                reflection.NumHrs = numHrs;

            db.SaveChanges();
        }

        public void EditReading(int versID, int numEntries)
        {
            ReadingLogEntry log = db.ReadingLogEntries.Find(versID);
            if (log == null)
            {
                log = new ReadingLogEntry()
                {
                    NumEntries = numEntries,
                    VersID = versID
                };
                db.ReadingLogEntries.Add(log);
            }
            else
                log.NumEntries = numEntries;

            db.SaveChanges(); 
        }


        public void saveImpactRating(CompleteRating impactRating, int VersID)
        {
            if (impactRating.ImpactRating.RatingID > 0)
            {
                ImpactTypeRating impact = db.ImpactTypeRatings.Find(impactRating.ImpactRating.RatingID);
                impact.Sscore = impactRating.ImpactRating.Sscore;
                impact.Pscore = impactRating.ImpactRating.Pscore;
                impact.Lscore = impactRating.ImpactRating.Lscore;
            }
            else
            {
                TaskRating rating = new TaskRating()
                {
                    FacultyID = impactRating.FacultyID == 0 ? 2: impactRating.FacultyID,  //////Add real facID
                    ReviewDate = DateTime.Now,
                    VersID = VersID,
                };

                ImpactTypeRating impact = new ImpactTypeRating()
                {
                    Sscore = impactRating.ImpactRating.Sscore,
                    Pscore = impactRating.ImpactRating.Pscore,
                    Lscore = impactRating.ImpactRating.Lscore,
                    TaskRating = rating
                };
                db.TaskVersions.Find(VersID).RatingStatus = "Complete"; 
                db.TaskRatings.Add(rating);
                db.ImpactTypeRatings.Add(impact);
            }
            db.SaveChanges(); 
        }


        public Document findDocumentByVersID(int VersID)
        {
            return db.TaskVersions.Find(VersID).Document; 
        }
    }
}