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
            CoreScoreDataSource.SelectParameters["userid"].DefaultValue =
                ImpactScoreDataSource.SelectParameters["userid"].DefaultValue =
                User.Identity.GetUserId();

            string versid = Request.QueryString["versid"];
            Label1.Text = ledeDB.getScoreLabel(versid);
        }
    }
}