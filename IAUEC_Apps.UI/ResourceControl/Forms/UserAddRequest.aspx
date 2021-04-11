<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="UserAddRequest.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.UserAddRequest" %>


<%@ Register Src="../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>فرم ثبت درخواست کلاس </h3>
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        label {
            padding-right: 5px;
        }

        .chkPadding {
            margin-right: 10px;
            margin-left: 5px;
        }
    </style>
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%# chblOptions.ClientID %>");
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


        //<![CDATA[
        var RadTimePicker1;
        var RadTimePicker2;

        function validateTimes(sender, args) {
            var time1 = $find("<%=RadTimePicker1.ClientID %>").get_dateInput().get_value();
            var time2 = $find("<%=RadTimePicker2.ClientID %>").get_dateInput().get_value();

            if ((time2 <= time1)) {

                args.IsValid = false;
                return;
            } else {
                args.IsValid = true;
                return;
            }
        }

        function onLoadRadTimePicker1(sender, args) {
            RadTimePicker1 = sender;
        }

        function onLoadRadTimePicker2(sender, args) {
            RadTimePicker2 = sender;
        }
        //]]>

    </script>

    <div class="rtl">

        <uc1:AccessControl ID="AccessControl1" runat="server" />

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-8 ">
                        <div class="table-responsive">
                            <table class="table table-bordered table-condensed table-striped  tbl">
                                <tr>
                                    <td colspan="2" class="bg-primary">
                                        <asp:Label ID="lblHeader" Text="ثبت درخواست" runat="server" CssClass="h3" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>لطفا نوع کلاس مورد نیاز خود را انتخاب کنید</td>
                                    <td>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="drpCategory" runat="server" CssClass="dropdown form-control" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged1" AutoPostBack="True" />
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCategory" ErrorMessage="لطفا نوع کلاس مورد نیاز خود را مشخص کنید" InitialValue="انتخاب کنید" Text="*" ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <asp:CheckBox ID="chkMeeting" runat="server" Checked="false" AutoPostBack="true" CssClass="chkPadding" Text=" تخصیص سالن کنفرانس جهت برگزاری جلسات" OnCheckedChanged="chkMeeting_CheckedChanged" />
                                        </div>
                                    </td>
                                </tr>
                                <tr id="trDaneshkadeh" runat="server" visible="false">
                                    <td>لطفا دانشکده را انتخاب کنید</td>
                                    <td>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="drpChooseDanshkade" runat="server" OnSelectedIndexChanged="drpChooseDanshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdown form-control" Enabled="false" >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="لطفا دانشکده را انتخاب کنید" ControlToValidate="drpChooseDanshkade" ForeColor="Red" InitialValue="انتخاب کنید" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr id="trProfCrs" runat="server">
                                    <td>
                                        <span>استاد :</span>

                                        <asp:RequiredFieldValidator ID="valField" ControlToValidate="RadComboBoxField" ForeColor="Red" ValidationGroup="submit" Text="*" runat="server" InitialValue="جستجو و انتخاب کنید" ErrorMessage="لطفا نام استاد را انتخاب کنید"></asp:RequiredFieldValidator>
                                        <div>
                                            <telerik:RadComboBox ID="RadComboBoxField" runat="server" OnSelectedIndexChanged="RadComboBoxField_SelectedIndexChanged" AutoPostBack="true" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Culture="(Default)" Height="300px"></telerik:RadComboBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="col-sm-9">
                                            <span>درس :</span>
                                            <asp:DropDownList ID="drpCourse" runat="server" CssClass=" dropdown form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="انتخاب کنید" Text="*" ForeColor="Red" ErrorMessage="لطفا کلاس را انتخاب نمایید" ControlToValidate="drpCourse" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr id="trOptions" runat="server" visible="false">
                                    <td>
                                        <span>لطفا امکانات مورد نیاز خود را انتخاب کنید :</span>
                                        <br />
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="لطفا امکانات مورد نیاز خود را انتخاب کنید" ForeColor="Red" ValidationGroup="submit" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="ValidateCheckBoxList" EnableClientScript="true">*</asp:CustomValidator>
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="chblOptions" runat="server" CssClass="checkbox1" RepeatColumns="1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="col-sm-8">
                                            <span>ظرفیت کلاس :</span>
                                            <asp:TextBox ID="txtCapacity" runat="server" />

                                        </div>
                                        <br />
                                        <br />
                                        <span>نفر</span>
                                        <asp:RequiredFieldValidator  EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت الزامیست" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />

                                        <asp:RangeValidator MinimumValue="1" MaximumValue="50" EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت مورد الزامیست(حداقل 1 نفر و حداکثر 50 نفر)" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />
                                    </td>
                                    <td>
                                        <div class="col-sm-4">
                                            <span>محل استفاده :</span>
                                            <asp:DropDownList ID="drpLocation" runat="server" CssClass="dropdown form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="لطفا محل را مشخص نمایید" ForeColor="Red" ControlToValidate="drpLocation" ValidationGroup="submit" InitialValue="انتخاب کنید">*</asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>توضیحات  :</td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>لطفا زمان استفاده را انتخاب کنید :
                                    </td>
                                    <td id="dvTime" runat="server">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span>ساعت آغاز :</span>
                                                <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker1" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView4" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                    <DateInput ID="DateInput1" runat="server">
                                                        <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </div>
                                            <div class="col-md-6">
                                                <span>ساعت پایان :</span>
                                                <asp:RequiredFieldValidator ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />

                                                <asp:CustomValidator ID="vldTimes" ErrorMessage="زمان پایان باید بعد از ساعت شروع باشد" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ClientValidationFunction="validateTimes" OnServerValidate="vldTimes_ServerValidate" ControlToValidate="RadTimePicker2" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker2" RenderMode="Lightweight" runat="server" EnableTyping="false" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView1" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                    <DateInput ID="DateInput2" runat="server">
                                                        <ClientEvents OnLoad="onLoadRadTimePicker2"></ClientEvents>
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p id="dateofsession">تاریخ :</p>
                                                <asp:TextBox ID="pcal1" runat="server" CssClass="pdate form-control" Enabled="False"/>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal1" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                                <br />
                                                <asp:CheckBox ID="chbRepeat" Text="ثبت درخواست بیش از یکبار " OnCheckedChanged="chbRepeat_CheckedChanged" AutoPostBack="true" CssClass="checkbox1" runat="server" />
                                            </div>
                                            <div id="dvEndDate" runat="server" visible="false" class="col-md-6">
                                                <p id="enddateofsession">تاریخ پایان :</p>
                                                <asp:TextBox ID="pcal2" runat="server" CssClass="pdate form-control" Enabled="False"/>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal2" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="pcal2" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                                <br />
                                                <strong>روزهای تکرار در هر هفته</strong>
                                                <asp:CheckBoxList ID="chblWeekDates" runat="server" CssClass="checkbox1">
                                                    <asp:ListItem Text="شنبه" Value="6" />
                                                    <asp:ListItem Text="یکشنبه" Value="0" />
                                                    <asp:ListItem Text="دوشنبه" Value="1" />
                                                    <asp:ListItem Text="سه شنبه" Value="2" />
                                                    <asp:ListItem Text="چهارشنبه" Value="3" />
                                                    <asp:ListItem Text="پنج شنبه" Value="4" />
                                                    <asp:ListItem Text="جمعه" Value="5" />
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center" colspan="2">
                                        <asp:Button Text="ثبت درخواست" ID="btnAddRquest" runat="server" CssClass="btn btn-primary" OnClick="btnAddRquest_Click" ValidationGroup="submit" />
                                        <br />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="لطفا موارد مشخص شده را وارد نمایید :" ValidationGroup="submit" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <AlertTemplate>
                <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                    <div class="rwDialogContent">
                        <div style="color: black; font-size: 13px;" class="rwDialogMessage">
                            {1}
                        </div>
                    </div>
                    <div class="rwDialogButtons text-center">
                        <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                    </div>
                </div>
            </AlertTemplate>
        </telerik:RadWindowManager>
    </div>
    <script>
        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1',
            { extraInputID: 'ContentPlaceHolder1_pcal1', extraInputFormat: 'yyyy/mm/dd' });
        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder2_pcal2',
                        { extraInputID: 'ContentPlaceHolder2_pcal2', extraInputFormat: 'yyyy/mm/dd' });

        function redirectToLast() {
            window.history.back();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginreq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginreq(sender, args) {
            postbackElement = args.get_postBackElement();
            $("#wait").css("display", "block");
        }
        function endReq(sender, args) {
            document.getElementById(postbackElement.id).focus();
            $("#wait").css("display", "none");
        }


    </script>
    <div id="wait" class="modal" style="display: none">
        <div class="center">
            <img alt="loading" src="../Content/animatedEllipse.gif" />
            <span>درحال بارگذاری...</span>
        </div>
    </div>
</asp:Content>

