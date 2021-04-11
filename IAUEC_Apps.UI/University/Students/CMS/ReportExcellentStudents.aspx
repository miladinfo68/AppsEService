<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ReportExcellentStudents.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.ReportExcellentStudents" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
<title> <asp:Literal ID="t" runat="server"></asp:Literal></title>

   <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
        <h3 style="color:blue">
       گزارش دانشجویان ممتاز فارغ التحصیل
    </h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="Report-Area" dir="rtl">

 <div class="row">
      <div class="col-md-3"></div>
            <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
       <div class="col-md-2">
           <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" ></asp:Label>
       </div>
     <div class="col-md-1">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
             <ContentTemplate>
                 <asp:DropDownList ID="ddl_Daneshkade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel>
     </div></div>

     <div class="row">
      <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
       <div class="col-md-2">
           <asp:Label ID="lbl_Field" runat="server" Text="رشته تحصیلی :" ></asp:Label>
       </div>
     <div class="col-md-1">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
             <ContentTemplate>
                 <asp:DropDownList ID="ddl_Field" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel>
     </div></div>

    <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
      <div class="col-md-2">
          <asp:Label ID="lbl_Degree" runat="server" Text="مقطع تحصیلی :" ></asp:Label>
       </div>
     <div class="col-md-1">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
             <ContentTemplate>
                 <asp:DropDownList ID="ddl_Degree" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel>
     </div></div>


    <div class="row">
     <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
      <div class="col-md-2">
          <asp:Label ID="lbl_Education" runat="server" Text="سیستم آموزشی :" ></asp:Label>
       </div>
     <div class="col-md-1">
          <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
             <ContentTemplate>
                 <asp:DropDownList ID="ddl_Education" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Education_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Education" EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel>
     </div></div>

    <div class="row">
     <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
      <div class="col-md-2">
          <asp:Label ID="lbl_Sex" runat="server" Text="جنسیت :" ></asp:Label>
       </div>
     <div class="col-md-1">
          <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
             <ContentTemplate>
                 <asp:DropDownList ID="ddl_Sex" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Sex_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Sex" EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel>
     </div></div>

        <div class="row">
     <div class="col-md-3"></div>
       <div class="col-md-1">
           <span style="color:red; font-size:small;">
           <sup></sup>
           </span>
      </div>
      <div class="col-md-2">
          <asp:Label ID="lbl_VaziatStu" runat="server" Text="وضعیت دانشجو :" ></asp:Label>
       </div>
     <div class="col-md-1">
          <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
             <ContentTemplate>
                 <asp:DropDownList ID="ddl_VaziatStu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_VaziatStu_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_VaziatStu" EventName="SelectedIndexChanged" />
             </Triggers>
           </asp:UpdatePanel>
     </div></div>

     <div class="row">
       <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div>
        <div class="col-md-2"><asp:Label ID="lbl_SalVorod" runat="server" Text="سال ورود :" Width="150px" ></asp:Label></div>
        <div class="col-md-1"><asp:TextBox ID="txt_SalVorod" runat="server" MaxLength="2" CssClass="form-control" Width="50px"></asp:TextBox></div>
     </div>

    <div class="row">
       <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div>
        <div class="col-md-2"><asp:Label ID="lbl_NimsalVorod" runat="server" Text="ورودی نیمسال :" Width="150px" ></asp:Label></div>
        <div class="col-md-1"><asp:TextBox ID="txt_NimsalVorod" runat="server" MaxLength="1" CssClass="form-control" Width="50px"></asp:TextBox></div>
     </div>

       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
        <div class="col-md-2"><asp:Label ID="lbl_FromDateFeraghat" runat="server" Text="از تاریخ فارغ التحصیل :" Width="150px" ></asp:Label></div>
        <div class="col-md-1">
         <pdc:PersianDateTextBox ID="txt_FromDateFeraghat" runat="server" DefaultDate="13  /  /  "
        IconUrl ="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px"></pdc:PersianDateTextBox>
            <%--<asp:TextBox ID="txt_FromDateFeraghat" runat="server"></asp:TextBox>--%>
        </div>
     </div>

       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
        <div class="col-md-2"><asp:Label ID="lbl_TODateFeraghat" runat="server" Text="تا تاریخ فارغ التحصیل :" Width="150px" ></asp:Label></div>
        <div class="col-md-1">
        <pdc:PersianDateTextBox ID="txt_ToDateFeraghat" runat="server" DefaultDate="13  /  /  "
        IconUrl ="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px"></pdc:PersianDateTextBox>
            <%--<asp:TextBox ID="txt_ToDateFeraghat" runat="server"></asp:TextBox>--%>
        </div>
     </div>

      <div class="row">
       <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
        <div class="col-md-8">
            <asp:Button ID="btn_ShowInfo" Text="نمایش اطلاعات" runat="server" OnClick="btn_ShowInfo_Click" CssClass="btn btn-success"/></div></div> 
     
     <telerik:RadWindowManager ID="rwd" runat="server" ></telerik:RadWindowManager>

     <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false" />

     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
         
    <div id="Div4" runat="server">
            <asp:GridView ID="GridView" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی و نام" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="avrg_kol" HeaderText="معدل کل" />
                    <asp:BoundField DataField="Date" HeaderText="تاریخ"  />
                </Columns>
            </asp:GridView>
      </div>
    <asp:Label ID="lbl_FromDateFeraghat1" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_ToDateFeraghat1" runat="server" Visible="false"></asp:Label>
</div>
</asp:Content>
