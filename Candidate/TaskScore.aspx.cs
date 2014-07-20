using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace ECSEL.Candidate
{
    public partial class TaskScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            string versid = Request.QueryString["versid"];

            if (versid == null)
                Response.Redirect("~/Candidate/Tasks.aspx");

            Label1.Text = ledeDB.getScoreLabel(versid);
            SeminarLabel.Text = ledeDB.getSeminarName(versid); 
        }
    }
}