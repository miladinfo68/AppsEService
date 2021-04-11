<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowContract.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ShowContract" %>

<%@ Register Src="~/University/CooperationRequest/UserControls/Contract.ascx" TagPrefix="uc" TagName="Contract" %>
<%@ Register Src="~/University/CooperationRequest/UserControls/Contract_DeputyGroup.ascx" TagPrefix="uc" TagName="Contract_DeputyGroup" %>
<%@ Register Src="~/University/CooperationRequest/UserControls/Contract_HeadOfDepartment.ascx" TagPrefix="uc" TagName="Contract_HeadOfDepartment" %>

<link href="css/Contract.css?v=124" rel="stylesheet" type="text/css" />
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../Education/BootStrap/bootstrap.min.css" rel="stylesheet" />
    <style>
        .showContractWrapper {
            direction: rtl;
            width: 100%;
        }

        .buttonsWrapper {
            text-align: center;
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px;
            width: 875px;
            margin: 0 auto;
        }

        .contractWrapper {
            width: 875px;
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px;
            margin: 0 auto 10px;
        }

        .rejectWrapper {
            display: none;
        }

        a, a:active, a:hover, a:focus {
            text-decoration: none;
        }

        @media (min-width: 768px) {
            .col-sm-1, .col-sm-2, .col-sm-3, .col-sm-4, .col-sm-5, .col-sm-6, .col-sm-7, .col-sm-8, .col-sm-9, .col-sm-10, .col-sm-11, .col-sm-12 {
                float: right;
            }
        }
    </style>

    <script type="text/javascript">
        function denyAspButton(button, commandName) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            if (commandName == 'accept')
                radconfirm("آیا از تأیید قرار داد اطمینان دارید؟", btnAcceptResume, 330, 100, null, "تأیید");
            else if(commandName == 'lastStep')
                radconfirm("آیا ارسال قرارداد به مرحله قبل اطمینان دارید؟", btnLastStepResume, 330, 100, null, "تأیید");
        }
        function btnAcceptResume(arg) {
            if (arg) {
                document.getElementById("hdnbtnAccept").click();
            }
        }

        function btnLastStepResume(arg) {
            if (arg) {
                document.getElementById("hdnbtnLastStep").click();
            }
        }
        <%-- function btnRejectResume(arg) {
                __doPostBack("<%# btnReject.ClientID %>", "onclick_reject");
            }--%>


        function switchRejectReasonView(showRejectWrapper) {
            if (showRejectWrapper) {
                document.getElementById('rejectWrapper').style.display = 'block';
                document.getElementById('buttonsWrapper').style.display = 'none';
            }
            else {
                document.getElementById('buttonsWrapper').style.display = 'block';
                document.getElementById('rejectWrapper').style.display = 'none';
            }
            event.preventDefault ? event.preventDefault() : event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
        <telerik:RadWindowManager ID="rwmMain" runat="server"></telerik:RadWindowManager>
        <div class="showContractWrapper">
            <asp:UpdatePanel runat="server" ID="uplConfirm">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="contractWrapper" id="ContractWrapper">
                                <asp:Panel ID="plcUserControl" runat="server"></asp:Panel>
                                <%--<uc:Contract runat="server" ID="ucContract" EnableViewState="false" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="buttonsWrapper" id="buttonsWrapper">
                                <div>
                                    <asp:CheckBox runat="server" ID="chbConfirm" Text="قرارداد فوق را مطالعه نموده و تائید می کنم" Checked="true" Visible="false"
                                        OnCheckedChanged="chbConfirm_CheckedChanged" />
                                </div>
                                <div>
                                    <asp:Button runat="server" ID="btnAccept" CssClass="btn btn-success" Text="تأیید و امضاء"
                                        OnClientClick="return denyAspButton(this, 'accept');" UseSubmitBehavior="true" />

                                    <asp:Button runat="server" ID="hdnbtnLastStep" CssClass="hidden" OnClick="btnLastStep_Click" />
                                    <asp:Button runat="server" ID="hdnbtnAccept" CssClass="hidden" OnClick="btnAccept_Click" />

                                    <asp:Button runat="server" ID="btnLastStep" CssClass="btn btn-primary" Text="بازگشت به مرحله قبل"  OnClientClick="return denyAspButton(this, 'lastStep');" />

                                    <%--<a href="#" onclick="switchRejectReasonView(true);" class="btn btn-danger">رد قرارداد</a>
                                    <a href="#" onclick="printWindow();" class="btn btn-info">چاپ</a>--%>
                                    <asp:Button runat="server" ID="btnRejectSwitch" CssClass="btn btn-danger" Text="رد قرارداد"
                                        OnClientClick="switchRejectReasonView(true); return false;" UseSubmitBehavior="false" />
                                    <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-info" Text="چاپ" OnClientClick="printWindow(); return false;"
                                        UseSubmitBehavior="false" />
                                    <asp:Button runat="server" ID="btnClose" CssClass="btn btn-info" Text="بستن" OnClientClick="Close(); return false;" BackColor="#ff9933" BorderColor="#ff9933" />
                                </div>
                            </div>
                            <div class="rejectWrapper" id="rejectWrapper">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:ValidationSummary runat="server" CssClass="alert alert-danger" ValidationGroup="reject" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-1">
                                        <asp:RequiredFieldValidator ID="rfvReject" ControlToValidate="txtRejectReason" ValidationGroup="reject" ForeColor="Red"
                                            ErrorMessage="لطفا دلیل رد را وارد فرمایید." Display="none" runat="server"></asp:RequiredFieldValidator>
                                        <asp:Label runat="server" ID="lblReject" Text="دلیل رد: "></asp:Label>
                                    </div>
                                    <div class="col-sm-7">
                                        <asp:TextBox runat="server" ID="txtRejectReason" CssClass="form-control" ValidationGroup="reject"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button runat="server" ID="btnReject" CssClass="btn btn-danger" Text="رد قرارداد" OnClick="btnReject_Click"
                                            ValidationGroup="reject" />
                                        <a href="#" onclick="switchRejectReasonView(false);" class="btn btn-warning">انصراف</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script>
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function Close() {
            GetRadWindow().close();
        }

        function RedirectToContractsMain() {
            window.location.href = "/University/CooperationRequest/teachers/contractsMain.aspx";
        }
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }
        function printWindow() {
            var frm = document.getElementById('ContractWrapper');
            var style = '<link href="css/Contract.css?v=124" rel="stylesheet" type="text/css">';
            var myWindow = window.open('', '', 'width=900,height=700')
            myWindow.document.write(style + frm.innerHTML);
            setTimeout(function () { myWindow.print(); myWindow.close(); }, 1000);
        }
        function CheckItem(sender, e) {
            if (sender._commandName == "btnConfirm") {
                radconfirm("آیا اطمینان دارید؟", btnConfirmResume, 330, 100, null, "تأیید");
                sender.set_autoPostBack(false);
            }
            else {
                sender.set_autoPostBack(true);
            }
        }

        function btnConfirmResume(arg) {
            if (arg) {
                __doPostBack("btnConfirm", "onclick");
            }
        }
    </script>
</body>
</html>
