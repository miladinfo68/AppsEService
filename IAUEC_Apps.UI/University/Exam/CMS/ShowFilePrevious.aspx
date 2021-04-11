<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowFilePrevious.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ShowFilePrevious" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        body {
            margin: 0px;
        }

        .lblAlert {
            font-family: Arial;
            font-size: 40px;
            color: red;
            direction: rtl;
            text-align: center;
            /* padding: 20px 10px 20px 100px; */
            margin: 100px auto;
        }

        .embed {
            visibility: hidden;
        }
    </style>
</head>

<script language="JavaScript">
    
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel runat="server" ID="pnlEmbed">
                <object style="width: 100%; height: 900px">
                    <embed src="<%= EmbedSrc %>" width="100%" height="900px" />
                </object>
            </asp:Panel>
            <div class="lblAlert">
                <asp:Label Text="فایلی جهت نمایش وجود ندارد" runat="server" Visible="false" ID="lblAlert" />
            </div>
        </div>
    </form>
</body>
</html>
