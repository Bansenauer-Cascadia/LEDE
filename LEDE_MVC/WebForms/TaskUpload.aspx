<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Site1.Master" AutoEventWireup="true" CodeBehind="TaskUpload.aspx.cs" Inherits="LEDE_MVC.WebForms.TaskUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Upload an Assignment </h1>        
           
    Performance Task:<asp:DropDownList ID="taskNameDropDown" runat="server" DataSourceID="taskNameDataSource"
                      DataValueField="TaskID" DataTextField="TaskName" AutoPostBack="True" ></asp:DropDownList> 
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
        <asp:GridView ID="UploadGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="VersID" 
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
                <asp:ButtonField CommandName="score" Text="View Score" HeaderText=""/>
            </Columns>            
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="getUploadGrid" TypeName="ledeDB">
            <SelectParameters>
                <asp:Parameter Name="userID" Type="String" />
                <asp:ControlParameter ControlID="taskNameDropDown" DefaultValue="" Name="taskid" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
</asp:Content>
