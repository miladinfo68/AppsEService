<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/AdobeCMS.Master" AutoEventWireup="true" CodeBehind="EditDeleteMergeClasses.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.EditDeleteMergeClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../css/js-persian-cal.css" rel="stylesheet" />
    <script src="../js/js-persian-cal.min.js"></script>
    <style>
        .padding5 {
            padding: 5px;
        }

        .marginRight5 {
            margin-right: 5px;
        }

        .marginRight75 {
            margin-right: 75px;
        }

        .marginRight30 {
            margin-right: 30px;
        }

        .marginRight23 {
            margin-right: 23px;
        }

        .margintop5 {
            margin-top: 5px;
        }

        .margintop10 {
            margin-top: 10px;
        }

        .marginRight0 {
            margin-right: 0px;
        }

        .redColor {
            color: red;
        }
    </style>
    <script type="text/javascript">
        function confirmAspButton(button) {
            if (Page_ClientValidate("Edit")) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }
        }
        function confirmAspButton1(button) {

            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
        }

        function onLoadRadTimePicker1(sender, args) {
            RadTimePicker1 = sender;
        }

        function onLoadRadTimePicker2(sender, args) {
            RadTimePicker2 = sender;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" dir="rtl">
        <asp:UpdatePanel ID="upSearch" runat="server">
            <ContentTemplate>
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3 margintop5">
                                کد ادغام کلاس:
                        <asp:RequiredFieldValidator ID="rfvmerge" runat="server" Text="*" ErrorMessage="کد ادغامی را وارد کنید" Font-Bold="true" Font-Size="large" ControlToValidate="txtmergeCode" ForeColor="Red" ValidationGroup="Search"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtmergeCode" runat="server" CssClass="marginRight5" TabIndex="0"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnSearch" runat="server" Text="جستجو" OnClick="btnSearch_Click" ValidationGroup="Search" CssClass="btn btn-primary" TabIndex="1" />
                                <asp:Button ID="btnClear" runat="server" Text="پاک کردن" OnClick="btnClear_Click" CssClass="btn btn-dark" />

                            </div>
                            <div class="col-md-5">

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 marginRight75">
                                <asp:ValidationSummary ID="vsSearch" runat="server" ValidationGroup="Search" ForeColor="Red" ShowSummary="true" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row margintop10" id="divGrid" runat="server" visible="false">
                    <div class="col-md-12">
                        <telerik:RadGrid ID="grdClassList" runat="server" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin" MasterTableView-ShowHeadersWhenNoRecords="true">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="ردیف">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndex" runat="server" Text="<%#Container.ItemIndex + 1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="کد کلاس" DataField="codeclass"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="کد درس" DataField="codedars"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="نام درس" DataField="namedars"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="نام استاد" DataField="mergeostad"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="تاریخ اولین جلسه" DataField="date_first_session"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="ساعت کلاس" DataField="saatklass"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="تعداد جلسات" DataField="count_sessions"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
                <div class="row margintop10">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <asp:RadioButton ID="rdbEdit" runat="server" Text="ویرایش" TabIndex="2" Visible="false" AutoPostBack="true" GroupName="divs" OnCheckedChanged="rdbEdit_CheckedChanged" />
                        <asp:RadioButton ID="rdbDelete" runat="server" Text="حذف" TabIndex="3" Visible="false" AutoPostBack="true" GroupName="divs" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="row padding5 bg-warning" id="divEdit" runat="server" visible="false">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                کد درس:
                    <asp:RequiredFieldValidator ID="rfvClassCode" runat="server" Text="*" ErrorMessage="کد درس را وارد کنید" Font-Bold="true" Font-Size="large" ControlToValidate="txtCourseCode" ForeColor="Red" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtCourseCode" runat="server" TabIndex="4"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                تاریخ اولین جلسه:
                                <asp:RequiredFieldValidator ID="rfvFirstSession" runat="server" Text="*" Font-Bold="true" Font-Size="large" ErrorMessage="تاریخ اولین جلسه را وارد کنید" ControlToValidate="txtFirstSession" ForeColor="Red" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtFirstSession" runat="server" CssClass="pdate" TabIndex="5"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                تعداد جلسات:
                                <asp:RequiredFieldValidator ID="rfvSessionCount" runat="server" Text="*" Font-Bold="true" Font-Size="large" ErrorMessage="تعداد جلسات را وارد کنید" ControlToValidate="txtSessionCount" ForeColor="Red" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtSessionCount" runat="server" TabIndex="6"></asp:TextBox>
                                <asp:RangeValidator ID="rvSessionCount" runat="server" ControlToValidate="txtSessionCount" ValidationGroup="Edit" MinimumValue="1" MaximumValue="20" Text="*" ErrorMessage="تعداد جلسات باید بین 1 تا 20 باشد" Font-Bold="true" Font-Size="large" ForeColor="Red" Type="Integer" Display="Dynamic"></asp:RangeValidator>
                            </div>
                        </div>
                        <div class="row marginRight0 margintop10">
                            <div class="col-md-3">
                                نام استاد:
                    <asp:RequiredFieldValidator ID="rfvProf" runat="server" Text="*" Font-Bold="true" Font-Size="large" ControlToValidate="cmbProf" ErrorMessage="نام استاد را انتخاب کنید" ForeColor="Red" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                                <telerik:RadComboBox ID="cmbProf" runat="server" EmptyMessage="انتخاب کنید" ValidationGroup="merge" MarkFirstMatch="true" AutoPostBack="true" AllowCustomText="true" ExpandDirection="Down"></telerik:RadComboBox>
                            </div>
                            <div class="col-md-3">
                                روز تشکیل کلاس:
                                    <asp:RangeValidator ID="rfvDay" runat="server" Text="*" Font-Bold="true" Font-Size="large" ControlToValidate="drpDay" MinimumValue="1" MaximumValue="7" ErrorMessage="روز هفته را انتخاب کنید" ForeColor="Red" ValidationGroup="Edit"></asp:RangeValidator>
                                <asp:DropDownList ID="drpDay" runat="server" AutoPostBack="true">
                                    <asp:ListItem Text="--انتخاب کنید--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="شنبه" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="یکشنبه" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="دوشنبه" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="سه شنبه" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="چهارشنبه" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="پنج شنبه" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="جمعه" Value="7"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                ساعت شروع:
                        <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="Edit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                <telerik:RadTimePicker ID="RadTimePicker1" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                    <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                    </TimeView>
                                    <DateInput ID="Stime" runat="server">
                                        <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                    </DateInput>
                                </telerik:RadTimePicker>
                            </div>
                            <div class="col-md-3">
                                ساعت پایان:
                        <asp:RequiredFieldValidator ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="Edit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                <asp:CustomValidator ID="vldTimes" ErrorMessage="زمان پایان باید بعد از ساعت شروع باشد" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ClientValidationFunction="validateTimes" OnServerValidate="vldTimes_ServerValidate" ControlToValidate="RadTimePicker2" runat="server" />
                                <telerik:RadTimePicker ID="RadTimePicker2" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                    <TimeView ID="TimeView1" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                    </TimeView>
                                    <DateInput ID="Etime" runat="server">
                                        <ClientEvents OnLoad="onLoadRadTimePicker2"></ClientEvents>
                                    </DateInput>
                                </telerik:RadTimePicker>
                            </div>
                        </div>
                        <div class="row margintop10">
                            <div class="col-md-5">
                            </div>
                            <div class="col-md-2 marginRight23">
                                <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-warning" Text="ویرایش" ValidationGroup="Edit" OnClientClick="confirmAspButton(this); return false;" OnClick="btnEdit_Click" />
                            </div>
                            <div class="col-md-5">
                            </div>
                        </div>
                    </div>
                    <div class="row marginTop10 padding5">
                        <div class="col-md-12">
                            <asp:ValidationSummary ID="vlds" runat="server" ValidationGroup="Edit" ForeColor="Red" HeaderText="لطفا به موارد زیر توجه کنید:" ShowSummary="true" />
                        </div>
                    </div>
                </div>
                <div class="row padding5 bg-danger" id="divDelete" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-6 marginRight30">
                            <p class="redColor">*توجه داشته باشید که تمامی کلاس هایی که با کد ادغام فوق تشکیل شده اند حذف میگردند</p>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5"></div>
                        <div class="col-md-2 marginRight30">
                            <asp:Button ID="btnDelete" runat="server" Text="حذف" OnClick="btnDelete_Click" OnClientClick="confirmAspButton1(this); return false;" CssClass="btn btn-danger" />
                        </div>
                        <div class="col-md-5"></div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rdbEdit" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="rdbDelete" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnEdit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script>
        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtFirstSession',
            { extraInputID: 'ContentPlaceHolder1_txtFirstSession', extraInputFormat: 'yyyy/mm/dd' });
        function redirectToLast() {
            window.history.back();
        }

    </script>
</asp:Content>
