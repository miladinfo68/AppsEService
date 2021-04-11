<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminAddCategory.aspx.cs" Inherits="ResourceControl.PL.Admin.AdminAddCategory" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>دسته بندی کلاسها</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container rtl">
        <div class="row">
            <div class="col-md-6">
                <div class="table-responsive">
                    <table id="tblAddCategory"  class="table table-bordered table-hover table-striped tbl">
                        <tr>
                            <td colspan="2" class="bg-primary">
                                <h3>ایجاد دسته بندی جدید</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>نام دسته بندی جدید :
                        <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نام دسته بندی جدید را ذکر کنید" ForeColor="Red" ControlToValidate="txtCategoryName" ValidationGroup="aaa"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <td>توضیحات :
                        <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription" ErrorMessage="لطفا توضیح دسته بندی جدید را وارد کنید" ForeColor="Red" ValidationGroup="aaa"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Width="188px" TextMode="MultiLine" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:LinkButton ID="btnAddCategory" Text="ایجاد دسته بندی" runat="server" CssClass="btn btn-danger" OnClick="btnAddCategory_Click1" ValidationGroup="aaa" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="col-md-6">
                <table id="tblAddOptions" class="table table-bordered table-hover table-striped tbl">
                    <tr>
                        <td class="bg-info">
                            <h3>لیست دسته بندی های موجود</h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="direction: rtl; max-height: 300px; min-height: 100px; overflow-y: scroll">
                            <asp:GridView ID="grdCategoryList" runat="server" AutoGenerateColumns="false" EmptyDataText="هیچ دسته بندی موجود نیست." ShowHeaderWhenEmpty="true" CssClass="tbl table table-bordered table-striped">
                                <Columns>
                                    <asp:BoundField HeaderText="شماره" DataField="ID" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField HeaderText="نام دسته بندی" DataField="name" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField HeaderText="توضیحات" DataField="Description" HeaderStyle-CssClass="rtl bg-info" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
