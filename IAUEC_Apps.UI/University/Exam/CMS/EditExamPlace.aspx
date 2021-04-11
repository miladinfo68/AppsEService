<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditExamPlace.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.EditExamPlace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script>
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }


        function CloseModal(args) {

            setTimeout(function () {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();

            }, 0);

        }

    </script>
    <div dir="rtl">

        <asp:Panel runat="server" ID="pnl_Main" BorderColor="Black" BorderStyle="Groove">


            <form id="form1" runat="server">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
                <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
                <div>
                    
                    <table>
                        <tr>
                            <td>نام شهر
                            </td>
                            <td>
                                <asp:TextBox ID="txt_City" runat="server" Height="48px" ForeColor="Black"></asp:TextBox>
                            </td>
                            <td>آدرس
                            </td>
                            <td>
                                <asp:TextBox ID="txt_Address" runat="server" TextMode="MultiLine" Height="54px" Width="393px" ForeColor="Black"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                فعال
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="drpIsActive" CssClass="dropdown form-control"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btn_Edit" runat="server" BackColor="#9B59B6" Width="100px" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" Text="ثبت" OnClick="btn_Edit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </form>
        </asp:Panel>
    </div>
</body>
</html>
