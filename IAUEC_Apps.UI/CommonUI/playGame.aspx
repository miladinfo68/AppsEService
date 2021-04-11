<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="playGame.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.playGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server" style="background-color:orange; height:100%">
        <asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
        <div class="container" style="width:700px;height:100%; background-color:orange">
            <div class="row">
                <div class="col-md-12">
                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="700px" height="700px" id="Yourfilename" align="">
                        <param name="movie" class="paramValue" value="">
                        <param name="quality" value="high">
                        <param name="bgcolor" value="#32CD32">
                        <embed src="" class="embedSrc" quality="high" bgcolor="#32CD32" width="700px" height="700px" name="Yourfilename" align="" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer"></embed>
                    </object>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function setSWFfile(swfFile) {
                var a = document.getElementsByClassName("paramValue");
                a[0].value = swfFile;
                var b = document.getElementsByClassName("embedSrc");
                b[0].src = swfFile
            };

        </script>
    </form>
</body>
</html>
