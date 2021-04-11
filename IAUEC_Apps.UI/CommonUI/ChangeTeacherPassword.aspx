<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeTeacherPassword.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.ChangeTeacherPassword" %>

<!DOCTYPE html>
<link href="css/bootstrap.min.css" rel="stylesheet" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="p" runat="server" DefaultButton="btn_Change">
            <table class="table-responsive" style="width: 100%" dir="ltr">
                <tr>
                    <td dir="rtl">
                        <asp:TextBox ID="txt_OldPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;">رمز عبور فعلی:&nbsp;</td>
                </tr>
                <tr>
                    <td dir="rtl">
                        <asp:TextBox ID="txt_NewPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;">رمز عبور جدید:</td>
                </tr>
                <tr>
                    <td dir="rtl">
                        <asp:TextBox ID="txt_ConfNewPass" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;">تکرار رمز عبور جدید:</td>
                </tr>
                <tr>
                    <td dir="rtl">
                        <asp:Button ID="btn_Change" CssClass="btn btn-success" Width="110px" runat="server" Font-Names="Tahoma" Font-Size="Small" OnClick="btn_Change_Click" Text="تغییر رمز عبور" />
                        <asp:Label ID="lbl_Message" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="#FF3300"></asp:Label>

                    </td>
                    <td dir="rtl" style="width: 20%; font-family: tahoma; font-size: small;"></td>
                </tr>
               
            </table>
        </asp:Panel>
    </form>
</body>
</html>
