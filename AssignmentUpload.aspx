<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignmentUpload.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Upload an Assignment </h1>
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home" Value="Home"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <br />
    Performance Task:<asp:DropDownList ID="taskNameDropDown" runat="server" DataSourceID="taskNameDataSource"
                      DataValueField="taskid" DataTextField="taskname" AutoPostBack="True" ></asp:DropDownList> 
        <br />
        <br /> 
        
        <asp:Panel ID="UploadPanel" runat="server"> 
        <asp:FileUpload ID="FileUpload1" runat="server" />        
            </asp:Panel>        

        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />

        <asp:Label ID="UploadLabel" runat="server" Text=""></asp:Label>

        <asp:ObjectDataSource id="taskNameDataSource" runat="server" SelectMethod="getTasks" TypeName="ledeDB"> </asp:ObjectDataSource>
        <br />
        <br />
        Previously Uploaded Versions:
        <br />
        <asp:GridView ID="UploadGridView" runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource1" OnRowCommand="UploadGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="taskname" HeaderText="Task" />
                <asp:BoundField DataField="version" HeaderText="Version" />              
                <asp:ButtonField CommandName="submission" DataTextField="filename" HeaderText="Assignment File" />
                <asp:BoundField DataField="uploaddate" HeaderText="Upload Date" />
                <asp:TemplateField HeaderText="Feedback File" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="FeedbackLink" runat="server" CausesValidation="false" 
                            CommandName="feedback" Text='<%# Eval("feedbackfilename") %>'
                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="feedbackuploaddate" HeaderText="Upload Date" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="getUploadGrid" TypeName="ledeDB">
            <SelectParameters>
                <asp:Parameter DefaultValue="3" Name="userID" Type="String" />
                <asp:ControlParameter ControlID="taskNameDropDown" DefaultValue="" Name="taskid" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
