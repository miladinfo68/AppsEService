<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="Thesis.aspx.cs" Inherits="IAUEC_Apps.UI.University.Education.CMS.Thesis" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
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
        <div class="col-md-1"><asp:Label ID="lbl_Term" runat="server" Text="ترم :" Width="50px"></asp:Label></div>
        <div class="col-md-1">
                       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Term" runat="server" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" AutoPostBack="true" Width="150px" CssClass="form-control input-sm"></asp:DropDownList>
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
       <div class="col-md-1"><asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :" Width="150px"></asp:Label></div>
      <div class="col-md-1">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                             <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="150px" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
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
     <div class="col-md-1"></div>         
     <div class="col-md-3">
         <asp:Button ID="btn_ShowReport" runat="server" Text="نمایش اطلاعات" Width="120px" OnClick="btn_ShowReport_Click" CssClass="btn btn-success" /></div>
     </div>
     <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="false"/>
     </div>  
    <telerik:RadWindowManager ID="rwd" runat="server"></telerik:RadWindowManager>
    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" Visible="false"/>
    </div>
</asp:Content>
