<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowExamSheet.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ShowExamSheet" %>

<%@ Register assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" namespace="Stimulsoft.Report.Web" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
    
    </div>
    </form>
</body>
</html>
