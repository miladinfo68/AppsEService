<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminEditResource.aspx.cs" Inherits="ResourceControl.PL.Admin.AdminEditResource" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>ویرایش منابع</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkblResourceNewOptions.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>
    <script type="text/javascript">
        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
        }
    </script>
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container rtl">

        <div class="col-md-6 col-md-offset-6">
            <table id="tblEditResource" class="table table-bordered table-hover table-striped tbl rtl">
                <tr>
                    <td class="text-center bg-primary" colspan="2">
                        <h3>ویرایش منابع</h3>
                    </td>
                </tr>
                <tr>
                    <td>محل
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="drpResourceNewLocation" ErrorMessage="لطفا محل جدید کلاس را وارد نمایید" ForeColor="Red" InitialValue="0" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpResourceNewLocation" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="drpResourceNewLocation_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>انتخاب دسته بندی<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpChooseToEditCategory" ErrorMessage="لطفا دسته بندی را انتخاب کنید" ForeColor="Red" InitialValue="0" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpChooseToEditCategory" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="drpChooseToEditCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>انتخاب کلاس مورد نظر<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpChoosToEditResource" ErrorMessage="لطفا کلاس را انتخاب کنید" ForeColor="Red" InitialValue="0" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpChoosToEditResource" runat="server" CssClass="dropdown" OnSelectedIndexChanged="drpChoosToEditResource_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>نام جدید<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtResourceNewName" ErrorMessage="لطفا نام جدید کلاس را وارد کنید" ForeColor="Red" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResourceNewName" runat="server" CssClass="form-control" />
                    </td>
                </tr>
                <tr>
                    <td>ظرفیت :<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCapacity" ErrorMessage="ظرفیت صحیح بین 1 و 100 می باشد" ForeColor="Red" MaximumValue="100" MinimumValue="1" Type="Integer" ValidationGroup="ddd">*</asp:RangeValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" />
                    </td>
                </tr>
                <tr>
                    <td>امکانات
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="لطفا امکانات کلاس جدید را انتخاب کنید" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="ValidateCheckBoxList" ValidationGroup="ddd">*</asp:CustomValidator>
                    </td>
                    <td dir="rtl">
                        <asp:CheckBoxList ID="chkblResourceNewOptions" runat="server" CssClass="checkbox1" RepeatColumns="1" OnSelectedIndexChanged="chkblResourceNewOptions_SelectedIndexChanged">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>توضیحات</td>
                    <td>
                        <asp:TextBox ID="txtResourceNewDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td>وضعیت فعالیت<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rdbtnlistStatus" ErrorMessage="لطفا فعال یا غیر فعال بودن کلاس را مشخص نمایید" ForeColor="Red" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdbtnlistStatus" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="فعال" Value="false"></asp:ListItem>
                            <asp:ListItem Text="غیرفعال" Value="true"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnEditResource" Text="ثبت تغییرات" runat="server" CssClass="btn btn-info" OnClick="btnEditResource_Click" OnClientClick="confirmAspButton(this); return false;" ValidationGroup="ddd" />
                    </td>
                    <td>
                        <asp:Button ID="btnDeleteResource" Text="حذف منبع" runat="server" CssClass="btn btn-danger" OnClick="btnDeleteResource_OnClick" OnClientClick="confirmAspButton(this); return false;" ValidationGroup="ddd" />

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="لطفا موارد مشخص شده را تکمیل نمایید" ValidationGroup="ddd" />

                    </td>
                </tr>
            </table>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
    </div>
</asp:Content>
