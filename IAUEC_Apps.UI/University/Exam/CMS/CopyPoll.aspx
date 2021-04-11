<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyPoll.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.CopyPoll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <link href="../../Theme/css/bootstrap-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/style-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/responsive-rtl.css" rel="stylesheet" />
    <link href="../../Theme/css/style.css" rel="stylesheet" />
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
            
        }
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }
        function Close() {
            GetRadWindow().close();
        }
        function makeSafe() {
            document.getElementById('txtDescription').value =
                document.getElementById('txtDescription').value.replace(/</g, '&lt;').replace(/>/g, '&gt;');
            return true;
        }
    </script>
    <style>
        .copyPollWrapper{margin-top: 55px;}
        .copyPollWrapper .alert-danger{margin-top: -45px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="copyPollWrapper container">
        <asp:Panel runat="server" ID="pnlError" CssClass="row" Visible="false">
            <div class="col-sm-12 alert alert-danger">
                <asp:Label runat="server" ID="lblError"></asp:Label>
            </div>
        </asp:Panel>
        <div class="row">
            <div class="col-sm-3">
                <span>ایجاد کپی برای ترم:</span>
            </div>
            <div class="col-sm-3">
                <asp:DropDownList runat="server" ID="ddlTerms" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-sm-2">
                <asp:Button runat="server" ID="btnCopy" CssClass="btn btn-success" Text="ذخیره" OnClick="btnCopy_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
