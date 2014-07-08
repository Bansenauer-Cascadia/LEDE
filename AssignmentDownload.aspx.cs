using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms; 
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;



public partial class AssignmentDownload : System.Web.UI.Page
{
    protected static string accountName = "ledeportal";
    protected static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";
    protected static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
    protected static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
    protected static CloudBlobClient client = account.CreateCloudBlobClient();
    protected static string versID; 


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void AssignmentGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {   
        int index = Convert.ToInt32(e.CommandArgument);        
        GridViewRow row = AssignmentGrid.Rows[index];
        versID = row.Cells[0].Text;                              
      
        /*if (e.CommandName == "Download")
        {

            CloudBlobContainer sampleContainer = client.GetContainerReference("student1");
            sampleContainer.CreateIfNotExists();

            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference("1/1");
            using (Stream outputFile = new FileStream("C:/Users/John/Downloads/test.docx", FileMode.Create))
            {
                blob.DownloadToStream(outputFile);
            }
        }*/
        if (e.CommandName == "Download")
        {

            CloudBlobContainer sampleContainer = client.GetContainerReference("student1");
            sampleContainer.CreateIfNotExists();

            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference("1/1");

            //byte[] target = new byte[blob.StreamMinimumReadSizeInBytes];
            //blob.DownloadToByteArray(target, 0);

           // Response.ContentType = "text/HTML"; 
           // Response.BinaryWrite(target); 
            

        }

        else if (e.CommandName == "Upload")
        {
            string assignment = row.Cells[1].Text; 
            string version = row.Cells[2].Text;

            showUploadControls(); 
        }
  
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile == true)
        {
            try
            {
                string containerString = StudentDropDown.SelectedValue;

                CloudBlobContainer studentContainer = client.GetContainerReference("student" + containerString);
                studentContainer.CreateIfNotExists();

                CloudBlockBlob blob = studentContainer.GetBlockBlobReference(versID + "feedback");
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
            }                        
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