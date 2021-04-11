<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/PageRequestMaster.Master" AutoEventWireup="true" CodeBehind="EditPersonalInformationUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.EditPersonalInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 20%;
            margin-bottom: 15px;
            overflow-x: auto;
            overflow-y: hidden;
            -webkit-overflow-scrolling: touch;
            -ms-overflow-style: -ms-autohiding-scrollbar;
            border: 1px solid #ddd;
            height: 192px;
        }

        .auto-style2 {
            width: 100%;
            margin-bottom: 15px;
            overflow-x: auto;
            overflow-y: hidden;
            -webkit-overflow-scrolling: touch;
            -ms-overflow-style: -ms-autohiding-scrollbar;
            border: 1px solid #ddd;
            height: 192px;
        }

        .auto-style5 {
            margin-bottom: 15px;
            overflow-x: auto;
            overflow-y: hidden;
            -webkit-overflow-scrolling: touch;
            -ms-overflow-style: -ms-autohiding-scrollbar;
            border: 1px solid #ddd;
        }
    </style>
    <style>
        .tel {
            display: inline-block;
            width: 80%;
        }

        .telCode {
            display: inline-block;
            width: 19%;
        }

        .editBox {
            background: #ddd;
            padding: 15px;
        }

            .editBox .row {
                margin-bottom: 10px;
            }

        .editBottuns {
            float: left;
            color: #66f;
        }

        .historyTitle {
            text-align: center;
            padding: 15px;
        }

        .pcalWrapper {
            position: relative;
        }

            .pcalWrapper > a.pcalBtn {
                position: absolute;
                left: 7px;
                top: 0;
                bottom: 0;
                margin: auto;
            }

            .pcalWrapper > input {
                padding-left: 30px;
            }

        .pcalBtn.disabled {
            pointer-events: none;
            cursor: default;
        }

        .RadGridRTL .rgNumPart a {
            border: 1px solid #ccc;
            padding: 0px 8px !important;
            margin: 0 3px !important;
            border-radius: 5px;
        }

        .RadGrid .rgNumPart a.rgCurrentPage {
            background: #ddd;
            color: #000;
        }

        .RadGrid td.rgPagerCell {
            border-top: 1px solid #ccc !important;
        }
    </style>
    <link href="../../Theme/css/StyleUpload.css" rel="stylesheet" />
    <link href="../../Theme/css/Grid.Silk.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    درخواست ویرایش اطلاعات فردی
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <telerik:RadWindowManager ID="rwm_Validations" runat="server">
        </telerik:RadWindowManager>

    </p>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "../../../CommonUI/IntroPage.aspx";
        }
    </script>
    <div style="width: 100%" runat="server" id="div_info">
        <div class="container">
            <asp:Panel runat="server" ID="pnlEditInfo" class="editBox">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="tel" CssClass="alert alert-danger" />
                        <asp:ValidationSummary runat="server" ID="validationSummary2" ValidationGroup="mobile" CssClass="alert alert-danger" />
                        <asp:ValidationSummary runat="server" ID="validationSummary3" ValidationGroup="stateCity" CssClass="alert alert-danger" />
                        <asp:ValidationSummary runat="server" ID="validationSummary4" ValidationGroup="address" CssClass="alert alert-danger" />
                        <asp:ValidationSummary runat="server" ID="validationSummary5" ValidationGroup="postalCode" CssClass="alert alert-danger" />
                        <asp:ValidationSummary runat="server" ID="validationSummary" ValidationGroup="pic" CssClass="alert alert-danger" />
                    </div>
                </div>
                <%-- ---------------------------- --%>
                <div class="row">
                    <div class="col-sm-2">
                        <span>تاریخ تولد:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1011" CssClass="editBottuns"
                            ValidationGroup="Birthday"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvBirthday" ControlToValidate="txtBirthday" Display="None" ValidationGroup="Birthday"
                            ErrorMessage="تاریخ تولد را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtBirthday" runat="server" Visible="true" CssClass="form-control  pcal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        <span>کدپستی:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="8" CssClass="editBottuns"
                            ValidationGroup="postalCode"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvPostalCode" ControlToValidate="txt_CodePosti" Display="None" ValidationGroup="postalCode"
                            ErrorMessage="کد پستی را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_CodePosti" runat="server" Visible="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <%-- ---------------------------- --%>
                <div class="row">

                    <div class="col-sm-2">
                        <span>استان محل تولد: </span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1012" CssClass="editBottuns"
                            ValidationGroup="BirthPlace_state"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvBirthPlace_state" ControlToValidate="drpBirthPlace_State" Display="None" ValidationGroup="BirthPlace_State"
                            ErrorMessage="نام استان محل تولد را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList OnSelectedIndexChanged="drpBirthPlace_State_SelectedIndexChanged" AutoPostBack="true" ID="drpBirthPlace_State" runat="server" Visible="true" CssClass="form-control" Enabled="false"></asp:DropDownList>
                    </div>

                    <div class="col-sm-2">
                        <span>شهر محل تولد: </span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1015" CssClass="editBottuns" ID="btnEditCity_BirthPlace"
                            ValidationGroup="BirthPlace_City"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvBirthPlace_City" ControlToValidate="drpBirthPlace_City" Display="None" ValidationGroup="BirthPlace_City"
                            ErrorMessage="نام شهر محل تولد را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="drpBirthPlace_City" runat="server" Visible="true" CssClass="form-control" Enabled="false"></asp:DropDownList>
                    </div>
                </div>
                <%-- ---------------------------- --%>
                <div class="row">

                    <div class="col-sm-2">
                        <span>استان محل صدور:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1013" CssClass="editBottuns"
                            ValidationGroup="IssuePlace_State"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvIssuePlace_State" ControlToValidate="drpIssuePlace_State" Display="None" ValidationGroup="IssuePlace_State"
                            ErrorMessage="نام استان محل صدور را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList OnSelectedIndexChanged="drpIssuePlace_State_SelectedIndexChanged" AutoPostBack="true" ID="drpIssuePlace_State" runat="server" Visible="true" CssClass="form-control" Enabled="false"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <span>شهر محل صدور:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1016" CssClass="editBottuns" ID="btnEditCity_IssuePlace"
                            ValidationGroup="IssuePlace_City"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvIssuePlace_City" ControlToValidate="drpIssuePlace_City" Display="None" ValidationGroup="IssuePlace_City"
                            ErrorMessage="نام شهر محل صدور را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="drpIssuePlace_City" runat="server" Visible="true" CssClass="form-control" Enabled="false"></asp:DropDownList>
                    </div>


                </div>
                <%-- ---------------------------- --%>
                <div class="row">
                    <div class="col-sm-2">
                        <span>تلفن منزل:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="6" CssClass="editBottuns"
                            ValidationGroup="tel"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvTel" ControlToValidate="txt_Tell" Display="None" ValidationGroup="tel"
                            ErrorMessage="شماره تلفن را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator runat="server" ID="rfvPish" ControlToValidate="txt_Pishshomare" Display="None" ValidationGroup="tel"
                            ErrorMessage="پیش شماره را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_Tell" runat="server" Visible="true" CssClass="form-control tel" Enabled="false"></asp:TextBox>
                        <asp:TextBox ID="txt_Pishshomare" runat="server" Visible="true" CssClass="form-control telCode" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        <span>تلفن همراه:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="9" CssClass="editBottuns"
                            ValidationGroup="mobile"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvMobile" ControlToValidate="txt_Hamrah" Display="None" ValidationGroup="mobile"
                            ErrorMessage="شماره تلفن همراه را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_Hamrah" runat="server" Visible="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <%-- ---------------------------- --%>
                <div class="row">
                    <div class="col-sm-2">
                        <span>استان محل سکونت:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="12" CssClass="editBottuns"
                            ValidationGroup="stateCity"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvState" ControlToValidate="ddl_Ostan" Display="None" ValidationGroup="stateCity"
                            ErrorMessage="نام استان محل سکونت را انتخاب نمائید." Enabled="false" InitialValue="انتخاب نمایید.."></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddl_Ostan" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="drd_ostan_SelectedIndexChanged"
                            DataTextField="Title" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <span>شهر محل سکونت:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="11" CssClass="editBottuns" ID="btnEditCity"
                            ValidationGroup="stateCity"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvCity" ControlToValidate="ddl_Shahr" Display="None" ValidationGroup="stateCity"
                            ErrorMessage="نام شهر محل سکونت را انتخاب نمائید." Enabled="false" InitialValue="انتخاب نمایید.."></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddl_Shahr" DataValueField="ID" DataTextField="Title" runat="server" CssClass="form-control" Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <%-- ---------------------------- --%>
                <div class="row">
                    <div class="col-sm-2">
                        <span>آدرس:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="7" CssClass="editBottuns"
                            ValidationGroup="address"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvAddress" ControlToValidate="txt_Address" Display="None" ValidationGroup="address"
                            ErrorMessage="آدرس را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_Address" runat="server" Visible="true" TextMode="MultiLine" CssClass="form-control" Enabled="false"></asp:TextBox>
                        <div style="color: red">*آدرس بدون ذکر نام استان و شهر نوشته شود و نام استان و شهر از منوهای بالا انتخاب گردد</div>
                    </div>
                    <div class="col-sm-2">
                        <span>عکس پرسنلی:</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="10" CssClass="editBottuns" ID="btnPic"
                            ValidationGroup="pic"></asp:LinkButton>
                    </div>
                    <div class="col-sm-4">
                        <telerik:RadBinaryImage ID="img_Personal" runat="server" Height="150px" ResizeMode="Fit" Width="124px" Visible="true" />
                        <asp:CustomValidator runat="server" ID="rfvPic" ClientValidationFunction="validateUpload" Display="None" ValidationGroup="pic"
                            ErrorMessage="فایل عکس پرسنلی را انتخاب نمائید." Enabled="false"></asp:CustomValidator>
                        <telerik:RadAsyncUpload ID="RadAsyncUploadimg" runat="server" AllowedFileExtensions="jpg,jpeg" MaxFileSize="204800" MaxFileInputsCount="1" PostbackTriggers="btnPic" Visible="true" Height="16px" Width="777px" OnClientValidationFailed="validationFailed">
                            <Localization Cancel="انصراف" Remove="حذف" Select="انتخاب" />
                        </telerik:RadAsyncUpload>
                    </div>
                </div>
                <%-- ---------------------------- --%>

                <br />
                <hr />
                <br />
                <%-- ---------------------------- --%>
                <div class="row">
                    <div class="col-md-2">
                        <span>آدرس ایمیل</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1014" CssClass="editBottuns"
                            ValidationGroup="email"></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ControlToValidate="txt_Email" Display="None" ValidationGroup="email"
                            ErrorMessage="آدرس ایمیل را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox Enabled="false" ID="txt_Email" runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>ملیت</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1003" CssClass="editBottuns"
                            ValidationGroup="Nationality"></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvNationality" ControlToValidate="drpNationality" Display="None" ValidationGroup="Nationality"
                            ErrorMessage="ملیت را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList Enabled="false" ID="drpNationality" runat="server" Visible="true" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <%-- ---------------------------- --%>
                <div class="row">
                    <div class="col-md-2">
                        <span>دین</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1001" CssClass="editBottuns"
                            ValidationGroup="religion"></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="drpReligion" Display="None" ValidationGroup="religion"
                            ErrorMessage="دین را به درستی انتخاب فرمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList Enabled="false" ID="drpReligion" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpReligion_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <div class="col-md-2">
                        <span>وضعیت جسمانی</span>
                        <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1005" CssClass="editBottuns"
                            ValidationGroup="BodyStatus"></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:RequiredFieldValidator runat="server" ID="rfvBodyStatus" ControlToValidate="drpAccommodation" Display="None" ValidationGroup="BodyStatus"
                            ErrorMessage="لطفا وضعیت جسمانی را به درستی تعیین فرمایید" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:DropDownList Enabled="false" ID="ddlBodyStatus" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                </div>
                <%-- ---------------------------- --%>

                <div id="initialRegisterDiv" runat="server">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Panel runat="server" ID="pnlReligionSect">
                                <span>مذهب</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1002" CssClass="editBottuns"
                                    ValidationGroup="ReligionSect"></asp:LinkButton>
                            </asp:Panel>
                        </div>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator runat="server" ID="rfvReligionSect" ControlToValidate="drpReligionSect" Display="None" ValidationGroup="ReligionSect"
                                ErrorMessage="مذهب را به درستی انتخاب فرمایید" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:DropDownList Enabled="false" ID="drpReligionSect" runat="server" CssClass="form-control">
                                <asp:ListItem Text="شیعه" Value="1"></asp:ListItem>
                                <asp:ListItem Text="سنی" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <span>وضعیت اسکان</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1004" CssClass="editBottuns"
                                ValidationGroup="Accommodation"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator runat="server" ID="rfvAccommodation" ControlToValidate="drpAccommodation" Display="None" ValidationGroup="Accommodation"
                                ErrorMessage="لطفا وضعیت اسکان را به درستی تعیین فرمایید" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:DropDownList Enabled="false" ID="drpAccommodation" runat="server" CssClass="form-control">
                                <asp:ListItem Text="بومی" Value="1"></asp:ListItem>
                                <asp:ListItem Text="غیر بومی" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%-- ---------------------------- --%>
                    <div class="row">
                        <div class="col-md-2">
                            <span>نحوه آشنایی با واحد</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1006" CssClass="editBottuns"
                                ValidationGroup="IntroductionMethod"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator runat="server" ID="rfvIntroductionMethod" ControlToValidate="drpIntroductionMethod" Display="None" ValidationGroup="IntroductionMethod"
                                ErrorMessage="لطفا نحوه آشنایی با واحد الکترونیکی را انتخاب فرمایید" InitialValue="-1" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:DropDownList Enabled="false" ID="drpIntroductionMethod" runat="server" CssClass="form-control">
                                <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="دوستان و اقوام" Value="1"></asp:ListItem>
                                <asp:ListItem Text="روزنامه های کثیرالانتشار" Value="2"></asp:ListItem>
                                <asp:ListItem Text="سایت های خبری" Value="3"></asp:ListItem>
                                <asp:ListItem Text="صدا و سیما" Value="4"></asp:ListItem>
                                <asp:ListItem Text="بیلبورد" Value="5"></asp:ListItem>
                                <asp:ListItem Text="دفترچه راهنمای انتخاب رشته" Value="6"></asp:ListItem>
                                <asp:ListItem Text="سایر" Value="7"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <span>نوع دانشگاه مدرک پایه</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1007" CssClass="editBottuns"
                                ValidationGroup="BaseEducationUniType"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator runat="server" ID="rfvBaseEducationUniType" ControlToValidate="drpBaseEducationUniType" Display="None" ValidationGroup="BaseEducationUniType"
                                ErrorMessage="نوع دانشگاه مدرک پایه را مشخص فرمایید" InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:DropDownList Enabled="false" ID="drpBaseEducationUniType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="انتخاب نمایید" Value="0" />
                                <asp:ListItem Text="دانشگاه آزاد اسلامی" Value="1" />
                                <asp:ListItem Text="دانشگاه دولتی" Value="2" />
                                <asp:ListItem Text="دانشگاه غیر انتفاعی" Value="3" />
                                <asp:ListItem Text="دانشگاه علمی و کاربردی" Value="4" />
                                <asp:ListItem Text="دانشگاه پیام نور" Value="5" />
                                <asp:ListItem Text="حوزه" Value="6" />
                                <asp:ListItem Text="خارج از کشور" Value="7" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <%-- ---------------------------- --%>
                    <div class="row">
                        <div class="col-md-2">
                            <span>نحوه اتصال به کلاس ها</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1008" CssClass="editBottuns"
                                ValidationGroup="ServiceProvider"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator runat="server" ID="rfvConnectionType" ControlToValidate="drpConnectionType" Display="None" ValidationGroup="ServiceProvider"
                                ErrorMessage="لطفا نحوه اتصال را مشخص فرمایید" InitialValue="-1" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:DropDownList Enabled="false" ID="drpConnectionType" runat="server" Visible="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpConnectionType_SelectedIndexChanged">
                                <asp:ListItem Value="-1" Text="انتخاب کنید"></asp:ListItem>
                                <asp:ListItem Value="1" Text="اینترنت همراه"></asp:ListItem>
                                <asp:ListItem Value="2" Text="اینترنت ADSL خانگی"></asp:ListItem>
                                <asp:ListItem Value="3" Text="وایمکس"></asp:ListItem>
                                <asp:ListItem Value="4" Text="TD-LTE"></asp:ListItem>
                                <asp:ListItem Value="5" Text="سایر"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <span>نام ارائه دهنده سرویس</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1009" CssClass="editBottuns"
                                ValidationGroup="ServiceProvider"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:RequiredFieldValidator runat="server" ID="rfverviceProvider" ControlToValidate="drpServiceProvider" Display="None" ValidationGroup="ServiceProvider"
                                ErrorMessage="لطفا نام ارائه دهنده سرویس را انتخاب فرمایید" InitialValue="-1" Enabled="false"></asp:RequiredFieldValidator>
                            <asp:DropDownList Enabled="false" ID="drpServiceProvider" runat="server" Visible="true" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <%-- ---------------------------- --%>
                    <div class="row">
                        <div class="col-md-2">
                            <span>تجهیزات ارتباطی</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1010" CssClass="editBottuns"
                                ValidationGroup="CommunicationEquipment"></asp:LinkButton>
                        </div>
                        <div class="col-md-10">

                            <asp:CheckBoxList ID="chbCommunicationEquipment" runat="server" Visible="true" RepeatDirection="Horizontal" Enabled="false">
                                <asp:ListItem Text="کامپیوتر شخصی" Value="1"></asp:ListItem>
                                <asp:ListItem Text="لپتاپ" Value="2"></asp:ListItem>
                                <asp:ListItem Text="تبلت_اندروید" Value="3"></asp:ListItem>
                                <asp:ListItem Text="تبلت_IOS" Value="4"></asp:ListItem>
                                <asp:ListItem Text="گوشی موبایل_اندروید" Value="5"></asp:ListItem>
                                <asp:ListItem Text="گوشی موبایل_IOS" Value="6"></asp:ListItem>
                                <asp:ListItem Text="هدست" Value="7"></asp:ListItem>
                                <asp:ListItem Text="قلم نوری" Value="8"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <%-- ---------------------------- --%>

                    <br />
                    <hr />
                    <h3 style="text-align: center; font-style: italic">اطلاعات تحصیل همزمان</h3>
                    <br />
                    <%-- ---------------------------- --%>
                    <div class="row">
                        <div class="col-md-2">
                            <span>آیا به جز این دانشگاه، در حال تحصیل به صورت همزمان هستید؟</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1100" CssClass="editBottuns"
                                ValidationGroup="EduLevel"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">

                            <asp:DropDownList Enabled="false" ID="drpSyncEdu" runat="server" AutoPostBack="true" Visible="true" CssClass="form-control" OnSelectedIndexChanged="drpSyncEdu_SelectedIndexChanged">
                                <asp:ListItem Text="بله" Value="1"></asp:ListItem>
                                <asp:ListItem Text="خیر" Value="2" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <%-- ---------------------------- --%>
                    <div id="dvSyncEdu" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-2">
                                <span>مقطع</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1101" CssClass="editBottuns"
                                    ValidationGroup="EduLevel"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvEduLevel" ControlToValidate="drpEduLevel" Display="None" ValidationGroup="EduLevel"
                                    ErrorMessage="لطفا مقطع تحصیلی همزمان را تعیین فرمایید" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList Enabled="false" ID="drpEduLevel" runat="server" Visible="true" CssClass="form-control">
                                    <asp:ListItem Text="انتخاب نمایید" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="کارداني" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="کارداني پيوسته" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="کارشناسي" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="کارشناسي نا پيوسته" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="کارشناسي ارشد" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="دکتري تخصصي" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="دکتري حرفه اي" Value="10"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <span>رشته تحصیلی</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1102" CssClass="editBottuns"
                                    ValidationGroup="EduField"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvEduField" ControlToValidate="drpEduField" Display="None" ValidationGroup="EduField"
                                    ErrorMessage="لطفا رشته تحصیلی همزمان را تعیین فرمایید" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList Enabled="false" ID="drpEduField" runat="server" Visible="true" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>نوع دانشگاه</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1103" CssClass="editBottuns"
                                    ValidationGroup="UniType"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="rfvUniType" runat="server" ControlToValidate="drpUniType" Display="None" ValidationGroup="UniType"
                                    ErrorMessage="نوع دانشگاه تحصیل همزمان را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpUniType" Enabled="false" runat="server" Visible="true" CssClass="form-control">
                                    <asp:ListItem Text="انتخاب نمایید" Value="0" />
                                    <asp:ListItem Text="دانشگاه آزاد اسلامی" Value="1" />
                                    <asp:ListItem Text="دانشگاه دولتی" Value="2" />
                                    <asp:ListItem Text="دانشگاه غیر انتفاعی" Value="3" />
                                    <asp:ListItem Text="دانشگاه علمی و کاربردی" Value="4" />
                                    <asp:ListItem Text="دانشگاه پیام نور" Value="5" />
                                    <asp:ListItem Text="حوزه" Value="6" />
                                    <asp:ListItem Text="خارج از کشور" Value="7" />
                                </asp:DropDownList>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2">
                                <span>نام دانشگاه</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1104" CssClass="editBottuns"
                                    ValidationGroup="UniName"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvUniName" ControlToValidate="drpUniName" Display="None" ValidationGroup="UniName"
                                    ErrorMessage="نام دانشگاه محل تحصیل همزمان را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpUniName" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>سال ورود</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1105" CssClass="editBottuns"
                                    ValidationGroup="EnterYear"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvEnterYear" ControlToValidate="txtEnterYear" Display="None" ValidationGroup="EnterYear"
                                    ErrorMessage="سال ورود به دانشگاه تحصیل همزمان را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtEnterYear" Enabled="false" runat="server" Visible="true" CssClass="form-control" MaxLength="4"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <%-- ---------------------------- --%>
                    <br />
                    <hr />
                    <h3 style="text-align: center; font-style: italic">اطلاعات شغلی</h3>
                    <br />
                    <%-- ---------------------------- --%>
                    <div class="row">
                        <div class="col-md-2">
                            <span>وضعیت اشتغال</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1200" CssClass="editBottuns"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpJobstatus" Enabled="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpJobstatus_SelectedIndexChanged" Visible="true" CssClass="form-control">
                                <asp:ListItem Text="شاغل" Value="1"></asp:ListItem>
                                <asp:ListItem Text="غیر شاغل" Value="0" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="dvEmploy" runat="server" visible="false">
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>عنوان شغلی</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1201" CssClass="editBottuns"
                                    ValidationGroup="jobTitle"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvJobTitle" ControlToValidate="txtJobTitle" Display="None" ValidationGroup="jobTitle"
                                    ErrorMessage="عنوان شغلی خود را انتخاب نمایید" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtJobTitle" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2">
                                <span>نوع همکاری</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1202" CssClass="editBottuns"
                                    ValidationGroup="HireType"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvHireType" ControlToValidate="drpHireType" Display="None" ValidationGroup="HireType"
                                    ErrorMessage="لطفا نوع همکاری را انتخاب فرمایید" Enabled="false" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpHireType" Enabled="false" runat="server" Visible="true" CssClass="form-control">
                                    <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="تمام وقت" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="پاره وقت" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>نحوه همکاری(نوع استخدام)</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1203" CssClass="editBottuns"
                                    ValidationGroup="JobContract"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator ID="rfvJobContract" runat="server" ControlToValidate="drpJobContract" Display="None" ValidationGroup="JobContract"
                                    ErrorMessage="لطفا نحوه همکاری را انتخاب فرمایید" Enabled="false" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpJobContract" Enabled="false" runat="server" Visible="true" CssClass="form-control">
                                    <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="رسمی" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="قراردادی" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="ساعتی" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="سایر" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2">
                                <span>نوع محل اشتغال</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1204" CssClass="editBottuns"
                                    ValidationGroup="WorkplaceType"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvWorkplaceType" ControlToValidate="drpWorkplaceType" Display="None" ValidationGroup="WorkplaceType"
                                    ErrorMessage="لطفا نوع محل اشتغال را انتخاب فرمایید" Enabled="false" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpWorkplaceType" Enabled="false" runat="server" Visible="true" CssClass="form-control">
                                    <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="آزاد" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="دولتی" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="خصوصی" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>سمت</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1205" CssClass="editBottuns"
                                    ValidationGroup="JobPosition"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvJobPosition" ControlToValidate="drpJobPosition" Display="None" ValidationGroup="JobPosition"
                                    ErrorMessage="لطفا سمت شغلی خود را انتخاب فرمایید" Enabled="false" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpJobPosition" Enabled="false" runat="server" Visible="true" CssClass="form-control">
                                    <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="کارمند" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="کارگر" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="مدیر" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="مدیر کل" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="کارشناس" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="مدیرعامل" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="سایر" Value="7"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>تلفن محل کار</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1206" CssClass="editBottuns"
                                    ValidationGroup="telJob"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvTel_Job" ControlToValidate="txtTel_Job" Display="None" ValidationGroup="telJob"
                                    ErrorMessage="شماره تلفن را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator runat="server" ID="rfvZipCode_Job" ControlToValidate="txtZipCode_Job" Display="None" ValidationGroup="telJob"
                                    ErrorMessage="پیش شماره را وارد نمائید." Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtTel_Job" Enabled="false" runat="server" Visible="true" CssClass="form-control  tel" MaxLength="8"></asp:TextBox>
                                <asp:TextBox ID="txtZipCode_Job" Enabled="false" runat="server" Visible="true" CssClass="form-control telCode" MaxLength="3"></asp:TextBox>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2">
                                <span>کد پستی محل کار</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1207" CssClass="editBottuns"
                                    ValidationGroup="PostalCode_Job"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvPostalCode_Job" ControlToValidate="txtPostalCode_Job" Display="None" ValidationGroup="PostalCode_Job"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtPostalCode_Job" Enabled="false" runat="server" Visible="true" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>استان</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1208" CssClass="editBottuns"
                                    ValidationGroup="State_Work"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvState_Work" ControlToValidate="drpState_Work" Display="None" ValidationGroup="State_Work"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpState_Work" Enabled="false" runat="server" Visible="true" CssClass="form-control" OnSelectedIndexChanged="drpState_Work_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2">
                                <span>شهرستان</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1209" CssClass="editBottuns" ID="btnCity_Work"
                                    ValidationGroup="City_Work"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvCity_Work" ControlToValidate="drpCity_Work" Display="None" ValidationGroup="City_Work"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpCity_Work" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>آدرس</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1210" CssClass="editBottuns"
                                    ValidationGroup="Address_wor"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvAddress_work" ControlToValidate="txtAddress_work" Display="None" ValidationGroup="Address_work"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtAddress_work" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <%-- ---------------------------- --%>
                    </div>
                    <br />
                    <hr />
                    <h3 style="text-align: center; font-style: italic">اطلاعات خانواده</h3>
                    <br />
                    <%-- ---------------------------- --%>
                    <div class="row">
                        <div class="col-md-2">
                            <span>وضعیت تاهل</span>
                            <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1300" CssClass="editBottuns"
                                ValidationGroup="MaritalStatus"></asp:LinkButton>
                        </div>
                        <div class="col-md-4">

                            <asp:DropDownList Enabled="false" ID="drpMaritalStatus" AutoPostBack="true" runat="server" Visible="true" CssClass="form-control" OnSelectedIndexChanged="drpMaritalStatus_SelectedIndexChanged">
                                <asp:ListItem Text="مجرد" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="متاهل" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="dvMaritalStatus" runat="server" visible="false">
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>نام همسر</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1301" CssClass="editBottuns"
                                    ValidationGroup="Name_Spouse"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvName_Spouse" ControlToValidate="txtName_Spouse" Display="None" ValidationGroup="Name_Spouse"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtName_Spouse" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2">
                                <span>نام خانوادگی همسر</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1302" CssClass="editBottuns"
                                    ValidationGroup="Lastname_spouse"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvLastname_spouse" ControlToValidate="txtLastname_spouse" Display="None" ValidationGroup="Lastname_spouse"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtLastname_spouse" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <div class="row">
                            <div class="col-md-2">
                                <span>وضعیت اشتغال همسر</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1303" CssClass="editBottuns"
                                    ValidationGroup="Jobstatus_spouse"></asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:RequiredFieldValidator runat="server" ID="rfvJobstatus_spouse" ControlToValidate="drpJobstatus_spouse" Display="None" ValidationGroup="Jobstatus_spouse"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="drpJobstatus_spouse" AutoPostBack="true" Enabled="false" runat="server" Visible="true" CssClass="form-control" OnSelectedIndexChanged="drpJobstatus_spouse_SelectedIndexChanged">
                                    <asp:ListItem Text="شاغل" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="غیر شاغل" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <%-- ---------------------------- --%>
                            <div class="col-md-2" id="dvJobTitle_spouse_Text" runat="server">
                                <span>عنوان شغلی همسر</span>
                                <asp:LinkButton runat="server" Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CommandArgument="1304" CssClass="editBottuns"
                                    ValidationGroup="JobTitle_spouse"></asp:LinkButton>
                            </div>
                            <div class="col-md-4" id="dvJobTitle_spouse" runat="server">
                                <asp:RequiredFieldValidator runat="server" ID="rfvJobTitle_spouse" ControlToValidate="txtJobTitle_spouse" Display="None" ValidationGroup="JobTitle_spouse"
                                    ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtJobTitle_spouse" Enabled="false" runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="upnlChildren" runat="server">
                                    <ContentTemplate>
                                        <telerik:RadGrid runat="server" ID="rgvChildren" OnNeedDataSource="rgvChildren_NeedDataSource" AutoGenerateColumns="false"
                                            OnItemDataBound="rgvChildren_ItemDataBound" CssClass="width93" OnItemCommand="rgvChildren_ItemCommand">
                                            <MasterTableView CssClass="ex1 noPad" HeaderStyle-CssClass="gridHead">
                                                <Columns>
                                                    <telerik:GridTemplateColumn HeaderText="نام فرزند">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Text='<%# Eval("FirstName") %>' MaxLength="50"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="نام خانوادگی فرزند">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Text='<%# Eval("LastName") %>' MaxLength="50"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="جنسیت فرزند">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control">
                                                                <asp:ListItem Text="انتخاب کنید" Value="-1"></asp:ListItem>
                                                                <asp:ListItem Text="پسر" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="دختر" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="تاریخ تولد فرزند">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" Text='<%# Eval("ShamsiBirthDate") %>' MaxLength="10"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Button ID="addEditChild" runat="server" Text="ثبت و اعمال تغییرات" CommandName="addEditChild_Click" CommandArgument='<%# Eval("id") %>' />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                        <asp:Button runat="server" ID="btnAddChild" Text="افزودن فرزند" CssClass="btn btn-primary" OnClick="btnAddChild_Click" Style="font-size: 14px; font-family: tahoma;" />
                                        <asp:Label runat="server" ID="lblErr" ForeColor="Red" Text="لطفا تمامی فیلد های مربوط به فرزند را تکمیل فرمایید." Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <%-- ---------------------------- --%>
                    <%--<div class="row">
                    <div class="col-md-2">
                        <span>fillIT</span>
                        <asp:LinkButton runat="server"  Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CssClass="editBottuns"
                            ValidationGroup="FillIT"></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:RequiredFieldValidator runat="server"  ControlToValidate="fillIT" Display="None" ValidationGroup="fillIT"
                            ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox  runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <span>fillIT</span>
                        <asp:LinkButton runat="server"  Text="<i class='fa fa-pencil'></i> <span>ویرایش</span>" OnClick="btnEdit_Click" CssClass="editBottuns"
                            ValidationGroup="FillIT"></asp:LinkButton>
                    </div>
                    <div class="col-md-4">
                        <asp:RequiredFieldValidator runat="server"  ControlToValidate="fillIT" Display="None" ValidationGroup="fillIT"
                            ErrorMessage="fillIT" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:TextBox  runat="server" Visible="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>--%>
                </div>
            </asp:Panel>

            <div class="row">
                <div class="col-md-12 historyTitle">سوابق درخواست ها</div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:UpdatePanel runat="server" ID="upnlGrid">
                        <ContentTemplate>
                            <telerik:RadGrid ID="grd_EditeRequestState" BorderStyle="Solid" Width="100%" AutoGenerateColumns="false" runat="server" Visible="false" OnNeedDataSource="grd_EditeRequeststate_NeedDataSource"
                                EnableEmbeddedSkins="false" AllowPaging="true" PageSize="20">
                                <PagerStyle PageSizeLabelText="تعداد در صفحه" ShowPagerText="false" Mode="NumericPages" />
                                <MasterTableView>
                                    <HeaderStyle CssClass="bg-blue" HorizontalAlign="Center" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="createdate" HeaderText="تاریخ" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px" />
                                        <telerik:GridBoundColumn DataField="darkhast" HeaderText="ویرایش درخواست شده" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="400px" />
                                        <telerik:GridBoundColumn DataField="vaziat" HeaderText="وضعیت" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" />
                                        <telerik:GridBoundColumn DataField="tozihat" HeaderText="توضیحات" ItemStyle-HorizontalAlign="right" />
                                    </Columns>
                                </MasterTableView>

                            </telerik:RadGrid>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //<![CDATA[
        var $ = $telerik.$;
        function validationFailed(radAsyncUpload, args) {
            var $row = $(args.get_row());
            var erorMessage = getErrorMessage(radAsyncUpload, args);
            var span = createError(erorMessage);
            $row.addClass("ruError");
            $row.append(span);
        }

        function getErrorMessage(sender, args) {
            var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
            if (args.get_fileName().lastIndexOf('.') != -1) {
                if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1)
                    return ("نوع تصویر غیر قابل قبول است");
                else
                    return ("حجم فایل بیش از حد مجاز است");
            }
            else {
                return ("not correct extension.");
            }
        }

        function createError(erorMessage) {
            var input = '<span class="ruErrorMessage">' + erorMessage + ' </span>';
            return input;
        }
        //]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        Telerik.Web.UI.RadAsyncUpload.prototype.getUploadedFiles = function () {
            var files = [];

            $telerik.$(".ruUploadSuccess", this.get_element()).each(function (index, value) {
                files[index] = $telerik.$(value).text();
            });
            return files;
        }
        function validateUpload(sender, args) {
            var upload = $find("<%= RadAsyncUploadimg.ClientID %>");
            args.IsValid = upload.getUploadedFiles().length != 0;
        }
        //]]>
    </script>
</asp:Content>
