<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="InformationRation.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.InformationRation" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <h3 style="color:blue">
       گزارش دانشجویان دارای سهمیه
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="Report-Area" dir="rtl">
    <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
       <div class="col-md-2"><asp:Label ID="lbl_Term" runat="server" Text="ترم :" ></asp:Label></div>
       <div class="col-md-1">
               <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                </asp:UpdatePanel></div></div>

     <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
       <div class="col-md-2"><asp:Label ID="lbl_StatusStu" runat="server" Text="وضعیت دانشجو :" ></asp:Label></div>
       <div class="col-md-1">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_StatusStu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_StatusStu_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_StatusStu" EventName="SelectedIndexChanged" />
                             </Triggers>
                </asp:UpdatePanel></div></div>

     <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
       <div class="col-md-2"><asp:Label ID="lbl_Degree" runat="server" Text="مقطع :" ></asp:Label></div>
       <div class="col-md-1">
               <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Degree" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                             </Triggers>
                </asp:UpdatePanel></div></div>

     <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
       <div class="col-md-2"><asp:Label ID="lbl_Sex" runat="server" Text="جنسیت :" ></asp:Label></div>
       <div class="col-md-1">
               <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Sex" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Sex_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Sex" EventName="SelectedIndexChanged" />
                             </Triggers>
                </asp:UpdatePanel></div></div>


      <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-5">
            <asp:RadioButton ID="rdb_Sahmie" runat="server" Text="دانشجویان سهمیه دار که وضعیت عادی دارند و وضعیت اولین ترم آنها مرخصی است" AutoPostBack="true" GroupName="ReportStudents" ValidationGroup="7" />         
       </div>
    </div>

    <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-5">
            <asp:RadioButton ID="rdb_EntekhabvahedSahmie" runat="server" Text="دانشجویانی که دارای سهمیه هستند و اولین باری است که انتخاب واحد کرده اند" AutoPostBack="true" GroupName="ReportStudents" ValidationGroup="7" Checked="true"/>         
       </div>
    </div>

    <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-5">
            <asp:RadioButton ID="rdbPermitted" runat="server" Text="دانشجویانی که در ترم انتخابی شرایط استفاده از سهمیه را دارند" AutoPostBack="true" GroupName="ReportStudents" ValidationGroup="7" />         
       </div>
    </div>


      <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
        <div class="col-md-8"><asp:Button ID="btn_Show" Text="نمایش اطلاعات" runat="server" OnClick="btn_Show_Click" CssClass="btn btn-success"/>
        </div></div> 

      <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" onclick="img_ExportToExcel_Click" Visible="false"/>

    <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
        AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />

    <asp:ImageButton ID="imgExportToExcelPermitted" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
        AlternateText="ExcelML" OnClick="imgExportToExcelPermitted_Click" Visible="false" />



        <asp:GridView ID="grd_Show" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
 <asp:BoundField DataField="idd_meli" HeaderText="کد ملی" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="nesbat" HeaderText=" نسبت ایثارگری" />
                    <asp:BoundField DataField="darsad" HeaderText="درصد جانبازی" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="mahalSodor" HeaderText="محل صدور شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="code_rayane" HeaderText="شماره ایثارگری" />
                    <asp:BoundField DataField="Column1" HeaderText="تلفن همراه" />
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="magh" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField HeaderText="واحد اخذ شده ترم قبل" />
                    <asp:BoundField HeaderText="واحد گذرانده ترم قبل" />
                    <asp:BoundField HeaderText="معدل ترم" />
                    <asp:BoundField HeaderText="واحداخذ شده ترم جاری" />
                    <asp:BoundField DataField="dateenteghal" HeaderText="تاریخ انتقال" />
                    <asp:BoundField DataField="fix_caculterm" HeaderText="شهریه ثابت" />
                    <asp:BoundField DataField="mot_caculterm" HeaderText="شهریه متغیر" />
                    <asp:BoundField HeaderText="جمع شهریه" />
                    <asp:BoundField HeaderText="درصد شهریه" />
                    <asp:BoundField HeaderText="درصد پرداختی" />
                    <asp:BoundField HeaderText="شهریه تأیید شده دانشگاه" />
                    <asp:BoundField HeaderText="شهریه تأیید شده بنیاد" />
                </Columns>
       </asp:GridView>

       <asp:GridView ID="grd_Show2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <asp:BoundField DataField="idd_meli" HeaderText="کد ملی" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="nesbat" HeaderText=" نسبت ایثارگری" />
                    <asp:BoundField DataField="darsad" HeaderText="درصد جانبازی" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="mahalSodor" HeaderText="محل صدور شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="code_rayane" HeaderText="شماره ایثارگری" />
                    <asp:BoundField DataField="Column1" HeaderText="تلفن همراه" />
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="magh" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField HeaderText="واحد اخذ شده ترم قبل" />
                    <asp:BoundField HeaderText="واحد گذرانده ترم قبل" />
                    <asp:BoundField HeaderText="معدل ترم" />
                    <asp:BoundField DataField="ent_jary" HeaderText="واحداخذ شده ترم جاری" />
                    <asp:BoundField DataField="dateenteghal" HeaderText="تاریخ انتقال" />
                    <asp:BoundField DataField="fix_caculterm" HeaderText="شهریه ثابت" />
                    <asp:BoundField DataField="mot_caculterm" HeaderText="شهریه متغیر" />
                    <asp:BoundField HeaderText="جمع شهریه" />
                    <asp:BoundField HeaderText="درصد شهریه" />
                    <asp:BoundField HeaderText="درصد پرداختی" />
                    <asp:BoundField HeaderText="شهریه تأیید شده دانشگاه" />
                    <asp:BoundField HeaderText="شهریه تأیید شده بنیاد" />
                </Columns>
       </asp:GridView>

    <asp:GridView ID="grdPermitted" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <asp:BoundField DataField="idd_meli" HeaderText="کد ملی" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="nesbat" HeaderText=" نسبت ایثارگری" />
                    <asp:BoundField DataField="darsad" HeaderText="درصد جانبازی" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="mahalSodor" HeaderText="محل صدور شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="code_rayane" HeaderText="شماره ایثارگری" />
                    <asp:BoundField DataField="mobile" HeaderText="تلفن همراه" />
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="magh" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField HeaderText="واحد اخذ شده ترم قبل" />
                    <asp:BoundField HeaderText="واحد گذرانده ترم قبل" />
                    <asp:BoundField HeaderText="معدل ترم" />
                    <asp:BoundField DataField="ent_jary" HeaderText="واحداخذ شده ترم جاری" />
                    <asp:BoundField DataField="dateenteghal" HeaderText="تاریخ انتقال" />
                    <asp:BoundField DataField="fix_caculterm" HeaderText="شهریه ثابت" />
                    <asp:BoundField DataField="mot_caculterm" HeaderText="شهریه متغیر" />
                    <asp:BoundField HeaderText="جمع شهریه" />
                    <asp:BoundField HeaderText="درصد شهریه" />
                    <asp:BoundField HeaderText="درصد پرداختی" />
                    <asp:BoundField HeaderText="شهریه تأیید شده دانشگاه" />
                    <asp:BoundField HeaderText="شهریه تأیید شده بنیاد" />
                    <asp:BoundField DataField="LastTermCondition" HeaderText="مشروطی در ترم قبل" />
                </Columns>
       </asp:GridView>

      <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
       <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="1300px" Width="1050px" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" 
       ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true"/><uc1:AccessControl ID="AccessControl1" runat="server" />
   </div>
</asp:Content>
