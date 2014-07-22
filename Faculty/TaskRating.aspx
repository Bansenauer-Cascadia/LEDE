<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskRating.aspx.cs" Inherits="ECSEL.Faculty.TaskRating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ratings For <asp:Label ID="SeminarLabel" runat="server"></asp:Label>&nbsp;Seminar</h1>
        
    <h2>&nbsp;<asp:Label ID="TaskLabel" runat ="server"></asp:Label></h2>
    <div>
        <table>
            <tr>                
                <td>
                    Candidate Name: <asp:Label ID="CandidateLabel" runat="server"></asp:Label>
                </td>
                <td>
                     <asp:Label ID="VersionLabel" runat="server"></asp:Label>: <asp:DropDownList ID="VersionDropDown" runat="server"  AutoPostBack="true" DataSourceID="VersionDataSource" DataTextField="Version"
                        DataValueField="versid">
                    </asp:DropDownList> 
                </td>
            </tr>           
        </table>
        
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
                        <asp:Label ID="HoursText" runat="server" Text='<%# Eval("NumHrs") %>'></asp:Label>                        
                    </td>  
                    <td>
                        <asp:Label ID="SumHoursText" runat="server" Text='<%# Eval("SumHrs") %>'></asp:Label>
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
                        <asp:CompareValidator ControlToValidate="HoursText" Type="Double" runat="server" Operator="DataTypeCheck"
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
                            ErrorMessage="Please enter an integer number of hours" ValidationGroup="ReflectionEdit">*</asp:CompareValidator>
                    </td>    
                     <td>
                        <asp:Label ID="SumHoursText" runat="server" Text='<%# Eval("SumHrs") %>'></asp:Label>
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
                                    <th runat="server">Entry Hours</th>
                                    <th runat="server">Cumulative Hours</th>                                                                       
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
        <strong>
        <br />
        Seminar Core Topic Assesments</strong><asp:ListView ID="TaskCoreRating" runat="server" DataSourceID="TaskRatingDataSource" DataKeyNames="coretopicid, ratingid">            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text='<%# Eval("CoreTopicDesc") %>'></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Cscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="CEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="CText" Type="Integer" runat="server"
                            ErrorMessage="C Score Must Be Between 0 and 3" ValidationGroup="CoreEdit">*</asp:RangeValidator>
                    </td>                    
                    <td>
                        <asp:TextBox ID="SText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="SEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="SText" Type="Integer" runat="server"
                            ErrorMessage="S Score Must Be Between 0 and 3" ValidationGroup="CoreEdit">*</asp:RangeValidator>
                    </td> 
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="PEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="PText" Type="Integer" runat="server"
                            ErrorMessage="P Score Must Be Between 0 and 3" ValidationGroup="CoreEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="CoreEdit" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>            
            <ItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID="CoreLabel" runat="server" Text='<%# Eval("CoreTopicDesc") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CLabel" runat="server" Text='<%# Eval("Cscore") %>'></asp:Label>
                    </td>                    
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("Sscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>
                    </td>
                    <td>                        
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Clear"/>
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">                                    
                                    <th runat="server">Core Topic</th>
                                    <th runat="server">Conceptual</th>
                                    <th runat="server">Strategic</th>
                                    <th runat="server">Personal</th>
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
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <asp:ValidationSummary ID="CoreRatingValidationSummary" runat="server" HeaderText="Please Correct These Entries:" 
            ValidationGroup="CoreEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/>         


          <asp:ObjectDataSource ID="TaskRatingDataSource" runat="server" SelectMethod="getTaskRatings" TypeName="ledeDB" 
              DeleteMethod="deleteTaskRating" OldValuesParameterFormatString="original_{0}" UpdateMethod="updateTaskRating">  
            <DeleteParameters>            
            </DeleteParameters>         
            <SelectParameters>
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
            </SelectParameters>          
            <UpdateParameters>  
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" />     
                <asp:Parameter Name ="FacultyID" />     
            </UpdateParameters>
        </asp:ObjectDataSource>

        <span class="auto-style2"><strong>Other Core Rating Assesments</strong></span> 

        <asp:ListView ID="ListView1" runat="server" DataSourceID="CoreRatingDataSource" DataKeyNames="ratingid" InsertItemPosition="FirstItem" 
            >    
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>           
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text='<%# Eval("CoreTopicDesc") %>'></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Cscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="AllCEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="CText" Type="Integer" runat="server"
                            ErrorMessage="C Score Must Be Between 0 and 3" ValidationGroup="AllCoreEdit">*</asp:RangeValidator>
                    </td>                    
                    <td>
                        <asp:TextBox ID="SText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="AllSEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="SText" Type="Integer" runat="server"
                            ErrorMessage="S Score Must Be Between 0 and 3" ValidationGroup="AllCoreEdit">*</asp:RangeValidator>
                    </td> 
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="AllPEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="PText" Type="Integer" runat="server"
                            ErrorMessage="P Score Must Be Between 0 and 3" ValidationGroup="AllCoreEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="AllCoreEdit"/>
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">                   
                    <td>
                        <asp:DropDownList ID="CoreDropDown" runat="server" DataSourceID="CoreTopicDataSource" 
                            DataTextField="CoreTopicDesc" DataValueField="CoreTopicID"  SelectedValue='<%# Bind("CoreTopicID") %>'>
                        </asp:DropDownList>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Cscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="AllCInsertValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="CText" Type="Integer" runat="server"
                            ErrorMessage="C Score Must Be Between 0 and 3" ValidationGroup="AllCoreEdit">*</asp:RangeValidator>
                    </td>                    
                    <td>
                        <asp:TextBox ID="SText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="AllSInsertValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="SText" Type="Integer" runat="server"
                            ErrorMessage="S Score Must Be Between 0 and 3" ValidationGroup="AllCoreEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="AllPInsertValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="PText" Type="Integer" runat="server"
                            ErrorMessage="P Score Must Be Between 0 and 3" ValidationGroup="AllCoreEdit">*</asp:RangeValidator>
                    </td>
                     <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="AllCoreEdit"/>
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID="CoreLabel" runat="server" Text='<%# Eval("CoreTopicDesc") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CLabel" runat="server" Text='<%# Eval("Cscore") %>'></asp:Label>
                    </td>                    
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("Sscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Clear" Text="Clear" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">                                    
                                    <th runat="server">Core Topic</th>
                                    <th runat="server">Conceptual</th>
                                    <th runat="server">Strategic</th>
                                    <th runat="server">Personal</th>
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
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <asp:ValidationSummary ID="AllCoreValidationSummary" runat="server" HeaderText="Please Correct These Entries:" 
            ValidationGroup="AllCoreEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/> 

        <br />
        <span class="auto-style2">
            <strong>Impact Type Assessments
        </strong></span>        

        <asp:ListView ID="ImpactListView" runat="server" DataSourceID="ImpactGridDataSource" DataKeyNames="RatingID" 
            InsertItemPosition="LastItem" OnDataBound="ImpactListView_DataBound" >            
            <EditItemTemplate>
                <tr style="" >                                     
                    <td>
                        <asp:TextBox ID="SText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="SEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="SText" Type="Integer" runat="server"
                            ErrorMessage="S Score Must Be Between 0 and 3" ValidationGroup="ImpactEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="PEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="PText" Type="Integer" runat="server"
                            ErrorMessage="P Score Must Be Between 0 and 3" ValidationGroup="ImpactEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="LText" runat="server" Text='<%# Bind("Lscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="LEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="LText" Type="Integer" runat="server"
                            ErrorMessage="L Score Must Be Between 0 and 3" ValidationGroup="ImpactEdit">*</asp:RangeValidator>
                    </td> 
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="ImpactEdit"/>
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>                            
                    <td>
                        <asp:TextBox ID="SText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="SEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="SText" Type="Integer" runat="server"
                            ErrorMessage="S Score Must Be Between 0 and 3" ValidationGroup="ImpactEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="PEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="PText" Type="Integer" runat="server"
                            ErrorMessage="P Score Must Be Between 0 and 3" ValidationGroup="ImpactEdit">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="LText" runat="server" Text='<%# Bind("Lscore") %>'></asp:TextBox>
                        <asp:RangeValidator ID="LEditValidator" MinimumValue="0" MaximumValue="3"
                            ControlToValidate="LText" Type="Integer" runat="server"
                            ErrorMessage="L Score Must Be Between 0 and 3" ValidationGroup="ImpactEdit">*</asp:RangeValidator>
                    </td> 
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="ImpactEdit"/>
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
            </InsertItemTemplate>          
            <ItemTemplate>
                <tr style="">                                        
                    <td>
                        <asp:Label ID="CLabel" runat="server" Text='<%# Eval("Sscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("Lscore") %>'></asp:Label>
                    </td>
                    <td>                       
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server">                                                                        
                                    <th runat="server">Structures & Operations</th>
                                    <th runat="server">Professional Practices</th>
                                    <th runat="server">Student Learning</th>
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
            <SelectedItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                    </td>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <asp:ValidationSummary ID="ImpactValidationSummary" runat="server" HeaderText="Please Correct These Entries:" 
            ValidationGroup="ImpactEdit" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"/> 

         <asp:ObjectDataSource ID="ImpactGridDataSource" runat="server" SelectMethod="getImpactGrid" 
             UpdateMethod="updateImpactGrid" InsertMethod="insertImpactGrid" TypeName="ledeDB">
            <SelectParameters>               
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
            </SelectParameters>
            <UpdateParameters>             
            </UpdateParameters>
             <InsertParameters>
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                 <asp:Parameter Name ="FacultyID" />
             </InsertParameters>
        </asp:ObjectDataSource>                                                 

        <asp:ObjectDataSource ID="CoreTopicDataSource" runat="server" SelectMethod="getCoreTopics" TypeName="ledeDB">
            <SelectParameters>
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
            </SelectParameters>
        </asp:ObjectDataSource>      
                
        <asp:ObjectDataSource ID="CoreRatingDataSource" runat="server" SelectMethod="getCoreRatings" TypeName="ledeDB" DeleteMethod="deleteCoreRating" InsertMethod="insertCoreRating" OldValuesParameterFormatString="original_{0}" UpdateMethod="updateCoreRating">  
            <DeleteParameters>           
            </DeleteParameters>
            <InsertParameters>
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
                <asp:Parameter Name ="FacultyID" />
            </InsertParameters>
            <SelectParameters>                
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
            </SelectParameters>          
            <UpdateParameters>              
            </UpdateParameters>
        </asp:ObjectDataSource>
       
        <asp:ObjectDataSource ID="VersionDataSource" runat="server" TypeName="ledeDB" SelectMethod="getVersion">
            <SelectParameters>
                <asp:QueryStringParameter Name="versid" QueryStringField="versid" /> 
            </SelectParameters>
        </asp:ObjectDataSource>    
    </div>
</asp:Content>
