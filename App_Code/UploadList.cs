using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


/// <summary>
/// Summary description for UploadList
/// </summary>
public class UploadList
{
    private List<FileUpload> FileUploadList; 

	public UploadList()
	{
        FileUploadList = new List<FileUpload>(); 
	}
}