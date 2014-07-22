using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNet.Identity;
using ECSEL.Models;
using System.IO; 

namespace ECSEL.Candidate
{
    public partial class Tasks : System.Web.UI.Page
    {
        private static string accountName = "ledeportal";
        private static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";
        private static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        private static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        private static CloudBlobClient client = account.CreateCloudBlobClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            ObjectDataSource1.SelectParameters["userID"].DefaultValue = User.Identity.GetUserId(); //"16";

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
            //reset Upload Panel
            UploadLabel.Text = "";
            UploadPanel.Visible = true;
            
        }

        private void formatForTask(int taskID)
        {
            List<int> ReadingTaskIDs = (List<int>)ViewState["ReadingTaskIDs"];
            List<int> ReflectionTaskIDs = (List<int>)ViewState["ReflectionTaskIDs"];

            TaskLabel.Text = ledeDB.getTaskLabel(taskID); 

            if (ReadingTaskIDs.Contains(taskID)) //task is a reading log
            {
                UploadGridView.Columns[1].HeaderText = "Entry";
               
                ReflectionSubmitButton.Visible = false;
                SubmitButton.Visible = true;

                ReadingEntriesTextBox.Visible = true;
                ReadingEntriesLabel.Visible = true; 
                ReflectionHoursTextBox.Visible = false;
                ReflectionHoursLabel.Visible = false;

                ViewState.Add("TaskType", "ReadingLog"); 
            }
            else if (ReflectionTaskIDs.Contains(taskID)) // task is a reflection
            {
                UploadGridView.Columns[1].HeaderText = "Version";
               
                ReflectionSubmitButton.Visible = true;
                SubmitButton.Visible = false;

                ReadingEntriesTextBox.Visible = false;
                ReadingEntriesLabel.Visible = false; 
                ReflectionHoursTextBox.Visible = true;
                ReflectionHoursLabel.Visible = true;

                ViewState.Add("TaskType", "Reflection"); 
            }
            else //task is regular assignment 
            {                
                UploadGridView.Columns[1].HeaderText = "Version";
                ReflectionSubmitButton.Visible = false;
                SubmitButton.Visible = true;

                ReadingEntriesTextBox.Visible = false;
                ReadingEntriesLabel.Visible = false; 
                ReflectionHoursTextBox.Visible = false;
                ReflectionHoursLabel.Visible = false;

                ViewState.Add("TaskType", "Normal"); 
            
            }
        }

        protected void UploadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int versID = (int)UploadGridView.DataKeys[index].Value;

            if (e.CommandName == "score")
            {
                Response.Redirect("~/Candidate/TaskScore.aspx?versid=" + versID);
                return; 
            }
            else if (e.CommandName == "delete")
            {
                return; 
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

        protected bool isDeleteVisible(bool RatingSubmitted)
        {
            return !RatingSubmitted; 
        }

        protected bool isScoreVisible(bool RatingSubmitted)
        {
            return RatingSubmitted; 
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true)
            {
                string userID = User.Identity.GetUserId();
                string taskid = taskNameDropDown.SelectedValue;
                string version = ledeDB.getUploadVersion(taskNameDropDown.SelectedValue, userID);
                string filename = FileUpload1.PostedFile.FileName;
                int filesize = FileUpload1.PostedFile.ContentLength;
                string filepath = taskid + "/" + version;
                int numHours; 
                numHours = Int32.TryParse(ReflectionHoursTextBox.Text, out numHours) == true ? numHours: -1;
                int numEntries;
                numEntries = Int32.TryParse(ReadingEntriesTextBox.Text, out numEntries) == true ? numEntries : -1; 

                //make sure the user has entered appropriate fields if we are dealing with a log or reflection 
                if (ViewState["TaskType"] == "Reflection" && numHours == -1)
                {
                    UploadLabel.Text = "Please enter an integer number of hours";
                    return;
                }
                else if (ViewState["TaskType"] == "ReadingLog" && numEntries == -1) 
                {
                    UploadLabel.Text = "Please enter an integer number of entries";
                    return; 
                }

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

                ledeDB.submitAssignment(taskid, userID, version, filename, filepath, filesize, numEntries, numHours);
                UploadLabel.Text = "Upload Successful!";
                UploadPanel.Visible = false;
                UploadGridView.DataBind(); // refresh the gridview to show the uploaded file
            }
            else
                UploadLabel.Text = "Please choose a file to submit";

        }      

        protected void UploadToggleButton_Click(object sender, EventArgs e)
        {
            UploadPanel.Visible = true;
            UploadToggleButton.Visible = false;  
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            UploadPanel.Visible = false;
            UploadToggleButton.Visible = true;
        }        
    }
}