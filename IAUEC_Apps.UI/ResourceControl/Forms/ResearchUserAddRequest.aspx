<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="ResearchUserAddRequest.aspx.cs" Inherits="ResourceControl.PL.Forms.ResearchUserAddRequest" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h1>ثبت درخواست رزرو کلاس حضوری برای دوره های کوتاه مدت</h1>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById("<%=chblOptions.ClientID %>");
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

        function validateTimes(sender, args) {
            var time1 = $find("<%=RadTimePicker1.ClientID %>").get_dateInput().get_value();
            var time2 = $find("<%=RadTimePicker2.ClientID %>").get_dateInput().get_value();

            if ((time2 < time1)) {

                args.IsValid = false;
                return;
            } else {
                args.IsValid = true;
                return;
            }
        }



    </script>
    <div class="container rtl">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <div class="table-responsive col-sm-8">
                    <table class="table table-bordered table-condensed table-striped tbl">
                        <tr>
                            <td colspan="2" class="bg-primary">
                                <h3>ثبت درخواست</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>لطفا نوع کلاس مورد نیاز خود را انتخاب کنید :</td>
                            <td>
                                <asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged1" AutoPostBack="True" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCategory" ErrorMessage="لطفا نوع منبع مورد نیاز خود را مشخص کنید" InitialValue="انتخاب کنید" Text="*" ValidationGroup="submit" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <%--<td>
                                <span>استاد :</span>
                                
                                <asp:RequiredFieldValidator ID="valField" Display="Dynamic" ControlToValidate="RadComboBoxField" ForeColor="Red" ValidationGroup="submit" Text="*" runat="server" InitialValue="جستجو و انتخاب کنید" ErrorMessage="لطفا رشته تحصیلی خود را انتخاب کنید"></asp:RequiredFieldValidator>
                                <div>
                                    <telerik:RadComboBox ID="RadComboBoxField" runat="server" OnSelectedIndexChanged="RadComboBoxField_SelectedIndexChanged" AutoPostBack="true" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Culture="(Default)" Height="300px"></telerik:RadComboBox>
                                </div>
                            </td>--%>
                            <td>
                                <div class="col-sm-9">
                                    <span>نام دوره را وارد نمایید :</span>
                                    <asp:TextBox ID="txt_courseName" runat="server"></asp:TextBox>
                                    <%--<asp:DropDownList ID="drpCourse" runat="server" CssClass=" dropdown form-control"></asp:DropDownList>--%>
                                    <asp:RequiredFieldValidator ErrorMessage="لطفا نام کلاس را وارد نمایید" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />

                                </div>
                                <div class="col-sm-3">
                                    <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="انتخاب کنید" Text="*" ForeColor="Red" ErrorMessage="لطفا نام کلاس را وارد نمایید" ControlToValidate="txt_courseName" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                </div>
                            </td>
                            <td></td>
                        </tr>
                        <tr id="trOptions" runat="server">
                            <td>
                                <span>لطفا امکانات مورد نیاز خود را انتخاب کنید :</span>
                                <br />
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="لطفا امکانات مورد نیاز خود را انتخاب کنید" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="ValidateCheckBoxList" ValidationGroup="submit" EnableClientScript="true">*</asp:CustomValidator>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chblOptions" runat="server" CssClass="checkbox1" RepeatColumns="1" Width="168px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>ظرفیت کلاس :</span>
                            </td>
                            <td>
                                <div class="col-sm-2">

                                    <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" />
                                </div>

                                <span>نفر</span>
                                <asp:RequiredFieldValidator ErrorMessage="درج ظریفت مورد نیاز الزامی است." ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />
                                <asp:RangeValidator MinimumValue="1" MaximumValue="50" EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت مورد الزامیست(حداقل 1 نفر و حداکثر 50 نفر)" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>محل استفاده :</span>
                                <br />
                                <%--                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ValidationGroup="submit" ControlToValidate="drpLocation" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                            </td>
                            <td>
                                <asp:DropDownList ID="drpLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="لطفا محل را مشخص نمایید" ForeColor="Red" ControlToValidate="drpLocation" ValidationGroup="submit" InitialValue="انتخاب کنید">*</asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>توضیحات  :</td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>لطفا زمان استفاده را انتخاب کنید :</td>
                            <td id="dvTime" runat="server">
                                <div class="row">
                                    <div class="col-md-6">
                                        <span>ساعت آغاز :</span>
                                        <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                        <telerik:RadTimePicker ID="RadTimePicker1" EnableTyping="False" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                            <TimeView ID="TimeView4" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                            </TimeView>
                                        </telerik:RadTimePicker>
                                    </div>
                                    <div class="col-md-6">
                                        <span>ساعت پایان :</span>
                                        <asp:RequiredFieldValidator ErrorMessage="زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                        <asp:CustomValidator ID="vldTimes" ErrorMessage="ساعت پایان باید بعد از ساعت شروع باشد" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ClientValidationFunction="validateTimes" OnServerValidate="vldTimes_ServerValidate" ControlToValidate="RadTimePicker2" runat="server" />
                                        <telerik:RadTimePicker ID="RadTimePicker2" EnableTyping="False" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                            <TimeView ID="TimeView1" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                            </TimeView>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="لطفا تاریخ پایان درخواست خود را مشخص کنید" ControlToValidate="pcal2" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="pcal2" ErrorMessage="لطفا تاریخ پایان استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
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
                            </td>
                        </tr>
                        <tr id="errorsummery">
                            <td colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="لطفا موارد مشخص شده را وارد نمایید :" ValidationGroup="submit" />
                            </td>
                        </tr>
                    </table>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    </div>
</asp:Content>
