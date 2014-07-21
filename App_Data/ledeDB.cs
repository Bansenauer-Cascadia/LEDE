using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using ECSEL.Models;
using ECSEL.Models.FormModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;


/// <summary>
/// Summary description for ledeDB
/// </summary>
/// 

[DataObject(true)]  //this allows conifgura data source wizard to recognize this as a data access method
public class ledeDB
{
    private static ApplicationDbContext db = new ApplicationDbContext();

    public ledeDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //TaskRating Methods

    public static IEnumerable getTaskRatings(string versid)
    {
        int VersIDint;

        if (versid == null)
            return null;
        else
            VersIDint = Convert.ToInt32(versid);

        List<CoreGridView> taskgrid = new List<CoreGridView>();

        var coretopics = db.CoreTopics.Where(c => c.Seminar.Tasks.
            FirstOrDefault(t => t.TaskVersions.FirstOrDefault(v => v.VersID == VersIDint) != null) != null).
            Select(c => new { c.CoreTopicID, CoreTopicDesc = c.CoreTopicNum + ": " + c.CoreTopicDesc, c.CoreRatings });

        foreach (var c in coretopics)
        {
            CoreGridView rating = new CoreGridView();

            rating.CoreTopicDesc = c.CoreTopicDesc;
            rating.CoreTopicID = c.CoreTopicID;

            if ((c.CoreRatings.Count > 0) && (c.CoreRatings.SingleOrDefault(r => r.TaskRating.VersID == VersIDint) != null))
            {
                var sourceRating = c.CoreRatings.SingleOrDefault(r => r.TaskRating.VersID == VersIDint);
                rating.RatingID = sourceRating.RatingID;
                rating.Cscore = sourceRating.Cscore;
                rating.Pscore = sourceRating.Pscore;
                rating.Sscore = sourceRating.Sscore;
            }
            else
            {
                rating.RatingID = -1;
                rating.Cscore = rating.Pscore = rating.Sscore = null;
            }
            taskgrid.Add(rating);
        }
        return taskgrid;
    }

    public static void deleteTaskRating(int original_coretopicid, int original_ratingid)
    {
        if (original_ratingid > 0)
        {
            var coreRating = db.CoreRatings.Find(original_ratingid);
            var taskRating = db.TaskRatings.Find(original_ratingid);

            db.CoreRatings.Remove(coreRating);
            db.TaskRatings.Remove(taskRating);
            db.SaveChanges();
        }
    }

    public static void updateTaskRating(int original_coretopicid, int original_ratingid, string Cscore, string Pscore,
        string Sscore, string FacultyID, string versid)
    {
        if (original_ratingid > 0) //update existing rating
        {
            updateCoreRating(Cscore, Sscore, Pscore, original_ratingid);
        }
        else // insert new rating
        {
            insertCoreRating(FacultyID, versid, Cscore, Pscore, Sscore, original_coretopicid.ToString());
        }
    }

    [DataObjectMethod(DataObjectMethodType.Select)]
    public static IEnumerable getCoreRatings(string versid)
    {
        int VersIDint = Convert.ToInt32(versid);

        if (versid == null)
            return null;
        else
            VersIDint = Convert.ToInt32(versid);

        var ratings = db.CoreRatings.
            Where(r => r.TaskRating.VersID == VersIDint && r.CoreTopic.Seminar.Tasks.
                FirstOrDefault(t => t.TaskVersions.FirstOrDefault(v => v.VersID == VersIDint) != null) == null).
            Select(r => new
            {
                r.RatingID,
                CoreTopicDesc = r.CoreTopic.CoreTopicNum + ": " + r.CoreTopic.CoreTopicDesc,
                r.Cscore,
                r.Pscore,
                r.Sscore
            });
        return ratings;

    }
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public static void deleteCoreRating(int original_RatingID)
    {
        var coreRating = db.CoreRatings.Single(c => c.RatingID == original_RatingID);
        var taskRating = db.TaskRatings.Single(c => c.RatingID == original_RatingID);
        db.CoreRatings.Remove(coreRating);
        db.TaskRatings.Remove(taskRating);
        db.SaveChanges();
    }

    [DataObjectMethod(DataObjectMethodType.Insert)]
    public static void insertCoreRating(string facultyID, string versID, string Cscore, string Pscore, string Sscore, string CoreTopicID)
    {
        var taskRating = new TaskRating
        {
            FacultyID = Convert.ToInt32(facultyID),
            VersID = Convert.ToInt32(versID),
            ReviewDate = DateTime.Now,
        };
        var coreRating = new CoreRating
        {
            TaskRating = taskRating,
            CoreTopicID = Convert.ToInt32(CoreTopicID),
            Cscore = Cscore == null ? null : (int?)Convert.ToInt32(Cscore),
            Sscore = Sscore == null ? null : (int?)Convert.ToInt32(Sscore),
            Pscore = Pscore == null ? null : (int?)Convert.ToInt32(Pscore)
        };

        db.TaskVersions.Find(Convert.ToInt32(versID)).RatingStatus = "Complete";
        db.TaskRatings.Add(taskRating);
        db.CoreRatings.Add(coreRating);
        db.SaveChanges();
    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void updateCoreRating(string Cscore, string Sscore, string Pscore, int original_RatingID)
    {
        var coreRating = db.CoreRatings.Find(original_RatingID);
        coreRating.Cscore = Cscore == null ? null : (int?)Convert.ToInt32(Cscore);
        coreRating.Sscore = Sscore == null ? null : (int?)Convert.ToInt32(Sscore);
        coreRating.Pscore = Pscore == null ? null : (int?)Convert.ToInt32(Pscore);
        db.SaveChanges();
    }

    public static IEnumerable getImpactGrid(string versid)
    {
        int versidInt;
        if (versid == null)
            return null;
        else
            versidInt = Convert.ToInt32(versid);

        var grid = db.ImpactTypeRatings.Where(i => i.TaskRating.VersID == versidInt).
        Select(i => new { i.RatingID, i.Sscore, i.Pscore, i.Lscore });
        return grid;

    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void updateImpactGrid(int RatingID, string Sscore, string Pscore, string Lscore)
    {
        var impactRating = db.ImpactTypeRatings.Find(RatingID);
        impactRating.Lscore = Lscore == null ? null : (int?)Convert.ToInt32(Lscore);
        impactRating.Sscore = Sscore == null ? null : (int?)Convert.ToInt32(Sscore);
        impactRating.Pscore = Pscore == null ? null : (int?)Convert.ToInt32(Pscore);
        db.SaveChanges();
    }

    public static void insertImpactGrid(string Sscore, string Pscore, string Lscore, string facultyID, string versID)
    {

        var taskRating = new TaskRating
        {
            FacultyID = Convert.ToInt32(facultyID),
            VersID = Convert.ToInt32(versID),
            ReviewDate = DateTime.Now,
        };
        var impactRating = new ImpactTypeRating
        {
            TaskRating = taskRating,
            Sscore = Sscore == null ? null : (int?)Convert.ToInt32(Sscore),
            Pscore = Pscore == null ? null : (int?)Convert.ToInt32(Pscore),
            Lscore = Lscore == null ? null : (int?)Convert.ToInt32(Lscore)
        };

        db.TaskVersions.Find(Convert.ToInt32(versID)).RatingStatus = "Complete";
        db.TaskRatings.Add(taskRating);
        db.ImpactTypeRatings.Add(impactRating);
        db.SaveChanges();
    }

    //additional info select/ submission 


    public static ReadingLog getReadingLog(string versid)
    {
        if (versid == null)
            return null;
        var readinglog = db.ReadingLogs.Find(Convert.ToInt32(versid));
        return readinglog;
    }

    public static void updateReadingLog(int numEntries, int versid)
    {
        ReadingLog updateLog = db.ReadingLogs.Find(versid);
        updateLog.NumEntries = numEntries; 

        db.SaveChanges();
    }

    public static void insertReadingLog(int numEntries, string versid)
    {
        ReadingLog submitLog = new ReadingLog
        {
            VersID = Convert.ToInt32(versid),

        };

        db.ReadingLogs.Add(submitLog);
        db.SaveChanges();
    }

    public static InternGrid getReflectionEntry(string versid)
    {
        if (versid == null)        
            return null;        

        int versidint = Convert.ToInt32(versid);
        int userid = db.TaskVersions.Find(versidint).ID;

        var reflections = db.InternReflections.Where(r => r.TaskVersion.ID == userid);
        if (reflections == null)
            return null; 

        double SumHrs = reflections.Sum(r=> r.NumHrs);
        double NumHrs = db.InternReflections.Find(versidint).NumHrs;

        return new InternGrid() { versid = versidint, NumHrs = NumHrs, SumHrs = SumHrs };
    }

    public static void insertReflectionEntry(double NumHrs, string ReflectionDate, string versid)
    {
        InternReflection insertReflection = new InternReflection
        {
            VersID = Convert.ToInt32(versid),

            NumHrs = NumHrs
        };

        db.InternReflections.Add(insertReflection);
        db.SaveChanges();
    }

    public static void updateReflectionEntry(double NumHrs, int versid)
    {
        InternReflection updateReflection = db.InternReflections.Find(versid);
        updateReflection.NumHrs = NumHrs;


        db.SaveChanges();
    }

    public static Task getTask(int versid)
    {
        if (versid == 0)
            return null;
        var task = db.Tasks.Include(t => t.Seminar).FirstOrDefault(t => t.TaskVersions.FirstOrDefault(v => v.VersID == versid) != null);
        if (task == null)
            return null;
        else
            return task;
    }

    public static string getCandidateName(int versid)
    {
        if (versid == 0)
            return null;
        var taskversion = db.TaskVersions.Find(versid);
        if (taskversion == null)
            return null;
        else
            return taskversion.User.LastName + ", " + taskversion.User.FirstName;
    }

    public static IEnumerable<Task> getTasks()
    {
        var sel = db.Tasks.Where(t => t.TaskVersions.FirstOrDefault() != null);
        return sel;
    }

    public static IEnumerable getVersion(string versid)
    {
        if (versid == null)
            return null;
        int versidint = Convert.ToInt32(versid);
        TaskVersion version = db.TaskVersions.Find(versidint);
        if (version != null)
        {
            var versiondropdown = db.TaskVersions.Where(v => v.ID == version.ID && v.TaskID == version.TaskID).OrderByDescending(v => v.Version);
            return versiondropdown;
        }
        else
            return null;


    }
    //Task Score Methods

    public static IEnumerable getCoreScores(int versid, int userid)
    {      
        var scores = db.CoreRatings.Where(r => r.TaskRating.VersID == versid && r.TaskRating.TaskVersion.ID == userid).
            Select(r => new
        {
            r.RatingID,
            r.CoreTopic.CoreTopicDesc,
            r.Cscore,
            r.Pscore,
            r.Sscore
        });

        return scores;
    }

    public static IEnumerable getImpactScores(int versid, int userid)
    {
        var scores = db.ImpactTypeRatings.Where(i => i.TaskRating.VersID == versid && i.TaskRating.TaskVersion.ID == userid).
            Select(i => new
            {
                i.RatingID,
                i.Sscore,
                i.Pscore,
                i.Lscore
            });
        return scores;
    }

    public static string getScoreLabel(string versid)
    {
        if (versid == null)
            return "No Scores For This Task";

        int versidint = Convert.ToInt32(versid);
        var taskversion = db.TaskVersions.Include(t => t.Task).FirstOrDefault(t => t.VersID == versidint);
        return taskversion.Task.TaskName + " Version " + taskversion.Version;
    }

    //misc
    public static string getSeminarLabel(int taskid)
    {
        string title = db.Tasks.Find(taskid).Seminar.SeminarTitle;
        return "Core Ratings for " + title + " Seminar";
    }
    public static string getInitialSeminarLabel()
    {
        return getSeminarLabel(initializeTaskSelectedValue());
    }

    public static string getSeminarName(string versid)
    {
        int versidint = Convert.ToInt32(versid);
        var seminar = db.TaskVersions.Find(versidint).Task.Seminar;
        return seminar.SeminarTitle; 

    }


    ///////////////////////////////////////////////////////

    //Upload Data Methods
    [DataObjectMethod(DataObjectMethodType.Select)]
    public static IEnumerable getUploadGrid(string userID, string taskID)
    {
        int TaskIDint = Convert.ToInt32(taskID);
        int UserIDint = Convert.ToInt32(userID);

        var uploads = db.TaskVersions.Include(t=> t.TaskRatings).Where(v => v.TaskID == TaskIDint && v.ID == UserIDint).
            Select(v => new
            {
                v.VersID,
                v.Task.TaskName,
                v.Version,
                v.Document.FileName,
                v.Document.UploadDate,
                feedbackfilename = v.FeedbackDocument.FileName,
                feedbackuploaddate = (DateTime?)v.FeedbackDocument.UploadDate,
                RatingSubmitted = (v.TaskRatings.FirstOrDefault() != null)
            }).OrderByDescending(v=> v.Version);

        return uploads;
    }

    public static void deleteTaskVersion(int versid)
    {
        TaskVersion deleteVersion = db.TaskVersions.Find(versid);
        ReadingLog deleteLog = deleteVersion.ReadingLogEntry;
        InternReflection deleteReflection = deleteVersion.InternReflection;
        if (deleteLog != null)
            db.ReadingLogs.Remove(deleteLog);
        else if (deleteReflection != null)
            db.InternReflections.Remove(deleteReflection);
        db.TaskVersions.Remove(deleteVersion);

        db.SaveChanges(); 
        
    }

    public static string getTaskLabel(int taskid)
    {
        Task task = db.Tasks.Find(taskid);
        return task.TaskCode + ": " + task.TaskName;  
    }

    public static IEnumerable getUploadTasks()
    {
        return db.Tasks; 
    }

    public static IEnumerable getCoreTopics(string versid)
    {
        if (versid == null)
            return null;
        else
        {
            int versidInt = Convert.ToInt32(versid);

            var topics = db.CoreTopics.Where(c => c.Seminar.Tasks.FirstOrDefault(t => t.TaskVersions.
                FirstOrDefault(v => v.VersID == versidInt) != null) == null &&
                c.CoreRatings.FirstOrDefault(r => r.TaskRating.VersID == versidInt) == null).
                Select(t => new {t.CoreTopicID, CoreTopicDesc = t.CoreTopicNum + ": " + t.CoreTopicDesc});
            return topics;
        }
    }

    public static string getUploadVersion(string taskid, string userid)
    {
        int useridint = Convert.ToInt32(userid);
        int taskidint = Convert.ToInt32(taskid);
        var currentTaskVersions = db.TaskVersions.Where(v => v.TaskID == taskidint && v.ID == useridint);
        if (currentTaskVersions.FirstOrDefault() != null)
            return (currentTaskVersions.Max(v => v.Version) + 1).ToString();
        else
            return "1";
    }

    public static void submitAssignment(string taskid, string userid, string version,
        string filename, string filepath, int filesize, int numEntires, int numHours)
    {
        DateTime UniversalTime = DateTime.UtcNow;
        DateTime date = UniversalTime.AddHours(-7);
        Document uploadDoc = new Document
        {
            FileName = filename,
            FilePath = filepath,
            FileSize = filesize.ToString(),
            UploadDate = date
        };

        TaskVersion uploadTask = new TaskVersion
        {
            Document = uploadDoc,
            TaskID = Convert.ToInt32(taskid),
            ID = Convert.ToInt32(userid),
            Version = Convert.ToInt32(version),
            RatingStatus = "Pending"
        };       

        db.Documents.Add(uploadDoc);
        db.TaskVersions.Add(uploadTask);

        if (numEntires > 0) //enter a Reading Log
        {
            ReadingLog log = new ReadingLog { NumEntries = numEntires, TaskVersion = uploadTask };
            db.ReadingLogs.Add(log); 
        }

        if (numHours > 0)
        {
            InternReflection reflection = new InternReflection { NumHrs = numHours, TaskVersion = uploadTask };
            db.InternReflections.Add(reflection); 
        }

        db.SaveChanges();
    }

    public static Document getDocument(int VersID)
    {
        Document document = db.TaskVersions.Find(VersID).Document;
        return document;
    }

    public static Document getFeedbackDocument(int VersID)
    {
        Document feedback = db.TaskVersions.Find(VersID).FeedbackDocument;
        return feedback;
    }

    //Tasks 

    public static IEnumerable getUsers()
    {
        Role candidateRole = db.Roles.Find(1);
        var users = db.Users.Where(u => u.Roles.FirstOrDefault(r => r.RoleId == 1) != null).Select(u => new { u.Id, name = u.LastName + ", " + u.FirstName }).OrderBy(u => u.name);
        return users;
    }

    public static IEnumerable getAssignments(int userid, string sortOrder)
    {
        var assignments = db.TaskVersions.Where(v => v.ID == userid).Select(v => new
        {
            v.VersID,
            Assignment = v.Task.TaskCode + ": " + v.Task.TaskName,
            v.Version,
            v.Document.FileName,
            v.Document.UploadDate,
            FeedbackFileName = v.FeedbackDocument.FileName,
            FeedbackUploadDate = (DateTime?)v.FeedbackDocument.UploadDate,
            v.RatingStatus,
            RatingLink = (v.RatingStatus == "Complete") ? "View Rating" : "Rate Now"
        });

        switch (sortOrder)
        {
            case "uploaddate":
                return assignments.OrderBy(a => a.UploadDate);
            case "uploaddate DESC":
                return assignments.OrderByDescending(a => a.UploadDate);
            case "assignment, version":
                return assignments.OrderBy(a => a.Assignment).ThenByDescending(a => a.Version);
            case "assignment, version DESC":
                return assignments.OrderByDescending(a => a.Assignment).ThenByDescending(a => a.Version);
            case "ratingstatus":
                return assignments.OrderBy(a => a.RatingStatus).ThenBy(a => a.Assignment).ThenBy(a => a.Version);
            case "ratingstatus DESC":
                return assignments.OrderByDescending(a => a.RatingStatus).ThenBy(a => a.Assignment).ThenByDescending(a => a.Version);
            default:
                return assignments.OrderByDescending(a => a.RatingStatus).ThenBy(a => a.Assignment).ThenByDescending(a => a.Version);
        }
    }

    public static void submitFeedback(Document feedbackDoc, int versid)
    {
        db.Documents.Add(feedbackDoc);
        TaskVersion tv = db.TaskVersions.Find(versid);
        tv.FeedbackDocument = feedbackDoc;
        db.SaveChanges();
    }

    //Conditional Formatting For Reading Log and Impact Entries 
    private static List<int> getTaskIDs(int taskType)
    {
        List<int> readingIDs = new List<int>();
        foreach (var task in db.Tasks.Where(t => t.TaskTypeID == taskType))
        {
            readingIDs.Add(task.TaskID);
        }

        return readingIDs;
    }

    public static List<int> getReadingTaskIDs()
    {
        return getTaskIDs(2);
    }

    public static List<int> getReflectionTaskIDs()
    {
        return getTaskIDs(3);
    }

    public static int initializeTaskSelectedValue()
    {
        var value = getTasks();
        string foo = value.FirstOrDefault().TaskID.ToString();
        return value.FirstOrDefault().TaskID;
    }
    //Account methods
    public static IEnumerable getRoles()
    {
        return db.Roles;
    }
}