<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" CodeBehind="editTeachersInfos.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.editTeachersInfos" %>

<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openModalConfirm() {
            $('#modalConfirm').modal('show');
        }
        function closeModalConfirm() {
            $('#modalConfirm').modal('hide');
        } function openModalMsg() {
            $('#modalMsg').modal('show');
        }
        function closeModalMsg() {
            $('#modalMsg').modal('hide');
        }
    </script>
    <style type="text/css">
        .form-control {
            width: 80% !important;
        }
    </style>
    <div class="modal fade" id="modalConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="confirmMessage" runat="server" Text="آیا از آپلود عکس برای استاد اطمینان دارید؟" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="rbConfirmeUploadImage" runat="server" OnClick="rbConfirmeUploadImage_Click" Text="بله">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="rbConfirmUploadImage" runat="server" OnClientClicked="closeModalConfirm" Text="خیر">
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalMsg" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
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
                                    <asp:Literal ID="ltrMsg" runat="server" Text="" Mode="PassThrough" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <telerik:RadButton ID="closeMsg" runat="server" OnClientClicked="closeModalMsg" Text="بستن پنجره">
                    </telerik:RadButton>

                </div>
            </div>
        </div>
    </div>


    <div class="container" dir="rtl">
        <div class="row">
            <div class="col-md-2">
                <asp:DropDownList ID="ddlEditType" runat="server" Width="100%">
                    <asp:ListItem Value="1" Text="درج عکس پرسنلی" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="2" Text="ویرایش اطلاعات پرسنلی"></asp:ListItem>
                    <asp:ListItem Value="3" Text="ویرایش اطلاعات تماس"></asp:ListItem>
                    <asp:ListItem Value="4" Text="درج حکم کارگزینی"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button runat="server" CssClass="btn btn-default" Text="مشاهده" ID="btnShow" OnClick="btnShow_Click" />
            </div>
        </div>
        <div class="row" dir="rtl" style="padding: 20px">
            <%--درج عکس پرسنلی--%>
            <div class="panel panel-success" id="pnlPersonalImage" runat="server" visible="false">
                <div class="panel panel-heading">
                    <span>لیست اساتید بدون عکس پرسنلی</span>
                </div>
                <div class="panel panel-body" style="margin: 10px">
                    <div class="row" style="background-color: #dbfce0;">
                        <div class="col-md-6" style="margin: 10px">
                            <div class="row">
                                <div class="col-md-3">
                                    <span>کد ملی:</span>

                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtIDDMeli" runat="server" ToolTip="کد ملی استاد"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnSearchTeacher_HaveNotPersonalImage" Width="80%" runat="server" OnClick="btnSearchTeacher_HaveNotPersonalImage_Click" Text="جستجو" CssClass="btn btn-success" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" id="dvRadUpload" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblTeacher_PersonalImage" runat="server"></asp:Label>

                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadAsyncUpload ID="radUploadPersonalImage" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="71680" PostbackTriggers="rbConfirmeUploadImage">
                                                <Localization Cancel="انصراف" Remove="حذف" Select="انتخاب" />

                                            </telerik:RadAsyncUpload>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnUploadPersonalImage" Width="80%" CssClass="btn btn-success" Text="آپلود" runat="server" OnClick="btnUploadPersonalImage_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 center">
                                            <code>حداکثر تا حجم 70 کیلو بایت</code>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <asp:Image ID="imgPersonalImage" runat="server" Width="90%" />
                        </div>
                    </div>
                    <hr />
                    <div class="row" dir="rtl">
                        <div class="container">
                            <div class="row">
                                <div class="col-md-6">
                                    <span><font color="green">  اساتید با قابلیت درج عکس  </font></span>
                                    <asp:ImageButton ID="btnExportExcel_grdPersonalImage" runat="server" ImageUrl="~/CommonUI/images/Excel02.jpg" CommandArgument="grdPersonalImage" OnClick="btnExportExcel_Click" />
                                    <telerik:RadGrid ID="grdPersonalImage" runat="server" AutoGenerateColumns="false" EmptyDataText="رکوردی برای این جدول وجود ندارد" AllowFilteringByColumn="True" OnNeedDataSource="grdPersonalImage_NeedDataSource"
                                        PageSize="100" AllowPaging="True" OnExcelMLWorkBookCreated="grid_ExcelMLWorkBookCreated">
                                        <MasterTableView Dir="RTL" BackColor="LightGreen" AlternatingItemStyle-BackColor="LightGreen">
                                            <HeaderStyle BackColor="DarkGreen" ForeColor="DarkGreen" />
                                            <Columns>

                                                <telerik:GridBoundColumn DataField="hrID" Visible="false"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="کد ملی" DataField="iddMeli" AllowFiltering="true"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="نام" DataField="name" AllowFiltering="true"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="نام خانوادگی" DataField="family" AllowFiltering="true"></telerik:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div class="col-md-6">
                                    <span><font color="red">اساتید فاقد عکس و بدون امکان آپلود</font></span>
                                    <asp:ImageButton ID="btnExportExcel_grdCantUploadPErsonnelyImage" runat="server" ImageUrl="~/CommonUI/images/Excel02.jpg" CommandArgument="grdCantUploadPErsonnelyImage" OnClick="btnExportExcel_Click" />

                                    <telerik:RadGrid ID="grdCantUploadPErsonnelyImage" runat="server" AutoGenerateColumns="false" EmptyDataText="رکوردی برای این جدول وجود ندارد" AllowFilteringByColumn="True" OnNeedDataSource="grdCantUploadPErsonnelyImage_NeedDataSource"
                                        PageSize="100" AllowPaging="True" OnExcelMLWorkBookCreated="grid_ExcelMLWorkBookCreated">
                                        <MasterTableView Dir="RTL" BackColor="#ff9f9f" AlternatingItemStyle-BackColor="#ff9f9f">
                                            <HeaderStyle BackColor="Red" ForeColor="DarkRed" />

                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="کد استادی" DataField="codeOstad" AllowFiltering="true" />
                                                <telerik:GridBoundColumn HeaderText="کد ملی" DataField="idd_Meli" AllowFiltering="true" />
                                                <telerik:GridBoundColumn HeaderText="نام" DataField="name" AllowFiltering="true" />
                                                <telerik:GridBoundColumn HeaderText="نام خانوادگی" DataField="family" AllowFiltering="true" />

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--ویرایش اطلاعات فردی--%>
            <div class="panel panel-success" id="pnlEditPersonalInfo" runat="server" visible="false">
                <div class="panel panel-heading">
                    <span>ویرایش اطلاعات فردی استاد</span>
                    <hr />
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <span>کد استادی:</span>
                            <asp:TextBox ID="txtSearchProfessorCode_PersonalInfo" runat="server" ToolTip="کد استادی"></asp:TextBox>
                        </div>
                        <div class="col-md-6 text-right">
                            <asp:Button ID="btnSearchTeacher_PersonalInfo" OnClick="btnSearchTeacher_PersonalInfo_Click" Width="30%" runat="server" Text="جستجو" CssClass="btn btn-success" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 text-center info">
                            <span>استاد: </span>
                            <asp:Label ID="txtProfessorName_PersonalInfo" runat="server"></asp:Label>
                            <span>---</span>
                            <asp:Label ID="txtProfessorCode_PersonalInfo" runat="server"></asp:Label>
                        </div>

                    </div>
                </div>
                <div class="panel-body ">

                    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-md-3">
                                    <div style="padding-right: 30px;">

                                        <div class="row">
                                            <div class="col-md-10">
                                                <p class="lblInput">نام</p>
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <p class="lblInput">نام خانوادگی</p>
                                                <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-md-10">
                                                <p class="lblInput">نام پدر</p>
                                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <p class="lblInput">شماره شناسنامه</p>
                                                <asp:TextBox ID="txtShCode" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10">
                                                <p class="lblInput">سال تولد</p>
                                                <asp:TextBox ID="txtYearBorn" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>آخرین مدرک تحصیلی</span>
                                                <asp:DropDownList ID="drpLastMaghta" runat="server" CssClass="form-control form-inline dropdown" AutoPostBack="true" OnSelectedIndexChanged="drpLastMaghta_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>رشته تحصیلی</span>
                                            </p>
                                            <div>
                                                <telerik:RadComboBox ID="drpReshte" runat="server"
                                                    MarkFirstMatch="True"
                                                    Filter="Contains"
                                                    HighlightTemplatedItems="True"
                                                    RenderMode="Lightweight" Width="80%"
                                                    AllowCustomText="false"
                                                    ExpandDirection="Down" Culture="(Default)" Height="300px">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>سال اخذ مدرک <code>چهار رقم</code></span>
                                                <asp:RangeValidator ID="RangeValidator1"
                                                    ErrorMessage="سال اخذ مدرک باید عددی بین 1300 تا 1400 باشد"
                                                    ControlToValidate="txtYearGetMadrak" runat="server"
                                                    Text="*" ForeColor="Red" MaximumValue="1400" MinimumValue="1300" Type="Integer" />
                                            </p>
                                            <asp:TextBox runat="server" ID="txtYearGetMadrak" ToolTip="چهار رقم" CssClass="form-control" MaxLength="4" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>کشور محل اخذ مدرک</span>
                                            </p>
                                            <asp:DropDownList ID="drpCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>نوع دانشگاه اخذ مدرک</span>
                                            </p>
                                            <asp:DropDownList ID="drpUniversityType" runat="server" CssClass="form-control">
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
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>نام دانشگاه محل اخذ آخرین مدرک تحصیلی</span>
                                            </p>
                                            <div>
                                                <telerik:RadComboBox ID="drpUniName"
                                                    runat="server"
                                                    MarkFirstMatch="True"
                                                    Filter="Contains" HighlightTemplatedItems="True"
                                                    RenderMode="Lightweight" Width="80%" AllowCustomText="false"
                                                    ExpandDirection="Down" Height="300px">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <p>
                                                <span>جنسیت</span>
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
                                            <div class="col-md-10">

                                                <p class="lblInput">
                                                    <span>وضعیت نظام وظیفه</span>
                                                </p>
                                                <asp:DropDownList ID="drpNezam" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <div class="row">
                                        <div class="col-md-10">
                                            <p class="lblInput">
                                                <span>وضعیت تاهل</span>
                                                <asp:RadioButtonList ID="rdblMarriage" runat="server" RepeatDirection="Horizontal" CssClass="">
                                                    <asp:ListItem Text="مجرد" Value="1" />
                                                    <asp:ListItem Text="متاهل" Value="2" />
                                                </asp:RadioButtonList>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <p class="lblInput">
                                                    <span>سنوات تدریس</span>

                                                    <asp:RangeValidator ID="RangeValidator3" ErrorMessage="سنوات تدریس باید بین 0 تا 99 سال باشد"
                                                        ControlToValidate="txtSanavat" Type="Integer" MinimumValue="0" MaximumValue="99" Text="*" ForeColor="Red" Display="Dynamic" runat="server" />

                                                </p>
                                                <br />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtSanavat" runat="server" CssClass="form-control " MaxLength="2" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label ID="lbl_Sal" runat="server" Text="سال"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <asp:Label ID="Label27" Text="شماره حساب سیبا" runat="server" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ErrorMessage="شماره حساب باید 13 رقم باشد" ControlToValidate="txtSiba" ValidationExpression="^[0-9]{13}$" runat="server" Text="*" ForeColor="Red" ValidationGroup="info" />
                                            <asp:TextBox ID="txtSiba" runat="server" CssClass="form-control" Text="0" MaxLength="13" />

                                            <strong dir="rtl">توجه: فقط شماره حساب سیبا بانک ملی و به نام استاد مورد قبول است. با توجه به اینکه پرداخت حق الزحمه به این شماره حساب صورت می گیرد، لذا خواهشمند است در ثبت آن دقت فرمایید.</strong>
                                        </div>

                                    </div>


                                </div>
                                <div class="col-md-3">

                                    <div class="row">
                                        <div class="col-md-10">

                                            <div class="row">
                                                <div class="col-sm-7">
                                                    <asp:CheckBox ID="chbkIsRetired" OnCheckedChanged="chbkIsRetired_CheckedChanged" runat="server" AutoPostBack="true" Text="بازنشسته" CssClass="" />
                                                </div>
                                            </div>

                                            <asp:Label Text="وضعیت بیمه" runat="server" />
                                            <asp:RadioButtonList ID="rdblBimehStatus" OnSelectedIndexChanged="rdblBimehStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="" runat="server">
                                                <asp:ListItem Text="دارای سابقه بیمه" Value="1" />
                                                <asp:ListItem Text="بدون سابقه بیمه" Value="2" Selected="True" />
                                            </asp:RadioButtonList>
                                            <asp:Label Text="نوع بیمه" runat="server" />
                                            <asp:DropDownList ID="drpBimehType" Enabled="false" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                <asp:ListItem Text="مشمول بیمه" Value="1" />
                                                <asp:ListItem Text="لشکری" Value="2" />
                                                <asp:ListItem Text="کشوری" Value="3" />
                                                <asp:ListItem Text="خدمات درمانی" Value="4" />
                                                <asp:ListItem Text="سلامت" Value="5" />
                                                <asp:ListItem Text="تامین اجتماعی" Value="6" />
                                                <asp:ListItem Text="بازنشسته" Value="7" />
                                                <asp:ListItem Text="سایر موارد" Value="8" />
                                            </asp:DropDownList>
                                            <asp:Label ID="Label26" Text="شماره بیمه" runat="server" />
                                            <asp:RegularExpressionValidator ErrorMessage="شماره بیمه وارد شده باید 10 رقمی باشد" ControlToValidate="txtInsuranceNumber" ValidationExpression="^[0-9]{10}$" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />

                                            <asp:TextBox ID="txtInsuranceNumber" Enabled="false" runat="server" CssClass="form-control" EnableViewState="True" MaxLength="10" />
                                            

                                            <hr />
                                            <asp:Label ID="lblDesciption" runat="server" Text="توضیحات"></asp:Label>
                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <%--اسکن مدارک فردی--%>
                            <div class="row text-center">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6" id="dvPersonelly">
                                            <span class="lblInput">اسکن عکس پرسنلی:</span>
                                            <code>حداکثر تا حجم 70 کیلو بایت</code>

                                            <div class="mid_center" style="padding-right: 50px">
                                                <telerik:RadAsyncUpload
                                                    ID="ruScanPersonelly" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="71680" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges" LocalizationPath="مسیر">
                                                </telerik:RadAsyncUpload>
                                                <asp:CustomValidator runat="server" ID="cvPersonelly" Enabled="false"
                                                    ClientValidationFunction="validateAsyncUploadPrsn" ValidationGroup="inf" ErrorMessage="لطفا اسکن عکس پرسنلی را آپلود فرمایید" Text="*" ForeColor="Red"></asp:CustomValidator>

                                            </div>
                                        </div>
                                        <div class="col-md-6" id="dvNezam" runat="server" visible="false">
                                            <span class="lblInput">اسکن نظام وظیفه:</span>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">

                                                <telerik:RadAsyncUpload
                                                    ID="ruScanNezam" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges_PersonalInfo">
                                                </telerik:RadAsyncUpload>

                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" id="dvShenasname">
                                            <span>اسکن شناسنامه:</span>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">
                                                <telerik:RadAsyncUpload Visible="true"
                                                    ID="ruScanShenasname" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="716800" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges_PersonalInfo" LocalizationPath="مسیر">
                                                </telerik:RadAsyncUpload>
                                            </div>

                                        </div>
                                        <div class="col-md-6" id="dvMelli">
                                            <span class="lblInput">اسکن کارت ملی:</span>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>

                                            <div class="mid_center " style="padding-right: 50px">
                                                <telerik:RadAsyncUpload
                                                    ID="ruScanMelli" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg" MaxFileSize="716800" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges_PersonalInfo" LocalizationPath="مسیر">
                                                </telerik:RadAsyncUpload>


                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" id="dvMadrak" runat="server" visible="true">
                                            <span class="lblInput">اسکن مدرک تحصیلی:</span>
                                            <code>حداکثر تا حجم 1024 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">
                                                <telerik:RadAsyncUpload
                                                    ID="ruScanMadrak" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="1048576" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب">
                                                </telerik:RadAsyncUpload>
                                            </div>
                                        </div>
                                        
                                        <div class="col-md-6" id="dvJame" runat="server" visible="false">
                                            <span class="lblInput">اسکن گواهی امتحان جامع:</span>
                                            <code>حداکثر تا حجم 1024 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">
                                                <telerik:RadAsyncUpload
                                                    ID="ruScanJame" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="1048576" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب">
                                                </telerik:RadAsyncUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-6" id="dvArzeshname" runat="server" visible="false">
                                            <span class="lblInput">اسکن ارزشنامه تحصیلی وزارت علوم:</span>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">
                                                <telerik:RadAsyncUpload
                                                    ID="ruScanArzeshname" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges_PersonalInfo">
                                                </telerik:RadAsyncUpload>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" id="dvBime" runat="server" visible="false">
                                            <span class="lblInput" id="lbl_P_BimeRetired" runat="server">اسکن بیمه:</span>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">
                                                <telerik:RadAsyncUpload
                                                    ID="ruScanBime" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges_PersonalInfo">
                                                </telerik:RadAsyncUpload>

                                            </div>
                                        </div>
                                        <div class="col-md-6" id="dvBazneshaste" runat="server" visible="false">
                                            <span class="lblInput" id="P1" runat="server">حکم بازنشستگی:</span>
                                            <code>حداکثر تا حجم 700 کیلو بایت</code>
                                            <div class="mid_center" style="padding-right: 50px">

                                                <telerik:RadAsyncUpload
                                                    ID="ruScanBazneshaste" runat="server"
                                                    MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800" Localization-Cancel="لغو"
                                                    Localization-Remove="حذف" Localization-Select="انتخاب" PostbackTriggers="btnSubmitChanges_PersonalInfo">
                                                </telerik:RadAsyncUpload>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <hr />
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <br />
                            <br />
                            <asp:Button ID="btnSubmitChanges_PersonalInfo" OnClick="btnSubmitChanges_PersonalInfo_Click" Text="ثبت تغییرات" runat="server" CssClass="btn btn-success" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:ValidationSummary runat="server" ValidationGroup="inf" ForeColor="Red" HeaderText="لطفا خطاهای زیر را برطرف فرمایید:" />
                    </div>
                </div>
            </div>
            <%--ویرایش اطلاعات تماس استاد--%>
            <div class="panel panel-success" id="pnlEditContactInfo" runat="server" visible="false">
                <div class="panel panel-heading">
                    <span>ویرایش اطلاعات تماس استاد</span>
                    <hr />
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <span>کد استادی:</span>
                            <asp:TextBox ID="txtSearchProfessorCode_ContactInfo" runat="server" ToolTip="کد استادی"></asp:TextBox>
                        </div>
                        <div class="col-md-6 text-right">
                            <asp:Button ID="btnSearchTeacher_ContactInfo" OnClick="btnSearchTeacher_ContactInfo_Click" Width="30%" runat="server" Text="جستجو" CssClass="btn btn-success" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center info">
                            <span>استاد: </span>
                            <asp:Label ID="txtProfessorName_ContactInfo" runat="server"></asp:Label>
                            <span>---</span>
                            <asp:Label ID="txtProfessorCode_ContactInfo" runat="server"></asp:Label>
                        </div>

                    </div>
                </div>
                <div class="panel-body " style="margin: 30px">
                    <div class="row">
                        <div class="col-md-3 ">
                            <asp:Label ID="Label29" Text="تلفن منزل" runat="server" />
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="0[1-8][1-9][2-8][\d]{7}" ErrorMessage="تلفن منزل نادرست است. باید به همراه کد شهر و 11 رقمی باشد" ControlToValidate="txtHomePhone"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا تلفن منزل را به همراه کد شهر وارد فرمایید" ControlToValidate="txtHomePhone"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtHomePhone" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                        </div>


                        <div class="col-md-3 ">
                            <asp:Label ID="Label31" Text="تلفن محل کار" runat="server" />
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="0[1-8][1-9][2-8][\d]{7}" ErrorMessage="تلفن محل کار نادرست است. باید به همراه کد شهر و 11 رقمی باشد" ControlToValidate="txtWorkPhone"></asp:RegularExpressionValidator>

                            <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="Label33" Text="تلفن همراه" runat="server" />
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="09[0-9]{9}" ErrorMessage="تلفن همراه نادرست است. باید به صورت عددی و 11 رقمی باشد" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا تلفن همراه را وارد فرمایید." ControlToValidate="txtMobileNumber"></asp:RequiredFieldValidator>

                            <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="Label34" Text="پست الکترونیک" runat="server" />
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="\w+([-_.]?\w+)?@\w+([-_.]?\w+)?\.\w+([-_.]?\w+)" ErrorMessage="آدرس پست الکترونیک نادرست وارد شده است." ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="وارد کردن پست الکترونیک الزامی است." ControlToValidate="txtEmail"></asp:RequiredFieldValidator>


                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-inline" MaxLength="40" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <h4>آدرس محل سکونت</h4>
                                </div>
                            </div>

                            <div class="row">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="col-md-3">
                                            <asp:Label ID="Label35" Text="استان" runat="server" />
                                            <asp:DropDownList ID="drpProvince_Home" runat="server" OnSelectedIndexChanged="drpProvince_Home_SelectedIndexChanged" CssClass=" form-control form-inline" AutoPostBack="true" Height="40px" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label36" Text="شهر" runat="server" />
                                            <asp:DropDownList ID="drpCity_Home" runat="server" CssClass="form-control form-inline" Height="40px" />
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="Label38" Text="کد پستی" runat="server" />
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ValidationExpression="[1-9][0-9]{9}" ErrorMessage="کد پستی نادرست است. باید به صورت عددی و 10 رقمی باشد." ControlToValidate="txtLivingZipCode"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ForeColor="Red" Text="*" ErrorMessage="لطفا کد پستی 10 رقمی را وارد فرمایید" ControlToValidate="txtLivingZipCode"></asp:RequiredFieldValidator>

                                    <asp:TextBox ID="txtLivingZipCode" runat="server" CssClass="form-control  form-inline" MaxLength="10" />
                                </div>
                                <div class="col-md-8">
                                    <asp:Label ID="Label37" Text="آدرس" runat="server" />
                                    <asp:TextBox ID="txtLivingAddress" runat="server" CssClass=" form-control  form-inline" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                    <div class="row">

                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <h4>آدرس محل کار</h4>
                                </div>
                            </div>
                            <div class="row">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label40" Text="استان" runat="server" />
                                            <asp:DropDownList ID="drpProvince_Work" runat="server" OnSelectedIndexChanged="drpProvince_Work_SelectedIndexChanged" CssClass=" form-control  form-inline" AutoPostBack="true" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label39" Text="شهر" runat="server" />
                                            <asp:DropDownList ID="drpCity_Work" runat="server" CssClass="form-control form-inline" />
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="row">
                                <asp:Label ID="Label41" Text="آدرس" runat="server" />
                                <asp:TextBox ID="txtWorkingAddress" runat="server" CssClass=" form-control form-inline" />

                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <br />
                            <br />
                            <asp:Button ID="btnSubmitChanges_ContactInfo" OnClick="btnSubmitChanges_ContactInfo_Click" Text="ثبت تغییرات" runat="server" CssClass="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>
            <%--ویرایش آخرین حکم کارگزینی--%>
            <div class="panel panel-success" id="pnlEditEmployInfo" runat="server" visible="false">
                <div class="panel panel-heading">
                    <span>درج حکم کارگزینی</span>
                    <hr />
                    <div class="row">
                        <div class="col-md-6 text-left">
                            <span>کد استادی:</span>
                            <asp:TextBox ID="txtSearchProfessorCode_EmployInfo" runat="server" ToolTip="کد استادی"></asp:TextBox>
                        </div>
                        <div class="col-md-6 text-right">
                            <asp:Button ID="btnSearchTeacher_EmployInfo" OnClick="btnSearchTeacher_EmployInfo_Click" Width="30%" runat="server" Text="جستجو" CssClass="btn btn-success" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center info">
                            <span>استاد: </span>
                            <asp:Label ID="txtProfessorName_EmployInfo" runat="server"></asp:Label>
                            <span>---</span>
                            <asp:Label ID="txtProfessorCode_EmployInfo" runat="server"></asp:Label>
                        </div>

                    </div>
                </div>
                <div class="panel-body ">
                    <div class="row">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p class="lblInput">آیا عضو هیات علمی می باشید؟</p>
                                            <asp:RadioButtonList runat="server" ID="rblIsHeiat" AutoPostBack="true" RepeatDirection="Horizontal"
                                                CssClass="radio isHeiat" OnSelectedIndexChanged="rblIsHeiat_SelectedIndexChanged1">
                                                <asp:ListItem Text="بله" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="خیر" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel runat="server" ID="pnlDetails" Enabled="false">
                                    <div class="col-md-4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">مرتبه دانشگاهی</p>
                                                <asp:RequiredFieldValidator ID="rfvMartabe" ValidationGroup="bbb" ErrorMessage="لطفا مرتبه علمی خود را انتخاب کنید" InitialValue="0" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="drpMartabe" runat="server" />
                                                <asp:DropDownList ID="drpMartabe" runat="server" CssClass="form-control" Height="40px" ValidationGroup="bbb">
                                                    <asp:ListItem Text="انتخاب کنید" Value="0" Selected="True" />
                                                    <asp:ListItem Text="مربی" Value="1" />
                                                    <asp:ListItem Text="دانشیار" Value="2" />
                                                    <asp:ListItem Text="استادیار" Value="3" />
                                                    <asp:ListItem Text="استاد" Value="4" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">پایه</p>
                                                <asp:RequiredFieldValidator ID="rfvPaye" ValidationGroup="bbb" ErrorMessage="لطفا پایه خود را درج کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtPaye" runat="server" />
                                                <asp:RangeValidator ID="RangeValidator4" ValidationGroup="bbb" ErrorMessage="مقدار پایه باید عددی بین 0 و 50 باشد" Type="Integer" MinimumValue="0" MaximumValue="50" ControlToValidate="txtPaye" Display="Dynamic" ForeColor="Red" Text="*" runat="server" />
                                                <asp:TextBox ID="txtPaye" runat="server" ValidationGroup="bbb" CssClass="form-control" MaxLength="2" Text="0" />

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">نوع استخدام</p>
                                                <asp:RequiredFieldValidator ValidationGroup="bbb" ID="rfvHireType" ErrorMessage="لطفا نوع استخدام خود را انتخاب کنید" InitialValue="-1" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="drpHireType" runat="server" />
                                                <asp:DropDownList ID="drpHireType" ValidationGroup="bbb" runat="server" CssClass="form-control" Height="40px">
                                                    <asp:ListItem Text="انتخاب کنید" Value="-1" />
                                                    <asp:ListItem Text="رسمی" Value="1" />
                                                    <asp:ListItem Text="آزمایشی" Value="2" />
                                                    <asp:ListItem Text="قراردادی" Value="3" />
                                                    <asp:ListItem Text="مامور به خدمت" Value="4" />
                                                    <asp:ListItem Text="موقت" Value="5" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">دانشگاه محل خدمت</p>
                                                <asp:RequiredFieldValidator ValidationGroup="bbb" ID="valUniName" Display="Dynamic" ErrorMessage="لطفا نام دانشگاه محل خدمت خود را انتخاب کنید" ControlToValidate="drpPastUni" runat="server" InitialValue="جستجو و انتخاب کنید" Text="*" ForeColor="Red" />
                                                <telerik:RadComboBox ID="drpPastUni" runat="server"
                                                    MarkFirstMatch="True"
                                                    Filter="Contains" HighlightTemplatedItems="True"
                                                    RenderMode="Lightweight" Width="80%" AllowCustomText="false"
                                                    ExpandDirection="Down" Height="300px">
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">نوع دانشگاه محل خدمت</p>
                                                <asp:RequiredFieldValidator ValidationGroup="bbb" ID="rfvPastUniType" Display="Dynamic" ErrorMessage="لطفا نوع دانشگاه محل خدمت خود را انتخاب کنید" ControlToValidate="ddlPastUniType" runat="server" InitialValue="0" Text="*" ForeColor="Red" />
                                                <div>
                                                    <asp:DropDownList ID="ddlPastUniType" ValidationGroup="bbb" runat="server" CssClass="form-control" Height="40px">
                                                        <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                        <asp:ListItem Text="دولتی" Value="1" />
                                                        <asp:ListItem Text="آزاد" Value="2" />
                                                        <asp:ListItem Text="حوزه" Value="3" />
                                                        <asp:ListItem Text="خارج از کشور" Value="4" />
                                                        <asp:ListItem Text="سایر" Value="5" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">تاریخ صدور حکم کارگزینی</p>
                                                <asp:RequiredFieldValidator ID="rfvDateSodoorHokm" ValidationGroup="bbb" ErrorMessage="درج تاریخ صدور حکم الزامی می باشد" ControlToValidate="txtDateSodoorHokm" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                                                <asp:RegularExpressionValidator ID="revDateSodoorHokm" ValidationGroup="bbb" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateSodoorHokm" runat="server" />
                                                <div class="pcalWrapper">
                                                    <asp:TextBox ID="txtDateSodoorHokm" runat="server" ValidationGroup="bbb" CssClass="form-control form-inline pcal" MaxLength="10" />
                                                </div>
                                            </div>
                                        </div>
                                        <%--  --%>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">تاریخ اجرای حکم</p>
                                                <asp:RequiredFieldValidator ID="rfvDateEjraHokm" ValidationGroup="bbb" ErrorMessage="درج تاریخ اجرای حکم الزامی می باشد" ControlToValidate="txtDateEjraHokm" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                                                <asp:RegularExpressionValidator ID="revDateEjraHokm" ValidationGroup="bbb" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateEjraHokm" runat="server" />
                                                <div class="pcalWrapper">
                                                    <asp:TextBox ID="txtDateEjraHokm" runat="server" ValidationGroup="bbb" CssClass="form-control form-inline pcal" MaxLength="10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">تاریخ اجرای حکم در این واحد</p>
                                                <asp:RequiredFieldValidator ID="rfvDateEjraHokmHere" ValidationGroup="bbb" ErrorMessage="درج تاریخ اجرای حکم الزامی می باشد" ControlToValidate="txtDateEjraHokm" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="bbb" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateEjraHokm" runat="server" />
                                                <div class="pcalWrapper">
                                                    <asp:TextBox ID="txtDateEjraHokmHere" runat="server" ValidationGroup="bbb" CssClass="form-control form-inline pcal" MaxLength="10" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <p class="lblInput">شماره حکم</p>
                                                <asp:RequiredFieldValidator ValidationGroup="bbb" ID="RequiredFieldValidator6" ErrorMessage="لطفا شماره حکم خود را وارد کنید" ControlToValidate="txtHokmNumber" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                                                <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" MaxLength="15" ValidationGroup="bbb" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="row">

                                            <div class="col-md-12">
                                                <p class="lblInput">مبلغ حکم</p>
                                                <div class="currencyWrapper">
                                                    <asp:TextBox ID="txtMablaghHokm" runat="server" ToolTip="ریال" CssClass="form-control form-inline " ValidationGroup="bbb" />
                                                    <span>ریال</span>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <p class="lblInput">نحوه همکاری در دانشگاه مبدا</p>
                                                <asp:RequiredFieldValidator ID="rfvHireType2" ValidationGroup="bbb" ErrorMessage="لطفا نحوه همکاری خود در دانشگاه مبدا را مشخص کنید" ControlToValidate="rdblHireType" runat="server" Text="*" ForeColor="Red" Display="Dynamic" />
                                                <asp:RadioButtonList ID="rdblHireType" runat="server" ValidationGroup="bbb" CssClass="radio" RepeatColumns="3" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="تمام وقت 32 ساعت" Value="1" />
                                                    <asp:ListItem Text="نیمه وقت" Value="2" />
                                                    <asp:ListItem Text="ساعتی" Value="3" />
                                                    <asp:ListItem Text="تمام وقت 44 ساعت" Value="7" />
                                                    <asp:ListItem Text="کارمند" Value="6" />
                                                    <asp:ListItem Text="بورسیه دکتری" Value="5" />
                                                    <asp:ListItem Text="تمام وقت طرح مشمولان" Value="4" />
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:CheckBox runat="server" ID="chkBoundHour" Text="متقاضی تکمیل ساعت موظفی در واحد الکترونیکی هستم" />
                                            </div>
                                            <div class="col-md-12">
                                                <p class="lblInput">لطفا آخرین حکم کارگزینی بارگذاری گردد.</p>
                                                <%--<code>در صورتی که تصویر حکم بارگزاری نگردد، آخرین تصویر حکم  موجود برای این حکم کارگزینی بارگذاری خواهد شد</code>--%>
                                                <%--<asp:CustomValidator ValidationGroup="bbb" ID="vldEmployActionScan" ErrorMessage="قراردادن عکس حکم الزامی می باشد" Display="Dynamic" Text="*" ForeColor="Red" OnServerValidate="vldEmployActionScan_ServerValidate" ClientValidationFunction="validateUpload1" runat="server" />--%>
                                                <div>
                                                    <telerik:RadAsyncUpload ID="ruScanHokm" runat="server"
                                                        Width="100%" MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,pdf" MaxFileSize="716800"
                                                        Localization-Cancel="لغو" Localization-Remove="حذف" Localization-Select="انتخاب" ValidationGroup="bbb" PostbackTriggers="btnSubmitChanges_EmployInfo">
                                                    </telerik:RadAsyncUpload>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-sm-4 col-sm-pull-5">
                            <asp:ValidationSummary runat="server" DisplayMode="BulletList" ForeColor="Red" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <br />
                            <br />
                            <asp:Button ID="btnSubmitChanges_EmployInfo" OnClick="btnSubmitChanges_EmployInfo_Click" Text="ثبت تغییرات" runat="server" CssClass="btn btn-success" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
