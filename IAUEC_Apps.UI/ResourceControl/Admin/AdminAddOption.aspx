<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminAddOption.aspx.cs" Inherits="ResourceControl.PL.Admin.AdminAddOptions" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style type="text/css">
        .auto-style1 {
            background-color: #d9edf7;
            width: 196px;
        }
        .auto-style2 {
            width: 196px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>امکانات کلاسها<uc1:AccessControl ID="AccessControl1" runat="server" />
&nbsp;</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container rtl">
        <div class="col-md-6">
            <table class="table tbl table-bordered table-striped">
                <tr>
                    <td colspan="2" class="bg-primary">
                        <h3>درج امکان جدید</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtOptionName" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نام امکان جدید را وارد کنید" ControlToValidate="txtOptionName" ForeColor="Red" ValidationGroup="ccc"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:LinkButton ID="btnAddOption" Text="اضافه کردن امکان" runat="server" CssClass="btn btn-danger" OnClick="Button1_Click" ValidationGroup="ccc" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="col-md-6">
            <table id="tblAddOptions" class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td class="auto-style1">
                        <h3>لیست امکانات موجود</h3>
                    </td>
                </tr>
                <tr>
                    <td style="direction: rtl; max-height: 300px; min-height: 100px; overflow-y: scroll" class="auto-style2">
                        <asp:GridView ID="grdOptionList" runat="server" CssClass="table table-bordered tab-striped " EmptyDataText="هیچ امکانی موجود نیست." ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="شماره" DataField="ID" HeaderStyle-CssClass="rtl bg-info" />
                                <asp:BoundField HeaderText="نام" DataField="name" HeaderStyle-CssClass="rtl bg-info" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
