<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertRejectDescription.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.InsertRejectDescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../ResourceControl/Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        .fff {
            width: 90%;
        }
        .bbb {
            float: left;
            margin-left: 10%;
        }
    </style>
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
    <form id="form1" runat="server">
        <div class="row">
            <div>
                <div dir="rtl" class="form-inline" style="width: 100%;float: right;margin: 5%">
                    <h4>دلیل رد درخواست:</h4>
<%--                    <asp:Label ID="lbl_Rad" Text="" runat="server" CssClass="form-label"></asp:Label>--%>
                    <asp:TextBox ID="txt_Rad" runat="server" CssClass="form-control fff"></asp:TextBox>
                    <br />
                    <asp:Button ID="btn_RejectRequest" runat="server" CssClass="btn btn-info bbb" Text="رد درخواست" OnClick="btn_Reject_Click" />

                </div>

            </div>
        </div>
    </form>
</body>
</html>
