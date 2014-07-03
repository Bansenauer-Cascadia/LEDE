using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;

/// <summary>
/// Summary description for ledeDB
/// </summary>
/// 

[DataObject(true)]  //this allows conifgura data source wizard to recognize this as a data access method
public class ledeDB
{
    private static string userName = "LEDE_UWB@aywzhbww7u";
    private static string password = "L3D3worker";
    private static string dataSource = "aywzhbww7u.database.windows.net";
    private static string DatabaseName = "testportal";

    public ledeDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }          

    private static SqlDataReader selectStatement(string sel){
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(sel, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }
    
    [DataObjectMethod(DataObjectMethodType.Select)]
    public static IEnumerable getCoreRatings(string userid, string versid, string taskid)
    {
        if (userid == null || versid == null || taskid == null)
        {
            return null; 
        }        
            string sel = @"coregrid @userid, @versid, @taskid";
            SqlConnection con = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(sel, con);
            cmd.Parameters.AddWithValue("userid", userid);
            cmd.Parameters.AddWithValue("versid", versid);
            cmd.Parameters.AddWithValue("taskid", taskid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        
    }
    [DataObjectMethod(DataObjectMethodType.Delete)]
    public static void deleteCoreRating(string KnowledgeC, string KnowledgeP, string KnowledgeR, string CoreTopicID)
    {
        int foo = 0; 
    }

    [DataObjectMethod(DataObjectMethodType.Insert)]    
    public static void insertCoreRating(string KnowledgeC, string KnowledgeP, string KnowledgeR, string CoreTopicID)
    {

        int bar = 2;
    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void updateCoreRating(string KnowledgeC, string KnowledgeP, string KnowledgeR, int original_coretopicid, string CoreTopicName)
    {
        int foobar = 0; 
    }

    [DataObjectMethod(DataObjectMethodType.Select)]
    public static IEnumerable getUploadGrid(string userID, string taskID)
    {
        string sel = @"uploadgrid @userid, @taskID";
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(sel, con);
        cmd.Parameters.AddWithValue("userid", userID);
        cmd.Parameters.AddWithValue("taskID", taskID); 
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    public static IEnumerable getUsers()
    {       
        string sel = "select dbuserid, (lastname + ', ' + firstname) as name from dbusers";        
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(sel, con);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        return dr; 
    }

    public static IEnumerable getTasks()
    {
        string sel = "select taskid, taskname from task";
        return selectStatement(sel); 
    }

    public static IEnumerable getVersion(string taskID, string userID)
    {

        string sel = "select version from taskversion where taskid = @taskID and userid = @userid";
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(sel, con);
        cmd.Parameters.AddWithValue("taskID", taskID);
        cmd.Parameters.AddWithValue("userID", userID); 
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        return dr; 
    }
    

    public static IEnumerable getImpactGrid(string userid, string version, string taskid)
    {
        if (userid == null || version == null || taskid == null)
        {
            return null;
        }
        else
        {
            string sel = 
@"impactgrid @userid, @version, @taskid";
            SqlConnection con = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(sel, con);
            cmd.Parameters.AddWithValue("userid", userid);
            cmd.Parameters.AddWithValue("version", version);
            cmd.Parameters.AddWithValue("taskid", taskid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void updateImpactGrid()
    {
        int foo = 0; 
    }

    public static IEnumerable getCoreTopics(string userid, string versid, string taskid)
    {
        if (userid == null || versid == null || taskid == null)
        {
            return null;
        }
        else
        {
            string sel = @"select distinct(coretopic.coretopicid) as CoreTopicID, (cast(coretopic.coretopicnum as varchar) + ': ' + coretopic.coretopicdesc) as Name from coretopic
where coretopic.coretopicid not in (select corerating.coretopicid from corerating
join taskrating on taskrating.ratingid = corerating.ratingid
join taskversion on taskversion.versid = taskrating.versid
where taskversion.userid = @userid and taskversion.version = @versid and taskversion.taskid = @taskid)";
            SqlConnection con = new SqlConnection(getConnectionString());
            SqlCommand cmd = new SqlCommand(sel, con);
            cmd.Parameters.AddWithValue("userid", userid);
            cmd.Parameters.AddWithValue("versid", versid);
            cmd.Parameters.AddWithValue("taskid", taskid);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        
    }

    public static IEnumerable getAssignments(int userid)
    {
        string sel =
@"select taskversion.versid, task.taskname as assignment, taskversion.version, document.uploaddate as 'Date Modified' from taskversion
join task on taskversion.taskid = task.taskid
join dbusers on taskversion.userid = dbusers.dbuserid 
join document on document.documentid = taskversion.documentid
where taskversion.ratingstatus = 'pending' and dbusers.dbuserid=@userid";
        SqlConnection con = new SqlConnection(getConnectionString());
        SqlCommand cmd = new SqlCommand(sel, con);
        cmd.Parameters.AddWithValue("userid", userid);
        con.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        return dr;
    }

    //^
   
    public static string getConnectionString()
    {
        // Create a connection string for the  database
        SqlConnectionStringBuilder connStringBuilder;
        connStringBuilder = new SqlConnectionStringBuilder();

        //4 required connstringbuilder fields
        connStringBuilder.DataSource = dataSource;
        connStringBuilder.InitialCatalog = DatabaseName;
        connStringBuilder.UserID = userName;
        connStringBuilder.Password = password;

        //couple additional security methods 
        connStringBuilder.Encrypt = true;
        connStringBuilder.TrustServerCertificate = false;
        return connStringBuilder.ToString();
    }
}