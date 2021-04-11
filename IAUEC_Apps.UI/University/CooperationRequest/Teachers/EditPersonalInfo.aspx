<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/TeacherMaster.Master" AutoEventWireup="true" CodeBehind="EditPersonalInfo.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.EditPersonalInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .lblInput {
            padding-top: 5px !important;
        }

        .rwContentRow th {
            font-family: 'B Titr' !important;
            font-weight: bold;
        }

        .rwContentRow {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2>مشخصات فردی</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="modal fade" id="cantReg_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="HT">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="msgIncorrectScan" runat="server" Mode="PassThrough"></asp:Literal>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                </div>
            </div>
        </div>
    </div>

    <div class="row" dir="rtl">
        <div class="col-sm-12">
            <div id="dvPersonInfo" runat="server" class="panel panel-primary">
                <div class="panel-heading">
                    <h4>مشخصات فردی</h4>
                </div>
                <div class="panel-body ">
                    <div class="row">
                        <div class="col-md-3">
                            <div style="padding-right: 30px;">
                                <div class="row">
                                    <div class="col-md-7">
                                        <p class="lblInput">کد ملی</p>
                                        <asp:TextBox ID="txtCodeMeli" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <p class="lblInput">نام</p>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <p class="lblInput">نام خانوادگی</p>
                                        <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>

                                <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-7">
                                                <p class="lblInput">نام پدر</p>
                                                <asp:RequiredFieldValidator ID="rfvtxtFatherName" runat="server" ErrorMessage="لطفا نام پدر را وارد فرمایید." ValidationGroup="inf" ControlToValidate="txtFatherName" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtFatherName" OnTextChanged="Control_StateChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-7">
                                                <p class="lblInput">شماره شناسنامه</p>
                                                <asp:RequiredFieldValidator ID="rfvtxtShCode" runat="server" ErrorMessage="لطفا شماره شناسنامه خود را وارد فرمایید." ValidationGroup="inf" ControlToValidate="txtShCode" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtShCode" OnTextChanged="Control_StateChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-7">
                                                <p class="lblInput">سال تولد</p>
                                                <asp:RequiredFieldValidator ID="rfvYearBorn" runat="server" ErrorMessage="لطفا سال تولد خود را وارد فرمایید." ValidationGroup="inf" ControlToValidate="txtYearBorn" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtYearBorn" OnTextChanged="Control_StateChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>

                                        <div class="row" id="dvShenasname" runat="server" visible="false">
                                            <div class="col-md-7">
                                                <p class="lblInput">اسکن شناسنامه:<span class="text-danger"></span></p>
                                                <div>
                                                    <telerik:RadAsyncUpload
                                                        ID="ruScanShenasname" runat="server" Width="100%"  OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب"  PostbackTriggers="btnSubmitChanges" LocalizationPath="مسیر">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvShenasname"
                                                        ClientValidationFunction="validateAsyncUploadSH" ValidationGroup="inf" ErrorMessage="لطفا اسکن شناسنامه را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                    
                                                </div>
                                                <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-md-3">


                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p class="lblInput">
                                                <span>آخرین مدرک تحصیلی</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ControlToValidate="drpLastMaghta"
                                                    ErrorMessage="لطفا سطح آخرین مدرک تحصیلی خود را مشخص فرمایید"
                                                    ForeColor="Red" Display="Dynamic" Text="*" runat="server"
                                                    InitialValue="انتخاب فرمایید" />
                                                <asp:DropDownList ID="drpLastMaghta" runat="server" OnSelectedIndexChanged="Control_StateChanged" AutoPostBack="true" CssClass="form-control form-inline dropdown">
                                                </asp:DropDownList>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p class="lblInput">
                                                <span>رشته تحصیلی</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ID="valField" Display="Static" ControlToValidate="drpReshte"
                                                    ForeColor="Red" Text="*" runat="server" InitialValue="جستجو و انتخاب فرمایید"
                                                    ErrorMessage="لطفا رشته تحصیلی خود را انتخاب فرمایید"></asp:RequiredFieldValidator>
                                            </p>
                                            <div>
                                                <telerik:RadComboBox ID="drpReshte" runat="server"
                                                    OnSelectedIndexChanged="Radcombo_SelectedIndexChanged"
                                                    AutoPostBack="true"
                                                    MarkFirstMatch="True"
                                                    Filter="Contains"
                                                    HighlightTemplatedItems="True"
                                                    RenderMode="Lightweight" Width="100%"
                                                    AllowCustomText="false"
                                                    ExpandDirection="Down" Culture="(Default)" Height="300px">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row"  id="dvMadrak" runat="server" visible="false">
                                        <div class="col-md-7">
                                            <p class="lblInput">اسکن مدرک تحصیلی:<span class="text-danger"></span></p>
                                            <div>
                                                 <telerik:RadAsyncUpload
                                                        ID="ruScanMadrak" runat="server" Width="100%" OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="1048576" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvMadrak"
                                                        ClientValidationFunction="validateAsyncUploadMdr" ValidationGroup="inf" ErrorMessage="لطفا اسکن آخرین مدرک تحصیلی خود را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                            </div>
                                            <code>حداکثر تا حجم 1024 کیلو بایت</code>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <p class="lblInput">
                                                <span>سال اخذ مدرک <code>چهار رقم</code></span>
                                                <asp:RangeValidator ID="RangeValidator1"
                                                    ErrorMessage="سال اخذ مدرک باید عددی بین 1300 تا 1400 باشد"
                                                    ControlToValidate="txtYearGetMadrak" runat="server"
                                                    Text="*" ForeColor="Red" MaximumValue="1400" MinimumValue="1300" Type="Integer" />
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ID="RequiredFieldValidator3"
                                                    runat="server" ControlToValidate="txtYearGetMadrak" Text="*" ForeColor="Red"
                                                    ErrorMessage="لطفا سال اخذ آخرین مدرک خود را درج فرمایید"></asp:RequiredFieldValidator>
                                            </p>
                                            <asp:TextBox runat="server" ID="txtYearGetMadrak" ToolTip="چهار رقم" OnTextChanged="Control_StateChanged" AutoPostBack="true" CssClass="form-control" MaxLength="4" />

                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p class="lblInput">
                                                <span>کشور محل اخذ مدرک</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ErrorMessage="لطفا کشور محل اخذ مدرک تحصیلی خود را انتخاب فرمایید."
                                                    ControlToValidate="drpCountry" runat="server"
                                                    ForeColor="Red" Display="Dynamic" Text="*"
                                                    InitialValue="انتخاب کنید" />
                                            </p>
                                            <asp:DropDownList ID="drpCountry" runat="server" OnSelectedIndexChanged="Control_StateChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row"  id="dvArzeshname" runat="server" visible="false">
                                        <div class="col-md-7">
                                            <p class="lblInput">اسکن ارزشنامه تحصیلی وزارت علوم:<span class="text-danger"></span></p>
                                            <div>
                                                 <telerik:RadAsyncUpload
                                                        ID="ruScanArzeshname" runat="server" Width="100%" OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvArzeshname"
                                                        ClientValidationFunction="validateAsyncUploadArz" ValidationGroup="inf" ErrorMessage="لطفا اسکن ارزشنامه تحصیلی خود را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                
                                            </div>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p class="lblInput">
                                                <span>نوع دانشگاه اخذ مدرک</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ID="valUniType" Display="Dynamic"
                                                    ErrorMessage="لطفا نوع دانشگاه محل تحصیل خود را انتخاب فرمایید"
                                                    ControlToValidate="drpUniversityType" runat="server" InitialValue="0" Text="*"
                                                    ForeColor="Red" />
                                            </p>
                                            <asp:DropDownList ID="drpUniversityType" runat="server" CssClass="form-control" OnSelectedIndexChanged="Control_StateChanged">
                                                <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                <asp:ListItem Text="دولتی" Value="1" />
                                                <asp:ListItem Text="آزاد" Value="2" />
                                                <asp:ListItem Text="حوزه" Value="3" />
                                                <asp:ListItem Text="خارج از کشور" Value="4" />
                                                <asp:ListItem Text="سایر" Value="5" />
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p class="lblInput">
                                                <span>نام دانشگاه محل اخذ آخرین مدرک تحصیلی</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ID="valUniName" Display="Dynamic"
                                                    ErrorMessage="لطفا نام دانشگاه محل تحصیل خود را انتخاب فرمایید"
                                                    ControlToValidate="drpUniName" runat="server" InitialValue="جستجو و انتخاب فرمایید" Text="*"
                                                    ForeColor="Red" />
                                            </p>
                                            <div>
                                                <telerik:RadComboBox ID="drpUniName"
                                                    runat="server"
                                                    OnSelectedIndexChanged="Radcombo_SelectedIndexChanged"
                                                    AutoPostBack="true" MarkFirstMatch="True"
                                                    Filter="Contains" HighlightTemplatedItems="True"
                                                    RenderMode="Lightweight" Width="100%" AllowCustomText="false"
                                                    ExpandDirection="Down" Height="300px">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div class="col-md-3">
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p>
                                                <span>جنسیت</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ControlToValidate="rblGender"
                                                    ErrorMessage="لطفا جنسیت خود را انتخاب فرمایید."
                                                    ForeColor="Red" Display="Dynamic" Text="*" runat="server"></asp:RequiredFieldValidator>
                                            </p>
                                            <asp:RadioButtonList ID="rblGender" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" CssClass=""
                                                OnSelectedIndexChanged="rblGender_SelectedIndexChanged">
                                                <asp:ListItem Text="مرد" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="زن" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <asp:Panel runat="server" ID="pnlMilitary">
                                        <div class="row">
                                            <div class="col-md-7">

                                                <p class="lblInput">
                                                    <span>وضعیت نظام وظیفه</span>
                                                    <asp:RequiredFieldValidator ValidationGroup="inf" ControlToValidate="drpNezam"
                                                        InitialValue="انتخاب فرمایید"
                                                        ErrorMessage="لطفا وضعیت نظام وظیفه خود را انتخاب فرمایید."
                                                        ForeColor="Red" Display="Dynamic" Text="*" runat="server" />
                                                </p>
                                                <asp:DropDownList ID="drpNezam" runat="server" OnSelectedIndexChanged="Control_StateChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="row"  id="dvNezam" runat="server" visible="false">
                                            <div class="col-md-7">
                                                <p class="lblInput">اسکن نظام وظیفه:<span class="text-danger"></span></p>

                                                <div>
                                                    
                                                 <telerik:RadAsyncUpload
                                                        ID="ruScanNezam" runat="server" Width="100%" OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvNezam"
                                                        ClientValidationFunction="validateAsyncUploadNzm" ValidationGroup="inf" ErrorMessage="لطفا اسکن نظام وظیفه خود را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                
                                                </div>
                                                <code>حداکثر تا حجم 700 کیلو بایت</code>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <p class="lblInput">
                                                <span>وضعیت تاهل</span>
                                                <asp:RequiredFieldValidator ValidationGroup="inf" ErrorMessage="لطفا وضعیت تاهل خود را مشخص فرمایید." ControlToValidate="rdblMarriage" ForeColor="Red" Display="Dynamic" Text="*" runat="server" />
                                                <asp:RadioButtonList ID="rdblMarriage" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Control_StateChanged" CssClass="">
                                                    <asp:ListItem Text="مجرد" Value="1" />
                                                    <asp:ListItem Text="متاهل" Value="2" />
                                                </asp:RadioButtonList>
                                            </p>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <p class="lblInput">
                                                    <span>سنوات تدریس</span>

                                                    <asp:RangeValidator ID="RangeValidator3" ErrorMessage="سنوات تدریس باید بین 0 تا 99 سال باشد"
                                                        ControlToValidate="txtSanavat" Type="Integer" MinimumValue="0" MaximumValue="99" Text="*" ForeColor="Red" Display="Dynamic" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="inf" ID="RequiredFieldValidator13" ControlToValidate="txtSanavat"
                                                        ForeColor="Red" Text="*" runat="server" ErrorMessage="لطفا سنوات تدریس خود را درج فرمایید"></asp:RequiredFieldValidator>
                                                </p>
                                                <br />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtSanavat" runat="server" OnTextChanged="Control_StateChanged" AutoPostBack="true" CssClass="form-control " MaxLength="2" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lbl_Sal" runat="server" Text="سال"></asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label27" Text="شماره حساب سیبا" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ErrorMessage="شماره حساب باید 13 رقم باشد" ControlToValidate="txtSiba" ValidationExpression="^[0-9]{13}$" runat="server" Text="*" ForeColor="Red" ValidationGroup="info" />
                                            <asp:RequiredFieldValidator ID="rfvSiba" ControlToValidate="txtSiba" runat="server" ValidationGroup="inf" ForeColor="Red" Text="*" ErrorMessage="وجود شماره سیبا الزامی است."></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtSiba" OnTextChanged="Control_StateChanged" runat="server" CssClass="form-control" Text="0" MaxLength="13" />

                                            <strong dir="rtl">توجه: فقط شماره حساب سیبا بانک ملی و به نام استاد مورد قبول است. با توجه به اینکه پرداخت حق الزحمه به این شماره حساب صورت می گیرد، لذا خواهشمند است در ثبت آن دقت فرمایید.</strong>
                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-3">

                            <div class="row">
                                <div class="col-md-10">
                                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-7">
                                                    <asp:CheckBox ID="chbkIsRetired" OnCheckedChanged="chk_IsRetired_CheckedChanged" runat="server" AutoPostBack="true" Text="بازنشسته" CssClass="" />
                                                </div>
                                            </div>
                                            <div class="row" id="dvBazneshaste" runat="server" visible="false">
                                                <div class="col-md-7">
                                                    <p class="lblInput" id="P1" runat="server">حکم بازنشستگی:<span class="text-danger" id="Span1" runat="server"></span></p>
                                                    <div>
                                                        
                                                 <telerik:RadAsyncUpload
                                                        ID="ruScanBazneshaste" runat="server" Width="100%" OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvBazneshaste"
                                                        ClientValidationFunction="validateAsyncUploadBzn" ValidationGroup="inf" ErrorMessage="لطفا اسکن حکم بازنشستگی خود را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                
                                                    </div>
                                                    <code>حداکثر تا حجم 700 کیلو بایت</code>
                                                </div>
                                            </div>
                                            <asp:Label Text="وضعیت بیمه" runat="server" />
                                            <asp:RequiredFieldValidator ErrorMessage="لطفا وضعیت بیمه بودن یا نبودن را مشخص فرمایید" Display="None" ForeColor="Red" ValidationGroup="vg" Text="*" ControlToValidate="rdblBimehStatus" runat="server" />
                                            <asp:RadioButtonList ID="rdblBimehStatus" OnSelectedIndexChanged="rdblBimehStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="" runat="server">
                                                <asp:ListItem Text="دارای سابقه بیمه" Value="1" />
                                                <asp:ListItem Text="بدون سابقه بیمه" Value="2" Selected="True" />
                                            </asp:RadioButtonList>
                                            <asp:Label Text="نوع بیمه" runat="server" />
                                            <asp:RequiredFieldValidator ID="valBimehType" ErrorMessage="لطفا نوع بیمه را مشخص فرمایید" InitialValue="0" ControlToValidate="drpBimehType" ForeColor="Red" Display="Dynamic" ValidationGroup="vg" Text="*" runat="server" />
                                            <asp:DropDownList ID="drpBimehType" Enabled="false" OnSelectedIndexChanged="Control_StateChanged" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                <asp:ListItem Text="مشمول بیمه" Value="1" />
                                                <asp:ListItem Text="لشکری" Value="2" />
                                                <asp:ListItem Text="کشوری" Value="3" />
                                                <asp:ListItem Text="خدمات درمانی" Value="4" />
                                                <asp:ListItem Text="سلامت" Value="5" />
                                                <asp:ListItem Text="تامین اجتماعی" Value="6" />
                                                <asp:ListItem Text="بازنشسته" Value="7" />
                                                <asp:ListItem Text="سایر موارد" Value="8" />
                                                <%--<asp:ListItem Text="انتخاب کنید" Value="0" />
                                                <asp:ListItem Text="تامین اجتماعی" Value="1" />
                                                <asp:ListItem Text="خدمات درمانی" Value="2" />
                                                <asp:ListItem Text="نیروهای مسلح" Value="3" />
                                                <asp:ListItem Text="بازنشسته" Value="4" />--%>
                                            </asp:DropDownList>
                                            <asp:Label ID="Label26" Text="شماره بیمه" runat="server" />
                                            <asp:RegularExpressionValidator ErrorMessage="شماره بیمه وارد شده باید 10 رقمی باشد" ControlToValidate="txtInsuranceNumber" ValidationExpression="^[0-9]{10}$" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtInsuranceNumber" ErrorMessage="لطفاشماره بیمه را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                            <asp:TextBox ID="txtInsuranceNumber" OnTextChanged="Control_StateChanged" Enabled="false" runat="server" CssClass="form-control" EnableViewState="True" MaxLength="10" />

                                            <div class="row"  id="dvBime" runat="server" visible="false">
                                                <div class="col-md-7">
                                                    <p class="lblInput" id="lbl_P_BimeRetired" runat="server">اسکن بیمه:<span class="text-danger" id="lbl_S_BimeRetired" runat="server">*</span></p>
                                                    <div>
                                                 <telerik:RadAsyncUpload
                                                        ID="ruScanBime" runat="server" Width="100%" OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvBime"
                                                        ClientValidationFunction="validateAsyncUploadBim" ValidationGroup="inf" ErrorMessage="لطفا اسکن بیمه خود را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                
                                                    </div>
                                                    <code>حداکثر تا حجم 700 کیلو بایت</code>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    <div class="row" id="dvPersonelly" runat="server" visible="true">
                                            <div class="col-md-7">
                                                <p class="lblInput">اسکن عکس پرسنلی:<span class="text-danger"></span></p>
                                                <asp:Label runat="server" CssClass="lblInput" ID="lbloptionalPersonelly" Visible="true" Text="اختیاری"></asp:Label>

                                                <div>
                                                    <telerik:RadAsyncUpload
                                                        ID="ruScanPersonelly" runat="server" Width="100%"  OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب"  PostbackTriggers="btnSubmitChanges" LocalizationPath="مسیر">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvPersonelly" Enabled="false"
                                                        ClientValidationFunction="validateAsyncUploadPrsn" ValidationGroup="inf" ErrorMessage="لطفا اسکن عکس پرسنلی را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                    
                                                </div>
                                                <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            </div>
                                        </div>
                                    <div class="row" id="dvMelli" runat="server" visible="true">
                                            <div class="col-md-7">
                                                <p class="lblInput">اسکن کارت ملی:<span class="text-danger"></span></p>
                                                <asp:Label runat="server" CssClass="lblInput" ID="lblOptionalMelli" Visible="true" Text="اختیاری"></asp:Label>

                                                <div>
                                                    <telerik:RadAsyncUpload
                                                        ID="ruScanMelli" runat="server" Width="100%"  OnClientValidationFailed="OnClientValidationFailed"
                                                        MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="716800" Localization-Cancel="لغو"
                                                        Localization-Remove="حذف" Localization-Select="انتخاب"  PostbackTriggers="btnSubmitChanges" LocalizationPath="مسیر">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:CustomValidator runat="server" ID="cvMelli" Enabled="false"
                                                        ClientValidationFunction="validateAsyncUploadMli" ValidationGroup="inf" ErrorMessage="لطفا اسکن کارت ملی را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>
                                                    
                                                </div>
                                                <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            </div>
                                        </div>
                                    <div class="text-center">
                                        <br />
                                        <br />
                                        <asp:Button ID="btnSubmitChanges" ValidationGroup="inf" OnClick="btnSubmitChanges_Click" Text="ثبت تغییرات" runat="server" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnCancel" Text="انصراف" OnClick="btnCancel_Click" CssClass="btn btn-warning" runat="server" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:ValidationSummary runat="server" ValidationGroup="inf" ForeColor="Red" HeaderText="لطفا خطاهای زیر را برطرف فرمایید:" />
                    </div>
                </div>

                <br />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfNezam" runat="server" Value="0" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

    <script type="text/javascript">
        function validateAsyncUploadSH(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanShenasname.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadMdr(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanMadrak.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadArz(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanArzeshname.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadNzm(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanNezam.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadBzn(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanBazneshaste.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadBim(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanBime.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadPsnl(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanPersonelly.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };
        function validateAsyncUploadMli(sender, args) {
            debugger;
            var cntr = $find("<%=ruScanMelli.ClientID%>");
            args.IsValid = cntr._uploadedFiles.length != 0;
        };

        function onFileUploaded(sender, args) {
            <%--debugger;
            var CustomValidator = document.getElementById("<%=cvShenasname.ClientID %>");
            ValidatorValidate(CustomValidator);--%>
        }

    </script>

    <script type="text/javascript">
        function CantRegModal() {
            $('#cantReg_modal').modal('show');
        }
        function uploadDocument() {
            $('#mdlUploadDocument').modal('show');
        }
    </script>

    <script>
        function OnClientValidationFailed(sender, args) {
            debugger;
            var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
            if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                    radalert("فرمت فایل انتخابی صحیح نمی باشد!", 0, 100, "هشدار", "");
                }
                else {
                    radalert("حجم فایل انتخابی بیش از اندازه مجاز می باشد!", 0, 100, "هشدار", "");
                }
            }
            else {
                radalert("فرمت صحیح نمی باشد!", 0, 100, "هشدار", "");
            }
        }


        function RedirectToMain() {
            window.location = "EditMain.aspx";
        }
    </script>

</asp:Content>
