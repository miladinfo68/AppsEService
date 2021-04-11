<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/TeacherMaster.Master" AutoEventWireup="true" CodeBehind="EditContactInfo.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.EditContactInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>بروزرسانی اطلاعات تماس</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" dir="rtl">
        <div class="row">
            <div class="col-sm-12">
                <div id="dvContactInfo" class=" panel panel-info">
                    <div class="panel-heading">
                        <h4>اطلاعات تماس</h4>
                    </div>
                    <div class="panel-body">
                        <div id="dvAddressFileds" runat="server" class="row">
                            <div class="col-md-3 ">

                                <div class="col-md-10">
                                    <h4>تلفن ها و پست الکترونیکی</h4>
                                </div>

                                <div class="col-md-12 ">
                                    <asp:Label ID="Label29" Text="تلفن منزل" runat="server" />
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="0[1-8][1-9][2-8][\d]{7}" ErrorMessage="تلفن منزل نادرست است. باید به همراه کد شهر و 11 رقمی باشد" ControlToValidate="txtHomePhone"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا تلفن منزل را به همراه کد شهر وارد فرمایید" ControlToValidate="txtHomePhone"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtHomePhone" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                                </div>


                                <div class="col-md-12 ">
                                    <asp:Label ID="Label31" Text="تلفن محل کار" runat="server" />
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="0[1-8][1-9][2-8][\d]{7}" ErrorMessage="تلفن محل کار نادرست است. باید به همراه کد شهر و 11 رقمی باشد" ControlToValidate="txtWorkPhone"></asp:RegularExpressionValidator>
                                    <%--<asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا تلفن محل کار را به همراه کد شهر وارد فرمایید" ControlToValidate="txtWorkPhone"></asp:RequiredFieldValidator>--%>

                                    <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                                </div>

                                <div class="col-md-12">
                                    <asp:Label ID="Label33" Text="تلفن همراه" runat="server" />
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="09[0-9]{9}" ErrorMessage="تلفن همراه نادرست است. باید به صورت عددی و 11 رقمی باشد" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا تلفن همراه را وارد فرمایید." ControlToValidate="txtMobileNumber"></asp:RequiredFieldValidator>

                                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="11" />
                                </div>

                                <div class="col-md-12">
                                    <asp:Label ID="Label34" Text="پست الکترونیک" runat="server" />
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="\w+([-_.]?\w+)?@\w+([-_.]?\w+)?\.\w+([-_.]?\w+)" ErrorMessage="آدرس پست الکترونیک نادرست وارد شده است." ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="وارد کردن پست الکترونیک الزامی است." ControlToValidate="txtEmail"></asp:RequiredFieldValidator>


                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="40" />
                                </div>

                            </div>
                            <div class="col-md-8 col-md-pull-1">

                                <div class="row">
                                    <div class="col-md-10">
                                        <h4>آدرس محل سکونت</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div class="col-md-3 col-md-offset-1">
                                                <asp:Label ID="Label35" Text="استان" runat="server" />
                                                <asp:DropDownList ID="drpProvince1" runat="server" CssClass=" form-control" AutoPostBack="true" Height="40px" OnSelectedIndexChanged="drpProvince1_SelectedIndexChanged" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="Label36" Text="شهر" runat="server" />
                                                <asp:DropDownList ID="drpLivingCity" runat="server" CssClass="form-control" Height="40px" />
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label38" Text="کد پستی" runat="server" />
                                        <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="[1-9][0-9]{9}" ErrorMessage="کد پستی نادرست است. باید به صورت عددی و 10 رقمی باشد." ControlToValidate="txtLivingZipCode"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا کد پستی 10 رقمی را وارد فرمایید" ControlToValidate="txtLivingZipCode"></asp:RequiredFieldValidator>

                                        <asp:TextBox ID="txtLivingZipCode" runat="server" CssClass="form-control" MaxLength="10" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Label ID="Label37" Text="آدرس" runat="server" />
                                        <asp:TextBox ID="txtLivingAddress" runat="server" CssClass=" form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-md-offset-1">
                                        <h4>آدرس محل کار</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-3 col-md-offset-1">
                                                <asp:Label ID="Label40" Text="استان" runat="server" />
                                                <asp:DropDownList ID="drpProvince2" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="drpProvince2_SelectedIndexChanged" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="Label39" Text="شهر" runat="server" />
                                                <asp:DropDownList ID="drpWorkingCity" runat="server" CssClass="form-control" />
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Label ID="Label41" Text="آدرس" runat="server" />
                                        <asp:TextBox ID="txtWorkingAddress" runat="server" CssClass=" form-control" />
                                    </div>
                                    <div class="col-md-3"></div>
                                </div>


                            </div>
                        </div>
                        <hr />
                        <asp:ValidationSummary ValidationGroup="vg" runat="server" ForeColor="Red" />
                        <div class="row text-center">
                            <div class="col-md-2 col-md-pull-5">
                                <asp:Button ID="btnSubmitChanges" Text="ثبت تغییرات" OnClick="btnSubmitChanges_Click" ValidationGroup="vg" CssClass="btn btn-info" runat="server" />
                                <asp:Button ID="btnCancel" Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-warning" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    </div>
    <script type="text/javascript">
        function RedirectToMain() {
            window.location = "EditMain.aspx";
        }
    </script>
</asp:Content>
