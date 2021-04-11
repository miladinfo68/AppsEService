<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Students/MasterPage/StudentsMasterPages.Master" AutoEventWireup="true" CodeBehind="CardsExam.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.CardsExam"  %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="CardsExam.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.CardsExam"  %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title> <asp:Literal ID="t" runat="server"></asp:Literal></title>
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3 style="color:blue">
       گزارش کارت امتحان
    </h3>
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
                            <telerik:AjaxUpdatedControl ControlID="txt_stCode" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>                 
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>


      <div class="row">
        <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Term" runat="server" Text="ترم :"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                            </Triggers>
                       </asp:UpdatePanel>            
            </div>
          </div>

      <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Daneshkade" runat="server" text="دانشکده :" Width="180px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Daneshkade" runat="server" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                            </Triggers>
                       </asp:UpdatePanel>            
            </div>
          </div>

       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Field" runat="server" Text="رشته تحصیلی :" Width="180px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Field" runat="server" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                            </Triggers>
                       </asp:UpdatePanel>            
            </div>
          </div>

       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Degree" runat="server" Text="مقطع تحصیلی :" Width="180px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Degree" runat="server" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                            </Triggers>
                       </asp:UpdatePanel>            
            </div>
          </div>

       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Dorpar" runat="server" Text="سیستم آموزشی :" Width="180px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Dorpar" runat="server" OnSelectedIndexChanged="ddl_Dorpar_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Dorpar" EventName="SelectedIndexChanged" />
                            </Triggers>
                       </asp:UpdatePanel>            
            </div>
          </div>

       <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_Sex" runat="server" Text="جنسیت :" Width="180px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Sex" runat="server" OnSelectedIndexChanged="ddl_Sex_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Sex" EventName="SelectedIndexChanged" />
                            </Triggers>
                       </asp:UpdatePanel>            
            </div>
          </div>

        <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
          <span style="color:red; font-size:small;">
           <sup></sup>
          </span>
        </div> 
       <div class="col-md-1">
              <asp:Label ID="lbl_stCode" runat="server" Text="شماره دانشجویی :" Width="180px"></asp:Label></div>
                 <div class="col-md-2">
              <asp:TextBox ID="txt_stCode" runat="server" Width="150px" CssClass="form-control" ></asp:TextBox>
                 </div>
         <div class="col-md-2"><asp:Button ID="btn_stCode" runat="server" Text="انتخاب کنید" Width="110px" class="btn btn-success btn-lg" OnClick="btn_stCode_Click"/>
         </div>
    </div>


    <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
          <span style="color:red; font-size:small;">
           <sup></sup>
          </span>
        </div> 
       <div class="col-md-1">
              <asp:Label ID="lbl_TarikhSodor" runat="server" Text=" تاریخ صدور :" Width="200px"></asp:Label></div>
        <div class="col-md-2">
            <pdc:PersianDateTextBox ID="txt_TarikhSodor" runat="server" DefaultDate="13  /  /  " Visible="true" CssClass="form-control"
            IconUrl ="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="180px"></pdc:PersianDateTextBox> 
         </div>
    </div>

    <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
          <span style="color:red; font-size:small;">
           <sup></sup>
          </span>
        </div> 
       <div class="col-md-1">
              <asp:Label ID="lbl_SalVorod" runat="server" Text="سال ورود :" Width="180px"></asp:Label></div>
        <div class="col-md-2">
            <asp:TextBox ID="txt_SalVorod" runat="server" MaxLength="2" Width="50px" CssClass="form-control" ></asp:TextBox>
         </div>
    </div>

     <div class="row">
        <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
          </div> 
       <div class="col-md-1"><asp:Label ID="lbl_Etebar" runat="server" Text="مدت اعتبار :" Width="180px"></asp:Label></div>
          <div class="col-md-2">
               <asp:TextBox ID="txt_Etebar" runat="server" MaxLength="2" Width="50px" CssClass="form-control" text="6"></asp:TextBox>                    
            </div>
       </div>

       <div class="row">
        <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
          </div> 
       <div class="col-md-1"><asp:Label ID="lbl_BedehiAz" runat="server" Text="مبلغ بدهی از :" Width="180px" ></asp:Label></div>
          <div class="col-md-2">
               <asp:TextBox ID="txt_BedehiAz" runat="server" MaxLength="6" Width="100px" CssClass="form-control" Text="0" ></asp:TextBox>                      
            </div>
       </div>

      <div class="row">
        <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
          </div> 
           <div class="col-md-1"><asp:Label ID="lbl_BedehiTa" runat="server" Text="مبلغ بدهی تا :" Width="180px" ></asp:Label></div>
          <div class="col-md-2">
                  <asp:TextBox ID="txt_BedehiTa" runat="server" MaxLength="6" Width="100px" CssClass="form-control" Text="0" ></asp:TextBox>           
            </div>
       </div>

           <div class="row">
        <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
          </div> 
           <div class="col-md-1"><asp:Label ID="lbl_Semat" runat="server" Text="سمت :" Width="180px"></asp:Label></div>
          <div class="col-md-2">
                  <asp:TextBox ID="txt_Semat" runat="server" Text="معاون دانشجویی واحد" Width="150" CssClass="form-control" ></asp:TextBox>          
            </div>
       </div>

      <div class="row">
        <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
          </div> 
       <div class="col-md-1"><asp:Label ID="lbl_FamilySemat" runat="server" Text="فامیلی سمت :" Width="180px"></asp:Label></div>
          <div class="col-md-2">
            <asp:TextBox ID="txt_FamilySemat" runat="server" MaxLength="2" Width="150px" CssClass="form-control" ></asp:TextBox>           
            </div>
       </div>

     <div class="row">
      <div class="col-md-3"></div>
      <div class="col-md-4">
         <asp:CheckBox ID="chk_ControlCheck" runat="server" Text="کنترل چک وصول نشده" Checked="true" />
      </div>
     </div>

      <div class="row">
      <div class="col-md-3"></div>
      <div class="col-md-4">
         <asp:CheckBox ID="chk_SearchStu" runat="server" Text="جستجو بر اساس دانشجوی خاص(وضعیت دانشجو عادی نباشد)" />
      </div>
     </div>


     <div class="row">
      <div class="col-md-3"></div>
      <div class="col-md-2">
          <asp:Button id="btn_ShowKartMovaghat" runat="server" Text="نمایش کارت موقت" Font-Bold="true" OnClick="btn_ShowKartMovaghat_Click" ForeColor="RoyalBlue" CssClass="btn btn-success"/>
      </div>
     <div class="col-md-2">
          <asp:Button id="btn_ShowKartMovaghat2" runat="server" Text="نمایش کارت موقت 2" Font-Bold="true" ForeColor="RoyalBlue" OnClick="btn_ShowKartMovaghat2_Click" CssClass="btn btn-success"/>
      </div>
      <div class="col-md-2">
          <asp:Button id="btn_ShowAx" runat="server" Text="لیست دانشجویانی که عکس ندارند" Font-Bold="true" ForeColor="RoyalBlue" OnClick="btn_ShowAx_Click" CssClass="btn btn-success"/>
      </div>
     </div> 

      <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false"/>

      <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false"/>

      <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
      AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false"/>
                                
     <telerik:RadWindowManager ID="rwd" runat="server" ></telerik:RadWindowManager>

    <asp:Label ID="lbl_CtrlCheck" runat="server" Visible="false"></asp:Label>
     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
     

        <asp:GridView ID="grd_Show1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
        RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
             <Columns>
                    <%--<asp:BoundField DataField="pic" HeaderText="عکس دانشجو" />--%>
                    <asp:BoundField DataField="nameStu" HeaderText="نام و نام خانوادگی" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="Education" HeaderText="سیستم آموزشی" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="Date" HeaderText="تاریخ صدور کارت" />
                </Columns>
       </asp:GridView>


      <asp:GridView ID="grd_Show2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
      RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
      <Columns>
                    <%--<asp:BoundField DataField="pic" HeaderText="عکس دانشجو" />--%>
                    <asp:BoundField DataField="nameStu" HeaderText="نام و نام خانوادگی" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="Date" HeaderText="تاریخ صدور کارت" />
        </Columns>
       </asp:GridView>


      <asp:GridView ID="grd_Show3" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
      RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
      <Columns>
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="NameStu" HeaderText="نام خانوادگی و نام" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="idd_meli" HeaderText="کد ملی" />
                    <asp:BoundField DataField="degree" HeaderText="مقطع تحصیلی" />
        </Columns>
       </asp:GridView>
   </div> 
</asp:Content>
