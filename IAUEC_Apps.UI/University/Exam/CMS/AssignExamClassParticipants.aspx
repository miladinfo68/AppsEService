<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="AssignExamClassParticipants.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.AssignExamClassParticipants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" media="all" href="../../../Adobe/css/aqua/theme.css" title="Aqua" />



    <!-- import the Jalali Date Class script -->
    <script type="text/javascript" src="../../../Adobe/js/jalali.js"></script>

    <!-- import the calendar script -->
    <script type="text/javascript" src="../../../Adobe/js/calendar.js"></script>

    <!-- import the calendar script -->
    <script type="text/javascript" src="../../../Adobe/js/calendar-setup.js"></script>

    <!-- import the language module -->
    <script type="text/javascript" src="../../../Adobe/js/lang/calendar-fa.js"></script>
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>

    <style>
        .separatorBox {
            border: 1px solid #ccc;
            margin: 10px 0;
            padding: 10px;
            border-radius: 5px;
        }

            .separatorBox .row {
                margin: 5px 0;
            }
    </style>
    <script type="text/javascript">
        function denyAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding: 1%; color: #000">
        <div class="row">
            <div class="col-md-12">
                <div class="separatorBox">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:RequiredFieldValidator runat="server" ID="rfvExaminerName" ValidationGroup="AddExaminer" ControlToValidate="ddlExaminerName" ForeColor="Red"
                                Display="Dynamic" Text="*" ErrorMessage="لطفا کاربر مورد نظر را انتخاب نمائید." InitialValue="-1"></asp:RequiredFieldValidator>
                            <asp:Label runat="server" Text="نام" CssClass="col-md-3"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlExaminerName" CssClass="col-md-8"></asp:DropDownList>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-3">
                                    <asp:RequiredFieldValidator runat="server" ID="rfvLmsPass" ValidationGroup="AddExaminer" ControlToValidate="txtLmsPass" ForeColor="Red" Display="Dynamic"
                                        Text="*" ErrorMessage="لطفا کلمه عبور کاربر در سامانه مدیریت یادگیری را وارد نمائید."></asp:RequiredFieldValidator>
                                    <asp:Label runat="server" Text="کلمه عبور ایمیل" CssClass="col-md-3"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtLmsPass" CssClass="col-md-8" TextMode="Password"></asp:TextBox>
                                </div>

                                <div class="col-md-4" style="text-align: center;">

                                    <asp:TextBox runat="server" ID="txtGeneratePassword" CssClass="col-md-7" ReadOnly="true"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnGeneratePassword" Text="ایجاد پسورد جدید" CssClass="btn btn-success" OnClick="btnGeneratePassword_Click" />


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-md-2" style="text-align: center;">
                            <asp:Button runat="server" ID="btnAddExaminer" Text="ثبت" CssClass="btn btn-success" OnClick="btnAddExaminer_Click" ValidationGroup="AddExaminer" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2" style="text-align: center;">
                            <asp:Button runat="server" ID="btnPasswordReset" Text="ریست و فعال سازی رمزهای عبور" CssClass="btn btn-success" OnClick="btnPasswordReset_Click" />
                            <asp:Label ID="lbl_Message" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="#2E7D32"></asp:Label>
                            <asp:Label ID="lbl_ErrprMessage" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small" ForeColor="#FF3300"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="separatorBox">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:HiddenField runat="server" ID="hdnItemId" />
                            <asp:Label runat="server" Text="نام" CssClass="col-md-3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtExaminerName" CssClass="col-md-9"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" Text="شهر" CssClass="col-md-3"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlExaminerPlaceId" CssClass="col-md-9"></asp:DropDownList>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-6">
                            <asp:Label runat="server" Text="موبایل" CssClass="col-md-3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="col-md-9"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label runat="server" Text="ایمیل" CssClass="col-md-3"></asp:Label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="col-md-9"></asp:TextBox>
                        </div>
                    </div>--%>
                    <asp:UpdatePanel ID="pnl2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label runat="server" Text="نام کاربری" CssClass="col-md-3"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtUserName" CssClass="col-md-9"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label runat="server" Text="کلمه عبور LMS" CssClass="col-md-3"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtPassword" CssClass="col-md-9" TextMode="Password"></asp:TextBox>


                                </div>
                            </div>
                            <div class="row">
                                <%----%>
                                <div class="col-md-6" style="text-align: center;">
                                    <asp:Button runat="server" ID="btnAddCustomExaminer" Text="ثبت" CssClass="btn btn-success" OnClick="btnAddCustomExaminer_Click" />
                                    <asp:Button runat="server" ID="btnCancelEdit" Text="انصراف" CssClass="btn btn-danger" OnClick="btnCancelEdit_Click" />
                                </div>
                                <div class="col-md-6" style="text-align: center;">

                                    <asp:TextBox runat="server" ID="txtLMSGeneratePassword" CssClass="col-md-8" ReadOnly="true"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnLMSGeneratePassword" Text="ایجاد پسورد جدید" CssClass="btn btn-success" OnClick="btnLMSGeneratePassword_Click" />


                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadGrid ID="gvParticipants" runat="server" AutoGenerateColumns="false" EnableEmbeddedSkins="false" Skin="MyCustomSkin" AllowPaging="true"
                    PageSize="30" AllowSorting="true" OnNeedDataSource="gvParticipants_NeedDataSource" SortingSettings-EnableSkinSortStyles="false"
                    OnItemCommand="gvParticipants_ItemCommand">
                    <MasterTableView AutoGenerateColumns="false">
                        <ItemStyle BackColor="#e6ffff" Font-Names="tahoma" />
                        <HeaderStyle Font-Names="tahoma" HorizontalAlign="Center" ForeColor="White" />
                        <AlternatingItemStyle Font-Names="tahoma" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="نام" DataField="ExaminerName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="نام کاربری" DataField="ExaminerUserName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="شهر" DataField="Name_City"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn HeaderText="تلفن همراه" DataField="Mobile"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="تخصیص داده شده">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkAssign" Checked='<%# CheckParticipantIsAssigned(Eval("ExaminerID")) %>' />
                                    <asp:HiddenField runat="server" ID="hdnExaminerID" Value='<%# Eval("ExaminerID").ToString() %>' />
                                    <asp:HiddenField runat="server" ID="hdnExamPlaceID" Value='<%# Eval("ExamPlaceID").ToString() %>' />
                                    <asp:HiddenField runat="server" ID="hdnExaminerName" Value='<%# Eval("ExaminerName").ToString() %>' />
                                    <asp:HiddenField runat="server" ID="hdnUserName" Value='<%# Eval("UserName").ToString() %>' />
                                    <asp:HiddenField runat="server" ID="hdnPassword" Value='<%# Eval("Password").ToString() %>' />
                                    <asp:HiddenField runat="server" ID="hdnEPass" Value='<%# Eval("ePass").ToString() %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnEdit" Text="ویرایش" CssClass="btn btn-info" CommandName="EditCustome" CommandArgument='<%# Eval("id") %>'
                                        Visible='<%# (string.IsNullOrEmpty(Eval("ExaminerID").ToString()) || Convert.ToInt32(Eval("ExaminerID")) <= 0) %>' />
                                    <asp:Button runat="server" ID="btnDelete" Text="حذف" CssClass="btn btn-danger" CommandName="DeleteExaminer" CommandArgument='<%# Eval("id") %>'
                                        OnClientClick="return denyAspButton(this);" UseSubmitBehavior="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-md-12">
                <asp:Button runat="server" ID="btnSaveList" Text="ذخیره" OnClick="btnSaveList_Click" CssClass="btn btn-success" />
            </div>
        </div>--%>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
</asp:Content>
