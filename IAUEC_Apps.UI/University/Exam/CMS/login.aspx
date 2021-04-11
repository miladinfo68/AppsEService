<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1 {
            height: 20px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 70%">
                <tr>
                    <td style="text-align: right">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_UserName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
                    </td>
                    <td dir="rtl" style="width: 10%; font-family: tahoma; font-size: small;">نام کاربری:</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Pass" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_Pass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td dir="rtl" style="width: 10%; font-family: tahoma; font-size: small;">رمز عبور:</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Button ID="btn_Login" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" OnClick="btnLogin_Click" Text="ورود" />
                    </td>
                    <td dir="rtl" style="font-family: tahoma; font-size: small;"></td>
                </tr>
                <tr>
                    <td class="auto-style1" style="text-align: right">
                        <asp:Label ID="lbl_Msg" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
                    <td dir="rtl" style="font-family: tahoma; font-size: small;"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
