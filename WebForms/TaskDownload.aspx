<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Site1.Master" AutoEventWireup="true" CodeBehind="TaskDownload.aspx.cs" Inherits="LEDE_MVC.WebForms.TaskDownload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Download Assignments</h1>
        <p></p>   
    <div>
    Student:<asp:DropDownList ID="StudentDropDown" AutoPostBack="true" runat="server" DataSourceID="StudentDataSource" 
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
                <asp:BoundField datafield="uploaddate" HeaderText="Date Modified" />
                <asp:BoundField DataField ="ratingstatus" HeaderText="Rating Status" />
                <asp:ButtonField Text="Download" CommandName="Download"/>
                <asp:ButtonField Text="Upload Feedback" CommandName="Upload"/>
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
