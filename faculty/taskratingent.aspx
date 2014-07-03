<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskRatingEnt.aspx.cs" Inherits="LEDEPortal.Faculty.TaskRating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">'
    <h4>Rate a Task</h4>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="userID, taskID, version, coretopicid, coretopicname" 
         SelectMethod="ListView1_GetData" ItemType="LEDEPortal.Models.coreGridView" InsertItemPosition="FirstItem">            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text="<%# Item.coretopicname %>"></asp:Label>                        
                    </td>
                    <td>
                        <asp:TextBox ID="CText" runat="server" Text="<%# Item.knowledgec %>"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="PText" runat="server" Text="<%# Item.knowledgep %>"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text="<%# Item.knowledger %>"></asp:TextBox>
                    </td> 
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>                     
                </tr>
            </EditItemTemplate>
         <InsertItemTemplate>
                <tr style="">                   
                    <td>                       
                        <asp:DropDownList ID="CoreDropDown" runat="server" SelectMethod="getCoreTopics" 
                            DataTextField="CoreTopicDesc" DataValueField="CoreTopicID"></asp:DropDownList>                        
                    </td>
                    <td>
                        <asp:TextBox ID="InsertC" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="InsertP" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="InsertR" runat="server"></asp:TextBox>
                    </td> 
                     <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
                </tr>
            </InsertItemTemplate>
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
                        <asp:Label ID="CoreLabel" runat="server" Text="<%# Item.coretopicname %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CLabel" runat="server" Text="<%# Item.knowledgec %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PLabel" runat="server"  Text="<%# Item.knowledgep %>"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text="<%# Item.knowledger %>"></asp:Label>
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
</asp:Content>
