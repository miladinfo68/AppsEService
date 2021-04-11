<%@ Page Language="C#"  CodeBehind="HomePageUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.HomePageUI" %>
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
    


    <style type="text/css">
        .Orange {
            height: 26px;
            width: 36px;
        }
        .auto-style1 {
            width: 380px;
            height: 80px;
        }
        .auto-style2 {
            height: 20px;
            text-align: right;
        }
        .auto-style3 {
            height: 20px;
            text-align: left;
        }
        .auto-style4 {
            height: 20px;
            text-align: center;
        }
        .auto-style5 {
            height: 80px;
        }
    </style>

</head>
     
      
<body style="background-color:rgb(230,230,230)">
    	<div style="background-position: center 0px; background-image: url('../images/light.png'); background-repeat: no-repeat; height:611px">
	<div style="background-position: center 130px; background-image: url('../images/bg.png'); background-repeat: no-repeat; height:600px">
     
<div class="login-sec">
	<div class="login">
		<div class="login-form">
            	<form  runat="server" id="form1">
                <ul class="reg-social-btns" style="background-color: #FF9900">
			<li><a href="../../../EmailReg/Pages/CreateEmail.aspx" title="درخواست ایجاد ایمیل دانشگاهی" ><i class="fa fa-envelope-o"></i></a></li>
			<li><a href="RequestStudentCartsUI.aspx" title="درخواست پست کارت دانشجویی"><i class="fa fa-credit-card"></i></a></li>
			<li><a href="EditPersonalInformationUI.aspx" title="درخواست ویرایش اطلاعات فردی"><i class="fa fa-pencil-square-o"></i></a></li>
                    <li><a href="RequestGovahiUI.aspx" title="درخواست پست گواهی اشتفال به تحصیل"><i class="fa fa-file-text-o"></i></a></li>
		</ul>
			&nbsp;<table class="nav-justified">
                        <tr>
                            <td colspan="4" class="table-responsive">
                                <img alt="" class="table-responsive" src="../../Theme/images/logowelcome.png" /></td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_LastNamePrev" runat="server"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_LastName" runat="server" Font-Bold="True" Text=":نام خانوادگی"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_NamePrev" runat="server"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_Name" runat="server" Font-Bold="True" Text=":نام"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                            <td class="table-responsive">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_SalVorudPrev" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_SalVorod" runat="server" Font-Bold="True">:سال ورود</asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_ShomareDaneshjuPrev" runat="server"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_ShomareDaneshju" runat="server" Font-Bold="True" Text=":شماره دانشجویی"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table-responsive">&nbsp;</td>
                            <td class="table-responsive">&nbsp;</td>
                            <td class="table-responsive">&nbsp;</td>
                            <td class="table-responsive">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_MaghtaPrev" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_Maghta" runat="server" Font-Bold="True">:مقطع</asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_ReshtePrev" runat="server" Font-Bold="False"></asp:Label>
                            </td>
                            <td class="table-responsive">
                                <asp:Label ID="lbl_Reshte" runat="server" Font-Bold="True" Text=":نام رشته"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </form>
			</div>
		
		
	</div>
</div><!-- Log in Sec -->	
        </div>
            </div>
</body>
</html>
