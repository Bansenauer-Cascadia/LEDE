using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using LEDE_MVC.Models;
using LEDE_MVC.Models.LEDE;
using LEDE_MVC.Models.FormModels;
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
    public static IEnumerable getTaskRatings(string versid, string taskid)
    {
        int VersIDint = Convert.ToInt32(versid);
        int TaskIDint = Convert.ToInt32(taskid);
        List<CoreGridView> taskgrid = new List<CoreGridView>();
        var coretopics = db.CoreTopics.Where(c => c.Seminar.Tasks.FirstOrDefault(t => t.TaskID == TaskIDint) != null).
            Select(c => new { c.CoreTopicID, c.CoreTopicDesc, c.CoreRatings });
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
            insertCoreRating(FacultyID, versid, Cscore, Pscore,Sscore, original_coretopicid.ToString());            
        }
    }

    [DataObjectMethod(DataObjectMethodType.Select)]
    public static IEnumerable getCoreRatings(string versid, string taskid)
    {
        int versidInt = Convert.ToInt32(versid);
        int taskidInt = Convert.ToInt32(taskid);
        if (versid == null)
        {
            return null;
        }
        var ratings = db.CoreRatings.
            Where(r => r.TaskRating.VersID == versidInt && r.CoreTopic.Seminar.Tasks.FirstOrDefault(t=> t.TaskID == taskidInt) == null).
            Select(r => new
            {
                r.RatingID,
                r.CoreTopic.CoreTopicDesc,
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
            Cscore = Convert.ToInt32(Cscore),
            Pscore = Convert.ToInt32(Pscore),
            Sscore = Convert.ToInt32(Sscore)
        };

        db.TaskRatings.Add(taskRating);
        db.CoreRatings.Add(coreRating);
        db.SaveChanges();
    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void updateCoreRating(string Cscore, string Sscore, string Pscore, int original_RatingID)
    {
        var coreRating = db.CoreRatings.Find(original_RatingID);
        coreRating.Cscore = Convert.ToInt32(Cscore);
        coreRating.Sscore = Convert.ToInt32(Sscore);
        coreRating.Pscore = Convert.ToInt32(Pscore);
        db.SaveChanges();
    }


    public static IEnumerable getImpactGrid(string versid)
    {
        int versidInt = Convert.ToInt32(versid);
        if (versid == null)
            return null;

        var grid = db.ImpactTypeRatings.Where(i => i.TaskRating.VersID == versidInt).
            Select(i => new { i.RatingID, i.Sscore, i.Pscore, i.Lscore });
        return grid;

    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void updateImpactGrid(int original_RatingID, string Sscore, string Pscore, string Lscore)
    {
        var impactRating = db.ImpactTypeRatings.Find(original_RatingID);
        impactRating.Sscore = Convert.ToInt32(Sscore);
        impactRating.Pscore = Convert.ToInt32(Pscore);
        impactRating.Lscore = Convert.ToInt32(Lscore);
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
            Sscore = Convert.ToInt32(Sscore),
            Pscore = Convert.ToInt32(Pscore),
            Lscore = Convert.ToInt32(Lscore)
        };

        db.TaskRatings.Add(taskRating);
        db.ImpactTypeRatings.Add(impactRating);
        db.SaveChanges();
    }

    public static IEnumerable getUsers(string taskID)
    {
        int IntTaskID = Convert.ToInt32(taskID);
        var users = db.Users.Where(u=> u.TaskVersions.FirstOrDefault(v=> v.TaskID == IntTaskID) != null);
        return users;
    }

    public static IEnumerable getTasks()
    {
        var sel = db.Tasks.Select(t => new { t.TaskID, t.TaskName });        
        return sel;
    }

    public static IEnumerable getVersion(string taskID, string userID)
    {
        int taskidInt = Convert.ToInt32(taskID);
        int useridInt = Convert.ToInt32(userID);
        var version = db.TaskVersions.Where(v => v.TaskID == taskidInt && v.ID == useridInt).
            Select(v => new { v.VersID, v.Version }).OrderByDescending(v=> v.Version);
        return version;
        
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
        var taskversion = db.TaskVersions.Include(t=> t.Task).FirstOrDefault(t=> t.VersID == versidint);
        return taskversion.Task.TaskName + " Version " + taskversion.Version;
    }

    //Upload Data Methods
    [DataObjectMethod(DataObjectMethodType.Select)]
    public static IEnumerable getUploadGrid(string userID, string taskID)
    {
        int TaskIDint = Convert.ToInt32(taskID);
        int UserIDint = Convert.ToInt32(userID);

        var uploads = db.TaskVersions.Where(v => v.TaskID == TaskIDint && v.ID == UserIDint).
            Select(v => new { v.VersID, v.Task.TaskName, v.Version, v.Document.FileName,v.Document.UploadDate,
            feedbackfilename = v.FeedbackDocument.FileName, feedbackuploaddate = (DateTime?)v.FeedbackDocument.UploadDate});

        return uploads; 
    }

    public static IEnumerable getCoreTopics(string taskid, string versid)
    {                    
        if (taskid == null || versid == null)
        {
            return null;
        }
        else
        {
            int taskidInt = Convert.ToInt32(taskid); 
            int versidInt = Convert.ToInt32(versid);

            var topics = db.CoreTopics.Where(c=> c.Seminar.Tasks.FirstOrDefault(t=> t.TaskID == taskidInt) == null &&
                c.CoreRatings.FirstOrDefault(r=> r.TaskRating.VersID == versidInt) == null);
            return topics;
        }
    }

    public static string getUploadVersion(string taskid, string userid)
    {
        int useridint = Convert.ToInt32(userid);
        int taskidint = Convert.ToInt32(taskid); 
        var currentTaskVersions = db.TaskVersions.Where(v=> v.TaskID == taskidint && v.ID == useridint);
        if (currentTaskVersions.FirstOrDefault() != null)
            return  (currentTaskVersions.Max(v=> v.Version) + 1).ToString(); 
        else
            return "1"; 
    }

    public static void submitAssignment(string taskid, string userid, string version,
        string filename, string filepath, int filesize)
    {
        Document uploadDoc = new Document
        {
            FileName = filename,
            FilePath = filepath,
            FileSize = filesize.ToString(),
            UploadDate = DateTime.Now
        };

        TaskVersion uploadTask = new TaskVersion
        {
            Document = uploadDoc,
            TaskID = Convert.ToInt32(taskid),
            ID = Convert.ToInt32(userid),
            Version = Convert.ToInt32(version),
            RatingStatus = "pend"
        };

        db.Documents.Add(uploadDoc);
        db.TaskVersions.Add(uploadTask);
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

    //TaskDownload 

    public static IEnumerable getUsers()
    {
        var users = db.Users.Select(u => new { u.Id, name = u.LastName + ", " + u.FirstName}).OrderBy(u=> u.name);
        return users;
    }

    public static IEnumerable getAssignments(int userid)
    {
        var assignments = db.TaskVersions.Where(v => v.ID == userid).Select(v => new
        {
            v.VersID, Assignment = v.Task.TaskName, v.Version,
            v.Document.UploadDate, v.RatingStatus
        }).OrderBy(v=> v.RatingStatus);

        return assignments;
    }

    public static void submitFeedback(Document feedbackDoc, int versid)
    {
        db.Documents.Add(feedbackDoc);
        TaskVersion tv = db.TaskVersions.Find(versid);
        tv.FeedbackDocument = feedbackDoc;
        db.SaveChanges(); 
    }
   
}