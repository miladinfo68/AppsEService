<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/ResourceControlUsers.Master" CodeBehind="ProfessorAddRequest.aspx.cs" Inherits="ResourceControl.PL.Forms.professor" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h3>سامانه رزرواسیون کلاس</h3>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

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
        var RadTimePicker1 = $find('<%= RadTimePicker1.ClientID %>');
        var RadTimePicker2 = $find('<%= RadTimePicker2.ClientID %>');

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

    <div class="container rtl">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <table class="table table-bordered table-hover table-striped tbl">
                    <tr>
                        <td colspan="2" class="bg-primary">
                            <h3>ثبت درخواست رزرو کلاس</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>لطفا نوع کلاس مورد نیاز خود را انتخاب کنید :</td>
                        <td>
                            <asp:DropDownList ID="drpCategory" runat="server" CssClass="dropdown" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged1" AutoPostBack="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCategory" ErrorMessage="لطفا نوع کلاس مورد نیاز خود را مشخص کنید" InitialValue="انتخاب کنید" Text="*" ForeColor="Red" ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>کلاس :</td>
                        <td>
                            <asp:DropDownList ID="drpCourse" runat="server" CssClass="dropdown" OnDataBound="drpCourse_DataBound"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="انتخاب کنید" Text="*" ForeColor="Red" ErrorMessage="لطفا کلاس را انتخاب نمایید" ControlToValidate="drpCourse" ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%--  <tr>
                    <td>موضوع درخواست :</td>
                    <td>                    
                        <asp:RadioButtonList ID="txtSubject" runat="server" CssClass="radio" RepeatColumns="2" >
                            <asp:ListItem Text="تدریس آنلاین" Value="تدریس آنلاین" />
                            <asp:ListItem Text="کلاس جبرانی" Value="کلاس جبرانی" />
                            <asp:ListItem Text="تحویل پروژه" Value="تحویل پروژه" />
                        </asp:RadioButtonList>    
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" ErrorMessage="لطفا موضوع درخواست خود را مشخص کنید" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                    </tr>--%>
                    <tr id="trOptions" runat="server">
                        <td>لطفا امکانات مورد نیاز خود را انتخاب کنید :
                    <br />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="لطفا امکانات مورد نیاز خود را انتخاب کنید" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="ValidateCheckBoxList" EnableClientScript="true" ValidationGroup="submit">*</asp:CustomValidator>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chblOptions" runat="server" CssClass="" RepeatColumns="6" Width="442px"/>
                        </td>
                    </tr>
                    <tr>
                        <td>محل استفاده:
                            <asp:DropDownList ID="drpLocation" runat="server" CssClass="dropdown"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="لطفا محل را مشخص نمایید" ControlToValidate="drpLocation" ForeColor="Red" InitialValue="انتخاب کنید" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                        </td>
                        <td>ظرفیت مورد نیاز :
                                    <asp:TextBox ID="txtCapacity" runat="server" />
                            <asp:RequiredFieldValidator EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت الزامیست" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />

                            <asp:RangeValidator MinimumValue="1" MaximumValue="50" EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت مورد الزامیست(حداقل 1 نفر و حداکثر 50 نفر)" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />


                        </td>
                    </tr>
                    <tr>
                        <td>توضیحات  :</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>لطفا زمان استفاده را انتخاب کنید :
                            
                        </td>
                        <td>
                            <div class="col-md-3">
                                ساعت آغاز :
                                    <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                <telerik:RadTimePicker ID="RadTimePicker1" runat="server" EnableTyping="false" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                    <TimeView ID="TimeView4" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                    </TimeView>
                                </telerik:RadTimePicker>
                            </div>
                            <div class="col-md-3">
                                ساعت پایان :
                                    <asp:RequiredFieldValidator ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                <asp:CustomValidator ID="vldTimes" ErrorMessage="زمان پایان باید بعد از ساعت شروع باشد" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ClientValidationFunction="validateTimes" OnServerValidate="vldTimes_ServerValidate" ControlToValidate="RadTimePicker2" runat="server" />
                                <telerik:RadTimePicker ID="RadTimePicker2" runat="server" EnableTyping="false" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                    <TimeView ID="TimeView1" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                    </TimeView>
                                </telerik:RadTimePicker>
                            </div>

                        </td>

                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            <div class="col-md-3">
                                <p id="dateofsession">تاریخ :</p>
                                <asp:TextBox ID="pcal1" runat="server" CssClass="pdate" Enabled="False"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal1" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                <br />
                                <asp:CheckBox ID="chbRepeat" Text="ثبت درخواست بیش از یکبار " OnCheckedChanged="chbRepeat_CheckedChanged" AutoPostBack="true" CssClass="" runat="server" />
                            </div>
                            <div id="dvEndDate" runat="server" visible="false" class="col-md-3">
                                <p id="enddateofsession">تاریخ پایان :</p>
                                <asp:TextBox ID="pcal2" runat="server" CssClass="pdate" Enabled="False"/>
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
                        </td>
                    </tr>
                    <tr>
                        <td class="text-center" colspan="2">
                            <asp:Button Text="ثبت درخواست" ID="btnAddRquest" runat="server" CssClass="btn btn-primary" OnClick="btnAddRquest_Click" ValidationGroup="submit" />
                        </td>
                    </tr>
                    <tr id="errorsummery">
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" CssClass="list-group" HeaderText="لطفا موارد مشخص شده را وارد نمایید :" ValidationGroup="submit" />
                        </td>
                    </tr>
                </table>
                <script>
                    var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder2_pcal1',
                        { extraInputID: 'ContentPlaceHolder2_pcal1', extraInputFormat: 'yyyy/mm/dd' });

                    var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder2_pcal2',
                        { extraInputID: 'ContentPlaceHolder2_pcal2', extraInputFormat: 'yyyy/mm/dd' });
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <AlertTemplate>
                <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                    <div class="rwDialogContent">
                        <div style="color: black; font-size=13px;" class="rwDialogMessage">
                            {1}
                        </div>
                    </div>
                    <div class="rwDialogButtons text-center">
                        <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                    </div>
                </div>
            </AlertTemplate>
        </telerik:RadWindowManager>
        <script type="text/javascript">
            function redirectToLast() {
                window.location = "ProfessorReview.aspx";
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
    </div>

</asp:Content>

