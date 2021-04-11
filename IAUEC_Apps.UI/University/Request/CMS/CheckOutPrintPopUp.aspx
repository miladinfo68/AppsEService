<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOutPrintPopUp.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutPrintPopUp" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
</head>
<body>
    <script>
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
            return oWindow;
        }

        function CloseModal(args) {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();
        }
    </script>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
                </asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
                </asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>

    <div>
         <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Center" ShowParametersButton="False" ShowViewMode="false" ShowBookmarksButton="false" ShowPrevButton="false" Visible="false" ShowCurrentPage="False" ShowFirstButton="False" ShowLastButton="False" ShowNextButton="False" ToolBarBackColor="WhiteSmoke" />
    </div>
    </div>
    </form>
</body>
</html>
