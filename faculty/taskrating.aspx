<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskRating.aspx.cs" Inherits="LEDEPortal.Faculty.TaskRating1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Enter A Task Rating</h3>
    Task: <asp:DropDownList ID ="TaskDropDown" runat="server" DataSourceID="TaskDataSource" DataTextField="taskname"
            DataValueField="taskid" AutoPostBack="true"></asp:DropDownList><br />
        Candidate: <asp:DropDownList ID="StudentDropDown" runat="server" DataSourceID="StudentDataSource"
            datatextfield="name" DataValueField="dbuserid" AutoPostBack="true"></asp:DropDownList> <br />
        Version:<asp:DropDownList ID="VersionDropDown" runat="server" DataSourceID="VersionDataSource"
            datatextfield="version" DataValueField="version" AutoPostBack="true"></asp:DropDownList> 
        <br />
        <br />
        <h4>Edit Core Rating Assesments</h4>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="CoreRatingDataSource" DataKeyNames="coretopicid" InsertItemPosition="FirstItem">            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text='<%# Bind("CoreTopicName") %>'></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("knowledgec") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("knowledgep") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("knowledger") %>'></asp:TextBox>
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
                            DataTextField="Name" DataValueField="CoreTopicID"  SelectedValue='<%# Bind("CoreTopicID") %>'></asp:DropDownList>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("knowledgec") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("knowledgep") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("knowledger") %>'></asp:TextBox>
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
                        <asp:Label ID="CoreLabel" runat="server" Text='<%# Eval("coretopicname") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CLabel" runat="server" Text='<%# Eval("knowledgec") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("knowledgep") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("knowledger") %>'></asp:Label>
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
                                    <th runat="server">R Score</th>
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
    <br /><br />
        <h4>Edit Impact Type Assessments</h4>            
        <asp:ListView ID="ImpactListView" runat="server" DataSourceID="ImpactGridDataSource">            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text="New Ratings:"></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text='<%# Bind("impactS") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text='<%# Bind("impactP") %>'></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text='<%# Bind("impactL") %>'></asp:TextBox>
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
                        <asp:Label ID="CoreLabel" runat="server" Text="Current Ratings:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CLabel" runat="server" Text='<%# Eval("impactS") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PLabel" runat="server" Text='<%# Eval("impactP") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text='<%# Eval("impactL") %>'></asp:Label>
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
                                    <th runat="server"></th>
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


         <asp:ObjectDataSource ID="ImpactGridDataSource" runat="server" SelectMethod="getImpactGrid" UpdateMethod="updateImpactGrid" TypeName="ledeDB">
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="taskid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="StudentDropDown" Name="userid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="VersionDropDown" Name="version" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
            <UpdateParameters>             
            </UpdateParameters>
        </asp:ObjectDataSource>       
                                   
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="SingleParagraph" ValidationGroup="ImpactGrid" />

        <asp:ObjectDataSource ID="CoreTopicDataSource" runat="server" SelectMethod="getCoreTopics" TypeName="ledeDB">
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="taskid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="StudentDropDown" Name="userid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
        </asp:ObjectDataSource>      
                
        <asp:ObjectDataSource ID="CoreRatingDataSource" runat="server" SelectMethod="getCoreRatings" TypeName="ledeDB" DeleteMethod="deleteCoreRating" InsertMethod="insertCoreRating" OldValuesParameterFormatString="original_{0}" UpdateMethod="updateCoreRating">  
            <DeleteParameters>
                <asp:Parameter Name="KnowledgeC" Type="String" />
                <asp:Parameter Name="KnowledgeP" Type="String" />
                <asp:Parameter Name="KnowledgeR" Type="String" />
                <asp:Parameter Name="CoreTopicID" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="KnowledgeC" Type="String" />
                <asp:Parameter Name="KnowledgeP" Type="String" />
                <asp:Parameter Name="KnowledgeR" Type="String" />
                <asp:Parameter Name="CoreTopicID" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="StudentDropDown" Name="userid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="VersionDropDown" Name="versid" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="TaskDropDown" Name="taskid" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>          
            <UpdateParameters>
                <asp:Parameter Name="KnowledgeC" Type="String" />
                <asp:Parameter Name="KnowledgeP" Type="String" />
                <asp:Parameter Name="KnowledgeR" Type="String" />               
            </UpdateParameters>
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="StudentDataSource" runat="server" SelectMethod="getUsers" TypeName="ledeDB" >
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="TaskDataSource" runat="server" TypeName="ledeDB" SelectMethod="getTasks">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="VersionDataSource" runat="server" TypeName="ledeDB" SelectMethod="getVersion">
            <SelectParameters>
                <asp:ControlParameter ControlID="TaskDropDown" Name="TaskID" PropertyName="SelectedValue" type="String"/>
                <asp:ControlParameter ControlID="StudentDropDown" Name="UserID" PropertyName="SelectedValue" type="String"/>
            </SelectParameters>
        </asp:ObjectDataSource>
</asp:Content>
