<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignmentDownload.aspx.cs" Inherits="AssignmentDownload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download Assignments</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Download Assignments</h1>
        <p></p>
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home" Value="Home"></asp:MenuItem>
            </Items>
        </asp:Menu>
    <div>
    Student:<asp:DropDownList ID="StudentDropDown" AutoPostBack="true" runat="server" DataSourceID="StudentDataSource" DataTextField="name" DataValueField="id">
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
            DataKeyNames="versID" OnRowCommand="AssignmentGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="versid" />
                <asp:BoundField DataField="assignment" HeaderText="Assignment " />
                <asp:BoundField DataField="version" HeaderText ="Version" />
                <asp:BoundField datafield="Date Modified" HeaderText="Date Modified" />
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
    </form>
</body>
</html>
