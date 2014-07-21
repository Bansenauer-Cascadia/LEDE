<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskEvaluation.aspx.cs" Inherits="ECSEL.Faculty.TaskEvaluation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Task Evaluation</h1>
        <p></p>   
    <div>
    Choose a Student:<asp:DropDownList ID="StudentDropDown" AutoPostBack="true" runat="server" DataSourceID="StudentDataSource" 
        DataTextField="name" DataValueField="id">
            </asp:DropDownList>

        <asp:ObjectDataSource ID="StudentDataSource" runat="server" SelectMethod="getUsers" 
            TypeName="ledeDB" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource> <br /> 
         <asp:Label ID="UploadLabel" runat="server"></asp:Label>
        <br />
         <asp:FileUpload ID="FileUpload1" runat="server" visible="false"/>

        <asp:Button ID="SubmitButton" runat="server" Text="Submit" visible="false" OnClick="SubmitButton_Click" /> 
        <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
        <br />
        <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" Visible="false" />
        <br /> 
        <asp:GridView ID="AssignmentGrid" runat="server" AutoGenerateColumns="false" DataSourceID="AssignmentGridDataSource" 
            DataKeyNames="versid" OnRowCommand="AssignmentGrid_RowCommand" AllowSorting="true">
            <Columns>                
                <asp:BoundField DataField="assignment" HeaderText="Task " SortExpression ="assignment, version"/>
                <asp:BoundField DataField="version" HeaderText ="Version" />
                <asp:ButtonField DataTextField="FileName" CommandName="DownloadAssignment" HeaderText="Document" />
                <asp:BoundField datafield="uploaddate" HeaderText="Date Modified" SortExpression="uploaddate"/>
                <asp:ButtonField DataTextField="FeedbackFileName" CommandName="DownloadFeedback" 
                    HeaderText="Feedback Document"/>                
                <asp:ButtonField Text="Upload Feedback" CommandName="Upload"/>
                <asp:BoundField DataField="FeedbackUploadDate" HeaderText="Date Modified" />
                <asp:BoundField DataField ="ratingstatus" HeaderText="Rating Status" SortExpression ="ratingstatus"/>
                <asp:ButtonField DataTextField="RatingLink" CommandName="Rate"></asp:ButtonField>               
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="AssignmentGridDataSource" runat="server" SelectMethod="getAssignments" TypeName="ledeDB" 
            SortParameterName="sortOrder">
            <SelectParameters>
                <asp:ControlParameter ControlID="StudentDropDown" Name="userID" PropertyName="SelectedValue" type="String"/>  
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        
    </div>
</asp:Content>
