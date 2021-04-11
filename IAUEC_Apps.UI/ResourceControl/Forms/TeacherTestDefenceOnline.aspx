<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageContactOS.Master" AutoEventWireup="true" CodeBehind="TeacherTestDefenceOnline.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.TeacherTestDefenceOnline" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-info " role="alert">
  <p class="alert-heading  " style="font-size:20px">جلسات دفاع آنلاین آزمایشی</p>
  <p style="font-size:15px">استاد گرامی</p>
        <p style="font-size:15px">
با توجه به برگزاری جلسات دفاع از پایان نامه بصورت کاملا" آنلاین، لطفا" جهت کسب آمادگی لازم و تست تجهیزات فنی خود، ضمن مطالعه راهنما و نصب نرم افزارهای مورد نیاز واقع در آدرس<a style="color:white;font-size:15px" href="http://iauec.ac.ir/support"> http://iauec.ac.ir/support </a>با مراجعه به یکی از لینک های زیر ، در جلسه دفاع آنلاین آزمایشی شرکت نمایید.
            </p>

        <br />
        <p style="font-size:15px">
زمان برگزاری جلسات دفاع آنلاین آزمایشی:
            </p>
        <p>
شنبه تا چهارشنبه از ساعت ۸ الی ۱۶.</p>
  <hr/>
  <p class="mb-0">
                                             <asp:Button ID="btnTesti" runat="server" Text=" جلسه تستی1 " OnClick="btnTesti_Click"   class="btn btn-danger"  />
                                                
                         <asp:Button ID="btnTesti2" runat="server" Text="جلسه تستی2  "  OnClick="btnTesti2_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti3" runat="server" Text="جلسه تستی3  "   OnClick="btnTesti3_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti4" runat="server" Text="جلسه تستی4  "  OnClick="btnTesti4_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti5" runat="server" Text="جلسه تستی5  "   OnClick="btnTesti5_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti6" runat="server" Text="جلسه تستی6  " Visible="false"  OnClick="btnTesti6_Click"   class="btn btn-danger"  />
  </p>
</div>
</asp:Content>
