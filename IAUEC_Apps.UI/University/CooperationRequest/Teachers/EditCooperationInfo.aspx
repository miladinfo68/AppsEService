<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/TeacherMaster.Master" AutoEventWireup="true" CodeBehind="EditCooperationInfo.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.EditCooperationInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .well {
            margin-bottom: 0px !important;
        }
    </style>
    <script type="text/javascript">
        function ValidateModuleList(source, args) {
            var chkListModules = document.getElementById('<%= chbkCooperation.ClientID %>');
    var chkListinputs = chkListModules.getElementsByTagName("input");
    for (var i = 0; i < chkListinputs.length; i++) {
        if (chkListinputs[i].checked) {
            args.IsValid = true;
            return;
        }
    }
    args.IsValid = false;
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2>ویرایش اطلاعات نحوه همکاری</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row" dir="rtl">
            <div class="col-sm-12">
                <br />
                <div id="dvPanelCooperation" class="panel panel-danger">
                    <div class="panel-heading">نحوه همکاری</div>
                    <div class="panel-body well">
                        <span>نحوه همکاری</span>
                        <span style="color: red">*</span>
                        <asp:CustomValidator runat="server" ID="cvmodulelist"
                            ClientValidationFunction="ValidateModuleList" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="لطفا نحوه همکاری خود را مشخص کنید." ValidationGroup="eduGroup"></asp:CustomValidator>
                        <asp:CheckBoxList ID="chbkCooperation" runat="server" CssClass="checkbox" RepeatDirection="Horizontal">
                            <asp:ListItem Text="همکاری برای تدریس" Value="1" />
                            <asp:ListItem Text="همکاری در خصوص مشاوری یا راهنمایی پایان نامه" Value="2" />
                        </asp:CheckBoxList>
                    </div>
                </div>
                <br />
                <div class="panel panel-info">
                    <div class="panel-heading">گروههای آموزشی تدریس</div>
                    <div class="panel-body well">
                        <asp:UpdatePanel runat="server" ID="pnlGroups">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chbkDaneshkade" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label18" Text="دانشکده مورد نظر جهت تدریس" runat="server" />
                                        <asp:CheckBoxList ID="chbkDaneshkade" runat="server" CssClass="checkbox" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chbkDaneshkade_SelectedIndexChanged">
                                           
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="Label42" Text="گروههای آموزشی همکاری" runat="server" />
                                        <asp:CheckBoxList ID="chbkGroup" runat="server" CssClass="checkbox"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:CustomValidator ID="customValidator1" ValidationGroup="eduGroup" Display="Dynamic" ForeColor="Red" OnServerValidate="customValidator1_ServerValidate" runat="server" Text="لطفا خطای زیر را برطرف نمایید"></asp:CustomValidator>
                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="eduGroup" ForeColor="Red" EnableClientScript="false" />
                    </div>
                </div>

                <asp:Button ID="btnSubmitCooperation" Text="ثبت وضعیت" OnClick="btnSubmitCooperation_Click" CssClass="btn btn-danger" runat="server" ValidationGroup="eduGroup" />
                <asp:Button ID="btnCancel" Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-warning" runat="server" />
                <asp:ValidationSummary runat="server" ValidationGroup="coop" ForeColor="Red" />
            </div>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <script type="text/javascript">
        function RedirectToMain() {
            window.location = "EditMain.aspx";
        }
    </script>
</asp:Content>
