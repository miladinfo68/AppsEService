<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="VahedReshte.aspx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.VahedReshte" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .grid td, .grid th {
            text-align: center;
            padding: 5px;
        }

        .padd {
            padding-right: 3px;
        }

        .spacing {
            margin-right: 20px;
        }

        .marginItem {
            margin-right: 20px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .rcbInner {
            height: 36px !important;
            border-top: 1px solid #cccccc !important;
            border-right: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-left: none !important;
            color: #555555 !important;
            padding-bottom: 5px;
        }

        .rcbActionButton {
            height: 36px !important;
            background-color: white !important;
            background-image: none;
            border-top: 1px solid #cccccc !important;
            border-left: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-right: none !important;
            padding-bottom: 5px;
        }

        .RadComboBox_Default .rcbActionButton {
            background-image: none !important;
            padding-bottom: 5px;
        }

        .RadComboBox_Default .rcbInput {
            height: 20px !important;
            font-family: Yekan,'B Yekan' !important;
            font-size: 14px !important;
            font-weight: bold !important;
            padding-right: 11px !important;
            color: #555555 !important;
            padding-bottom: 5px;
        }

        .rcbItem, rcbHovered {
            font-family: Yekan,'B Yekan' !important;
            font-size: 13px !important;
            font-weight: bold !important;
            color: #555555 !important;
            padding-bottom: 5px;
        }

        .RadComboBoxDropDown_Default .rcbHovered {
            background-color: #2fa4e7 !important;
            color: white !important;
            font-family: Yekan,'B Yekan' !important;
            font-weight: bold !important;
            padding-bottom: 5px;
        }

        .labelMargin {
            margin-right: 10px;
        }
    </style>
    <script type="text/javascript">
        function Confirm() {
            if (performCheck()) {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("آيا مطمئن هستيد؟")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
            else {
                alert('لطفا ورودي هاي خود را بررسي کنيد!');
            }
        }
        function performCheck() {
            if (Page_ClientValidate("inputDataValidationGroup")) {
                return true;
            }
            else if (Page_ClientValidate("inputDataValidationGroup2")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <br />
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>
                <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                    <div class="row">
                        <div class="col-md-2" style="width: 145px">
                        </div>
                      <%--  <div class="col-md-2" style="background: #00C5FF; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 1%">
                            <asp:Button ID="btnReshVahed" CssClass="button" runat="server" Style="background: #00C5FF; text-align: center; color: #fff; margin-left: 5px" OnClick="btnReshVahed_Click" Text="ثبت رشته - واحد" />
                        </div>
                        <div class="col-md-2" style="background: #00DF04; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 0.5%">
                            <asp:Button ID="btnSematVahed" runat="server" Style="background: #00DF04; text-align: center; color: #fff; margin-left: 5px" OnClick="btnSematVahed_Click" Text="ثبت سمت - واحد" />
                        </div>
                        <div class="col-md-2" style="background: #ffb150; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 0.5%">
                            <asp:Button ID="btnSemat" runat="server" Style="background: #ffb150; text-align: center; color: #fff; margin-left: 5px" OnClick="btnSemat_Click" Text="ثبت سمت" />
                        </div>
                        <div class="col-md-2" style="background: #FF6666; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 0.5%">
                            <asp:Button ID="bntVahed" runat="server" Style="background: #FF6666; text-align: center; color: #fff; margin-left: 5px" OnClick="bntVahed_Click" Text="ثبت واحد" />
                        </div>--%>
                         <div class="col-md-2" style="background: #00C5FF; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 1%">
                            <asp:RadioButton ID="rdbReshVahed" runat="server" Style="margin-left: 5px" GroupName="a" AutoPostBack="True" OnCheckedChanged="rdbReshVahed_CheckedChanged" />ثبت رشته - واحد
                        </div>
                        <div class="col-md-2" style="background: #00DF04; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 0.5%">
                            <asp:RadioButton ID="rdbSematVahed" runat="server" Style="margin-left: 5px" GroupName="a" AutoPostBack="True" OnCheckedChanged="rdbSematVahed_CheckedChanged" />ثبت سمت - واحد
                        </div>
                        <div class="col-md-2" style="background: #ffb150; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 0.5%">
                            <asp:RadioButton ID="rdbSemat" runat="server" Style="margin-left: 5px" GroupName="a" AutoPostBack="True" OnCheckedChanged="rdbSemat_CheckedChanged" />ثبت سمت
                        </div>
                        <div class="col-md-2" style="background: #FF6666; text-align: center; padding: 1%; color: #fff; margin-left: 0.5%; margin-right: 0.5%">
                            <asp:RadioButton ID="rdbVahed" runat="server" Style="margin-left: 5px" GroupName="a" AutoPostBack="True" OnCheckedChanged="rdbVahed_CheckedChanged" />ثبت واحد
                        </div>
                    </div>
                </div>
                <asp:Panel runat="server" ID="pnlReshteVahedRV" DefaultButton="btnOkRV" Visible="true">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfvVahedRv" runat="server" ControlToValidate="cmbVahedRV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                واحد:
                        <telerik:RadComboBox ID="cmbVahedRV" EmptyMessage="انتخاب نمایید..." runat="server"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="rfvReshteRV" runat="server" ControlToValidate="cmbReshteRV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                رشته:
                        <telerik:RadComboBox ID="cmbReshteRV" EmptyMessage="انتخاب نمایید..." Width="80%" CheckBoxes="true" AllowCustomText="true" runat="server" Filter="Contains"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfvVoroodiRV" runat="server" ControlToValidate="txtVoroodiRV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                ورودی:
                        <asp:TextBox ID="txtVoroodiRV" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnOkRV" runat="server" Text="تایید" CssClass="btn btn-primary" OnClientClick="Confirm()" OnClick="btnOkRV_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlSematVahedSV" DefaultButton="btnOkSV" Visible="true">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfvVahedSV" runat="server" ControlToValidate="cmbVahedSV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                واحد:
                        <telerik:RadComboBox ID="cmbVahedSV" EmptyMessage="انتخاب نمایید..." runat="server"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="rfvSematSV" runat="server" ControlToValidate="cmbSematSV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                سمت:
                        <telerik:RadComboBox ID="cmbSematSV" EmptyMessage="انتخاب نمایید..." Width="70%" runat="server"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfvNameSV" runat="server" ControlToValidate="txtNameSV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                نام و نام خانوادگی:
                        <asp:TextBox ID="txtNameSV" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnOkSV" runat="server" Text="تایید" CssClass="btn btn-primary" OnClientClick="Confirm()" OnClick="btnOkSV_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlSematOrVahedSOV" DefaultButton="btnOkSOV" Visible="true">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-3">
                                <asp:RequiredFieldValidator ID="rfvSematOrVahedSOV" runat="server" ControlToValidate="txtSematOrVahedSOV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblCaptionSOV" runat="server" ForeColor="#6C777A" Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="txtSematOrVahedSOV" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnOkSOV" runat="server" Text="تایید" CssClass="btn btn-primary" OnClientClick="Confirm()" OnClick="btnOkSOV_Click" />
                            </div>

                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlResults" Visible="true">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-6">
                                <asp:GridView ID="grdResults" HorizontalAlign="Center" runat="server" CssClass="grid" CellPadding="4" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" OnRowCommand="grdResults_RowCommand" OnRowDataBound="grdResults_RowDataBound">
                                    <Columns >
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditRow" CommandName="btnEdit" Text="ویرایش" Visible="true" CssClass="button" runat="server" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' ForeColor="#a173ff"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <%--<div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-4" style="margin-top: 10px;">
                                    *برای ویرایش، روی سطر مورد نظر از جدول رو به رو کلیک کنید
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEditSematOrVahedESOV" runat="server" Visible="true" DefaultButton="btnOkESOV">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-4" style="width: 320px; margin-top: 10px;">
                                <asp:RequiredFieldValidator ID="rfvEditSematOrVahedESOV" CssClass="spacing" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEditSematOrVahedESOV" ValidationGroup="inputDataValidationGroup2"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblEditCaptionESOV" runat="server" ForeColor="#6C777A" Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="txtEditSematOrVahedESOV" runat="server" CssClass="padd" Width="200px" Height="25px" TabIndex="0" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnOkESOV" runat="server" Text="ثبت" CssClass="btn btn-success" OnClientClick="Confirm()" OnClick="btnOkESOV_Click" />
                                <asp:Button ID="btnCancelESOV" runat="server" Text="لغو" CssClass="btn btn-danger" OnClick="btnCancelESOV_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEditReshteVahedERV" runat="server" Visible="true" DefaultButton="btnOkERV">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="rfvEditVahedERV" runat="server" ControlToValidate="cmbEditVahedERV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                واحد:
                        <telerik:RadComboBox ID="cmbEditVahedERV" EmptyMessage="انتخاب نمایید..." runat="server"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnOkERV" runat="server" Text="ثبت" CssClass="btn btn-success" OnClientClick="Confirm()" OnClick="btnOkERV_Click" />
                                <asp:Button ID="btnCancelERV" runat="server" Text="لغو" CssClass="btn btn-danger" OnClick="btnCancelERV_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEditSematVahedESV" runat="server" Visible="true" DefaultButton="btnOkESV">
                    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%; margin-top: 1%">
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="rfvEditNameESV" runat="server" ControlToValidate="txtNameSV" ErrorMessage="*" ValidationGroup="inputDataValidationGroup" ForeColor="Red"></asp:RequiredFieldValidator>
                                نام و نام خانوادگی:
                        <asp:TextBox ID="txtEditNameESV" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnOkESV" runat="server" Text="ثبت" CssClass="btn btn-success" OnClientClick="Confirm()" OnClick="btnOkESV_Click" />
                                <asp:Button ID="btnCancelESV" runat="server" Text="لغو" CssClass="btn btn-danger" OnClick="btnCancelESV_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdbReshVahed" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="rdbSematVahed" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="rdbSemat" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="rdbVahed" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <telerik:RadWindowManager ID="RadWinMng" runat="server"></telerik:RadWindowManager>
    <uc1:AccessControl ID="AccessControl1" runat="server" />
</asp:Content>
