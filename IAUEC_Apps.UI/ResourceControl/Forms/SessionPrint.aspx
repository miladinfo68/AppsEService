<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionPrint.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.SessionPrint" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<!DOCTYPE html >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" 
            ToolbarAlignment="Center"
            ShowViewMode="false"
            ShowBookmarksButton="false"
            ShowPrevButton="false"
            ShowCurrentPage="False"
            ShowFirstButton="False"
            ShowLastButton="False"
            ShowNextButton="False"
            ShowExportToBmp="false"
            ShowExportToCsv="false"
            ShowExportToDbf="false"
            ShowExportToDif="false"
            ShowExportToDocument="false"
            ShowExportToExcel="false"
            ShowExportToExcel2007="false"
            ShowExportToExcelXml="false"
            ShowExportToGif="false"
            ShowExportToHtml="false"
            ShowExportToJpeg="False"
            ShowExportToPng="True"
            ShowExportToMetafile="false"
            ShowExportToText="false"
            ShowExportToPowerPoint="false"
            ShowExportToMht="false"
            ShowExportToTiff="false"
            ShowExportToWord2007="false"
            ShowExportToPcx="false"
            ShowExportToSvg="false"
            ShowExportToSvgz="false"
            ShowExportToXps="false"
            ShowExportToXml="false"
            ShowExportToRtf="false"
            ShowExportToOdt="false"
            ShowExportToOds="false"
            PdfEmbeddedFonts="True"
            />   
        
        <script type="text/javascript">
            document.getElementById('StiWebViewer1_Print_child_PrintWithPreview_currItem').style.display = 'none';
            document.getElementById('StiWebViewer1_Print_child_PrintWithoutPreview_currItem').style.display = 'none';
       
    </script>
    </form>

</body>
</html>


