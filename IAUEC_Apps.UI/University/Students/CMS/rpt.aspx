<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="rpt.aspx.cs" Inherits="IAUEC_Apps.UI.University.Students.CMS.rpt" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style type="text/css">
    .rcbItem
    {
        font-family: tahoma;
      
    }
    .rcbHovered
{
    font-family: Tahoma;
    font-weight:bold;
}

</style>
      <link rel="stylesheet" type="text/css" media="all" href="../../../Adobe/css/aqua/theme.css" title="Aqua" />
    <script type="text/javascript" src="../../../Adobe/js/jalali.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../../../Adobe/js/calendar.js"></script>
		
		<!-- import the calendar script -->
		<script type="text/javascript" src="../../../Adobe/js/calendar-setup.js"></script>
		
		<!-- import the language module -->
		<script type="text/javascript" src="../../../Adobe/js/lang/calendar-fa.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindowManager ID="rwd" runat="server"></telerik:RadWindowManager>
      <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7);background-color:rgba(231, 230, 230,0.3);padding: 1%;border-radius:5px; margin-bottom:1%;">
        <div class="row">
            <div class="col-md-12" style="padding:1%">
                 <p style="color: #CC0000"><span style="font-size: large; font-weight: bold; ">*</span>در صورتیکه که ترم را انتخاب ننمایید ترم جاری به صورت پیش فرض در نظر گرفته می شود</p>
                 <div class="col-md-2">ترم :
                 </div>
                 <div class="col-md-2">
                     <telerik:RadComboBox ID="ddl_Term" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Vista">
                     </telerik:RadComboBox>
                 </div>
          <div class="col-md-1">سال ورود :
                      </div>
                      <div class="col-md-2">
                         
                          <asp:TextBox ID="txt_SalVorod" runat="server" Width="50px">0</asp:TextBox>
                         
                      </div>
                     <div class="col-md-1">نیمسال ورود :
                      </div>
                      <div class="col-md-2">
                           <telerik:RadComboBox ID="ddl_NimsalVorod" Runat="server" Width="65%" EmptyMessage="انتخاب نمایید..." Skin="Windows7">
                               <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                   <telerik:RadComboBoxItem runat="server" Text="مهر" Value="1" />
                                   <telerik:RadComboBoxItem runat="server" Text="بهمن" Value="2" />
                                   <%--<telerik:RadComboBoxItem runat="server" Text="تابستان" Value="3" />--%>
                               </Items>
                           </telerik:RadComboBox>
                      </div>
                 
             </div>
           </div>
          <div class="row">
                 <div class="col-md-12" style="padding:1%">
                       <div class="col-md-2">مقطع تحصیلی:</div>
                  <div class="col-md-2">
                      <telerik:RadComboBox ID="ddl_Degree" Runat="server" Width="100%" EmptyMessage="انتخاب نمایید..." Skin="Windows7">
                          <Items>
                               <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                              <telerik:RadComboBoxItem runat="server" Text="کاردانی" Value="2" />
                              <telerik:RadComboBoxItem runat="server" Text="کارشناسی" Value="1" />
                              <telerik:RadComboBoxItem runat="server" Text="کارشناسی ناپیوسته" Value="3" />
                              <telerik:RadComboBoxItem runat="server" Text="کارشناسی ارشد پیوسته" Value="4" />
                              <telerik:RadComboBoxItem runat="server" Text="کارشناسی ارشد ناپیوسته" Value="5" />
                              <telerik:RadComboBoxItem runat="server" Text="دکتری تخصصی" Value="7" />
                            
                          </Items>
                     </telerik:RadComboBox>
                  </div>
                     <div class="col-md-1">دانشکده:</div>
                 <div class="col-md-2">
                     <telerik:RadComboBox ID="ddl_Daneshkade" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Windows7" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true">
                     </telerik:RadComboBox>
                 </div>                

                  <div class="col-md-1">رشته:</div>
                     <asp:UpdatePanel runat="server" id="UpdatePanel1">  <ContentTemplate>
                  <div class="col-md-4">
                      <telerik:RadComboBox ID="ddl_Field" Runat="server" EmptyMessage="انتخاب یا جستجو نمایید..." Skin="Vista" Filter="Contains" MarkFirstMatch="True" Width="100%">
                     <Localization AllItemsCheckedString="همه موارد انتخاب شده است" CheckAllString="انتخاب همه موارد" />
                           </telerik:RadComboBox>
                  </div></ContentTemplate>
                         <Triggers><asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" /></Triggers>
                         </asp:UpdatePanel>
                     </div>
              </div>
            <div class="row">
                 <div class="col-md-12" style="padding:1%">
                     
                     
               
                       <div class="col-md-2">سیستم آموزشی :
                      </div>
                      <div class="col-md-2">
                           <telerik:RadComboBox ID="ddl_Dorpar"  Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Windows7">
                               <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                   <telerik:RadComboBoxItem runat="server" Text="دوره ای" Value="1" />
                                   <telerik:RadComboBoxItem runat="server" Text="پاره وقت" Value="2" />
                                   <telerik:RadComboBoxItem runat="server" Text="طرح معلمان" Value="3" />
                                   <telerik:RadComboBoxItem runat="server" Text="قراردادی" Value="4" />
                               </Items>
                           </telerik:RadComboBox>
                      </div>

                      <div class="col-md-1">نوع پذیرش :
                      </div>
                      <div class="col-md-2">
                           <telerik:RadComboBox ID="ddl_AcceptedStu" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Windows7">
                               <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون سراسری" Value="1" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون پاره وقت" Value="2" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون قراردادی" Value="3" />
                                   <telerik:RadComboBoxItem runat="server" Text="انتقالی از - آزمون" Value="4" />
                                   <telerik:RadComboBoxItem runat="server" Text="انتقالی از - سازمان" Value="5" />
                                   <telerik:RadComboBoxItem runat="server" Text="انتقالی از دانشگاه دولتی" Value="6" />
                                   <telerik:RadComboBoxItem runat="server" Text="انتقالی غیرانتفاعی" Value="7" />
                                   <telerik:RadComboBoxItem runat="server" Text="مأمور به تحصیل" Value="8" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون معلمان" Value="9" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون ارشد ناپیوسته" Value="10" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون دکتری تخصصی" Value="11" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون کارشناسی ناپیوسته" Value="12" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون کاردانی پیوسته" Value="13" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون خاص" Value="14" />
                                   <telerik:RadComboBoxItem runat="server" Text="آزمون رسمی" Value="15" />
                                   <telerik:RadComboBoxItem runat="server" Text="بدون آزمون" Value="16" />
                                   <telerik:RadComboBoxItem runat="server" Text="تکمیل ظرفیت" Value="17" />
                                   <telerik:RadComboBoxItem runat="server" Text="بدون آزمون - سهمیه قهرمانان" Value="18" />
                                   <telerik:RadComboBoxItem runat="server" Text="تک درس" Value="19" />
                               </Items>
                           </telerik:RadComboBox>
                      </div>
                       <div class="col-md-1">جنسیت :
                                 </div>
                                 <div class="col-md-2">
                                      <telerik:RadComboBox ID="ddl_Sex" Width="65%" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Windows7">
                                          <Items>
                                               <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                              <telerik:RadComboBoxItem runat="server" Text="زن" Value="2" />
                                              <telerik:RadComboBoxItem runat="server" Text="مرد" Value="1" />
                                          </Items>
                                      </telerik:RadComboBox>
                                 </div>
                       <div class="row">
                            <div class="col-md-12" style="padding:1%">
                                 <div class="col-md-2">وضعیت دانشجو :
                                 </div>
                                 <div class="col-md-4">
                                      <telerik:RadComboBox ID="ddl_StatusStu" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Vista" CheckBoxes="True" Width="100%">
                                          <Items>
                                               <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                              <telerik:RadComboBoxItem runat="server" Text="عادی" Value="1" />
                                              <telerik:RadComboBoxItem runat="server" Text="فارغ التحصیل" Value="7" />
                                              <telerik:RadComboBoxItem runat="server" Text="انصراف با اطلاع" Value="5" />
                                              <telerik:RadComboBoxItem runat="server" Text="انصراف ماده 51" Value="6" />
                                              <telerik:RadComboBoxItem runat="server" Text="اخراج آموزشی" Value="8" />
                                              <telerik:RadComboBoxItem runat="server" Text="عدم مراجعه" Value="16" />
                                              <telerik:RadComboBoxItem runat="server" Text="اخراج از کل واحدها" Value="17" />
                                              <telerik:RadComboBoxItem runat="server" Text="مهمان از" Value="2" />
                                              <telerik:RadComboBoxItem runat="server" Text="انصراف تغییر رشته" Value="3" />
                                              <telerik:RadComboBoxItem runat="server" Text="انتقال از" Value="4" />
                                              <telerik:RadComboBoxItem runat="server" Text="اخراج انظباطی" Value="9" />
                                              <telerik:RadComboBoxItem runat="server" Text="اخراج از واحدهای تهران" Value="10" />
                                              <telerik:RadComboBoxItem runat="server" Text="محروم" Value="12" />
                                              <telerik:RadComboBoxItem runat="server" Text="فوت" Value="13" />
                                              <telerik:RadComboBoxItem runat="server" Text="شهید" Value="14" />
                                              <telerik:RadComboBoxItem runat="server" Text="مهمان سازمان" Value="15" />
                                              <telerik:RadComboBoxItem runat="server" Text="تسویه حساب مدرک معادل" Value="18" />
                                              <telerik:RadComboBoxItem runat="server" Text="نا معلوم" Value="0" />
                                          </Items>
                                      </telerik:RadComboBox>
                                 </div>
                                <div class="col-md-2">
                         <%--           <asp:CheckBox ID="chk_tel" runat="server" />نمایش تلفن در گزارش--%>
                                </div>
                                <div class="col-md-1"> 
                                 </div>
                               
                                  <div class="col-md-2">
                     <asp:Button CssClass="btn btn-warning"  id="View_rptt" runat="server" OnClick="View_rpt_ServerClick" Text="مشاهده گزارش" />
                      </div>
                               
                             </div>
                      </div>
                 </div>
            </div>
          <div class="row">
              <div class="col-md-12">
                  <div class="row">
                     <div class="col-md-1"></div>
                        <div  class="col-md-10" style="background-color:rgba(255, 187, 51,0.5);padding: 1%;color:#CC0000;text-align:center">
                            در صورتیکه مایل هستید گزارش را بر اساس اطلاعات یک دانشجو مشاهده نمایید در کادر پایین شماره دانشجویی را وارد و یا اطلاعات دانشجوی مورد نظر را جستجو نمایید
                        </div>
                         <div class="col-md-1"></div>
                  </div>
                  <div class="row" >
                       <div class="col-md-1"></div>
                      <div class="col-md-10" style="background-color:rgba(255, 187, 51,0.1);padding: 1%;text-align:center;border: 1px solid rgba(255, 187, 51,0.5);margin-top:2px;color:#000">
                         <div class="row">
                      
                        <div  class="col-md-12">
                         <div class="col-md-2">
                شماره دانشجویی :
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txt_StCode" runat="server" Width="60%" MaxLength="14"></asp:TextBox>
            </div>
          <div class="col-md-2">
             نام خانوادگی :
           </div>
           <div class="col-md-2">
               <asp:TextBox ID="txt_Family" runat="server" ></asp:TextBox>
           </div>
                        </div>
                       
                  </div>
                          <div class="row">
                                 <div  class="col-md-12" style="margin-top:1%">
                               <div class="col-md-2">
               کد ملی:
           </div>
           <div class="col-md-2">
               <asp:TextBox ID="txt_IdMeli" runat="server"  Width="60%" MaxLength="10"></asp:TextBox>
         </div>
                         
                   <div class="col-md-2"><asp:Button ID="btn_simpleSearch" runat="server" Text="جستجو" CssClass="btn btn-info" OnClick="btn_simpleSearch_Click"  /></div>
                   <div class="col-md-2"><asp:Button ID="btn_advanceSearch" runat="server" Text="جستجو پیشرفته" CssClass="btn btn-success" OnClick="btn_advanceSearch_Click" /></div>                  
                    </div></div>
                          <div class="row">
                              <div class="col-md-12">
                                  <telerik:RadGrid ID="grd_Student" runat="server" OnItemCommand="grd_Student_ItemCommand"
        AutoGenerateColumns="false" HorizontalAlign="Center"
        CellSpacing="0" GridLines="None" Skin="Sunset" 
          >
         <MasterTableView DataKeyNames="stcode">
              <ItemStyle Font-Names="tahoma" /> 
            <HeaderStyle Font-Names="tahoma" />
            <AlternatingItemStyle Font-Names="tahoma" />        
                <Columns> 
                    
                     
                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                     <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true">
                    </telerik:GridBoundColumn>   
                      <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی" AllowFiltering="true">
                    </telerik:GridBoundColumn> 
                     <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true">
                    </telerik:GridBoundColumn>  
                     <telerik:GridBoundColumn DataField="idd" HeaderText="شماره شناسنامه" AllowFiltering="true">
                    </telerik:GridBoundColumn>   
                    <telerik:GridBoundColumn DataField="magh" HeaderText="مقطع" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idresh" HeaderText="رشته" AllowFiltering="true">
                    </telerik:GridBoundColumn>  

                    <telerik:GridTemplateColumn>  
                    <ItemTemplate>
                        <telerik:RadButton ID="btn_Select" Text="انتخاب" runat="server" CommandName="Select" CommandArgument='<%#Eval("stcode") %>'></telerik:RadButton>
                        </ItemTemplate>
                        </telerik:GridTemplateColumn>
                     </Columns>
            </MasterTableView>
            </telerik:RadGrid>
                              </div>
                          </div>
                    </div> <div class="col-md-1"></div>
                      </div>
           
              </div>
          </div>
       </div>
     <asp:UpdatePanel runat="server" id="UpdatePanel2">  <ContentTemplate>
      <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7);background-color:rgba(231, 230, 230,0.3);padding: 1%;border-radius:5px; margin-bottom:1%;margin-top:1%">
        <div class="row">
            <div class="col-md-12" style="padding:1%">
             
                  <div class="col-md-2" style="background:#45d4c6;padding:1%;text-align:center;color:#fff;margin-left:1%">
                     <asp:RadioButton ID="rdb_adamemoraje" runat="server" GroupName="a" OnCheckedChanged="rdb_adamemoraje_CheckedChanged"  Style="margin-left:5px" AutoPostBack="True" />عدم مراجعه
                      </div>
               
                 <div class="col-md-2" style="background:#e15f4f;padding:1%;text-align:center;color:#fff;margin-left:1%">
                     <asp:CheckBox id="chk_naghs" runat="server" style="margin-left:5px" AutoPostBack="true" OnCheckedChanged="chk_naghs_CheckedChanged" />نقص پرونده
                      </div>
                
                 <div class="col-md-2" style="background:#6cb7f0;padding:1%;text-align:center;margin-left:1%;color:#fff">
                      <asp:CheckBox id="chk_termstatus" runat="server" style="margin-left:5px" AutoPostBack="true" OnCheckedChanged="chk_termstatus_CheckedChanged" />وضعیت ترم
                      </div>
                     
                 <div class="col-md-1" style="background:#4264aa;padding:1%;text-align:center;color:#fff;margin-left:1%">
                    <asp:RadioButton ID="rdb_guest" runat="server" GroupName="a"  Style="margin-left:5px" AutoPostBack="true" OnCheckedChanged="rdb_guest_CheckedChanged"/>مهمان به
                      </div>
                  <div class="col-md-1" style="background:#ce93d8 ;padding:1%;text-align:center;margin-left:1%;color:#fff">
                   <asp:RadioButton ID="rdb_enteghal" runat="server" GroupName="a" Style="margin-left:5px" OnCheckedChanged="rdb_enteghal_CheckedChanged" AutoPostBack="true" />انتقالی از
                      </div>
                  <div class="col-md-2" style="background:#33b5e5;padding:1%;text-align:center;margin-left:1%;color:#fff">
                    <asp:CheckBox ID="chk_takhir" runat="server"   Style="margin-left:5px" AutoPostBack="true" OnCheckedChanged="rdb_takhir_CheckedChanged"/>ثبت نام با تاخیر
                      </div>
                <div class="col-md-1" style="background:#9575cd  ;padding:1%;text-align:center;margin-left:1%;color:#fff">
                    <asp:RadioButton ID="rdb_moadelsazi" runat="server"  GroupName="a" Style="margin-left:1px" AutoPostBack="true" OnCheckedChanged="rdb_moadelsazi_CheckedChanged"/>معادلسازی
                      </div>
                </div>
            </div>
            
        <div class="row" id="termstatuspnl" runat="server"  visible="false">
             <div class="col-md-12">
                  <div class="col-md-3"></div>
                  <div class="col-md-6" style="color:#000;border: 1px solid rgba(108, 183, 240,0.7);background-color:rgba(108, 183, 240,0.2);padding: 1%;border-radius:5px; margin-bottom:1%;margin-top:2px">
                      <p style="color: #CC0000"><span style="font-size: large">*</span>در صورتیکه که هیج وضعیتی را انتخاب ننمایید تمامی وضعیت ها در نظر گرفته خواهد شد</p> 
                      <div class="col-md-4">وضعیت ترم را انتخاب نمایید:</div>
                      <div class="col-md-5">
                           <telerik:RadComboBox ID="ddl_VaziiatTerm" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="WebBlue">
                               <Items>
                                   <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                   <telerik:RadComboBoxItem runat="server" Text="عادی" Value="1" />
                                   <telerik:RadComboBoxItem runat="server" Text="مرخصی با احتساب" Value="2" />
                                   <telerik:RadComboBoxItem runat="server" Text="محروم" Value="3" />
                                   <telerik:RadComboBoxItem runat="server" Text="میهمان به" Value="4" />
                                   <telerik:RadComboBoxItem runat="server" Text="حذف ترم" Value="5" />
                                   <telerik:RadComboBoxItem runat="server" Text="انتقال به" Value="6" />
                                   <telerik:RadComboBoxItem runat="server" Text="انصراف" Value="7" />
                                   <telerik:RadComboBoxItem runat="server" Text="فارغ التحصیلان" Value="8" />
                                   <telerik:RadComboBoxItem runat="server" Text="اخراج" Value="9" />
                                   <telerik:RadComboBoxItem runat="server" Text="ارجاع به استاد" Value="10" />
                                   <telerik:RadComboBoxItem runat="server" Text="مجوز ثبت نام" Value="11" />
                                   <telerik:RadComboBoxItem runat="server" Text="ارجاع به فارغ التحصیلان" Value="12" />
                                   <telerik:RadComboBoxItem runat="server" Text="مرخصی بدون احتساب" Value="13" />
                                   <telerik:RadComboBoxItem runat="server" Text="انتقال از" Value="14" />
                                   <telerik:RadComboBoxItem runat="server" Text="حذف ترم ماده 38 ت 2" Value="15" />
                                   <telerik:RadComboBoxItem runat="server" Text="محروم بدون احتساب" Value="16" />
                                   <telerik:RadComboBoxItem runat="server" Text="حذف ترم ماده 38" Value="17" />
                               </Items>
                     </telerik:RadComboBox>
                      </div>
                       <div class="col-md-3"></div>
                  </div>
                  <div class="col-md-3"></div>
             </div>
        </div>
               
          <div class="row" id="naghspnl" runat="server"  visible="false">
             <div class="col-md-12">
                 <div class="row">
                  <div class="col-md-3"></div>
                  <div class="col-md-6" style="color:#000;border: 1px solid rgba(255, 95, 79,0.7);background-color:rgba(255, 95, 79,0.2);padding: 1%;border-radius:5px; margin-bottom:1%;margin-top:2px">
                       <p style="color: #CC0000"><span style="font-size: large">*</span>در صورتیکه که هیج گزینه ای را انتخاب ننمایید تمامی موارد در نظر گرفته خواهد شد</p>
                       <div class="col-md-4">نوع نقص را انتخاب نمایید:</div>
                      <div class="col-md-5">
                           <telerik:RadComboBox ID="ddl_naghs" Runat="server" EmptyMessage="انتخاب نمایید..." Skin="Sunset">
                               <Items>
                                    <telerik:RadComboBoxItem runat="server" Text="هیچ کدام" Value="0" />
                                   <telerik:RadComboBoxItem runat="server" Text="مدرک تحصیلی" Value="35" />
                                   <telerik:RadComboBoxItem runat="server" Text="نظام وظیفه" Value="6" />
                                   <telerik:RadComboBoxItem runat="server" Text="عکس پرسنلی" Value="8" />
                                   <telerik:RadComboBoxItem runat="server" Text="صفحه آخر شناسنامه" Value="4" />
                                   <telerik:RadComboBoxItem runat="server" Text="صفحه اول شناسنامه" Value="2" />
                                   <telerik:RadComboBoxItem runat="server" Text="صفحه دوم شناسنامه" Value="3" />
                                   <telerik:RadComboBoxItem runat="server" Text="روی کارت ملی" Value="1" />
                                   <telerik:RadComboBoxItem runat="server" Text="پشت کارت ملی" Value="28" />
                                   <telerik:RadComboBoxItem runat="server" Text="سهمیه" Value="21" />
                                  
                               </Items>
                     </telerik:RadComboBox>
                      </div>
                       <div class="col-md-3"></div>
                      </div>
                 
                  <div class="col-md-3"></div> 

                 </div>

                 <div class="row">
                  <div class="col-md-3"></div>
                  <div class="col-md-6" style="color:#000;border: 1px solid rgba(255, 95, 79,0.7);background-color:rgba(255, 95, 79,0.2);padding: 1%;border-radius:5px; margin-bottom:1%;margin-top:2px">
                       <p style="color: #CC0000"><span style="font-size: large">*</span>در صورتیکه که تاریخ انتخاب نشود همه موارد در نظر گرفته خواهد شد</p>
                       <div class="col-md-2">از تایخ:</div>
                      <div class="col-md-7">
                         <div class="example">
                        <input id="date_input_1" type="text" runat="server" value="0" />
                        <img id="date_btn_1" src="../../../Adobe/images/cal.png" style="vertical-align: top;" />
                        <script type="text/javascript">

                            Calendar.setup({
                                
                                inputField: '<%# date_input_1.ClientID %>',   // id of the input field
                                button: "date_btn_1",   // trigger for the calendar (button ID)
                                ifFormat: "%Y/%m/%d",       // format of the input field
                                dateType: 'jalali',
                                weekNumbers: false
                            });
			    </script>
                        <script type="text/javascript">

                            setActiveStyleSheet(document.getElementById("defaultTheme"), "Aqua");
		        </script>
                    </div>
                      </div>
                       <div class="col-md-3"></div>
                      </div>
                 
                  <div class="col-md-3"></div> 

                 </div>
             </div>
        </div>
        
          <div class="row" id="guestpnl" runat="server" visible="false">
             <div class="col-md-12">
                  <div class="col-md-3"></div>
                  <div class="col-md-6" style="color:#000;border: 1px solid rgba(103, 149, 243,0.7);background-color:rgba(103, 149, 243,0.2);padding: 1%;border-radius:5px; margin-bottom:1%;margin-top:2px">
                       
                      <div class="col-md-4"> <asp:CheckBox id="chk_guest_term" runat="server"  style="margin-left:5px;" />دانشجوی ترم جاری باشد</div>
                      <div class="col-md-5">
                         
                      </div>
                       <div class="col-md-3"></div>
                  </div>
                  <div class="col-md-3"></div>
             </div>
        </div> 
               
          <div class="row" id="adamemorajepnl" runat="server" visible="false">
               
             <div class="col-md-12">
                  <div class="col-md-3"></div>
                  <div class="col-md-6" style="color:#000;border: 1px solid rgba(69, 212, 198,0.7);background-color:rgba(69, 212, 198,0.2);padding: 1%;border-radius:5px; margin-bottom:1%;margin-top:2px">
                      <p style="color: #CC0000"><span style="font-size: large">*</span>انتخاب یکی از موارد الزامی می باشد</p>
                      <div class="row">
                       <div class="col-md-4">  <asp:RadioButton ID="rdb3" runat="server" GroupName="ap" />بدون اخذ پایان نامه</div>
                      <div class="col-md-4">
                         <asp:RadioButton ID="rdb1" runat="server" GroupName="ap" /> با اخذ پایان نامه
                      </div>
                       <div class="col-md-4">
                          <asp:RadioButton ID="rdb2" runat="server" GroupName="ap" />با نمره پایان نامه
                       </div></div>
                       <div class="row">
                       <div class="col-md-4">  <asp:RadioButton ID="rdb_1temadamemoraje" runat="server" GroupName="ap" />دانشجویان عادی یک ترم عدم مراجعه</div>
                      <div class="col-md-4">
                         <asp:RadioButton ID="rdb_2temadamemoraje" runat="server" GroupName="ap" /> دانشجویان عادی بیش از دو ترم عدم مراجعه
                      </div>
                       </div>
                  </div>
                  <div class="col-md-3"></div>
             </div>
       
        </div>   

                                                            
          </div></ContentTemplate>
                     <Triggers><asp:AsyncPostBackTrigger ControlID="rdb_adamemoraje"  EventName="CheckedChanged" />
                         <asp:AsyncPostBackTrigger ControlID="rdb_guest"  EventName="CheckedChanged" />
                         <asp:AsyncPostBackTrigger ControlID="rdb_enteghal"  EventName="CheckedChanged" />
                         <asp:AsyncPostBackTrigger ControlID="chk_takhir"  EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="chk_naghs"  EventName="CheckedChanged" />
                          <asp:AsyncPostBackTrigger ControlID="chk_termstatus"  EventName="CheckedChanged" />
                         <asp:AsyncPostBackTrigger ControlID="rdb_moadelsazi" EventName="CheckedChanged" />
                     </Triggers>
                </asp:UpdatePanel>
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
         <cc1:stiwebviewer ID="StiWebViewer1" runat="server" Height="1100px" Width="1050px" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true" />
</asp:Content>
