<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="AdminEditLocation.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Admin.AdminEditLocation" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>ویرایش محل کلاسها</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container rtl">
        
        <div class="col-md-8 col-md-offset-4">
            <table id="tblEditLocation" class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td colspan="2" class="bg-primary">
                        <h3>ویرایش محل</h3>
                    </td>
                    <td class="bg-primary">توضیحات</td>
                </tr>
                <tr>
                    <td>انتخاب محل :<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpChooseLocation" ErrorMessage="لطفا محل قدیمی را وارد نمایید" ForeColor="Red" InitialValue="انتخاب کنید" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpChooseLocation" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="drpChooseLocation_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Literal ID="txtDescriptionOld" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>مشخصات جدید محل :
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="موارد زیر را وارد نمایید" ValidationGroup="bbb" />
                        <br />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نام جدید را وارد کنید" ControlToValidate="txtNameNew" ForeColor="Red" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtNameNew" runat="server" CssClass="form-control" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDecriptionNew" ErrorMessage="لطفا توضیحات جدید را درج کنید" ForeColor="Red" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDecriptionNew" runat="server" TextMode="MultiLine" CssClass="form-control" />
                    </td>

                </tr>
                <tr>

                    <td colspan="3" class="text-center">
                        <asp:LinkButton ID="btnEditLocation" Text="ویرایش محل" runat="server" CssClass="btn btn-danger" OnClick="btnEditLocation_Click" ValidationGroup="bbb" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
