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
using LEDE_MVC.Models.LEDE;

namespace LEDE_MVC.WebForms
{
    public partial class TaskDownload : System.Web.UI.Page
    {
        protected static string accountName = "ledeportal";
        protected static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";
        protected static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        protected static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        protected static CloudBlobClient client = account.CreateCloudBlobClient();
        protected static int versID;


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void AssignmentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            TableRow row = AssignmentGrid.Rows[index];
            int versid = (int)AssignmentGrid.DataKeys[index].Value;
            
            CloudBlobContainer sampleContainer = client.GetContainerReference("user" + StudentDropDown.SelectedValue);
            
            if (e.CommandName == "Download")
            {
                Document downloadDoc = ledeDB.getDocument(versid);
                CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(downloadDoc.FilePath);

                CloudBlockBlob fileBlob = sampleContainer.GetBlockBlobReference(downloadDoc.FilePath);

                Context.Response.Charset = "UTF-8";
                Context.Response.Buffer = false;
                Context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + downloadDoc.FileName + "\"");
                Context.Response.AddHeader("Content-Length", downloadDoc.FileSize); //Set the length the file
                Context.Response.ContentType = "application/msword";
                Context.Response.Flush();

                fileBlob.DownloadToStream(Context.Response.OutputStream);
            }

            else if (e.CommandName == "Upload")
            {                
                showUploadControls();
                versID = versid;
            }

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile == true)
            {
                Document feedbackUpload = new Document
                {
                    FileName = FileUpload1.PostedFile.FileName,
                    FilePath = versID + "/Feedback",
                    FileSize = FileUpload1.PostedFile.ContentLength.ToString(),
                    UploadDate = DateTime.Now
                };                
                try
                {
                    string userID = StudentDropDown.SelectedValue;

                    CloudBlobContainer studentContainer = client.GetContainerReference("user" + userID);
                    studentContainer.CreateIfNotExists();

                    CloudBlockBlob blob = studentContainer.GetBlockBlobReference(feedbackUpload.FilePath);
                    using (Stream fileStream = FileUpload1.PostedFile.InputStream)
                    {
                        blob.UploadFromStream(fileStream);
                    }

                    UploadStatusLabel.Text = "Upload Successfull!";
                    hideUploadControls();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    UploadStatusLabel.Text = "Upload Failed";
                    return; 
                }
                ledeDB.submitFeedback(feedbackUpload, versID);
            }
        }
        protected void EvaluationButton_Click(object sender, EventArgs e)
        {
            hideUploadControls();            
        }

        private void showUploadControls()
        {
            FileUpload1.Visible = true;
            SubmitButton.Visible = true;
            CancelButton.Visible = true;
            UploadLabel.Text = "";
        }

        private void hideUploadControls()
        {

            FileUpload1.Visible = false;
            SubmitButton.Visible = false;
            CancelButton.Visible = false;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            hideUploadControls();
        }
    }
}