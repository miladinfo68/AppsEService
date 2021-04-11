<%@ Page Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Contract.Master" AutoEventWireup="true" CodeBehind="ConfirmContract.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.ConfirmContract" %>

<%@ Register Src="~/University/CooperationRequest/UserControls/Contract.ascx" TagPrefix="uc" TagName="Contract" %>
<%@ Register Src="~/University/CooperationRequest/UserControls/Contract_DeputyGroup.ascx" TagPrefix="uc" TagName="Contract_DeputyGroup" %>
<%@ Register Src="~/University/CooperationRequest/UserControls/Contract_HeadOfDepartment.ascx" TagPrefix="uc" TagName="Contract_HeadOfDepartment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/Contract.css?v=124" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>تائید قرارداد</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function RedirectToEditMain() {
            window.location.href = "/University/CooperationRequest/Teachers/contractsMain.aspx";
        }
        function denyAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا از ثبت قرار داد اطمینان دارید؟", btnConfirmResume, 330, 100, null, "تأیید");
        }
        function btnConfirmResume(arg) {
            if (arg) {
                document.getElementById("<%= hdnBtnConfirm.ClientID %>").click();
                //document.getElementById("").click();
            }
        }
    </script>
    <telerik:RadWindowManager ID="rwmMain" runat="server"></telerik:RadWindowManager>
    <asp:UpdatePanel runat="server" ID="uplConfirm">
        <ContentTemplate>
            <div class="row">
                <div class="col-sm-12">
                    <div class="contractWrapper boxed" oncontextmenu="return false;">
                        <asp:PlaceHolder ID="plcUserControl" runat="server">

                        </asp:PlaceHolder>
                       </div>
                </div>
            </div>
            <div class="row">
                <div class="confirmPanel">
                    <div>
                        <asp:CheckBox runat="server" ID="chbConfirm" Text="قرارداد فوق را مطالعه نموده و تائید می کنم"
                            OnCheckedChanged="chbConfirm_CheckedChanged" AutoPostBack="true" />
                    </div>
                    <div>
                        <asp:Button runat="server" ID="btnClose" CssClass="btn btn-info" Text="بستن" OnClientClick="RedirectToEditMain()" />
                        <asp:Button runat="server" ID="btnConfirm" Text="تائید و امضاء" CssClass="btn btn-success btnConfirm"
                            Enabled="false" OnClientClick="return denyAspButton(this);" UseSubmitBehavior="true"/>

                        <asp:Button ID="hdnBtnConfirm" CssClass="hidden" OnClick="btnConfirm_Click" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
