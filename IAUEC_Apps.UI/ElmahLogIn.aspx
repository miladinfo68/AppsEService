<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElmahLogIn.aspx.cs" Inherits="IAUEC_Apps.UI.ElmahLogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/LoginElmah/css/normalize.min.css" rel="stylesheet" />
    <link href="Content/LoginElmah/css/style.css" rel="stylesheet" />
    <style>
        input {
            text-align: right;
        }

        img {
            display: block;
            margin: 0 auto;
            width: 12%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <img src="Content/LoginElmah/img/azad_university_2.png" />
            <h2 style="text-align: center; padding-top: 2%;">ورود به بخش مدیریت خطاها</h2>
            <fieldset>
                <input runat="server" id="UserName" type="text" placeholder="نام کاربری" />
                <input runat="server" id="PassWord" type="password" placeholder="رمز عبور " />
            </fieldset>
            <input runat="server" id="btnLogin" type="submit" value="ورود" style="text-align: center" onserverclick="btnLogin_OnServerClick" />
            <div class="utilities">
                <a href="CommonUI/LoginRequestCMS.aspx">ورود به بخش همکاران</a>
                <a href="CommonUI/login.aspx">ورود به بخش استاد و دانشجو &rarr;</a>
            </div>
        </div>
    </form>
</body>
</html>
