<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Site1.Master" AutoEventWireup="true" CodeBehind="TaskRating.aspx.cs" Inherits="LEDE_MVC.WebForms.TaskRating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Submit Task Assesment</h1>
        <p></p>     
    <div>
        Task: <asp:DropDownList ID ="TaskDropDown" runat="server" DataSourceID="TaskDataSource" DataTextField="taskname"
            DataValueField="taskid" AutoPostBack="true"></asp:DropDownList><br />
        Student: <asp:DropDownList ID="StudentDropDown" runat="server" DataSourceID="StudentDataSource"
            datatextfield="lastname" DataValueField="id" AutoPostBack="true"></asp:DropDownList> <br />
        Version:<asp:DropDownList ID="VersionDropDown" runat="server" DataSourceID="VersionDataSource"
            datatextfield="version" DataValueField="versid" AutoPostBack="true"></asp:DropDownList> 
        <br />
        <br />
        <span class="auto-style2"><strong>Seminar Core Rating Assesments</strong></span> 

                <asp:ListView ID="TaskCoreRating" runat="server" DataSourceID="TaskRatingDataSource" DataKeyNames="coretopicid, ratingid">            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text='<%# Eval("CoreTopicDesc") %>'></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Cscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                    </td> 
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
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
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("Sscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Clear"/>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
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
                                    <th runat="server">C Score</th>
                                    <th runat="server">P Score</th>
                                    <th runat="server">S Score</th>
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

          <asp:ObjectDataSource ID="TaskRatingDataSource" runat="server" SelectMethod="getTaskRatings" TypeName="ledeDB" 
              DeleteMethod="deleteTaskRating" OldValuesParameterFormatString="original_{0}" UpdateMethod="updateTaskRating">  
            <DeleteParameters>            
            </DeleteParameters>         
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="taskid" PropertyName="SelectedValue" type="String"/>                                
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/>                
            </SelectParameters>          
            <UpdateParameters>  
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/> 
                <asp:Parameter Name="FacultyID" Type="String" />            
            </UpdateParameters>
        </asp:ObjectDataSource>

        <span class="auto-style2"><strong>Other Core Rating Assesments</strong></span> 

        <asp:ListView ID="ListView1" runat="server" DataSourceID="CoreRatingDataSource" DataKeyNames="ratingid" InsertItemPosition="FirstItem" 
            >            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text='<%# Eval("CoreTopicDesc") %>'></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Cscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                    </td> 
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
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
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                    </td>
                     <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
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
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("Pscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("Sscore") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" />
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
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
                                    <th runat="server">C Score</th>
                                    <th runat="server">P Score</th>
                                    <th runat="server">S Score</th>
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
        <span class="auto-style2"><strong>Impact Type Assessments:
        </strong></span><strong>
        <br class="auto-style2" />
        <br class="auto-style2" />
        </strong>

        <asp:ListView ID="ImpactListView" runat="server" DataSourceID="ImpactGridDataSource" DataKeyNames="RatingID" 
            InsertItemPosition="LastItem">            
            <EditItemTemplate>
                <tr style="">                                     
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("Lscore") %>'></asp:TextBox>
                    </td> 
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>                            
                 <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("Sscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("Pscore") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("Lscore") %>'></asp:TextBox>
                    </td> 
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
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
                                <tr runat="server" style="">                                                                        
                                    <th runat="server">S Score</th>
                                    <th runat="server">P Score</th>
                                    <th runat="server">L Score</th>
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


         <asp:ObjectDataSource ID="ImpactGridDataSource" runat="server" SelectMethod="getImpactGrid" 
             UpdateMethod="updateImpactGrid" InsertMethod="insertImpactGrid" TypeName="ledeDB">
            <SelectParameters>               
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
            <UpdateParameters>             
            </UpdateParameters>
             <InsertParameters>
                <asp:Parameter Name ="FacultyID" Type ="String" />
                <asp:ControlParameter ControlID="VersionDropDown" Name="versID" PropertyName="SelectedValue" type="String"/>
             </InsertParameters>
        </asp:ObjectDataSource>                                                 

        <asp:ObjectDataSource ID="CoreTopicDataSource" runat="server" SelectMethod="getCoreTopics" TypeName="ledeDB">
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="taskid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
        </asp:ObjectDataSource>      
                
        <asp:ObjectDataSource ID="CoreRatingDataSource" runat="server" SelectMethod="getCoreRatings" TypeName="ledeDB" DeleteMethod="deleteCoreRating" InsertMethod="insertCoreRating" OldValuesParameterFormatString="original_{0}" UpdateMethod="updateCoreRating">  
            <DeleteParameters>            
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="CoreTopicID" Type="String" />
                <asp:Parameter Name ="FacultyID" Type ="String" />
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/> 
            </InsertParameters>
            <SelectParameters>                
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/> 
                <asp:ControlParameter ControlID="TaskDropDown" Name="taskid" PropertyName="SelectedValue" type="String"/>               
            </SelectParameters>          
            <UpdateParameters>              
            </UpdateParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="StudentDataSource" runat="server" SelectMethod="getUsers" TypeName="ledeDB" >
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="TaskID" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="TaskDataSource" runat="server" TypeName="ledeDB" SelectMethod="getTasks">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="VersionDataSource" runat="server" TypeName="ledeDB" SelectMethod="getVersion">
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="TaskID" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="StudentDropDown" Name="UserID" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
        </asp:ObjectDataSource>
     
    </div>
</asp:Content>
