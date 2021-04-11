<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminAddResource.aspx.cs" Inherits="ResourceControl.PL.Forms.Admin" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>لیست کلاسها</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chkblSelecetOptions.ClientID %>");
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
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container rtl">
        <div class="row">
            <div class="col-sm-5">
                <table id="tblAddResource" class="table table-bordered table-hover table-striped tbl">
                    <tr>
                        <td colspan="2" class="bg-primary">
                            <h3>ثبت کلاس جدید</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>انتخاب دسته بندی<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpChooseCategory" ErrorMessage="لطفا دسته بندی را انتخاب کنید" ForeColor="Red" InitialValue="انتخاب کنید" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpChooseCategory" runat="server" CssClass="dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>نام کلاس جدید :<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddNewResource" ErrorMessage="لطفا نام کلاس جدید را  وارد کنید" ForeColor="Red" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAddNewResource" runat="server" CssClass="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <td>محل استفاده<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drpNewResLocation" ErrorMessage="لطفا محل استفاده کلاس جدید را ذکر کنید" ForeColor="Red" ValidationGroup="ddd">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="drpNewResLocation" runat="server" CssClass="dropdown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>امکانات کلاس جدید :<br />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="لطفا امکانات کلاس جدید را انتخاب کنید" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="ValidateCheckBoxList" ValidationGroup="ddd">*</asp:CustomValidator>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkblSelecetOptions" runat="server" CssClass="checkbox1" RepeatColumns="1" ></asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>ظرفیت
                            <asp:RequiredFieldValidator ErrorMessage="لطفا ظرفیت کلاس را مشخص نمایید" ControlToValidate="txtCapacity" runat="server" ForeColor="Red" Text="*" ValidationGroup="ddd" />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCapacity" ErrorMessage="لطفا ظرفیت مجاز بین 1 تا 100 نفر وارد کنید" ForeColor="Red" MaximumValue="100" MinimumValue="1" Type="Integer" ValidationGroup="ddd">*</asp:RangeValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات :</td>
                        <td>
                            <asp:TextBox ID="txtNewDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Wrap="True" ViewStateMode="Inherit" />
                        </td>
                    </tr>
                    <tr>
                        <td class="text-center" colspan="2">
                            <asp:LinkButton ID="btnAddResource" Text="درج کلاس جدید" runat="server" CssClass="btn btn-danger" OnClick="btnAddResource_Click" ValidationGroup="ddd" />
                            <br />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="لطفا موارد مشخص شده را تکمیل نمایید" ValidationGroup="ddd" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-sm-7">
                <div id="tblAddOptions" >
                    <div class="bg-primary" >
                        <div style="padding:10px;">
                            <h3>لیست منابع موجود</h3>
                        </div>
                    </div>
                    <div>
                        <div class="table-responsive" style="direction: rtl; overflow-x:scroll">
                            <asp:GridView ID="grdResourceList" HeaderStyle-CssClass="rtl" ShowHeaderWhenEmpty="true" EmptyDataText="هیچ کلاسی موجود نیست." runat="server" DataKeyNames="ID" CssClass="table table-bordered table-striped table-hover table-condensed tbl" AutoGenerateColumns="false" OnRowDataBound="grdResourceList_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="شماره" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField DataField="name" HeaderText="نام" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField DataField="location" HeaderText="محل" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField DataField="CategoryName" HeaderText="دسته بندی" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField DataField="capacity" HeaderText="ظرفیت" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:BoundField DataField="description" HeaderText="توضیحات" HeaderStyle-CssClass="rtl bg-info" />
                                    <asp:TemplateField HeaderText="امکانات این کلاس" HeaderStyle-CssClass="rtl bg-info">
                                        <ItemTemplate>
                                            <asp:BulletedList ID="bltOption" runat="server" DataTextField="name" BulletStyle="Numbered"></asp:BulletedList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
