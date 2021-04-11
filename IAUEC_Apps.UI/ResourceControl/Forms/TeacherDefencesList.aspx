<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/ResourceControlUsers.Master"
    CodeBehind="TeacherDefencesList.aspx.cs" Inherits="ResourceControl.PL.Forms.TeacherDefencesList" %>

<%--<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>--%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>پنل مرتبط با دفاع استاد</h3>
    <link href="../../University/Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .RadGrid .rgFilterRow > td, .RadGrid_MyCustomSkin .rgAltRow td {
            border: solid #00C851;
            border-width: 0 0 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgFilterRow > td:first-child {
            border-width: 0px 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgRow > td:first-child, .RadGridRTL_MyCustomSkin .rgAltRow > td:first-child {
            border-right-width: 1px;
        }

        .searchBox {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px 15px;
            margin: 0 5px 15px;
        }

        .form-control {
            padding: 4px 12px;
        }

        .RadWindow_Default, .RadWindow table.rwTable {
            max-height: 100%;
        }

        .RadGrid_MyCustomSkin th.rgSorted {
            background-color: #10396c;
        }

        .RadGrid_MyCustomSkin .rgHeader a {
            color: white;
        }

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td, .RadGrid .rgFilterRow > td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
            padding-left: 20px !important;
        }
    </style>

    <script type="text/javascript">
        function openModal() {
            setTimeout(function () {
                $('#historyModal').modal('show');
            }, 200);
        }

        function closeRadWindow() {
            var window = $find('<%=RadWindow1.ClientID %>');
            window.close();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

    <telerik:RadWindow ID="RadWindow1" Height="300" Width="400" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelReject" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label Text="علت رد : " runat="server" CssClass="control-label" />
                                <asp:TextBox runat="server" ID="txtRejectReason" CssClass="form-control" />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <asp:Button Text="تائـــید" ID="btnConfirmReject" runat="server" OnClick="btnConfirmReject_Click" CssClass="btn btn-success" Style="width: 100%;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>




    <div class="main">
        <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important; margin-top: 2% !important;">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid HorizontalAlign="Center" ID="grdvTeacherDefenceList" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" AllowSorting="True"
                            OnNeedDataSource="grdvTeacherDefenceList_NeedDataSource"
                            OnItemCommand="grdvTeacherDefenceList_ItemCommand"
                            AllowFilteringByColumn="True"
                            Skin="MyCustomSkin"
                            EnableEmbeddedSkins="false"
                            EnableLinqExpressions="False">

                            <MasterTableView DataKeyNames="StudentCode">
                                <NoRecordsTemplate>
                                    <div class="alert alert-danger" style="text-align: center; margin-top: 5%">هیچ درخواستی وجود ندارد</div>
                                </NoRecordsTemplate>
                                <ItemStyle />
                                <HeaderStyle HorizontalAlign="Center" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="ID" HeaderText="شماره درخواست">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی دانشجو">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentMobile" HeaderText="موبایل دانشجویی">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="IssueTime" HeaderText="زمان ثبت درخواست">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="RequestDate" HeaderText="زمان برگزرای جلسه دفاع">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="StartTime" HeaderText="ساعت شروع">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="ProfossorRole" HeaderText="نقش استاد">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                        <ItemTemplate>
                                            <div class="row">
                                                <asp:Button ID="btnRejectRequest" runat="server" CommandName="RejectRequest" CommandArgument='<%#Int32.Parse( Eval("ID").ToString()) %>' Text="رد درخواست" ToolTip="رد درخواست" CssClass="btn btn-info" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="تاریخچه">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnHistory" runat="server" CommandName="History" CommandArgument='<%#Int32.Parse( Eval("ID").ToString()) %>' AlternateText="تاریخچه" ToolTip="تاریخچه" ImageUrl="../../University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>

                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="modal fade" id="historyModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="background-color: aqua;">
            <div class="modal-dialog" role="document" style="width: 70%">
                <div class="modal-content bg-info border-dark">

                    <div class="modal-header" dir="rtl">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                            <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                            </span>
                        </button>
                        <div class="modal-header bg-orange" dir="rtl">
                            <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                        </div>
                    </div>
                    <div class="modal-body">
                        <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border-bottom-color: black">
                            <tr class="bg-blue-sky">
                                <th>نام کاربر</th>
                                <th>تاریخ</th>
                                <th>ساعت</th>
                                <th>وضعیت</th>
                                <th>توضیحات</th>
                            </tr>

                            <asp:ListView ID="lst_history" runat="server">
                                <ItemTemplate>
                                    <tr class="bg-blue" style="text-align: center;">
                                        <td>
                                            <%#Eval("Name") %>
                                        </td>
                                        <td>
                                            <%#Eval("LogDate") %>
                                        </td>
                                        <td>
                                            <%#Eval("LogTime") %>
                                        </td>
                                        <td>
                                            <%#Eval("EventName") %>
                                        </td>
                                        <td>
                                            <%#Eval("Description") %>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </table>

                    </div>

                </div>

            </div>
        </div>

    </div>

</asp:Content>
