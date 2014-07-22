<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="ECSEL.Candidate.Tasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Tasks</h1>

    <h2>Performance Task:&nbsp;&nbsp;<asp:DropDownList ID="taskNameDropDown" runat="server" DataSourceID="taskNameDataSource"
        DataValueField="TaskID" DataTextField="TaskName" AutoPostBack="True">
    </asp:DropDownList>
    </h2>
    <asp:Button ID="UploadToggleButton" runat="server" OnClick="UploadToggleButton_Click" Text="Upload" />
    <asp:Panel ID="UploadPanel" runat="server" Visible="false">
        File<asp:FileUpload ID="FileUpload1" runat="server" />

        <br />
        <asp:Label ID="ReadingEntriesLabel" Text="Number of Entries" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="ReadingEntriesTextBox" runat="server" Visible="false"></asp:TextBox>
        <asp:RequiredFieldValidator ID="PageTextValidator"
            ControlToValidate="ReadingEntriesTextBox" runat="server"
            ErrorMessage="Please enter number of entries" ValidationGroup="Upload">*</asp:RequiredFieldValidator>
        <asp:CompareValidator ControlToValidate="ReadingEntriesTextBox" Type="Integer" runat="server" Operator="DataTypeCheck"
            ErrorMessage="Please enter an integer number of entries" ValidationGroup="Upload">*</asp:CompareValidator>

        <asp:Label ID="ReflectionHoursLabel" Text="Number of Hours" runat="server" Visible="false"></asp:Label>
        <asp:TextBox ID="ReflectionHoursTextBox" runat="server" Visible="false"></asp:TextBox>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
            ControlToValidate="ReflectionHoursTextBox" runat="server"
            ErrorMessage="Please enter number of hours" ValidationGroup="Upload">*</asp:RequiredFieldValidator>
        <asp:CompareValidator ControlToValidate="ReflectionHoursTextBox" Type="Double" runat="server" Operator="DataTypeCheck"
            ErrorMessage="Please enter a number value of hours" ValidationGroup="Upload">*</asp:CompareValidator>

        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" ValidationGroup="Upload" />        
        <asp:Button ID="ReflectionSubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" ValidationGroup="Upload" />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" />


        <!-- Validation and Label for Server Side Validation-->
        <asp:ValidationSummary ID="ReadingValidationSummary" runat="server" HeaderText="Please Correct These Entries:"
            ValidationGroup="Upload" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />

        
        <asp:RequiredFieldValidator runat="server" ValidationGroup="Upload" ID="UploadFileValidator"
            ControlToValidate="FileUpload1" ErrorMessage="Please choose a file to upload">*</asp:RequiredFieldValidator>

    </asp:Panel>
    <asp:Label ID="UploadLabel" runat="server" Text=""></asp:Label>

    <asp:ObjectDataSource runat="server" ID="TaskNameDataSource" SelectMethod="getUploadTasks" TypeName="ledeDB"></asp:ObjectDataSource>

    <h2>
    Submissions for Task <asp:Label ID="TaskLabel" runat = server></asp:Label>
    </h2>
    <asp:GridView ID="UploadGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="VersID"
        DataSourceID="ObjectDataSource1" OnRowCommand="UploadGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="taskname" HeaderText="Performance Task" />
            <asp:BoundField DataField="version" HeaderText="Version" />
            <asp:ButtonField CommandName="submission" DataTextField="filename" HeaderText="Submission" />
            <asp:BoundField DataField="uploaddate" HeaderText="Submission Date" />
            <asp:TemplateField HeaderText="Faculty Feedback" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="FeedbackLink" runat="server" CausesValidation="false"
                        CommandName="feedback" Text='<%# Eval("feedbackfilename") %>'
                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="feedbackuploaddate" HeaderText="Feedback Date" />
            <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button runat="server" Text="Delete" CommandName="delete" Visible='<%# !(bool)Eval("RatingSubmitted") %>'
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"/>
                        <asp:Button runat="server" Text="View Rating" CommandName="score" Visible='<%#(bool) Eval("RatingSubmitted")%>'
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"/>
                    </ItemTemplate>                    
                </asp:TemplateField>  
        </Columns>
        <EmptyDataTemplate>
            None 
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="{0}" SelectMethod="getUploadGrid" 
        DeleteMethod="deleteTaskVersion" TypeName="ledeDB">
        <SelectParameters>
            <asp:Parameter Name="userID" Type="String" />
            <asp:ControlParameter ControlID="taskNameDropDown" DefaultValue="" Name="taskid" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
