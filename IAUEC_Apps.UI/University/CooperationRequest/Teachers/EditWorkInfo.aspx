<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/TeacherMaster.Master" AutoEventWireup="true" CodeBehind="EditWorkInfo.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.EditWorkInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <style>
        .currencyWrapper {
            position: relative;
        }

            .currencyWrapper > .form-control {
                padding-left: 35px;
            }

            .currencyWrapper > span {
                display: block;
                position: absolute;
                left: 0;
                top: 0;
                bottom: 0;
                margin: auto;
                height: 50%;
                width: 30px;
                color: #999;
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
    </style>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoaded);
        function PageLoaded(sender, args) {
            var e = document.getElementById('<%= txtMablaghHokm.ClientID %>');
            e.addEventListener('input', function (e) {
                e.target.value = commaSeparateNumber(e.target.value);
            }, false);
        }
        function commaSeparateNumber(val) {
            if (val != '') {
                if (val != 0) {
                    val = val.replace(/,/g, '');
                    while (/(\d+)(\d{3})/.test(val.toString())) {
                        val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
                    }
                }
                else
                    return '';
            }
            return val;
        }
    </script>
    <h1>بروزرسانی اطلاعات حکم کارگزینی</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (ShowView)
        { %>
    <asp:Panel ID="p" runat="server" DefaultButton="btnSubmitChanges">
        <div class="row" dir="rtl">
            <div id="pnlSabeghe" class="col-sm-12" runat="server">
                <div class="panel panel-success">
                    <div class="panel-heading">سوابق فعالیت</div>
                    <div class="panel-body ">
                        <div class="row">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
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
                                    <div class="row">
                                        <div class="col-sm-12 text-center">
                                            <asp:Button ID="btnSubmitChanges" ValidationGroup="bbb" OnClick="btnSubmitChanges_Click" CssClass="btn btn-success " Text="ثبت تغییرات" runat="server" />
                                            <asp:Button ID="btnSubmitChangesNo" OnClick="btnSubmitChangesNo_OnClick" CssClass="btn btn-success " Text="ثبت تغییرات" runat="server" />

                                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-warning " Text="انصراف" runat="server" />

                                        </div>
                                        <div class="col-sm-4 col-sm-pull-5">
                                            <asp:ValidationSummary runat="server" DisplayMode="BulletList" ForeColor="Red" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>


    </asp:Panel>
    <script type="text/javascript">
        
        function OnClientFilesSelectedflpHokm() {
            debugger;
            var $radBtn1 = $("#<%= rblIsHeiat.ClientID %> input:radio");
            var $radChecked1 = $(':radio:checked');
            var radasyncupload1;
            if ($radChecked1.val() !== "1") {
                radasyncupload1 = $find('<%= flpHokm.ClientID %>');

                radasyncupload1.deleteFileInputAt(0);
                radalert("امکان آپلود فایل وجود ندارد", 0, 100, "هشدار", "");
            }
        }
        /*---------------------*/
        var formItems = [
            $('#<%= ddlPastUniType.ClientID %>'),
            $('#<%= drpHireType.ClientID %>'),
            $('#<%= drpMartabe.ClientID %>'),
            $('#<%= drpPastUni.ClientID %>'),
            $('#<%= txtPaye.ClientID %>'),
            $('#<%= txtDateSodoorHokm.ClientID %>'),
            $('#<%= txtDateEjraHokm.ClientID %>'),
            $('#<%= txtHokmNumber.ClientID %>'),
            $('#<%= txtMablaghHokm.ClientID %>'),
            $('#<%= rdblHireType.ClientID %> input').closest('table'),
            $('#<%= chkBoundHour.ClientID %>')
        ];
        <%----%>
        var isChanged = false;

        $(window).load(function () {
            setEventHandlers();

        });
        function changeFlpHokmStatus(newStatus, e) {
            formItems[5].val($('#<%= txtDateSodoorHokm.ClientID %>').val());
            formItems[6].val($('#<%= txtDateEjraHokm.ClientID %>').val());


            var radasyncupload = $find('<%= flpHokm.ClientID %>');
            if (radasyncupload != null) {
                if ($(e) != null && $(e).val() != null) {
                    if (newStatus && !isChanged) {
                        var lastValue = getLastValue(e);
                        if ($(e).val() != '' && $(e).val() != 'on' && !$(e).hasClass('RadComboBox') && lastValue != $(e).val().replace(/,/g, '')) {   // TextBox And DropDownLists
                            //alert($(e).val());
                            //alert('a ' +lastValue);
                            radasyncupload.set_enabled(true);
                            isChanged = true;
                        }
                        else if ($(e).hasClass('RadComboBox')) {                                                                                     // RadComboBox
                            setTimeout(function () {
                                //alert($(e).val());
                                //alert('b ' +lastValue);
                                var dropFind = $find($(e).attr('id'));
                                var valueFind = dropFind.get_selectedItem().get_value();
                                if (lastValue != valueFind && !isChanged) {
                                    radasyncupload.set_enabled(true);
                                    isChanged = true;
                                }
                                else if (!isChanged)
                                    radasyncupload.set_enabled(false);
                            }, 100);
                        }
                        else if (($(e).val() == 'on' || $(e).val() == 'off') && (lastValue !== $(e).is(":checked"))) {                                // CheckBoxes
                            //alert($(e).val());
                            //alert('x ' +lastValue);
                            radasyncupload.set_enabled(true);
                            isChanged = true;
                        }
                        else if ($(e).val() == '' && $(e).find('input').length <= 0 && lastValue != $(e).val().replace(/,/g, '') && lastValue != '0') {
                            //alert($(e).val());
                            //alert('y ' +lastValue);
                            radasyncupload.set_enabled(true);
                            isChanged = true;
                        }
                        else if ($(e).val() == '' && $(e).find('input:checked').length > 0 && lastValue != $(e).find('input:checked').val()) {                                                 // RadioButtons
                            //alert($(e).val());
                            //alert('z ' +lastValue);
                            radasyncupload.set_enabled(true);
                            isChanged = true;
                        }
                        else if ($('#<%= hdnHokmImage.ClientID %>').val() == "0") {
                                radasyncupload.set_enabled(true);
                                isChanged = true;
                            }
                            else
                                radasyncupload.set_enabled(false);
    }
    else if (isChanged) { }
    else
        radasyncupload.set_enabled(false);
}
else
    radasyncupload.set_enabled(false);
}
}
function getLastValue(e) {
    if (e.attr('id') == $('#<%= drpHireType.ClientID %>').attr('id'))
                return $('#<%= hdnHireType.ClientID %>').val();
            if (e.attr('id') == $('#<%= drpMartabe.ClientID %>').attr('id'))
                return $('#<%= hdnMartabe.ClientID %>').val();
            if (e.attr('id') == $('#<%= txtPaye.ClientID %>').attr('id'))
                return $('#<%= hdnPaye.ClientID %>').val();
            if (e.attr('id') == $('#<%= drpPastUni.ClientID %>').attr('id'))
                return $('#<%= hdnPastUni.ClientID %>').val();
            if (e.attr('id') == $('#<%= ddlPastUniType.ClientID %>').attr('id'))
                return $('#<%= hdnPastUniType.ClientID %>').val();
            if (e.attr('id') == $('#<%= txtDateSodoorHokm.ClientID %>').attr('id'))
                return $('#<%= hdnDateSodoorHokm.ClientID %>').val();
            if (e.attr('id') == $('#<%= txtDateEjraHokm.ClientID %>').attr('id'))
                return $('#<%= hdnDateEjraHokm.ClientID %>').val();
            if (e.attr('id') == $('#<%= txtHokmNumber.ClientID %>').attr('id'))
                return $('#<%= hdnHokmNumber.ClientID %>').val();
            if (e.attr('id') == $('#<%= txtMablaghHokm.ClientID %>').attr('id'))
                return $('#<%= hdnMablaghHokm.ClientID %>').val();
            if (e.attr('id') == $('#<%= rdblHireType.ClientID %>').attr('id'))
                return $('#<%= hdnHireType2.ClientID %>').val();
            if (e.attr('id') == $('#<%= chkBoundHour.ClientID %>').attr('id'))
                if ($('#<%= hdnBoundHour.ClientID %>').val() == "True")
                    return true;
                else
                    return false;
            return false;
        }
        function setEventHandlers() {
            changeFlpHokmStatus(false, null);

            $('#<%= ddlPastUniType.ClientID %>').change(function () { formItems[0].val(this.value); });
            $('#<%= drpHireType.ClientID %>').change(function () { formItems[1].val(this.value); });
            $('#<%= drpMartabe.ClientID %>').change(function () { formItems[2].val(this.value); });
            $('#<%= txtPaye.ClientID %>').on('input propertychange change paste', function () { formItems[4].val(this.value); });
            $('#<%= txtDateSodoorHokm.ClientID %>').on('input propertychange change paste', function () { formItems[5].val(this.value); });
            $('#<%= txtDateEjraHokm.ClientID %>').on('input propertychange change paste', function () { formItems[6].val(this.value); });
            $('#<%= txtHokmNumber.ClientID %>').on('input propertychange change paste', function () { formItems[7].val(this.value); });
            $('#<%= txtMablaghHokm.ClientID %>').on('input propertychange change paste', function () { formItems[8].val(this.value); });
            $('#<%= rdblHireType.ClientID %> input').change(function () { formItems[9].val(this.value); });
            $('#<%= chkBoundHour.ClientID %>').change(function () { formItems[10].prop("checked", this.checked); });


            setInterval(function () {
                isChanged = false;
                $.each(formItems, function (index, value) { changeFlpHokmStatus(true, value); });
            }, 100);
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () { setEventHandlers(); });
        /*---------------------*/


        function validateUpload1(sender, args) {
            var upload = $find("<%= flpHokm.ClientID %>");
            var rb = $('#<%= rblIsHeiat.ClientID %>').find('input:checked').val();
            args.IsValid = (upload.getUploadedFiles().length != 0 || rb == '2');
        }

        if ($(<%= txtMablaghHokm.ClientID %>).val() != '')
            $(<%= txtMablaghHokm.ClientID %>).number(true, 0);

        function OnClientValidationFailed(sender, args) {
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

        var $ = $telerik.$;

        function submitPage() {
            //finds all file uploads that are currently in progress
            var uploadingRows = $(".RadAsyncUpload").find(".ruUploadProgress");
            //iterates and checks is there any file uploads that are successfully completed or failed and if yes - pop-up an alert box and prevent page submitting 
            for (var i = 0; i < uploadingRows.length; i++) {
                if (!$(uploadingRows[i]).hasClass("ruUploadCancelled") && !$(uploadingRows[i]).hasClass("ruUploadFailure") && !$(uploadingRows[i]).hasClass("ruUploadSuccess")) {
                    radalert("لطفا تا اتمام بارگذاری تمامی فایل ها صبر کنید", 0, 100, "هشدار", "");
                    return false;
                }
            }

            return true;
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_pageLoaded(function (sender, e) {
                SetDatePicker();
            })
            //prm.add_endRequest(function (sender, e) {
            //    if (sender._postBackSettings.panelsToUpdate != null) {
            //        SetDatePicker();
            //    }
            //});
        };

        var pcalArray = [];
        function SetDatePicker() {
            var idArray = [];
            $('.pcal').each(function () {
                idArray.push(this.id);
            });

            if (idArray.length > 0) {
                for (var i = 0; i < idArray.length; i++) {
                    if (idArray[i] != 0) {
                        var x = new AMIB.persianCalendar(idArray[i],
                            { extraInputID: idArray[i], extraInputFormat: 'yyyy/mm/dd' });
                        pcalArray.push(x);
                    }
                }
            }
            var rb = $('#<%= rblIsHeiat.ClientID %>').find('input:checked').val();
            if (rb == '1')
                ChangeDatePickerStatus(true);
            else
                ChangeDatePickerStatus(false);
        }
        function ChangeDatePickerStatus(val) {
            if (!val) {
                $('.pcalBtn').each(function () { $(this).addClass('disabled') });
            }
            else {
                $('.pcalBtn').each(function () { $(this).removeClass('disabled') });
            }
        }

        function RedirectToMain() {
            window.location = "EditMain.aspx";
        }
        $(window).load(function () {
            debugger;
            var radasyncupload;
            if (<%= rblIsHeiat.SelectedValue %> !== 1) {
                radasyncupload = $find('<%= flpHokm.ClientID %>');
                radasyncupload.set_enabled(false);
            }
            else {
                radasyncupload = $find('<%= flpHokm.ClientID %>');

            }
        });

    </script>

    <% } %>

    <% else
        { %>
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server"></telerik:RadWindowManager>
    <script type="text/javascript">
        function RedirectToMain() {
            window.location = "EditMain.aspx";
        }

    </script>
    <% } %>
</asp:Content>
