<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListPayProf.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListPayProf" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
    <asp:Literal ID="pt" runat="server"></asp:Literal>
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
            <sup>*</sup>
            </span>
        </div>
       <div  class="col-md-1" > <asp:Label ID="lbl_Term" runat="server" Text="ترم :" Width="150px"></asp:Label></div>
        <div  class="col-md-2" >  <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel>          </div>
    </div>
            <div class="row">
       <div class="col-md-3"></div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div  class="col-md-1" > <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" Width="150px"></asp:Label></div>
                   <div class="col-md-2">
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                             <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="100%" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>
                   </div>
            </div>
              <div class="row">
       <div class="col-md-3">  </div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div  class="col-md-1" ><asp:Label ID="lbl_Departman" runat="server" Text="دپارتمان :" Width="150px"></asp:Label></div>
                  <div class="col-md-2"> <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                             <ContentTemplate>
                              <asp:DropDownList ID="ddl_Departman" runat="server" OnSelectedIndexChanged="ddl_Departman_SelectedIndexChanged1" Width="100%" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Departman" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>  </div>
              </div>
            <div class="row">
       <div class="col-md-3">  </div>
        <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div  class="col-md-1" >  <asp:Label ID="lbl_Field" runat="server" Text="رشته  :" Width="150px" ></asp:Label></div>
                 <div class="col-md-2">  <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Field" runat="server" Width="100%" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>   </div>
            </div>
            <div class="row">
       <div class="col-md-3">  </div>
       <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div  class="col-md-1" >   <asp:Label ID="lbl_Cooperation" runat="server" Text="نحوه همکاری :" Width="150px" ></asp:Label> </div>
                  <div class="col-md-2">    <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
                             <ContentTemplate>
                     <asp:DropDownList ID="ddl_Cooperation" runat="server" OnSelectedIndexChanged="ddl_Cooperation_SelectedIndexChanged" Width="100%" AutoPostBack="true" CssClass="form-control input-sm">
                    </asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Cooperation" EventName="SelectedIndexChanged" />
                             </Triggers>
                    </asp:UpdatePanel>  </div>
            </div>
           <div class="row">
             <div class="col-md-3">  </div>
             <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div  class="col-md-1" >  <asp:Label ID="lbl_CodeOstad" runat="server" Width="150px" Text="کد استاد :" ></asp:Label> </div>
                <div  class="col-md-1" >  <asp:TextBox ID="txt_CodeOstad" runat="server" Width="100%" CssClass="form-control"></asp:TextBox> </div>
                <div  class="col-md-2" >  <asp:Button ID="btn_SelectCodeOstad" runat="server" Text="انتخاب کنید" Width="100%" OnClick="btn_SelectCodeOstad_Click" CssClass="btn btn-success" /></div>
           </div>
             <div class="row">
             <div class="col-md-3">  </div>
            <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div  class="col-md-1" >  <asp:Label ID="lbl_Zarib" runat="server" Text="ضریب هفته :"></asp:Label> </div>
                    <div class="col-md-3">  <asp:TextBox ID="txt_Zarib" runat="server" Text="17" Width="50px" CssClass="form-control"></asp:TextBox>  </div>
             </div>
         
        <div class="row">  
          <div class="col-md-4"></div>  
          <div class="col-md-4" style="margin-right:200px; margin-top:20px"><asp:Button ID="btn_ShowReport" runat="server" Text="نمایش اطلاعات" Width="150px" CssClass="btn btn-primary" OnClick="btn_ShowReport_Click" /></div>
        </div>      
         <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namemadrak" HeaderText="مدرک تحصیلی" />
                    <asp:BoundField DataField="namemartabeh" HeaderText="نام" />
                    <asp:BoundField DataField="dvk_nazari" HeaderText="واحد نظری(مقطع کاردانی-کارشناسی)" />
                    <asp:BoundField DataField="dva_nazari" HeaderText="واحد نظری(مقطع کارشناسی ارشد - دکتری حرفه ای)" />
                    <asp:BoundField DataField="dvd_nazari" HeaderText="واحد نظری(مقطع دکتری تخصصی)" />
                    <asp:BoundField HeaderText="واحد عملی(مقطع کاردانی - کارشناسی)" />
                    <asp:BoundField HeaderText="واحد عملی (مقطع کارشناسی ارشد - دکتری حرفه ای)" />
                    <asp:BoundField DataField="dva_aa_amali" HeaderText="واحد عملی (مقطع دکتری تخصصی)" />
                    <asp:BoundField DataField="dva_aa_amali" HeaderText="کارآموز/کارورز" />
                    <asp:BoundField DataField="sumkol" HeaderText="جمع کل" />
                    <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موطفی" />
                    <asp:BoundField DataField="mazad" HeaderText="مازاد/کسر" />
                    <asp:BoundField DataField="saat_ghebat" HeaderText="ساعت غیبت" />
                    <asp:BoundField DataField="rial_ost" HeaderText="ارزش ریالی یک ساعت" />
                    <asp:BoundField DataField="mab" HeaderText="مبلغ قابل پرداخت" />
               </Columns>
             </asp:GridView>
             </div>   
    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="1000px" Width="1050px"  ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true"/>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
           <uc1:accesscontrol ID="AccessControl1" runat="server" />
</div>
</asp:Content>
