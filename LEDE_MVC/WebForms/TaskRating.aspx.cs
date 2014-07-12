using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace LEDE_MVC.WebForms
{
    public partial class TaskRating : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //make sure we get faculty ID for inserting new ratings            
            CoreRatingDataSource.InsertParameters["FacultyID"].DefaultValue = 
                TaskRatingDataSource.UpdateParameters["FacultyID"].DefaultValue = 
                ImpactGridDataSource.InsertParameters["FacultyID"].DefaultValue = User.Identity.GetUserId();            
        }

        protected void ListView1_DataBound(object sender, EventArgs e)
        {
            if (ImpactListView.Items.Any())
                ImpactListView.InsertItem.Visible = false;
        }                     
    }
}