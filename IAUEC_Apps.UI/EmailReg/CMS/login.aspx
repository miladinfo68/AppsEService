<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.login" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>
<html lang="en">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>دانشگاه آزاد اسلامی واحد الکترونیکی</title>

<%--<link href='http://fonts.googleapis.com/css?family=Roboto:400,300,500,700,900' rel='stylesheet' type='text/css' />
<link href='http://fonts.googleapis.com/css?family=Lato:300,400,700' rel='stylesheet' type='text/css' />--%>

<!-- Styles -->
<link rel="stylesheet" href="../css/font-awesome.css" type="text/css" /><!-- Font Awesome -->
<link rel="stylesheet" href="../css/bootstrap.css" type="text/css" /><!-- Bootstrap -->
<link rel="stylesheet" href="../css/style.css" type="text/css" /><!-- Style -->
<link rel="stylesheet" href="../css/responsive.css" type="text/css" /><!-- Responsive -->	
 <script src="../js/jquery1.js"></script>
    <script src="../js/jquery2.js"></script>
    <link href="../css/Style1.css" rel="stylesheet" />
    
    <script type="text/javascript">
        $(document).ready(function () {

            $("[name='UserName']").focusin(function () {
                $("[name='UserName']").tooltip('show');
            });


        });
    </script>

</head>
      
<body style="background-color:rgb(230,230,230)">
    	<div style="background-position: center 0px; background-image: url('../images/light.png'); background-repeat: no-repeat; height:600px">
	<div style="background-position: center 130px; background-image: url('../images/bg.png'); background-repeat: no-repeat; height:600px">
<div class="login-sec">
	<div class="login">
		<div class="login-form">
			<span><img src="../images/logo.png" alt="" /></span>
			<h5><strong></strong> </h5>
			<form  runat="server" id="form1">
				
				<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
				
				<fieldset class="textBoxLogin"><i class="fa fa-user"></i><asp:TextBox placeholder="نام کاربری" ID="UserName" autocomplete="off" runat="server" ForeColor="Black" Font-Names="B Titr" BorderStyle="None"></asp:TextBox></fieldset>
				<fieldset class="textBoxLogin"><asp:TextBox ID="password" runat="server"  placeholder="رمز عبور" ForeColor="Black" Font-Names="B Titr" TextMode="Password"></asp:TextBox><i class="fa fa-unlock-alt"></i></fieldset>
					
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="password" ErrorMessage="وارد کردن رمز عبور الزامی می باشد" ForeColor="Red" Font-Names="Tahoma" Font-Size="Small"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" ErrorMessage="وارد کردن نام کاربری الزامی می باشد" ForeColor="Red" Font-Names="Tahoma" Font-Size="Small"></asp:RequiredFieldValidator>
                  <div style="margin-left:30%;padding-top:5%">
                <asp:Button ID="Btn_Login" runat="server" Text="ورود"  CssClass="Orange" OnClick="Btn_Login_Click" />
    </div>
			<p id="errormsg" runat="server" visible="false" style="padding: 1%; background-color:rgba(255, 61, 61,0.2); border:1px solid red; border-radius:2px; margin-right: 2%; margin-left: 2%; color: #000000; text-align: center; margin-top: 10px; font-family: tahoma; font-size: small;">
                
                </p>
                
            </form>
	
		</div>
		<%--<ul class="reg-social-btns">
			<li><a href="#" title=""><i class="fa fa-facebook"></i></a></li>
			<li><a href="#" title=""><i class="fa fa-twitter"></i></a></li>
			<li><a href="#" title=""><i class="fa fa-github"></i></a></li>
			<li><a href="#" title=""><i class="fa fa-google-plus"></i></a></li>
		</ul>--%>
		
	</div>
</div><!-- Log in Sec -->	
        </div>
            </div>
</body>
</html>
