<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ReportConfirmsScores.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ReportConfirmsScores" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock runat="server" ID="code">
        <script>
            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
        }
        </script>
    </telerik:RadCodeBlock>
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
                <span style="color: red; font-size: small;">
                    <sup>*</sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lbl_Term" Text="ترم:" runat="server" div="center"></asp:Label>
            </div>
            <div class="col-md-1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-1">
                <asp:Label ID="Label2" Text="دانشکده:" runat="server" div="center"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlDanesh" runat="server">
                    <asp:ListItem Text="انتخاب کنید" Value="0" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lbl_CodeOstad" Text="کداستاد :" runat="server"></asp:Label></div>
            <div class="col-md-1">
                <asp:TextBox ID="txt_CodeOstad" runat="server" Width="160px"></asp:TextBox></div>
           <div class="col-md-1"></div>
            <div class="col-md-1">
                <asp:Button ID="btn_Select" runat="server" Text="انتخاب کنید" OnClick="btn_Select_Click" class="btn btn-success" /></div>
        </div>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="Label1" runat="server" Width="150px" Text="مشخصه کلاس:"></asp:Label>
            </div>
            <div class="col-md-1">
                <asp:TextBox ID="txt_did" runat="server" Width="160px" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4" style="margin-right: 100px; margin-top: 20px">
                <asp:Button ID="btn_ShowReport" runat="server" Text="نمایش اطلاعات" Width="150px" CssClass="btn btn-primary" OnClick="btn_ShowReport_Click" />
            </div>
        </div>
        <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
            AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
    </div>
    <div id="Div1" runat="server">
            <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
              RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"  AlternatingRowStyle-ForeColor="#000">
                <Columns>
                    <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                    <asp:BoundField DataField="name" HeaderText="نام" />
                    <asp:BoundField DataField="family" HeaderText="نام خانوادگی " />
                    <asp:BoundField DataField="did" HeaderText="مشخصه" />
                    <asp:BoundField DataField="namedars" HeaderText="درس" />
                    <asp:BoundField DataField="Mark" HeaderText="مورد تایید توسط استاد " />
                    <asp:BoundField DataField="Enteghal" HeaderText="مورد تایید اداره امتحانات" />
                    <asp:BoundField DataField="examTime" HeaderText="تاریخ امتحان" />
                    <asp:BoundField DataField="sabtNahayi" HeaderText="تاریخ ثبت نهایی نمره" />
                    <asp:BoundField DataField="datePasokh" HeaderText="تاریخ پاسخ به اعتراض" />
                    <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                    <asp:BoundField DataField="tel" HeaderText="موبایل" />
                </Columns>
            </asp:GridView>
            </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
   <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />      
</asp:Content>
