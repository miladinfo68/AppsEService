<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcceptAgreement.aspx.cs" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" Inherits="IAUEC_Apps.UI.University.CooperationRequest.AcceptAgreement" %>


<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">

    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <script type="text/javascript">
        function openModal() {
            debugger;
            $('#historyModal').modal('show');
        }
    </script>

    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="direction: rtl;">
        <div class="row">
            <div class="col-sm-12">
                <asp:ValidationSummary runat="server" ID="vsSearch" CssClass="alert alert-danger" ValidationGroup="search" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="searchBox">
                    <asp:RequiredFieldValidator runat="server" ID="rfvAgreementType" ValidationGroup="search" InitialValue="-1" ControlToValidate="ddlAgreementType"
                        Display="None" ErrorMessage="وضعیت تفاهم نامه را انتخاب کنید."></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator runat="server" ID="rfvUserType" ValidationGroup="search" InitialValue="-1" ControlToValidate="ddlUserType"
                        Display="None" ErrorMessage="نوع کاربری را انتخاب کنید."></asp:RequiredFieldValidator>

                    <div class="row">
                        <div class="col-sm-7">
                            <div class="col-sm-5">
                                <asp:DropDownList runat="server" ID="ddlUserType" ValidationGroup="search" CssClass="form-control">
                                    <asp:ListItem Text="نوع کاربری" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlAgreementType" ValidationGroup="search" CssClass="form-control">
                                    <asp:ListItem Text="وضعیت تفاهم نامه" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="تفاهم نامه های تأیید شده" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="تفاهم نامه های تأیید نشده" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField runat="server" ID="hdnType" />
                            </div>
                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" ValidationGroup="search" Text="جستجو"
                                CssClass="btn btn-info col-sm-2" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <telerik:RadGrid runat="server" ID="grdAgreement" PageSize="50" AutoGenerateColumns="false" AllowPaging="true"
                OnItemCommand="grdAgreement_ItemCommand" EnableEmbeddedSkins="false" AllowFilteringByColumn="True"
                OnNeedDataSource="grdAgreement_NeedDataSource" Skin="MyCustomSkin">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <%# Container.DataSetIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="code_ostad" HeaderText="کد استاد"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="statusName" HeaderText="وضعیت"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="عملیات" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnShowAgreement" Text="نمایش" CssClass="btn btn-info" CommandName="ShowAgreement" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"hrId")  %>' />
                                <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"hrid") %>' ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="refreshGrid" runat="server" />
        </div>

    </div>
    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" id="historyModal" style="background-color: aqua;">

        <div class="modal-dialog" role="document" style="width: 70%">
            <div class="modal-content bg-info border-dark">
                <div class="modal-header" dir="rtl">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                        <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                        </span>
                    </button>
                    <div class="modal-header bg-orange" dir="rtl">
                        <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه فعالیت</h4>
                        <h5 class="modal-title" id="TeacherName" runat="server"></h5>
                    </div>
                </div>
                <div class="modal-body">


                    <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border: black; border-width: thick">
                        <tr class="bg-blue-sky" style="text-align: center, right;">
                            <th></th>
                            <th style="text-align: center">نام کاربر</th>
                            <th style="text-align: center">تاریخ</th>
                            <th style="text-align: center">ساعت</th>
                            <th style="text-align: center">رویداد</th>
                            <%--<th style="text-align: center">توضیحات</th>--%>
                        </tr>

                        <asp:ListView ID="lst_history" runat="server">
                            <ItemTemplate>

                                <tr class="bg-blue" style="text-align: center;">

                                    <td></td>
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
                                    <%--<td>
                                        <%#Eval("Description") %>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>
                </div>
            </div>

        </div>
    </div>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbSubmitChanges">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwShowAgreement" />
                    <telerik:AjaxUpdatedControl ControlID="loadingPanelWrapper" LoadingPanelID="ralpConfiguration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock runat="server" ID="rdbScripts">
        <script type="text/javascript">

            function openWin(id) {
                setTimeout(function () {
                    var oWnd = $find("<%=rwShowAgreement.ClientID%>");
                    oWnd.setUrl('ShowAgreement.aspx?' + id);
                    oWnd.show();
                    oWnd.center();
                }, 200);
            }
            function closeWin() {
                var oWnd = $find("<%=rwShowAgreement.ClientID%>");
                oWnd.hide();
            }
            function refreshGrid() {
                document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
            }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpConfiguration"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwShowAgreement" runat="server" MinWidth="920px" Height="750px"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <uc1:AccessControl runat="server" ID="AccessControl" />

</asp:Content>
