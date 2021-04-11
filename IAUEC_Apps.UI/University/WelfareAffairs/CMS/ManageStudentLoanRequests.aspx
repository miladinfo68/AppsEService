<%@ Page Title="" Language="C#" MasterPageFile="~/University/WelfareAffairs/MasterPages/StudentLoanCMS.Master" AutoEventWireup="true" CodeBehind="ManageStudentLoanRequests.aspx.cs" Inherits="IAUEC_Apps.UI.University.WelfareAffairs.CMS.ManageStudentLoanRequests" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style>
        .manageStudentLoanRequestsWrapper {
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="manageStudentLoanRequestsWrapper">
        <telerik:RadGrid runat="server" ID="rgvLoanRequests" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin"
            OnNeedDataSource="rgvLoanRequests_NeedDataSource" OnItemCommand="rgvLoanRequests_ItemCommand" OnItemDataBound="rgvLoanRequests_ItemDataBound" AllowPaging="true" PageSize="50">
            <HeaderStyle HorizontalAlign="Center" />
            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn HeaderText="شناسه درخواست" DataField="LoanId"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="نام و نام خانوادگی">
                        <ItemTemplate>
                            <span><%# Eval("StudentFirstname") + " " + Eval("StudentLastname") %></span>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="نوع درخواست" DataField="LoanTypeTitle"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StudentCode"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="عملیات">
                        <ItemTemplate>
                            <asp:Panel runat="server" ID="pnlRequestButtons">
                                <asp:Button runat="server" ID="btnViewDetails" Text="جزئیات" CommandName="ViewDetails"
                                CommandArgument='<%# Eval("LoanId") %>' CssClass="btn btn-info" />
                            <asp:Button runat="server" ID="btnAccept" Text="تائید اولیه درخواست" CommandName="Accept"
                                CommandArgument='<%# Eval("LoanId") %>' CssClass="btn btn-success" Visible="false" />
                                <asp:Button runat="server" ID="btnFinalAccept" Text="تائید نهایی درخواست (دریافت فیزیکی مدارک)" CommandName="FinalAccept"
                                CommandArgument='<%# Eval("LoanId") %>' CssClass="btn btn-success" Visible="false" />
                            <asp:Button runat="server" ID="btnReject" Text="رد درخواست وام" CommandName="Reject" CssClass="btn btn-danger" />
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlConfirmAction" Visible="false">
                                <div style="text-align: right;">
                                    <span>علت رد:</span>
                                </div>
                                <div>
                                    <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Button runat="server" ID="btnRejectRequest" CssClass="btn btn-danger" Text="رد درخواست" CommandName="RejectRequest"
                                        CommandArgument='<%# Eval("LoanId") %>' />
                                    <asp:Button runat="server" ID="btnCancelAction" CssClass="btn btn-warning" Text="انصراف" CommandName="CancelAction" />
                                </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <telerik:RadWindowManager ID="rwmMessages" runat="server"></telerik:RadWindowManager>

    <script>
        <%--function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            GetRadWindow().close();
        }
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }--%>
        function Rebind() {
            __doPostBack('RebindeGrid', null);
        }
    </script>
</asp:Content>
