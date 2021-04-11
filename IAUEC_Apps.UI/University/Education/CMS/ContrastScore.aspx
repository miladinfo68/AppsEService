<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/Education/MasterPages/EducationMasterPage.Master" AutoEventWireup="true" CodeBehind="ContrastScore.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.ContrastScore" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ContrastScore.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.ContrastScore"  %>
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
       <div class="col-md-1"><asp:Label ID="lbl_Term" Text="ترم :" runat="server" Width="110px"></asp:Label></div>
       <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                              <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" Width="150px" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                </asp:UpdatePanel>
     </div></div>
       <div class="row">
      <div class="col-md-3"></div>
                  <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
       <div class="col-md-1"> <asp:Label ID="lbl_AsMoshakhase" runat="server" Text=" از مشخصه :" width="110px"></asp:Label></div>
       <div class="col-md-1">
                <asp:TextBox ID="txt_AzMoshakhase" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
</div></div>
       <div class="row">
      <div class="col-md-3"></div>
         <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
         </div> 
       <div class="col-md-1"><asp:Label ID="lbl_TaMoshakhase" runat="server" Width="110px" Text="تا مشخصه :"></asp:Label></div>
        <div class="col-md-1">
                <asp:TextBox ID="txt_TaMoshakase" runat="server" Width="150px" CssClass="form-control" ></asp:TextBox>
</div></div>
        <div class="row">
      <div class="col-md-3"></div>
                     <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
       <div class="col-md-1">
                <asp:Label ID="lbl_Field" runat="server" Width="110px" Text="رشته ارائه دهنده :"></asp:Label></div>
<div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_Field" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Field_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Field" EventName="SelectedIndexChanged" />
                             </Triggers>
                </asp:UpdatePanel>             
  </div></div>
<div class="row">
<div class="col-md-3"></div>
          <div class="col-md-2">
            <span style="color:red; font-size:small;">
            <sup></sup>
            </span>
         </div> 
<div class="col-md-1"><asp:Button ID="Btn_Show" Text="نمایش اطلاعات" runat="server" OnClick="Btn_Show_Click" CssClass="btn btn-success"/> </div></div>
          <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false" />
            <asp:GridView ID="gv_Show" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="stcode" HeaderText="شماره دانشجویی" />
                    <asp:BoundField DataField="namedars" HeaderText=" درس" />
                    <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                    <asp:BoundField DataField="v_nazari" HeaderText="واحد نظری" />
                    <asp:BoundField DataField="v_amali" HeaderText="واحد عملی" />
                    <asp:BoundField DataField="mark_ghadim" HeaderText="نمره امتحان" />
                    <asp:BoundField DataField="mark_tajdid" HeaderText="نمره تجدید نظر" />
                </Columns>
            </asp:GridView>
            </div> </div> 
<cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
<telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager><uc1:AccessControl ID="AccessControl1" runat="server" />
</div>
</asp:Content>