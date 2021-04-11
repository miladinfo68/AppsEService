<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="AdminAddLocation.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.AdminAddLocation" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>محل کلاسها</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container rtl">
        <div class="col-md-6">
            <table id="tblAddLocation" class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td colspan="2" class="bg-primary">
                        <h3>ایجاد محل جدید</h3>
                    </td>
                </tr>
                <tr>
                    <td>نام محل جدید :
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نام محل جدید را ذکر کنید" ForeColor="Red" ControlToValidate="txtLocationName" ValidationGroup="bbb"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocationName" runat="server" CssClass="form-control" />
                    </td>
                </tr>
                <tr>
                    <td>آدرس :
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" ErrorMessage="لطفا توضیح محل جدید را وارد کنید" ForeColor="Red" ValidationGroup="bbb"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="btnAddLocation" Text="ایجاد محل" runat="server" CssClass="btn btn-danger" OnClick="btnAddLocation_Click1" ValidationGroup="bbb" />
                    </td>
                </tr>
            </table>
        </div>

        <div class="col-md-6">
            <table id="tblAddOptions" class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td class="bg-info">
                        <h3>لیست محل های موجود</h3>
                    </td>
                </tr>
                <tr>
                    <td style="direction: rtl; max-height: 300px; min-height: 100px; overflow-y: scroll">
                        <asp:GridView ID="grdLocationList" runat="server" AutoGenerateColumns="false" EmptyDataText="هیچ محلی موجود نیست." ShowHeaderWhenEmpty="true" CssClass="tbl table table-bordered table-striped">
                            <Columns>
                                <asp:BoundField HeaderText="نام محل" DataField="name" HeaderStyle-CssClass="rtl bg-info" />
                                <asp:BoundField HeaderText="آدرس" DataField="Address" HeaderStyle-CssClass="rtl bg-info" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
