<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminEditCategory.aspx.cs" Inherits="ResourceControl.PL.Admin.AdminEditCategory" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>ویرایش دسته بندی </h1>
    <asp:Literal ID="pt" runat="server" Visible="false" ></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />  <div class="container rtl">
        
        <div class="col-md-8 col-md-offset-4">
            <table id="tblEditCategory" class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td colspan="2" class="bg-primary">
                        <h3>ویرایش دسته بندی</h3>
                    </td>
                    <td class="bg-primary">توضیحات</td>
                </tr>
                <tr>
                    <td>انتخاب دسته بندی :<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpChooseCategory" ErrorMessage="لطفا دسته بندی قدیمی را وارد نمایید" ForeColor="Red" InitialValue="انتخاب کنید" ValidationGroup="aaa">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpChooseCategory" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="drpChooseCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Literal ID="txtDescriptionOld" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>مشخصات جدید دسته بندی :
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="موارد زیر را وارد نمایید" ValidationGroup="aaa" />
                        <br />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نام جدید را وارد کنید" ControlToValidate="txtNameNew" ForeColor="Red" ValidationGroup="aaa">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNameNew" runat="server" CssClass="form-control" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDecriptionNew" ErrorMessage="لطفا توضیحات جدید را درج کنید" ForeColor="Red" ValidationGroup="aaa">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDecriptionNew" runat="server" TextMode="MultiLine" CssClass="form-control" />
                    </td>

                </tr>
                <tr>

                    <td colspan="3" class="text-center">
                        <asp:LinkButton ID="btnEditCategory" Text="ویرایش دسته بندی" runat="server" CssClass="btn btn-danger" OnClick="btnEditCategory_Click" ValidationGroup="aaa" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

