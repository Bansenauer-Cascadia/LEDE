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

public partial class _Default : System.Web.UI.Page
{
    protected static string accountName = "ledeportal";
    protected static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";   

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void UploadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = UploadGridView.Rows[index]; 

        if (e.CommandName == "submission")
        {
            int foo = 0; 
        }
        else if (e.CommandName == "feedback")
        {
            int foo = 0;
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {        
        if (FileUpload1.HasFile == true)
        {
            string containerString = "1";
            string taskid = taskNameDropDown.SelectedValue;
            string versionid = "1"; 

            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
                CloudBlobClient client = account.CreateCloudBlobClient();

                CloudBlobContainer studentContainer = client.GetContainerReference("student"+containerString);
                studentContainer.CreateIfNotExists();

                CloudBlockBlob blob = studentContainer.GetBlockBlobReference(taskid +"/" + versionid);
                using (Stream fileStream = FileUpload1.PostedFile.InputStream)
                {
                    blob.UploadFromStream(fileStream);
                }
              
                UploadLabel.Text = "Upload Successfull!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                UploadLabel.Text = "Upload Failed";
            }
        }
               
    }  
  
}