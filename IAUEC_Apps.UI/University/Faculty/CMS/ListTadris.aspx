<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ListTadris.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ListTadris" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <%--<link href="../../Theme/css/bootstrap.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
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
                <asp:Label ID="lbl_Term" Text="ترم :" runat="server"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
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
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lbl_Daneshkade" runat="server" Text="دانشکده :"></asp:Label></div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
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
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lbl_CodeOstad" runat="server" Text="کد استاد :"></asp:Label></div>
            <div class="col-md-1">
                <asp:TextBox ID="txt_CodeOstad" runat="server" Width="150px" CssClass="form-control"></asp:TextBox></div>
            <div class="col-md-1">
                <asp:Button ID="btn_Select" runat="server" Text="انتخاب کنید" OnClick="btn_Select_Click" CssClass="btn btn-success" /></div>
        </div>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lbl_CodeGroup" runat="server" Text="کد گروه :"></asp:Label></div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_CodeGroup" runat="server" Width="150px" OnSelectedIndexChanged="ddl_CodeGroup_SelectedIndexChanged" CssClass="form-control input-sm"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_CodeGroup" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-1">
                <asp:Label ID="lbl_Cooperation" runat="server" Text="نحوه همکاری :" Width="150px"></asp:Label></div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_Cooperation" runat="server" OnSelectedIndexChanged="ddl_Cooperation_SelectedIndexChanged" Width="150px" CssClass="form-control input-sm"></asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Cooperation" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:RadioButton ID="rdb_Tuition" runat="server" Text="جزئیات اطلاعات اساتید" GroupName="EblaghAsatid" ValidationGroup="0"  AutoPostBack="true" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdb_Tuition" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-1">
                <span style="color: red; font-size: small;">
                    <sup></sup>
                </span>
            </div>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:RadioButton ID="rdb_Tuition2" runat="server" Text=" اطلاعات مشخصه درس اساتید" GroupName="EblaghAsatid" ValidationGroup="0" AutoPostBack="true" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdb_Tuition2" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>


        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-3">
                <asp:Button ID="btn_ShowList" runat="server" Text="نمایش لیست" OnClick="btn_ShowList_Click" CssClass="btn btn-primary" /></div>
        </div>
        <div id="Div1" runat="server">
            <asp:ImageButton ID="img_ExportToExcel1" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
                AlternateText="ExcelML" OnClick="img_ExportToExcel1_Click" Visible="false" />
        </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
            RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000">
            <Columns>
                <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                <asp:BoundField DataField="name" HeaderText="نام " />
                <asp:BoundField DataField="family" HeaderText="فامیلی" />
                <asp:BoundField DataField="namep" HeaderText="نام پدر" />
                <asp:BoundField DataField="idd" HeaderText="شماره شناسنامه" />
                <asp:BoundField DataField="idd_meli" HeaderText="کد ملی" />
                <asp:BoundField DataField="jensiat" HeaderText="جنسیت" />
                <asp:BoundField DataField="sal_tav" HeaderText="سال تولد" />
                <asp:BoundField DataField="mt_name" HeaderText="محل تولد" />
                <asp:BoundField DataField="sal_madrak" HeaderText="سال اخذ مدرک" />
                <asp:BoundField DataField="name_keshvar" HeaderText="کشور محل اخذ مدرک" />
                <asp:BoundField DataField="nameuniversity" HeaderText="نام دانشگاه" />
                <asp:BoundField DataField="nametypeuniversity" HeaderText="نوع دانشگاه" />
                <asp:BoundField DataField="nameresh" HeaderText="نام رشته" />
                <asp:BoundField DataField="namemadrak" HeaderText="مدرک تحصیلی" />
                <asp:BoundField DataField="namemartabeh" HeaderText="مرتبه علمی" />
                <asp:BoundField DataField="name_nahveh" HeaderText="نحوه همکاری" />
                <asp:BoundField DataField="payeh" HeaderText="پایه" />
                <asp:BoundField DataField="saat_tadris" HeaderText="ساعت تدریس" />
                <asp:BoundField DataField="saat_movazaf" HeaderText="ساعت موظفی" />
                <asp:BoundField DataField="add_email" HeaderText="آدرس ایمیل" />
                <asp:BoundField DataField="mobile" HeaderText="موبایل" />
                <asp:BoundField DataField="add_hom" HeaderText="آدرس منزل" />
                <asp:BoundField DataField="add_kar" HeaderText="آدرس محل کار" />
                <asp:BoundField DataField="siba" HeaderText="شماره حساب" />
                <asp:BoundField DataField="sharh" HeaderText="توضیحات" />
            </Columns>
        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" CellPadding="0" CellSpacing="0" GridLines="Both" HorizontalAlign="Center" AutoGenerateColumns="false"
            RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000">
            <Columns>
                <asp:BoundField DataField="code_ostad" HeaderText="کد استاد" />
                <asp:BoundField DataField="nameostad" HeaderText="نام " />
                <asp:BoundField DataField="did" HeaderText="مشخصه" />
                <asp:BoundField DataField="namedars" HeaderText="نام درس" />
                <asp:BoundField DataField="name_mahal" HeaderText="نحوه تدریس" />
                <asp:BoundField DataField="vnazari" HeaderText="واحد نظری" />
                <asp:BoundField DataField="vamali" HeaderText="واحد عملی" />
                <asp:BoundField DataField="zarfporm" HeaderText="پرشده کلاس" />
                <asp:BoundField DataField="typedars" HeaderText="نوع درس" />
                <asp:BoundField DataField="nameresh" HeaderText="رشته" />
                <asp:BoundField DataField="namedanesh" HeaderText="دانشکده" />
                <asp:BoundField DataField="ZARIB_A" HeaderText="ضریب عملی" />
                <asp:BoundField DataField="ZARIB_N" HeaderText="ضریب نظری" />
                <asp:BoundField DataField="maghtadris" HeaderText="مقطع تدریس" />
                <asp:BoundField DataField="saat_amali" HeaderText="ساعت عملی" />
                <asp:BoundField DataField="saat_nazari" HeaderText="ساعت نظری" />
                <asp:BoundField DataField="vv" HeaderText="مجموع واحد اخذ شده با ضریب " />
            </Columns>
        </asp:GridView>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="1000px"  ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" ScrollBarsMode="true" />
    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>
