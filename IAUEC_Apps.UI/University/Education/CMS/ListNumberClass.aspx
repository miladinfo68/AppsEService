<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ListNumberClass.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListNumberClass" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListNumberClass.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.ListNumberClass" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
 <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
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
       <div class="col-md-1"><asp:Label runat="server" ID="lbl_Term" Text=" :ترم" Font-Size="Small" Font-Names="Tahoma" Width="120px" dir ="ltr"></asp:Label></div>
       <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                       <ContentTemplate>
                            <asp:DropDownList ID="ddl_Term" runat="server"  OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" AutoPostBack="true" width="150px" CssClass="form-control input-sm"></asp:DropDownList>
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
       <div class="col-md-1"> <asp:Label runat="server" Font-Names="Tahoma" Font-Size ="Small" ID="lbl_LocationClass" Text=": محل کلاس" Width="120px" dir="ltr"></asp:Label></div>
      <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                       <ContentTemplate>
                             <asp:DropDownList ID="ddl_LocationClass" runat="server" AutoPostBack="true" direction="rtl"  onselectedindexchanged="ddl_LocationClass_SelectedIndexChanged" Width="150px" CssClass="form-control input-sm" ></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_LocationClass" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel></div></div>
        <div class="row">
      <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
        </div>
       <div class="col-md-1"> <asp:Label runat="server" Id="lbl_NumberClass" Text=": شماره کلاس" Font-Size="Small" Font-Names="Tahoma" Width="130px" dir="ltr" ></asp:Label></div>
       <div class="col-md-1">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                       <ContentTemplate>
                              <asp:DropDownList ID="ddl_NumberClass" runat="server"  onselectedindexchanged="ddl_NumberClass_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddl_NumberClass" EventName="SelectedIndexChanged" />
                        </Triggers>
                </asp:UpdatePanel>            
       <div class="col-md-2"><asp:Button runat="server" ID="btn_PrintList" Text="نمایش اطلاعات"  Width="120px" OnClick="btn_PrintList_Click" CssClass="btn btn-success"/>
       </div></div></div>

           <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="tterm" HeaderText="نیمسال" />
                    <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="nameostad" HeaderText="نام استاد" />
                    <asp:BoundField DataField="namedars" HeaderText="درس " />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="idroz1" HeaderText="روز" />
                    <asp:BoundField DataField="saatstart" HeaderText=" ساعت شروع" />
                    <asp:BoundField DataField="saatend" HeaderText="ساعت پایان" />
                </Columns>
            </asp:GridView>
            </div>
            <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager>
            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" /><uc1:AccessControl ID="AccessControl1" runat="server" />
        </div>   
</asp:Content>
