<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ListClass.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListClass" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListClass.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListClass"  %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <script src="../../Theme/js/js-persian-cal.min.js"></script>
    <link href="../../Theme/css/js-persian-cal.css" rel="stylesheet" />
        <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
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
       <div class="col-md-1"><asp:Label ID="lbl_Term" runat="server" Text="ترم :" ></asp:Label></div>
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
            <sup></sup>
            </span>
         </div>
       <div class="col-md-1"><asp:Label runat="server" ID="lbl_Day" Text="روز :"></asp:Label></div>
       <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                       <ContentTemplate>
                           <asp:DropDownList ID="ddl_Day" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddl_Day_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Day" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>

      <div class="row">
      <div class="col-md-3"></div>
           <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
       <div class="col-md-1"><asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده:" ></asp:Label></div>
         <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_Daneshkade" runat ="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>

      <div class="row">
      <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
       <div class="col-md-1"><asp:Label ID="lbl_Field" runat="server" Text="رشته تحصیلی:" ></asp:Label></div>
       <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_Field" runat="server" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" Width="200px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>

       <div class="row">
       <div class="col-md-3"></div>
      <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
       <div class="col-md-1"><asp:Label ID="lbl_Degree" runat="server" text="مقطع کلاس:" ></asp:Label></div>
     <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_Degree" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" Width="200px" CssClass="form-control input-sm"></asp:DropDownList>
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
           <div class="col-md-1">
               <asp:Label ID="lbl_SaatStart" runat="server" Text="ساعت شروع :"></asp:Label>
           </div>
    <div class="col-md-1">
      <telerik:RadTimePicker ID="RTP1" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" >
          <TimeView Interval="00:15:00" Columns="6" runat="server">
          </TimeView>
      </telerik:RadTimePicker>
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
               <asp:Label ID="lbl_SaatEnd" runat="server" Text="ساعت پایان :"></asp:Label>
           </div>
        <div class="col-md-1">
      <telerik:RadTimePicker ID="RTP2" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" >
          <TimeView ID="TimeView1" Interval="00:15:00" Columns="6" runat="server">
          </TimeView>
      </telerik:RadTimePicker>
          </div>
        </div>
             <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-6"> 
         <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
             <ContentTemplate>
               <asp:RadioButton ID="rdb_ListClassDay" runat="server" Text="لیست کلاس بر اساس روز" Font-Names="Tahoma" dir="rtl" AutoPostBack="true"  GroupName="ReportListClass" ValidationGroup="0" Checked="true"/>
                  </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdb_ListClassDay"/>
                </Triggers>
              </asp:UpdatePanel>   
        </div></div>
          <br />
             <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-6"><asp:UpdatePanel ID="UpdatePanel7" runat="server" >
             <ContentTemplate>
             <asp:RadioButton ID="rdb_ListClassDelete" runat="server" Text="لیست کلاس هایی که حذف توسط گروه داشته اند" Font-Names="Tahoma" dir="rtl" AutoPostBack="true" GroupName="ReportListClass" ValidationGroup="1" />
                  </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdb_ListClassDelete"/>
                </Triggers>
              </asp:UpdatePanel> 
      </div></div>
                <br />       
   <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-6"> <asp:UpdatePanel ID="UpdatePanel8" runat="server" >
             <ContentTemplate>
             <asp:RadioButton ID="rdb_ListClassTarikh" runat="server" Font-Names="Tahoma" Text="لیست کلاس ها بر اساس تاریخ تحویل نمره استاد" AutoPostBack="true" GroupName="ReportListClass" ValidationGroup="2" />
                  </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdb_ListClassTarikh"/>
                </Triggers>
              </asp:UpdatePanel> </div></div>

   <div class="row">
     <div class="col-md-3"></div>
       <div class="col-md-1">
        <span style="color:red; font-size:small;">
        <sup></sup>
       </span>
      </div>
      <div class="col-md-2"><asp:Label ID="lbl_AzTarikhTahvilNomre" runat="server" Font-Names="Tahoma" Text="از تاریخ تحویل نمره :"></asp:Label></div>
       <div class="col-md-4">
        <pdc:PersianDateTextBox ID="txt_AzTarikhTahvilNomre" runat="server" DefaultDate="13  /  /  "
        IconUrl ="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px"></pdc:PersianDateTextBox>
       </div></div>
       
        
         <div class="row">
       <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div>
        <div class="col-md-2"><asp:Label ID="lbl_TaTarikhTahvilNomre" runat="server" Font-Names="Tahoma" Text="تا تاریخ تحویل نمره :" ></asp:Label></div>
        <div class="col-md-4">
        <pdc:PersianDateTextBox ID="txt_TaTarikhTahvilNomre" runat="server" DefaultDate="13  /  /  "
       IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px"></pdc:PersianDateTextBox>      
        </div></div>
            <br />  
             <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-4"> <asp:UpdatePanel ID="UpdatePanel9" runat="server" >
             <ContentTemplate> <asp:RadioButton ID="rdb_ListTadakhol" runat="server" Text="لیست تداخل کلاس با ساعت خاص" Font-Names="Tahoma" AutoPostBack="true" GroupName="ReportListClass" ValidationGroup="3" />
                  </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdb_ListTadakhol"/>
                </Triggers>
              </asp:UpdatePanel> </div>
       
      </div>
      <div class="row">
       <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div>
       <div class="col-md-2">
           <asp:Label ID="lbl_AzSaatTadakhol" runat="server" Font-Names="Tahoma" Text="از ساعت :" Width="100px"></asp:Label>
       </div>
          <div class="col-md-2">
          <telerik:RadTimePicker ID="RTP3" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" >
              <TimeView ID="TimeView3" Interval="00:15:00" Columns="6" runat="server">
              </TimeView>
          </telerik:RadTimePicker>
          </div>
          </div>
       <div class="row">
       <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div>
        <div class="col-md-2">
         <asp:Label ID="lbl_TaSaatTadakhol" runat="server" Font-Names="Tahoma" Text="تا ساعت :"  Width="100px"></asp:Label>
        </div>
        <div class="col-md-2">
       <telerik:RadTimePicker ID="RTP4" runat="server" Width="100px" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" >
       <TimeView ID="TimeView4" Interval="00:15:00" Columns="6" runat="server">
        </TimeView>
       </telerik:RadTimePicker>
        </div>
        </div> 
   <br />
    
             <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-6"><asp:UpdatePanel ID="UpdatePanel10" runat="server" >
             <ContentTemplate>
            <asp:RadioButton ID="rdb_ListHadClass" runat="server" Font-Names="Tahoma" Text="لیست کلاس ها بر اساس ظرفیت :" AutoPostBack="true" GroupName="ReportListClass" ValidationGroup="4" />
                  </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdb_ListHadClass"/>
                </Triggers>
              </asp:UpdatePanel></div>   </div>   
         <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-2"><asp:Label ID="lbl_ZarfiyatKamtar" runat="server" Font-Names="Tahoma" Text="ظرفیت پر شده کمتر از :" width="150px"></asp:Label></div>
       <div class="col-md-2"><asp:TextBox ID="txt_ZarfiyatKamtar" runat="server" MaxLength="3" CssClass="form-control" Width="50px"></asp:TextBox></div></div>
         <div class="row">
       <div class="col-md-3"></div>
        <div class="col-md-2"><asp:Label ID="lbl_ZargfiyatBishtar" runat="server" Font-Names="Tahoma" Text="ظرفیت پر شده بیشتر از :" Width="150px" ></asp:Label></div>
        <div class="col-md-2"><asp:TextBox ID="txt_ZarfiyatBishtar" runat="server" MaxLength="2" CssClass="form-control" Width="50px"></asp:TextBox></div>
         </div>

         <div class="row">
       <div class="col-md-3"></div>
        <div class="col-md-2"><asp:Label ID="lbl_TypeDivision" runat="server" Font-Names="Tahoma" Text="تعداد واحد :" Width="150px" ></asp:Label></div>
        <div class="col-md-2"><asp:TextBox ID="txt_TypeDivision" runat="server" MaxLength="1" CssClass="form-control" Width="50px"></asp:TextBox></div>
        </div>
  
          <br />    
             <%--<div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-6"><asp:UpdatePanel ID="UpdatePanel11" runat="server" >
             <ContentTemplate>
            <asp:RadioButton ID="rdb_MoghayeratBaTaghvim" runat="server" Font-Names="Tahoma" Text="لیست کلاس هایی که تاریخ امتحان مغایر با تقویم دانشگاهی است" AutoPostBack="true" GroupName="ReportListClass" ValidationGroup="5" />
                  </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdb_MoghayeratBaTaghvim"/>
                </Triggers>
              </asp:UpdatePanel> </div></div>
         <div class="row">
       <div class="col-md-3"></div>
        <div class="col-md-2"><asp:Label ID="lbl_AzTarikhExam" runat="server" Font-Names="Tahoma" Text="از تاریخ امتحان :" ></asp:Label></div>
        <div class="col-md-2">
        <pdc:PersianDateTextBox ID="txt_TaTarikhExam" runat="server" DefaultDate="13  /  /  "
       IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" width="200px"></pdc:PersianDateTextBox>  
        </div>  </div>
        <div class="row">
       <div class="col-md-3"></div>    
        <div class="col-md-2"><asp:Label ID="lbl_TaTarikh" runat="server" Font-Names="Tahoma" Text=" تا تاریخ امتحان :" ></asp:Label></div>
        <div class="col-md-2">
        <pdc:PersianDateTextBox ID="txt_TaTarikh" runat="server" DefaultDate="13  /  /  "
       IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" width="200px"></pdc:PersianDateTextBox>
        </div>
         </div>--%>
       <div class="row">
       <div class="col-md-3"></div>
        <div class="col-md-8"><asp:Button ID="btn_ReportListClass" Text="نمایش لیست" runat="server" OnClick="btn_ReportListClass_Click" CssClass="btn btn-success"/></div></div> 
          
        <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="gv_Show" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="idroz1" HeaderText="روز" />
                    <asp:BoundField DataField="zarfklass" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="Mard" HeaderText="ظرفیت پر شده کلاس(مرد)" />
                    <asp:BoundField DataField="Zan" HeaderText="ظرفیت پر شده کلاس(زن)" />
                    <asp:BoundField DataField="zarfporm" HeaderText="مجموع ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                </Columns>
            </asp:GridView>
            </div>    
              <div id="Div2" runat="server">
             <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                     <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="idroz" HeaderText="روز" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="Shomklas" HeaderText="محل کلاس" />
                    <asp:BoundField DataField="zarfklass" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="zarfporm" HeaderText="مجموع ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                </Columns>
            </asp:GridView>
            </div> 
              <div id="Div3" runat="server">
             <asp:ImageButton ID="img_ExportToExcel3" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel3_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                     <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="idroz" HeaderText="روز" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="Shomklas" HeaderText="محل کلاس" />
                    <asp:BoundField DataField="zarfklass" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="zarfporm" HeaderText="مجموع ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                    <asp:BoundField DataField="date_tahvil_nom" HeaderText="تاریخ تحویل نمره" />
                </Columns>
            </asp:GridView>
            </div> 
              <div id="Div4" runat="server">
             <asp:ImageButton ID="img_ExportToExcel4" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel4_Click" Visible="false" />
            <asp:GridView ID="GridView3" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                     <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="family" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="idroz" HeaderText="روز" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="Shomklas" HeaderText="محل کلاس" />
                    <asp:BoundField DataField="zarfklass" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="zarfporm" HeaderText="مجموع ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                </Columns>
            </asp:GridView>
            </div> 
            <div id="Div5" runat="server">
             <asp:ImageButton ID="img_ExportToExcel5" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel5_Click" Visible="false" />
            <asp:GridView ID="GridView4" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="saatstart" HeaderText="ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                    <asp:BoundField DataField="zarfklass" HeaderText="ظرفیت کلاس" />
                    <asp:BoundField DataField="zarfporm" HeaderText=" ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                </Columns>
            </asp:GridView>
            </div> 
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="1000px" Width="1050px" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true"/><uc1:AccessControl ID="AccessControl1" runat="server" />
         </div>
</asp:Content>