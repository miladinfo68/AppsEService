<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="NotRegistrationLicense.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.NotRegistrationLicense" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="NotRegistrationLicense.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.NotRegistrationLicense" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
 <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
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
                            <telerik:AjaxUpdatedControl ControlID="txt_StCode" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>                 
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>

 <div class="row">
  <div class="col-md-3"></div>
   <div class="col-md-2">
     <asp:Label ID="lbl_Term" runat="server" Text="ترم :" width="110px"></asp:Label></div>
      <div class="col-md-2">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
              <ContentTemplate>
                  <asp:DropDownList ID="ddl_Term" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
               </ContentTemplate>
               <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
              </Triggers>
  </asp:UpdatePanel></div></div>

 <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-2">
                <asp:Label ID="lbl_Field" runat="server" Text=" رشته تحصیلی :" width="110px"></asp:Label></div>
      <div class="col-md-2">
              <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                       <ContentTemplate>
                             <asp:DropDownList ID="ddl_Field" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>
 <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-2">
               <asp:Label ID="lbl_StCode" runat="server" Width="110px" Text=" کد دانشجو :"></asp:Label></div>
<div class="col-md-2">
              <asp:TextBox ID="txt_StCode" runat="server" Width="150px" CssClass="form-control" ></asp:TextBox></div>
             <div class="col-md-2"><asp:Button ID="btn_ShowStCode" runat="server" Text="انتخاب کنید" Width="110px" class="btn btn-success btn-lg" OnClick="btn_ShowStCode_Click" /></div></div>


     <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-2"><asp:Label ID="lbl_Degree" runat="server" Width="110px" Text="مقطع تحصیلی :"></asp:Label></div>
<div class="col-md-2">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                       <ContentTemplate>
                             <asp:DropDownList ID="ddl_Degree" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Degree" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel> </div></div>

      <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-2"><asp:Label ID="lbl_Education" runat="server" Text="سیستم آموزشی :" Width="150px"></asp:Label></div>
        <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                       <ContentTemplate>
                              <asp:DropDownList ID="ddl_Education" runat="server" OnSelectedIndexChanged="ddl_Education_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Education" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel> </div></div>

       <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-2"><asp:Label ID="lbl_Sex" Text="جنسیت :" runat="server" Width="150px"></asp:Label></div>
    <div class="col-md-2">
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                       <ContentTemplate>
                              <asp:DropDownList ID="ddl_Sex" runat="server" OnSelectedIndexChanged="ddl_Sex_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_Sex" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel> </div></div>

       <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-2"><asp:Label ID="lbl_SalVorud" runat="server" Text="سال ورود :" Width="150px"></asp:Label></div>
        <div class="col-md-2">
                    <asp:TextBox ID="txt_SalVorud" runat="server" MaxLength="2" Width="50px" CssClass="form-control" ></asp:TextBox></div></div>
       <div class="row">
      <div class="col-md-3"></div>
       <div class="col-md-6">
      <asp:Button ID="Btn_Show" Text="نمایش اطلاعات" runat="server" OnClick="Btn_Show_Click" CssClass="btn btn-success"/></div></div>
       <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="family" HeaderText="فامیلی" />
                    <asp:BoundField DataField="term" HeaderText="نیمسال " />
                    <asp:BoundField DataField="sharh" HeaderText="شرح" />
                </Columns>
            </asp:GridView>
      </div>
    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager> 
     <uc1:AccessControl ID="AccessControl1" runat="server" />
</div>
</asp:Content>

