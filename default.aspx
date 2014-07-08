<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem NavigateUrl="~/AssignmentDownload.aspx" Text="Download" Value="Download"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/AssignmentRating.aspx" Text="Rating" Value="Rating"></asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/AssignmentUpload.aspx" Text="Upload" Value="Upload"></asp:MenuItem>
            </Items>
        </asp:Menu>
    
    </div>
    </form>
</body>
</html>
