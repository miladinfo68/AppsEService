<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="UserEditRequest.aspx.cs" Inherits="ResourceControl.PL.Forms.EducationUserEditRequest" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .form-control {
            padding: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server" Visible="false"> </asp:Literal>
    <h1>فرم ویرایش درخواست کلاس </h1>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

        function validate(sender, args) {
            var Date1 = new Date(RadTimePicker1.get_selectedDate());
            var Date2 = new Date(RadTimePicker2.get_selectedDate());
            args.IsValid = true;
            if ((Date2 - Date1) < 0) {
                alert("The second time value should be greater than the first!");
                args.IsValid = false;
            }
        }

        function onLoadRadTimePicker1(sender, args) {
            
            RadTimePicker1 = sender;
        }

        function onLoadRadTimePicker2(sender, args) {
            
            RadTimePicker2 = sender;
        }

        function redirectToPrevious() {
            window.location.href = "<%#Request.UrlReferrer.ToString()%>";
        }
        //]]>
    </script>

    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container rtl">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-8 ">
                        <div class="table-responsive">
                            <table class="table table-bordered table-condensed table-striped tbl">
                                <tr>
                                    <td colspan="2" class="bg-primary">
                                        <h4>ویرایش درخواست</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>لطفا نوع کلاس مورد نیاز خود را انتخاب کنید :</span>
                                    </td>
                                    <td>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="drpCategory" runat="server" CssClass="dropdown form-control" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged1" AutoPostBack="True" Enabled="False" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCategory" ErrorMessage="لطفا نوع کلاس مورد نیاز خود را مشخص کنید" ValidationGroup="submit" InitialValue="انتخاب کنید" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr id="trProfCrs" runat="server">
                                    <td>
                                        <span>استاد :</span>

                                        <asp:DropDownList runat="server" ID="drpProfessor" CssClass="dropdown form-control " Enabled="False"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <span>درس :</span>
                                        <asp:DropDownList ID="drpCourse" runat="server" CssClass=" dropdown form-control" Enabled="False"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="انتخاب کنید" Text="*" ValidationGroup="submit" ForeColor="Red" ErrorMessage="لطفا کلاس را انتخاب نمایید" ControlToValidate="drpCourse"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trOptions" runat="server">
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
                                        <span>محل استفاده :</span>
                                    </td>
                                    <td>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="drpLocation" runat="server" CssClass="dropdown form-control" Enabled="False"></asp:DropDownList>

                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="لطفا محل را مشخص نمایید" ForeColor="Red" InitialValue="انتخاب کنید" ValidationGroup="submit" ControlToValidate="drpLocation">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>ظرفیت :</span>
                                    </td>
                                    <td>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" />
                                        </div>
                                        <span>نفر</span>
                                        <asp:RequiredFieldValidator  EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت الزامیست" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />

                                        <asp:RangeValidator MinimumValue="1" MaximumValue="50" EnableClientScript="True" ValidateRequestMode="Enabled" Type="Integer" ErrorMessage="درج ظرفیت مورد الزامیست(حداقل 1 نفر و حداکثر 50 نفر)" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" Text="*" ControlToValidate="txtCapacity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>توضیحات  :</td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p>زمان جدید استفاده</p>
                                    </td>
                                    <td id="dvTime" runat="server">
                                        <div class="row">
                                            <div class="col-md-6">
                                                ساعت آغاز :
                                            <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker1" EnableTyping="False" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView4" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                </telerik:RadTimePicker>
                                            </div>
                                            <div class="col-md-6">
                                                ساعت پایان :
                                            <asp:RequiredFieldValidator ErrorMessage="زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                                <asp:CustomValidator ID="vldTimes" ErrorMessage="ساعت پایان باید بعد از ساعت شروع باشد" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ClientValidationFunction="validateTimes" OnServerValidate="vldTimes_ServerValidate" ControlToValidate="RadTimePicker2" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker2" EnableTyping="False" runat="server" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView1" Interval="00:15:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                </telerik:RadTimePicker>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p id="dateofsession">تاریخ :</p>
                                                <asp:TextBox ID="pcal1" runat="server" CssClass="pdate form-control" ValidationGroup="submit" Enabled="False"/>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal1" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
<%--                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>--%>
                                                <br />
                                                <asp:CheckBox ID="chbRepeat" Text="ثبت درخواست بیش از یکبار " OnCheckedChanged="chbRepeat_CheckedChanged" AutoPostBack="true" CssClass="checkbox1" runat="server" />
                                            </div>
                                            <div id="dvEndDate" runat="server" visible="false" class="col-md-6">
                                                <p id="enddateofsession">تاریخ پایان :</p>
                                                <asp:TextBox ID="pcal2" runat="server" CssClass="pdate form-control" ValidationGroup="submit" Enabled="False"/>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="لطفا تاریخ پایان درخواست خود را مشخص کنید" ControlToValidate="pcal2" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
<%--                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="pcal2" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>--%>
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
                                <tr class="text-center">
                                    <td class="text-center">
                                        <asp:Button Text="ثبت درخواست" ID="btnEditRequest" runat="server" CssClass="btn btn-info" OnClick="btnEditRequest_Click" ValidationGroup="submit" Width="120px" />
                                    </td>
                                    <td class="text-center">
                                        <asp:Button Text="انصراف" ID="btnPutOff" runat="server" CssClass="btn btn-warning" Width="132px" OnClick="btnPutOff_Click"/>

                                    </td>
                                </tr>
                                <tr id="errorsummery">
                                    <td colspan="2">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ForeColor="Red" HeaderText="لطفا موارد مشخص شده را وارد نمایید :" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="table-responsive">
                            <div class="bg-blue-sky">
                                <h4>زمانهای درخواستی فعلی</h4>
                            </div>
                            <asp:GridView ID="grdOldDateTime" CssClass="table table-bordered table-condensed table-striped" runat="server" AutoGenerateColumns="false">
                                <HeaderStyle CssClass=" bg-blue-sky" />
                                <Columns>
                                    <asp:BoundField HeaderText="تاریخ" DataField="date" />
                                    <asp:TemplateField HeaderText="ساعت شروع">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ساعت پایان">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="direction: rtl">
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
        </div>
    </div>
    <script>
        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1',
            { extraInputID: 'ContentPlaceHolder1_pcal1', extraInputFormat: 'yyyy/mm/dd' });
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
