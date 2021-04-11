<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListAllProf.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListAllProf" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="Report-Area" dir="rtl">
    <div class="row">
    <div id="Div3" runat="server">
             <asp:ImageButton ID="img_ExportToExcel3" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel3_Click"/>
            <asp:GridView ID="GridView3" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
<%--                    <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="idd_meli" HeaderText="کدملی" />
                    <asp:BoundField DataField="nsex" HeaderText="جنسیت" />
                    <asp:BoundField DataField="namemadrak" HeaderText="نام مدرک" />
                    <asp:BoundField DataField="namemartabeh" HeaderText="مرتبه" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="name_nahveh" HeaderText="نحوه همکاری" />
                    <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موظفی" />
                    <asp:BoundField DataField="sal_tav" HeaderText="سال تولد" />
                    <asp:BoundField DataField="name_mahal_tav" HeaderText="محل تولد" />
                    <asp:BoundField DataField="name_mahal_sodor" HeaderText="محل صدور" />
                    <asp:BoundField DataField="sal_madrak" HeaderText="سال مدرک" />
                    <asp:BoundField DataField="name_keshvar" HeaderText="کشور" />
                    <asp:BoundField DataField="nametypeuniversity" HeaderText="نام نوع دانشگاه" />
                    <asp:BoundField DataField="nameuniversity" HeaderText="نام دانشگاه" />
                    <asp:BoundField DataField="namegroup" HeaderText="گروه" />
                    <asp:BoundField DataField="date_est" HeaderText="تاریخ استخدام" />
                    <asp:BoundField DataField="type_est" HeaderText="نوع استخدام" />
                    <asp:BoundField DataField="name_type_est" HeaderText="نام استخدام" />
                    <asp:BoundField DataField="add_kar" HeaderText="آدرس محل کار" />
                    <asp:BoundField DataField="add_hom" HeaderText="آدرس منزل" />
                    <asp:BoundField DataField="tel_kar" HeaderText="تلفن محل کار" />
                    <asp:BoundField DataField="tel_hom" HeaderText="تلفن منزل" />
                    <asp:BoundField DataField="saat_tadris" HeaderText="ساعت تدریس" />
                    <asp:BoundField DataField="add_email" HeaderText="آدرس ایمیل" />
                    <asp:BoundField DataField="semat" HeaderText="سمت" />
                    <asp:BoundField DataField="siba" HeaderText="سیبا" />
                    <asp:BoundField DataField="mobile" HeaderText="موبایل" />
                    <asp:BoundField DataField="code_posti" HeaderText="کد پستی" />
                    <asp:BoundField DataField="payeh" HeaderText="پایه" />
                    <asp:BoundField DataField="IsEmailSend" HeaderText="آیا ایمیل ارسال شود" />
                    <asp:BoundField DataField="codebank" HeaderText="کد بانک" />
                    <asp:BoundField DataField="old_code" HeaderText="کد قدیمی" />
                    <asp:BoundField DataField="omomi" HeaderText="عمومی" />
                    <asp:BoundField DataField="elmi" HeaderText="علمی" />
                    <asp:BoundField DataField="nezam" HeaderText="نظام" />
                    <asp:BoundField DataField="sharh" HeaderText="شرح" />--%>


                    <%--<asp:BoundField DataField="کد استاد" HeaderText="کد استاد" />
                    <asp:BoundField DataField="نام" HeaderText="نام" />
                    <asp:BoundField DataField="نام خانوادگی" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="نام پدر" HeaderText="نام پدر" />
                    <asp:BoundField DataField="شماره شناسنامه" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="کد ملی" HeaderText="کدملی" />
                    <asp:BoundField DataField="جنسیت" HeaderText="جنسیت" />
                    <asp:BoundField DataField="نام مدرک" HeaderText="نام مدرک" />
                    <asp:BoundField DataField="نام مرتبه" HeaderText="مرتبه" />
                    <asp:BoundField DataField="رشته" HeaderText="رشته" />
                    <asp:BoundField DataField="نحوه همکاری" HeaderText="نحوه همکاری" />
                    <asp:BoundField DataField="ساعت موظفی" HeaderText="ساعت موظفی" />
                    <asp:BoundField DataField="سال تولد" HeaderText="سال تولد" />
                    <asp:BoundField DataField="محل تولد" HeaderText="محل تولد" />
                    <asp:BoundField DataField="محل صدور" HeaderText="محل صدور" />
                    <asp:BoundField DataField="سال مدرک" HeaderText="سال مدرک" />
                    <asp:BoundField DataField="کشور" HeaderText="کشور" />
                    <asp:BoundField DataField="نوع دانشگاه" HeaderText="نام نوع دانشگاه" />
                    <asp:BoundField DataField="نام دانشگاه" HeaderText="نام دانشگاه" />
                    <asp:BoundField DataField="نام گروه" HeaderText="گروه" />
                    <asp:BoundField DataField="تاریخ استخدام" HeaderText="تاریخ استخدام" />
                    <asp:BoundField DataField="نوع استخدام" HeaderText="نوع استخدام" />
                    <asp:BoundField DataField="نام نوع استخدام" HeaderText="نام استخدام" />
                    <asp:BoundField DataField="آدرس محل کار" HeaderText="آدرس محل کار" />
                    <asp:BoundField DataField="آدرس منزل" HeaderText="آدرس منزل" />
                    <asp:BoundField DataField="تلفن محل کار" HeaderText="تلفن محل کار" />
                    <asp:BoundField DataField="تلفن منزل" HeaderText="تلفن منزل" />
                    <asp:BoundField DataField="ساعت تدریس" HeaderText="ساعت تدریس" />
                    <asp:BoundField DataField="آدرس ایمیل" HeaderText="آدرس ایمیل" />
                    <asp:BoundField DataField="سمت" HeaderText="سمت" />
                    <asp:BoundField DataField="سیبا" HeaderText="سیبا" />
                    <asp:BoundField DataField="موبایل" HeaderText="موبایل" />
                    <asp:BoundField DataField="کد پستی" HeaderText="کد پستی" />
                    <asp:BoundField DataField="پایه" HeaderText="پایه" />
                    <asp:BoundField DataField="آیا ایمیل ارسال شده" HeaderText="آیا ایمیل ارسال شود" />
                    <asp:BoundField DataField="کد بانک" HeaderText="کد بانک" />
                    <asp:BoundField DataField="کد قدیمی" HeaderText="کد قدیمی" />
                    <asp:BoundField DataField="عمومی" HeaderText="عمومی" />
                    <asp:BoundField DataField="علمی" HeaderText="علمی" />
                    <asp:BoundField DataField="نظام" HeaderText="نظام" />
                    <asp:BoundField DataField="شرح" HeaderText="شرح" />--%>
                </Columns>
            </asp:GridView>
      </div>
        </div>
    </div>
</asp:Content>
