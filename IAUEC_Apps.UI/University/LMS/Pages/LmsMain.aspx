<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LmsMain.aspx.cs" Inherits="IAUEC_Apps.UI.University.LMS.LmsMain" %>

<!DOCTYPE html>
<meta charset="utf-8">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
   
    <script type="text/javascript">
        function submitform() {
            document.forms["form1"].submit();
        }

</script>
   
    <form id="form1" runat="server"  method="post"  >
   <%--  <form id="form1" runat="server"  method="post"  action="../../../EmailReg/Pages/test.aspx">--%>
         <input type="hidden" runat="server"  id="UserName"  name="UserName"   tabindex="1"  class="text"/>
         <input type="hidden" runat="server"  id="Password"  name="Password"  tabindex="2"  />
         <input type="hidden" runat="server"  name="LogStatus" id="LogStatus"  tabindex="3" />
         </form>
    <div>
    
    </div>
   
</body>
</html>
