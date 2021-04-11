<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSGovahiRequestMaster.Master" AutoEventWireup="true" CodeBehind="ConfirmStudentGovahiNotPrinted.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ConfirmStudentGovahiNotPrinted" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
        <link href="../../Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">


            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                    $find("<%= grd_GovahiRequest.ClientID %>").get_masterTableView().rebind();
                }
            }


        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="rwmValidations" runat="server">
    </telerik:RadWindowManager>
    <div>
        <asp:Label ID="lbl_Nimsal" runat="server" Visible="false"></asp:Label>
    </div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
    <div style="width: 100%" dir="rtl">
        <div dir="rtl">
            <telerik:RadGrid ID="grd_GovahiRequest" Skin="MyCustomSkin" AllowFilteringByColumn="true" runat="server" AllowPaging="true" PageSize="50" OnItemDataBound="grd_GovahiRequest_ItemDataBound" AutoGenerateColumns="false" OnNeedDataSource="grd_GovahiRequest_NeedDataSource" OnItemCommand="grd_GovahiRequest_ItemCommand" Width="100%" EnableEmbeddedScripts="True" EnableEmbeddedSkins="False">

                <MasterTableView HorizontalAlign="Center" Width="100%">
                    <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" HorizontalAlign="Center" />
                    <FilterItemStyle HorizontalAlign="Center" />

                    <RowIndicatorColumn Visible="False">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn Created="True">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%#(grd_GovahiRequest.CurrentPageIndex * grd_GovahiRequest.PageSize)+ Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" ItemStyle-HorizontalAlign="Center" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام" ItemStyle-HorizontalAlign="Center" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" ItemStyle-HorizontalAlign="Center" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="salvorud" HeaderText="سال ورود" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="reshname" HeaderText="رشته" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn AllowFiltering="false">

                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Button ID="btn_taeid" Text="چاپ آدرس روی پاکت" CssClass="btn-info" CommandName="taeiddarkhast" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                        <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" EditText=" کد مرسوله پستی" UpdateText="ویرایش">
                            <ItemStyle Font-Names="Tahoma" Font-Size="Small" HorizontalAlign="center" Width="80px" ForeColor="blue" />
                        </telerik:GridEditCommandColumn>

                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="hd_Field"  Value='<%#Eval("StudentRequestID")%>' runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
                    <EditFormSettings UserControlName="TextBoxCodePost.ascx"
                        EditFormType="WebUserControl">
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

        <br />
        <uc1:AccessControl ID="AccessControl1" runat="server" />
    </div>
    <div>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
    </div>
</asp:Content>
