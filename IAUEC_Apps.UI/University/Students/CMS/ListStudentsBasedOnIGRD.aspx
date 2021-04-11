<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Students/MasterPage/StudentsMasterPages.Master" AutoEventWireup="true" CodeBehind="ListStudentsBasedOnIGRD.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.ListStudentsBasedOnIGRD" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListStudentsBasedOnIGRD.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.ListStudentsBasedOnIGRD"  %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">

 <title> <asp:Literal ID="t" runat="server"></asp:Literal></title>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3 style="color:blue">
       گزارش دانشجویان میهمان / ثبت نام با تأخیر / عدم مراجعه / عادی
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
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
   CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
   ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
   SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
 </pdc:PersianDateScriptManager>
 <div class="Report-Area" dir="rtl">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="txt_StCode" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>                 
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>
</div>
 
    <div class="Report-Area" dir="rtl">
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
            <sup></sup>
            </span>
         </div> 
           <div class="col-md-1"><asp:Label ID="lbl_Daneshkade" runat="server" text="دانشکده :"></asp:Label></div>
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
        <div class="col-md-1"><asp:Label ID="lbl_Field" runat="server" Text="رشته تحصیلی :"></asp:Label></div>
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
        <div class="col-md-1"><asp:Label ID="lbl_SalVorod" runat="server" Text="سال ورود :"></asp:Label></div>
         <div class="col-md-1">
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
        <div class="col-md-1"><asp:Label ID="lbl_NimsalVorod" runat="server" Text="نیمسال ورود :"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_NimsalVorod" runat="server" OnSelectedIndexChanged="ddl_NimsalVorod_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_NimsalVorod" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"><asp:Label ID="lbl_Sex" runat="server" Text="جنسیت :"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
        <div class="col-md-1"><asp:Label ID="lbl_Isargar" runat="server" Text="ایثارگر :"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Isargar" runat="server" OnSelectedIndexChanged="ddl_Isargar_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Isargar" EventName="SelectedIndexChanged" />
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
               <asp:Label ID="Label1" runat="server" Text="شماره دانشجویی :"></asp:Label></div>
        <div class="col-md-1">
              <asp:TextBox ID="txt_stCode" runat="server" Width="150px" CssClass="form-control" ></asp:TextBox>
        </div>
         <div class="col-md-1"><asp:Button ID="btn_stCode" runat="server" Text="انتخاب کنید" Width="110px" class="btn btn-success btn-lg" OnClick="btn_stCode_Click" />
         </div>
    </div>

          <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-1"><asp:Label ID="lbl_AcceptedStu" runat="server" Text="نوع پذیرش :"></asp:Label></div>
                  <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_AcceptedStu" runat="server" OnSelectedIndexChanged="ddl_AcceptedStu_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_AcceptedStu" EventName="SelectedIndexChanged" />
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
        <div class="col-md-1"> <asp:Label ID="lbl_StatusStu" runat="server" Text="وضعیت دانشجو :" Width="180px"></asp:Label></div>
              <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_StatusStu" runat="server" OnSelectedIndexChanged="ddl_StatusStu_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_StatusStu" EventName="SelectedIndexChanged" />
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
       <div class="col-md-3">
           <asp:RadioButton ID="rdb_Mehmani" runat="server" ValidationGroup="0" GroupName="ReportStudents" text="میهمانی به" OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>
       </div>
    </div>

         <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
           <asp:RadioButton ID="rdb_Enteghali" runat="server" ValidationGroup="1" GroupName="ReportStudents" Text="انتقالی از" OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>
       </div>
    </div>

     <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
            <asp:RadioButton ID="rdb_AddressTel" runat="server" ValidationGroup="2" GroupName="ReportStudents" Text="لیست دانشجو با آدرس و تلفن"  OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>
       </div>
    </div>

  <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
            <asp:RadioButton ID="rdb_AdamMoraje" runat="server" Text="لیست عدم مراجعه در ترم جاری با توجه به ثبت نام ترم" ValidationGroup="6" GroupName="ReportStudents"  OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>
       </div>
      <div class="col-md-1">
          <asp:TextBox ID="txt_Term" runat="server" Text="  -  - " Width="100px" Visible="false" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
            <asp:RadioButton ID="rdb_VaziiatTerm1" runat="server" Text="لیست بر اساس وضعیت ترم" ValidationGroup="3" GroupName="ReportStudents"  OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>
       </div>
        <div class="col-md-1">
            <asp:Label ID="lbl_VaziiatTerm" runat="server" Text="وضعیت ترم :" Visible="false"></asp:Label>
        </div>

         <div class="col-md-1">
           <asp:UpdatePanel ID="UpdatePanel11" runat="server">
             <ContentTemplate>
                  <asp:DropDownList ID="ddl_VaziiatTerm" runat="server" AutoPostBack="true" Visible="false" Width="150px" OnSelectedIndexChanged="ddl_VaziiatTerm_SelectedIndexChanged"></asp:DropDownList>
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_VaziiatNomre" EventName="SelectedIndexChanged" />
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
       <div class="col-md-3">
            <asp:RadioButton ID="rdb_VaziiatNomre" runat="server" text="لیست بر اساس وضعیت نمره " ValidationGroup="4" GroupName="ReportStudents"  OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>
       </div>
        <div class="col-md-1">
            <asp:Label ID="lbl_VaziiatNomre" runat="server" Text="وضعیت نمره :" Visible="false"></asp:Label>
        </div>

         <div class="col-md-1">
           <asp:UpdatePanel ID="UpdatePanel12" runat="server">
             <ContentTemplate>
                  <asp:DropDownList ID="ddl_VaziiatNomre" runat="server" Visible="false" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddl_VaziiatNomre_SelectedIndexChanged"></asp:DropDownList>
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_VaziiatNomre" EventName="SelectedIndexChanged" />
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
       <div class="col-md-3">
             <asp:RadioButton ID="rdb_NaghsParvande" runat="server" ValidationGroup="5" GroupName="ReportStudents" Text="دانشجویانی که نقص پرونده دارند"  OnCheckedChanged="rdb_Mehmani_CheckedChanged" AutoPostBack="true"/>          
       </div>
    </div>

    <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-4">
            <asp:RadioButton ID="rdb_FormMehmani" runat="server" Text="فرم میهمانی به(شماره نامه یا شماره دانشجویی را وارد کنید)" AutoPostBack="true" GroupName="ReportStudents" ValidationGroup="7" OnCheckedChanged="rdb_Mehmani_CheckedChanged" />         
       </div>
    </div>

   <div class="row">
        <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-2"><asp:Label ID="lbl_ShomareName" runat="server" Text="شماره نامه :" visible="false"></asp:Label></div>
                  <div class="col-md-2">
                  <asp:TextBox ID="txt_ShomareName" runat="server" visible="false" CssClass="form-control" ></asp:TextBox>           
            </div>
       </div>

       <div class="row">
        <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
        <div class="col-md-2"><asp:Label ID="lbl_TarikhName" runat="server" Text="تاریخ نامه :" Visible="false"></asp:Label></div>
        <div class="col-md-4">
        <pdc:PersianDateTextBox ID="txt_TarikhName" runat="server" DefaultDate="13  /  /  " Visible="false" CssClass="form-control"
        IconUrl ="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px"></pdc:PersianDateTextBox>  
            <%--<asp:TextBox ID="txt_TarikhName" runat="server" Visible="false" ></asp:TextBox>--%>   
            </div>
       </div>

     <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3">
            <asp:RadioButton ID="rdb_HichKodam" runat="server" Text="هیچ کدام از موارد فوق" AutoPostBack="true" GroupName="ReportStudents" ValidationGroup="7" OnCheckedChanged="rdb_Mehmani_CheckedChanged" Checked="true" />         
       </div>
    </div>

      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"><asp:CheckBox ID="chk_TaeedieTahsili" runat="server" Text="تاییدیه مدرک تحصیلی ندارند" /></div></div>

      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"><asp:CheckBox ID="chk_MoadelSazi" runat="server" Text="لیست دانشجویانی که معادلسازی دارند" /></div></div>

      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"><asp:checkBox ID="chk_TakmilZarfiat" runat="server" Text="دانشجو با تکمیل ظرفیت" /></div></div>

      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"><asp:CheckBox ID="chk_SabteNameBaTakhir" runat="server" Text="ثبت نام با تأخیر" /></div></div>

      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-3"><asp:CheckBox ID="chk_TermJari" runat="server" Text="دانشجویان ترم جاری" /></div></div>

     <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div class="col-md-1">
          <asp:Button id="btn_Show" runat="server" Text="نمایش اطلاعات" Font-Bold="true" ForeColor="RoyalBlue" OnClick="btn_Show_Click" CssClass="btn btn-success"/>
      </div></div> 

       <div id="Div1" runat="server">
        <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
          <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="namecoding" HeaderText="به دانشگاه" />
                    <asp:BoundField DataField="num_nam" HeaderText="شماره نامه" />
                    <asp:BoundField DataField="date_nam" HeaderText="مورخ " />
                    <asp:BoundField DataField="jensiat" HeaderText="جنسیت" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="magh" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="NameSodur" HeaderText="صادره از" />
                    <asp:BoundField DataField="sal_vorod" HeaderText="سال ورود" />
                    <asp:BoundField DataField="Nimsal" HeaderText="نیمسال ورود" />
                    <asp:BoundField DataField="Term1" HeaderText="سال" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="تعداد واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="تعداد واحد عملی" />
                    <asp:BoundField DataField="mark_emtehan" HeaderText="نمره امتحان" />
                    <asp:BoundField DataField="Harf" HeaderText="حرف" />
                </Columns>
            </asp:GridView>
            </div>

        <div id="Div2" runat="server">
        <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
          <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="namecoding" HeaderText="شرح نوع نقص پرونده" />
                    <asp:BoundField DataField="num_naghs" HeaderText="تعداد" />
                    <asp:BoundField DataField="date_mohlat" HeaderText="مهلت رفع نقص" />
                    <asp:BoundField DataField="datesabt" HeaderText="تاریخ ثبت" />
                </Columns>
            </asp:GridView>
            </div>

       <div id="Div3" runat="server">
        <asp:ImageButton ID="img_ExportToExcel3" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel3_Click" Visible="false" />
          <asp:GridView ID="GridView3" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText=" نام خانوادگی و نام " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                </Columns>
            </asp:GridView>
            </div>

        <div id="Div4" runat="server">
        <asp:ImageButton ID="img_ExportToExcel4" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel4_Click" Visible="false" />
          <asp:GridView ID="GridView4" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="عملی" />
                </Columns>
            </asp:GridView>
            </div>

       <div id="Div5" runat="server">
        <asp:ImageButton ID="img_ExportToExcel5" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel5_Click" Visible="false" />
          <asp:GridView ID="GridView5" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText=" نام خانوادگی و نام " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                </Columns>
            </asp:GridView>
        </div>

      <div id="Div6" runat="server">
        <asp:ImageButton ID="img_ExportToExcel6" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel6_Click" Visible="false" />
          <asp:GridView ID="GridView6" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText=" نام خانوادگی و نام " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                </Columns>
            </asp:GridView>
        </div>

      <div id="Div7" runat="server">
        <asp:ImageButton ID="img_ExportToExcel7" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel7_Click" Visible="false" />
          <asp:GridView ID="GridView7" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText=" نام خانوادگی و نام " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                </Columns>
            </asp:GridView>
        </div>

        <div id="Div8" runat="server">
        <asp:ImageButton ID="img_ExportToExcel8" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel8_Click" Visible="false" />
          <asp:GridView ID="GridView8" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText="نام خانوادگی و نام" />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر " />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="namecoding" HeaderText="میهمان به" />
                    <asp:BoundField DataField="num_nam" HeaderText="شماره نامه" />
                    <asp:BoundField DataField="date_nam" HeaderText="تاریخ نامه" />
                    <asp:BoundField DataField="Date" HeaderText="تاریخ صدور" />
                </Columns>
            </asp:GridView>
            </div>

         <div id="Div9" runat="server">
        <asp:ImageButton ID="img_ExportToExcel9" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel9_Click" Visible="false" />
          <asp:GridView ID="GridView9" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText=" نام خانوادگی و نام " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                </Columns>
            </asp:GridView>
            </div>

         <div id="Div10" runat="server">
        <asp:ImageButton ID="img_ExportToExcel10" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel10_Click" Visible="false" />
          <asp:GridView ID="GridView10" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameresh" HeaderText="رشته تحصیلی" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="familyy" HeaderText=" نام خانوادگی و نام " />
                    <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                    <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                    <asp:BoundField DataField="date_tav" HeaderText="تاریخ تولد" />
                    <asp:BoundField DataField="Degree" HeaderText="مقطع تحصیلی" />
                    <asp:BoundField DataField="tel" HeaderText="تلفن" />
                    <asp:BoundField DataField="mobile" HeaderText="موبایل" />
                    <asp:BoundField DataField="addressm" HeaderText="آدرس منزل" />
                </Columns>
            </asp:GridView>
            </div>

        <asp:Label ID="lbl_DateName" runat="server" Visible="false"></asp:Label>
         <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="1100px" Width="1050px" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true" />
       <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
   </div>
</asp:Content>