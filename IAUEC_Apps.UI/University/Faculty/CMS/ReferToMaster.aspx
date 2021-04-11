<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ReferToMaster.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ReferToMaster" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ReferToMaster.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ReferToMaster" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server"><title><asp:Literal ID="t" runat="server"></asp:Literal></title>
<%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
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
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-1" >
            <asp:Label ID="lbl_Term" runat="server" Text="ترم :" Width="100px"></asp:Label></div>
            <div class="col-md-2">
              <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                      <ContentTemplate>
                      <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                      </ContentTemplate>
                             <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
              </asp:UpdatePanel></div></div>
            
    <div class="row">
     <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-1" >
                 <asp:Label ID="lbl_CodeOstad" Text="کد استاد :" runat="server" Width="100px"></asp:Label></div>
      <div class="col-md-2">
                 <asp:TextBox ID="txt_CodeOstad" runat="server" CssClass="form-control"></asp:TextBox>
     </div>
         <div class="col-md-1">
                 <asp:Button ID="btn_Select" runat="server"  Text="انتخاب کنید" OnClick="btn_Select_Click" CssClass="btn btn-success"/></div>
</div>
      <div class="row">
      <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1" ><asp:Label ID="lbl_FromDate" runat="server" Text="از تاریخ :" Width="100px"></asp:Label></div>
      <div class="col-md-2">
        <%--<asp:TextBox ID="txt_FromDate" runat="server" Text="  /  /  " MaxLength="8" AutoPostBack="true" CssClass="form-control"></asp:TextBox></div>--%>
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
       <div class="col-md-1">
      <asp:Label ID="lbl_ToDate" runat="server" text="تا تاریخ :" Width="100px"></asp:Label></div>
      <div class="col-md-2">
      <%--<asp:TextBox ID="txt_ToDate" runat="server" MaxLength="8" Text="  /  /  " Width="120px" AutoPostBack="true" CssClass="form-control"></asp:TextBox>--%>
     <pdc:PersianDateTextBox ID="txt_ToDate" runat="server" DefaultDate="13  /  /  "
       IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>
      </div></div>

       <div class="row">
      <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">    
                   <asp:CheckBox ID="chk_FineCourse" runat="server" Text="ریز دروس انتخاب دانشجو" /></div></div>
       <div class="row">
      <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"> 
                   <asp:CheckBox ID="chk_Term" runat="server" Text="همه ترم ها" /></div></div>
       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"> 
                   <asp:CheckBox ID="chk_MaxRizesh" runat="server" Text="لیست آمار بیشترین ریزش دانشجو" /></div></div>
       <div class="row">
       <div class="col-md-3"></div>
              <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-2"> 
                    <asp:Button ID="btn_ShowList" runat="server" Text="نمایش لیست"  OnClick="btn_ShowList_Click" CssClass="btn btn-success"/>
           </div></div>
       <div class="col-md-4"></div>

<%--      <div  class="col-md-2"><asp:Button ID="btn_ShowInfo" runat="server" Text="نمایش اطلاعات" OnClick="btn_ShowInfo_Click" CssClass="btn btn-primary"/></div>--%>

     </div>
         
    <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="namestu" HeaderText="نام دانشجو" />
                    <asp:BoundField DataField="familystu" HeaderText="نام خانوادگی دانشجو" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="sumvn" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="sumva" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="sabt_web" HeaderText="نمره" />

               </Columns>
             </asp:GridView>
             </div>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" ></telerik:RadWindowManager>
       <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" /><uc1:AccessControl ID="AccessControl1" runat="server" />
        </div>
</asp:Content>
