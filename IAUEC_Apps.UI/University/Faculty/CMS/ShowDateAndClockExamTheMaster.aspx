<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ShowDateAndClockExamTheMaster.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ShowDateAndClockExamTheMaster" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server"><title><asp:Literal ID="t" runat="server"></asp:Literal></title>
<%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--     <script type="text/javascript">
</script>--%>
    <div class="Report-Area">
    <div class="row">
     <div class="col-md-3"></div>
          <div class="col-md-1">
            <span style="color:red; font-size:small;">
            <sup>*</sup>
            </span>
        </div>
      <div  class="col-md-1" ><asp:Label ID="lbl_Term" runat="server" Text="ترم :"></asp:Label></div>
        <div class="col-md-2"><asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList runat ="server" ID="ddl_NewType"  OnSelectedIndexChanged="ddl_NewType_SelectedIndexChanged"  CssClass="form-control"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_NewType" EventName="SelectedIndexChanged" />
                             </Triggers>
                         </asp:UpdatePanel>
        </div>

        <div class="col-md-2">
          <asp:Button ID="btn_Search" runat="server"  OnClick="btn_Search_Click" Text="نمایش اطلاعات" Width="103px" CssClass="btn btn-success"/> </div></div>

    <div id="Div1" runat="server">
             <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="family" HeaderText="نام استاد" />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="saatexam" HeaderText="ساعت امتحان" />
                    <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                    <asp:BoundField DataField="namedanesh" HeaderText="نام دانشکده" />
               </Columns>
             </asp:GridView>
             </div> 

           <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1"></telerik:RadWindowManager><uc1:AccessControl ID="AccessControl1" runat="server" />

        </div>
</asp:Content>
