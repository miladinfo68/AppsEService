<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Support/MasterPage/CMSSupportMaster.Master" CodeBehind="resetPasswordStudent.aspx.cs" Inherits="IAUEC_Apps.UI.University.Support.CMS.resetPasswordStudent" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#confirmModal').modal('show');
        }
        function closeModal() {
            $('#confirmModal').modal('hide');
        }
        </script>
    <div class="modal fade" id="confirmModal" tabindex="-1" dir="rtl" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content alert-danger">
                <div class="modal-header">
                    <h4 class="modal-title" id="exampleModalLabel">اخطار</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: white; border-radius: 5px 5px 0px 0px; padding: 1%; color: black; font-weight:bold; font-size:larger">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="confirmMessage" runat="server" Text="آیا از ایجاد رمز برای دانشجو اطمینان دارید؟ با فشردن دکمه 'بله' کد ملی به عنوان رمز عبور سامانه خدمات برای دانشجو تعیین خواهد شد." />
                                </div>
                                <div>
                                    <telerik:RadButton ID="rbResetPassword" runat="server" OnClick="rbResetPassword_Click" Text="بله">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="rbCancelReseting" runat="server" OnClientClicked="rbCancelReseting" Text="خیر">
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container" dir="rtl">
        <div class="panel panel-purple">
            <div class="panel panel-heading">
                <h3>ساخت رمز عبور دانشجویان </h3>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-3">
                        کد دانشجویی
                        <asp:TextBox ID="txtStcode" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        کد ملی
                        <asp:TextBox ID="txtNationalCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnSearchStudent" runat="server" CssClass="btn" OnClick="btnSearchStudent_Click" text="جستجو"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btnCreatePassword" Visible="false" runat="server" CssClass="btn btn-success" OnClick="btnCreatePassword_Click" Text="ایجاد رمز عبور سامانه خدمات" />

                    </div>
                    <div class="col-md-9">
                        <h1 class="alert " id="searchStatus" runat="server"></h1>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
