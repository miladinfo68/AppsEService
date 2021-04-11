<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListEblaghAsatid.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListEblaghAsatid" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server"><title><asp:Literal ID="t" runat="server"></asp:Literal></title>
<%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>

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
     <div class="Report-Area" dir="rtl" style="text-align:right">
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
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1">   
          <asp:Label ID="lbl_Term" runat="server" Text="ترم :" Width="150px"></asp:Label>   
  </div>
         <div class="col-md-2">  <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
        </div>
           <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-1">     <asp:Label ID="lbl_CodeOstad" Text="کد استاد :" runat="server" Width="150px"></asp:Label>  </div>
               <div  class="col-md-1"> <asp:TextBox ID="txt_CodeOstad" runat="server" Width="150px" CssClass="form-control"></asp:TextBox></div>
               <div class="col-md-1"> <asp:Button ID="btn_SelectCodeOstad" runat="server" Text="انتخاب کنید" Width="100px" OnClick="btn_SelectCodeOstad_Click" CssClass="btn btn-success" /></div>
               </div>

      <div class="row">
     <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-1">   <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" Width="150px" ></asp:Label></div>
                       <div class="col-md-2">     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="100%" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
                   </div>
            <div class="row">

     <div class="col-md-3"></div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-1">  <asp:Label ID="lbl_Departman" runat="server" Text="کد گروه :" Width="150px"></asp:Label></div>
                   <div class="col-md-2">  <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_Departman" runat="server" OnSelectedIndexChanged="ddl_Departman_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Departman" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
            </div>
           <div class="row">
     <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
      <div  class="col-md-1">  <asp:Label ID="lbl_Cooperation" runat="server" Text="نحوه همکاری :" Width="150px"></asp:Label> </div>
                  <div class="col-md-2"><asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_Cooperation" runat="server" OnSelectedIndexChanged="ddl_Cooperation_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Cooperation" EventName="SelectedIndexChanged" />
                             </Triggers>
                   </asp:UpdatePanel></div>
           </div>
        <br />
  
           <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3"><asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                                <asp:RadioButton ID="rdb_EblaghAsatid" runat="server" Text="ابلاغ اساتید" GroupName="EblaghAsatid" ValidationGroup="0" Checked="true" AutoPostBack="true" OnCheckedChanged="EblaghAsatid_OnCheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_EblaghAsatid" />
                             </Triggers>
                   </asp:UpdatePanel></div></div>
             <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3"> <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
                             <ContentTemplate>
                                 <asp:RadioButton ID="rdb_EblaghExam" runat="server" Text="ابلاغ امتحان اساتید" GroupName="EblaghAsatid" ValidationGroup="1" AutoPostBack="true" OnCheckedChanged="EblaghAsatid_OnCheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_EblaghExam" />
                             </Triggers>
                   </asp:UpdatePanel></div>
                 <div class="col-md-2">  <asp:CheckBox ID="chk_koli" runat="server" Text="کلی بدون متن" /></div>
             </div>
   <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3"><asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                             <ContentTemplate>
                                 <asp:RadioButton ID="rdb_TedadVahed" runat="server" Text="لیست تعداد واحد انتخاب اساتید" GroupName="EblaghAsatid" ValidationGroup="2" AutoPostBack="true" OnCheckedChanged="EblaghAsatid_OnCheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_TedadVahed" />
                             </Triggers>
                   </asp:UpdatePanel></div></div>
        <br />
     <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3">  <asp:UpdatePanel ID="UpdatePanel8" runat="server" >
                             <ContentTemplate>
                                  <asp:RadioButton ID="rdb_ListSabegheByTerm" runat="server" Text="لیست سوابق تدریس اساتید" GroupName="EblaghAsatid" ValidationGroup="4" AutoPostBack="true" OnCheckedChanged="EblaghAsatid_OnCheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_ListSabegheByTerm"/>
                             </Triggers>
                   </asp:UpdatePanel></div>

         <div class="col-md-1"> <asp:Label ID="lbl_FromTerm" runat="server" Text="از ترم :" Width="50px"></asp:Label><span style="color:red"> نمونه 2-94-93</span></div>
         <div class="col-md-2">  <asp:TextBox ID="txt_FromTerm" runat="server" Text="  -  - " MaxLength="7" width="80px" CssClass="form-control"></asp:TextBox></div>
         <div class="col-md-1"> <asp:Label ID="lbl_ToTerm" runat="server" Text="تا ترم :" ></asp:Label></div>
         <div class="col-md-2">   <asp:TextBox ID="txt_ToTerm" runat="server" Text="  -  - " MaxLength="7" Width="80px" CssClass="form-control"></asp:TextBox></div>
     </div>

       
        <br />
          <div class="row">
     <div class="col-md-3"></div>
      <div  class="col-md-3"><asp:UpdatePanel ID="UpdatePanel9" runat="server" >
                             <ContentTemplate>
                                    <asp:RadioButton ID="rdb_ListSavabeghRuz" Text=" لیست سوابق تدریس اساتید در یک روز بیش از  " runat="server" width="300px" ValidationGroup="3" GroupName="EblaghAsatid" AutoPostBack="true" OnCheckedChanged="EblaghAsatid_OnCheckedChanged"/>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="rdb_ListSavabeghRuz" />
                             </Triggers>
                   </asp:UpdatePanel></div>
              <div class="col-md-2"><asp:TextBox ID="txt_ListSavabeghRuz" runat="server" CssClass="form-control" Width="50" MaxLength="1"></asp:TextBox></div>
              <div class="col-md-1"><asp:Label ID="lbl_Clock" runat="server" Text="ساعت"></asp:Label></div>
          </div>
            <div class="row">
     <div class="col-md-4"></div>
      <div  class="col-md-2"><asp:Button ID="btn_ShowReport" runat="server" Text="نمایش اطلاعات" Width="100%" OnClick="btn_ShowReport_Click" CssClass="btn btn-primary"/></div></div>
           <br />
             <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                   <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="madrak" HeaderText="مدرک تحصیلی" />
                    <asp:BoundField DataField="name_nahveh" HeaderText="نحوه همکاری" />
                    <asp:BoundField DataField="namemartabeh" HeaderText="مرتبه علمی" />
                    <asp:BoundField DataField="tterm" HeaderText="ترم" />
                    <asp:BoundField DataField="saat_nazari_karshenasi" HeaderText="ساعت نطری کارشناسی" />
                    <asp:BoundField DataField="saat_amali_karshenasi" HeaderText="ساعت عملی کارشناسی" />
                    <asp:BoundField DataField="vahed_nazari_karshenasi" HeaderText="واحد نطری کارشناسی" />
                    <asp:BoundField DataField="vahed_amali_karshenasi" HeaderText="واحد عملی کارشناسی" />
                    <asp:BoundField DataField="saat_nazari_arshad" HeaderText="ساعت نظری کارشناسی ارشد" />
                    <asp:BoundField DataField="saat_amali_arshad" HeaderText="ساعت عملی کارشناسی ارشد" />
                    <asp:BoundField DataField="vahed_nazari_arshad" HeaderText="واحد نطری کارشناسی ارشد" />
                    <asp:BoundField DataField="vahed_amali_arshad" HeaderText="واحد عملی کارشناس ارشد" />
                    <asp:BoundField DataField="saat_nazari_dok" HeaderText="ساعت نطری دکتری" />
                    <asp:BoundField DataField="saat_amali_dok" HeaderText="ساعت عملی دکتری" />
                    <asp:BoundField DataField="vahed_nazari_dok" HeaderText="واحد نطری دکتری" />
                    <asp:BoundField DataField="vahed_amali_dok" HeaderText="واحد عملی دکتری" />
                    <asp:BoundField DataField="saat_tadris" HeaderText="مجموع کل ساعت تدریس" />
                    <asp:BoundField DataField="vahed_kol" HeaderText="مجموع واحد" />
                    <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موظفی" />
                    <asp:BoundField DataField="mazad" HeaderText="مازاد یا کسر تدریس" />                  
                </Columns>
            </asp:GridView>
      </div>
             <div id="Div2" runat="server">
             <asp:ImageButton ID="img_ExportToExcel2" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel2_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="idostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="reshostad" HeaderText="رشته استاد" />
                    <asp:BoundField DataField="namemartabeh" HeaderText="مرتبه علمی" />
                    <asp:BoundField DataField="name_nahveh" HeaderText="نحوه همکاری" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد درس نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد درس عملی" />
                    <asp:BoundField DataField="saat_nazari" HeaderText="ساعت تدریس نظری" />
                    <asp:BoundField DataField="saat_amali" HeaderText="ساعت تدریس عملی" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="jensiat" HeaderText="جنسیت" />
                    <asp:BoundField DataField="zarfporm" HeaderText="ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="saatklass" HeaderText="ساعت کلاس" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موظفی" />
                    <asp:BoundField DataField="saat_tadris" HeaderText="حداکثر ساعت تدریس" />
               </Columns>
             </asp:GridView>
             </div>
             <div id="Div3" runat="server">
             <asp:ImageButton ID="img_ExportToExcel3" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel3_Click" Visible="false" />
            <asp:GridView ID="GridView3" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="idostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="nameostad" HeaderText=" نام استاد" />
                    <asp:BoundField DataField="namedars" HeaderText="درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="zarfporm" HeaderText="ظرفیت پر شده کلاس" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                    <asp:BoundField DataField="mahal_exam" HeaderText="محل امتحان" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="date_tahvil_nom" HeaderText="تاریخ تحویل نمره" />
               </Columns>
             </asp:GridView>
             </div>
                      <div id="Div4" runat="server">
             <asp:ImageButton ID="img_ExportToExcel4" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel4_Click" Visible="false" />
            <asp:GridView ID="GridView4" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="idostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="zarfporm" HeaderText="تعداد ظرفیت کلاس" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                    <asp:BoundField DataField="mahal_exam" HeaderText="محل تشکیل امتحان" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
               </Columns>
             </asp:GridView>
             </div>
                      <div id="Div5" runat="server">
             <asp:ImageButton ID="img_ExportToExcel5" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel5_Click" Visible="false" />
            <asp:GridView ID="GridView5" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                   <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="madrak" HeaderText="مدرک تحصیلی" />
                    <asp:BoundField DataField="name_nahveh" HeaderText="نحوه همکاری" />
                    <asp:BoundField DataField="namemartabeh" HeaderText="مرتبه علمی" />
                    <asp:BoundField DataField="tterm" HeaderText="ترم" />
                    <asp:BoundField DataField="saat_nazari_karshenasi" HeaderText="ساعت نطری کارشناسی" />
                    <asp:BoundField DataField="saat_amali_karshenasi" HeaderText="ساعت عملی کارشناسی" />
                    <asp:BoundField DataField="vahed_nazari_karshenasi" HeaderText="واحد نطری کارشناسی" />
                    <asp:BoundField DataField="vahed_amali_karshenasi" HeaderText="واحد عملی کارشناسی" />
                    <asp:BoundField DataField="saat_nazari_arshad" HeaderText="ساعت نظری کارشناسی ارشد" />
                    <asp:BoundField DataField="saat_amali_arshad" HeaderText="ساعت عملی کارشناسی ارشد" />
                    <asp:BoundField DataField="vahed_nazari_arshad" HeaderText="واحد نطری کارشناسی ارشد" />
                    <asp:BoundField DataField="vahed_amali_arshad" HeaderText="واحد عملی کارشناس ارشد" />
                    <asp:BoundField DataField="saat_nazari_dok" HeaderText="ساعت نطری دکتری" />
                    <asp:BoundField DataField="saat_amali_dok" HeaderText="ساعت عملی دکتری" />
                    <asp:BoundField DataField="vahed_nazari_dok" HeaderText="واحد نطری دکتری" />
                    <asp:BoundField DataField="vahed_amali_dok" HeaderText="واحد عملی دکتری" />
                    <asp:BoundField DataField="saat_tadris" HeaderText="مجموع کل ساعت تدریس" />
                    <asp:BoundField DataField="vahed_kol" HeaderText="مجموع واحد" />
                    <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موظفی" />
                    <asp:BoundField DataField="mazad" HeaderText="مازاد یا کسر تدریس" />
               </Columns>
             </asp:GridView>
             </div>
                      <div id="Div6" runat="server">
             <asp:ImageButton ID="img_ExportToExcel6" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel6_Click" Visible="false" />
            <asp:GridView ID="GridView6" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی" />
                    <asp:BoundField DataField="name" HeaderText="نام " />
                    <asp:BoundField DataField="idroz" HeaderText="روز " />
                    <asp:BoundField DataField="s_vn" HeaderText="مجموع واحد نظری" />
                    <asp:BoundField DataField="s_va" HeaderText="مجموع واحد عملی" />
                    <asp:BoundField DataField="s_sn" HeaderText="مجموع ساعت نظری" />
                    <asp:BoundField DataField="s_sa" HeaderText="مجموع ساعت عملی" />
               </Columns>
             </asp:GridView>
             </div>
     <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager><uc1:AccessControl ID="AccessControl1" runat="server" />

 </div>
</asp:Content>
