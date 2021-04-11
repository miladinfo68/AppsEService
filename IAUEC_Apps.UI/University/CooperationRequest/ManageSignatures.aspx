<%@ Page Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ManageSignatures.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ManageSignatures" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .manageSignaturesWrapper {
            direction: rtl;
            border: 1px solid #ccc;
            padding: 15px;
            border-radius: 5px;
        }

        .searchBox, .searchResult {
            border: 1px solid #dedede;
            border-radius: 5px;
            padding: 15px;
        }

        .searchResult {
            margin-top: 15px;
        }

        .formControl {
            border-radius: 0;
            line-height: 30px;
            display: block;
            height: 34px;
            padding: 0 12px;
            font-size: 14px;
            background-color: #fff;
            background-image: none;
            border: 1px solid #DDE2E8;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,0.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,0.075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            text-align: right;
            width: 100%;
        }

        .imgSignatureBox {
            border: 1px solid #dedede;
            width: 310px;
            height: 310px;
            padding: 5px;
            margin: 0 auto 15px;
            text-align: center;
        }

        .infoBox > div {
            height: 40px;
        }

            .infoBox > div:nth-child(3) {
                margin-top: 65px;
            }

        .textCenter {
            text-align: center;
        }

        .RadUpload_Silk .ruStyled .ruFakeInput {
            width: 225px;
        }

        .teacherSearchBox, .userSearchBox {
            margin: 5px 20px;
            background: #ddd;
            padding: 20px 20px 15px 0;
            border-radius: 5px;
        }

        .pnlSearchResult {
            border: 1px solid #ddd;
            border-radius: 3px;
            margin: 5px 5px 5px 13px;
            padding: 15px;
        }

        .userInfo {
            height: 65px;
        }

        .RadUpload.rapSignature {
            margin: 0 auto;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
   <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="imgSignature" LoadingPanelID="LoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="LoadingPanel1" runat="server" Width="256px" Height="64px" MinDisplayTime="0">
        <asp:Label ID="lblLoading" runat="server">در حال بارگزاری ...</asp:Label>
    </telerik:RadAjaxLoadingPanel>


    <div class="manageSignaturesWrapper">

        <div class="row">
            <div class="validationMsg col-sm-12">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:ValidationSummary runat="server" ID="vsSearchTeacher" CssClass="alert alert-danger col-sm-12" ValidationGroup="searchTeacher" />
                    </div>
                    <div class="col-sm-12">
                        <asp:ValidationSummary runat="server" ID="vsSearchUser" CssClass="alert alert-danger col-sm-12" ValidationGroup="searchUser" />
                    </div>
                    <div class="col-sm-12">
                        <asp:Panel runat="server" ID="pnlSearchMessage" Visible="false" CssClass="alert alert-danger">
                            <asp:Label runat="server" ID="lblMessageText"></asp:Label>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="row">
                    <asp:Panel runat="server" ID="pnlTeacherSearchBox" class="col-sm-11 teacherSearchBox">

                        <div class="col-sm-7">
                            <asp:RequiredFieldValidator runat="server" ID="rfvSearchText" ErrorMessage="کد کاربری را وارد کنید." Display="None" ControlToValidate="txtSearch" ValidationGroup="searchTeacher">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ErrorMessage="کد کاربری صحیح نیست." Display="None"
                                ControlToValidate="txtSearch" ValidationGroup="searchTeacher" ValidationExpression="^[0-9]+$">
                            </asp:RegularExpressionValidator>
                            <asp:TextBox runat="server" ID="txtSearch" CssClass="col-sm-3 formControl" placeholder="کد کاربری" MaxLength="7"></asp:TextBox>
                        </div>
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-info col-sm-4" Text="جستجوی استاد" OnClick="btnSearch_Click" ValidationGroup="searchTeacher" />
                    </asp:Panel>
                </div>
                <div class="row">
                    <asp:Panel runat="server" ID="pnlUserSearchBox" class="col-sm-11 userSearchBox">
                        <div class="col-sm-7">
                            <asp:RequiredFieldValidator runat="server" ID="rfvUserType" ErrorMessage="نوع کاربر را انتخاب کنید." Display="None" ControlToValidate="ddlUserType" ValidationGroup="searchUser"
                                InitialValue="0"></asp:RequiredFieldValidator>
                            <asp:DropDownList runat="server" ID="ddlUserType" CssClass="formControl">
                                <asp:ListItem Selected="True" Value="0" Text="نوع کاربر را انتخاب نمایید"></asp:ListItem>
                                <%--<asp:ListItem Text="سرپرست دانشگاه" Value="77"></asp:ListItem>
                                <asp:ListItem Text="مسئول حق التدریس" Value="11"></asp:ListItem>
                                <asp:ListItem Text="کارشناس کارگزینی هیأت علمی" Value="12"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <asp:Button runat="server" ID="btnSearchUser" CssClass="btn btn-info col-sm-4" Text="جستجوی مسئول" OnClick="btnSearchUser_Click" ValidationGroup="searchUser" />
                    </asp:Panel>
                </div>

                <asp:Panel runat="server" ID="pnlSearchResult" Visible="false" CssClass="pnlSearchResult">
                    <div class="row userInfo">
                        <div class="col-sm-6">
                            <span class="col-sm-3">سمت: </span>
                            <asp:Label runat="server" ID="lblUserCode" CssClass="col-sm-9"></asp:Label>
                        </div>
                        <div class="col-sm-6">
                            <span class="col-sm-6">نام و نام خانوادگی: </span>
                            <asp:Label runat="server" ID="lblFirstName" CssClass="col-sm-6"></asp:Label>
                        </div>
                    </div>

                </asp:Panel>

            </div>
            <asp:Panel runat="server" ID="pnlEditSignature" Visible="false">
                <div class="col-sm-6">
                    <div class="imgSignatureBox">
                        <asp:Image runat="server" ID="imgSignature" Width="300px" Height="300px" />
                    </div>
                    <telerik:RadAsyncUpload ID="rapSignature" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="102400" UploadedFilesRendering="BelowFileInput"
                        OnClientValidationFailed="validationFailed" Skin="Silk" MaxFileInputsCount="1" OnClientFileUploaded="flieUploaded"
                        OnFileUploaded="rapSignature_FileUploaded" CssClass="rapSignature">
                        <Localization Select="انتخاب" Remove="حذف" />
                    </telerik:RadAsyncUpload>
                    <telerik:RadBinaryImage ID="rbi" runat="server" />
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button runat="server" ID="btnSaveChanges" CssClass="btn btn-success" OnClick="btnSaveChanges_Click" Text="ثبت تغییرات" />
                            <asp:Button runat="server" ID="btnDeleteSignature" CssClass="btn btn-danger" OnClick="btnDeleteSignature_Click" Text="حذف امضا" />
                        </div>
                    </div>
                </div>

            </asp:Panel>
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
            if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                    return ("نوع تصویر غیر قابل قبول است");
                }
                else {
                    return ("حجم فایل بیش از حد مجاز است");
                }
            }
            else {
                return ("not correct extension.");
            }
        }

        function createError(erorMessage) {
            var input = '<span class="ruErrorMessage">' + erorMessage + ' </span>';
            return input;
        }

        function flieUploaded(sender, args) {
            $find("<%= RadAjaxManager1.ClientID%>").ajaxRequest(args.get_fileName());
        }
        //]]>
    </script>
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
