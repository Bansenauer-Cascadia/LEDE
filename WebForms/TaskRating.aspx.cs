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

            if (!IsPostBack) //get the taskids which will require special behavior and add them to view state (reading log & reflection)
            {
                ViewState.Add("ReadingTaskIDs", ledeDB.getReadingTaskIDs());
                ViewState.Add("ReflectionTaskIDs", ledeDB.getReflectionTaskIDs());
                formatForTask(ledeDB.initializeTaskSelectedValue()); //in case default item is log/reflection    
            }

            //make sure a task is selected
            if (TaskDropDown.SelectedValue != "")
            {
                formatForTask(Convert.ToInt32(TaskDropDown.SelectedValue));
            }            
        }

        private void formatForTask(int taskID)
        {
            List<int> ReadingTaskIDs = (List<int>)ViewState["ReadingTaskIDs"];
            List<int> ReflectionTaskIDs = (List<int>)ViewState["ReflectionTaskIDs"];

            if (ReadingTaskIDs.Contains(taskID)) //student is uploading a reading log
            {
                VersionLabel.Text = "Entry: ";

                //Dynamically create Reading Log Data Entry
                Table readingTable = new Table();

                TableRow headerRow = new TableRow();
                TableRow controlRow = new TableRow();

                //reading title column 
                TableCell titleHeader = new TableCell();
                Label titleLabel = new Label(); titleLabel.Text = "Reading Title";
                titleHeader.Controls.Add(titleLabel);
                TableCell titleCell = new TableCell();
                TextBox titleText = new TextBox(); titleText.ID = "readingTitle";
                titleCell.Controls.Add(titleText);
                titleHeader.Controls.Add(titleLabel);

                headerRow.Controls.Add(titleHeader);
                controlRow.Controls.Add(titleCell);

                //number of pages column
                TableCell pageHeader = new TableCell();
                Label pageLabel = new Label(); pageLabel.Text = "Number of Pages";
                pageHeader.Controls.Add(pageLabel);
                TableCell pageCell = new TableCell();
                TextBox pageText = new TextBox(); pageText.ID = "numPages";
                pageCell.Controls.Add(pageText);
                pageHeader.Controls.Add(pageLabel);

                headerRow.Controls.Add(pageHeader);
                controlRow.Controls.Add(pageCell);

                //date column
                TableCell dateHeader = new TableCell();
                Label dateLabel = new Label(); dateLabel.Text = "Date";
                dateHeader.Controls.Add(dateLabel);
                TableCell dateCell = new TableCell();
                TextBox dateText = new TextBox(); dateText.ID = "readingDate";
                dateCell.Controls.Add(dateText);
                dateHeader.Controls.Add(dateLabel);

                headerRow.Controls.Add(dateHeader);
                controlRow.Controls.Add(dateCell);

                //add controls
                readingTable.Rows.Add(headerRow);
                readingTable.Rows.Add(controlRow);

                AdditionalDataPanel.Controls.Add(readingTable);

                Button submitButton = new Button();
                submitButton.Text = "Submit";
            }
            else if (ReflectionTaskIDs.Contains(taskID)) //student is uploading a reflection
            {
                VersionLabel.Text = "Version: ";

                //Dynamically create Reading Log Data Entry
                Table reflectionTable = new Table();

                TableRow headerRow = new TableRow();
                TableRow controlRow = new TableRow();

                //numHours column 
                TableCell hoursHeader = new TableCell();
                Label hoursLabel = new Label(); hoursLabel.Text = "Number of Hours";
                hoursHeader.Controls.Add(hoursLabel);
                TableCell hoursCell = new TableCell();
                TextBox hoursText = new TextBox(); hoursText.ID = "numHours";
                hoursCell.Controls.Add(hoursText);
                hoursHeader.Controls.Add(hoursLabel);

                headerRow.Controls.Add(hoursHeader);
                controlRow.Controls.Add(hoursCell);

                //ReflectionDate column
                TableCell dateHeader = new TableCell();
                Label dateLabel = new Label(); dateLabel.Text = "Date";
                dateHeader.Controls.Add(dateLabel);
                TableCell dateCell = new TableCell();
                TextBox dateText = new TextBox(); dateText.ID = "reflectionDate";
                dateCell.Controls.Add(dateText);
                dateHeader.Controls.Add(dateLabel);

                headerRow.Controls.Add(dateHeader);
                controlRow.Controls.Add(dateCell);

                //add controls
                reflectionTable.Rows.Add(headerRow);
                reflectionTable.Rows.Add(controlRow);

                AdditionalDataPanel.Controls.Add(reflectionTable);
            }
            else //student is uploading a regular assignment
            {
                VersionLabel.Text = "Version: ";
            }
        }         

        protected void ImpactListView_DataBound(object sender, EventArgs e)
        {
            if (ImpactListView.Items.Any())
                ImpactListView.InsertItem.Visible = false;
        }

        protected void StudentDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaskRatingDataSource.SelectParameters["userid"].DefaultValue =
            CoreRatingDataSource.SelectParameters["userid"].DefaultValue = 
            ImpactGridDataSource.SelectParameters["userid"].DefaultValue = StudentDropDown.SelectedValue;

            ImpactGridDataSource.SelectParameters["taskid"].DefaultValue = TaskDropDown.SelectedValue;
        }
                            
    }
}