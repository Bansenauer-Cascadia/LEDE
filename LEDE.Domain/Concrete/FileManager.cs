using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEDE.Domain.Abstract;
using LEDE.Domain.Entities;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System;

namespace LEDE.Domain.Concrete
{
    public class FileManager
    {
        protected static string accountName = "ledeportal";
        protected static string accountKey = "6xcqHiY0vhCSlLdVEmBWonsv9BH0nmBRr7bxBTxkvCXw/iklMXk2JF1rCoO46H3Vc19vbdq+j3nZEbI2P4BBOA==";
        protected static StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        protected static CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);
        protected static CloudBlobClient client = account.CreateCloudBlobClient();

        public static bool UploadDocument(Document uploadDoc, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {                   
                    CloudBlobContainer studentContainer = client.GetContainerReference(uploadDoc.Container);
                    studentContainer.CreateIfNotExists();

                    CloudBlockBlob blob = studentContainer.GetBlockBlobReference(uploadDoc.Blob);
                    using (Stream fileStream = file.InputStream)
                    {
                        blob.UploadFromStream(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                    return false;
                }
                return true; 
            }
            return false; 
        }

        public static void DownloadDocument(Document downloadDoc, HttpContextBase context)
        {
            CloudBlobContainer sampleContainer = client.GetContainerReference(downloadDoc.Container);
            CloudBlockBlob fileBlob = sampleContainer.GetBlockBlobReference(downloadDoc.Blob);

            context.Response.Charset = "UTF-8";
            context.Response.Buffer = false;
            context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + downloadDoc.FileName + "\"");
            context.Response.AddHeader("Content-Length", downloadDoc.FileSize); //Set the length the file
            context.Response.ContentType = "application/msword";
            context.Response.Flush();

            fileBlob.DownloadToStream(context.Response.OutputStream);
        }

    }

}