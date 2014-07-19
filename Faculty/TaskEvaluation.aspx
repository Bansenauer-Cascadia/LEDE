<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskEvaluation.aspx.cs" Inherits="ECSEL.Faculty.TaskEvaluation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Task Feedback</h1>
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
        <asp:GridView ID="AssignmentGrid" runat="server" AutoGenerateColumns="false" DataSourceID="GridDataSource" 
            DataKeyNames="versid" OnRowCommand="AssignmentGrid_RowCommand">
            <Columns>                
                <asp:BoundField DataField="assignment" HeaderText="Assignment " />
                <asp:BoundField DataField="version" HeaderText ="Version" />
                <asp:ButtonField DataTextField="FileName" CommandName="DownloadAssignment" HeaderText="Document" />
                <asp:BoundField datafield="uploaddate" HeaderText="Date Modified" />
                <asp:ButtonField DataTextField="FeedbackFileName" CommandName="DownloadFeedback" 
                    HeaderText="Feedback Document"/>                
                <asp:ButtonField Text="Upload Feedback" CommandName="Upload"/>
                <asp:BoundField DataField="FeedbackUploadDate" HeaderText="Date Modified" />
                <asp:BoundField DataField ="ratingstatus" HeaderText="Rating Status" />
                <asp:ButtonField DataTextField="RatingLink" CommandName="Rate"></asp:ButtonField>               
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="GridDataSource" runat="server" SelectMethod="getAssignments" TypeName="ledeDB">
            <SelectParameters>
                <asp:ControlParameter ControlID="StudentDropDown" Name="userID" PropertyName="SelectedValue" type="String"/>  
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        
    </div>
</asp:Content>
