<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="employmentAction.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.teacherForms.employmentAction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div>
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-12">
                        <p class="lblInput">آیا عضو هیات علمی می باشید؟</p>
                        <asp:RadioButtonList runat="server" ID="rblIsHeiat" AutoPostBack="true" RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblIsHeiat_SelectedIndexChanged" CssClass="radio isHeiat">
                            <asp:ListItem Text="بله" Value="1"></asp:ListItem>
                            <asp:ListItem Text="خیر" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnlDetails" Enabled="false">
                <div class="col-sm-4">
                    <div class="row">
                        <div class="col-sm-10">
                            <p class="lblInput">مرتبه دانشگاهی</p>
                            <asp:RequiredFieldValidator ID="rfvMartabe" ErrorMessage="لطفا مرتبه علمی خود را انتخاب کنید" InitialValue="0" Text="*" ForeColor="Red" Display="Dynamic" ControlToValidate="drpMartabe" ValidationGroup="bbb" runat="server" />
                            <asp:DropDownList ID="drpMartabe" runat="server" ValidationGroup="bbb" CssClass="form-control" Height="40px">
                                <asp:ListItem Text="انتخاب کنید" Value="0" Selected="True" />
                                <asp:ListItem Text="مربی" Value="1" />
                                <asp:ListItem Text="دانشیار" Value="2" />
                                <asp:ListItem Text="استادیار" Value="3" />
                                <asp:ListItem Text="استاد" Value="4" />
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" ID="hdnMartabe" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-5">
                            <p class="lblInput">پایه</p>
                            <asp:RequiredFieldValidator ID="rfvPaye" ValidationGroup="bbb" ErrorMessage="لطفا پایه خود را درج کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="txtPaye" runat="server" />
                            <asp:RangeValidator ID="RangeValidator4" ValidationGroup="bbb" ErrorMessage="مقدار پایه باید عددی بین 0 و 50 باشد" Type="Integer" MinimumValue="0" MaximumValue="50" ControlToValidate="txtPaye" Display="Dynamic" ForeColor="Red" Text="*" runat="server" />
                            <asp:TextBox ID="txtPaye" runat="server" ValidationGroup="bbb" CssClass="form-control" MaxLength="2" Text="0" />
                            <asp:HiddenField runat="server" ID="hdnPaye" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10">
                            <p class="lblInput">نوع استخدام</p>
                            <asp:RequiredFieldValidator ValidationGroup="bbb" ID="rfvHireType" ErrorMessage="لطفا نوع استخدام خود را انتخاب کنید" InitialValue="-1" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="drpHireType" runat="server" />
                            <asp:DropDownList ID="drpHireType" ValidationGroup="bbb" runat="server" CssClass="form-control" Height="40px">
                                <asp:ListItem Text="انتخاب کنید" Value="-1" />
                                <%--<asp:ListItem Text="رسمی" Value="0" />
                                                        <asp:ListItem Text="پیمانی" Value="1" />
                                                        <asp:ListItem Text="قراردادی" Value="2" />
                                                        <asp:ListItem Text="آزمایشی" Value="3" />--%>
                                <asp:ListItem Text="رسمی" Value="1" />
                                <asp:ListItem Text="آزمایشی" Value="2" />
                                <asp:ListItem Text="قراردادی" Value="3" />
                                <asp:ListItem Text="مامور به خدمت" Value="4" />
                                <asp:ListItem Text="موقت" Value="5" />
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" ID="hdnHireType" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10">
                            <p class="lblInput">دانشگاه محل خدمت</p>
                            <asp:RequiredFieldValidator ValidationGroup="bbb" ID="valUniName" Display="Dynamic" ErrorMessage="لطفا نام دانشگاه محل خدمت خود را انتخاب کنید" ControlToValidate="drpPastUni" runat="server" InitialValue="جستجو و انتخاب کنید" Text="*" ForeColor="Red" />
                            <div>
                                <telerik:RadComboBox ID="drpPastUni" runat="server" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" ValidationGroup="bbb" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Height="300px"></telerik:RadComboBox>
                            </div>
                            <asp:HiddenField runat="server" ID="hdnPastUni" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-10">
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
                                <asp:HiddenField runat="server" ID="hdnPastUniType" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="row">
                        <div class="col-sm-7">
                            <p class="lblInput">تاریخ صدور حکم کارگزینی</p>
                            <asp:RequiredFieldValidator ID="rfvDateSodoorHokm" ValidationGroup="bbb" ErrorMessage="درج تاریخ صدور حکم الزامی می باشد" ControlToValidate="txtDateSodoorHokm" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                            <asp:RegularExpressionValidator ID="revDateSodoorHokm" ValidationGroup="bbb" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateSodoorHokm" runat="server" />
                            <div class="pcalWrapper">
                                <asp:TextBox ID="txtDateSodoorHokm" runat="server" ValidationGroup="bbb" CssClass="form-control form-inline pcal" MaxLength="10" />
                            </div>
                            <asp:HiddenField runat="server" ID="hdnDateSodoorHokm" />
                        </div>
                    </div>
                    <%--  --%>
                    <div class="row">
                        <div class="col-sm-7">
                            <p class="lblInput">تاریخ اجرای حکم</p>
                            <asp:RequiredFieldValidator ID="rfvDateEjraHokm" ValidationGroup="bbb" ErrorMessage="درج تاریخ اجرای حکم الزامی می باشد" ControlToValidate="txtDateEjraHokm" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                            <asp:RegularExpressionValidator ID="revDateEjraHokm" ValidationGroup="bbb" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateEjraHokm" runat="server" />
                            <div class="pcalWrapper">
                                <asp:TextBox ID="txtDateEjraHokm" runat="server" ValidationGroup="bbb" CssClass="form-control form-inline pcal" MaxLength="10" />
                            </div>
                            <asp:HiddenField runat="server" ID="hdnDateEjraHokm" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-7">
                            <p class="lblInput">شماره حکم</p>
                            <asp:RequiredFieldValidator ValidationGroup="bbb" ID="RequiredFieldValidator6" ErrorMessage="لطفا شماره حکم خود را وارد کنید" ControlToValidate="txtHokmNumber" ForeColor="Red" Text="*" Display="Dynamic" runat="server" />
                            <%--<asp:RegularExpressionValidator ValidationGroup="bbb" ID="revHokmNumber" ControlToValidate="txtHokmNumber" runat="server" ValidationExpression="\d+" ErrorMessage="شماره حکم نادرست است. لطفا فقط عدد وارد فرمایید." ForeColor="Red" Text="*" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                            <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" MaxLength="15" ValidationGroup="bbb" />
                            <asp:HiddenField runat="server" ID="hdnHokmNumber" />
                        </div>
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="row">
                        <%-- <div class="col-sm-10">
                                                <p class="lblInput">شماره بیمه</p>
                                                <asp:RequiredFieldValidator ValidationGroup="bbb" ErrorMessage="درج شماره بیمه الزامی می باشد." ControlToValidate="txtInsuranceNumber" ForeColor="Red" Display="Dynamic" Text="*" runat="server" />
                                                <asp:RegularExpressionValidator ValidationGroup="bbb" ID="RegularExpressionValidator6" ErrorMessage="شماره بیمه باید 8 تا 10رقم باشد" ControlToValidate="txtInsuranceNumber" ValidationExpression="^[0-9]{8,10}$" runat="server" Text="*" ForeColor="Red" Display="Dynamic" />
                                                <asp:TextBox ID="txtInsuranceNumber" runat="server" CssClass="form-control" MaxLength="10" />
                                            </div>--%>
                        <div class="col-sm-10">
                            <p class="lblInput">مبلغ حکم</p>
                            <%--<asp:RequiredFieldValidator ID="rfvMablaghHokm" ValidationGroup="bbb" ErrorMessage="درج مبلغ حکم الزامی است." ControlToValidate="txtMablaghHokm" ForeColor="Red" Display="Dynamic" Text="*" runat="server" />--%>
                            <div class="currencyWrapper">
                                <asp:TextBox ID="txtMablaghHokm" runat="server" ToolTip="ریال" CssClass="form-control form-inline " ValidationGroup="bbb" />
                                <span>ریال</span>
                            </div>
                            <asp:HiddenField runat="server" ID="hdnMablaghHokm" />
                        </div>
                        <div class="col-sm-10">
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
                            <asp:HiddenField runat="server" ID="hdnHireType2" />
                        </div>
                        <div class="col-sm-10">
                            <asp:CheckBox runat="server" ID="chkBoundHour" Text="متقاضی تکمیل ساعت موظفی در واحد الکترونیکی هستم" />
                        </div>
                        <asp:HiddenField runat="server" ID="hdnBoundHour" />
                        <div class="col-sm-10   ">
                            <p class="lblInput"><span class="text-danger">استاد گرامی :</span> لطفا آخرین حکم کارگزینی بارگذاری گردد.</p>
                            <asp:CustomValidator ValidationGroup="bbb" ID="vldPersonalImage" ErrorMessage="قراردادن عکس حکم الزامی می باشد" Display="Dynamic" Text="*" ForeColor="Red" OnServerValidate="vldPersonalImage_ServerValidate" ClientValidationFunction="validateUpload1" runat="server" />
                            <div>
                                <telerik:RadAsyncUpload ID="flpHokm" runat="server" OnClientValidationFailed="OnClientValidationFailed" OnClientFilesSelected="OnClientFilesSelectedflpHokm" Width="100%" MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,,pdf" MaxFileSize="716800" Localization-Cancel="لغو" Localization-Remove="حذف" Localization-Select="انتخاب" ValidationGroup="bbb"></telerik:RadAsyncUpload>
                            </div>
                            <asp:HiddenField runat="server" ID="hdnHokmImage" />
                        </div>
                    </div>
                </div>


            </asp:Panel>
            
        </div>
    </form>
</body>
</html>
