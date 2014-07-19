<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="ECSEL.Candidate.Tasks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Upload an Assignment 
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </h1>        
           
    Performance Task:<asp:DropDownList ID="taskNameDropDown" runat="server" DataSourceID="taskNameDataSource"
                      DataValueField="TaskID" DataTextField="TaskName" AutoPostBack="True" ></asp:DropDownList>                      
        
        <asp:Panel ID="UploadPanel" runat="server"> 
        <asp:FileUpload ID="FileUpload1" runat="server" />        
            </asp:Panel>        

        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" ValidationGroup="ReadingEdit" />
        <asp:Button ID="ReflectionSubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" ValidationGroup="ReflectionEdit" />

        <asp:Label ID="UploadLabel" runat="server" Text=""></asp:Label>
    <br />

     <asp:Panel ID="AdditionalDataPanel" runat="server">
            <asp:ListView ID ="ReadingListView" runat="server" DataSourceID="ReadingDataSource" 
                DataKeyNames="versid" OnDataBound="ReadingListView_DataBound" InsertItemPosition="FirstItem">
                <ItemTemplate>
                    <td>
                        <asp:Label ID="PageText" runat="server" Text='<%# Eval("NumEntries") %>'></asp:Label>                        
                    </td>                    
                    <td>                       
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>                 
                </ItemTemplate>
                <InsertItemTemplate>
                    <td>
                        <asp:TextBox ID="PageText" runat="server" Text='<%# Bind("NumEntries") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PageTextValidator" 
                            ControlToValidate="PageText" runat="server"
                            ErrorMessage="Please enter number of entries" ValidationGroup="ReadingEdit">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="PageText" Type="Integer" runat="server" Operator="DataTypeCheck"
                            ErrorMessage="Please enter an integer number of entries" ValidationGroup="ReadingEdit">*</asp:CompareValidator>
                    </td>                    
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="ReadingEdit" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <td>
                        <asp:TextBox ID="PageText" runat="server" Text='<%# Bind("NumEntries") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PageTextValidator" 
                            ControlToValidate="PageText" runat="server"
                            ErrorMessage="Please enter number of entries" ValidationGroup="ReadingEdit">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="PageText" Type="Integer" runat="server" Operator="DataTypeCheck"
                            ErrorMessage="Please enter an integer number of entries" ValidationGroup="ReadingEdit">*</asp:CompareValidator>
                    </td>     
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="ReadingEdit" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </EditItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">                                                                        
                                    <th runat="server">Number of Entries</th>                                    
                                    <th runat="server"></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style=""></td>
                    </tr>
                </table>
                </LayoutTemplate>
            </asp:ListView>

            <asp:ObjectDataSource ID="ReadingDataSource" runat="server" SelectMethod="getReadingLog"
                 TypeName="ledeDB" InsertMethod="insertReadingLog" UpdateMethod="updateReadingLog">               
                <SelectParameters>                     
                    <asp:QueryStringParameter Name="versid" QueryStringField="versid" />                
                </SelectParameters>
                <InsertParameters>
                    <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                </InsertParameters>
                <UpdateParameters>
                    <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                </UpdateParameters>
            </asp:ObjectDataSource>

            <asp:ValidationSummary ID="ReadingValidationSummary" runat="server" HeaderText="Please Correct These Entries:" 
            ValidationGroup="ReadingEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/> 

            <asp:ListView ID ="ReflectionListView" runat="server" DataSourceID="ReflectionDataSource" 
                DataKeyNames="versid" OnDataBound="ReflectionListView_DataBound" InsertItemPosition="FirstItem">
                <ItemTemplate>
                    <td>
                        <asp:Label ID="PageText" runat="server" Text='<%# Eval("NumHrs") %>'></asp:Label>                        
                    </td>                                                    
                    <td>                       
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>                 
                </ItemTemplate>
                <InsertItemTemplate>
                    <td>
                        <asp:TextBox ID="HoursText" runat="server" Text='<%# Bind("NumHrs") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PageTextValidator" 
                            ControlToValidate="HoursText" runat="server" 
                            ErrorMessage="Please enter number of hours" ValidationGroup="ReflectionEdit">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="HoursText" Type="Integer" runat="server" Operator="DataTypeCheck"
                            ErrorMessage="Please enter an integer number of hours" ValidationGroup="ReflectionEdit">*</asp:CompareValidator>                        
                    </td>                             
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="ReflectionEdit" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                </InsertItemTemplate>
                <EditItemTemplate>
                     <td>
                        <asp:TextBox ID="HoursText" runat="server" Text='<%# Bind("NumHrs") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PageTextValidator" 
                            ControlToValidate="HoursText" runat="server"
                            ErrorMessage="Please enter number of hours" ValidationGroup="ReflectionEdit">*</asp:RequiredFieldValidator>
                         <asp:CompareValidator ControlToValidate="HoursText" Type="Integer" runat="server" Operator="DataTypeCheck"
                            ErrorMessage="Please enter an integer number of hours" ValidationGroup="ReadingEdit">*</asp:CompareValidator>
                    </td>                    
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="ReflectionEdit" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </EditItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">                                                                        
                                    <th runat="server">Number of Hours</th>                                                                       
                                    <th runat="server"></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style=""></td>
                    </tr>
                </table>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="ReflectionDataSource" runat="server" SelectMethod="getReflectionEntry"
                 TypeName="ledeDB" InsertMethod="insertReflectionEntry" UpdateMethod="updateReflectionEntry">
                <SelectParameters> 
                    <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                </SelectParameters>
                <InsertParameters>
                    <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                </InsertParameters>
                <UpdateParameters>
                    <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                </UpdateParameters>
            </asp:ObjectDataSource>

             <asp:ValidationSummary ID="ReflectionValidationSummary" runat="server" HeaderText="Please Correct These Entries:" 
            ValidationGroup="ReflectionEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/> 

        </asp:Panel>  

        <asp:ObjectDataSource id="taskNameDataSource" runat="server" SelectMethod="getUploadTasks" TypeName="ledeDB"> </asp:ObjectDataSource>
        
        
        <br />
        Prior Submissions:
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
