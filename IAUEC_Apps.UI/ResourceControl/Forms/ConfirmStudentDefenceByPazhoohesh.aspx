<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master"
    AutoEventWireup="true" CodeBehind="ConfirmStudentDefenceByPazhoohesh.aspx.cs"
    Inherits="ResourceControl.PL.Forms.ConfirmStudentDefenceByPazhoohesh" %>


<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <link href="../../University/Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
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

        
        .btnExport{
            width:50px;
            height : 48px;
            border-radius:8px;

        }
    </style>

    <script type="text/javascript">
        function openModal() {
            setTimeout(function () { $('#historyModal').modal('show'); }, 200);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" CssClass="radWindow">
    </telerik:RadWindowManager>

    <div class="row" dir="rtl" style="margin-top: 2%">
        <div class="col-md-3" style="margin-right: 2%;">
            <asp:DropDownList ID="drpDefenceTypeList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpDefenceTypeList_SelectedIndexChanged">
                <%--<asp:ListItem Text="انتخاب کنید" Value="0" />--%>
                <asp:ListItem Text="دفاع های بررسی نشده" Value="1" />
                <asp:ListItem Text="دفاع های برگزار نشده" Value="2" />
                <asp:ListItem Text="دفاع های برگزار شده" Value="3" />
                <asp:ListItem Text="لیست تمامی دفاع ها" Value="4" />
                <%-- <asp:ListItem Text="لیست دفاع های پرداخت شده" Value="5" />--%>
            </asp:DropDownList>
        </div>
        <div class="col-md-2 col-md-pull-1">
            <asp:DropDownList ID="drpTerms" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpTerms_SelectedIndexChanged">
            </asp:DropDownList>
        </div>

        <div class="col-md-1 col-md-pull-6">
            <div>
                <asp:ImageButton ID="bt1ExportExcle" runat="server" ToolTip="خروجی اکسل" ImageUrl="../Images/microsoft excel.png" OnClick="bt1ExportExcle_OnClick" CssClass="btnExport" />
            </div>
        </div>
    </div>
    <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important; margin-top: 2% !important;">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <telerik:RadGrid HorizontalAlign="Center" ID="grdvPazhoohesh" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" AllowSorting="True"
                        OnNeedDataSource="grdvPazhoohesh_NeedDataSource"
                        OnItemCommand="grdvPazhoohesh_ItemCommand"
                        OnItemDataBound="grdvPazhoohesh_ItemDataBound"
                        AllowFilteringByColumn="True" Skin="MyCustomSkin"
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
                                <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="ID" HeaderText="شماره درخواست">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="issue_time" HeaderText="زمان ثبت درخواست">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="nameresh" HeaderText="رشته تحصیلی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="CollegeName" AllowSorting="True" HeaderText="دانشکده">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="RequestDate" HeaderText="زمان برگزرای جلسه دفاع">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StartTime" HeaderText="ساعت شروع">
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                    <ItemTemplate>
                                        <div class="row">
                                            <asp:Button ID="btnDefenceHasDone" runat="server" CommandName="DefenceHasDone" CommandArgument='<%#Int32.Parse( Eval("ID").ToString()) %>' Text="دفاع برگزار شد" ToolTip="دفاع برگزار شد" CssClass="btn btn-info" OnClientClick="if(!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد'))  return false ;" Visible="false" />
                                            <asp:Button ID="btnDefenceHasNotDone" runat="server" CommandName="DefenceHasNotDone" CommandArgument='<%#Int32.Parse( Eval("ID").ToString())  %>' ToolTip="دفاع برگزار نشد" Text="دفاع برگزار نشد" CssClass="btn btn-danger " OnClientClick="if (!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) return false ;" Visible="false" />
                                            <asp:Button ID="btnDefencePayed" runat="server" CommandName="DefencePayed" CommandArgument='<%#Int32.Parse( Eval("ID").ToString())  %>' Text="دفاع پرداخت شده" ToolTip="دفاع پرداخت شده" CssClass="btn btn-success" OnClientClick="if (!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) return false ;" Visible="false" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="تاریخچه">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnHistory" runat="server" CommandName="History" CommandArgument='<%#Int32.Parse( Eval("ID").ToString()) %>' AlternateText="تاریخچه" ToolTip="تاریخچه" ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>


                        </MasterTableView>



                    </telerik:RadGrid>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

