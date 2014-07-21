using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECSEL.Models;
using Microsoft.AspNet.Identity; 

namespace ECSEL.Faculty
{
    public partial class TaskRating : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) //get the taskids which will require special behavior and add them to view state (reading log & reflection)
            {
                ViewState.Add("ReadingTaskIDs", ledeDB.getReadingTaskIDs());
                ViewState.Add("ReflectionTaskIDs", ledeDB.getReflectionTaskIDs());


            }
            Task task = null; int versid = 0;
            string versidquery = Request.QueryString["versid"];

            //refresh page if user changes version
            if (VersionDropDown.SelectedValue != "" && VersionDropDown.SelectedValue != versidquery)
                Response.Redirect("~/Faculty/TaskRating.aspx?versid=" + VersionDropDown.SelectedValue);

            if (versidquery != "")
            {
                versid = Convert.ToInt32(versidquery);
                task = ledeDB.getTask(versid);
            }

            //make sure we get faculty ID for inserting new ratings           
            CoreRatingDataSource.InsertParameters["FacultyID"].DefaultValue =
                TaskRatingDataSource.UpdateParameters["FacultyID"].DefaultValue =
                ImpactGridDataSource.InsertParameters["FacultyID"].DefaultValue = User.Identity.GetUserId();

            //Set up conditional formatting 
            if (task != null && versid != 0)
            {
                SeminarLabel.Text = task.Seminar.SeminarTitle;
                VersionDropDown.SelectedValue = versid.ToString();
                TaskLabel.Text = "Task " + task.TaskCode + ": " + task.TaskName;
                CandidateLabel.Text = ledeDB.getCandidateName(versid);
                formatForTask(task.TaskID);
            }
        }

        private void formatForTask(int taskID)
        {
            List<int> ReadingTaskIDs = (List<int>)ViewState["ReadingTaskIDs"];
            List<int> ReflectionTaskIDs = (List<int>)ViewState["ReflectionTaskIDs"];

            if (ReadingTaskIDs.Contains(taskID)) //task is a reading log
            {
                VersionLabel.Text = "Entry";
                ReadingListView.Visible = true;
                ReflectionListView.Visible = false;
            }
            else if (ReflectionTaskIDs.Contains(taskID)) // task is a reflection
            {
                VersionLabel.Text = "Version";
                ReadingListView.Visible = false;
                ReflectionListView.Visible = true;
            }
            else //task is regular assignment 
            {
                ReadingListView.Visible = false;
                ReflectionListView.Visible = false;
                VersionLabel.Text = "Version";
            }

        }
        //Hide insert templates for our listviews
        protected void ImpactListView_DataBound(object sender, EventArgs e)
        {
            if (ImpactListView.Items.Any())
                ImpactListView.InsertItem.Visible = false;
        }

        protected void ReadingListView_DataBound(object sender, EventArgs e)
        {
            if (ReadingListView.Items.Any())
                ReadingListView.InsertItem.Visible = false;
        }

        protected void ReflectionListView_DataBound(object sender, EventArgs e)
        {
            if (ReflectionListView.Items.Any())
                ReflectionListView.InsertItem.Visible = false;
        }

    }
}