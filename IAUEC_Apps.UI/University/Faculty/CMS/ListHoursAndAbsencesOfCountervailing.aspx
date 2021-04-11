<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListHoursAndAbsencesOfCountervailing.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListHoursAndAbsencesOfCountervailing" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server"><title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link rel="stylesheet" type="text/css" media="all" href="../../Theme/css/theme.css" title="Aqua" />
<%--    <script src="../../Theme/js/calendar-setup.js"></script>
    <script src="../../Theme/js/calendar.js"></script>
    <script src="../../Theme/js/jalali.js"></script>
    <script src="../../Theme/js/lang/calendar-fa.js"></script>--%>
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <telerik:RadCodeBlock runat="server" ID="code">
    <script>

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
        }
    </script></telerik:RadCodeBlock>
     <div class="Report-Area" dir="rtl">
                 <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="txt_CodeOstad" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>                 
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>
    <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-2">  
               <asp:Label ID="lbl_Term" Text="ترم :" runat="server"></asp:Label>    
   </div>
        <div class="col-md-3">  <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="128px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
    </div>
           <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-2"> <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" ></asp:Label> </div>
               <div class="col-md-3">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="128px" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel>
               </div>
           </div>
            <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-2">  <asp:Label ID="lbl_GroupOstad" runat="server" Text="گروه  :"></asp:Label></div>
                <div class="col-md-3">   <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                                 <asp:DropDownList ID="ddl_GroupOstad" runat="server" width="128px" OnSelectedIndexChanged="ddl_GroupOstad_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_GroupOstad" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
            </div>
               <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-2">  <asp:Label ID="lbl_CodeOstad" Text="کداستاد :" runat="server"></asp:Label></div>
         <div class="col-md-1"> <asp:TextBox ID="txt_CodeOstad" runat="server" Width="100%" CssClass="form-control" ></asp:TextBox></div>
               <div class="col-md-2"> <asp:Button ID="btn_Select" runat="server" Text="انتخاب کنید" OnClick="btn_Select_Click" CssClass="btn btn-success" /></div>
               </div>

   <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1"> <asp:Label ID="lbl_FromDate" runat="server" Text="از تاریخ :"></asp:Label></div>
                <div class="col-md-2"> 
                 <%--<asp:TextBox ID="txt_FromDate" runat="server" text="  /  /  " MaxLength="8" Width="100px" CssClass="form-control"></asp:TextBox>--%>
              <pdc:PersianDateTextBox ID="txt_FromDate" runat="server" DefaultDate="13  /  /  "
               IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>
                </div>
       </div>
     <div class="row">
     <div class="col-md-3"></div>
                 <div class="col-md-1">
                    <span style="color:red; font-size:small;">
                    <sup>*</sup>
                    </span>
                </div> 
                 <div class="col-md-1"> <asp:Label ID="lbl_ToDate" runat="server" Text="تا تاریخ :"></asp:Label></div>
                  <div class="col-md-1">  
                  <%--<asp:TextBox ID="txt_ToDate" runat="server" Text="  /  /  " MaxLength="8" Width="100px" CssClass="form-control"></asp:TextBox></div>--%>
              <pdc:PersianDateTextBox ID="txt_ToDate" runat="server" DefaultDate="13  /  /  "
               IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>
      </div>
         </div>
             <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
        <span style="color:red; font-size:small;">
        <sup></sup>
        </span>
        </div> 
      <div  class="col-md-1"> 
          <asp:Label ID="lbl_AzJobrani" runat="server" Text="از جبرانی :"></asp:Label></div>
                 <div  class="col-md-1">  
                     <%--<asp:TextBox ID="txt_AzJobrani" runat="server" text="  /  /  " MaxLength="8" Width="100px" CssClass="form-control"></asp:TextBox>--%>
              <pdc:PersianDateTextBox ID="txt_AzJobrani" runat="server" DefaultDate="13  /  /  "
               IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>
                 </div>
                 </div>
                <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-1">
                <span style="color:red; font-size:small;">
                <sup></sup>
                </span>
                </div> 
                 <div  class="col-md-1"> <asp:Label ID="lbl_ToJobrani" runat="server" Text="تا جبرانی :"></asp:Label></div>
                 <div  class="col-md-1">   
                 <%--<asp:TextBox ID="txt_ToJobrani" runat="server" Text="  /  /  " MaxLength="8" Width="100px" CssClass="form-control"></asp:TextBox>--%>
              <pdc:PersianDateTextBox ID="txt_ToJobrani" runat="server" DefaultDate="13  /  /  "
               IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>
                 </div>
             </div>
                <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-2">  <asp:Label ID="lbl_NumberAbsences" runat="server"  Text="تعداد غیبت بیش از :" ></asp:Label></div>
                      <div  class="col-md-2">    <asp:TextBox ID="txt_NumberAbsence" Width="50px" runat="server" CssClass="form-control"></asp:TextBox></div>
                </div>
  
        <br />
            <div class="row">
     <div class="col-md-3"></div>
                <div class="col-md-8"><asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                  <asp:RadioButton ID="rdb_YesAbsencesNoCountervailing" runat="server" Text="لیست اساتیدی که غیبت داشتند ولی جبرانی نداشتند" Checked="true" GroupName="AbsenceCountervailing" ValidationGroup="0"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_YesAbsencesNoCountervailing"/>
                             </Triggers>
                   </asp:UpdatePanel></div>
                
    
            </div>
            <div class="row">
     <div class="col-md-3"></div>
                   <div class="col-md-8"><asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                                  <asp:RadioButton ID="rdb_YesAbsenceYesCountervailing" runat="server" Text="لیست اساتیدی که غیبت و جبرانی داشتند" GroupName="AbsenceCountervailing" ValidationGroup="1" />
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_YesAbsenceYesCountervailing"/>
                             </Triggers>
                   </asp:UpdatePanel></div>
            </div>
            <div class="row">
     <div class="col-md-4"></div>
                <div class="col-md-3"><asp:Button ID="btn_ShowList" Width="100px" runat="server" Text="نمایش لیست" onclick="btn_ShowList_Click" CssClass="btn btn-success" /></div></div>
           <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedanesh" HeaderText="نام دانشکده" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="date_ghaebat" HeaderText="تاریخ غیبت" />
                    <asp:BoundField DataField="nghebat" HeaderText="تعداد غیبت" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="sharh" HeaderText="شرح" />
               </Columns>
             </asp:GridView>
             </div>
          <div id="Div2" runat="server">
             <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namedanesh" HeaderText="نام دانشکده" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="date_ghaebat" HeaderText="تاریخ غیبت" />
                    <asp:BoundField DataField="nghebat" HeaderText="تعداد غیبت" />
                    <asp:BoundField DataField="date_jobrani" HeaderText="تاریخ جبرانی" />
                    <asp:BoundField DataField="idroz" HeaderText="روز جبرانی" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="sharh" HeaderText="شرح" />
               </Columns>
             </asp:GridView>
             </div>
              <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" /><uc1:AccessControl ID="AccessControl1" runat="server" />
 </div>
</asp:Content>
