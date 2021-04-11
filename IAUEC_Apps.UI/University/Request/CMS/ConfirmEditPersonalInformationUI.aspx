<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSEditInfoRequest.Master" AutoEventWireup="true" CodeBehind="ConfirmEditPersonalInformationUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ConfirmEditPersonalInformationUI" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
            color: #000;
        }
        .RadMenu_rtl ul.rmVertical{
            background: #eee;
            border: 1px solid #ddd;
        }
        .RadMenu_rtl .rmGroup .rmLink{
            padding: 3px 0;
        }
        .RadMenu_rtl .rmGroup .rmLink:hover{
            text-decoration: none;
            background: #ddd;
        }
        .RadGridRTL .rgNumPart a{
            border: 1px solid #ccc;
            padding: 0px 8px !important;
            margin: 0 3px !important;
            border-radius: 5px;
        }
        .RadGrid .rgNumPart a.rgCurrentPage{
            background: #ddd;
            color: #000;
        }
        .RadGrid td.rgPagerCell{
            border-top: 1px solid #ccc !important;
        }
        .RadGrid .rgHeader a{
            color: #fff;
        }
    </style>
    <asp:Literal ID="pt" runat="server"></asp:Literal>


    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">

            function openWin(stcode) {
                window.radopen("EditInfoUI.aspx?stcode=" + stcode, "UserListDialog");
                return false;
            }
            function openWin1(stcode) {
                window.radopen("EditInformationUI.aspx?stcode=" + stcode, "UserListDialog");
                return false;
            }


        </script>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">


            <AjaxSettings>

                <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grd_EditeRequest" LoadingPanelID="LsitLoadingPanel"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>


            </AjaxSettings>
        </telerik:RadAjaxManager>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxLoadingPanel runat="server" ID="LsitLoadingPanel"></telerik:RadAjaxLoadingPanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div dir="rtl">
        <div class="panel panel-info">
            <div class="panel panel-heading">
                <span>درخواست ویرایش اطلاعات فردی</span>
            </div>
            <div class="panel panel-body">

        <telerik:RadGrid ID="grd_EditeRequest" AutoGenerateColumns="false" AllowFilteringByColumn="true" AllowPaging="true" AllowSorting="true" PageSize="20" OnNeedDataSource="grd_EditeRequest_NeedDataSource" runat="server" OnItemCommand="grd_EditeRequest_ItemCommand" EnableEmbeddedSkins="False" BorderStyle="Groove">
            <MasterTableView HorizontalAlign="Center" Width="100%">
                <PagerStyle PageSizeLabelText="تعداد در صفحه" ShowPagerText="false" Mode="NumericPages" />
                <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" Font-Bold="true" Font-Size="16px" />
                <FilterItemStyle CssClass="bg-blue" />

                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" AllowSorting="true" />
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" AllowSorting="true" />
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" AllowSorting="true" />
                    <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false" AllowSorting="true"></telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="type" HeaderText="نوع درخواست" AllowFiltering="true"></telerik:GridBoundColumn>--%>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Button ID="btn_ShowInfo" runat="server" Text="مشاهده اطلاعات" Style="font-family: Tahoma;  font-weight: bold; cursor: pointer;" CssClass="btn btn-primary" CommandArgument='<%#Eval("stcode") %>' CommandName="info" target="_blank" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings UserControlName="PreviousInfo.ascx"
                    EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
            <FilterMenu EnableEmbeddedSkins="False">
            </FilterMenu>
            <HeaderContextMenu EnableEmbeddedSkins="False">
            </HeaderContextMenu>
        </telerik:RadGrid>

            </div>
        </div>
         <div class="panel panel-info">
            <div class="panel panel-heading">
                <span>درخواست ویرایش عکس پرسنلی</span>
            </div>
            <div class="panel panel-body">

        <telerik:RadGrid ID="grd_Piceditrequest" FilterItemStyle-Height="40px" AutoGenerateColumns="false" AllowFilteringByColumn="true" AllowPaging="true" AllowSorting="true" PageSize="20" OnNeedDataSource="grd_Piceditrequest_NeedDataSource" runat="server" OnItemCommand="grd_Piceditrequest_ItemCommand" EnableEmbeddedSkins="False" BorderStyle="Groove">
            <MasterTableView HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="100%">
                <PagerStyle PageSizeLabelText="تعداد در صفحه" ShowPagerText="false" Mode="NumericPages" />
                <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" Font-Bold="true" Font-Size="16px" />
                <FilterItemStyle CssClass="bg-blue" />

                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" AllowSorting="true" />
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" AllowSorting="true" />
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" ItemStyle-HorizontalAlign="right" ItemStyle-Width="20%" AllowSorting="true" />
                    <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false" AllowSorting="true" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"></telerik:GridBoundColumn>

                    <telerik:GridTemplateColumn AllowFiltering="false">



                        <ItemTemplate>
                            <asp:Button ID="btn_ShowPic" runat="server" Text="مشاهده عکس " Style=" font-family: Tahoma;  font-weight: bold; cursor: pointer;" CssClass="btn btn-primary" CommandArgument='<%#Eval("stcode") %>' CommandName="pic" target="_blank" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <EditFormSettings UserControlName="PreviousInfo.ascx"
                    EditFormType="WebUserControl">

                    <EditColumn UniqueName="EditCommandColumn">
                    </EditColumn>
                </EditFormSettings>

            </MasterTableView>
            <FilterMenu EnableEmbeddedSkins="False">
            </FilterMenu>
            <HeaderContextMenu EnableEmbeddedSkins="False">
            </HeaderContextMenu>
        </telerik:RadGrid>
        
            </div>
        </div>
    </div>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>

