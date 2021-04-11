<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadAttachment.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.UploadAttachment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script>
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
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
    <asp:Panel ID="pnl_Main" runat="server" BorderColor="Black" BorderStyle="Groove">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
        <table>
            <tr>
                <td>
                    آپلود پیوست سوالات آزمون:
                </td>
                <td>
                    <asp:FileUpload ID="fileuploader" runat="server" />

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btn_Save" runat="server" Text="ثبت" OnClick="btn_Save_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
