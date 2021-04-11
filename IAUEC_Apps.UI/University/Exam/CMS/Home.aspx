<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    <div id="content-section" style="text-align:center">
      
            <asp:HyperLink ID="LNK_ClassRange" runat="server" NavigateUrl="~/University/Exam/CMS/classlistUI.aspx" Font-Names="tahoma" Font-Size="Small" Text="تخصیص بازه"></asp:HyperLink>
        <br /><br /><br />
                <asp:HyperLink ID="LNK_Chair" runat="server" NavigateUrl="~/University/Exam/CMS/ClassSeatSpecifyUI.aspx" Font-Names="tahoma" Font-Size="Small" Text="تخصیص صندلی"></asp:HyperLink>
        <br /><br /><br />
    <asp:HyperLink ID="LNK_ExamplaceSans" runat="server" NavigateUrl="~/University/Exam/CMS/ExamPlaceListSans.aspx" Font-Names="tahoma" Font-Size="Small" Text="محل برگزاری سانس"></asp:HyperLink>
        <br /><br /><br />
          <asp:HyperLink ID="LNK_Examplace" runat="server" NavigateUrl="~/University/Exam/CMS/ExamPlaceList.aspx" Font-Names="tahoma" Font-Size="Small" Text="محل برگزاری"></asp:HyperLink>
        <br /><br /><br />
          <asp:HyperLink ID="LNK_ChairList" runat="server" NavigateUrl="~/University/Exam/CMS/ChairExamPresentList.aspx" Font-Names="tahoma" Font-Size="Small" Text="لیست صندلی"></asp:HyperLink>
        <br /><br /><br />
          <asp:HyperLink ID="LNK_Present" runat="server" NavigateUrl="~/University/Exam/CMS/ExamPresentList.aspx" Font-Names="tahoma" Font-Size="Small" Text="لیست صورتجلسه"></asp:HyperLink>
              <br /><br /><br />
          <asp:HyperLink ID="LNK_CityExam" runat="server" NavigateUrl="~/University/Exam/CMS/ChangeCityExam.aspx" Font-Names="tahoma" Font-Size="Small" Text="تغییر محل امتحان"></asp:HyperLink>
     <br /><br /><br />
          <asp:HyperLink ID="LNK_Report" runat="server" NavigateUrl="~/University/Exam/CMS/Report.aspx" Font-Names="tahoma" Font-Size="Small" Text="گزارش نهایی"></asp:HyperLink>
       </div>
    </form>
</body>
</html>
