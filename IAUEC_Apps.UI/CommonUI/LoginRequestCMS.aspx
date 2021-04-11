<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRequestCMS.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.LoginRequestCMS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html lang="en">
<head>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../University/Theme/css/font-awesome.css" type="text/css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../University/Theme/css/bootstrap.css" type="text/css" />
    <!-- Bootstrap -->
    <link rel="stylesheet" href="../University/Theme/css/style.css" type="text/css" />
    <!-- Style -->
    <link rel="stylesheet" href="../University/Theme/css/responsive.css" type="text/css" />
    <!-- Responsive -->
    <script src="../University/Theme/js/jquery1.js"></script>
    <script src="../University/Theme/js/jquery2.js"></script>
    <link href="../University/Theme/css/Style1.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>دانشگاه آزاد اسلامی واحد الکترونیکی</title>

    <%--<link href='http://fonts.googleapis.com/css?family=Roboto:400,300,500,700,900' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Lato:300,400,700' rel='stylesheet' type='text/css' />--%>

    <!-- Styles -->
    <link rel="stylesheet" href="../University/Theme/css/font-awesome.css" type="text/css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../University/Theme/css/bootstrap.css" type="text/css" />
    <!-- Bootstrap -->
    <link rel="stylesheet" href="../University/Theme/css/style.css" type="text/css" />
    <!-- Style -->
    <link rel="stylesheet" href="../University/Theme/css/responsive.css" type="text/css" />
    <!-- Responsive -->
    <script src="../University/Theme/js/jquery1.js"></script>
    <script src="../University/Theme/js/jquery2.js"></script>
    <link href="../University/Theme/css/Style1.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

            $("[name='UserName']").focusin(function () {
                $("[name='UserName']").tooltip('show');

            });


        });
    </script>
    <script type="text/javascript">
        function fillInvisibleTextBox(sender) {
            var invisibleTextBox = $get("<%=RadCaptcha1.ClientID %>_InvisibleTextBox");
            var invisibleTextBox3 = $get("<%=RadCaptcha1.ClientID %>_InvisibleTextBox");
            if (sender.checked) {
                invisibleTextBox.value = "Filled";
                invisibleTextBox3.value = "Filled";
            }
            else {
                invisibleTextBox.value = "";
                invisibleTextBox3.value = "";
            }
        }
    </script>
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "VerificationPersonalData.aspx";
        }

        function CallBackConfirm1(arg) {
            if (arg)
                window.location.href = "Login.aspx";
        }
    </script>
    <style>
        .versionLabel{font-size: 13px;
    font-family: 'B Nazanin';
    display: inline-block;
    margin-bottom: 10px; width:29%;text-align:left;}
    </style>

</head>


<%--<style type="text/css">
        .Orange {
            height: 26px;
            width: 36px;
        }
        .login {
            width: 513px;
            text-align: right;
            margin-left: 392px;
            margin-top: 133px;
        }
        .login-form {
            width: 431px;
            margin-left: 85px;
        }
.login-sec {
    background-repeat: repeat;
    background-size: 100% 100%;
    height: 100%;
    left: 0;
    position: relative;
    top: 0;
    width: 100%;
}
* {
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
}
  * {
    color: #000 !important;
    text-shadow: none !important;
    background: transparent !important;
    -webkit-box-shadow: none !important;
            box-shadow: none !important;
  }
  .textBoxLogin {
    background: none repeat scroll 0 0 rgba(255, 255, 255, 0.8);
    border: 3px solid rgba(220, 220, 220, 0.9);
    float: right;
    height: 50px;
    margin-bottom: 15px;
    padding: 0 2px;
    position: relative;
    width: 100%;
    text-align: right;
    direction: rtl;
    font-family:'B Titr'
}
.fa {
  display: inline-block;
  font: normal normal normal 14px/1 FontAwesome;
  font-size: inherit;
  text-rendering: auto;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
.Gray {
    background: #666666;
    color: #FFFFFF;
    border: medium none;
    -webkit-border-radius: 4px;
    -moz-border-radius: 4px;
    -ms-border-radius: 4px;
    -o-border-radius: 4px;
    border-radius: 4px;
    font-family: 'B Titr';
    font-size: 16px;
    padding: 10px 26px;
}
</style>--%>


<body style="background-color: rgb(230,230,230);">
    <div style="background-position: center 0px; background-image: url('../../../University/Theme/images/light.png'); background-repeat: no-repeat; height: 611px; text-align: center;">
        <div style="background-position: center 130px; background-image: url('../../../University/Theme/images/bg.png'); background-repeat: no-repeat; height: 600px">
            <div class="login-sec">
                <div class="login">
                    <div class="login-form">
                        <span>
                            <img src="../University/Theme/images/logo.png" alt="" /></span>
                        <h5>&nbsp;</h5>
                        <form runat="server" id="form1">
                            <%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">--%>

                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>

                            <fieldset class="textBoxLogin" style="min-width: 0;">
                                <i class="fa fa-user"></i>
                                <asp:TextBox placeholder="نام کاربری" ID="UserName" autocomplete="off" runat="server" ForeColor="Black" Font-Names="B Titr" BorderStyle="None"></asp:TextBox>
                            </fieldset>
                            <fieldset class="textBoxLogin" style="min-width: 0;">
                                <asp:TextBox ID="password" runat="server" placeholder="رمز عبور" ForeColor="Black" Font-Names="B Titr" TextMode="Password"></asp:TextBox><i class="fa fa-unlock-alt"></i>
                            </fieldset>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div style="margin-left: 30%; margin-bottom: 5%">
                                <telerik:RadCaptcha ID="RadCaptcha1" runat="server" CaptchaMaxTimeout="30"
                                    Font-Names="Tahoma" ForeColor="Red" CaptchaTextBoxLabel="" CaptchaImage-TextChars="Numbers">
                                </telerik:RadCaptcha>
                            </div>
                            <div>
                        <asp:Label runat="server" ID="lblVersion" CssClass="versionLabel"></asp:Label>
                            <div style="padding-top: 5%;display:inline-block; width:69%;">
                                <asp:Button ID="loginBtn" runat="server" Text="ورود" CssClass="Gray" OnClick="ClickedME" />
                            </div>
                                </div>
                            <p id="errormsg" runat="server" visible="false" style="padding: 1%; background-color: rgba(255, 61, 61,0.2); border: 1px solid red; border-radius: 2px; margin-right: 2%; margin-left: 2%; color: #000000; text-align: center; margin-top: 10px; font-family: tahoma; font-size: small;"></p>


                        </form>

                    </div>
                </div>
            </div>
            <!-- Log in Sec -->
        </div>
    </div>
</body>
</html>
