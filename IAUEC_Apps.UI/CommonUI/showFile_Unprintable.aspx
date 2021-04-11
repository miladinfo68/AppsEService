<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showFile_Unprintable.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.showFile_Unprintable" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style>
        html, body, form, .pageWrapper {
            height: 100%;
            overflow: hidden;
        }

        #pnlEmbed {
            height: 100%;
        }

        body {
            -webkit-user-select: none; /* Chrome all / Safari all */
            -moz-user-select: none; /* Firefox all */
            -ms-user-select: none; /* IE 10+ */
            -o-user-select: none;
            user-select: none;
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

        .embededObject {
            width: 100%;
            height: 100%;
        }

        #pnlEmbed {
            position: relative;
        }
        .pageCover{position: absolute; left: 0; right:19px; top:0; bottom:0; margin: auto;}
    </style>

</head>
<body oncontextmenu="return false;" onmousedown="return false;">
    <form id="form1" runat="server">
        <div class="pageWrapper">
            <asp:Panel runat="server" ID="pnlEmbed">
                <div class="pageCover"></div>
                <object data="<%= EmbedSrc %>" type='<%=Session["contentTypeUnprintable"].ToString() %>' class="embededObject" id="embededObject">
                    <%--<embed src="<%= EmbedSrc %>" width="100%" height="900px" />--%>
                </object>
            </asp:Panel>
            <div class="lblAlert">
                <asp:Label Text="فایلی جهت نمایش وجود ندارد" runat="server" Visible="false" ID="lblAlert" />
            </div>
        </div>
    </form>

</body>
</html>
