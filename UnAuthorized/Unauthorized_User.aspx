<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unauthorized_User.aspx.cs"
    Inherits="Unauthorized_User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span>
            <asp:Image runat="server" ID="img" ImageUrl="~/Images/oops.png" Height="200px" Width="200px" /></span>
        <h1>
            SORRY! YOU ARE NOT AUTHORIZED TO ACCESS THIS PAGE</h1>
        <p>
            PLEASE CONTACT YOUR ADMINISTRATOR</p>
        <strong><a href="../Sugar/pgeHome.aspx">Go to Home Page</a></strong>
    </div>
    </form>
</body>
</html>
