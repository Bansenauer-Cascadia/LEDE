<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="LEDEPortal.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ListView ID="ListView1" runat="server" ItemType="LEDEPortal.Models.localModels.TestUser"
        SelectMethod="ListView1_GetData">            
            <EditItemTemplate>
                <tr style="">                    
                    <td>
                        <asp:Label ID ="CoreLabel" runat="server" Text="<%# Item.FirstName %>"></asp:Label>                        
                    </td>                    
                    <td>
                        <asp:TextBox ID="RText" runat="server" Text="<%# Item.LastName %>"></asp:TextBox>
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
                        <asp:Label ID="CoreLabel" runat="server" Text="<%# Item.FirstName %>"></asp:Label>
                    </td>                    
                    <td>
                        <asp:Label ID="RLabel" runat="server" Text="<%# Item.LastName %>"></asp:Label>
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
    </div>
    </form>
</body>
</html>
