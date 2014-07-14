using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using LEDE_MVC.Models.LEDE;

namespace LEDE_MVC.WebForms
{
    public partial class TaskUpload : System.Web.UI.Page
    {
        private static string accountName = "ledeportal";
        private static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";
        private static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        private static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        private static CloudBlobClient client = account.CreateCloudBlobClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            ObjectDataSource1.SelectParameters["userID"].DefaultValue = User.Identity.GetUserId();

            if (!IsPostBack) //get the taskids which will require special behavior and add them to view state (reading log & reflection)
            {              
                ViewState.Add("ReadingTaskIDs", ledeDB.getReadingTaskIDs());
                ViewState.Add("ReflectionTaskIDs", ledeDB.getReflectionTaskIDs());
                formatForTask(ledeDB.initializeTaskSelectedValue()); //in case default item is log/reflection    
            }

            //make sure a task is selected
            if (taskNameDropDown.SelectedValue != "")
            {
                formatForTask(Convert.ToInt32(taskNameDropDown.SelectedValue));
            }            
        }
      
        private void formatForTask(int taskID)
        {
            List<int> ReadingTaskIDs = (List<int>)ViewState["ReadingTaskIDs"];
            List<int> ReflectionTaskIDs = (List<int>)ViewState["ReflectionTaskIDs"];

            if (ReadingTaskIDs.Contains(taskID)) //student is uploading a reading log                            
                UploadGridView.Columns[1].HeaderText = "Entry";                            
            else if (ReflectionTaskIDs.Contains(taskID)) //student is uploading a reflection                            
                UploadGridView.Columns[1].HeaderText = "Version";                            
            else //student is uploading a regular assignment                            
                UploadGridView.Columns[1].HeaderText = "Version";                            
        }         

        protected void UploadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int versID = (int)UploadGridView.DataKeys[index].Value;

            if(e.CommandName == "score")
            {
                Response.Redirect("~/WebForms/TaskScore.aspx?versid=" + versID);  
            }
            else if (e.CommandName == "submission" || e.CommandName == "feedback")
            {
                Document downloadDoc;

                if (e.CommandName == "submission")
                    downloadDoc = ledeDB.getDocument(versID);
                else 
                    downloadDoc = ledeDB.getFeedbackDocument(versID);                    
                
                CloudBlobContainer sampleContainer = client.GetContainerReference("user" + User.Identity.GetUserId());
                CloudBlockBlob fileBlob = sampleContainer.GetBlockBlobReference(downloadDoc.FilePath); 
                
                Context.Response.Charset = "UTF-8";
                Context.Response.Buffer = false;
                Context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + downloadDoc.FileName + "\"");
                Context.Response.AddHeader("Content-Length", downloadDoc.FileSize); //Set the length the file
                Context.Response.ContentType = "application/msword";
                Context.Response.Flush();

                
                fileBlob.DownloadToStream(Context.Response.OutputStream);
            }            
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            TextBox title = (TextBox)ReadingPanel.FindControl("readingTitle");
            TextBox pages = (TextBox)ReadingPanel.FindControl("numPages");
            TextBox date = (TextBox)ReadingPanel.FindControl("date");
            if (FileUpload1.HasFile == true)
            {
                string userID = User.Identity.GetUserId();
                string taskid = taskNameDropDown.SelectedValue;
                string version = ledeDB.getUploadVersion(taskNameDropDown.SelectedValue, userID);
                string filename = FileUpload1.PostedFile.FileName;
                int filesize = FileUpload1.PostedFile.ContentLength;             
                string filepath = taskid + "/" + version;

                try
                {                    
                    CloudBlobContainer studentContainer = client.GetContainerReference("user" + userID);
                    studentContainer.CreateIfNotExists();

                    CloudBlockBlob blob = studentContainer.GetBlockBlobReference(taskid + "/" + version);
                    using (Stream fileStream = FileUpload1.PostedFile.InputStream)
                    {
                        blob.UploadFromStream(fileStream);
                    }                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    UploadLabel.Text = "Upload Failed - Storage Error";
                    return;
                }
                
                ledeDB.submitAssignment(taskid, userID, version, filename, filepath, filesize);             
                UploadLabel.Text = "Upload Successfull!";
            }
            else
                UploadLabel.Text = "Please choose a file to submit";

        }  
    }
}