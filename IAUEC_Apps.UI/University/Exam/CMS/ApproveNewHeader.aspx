<%@ Page Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    AutoEventWireup="true" CodeBehind="ApproveNewHeader.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ApproveNewHeader" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .searchBox {
            direction: rtl;
            line-height: 30px;
        }
        .resultBox{
            direction:rtl;
        }
    </style>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />



    <script type="text/javascript">
        function openShowFileInPopup(path) {
            setTimeout(function () { window.radopen(path, "UserListDialog"); }, 1000);
            return false;
        }

        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">
            function openModal() {
                $('#exampleModal').modal('show');
            }

            function refreshGrid(arg) {
                if (!arg) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
                    $find("<%= grd_Class.ClientID %>").get_masterTableView().rebind();
                }
            }
        </script>
    </telerik:RadCodeBlock>



    <div class="row searchBox">
        <div class="col-sm-1">دانشکده:</div>
        <div class="col-sm-2">
            <asp:DropDownList runat="server" ID="ddlCollege" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-sm-1">وضعیت:</div>
        <div class="col-sm-2">
            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                <asp:ListItem Text="همه" Value="0"></asp:ListItem>
                <asp:ListItem Text="بررسی نشده" Value="1"></asp:ListItem>
                <asp:ListItem Text="تایید سربرگ جدید" Value="2"></asp:ListItem>
                <asp:ListItem Text="رد سربرگ جدید" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:UpdatePanel runat="server" ID="upnlDateTime">
            <ContentTemplate>
                <div class="col-sm-1">تاریخ:</div>
                <div class="col-sm-2">
                    <asp:DropDownList runat="server" ID="ddlExamDate" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-sm-1">ساعت:</div>
                <div class="col-sm-1">
                    <asp:DropDownList runat="server" ID="ddlExamTime" CssClass="form-control"></asp:DropDownList>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="col-sm-1">
            <asp:Button runat="server" ID="btnFillGrid" CssClass="btn btn-info" OnClick="btnFillGrid_Click" Text="نمایش" />
        </div>
    </div>
    <div class="row resultBox">
        <div class="col-sm-12">
            <telerik:RadGrid HorizontalAlign="Center" ID="grd_Class" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" OnItemDataBound="grd_Class_ItemDataBound"
                OnItemCommand="grd_Class_ItemCommand" AllowFilteringByColumn="True" Skin="MyCustomSkin" EnableEmbeddedSkins="false"
                OnNeedDataSource="grd_Class_NeedDataSource">
                <MasterTableView DataKeyNames="coursecode">
                    <ItemStyle />
                    <HeaderStyle HorizontalAlign="Center" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" DataField="coursecode" HeaderText="کد کلاس">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="namedars" HeaderText="نام درس">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="dateexam" HeaderText="تاریخ امتحان">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" DataField="osname" HeaderText="نام استاد">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" DataField="HeaderStatusTitle" HeaderText="وضعیت سربرگ">
                        </telerik:GridBoundColumn>

                        <%--<telerik:GridBoundColumn AllowFiltering="false" DataField="FirstUploadDate" HeaderText="تاریخ اولین بارگذاری">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="LastModifiedDate" HeaderText="آخرین تغییر">
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <HeaderTemplate>
                                سوال
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnPassword" Value='<%# ((System.Data.DataRow)Container.DataItem).ItemArray[18] %>' />
                                <%--<asp:Button ID="btnShowNewHeaderForAll" runat="server" Enabled="true" CommandName="ShowNewHeaderForAll" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem).ItemArray[11] %>' CssClass="btn btn-info form-control" Text="نمایش سربرگ جدید برای همه" />--%>
                                <asp:Button ID="btnShowNewHeader" runat="server" Enabled="true" CommandName="ShowNewHeader" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem).ItemArray[11] %>' CssClass="btn btn-info form-control" Text="نمایش سربرگ جدید" />
                                <asp:Button ID="btnShowOldHeader" runat="server" Enabled="true" CommandName="ShowOldHeader" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem).ItemArray[11] %>' CssClass="btn btn-info form-control" Text="نمایش سربرگ اصلی" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="عملیات">
                            <ItemStyle  HorizontalAlign="Right" Width="100px" />
                            <ItemTemplate>
                                <asp:Button ID="btnApproveNewHeader" runat="server" CommandName="ApproveNewHeader" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem).ItemArray[13] %>' CssClass="btn btn-success form-control" Text="تائید سربرگ جدید" />
                                <asp:Button ID="btnRejectNewHeader" runat="server" CommandName="RejectNewHeader" CommandArgument='<%# ((System.Data.DataRow)Container.DataItem).ItemArray[13] %>' CssClass="btn btn-danger form-control" Text="رد سربرگ جدید" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>


    <telerik:RadWindowManager ID="rwm" runat="server" Width="800px" Height="600px"></telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="LsitLoadingPanel" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    </telerik:RadAjaxManager>
</asp:Content>
