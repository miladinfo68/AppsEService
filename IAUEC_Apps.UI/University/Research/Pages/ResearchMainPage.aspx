<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResearchMainPage.aspx.cs" Inherits="IAUEC_Apps.UI.University.Research.Pages.ResearchMainPage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "../../../CommonUI/IntroPage.aspx"
        }
    </script>

    <%--<script type="text/javascript">

 
    function PostBack() {
        if (document.forms["form1"].onsubmit()) {//this check triggers the validations
            document.forms["form1"].submit();
        }
        //}
    }
    //]]> 
 	</script>--%>


    <%--<script  type="text/javascript">
        function submitform() {
            if (document.forms["form1"].onsubmit()) {//this check triggers the validations
                document.forms["form1"].submit();
            }
        }
</script>--%>
    <script type="text/javascript">
        function submitform() {

            document.forms["form1"].submit();
        }
    </script>

    <form id="form1" method="post" runat="server">


        <input type="hidden" runat="server" id="userCode" name="userCode" style="width: 200px;" />
        <input type="hidden" runat="server" id="e_Code" name="e_Code" style="width: 200px;" />
        <input type="hidden" runat="server" id="e_Status" name="e_Status" style="width: 200px;" />
        <%--<asp:TextBox ID="txt_stcode" runat="server"></asp:TextBox>--%>
        <%--<input type="text" name="stcode" runat="server" id="stcode" />--%>
        <%--<input type="text" id="txtFirstName" runat="server" name="txtFirstName"/>--%>
        <%--<button type="submit"  id="btnSend" value="stcode"   runat="server">Send</button>--%>
        <%--<input runat="server" id="btn_submit" type="submit" value="Submit"/>--%>


        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableViewState="false">
        </telerik:RadScriptManager>
        <br />

        <telerik:RadWindowManager ID="rwm_Message" runat="server" EnableViewState="false"></telerik:RadWindowManager>
        <div>
        </div>
    </form>
</body>
</html>
