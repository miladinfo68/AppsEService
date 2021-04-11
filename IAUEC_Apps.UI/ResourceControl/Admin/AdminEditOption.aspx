<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminEditOption.aspx.cs" Inherits="ResourceControl.PL.Admin.AdminEditOption" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>ویرایش امکانات کلاسها</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" /> <div class="container rtl">
        
        <div class="col-md-6 col-md-offset-6">
            <table id="tblEditOptions" class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td colspan="2" class="bg-primary">ویرایش نام امکانات موجود</td>
                </tr>
                <tr>
                    <td>نام قدیم</td>
                    <td>
                        <asp:DropDownList ID="drpChooseOption" runat="server" CssClass="dropdown" />
                    </td>
                </tr>
                <tr>
                    <td>نام جدید<br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نام جدید را وارد کنید" ControlToValidate="txtOptionNewName" ForeColor="Red" ValidationGroup="ccc"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOptionNewName" runat="server" CssClass="form-control" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnEditOption" runat="server" Text="ثبت تغییرات" CssClass="btn btn-danger" OnClick="btnEditOption_Click" ValidationGroup="ccc" />
                         <asp:Button ID="btnDeleteOption" runat="server" Text="حذف امکان" CssClass="btn btn-danger" OnClick="btnDeleteOption_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
