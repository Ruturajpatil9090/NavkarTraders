<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login1.aspx.cs" Inherits="login1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login</title>
    <link href="Sugar/CSS/applelogin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var username = document.getElementById('username').value;
            var password = document.getElementById('password').value;
            if (username == "User Name" || password == "Password") {
                //document.getElementById('username').focus();
                alert('Please Enter User Name Or Password!');
                document.getElementById('username').focus();
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body oncontextmenu="return false">
    <form id="login" runat="server">
    <h1 id="ff-proof" class="ribbon">
        Accoweb Login &nbsp;&nbsp;</h1>
    <!--<div class="apple">
        <div class="top">
            <span></span>
        </div>
        <div class="butt">
            <span></span>
        </div>
        <div class="bite">
        </div>
    </div>-->
    <fieldset id="inputs">
        <asp:TextBox ID="username" runat="server" type="text" onblur="if(this.value=='')this.value='User Name';"
            onfocus="if(this.value=='User Name')this.value='';if(password.value!='Password')password.type='password';"
            value="User Name" />
        <asp:TextBox ID="password" runat="server" type="text" onblur="if(this.value=='')this.value='Password';if(this.value=='Password')this.type='text';if(this.value!='Password')this.type='password';"
            onfocus="if(this.value=='Password')this.value='';if(this.value!='Password')this.type='password';"
            value="Password" />
    </fieldset>
    <fieldset id="actions">
        <asp:Button ID="submit" runat="server" Text="Login" OnClientClick="javascript:return validate();"
            OnClick="Login" />
        <p class="option">
            Developed By,&nbsp;&nbsp;|<a href="#">Lata Software Consultancy</a></p>
    </fieldset>
    </form>
</body>
</html>
