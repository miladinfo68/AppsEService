<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="OfficeStudentReview.aspx.cs" Inherits="ResourceControl.PL.Forms.OfficeStudentReview" %>

<%@ Import Namespace="ResourceControl.BLL" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style>
        .GridViewEditRow input[type=text] {
            width: 100px;
        }
        /* size textboxes */
        .GridViewEditRow select {
            width: 100px;
        }
        /* size drop down lists */

        .cssPager span {
            background-color: #cccccc !important;
            color: black !important;
            font-family: 'B Yekan' !important;
            font-size: 24px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        .cssPager a {
            background-color: #ffffff !important;
            color: #808080 !important;
            font-family: 'B Yekan' !important;
            font-size: 22px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        .bTable {
            padding: 10px 15px;
            border-bottom: 1px solid transparent;
            border-top-right-radius: 3px;
            border-top-left-radius: 3px;
        }

        .buttons, button, .btn {
            margin-bottom: initial;
            margin-right: initial;
        }

        .tbl {
            max-width: 100%;
        }

            .tbl td {
                text-align: center !important;
                font-family: 'B Titr';
            }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            vertical-align: middle;
            font-family: 'B Titr';
        }

        #tdDrpRequestTypeList > select {
            float: right !important;
        }

        td {
            font-family: 'B Titr';
        }

        .text-primary {
            color: silver;
        }
    </style>
    <script type="text/javascript">
        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
        }
    </script>

    <style>
        .marginItem {
            margin: 10px;
        }

        .paddingItem {
            padding: 20px;
        }

        .centerItem {
            text-align: center !important;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#historyModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" Visible="false" runat="server"></asp:Literal>
    <h3>فرم بررسی کاربر اداری</h3>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />

    <div class="container" dir="rtl">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdRequestList" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <script type="text/javascript">
                    function refresgGrid() {
                        document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
                    }
                </script>
                <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass=" hidden" Text="refreshGrid" runat="server" />
                <table class="table tbl table-bordered table-condensed">
                    <tr>
                        <td class="bg-primary">
                            <h3>لیست درخواستها </h3>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdDrpRequestTypeList">
                            <asp:DropDownList ID="drpRequestTypeList" runat="server" CssClass="dropdown" AutoPostBack="True" OnSelectedIndexChanged="drpRequestTypeList_SelectedIndexChanged">
                                <asp:ListItem Text="لیست درخواستهای منتظر تایید" Value="1" />
                                <asp:ListItem Text="لیست درخواستهای تایید شده" Value="2" />
                                <%--          <asp:ListItem Text="لیست درخواستهای رد شده" Value="3" />
                                <asp:ListItem Text="لیست درخواستهای اطلاع رسانی شده" Value="4" />--%>
                                <asp:ListItem Text="لیست درخواستهای از دست رفته" Value="5" />
                                <asp:ListItem Text="لیست کلیه درخواستها" Value="6" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="direction: rtl; min-height: 100px; overflow-y: visible">
                                <asp:GridView ID="grdRequestList" runat="server" AllowPaging="true" PageSize="20"
                                    OnPageIndexChanging="grdRequestList_PageIndexChanging"
                                    CssClass="table table-bordered text-center table-condensed"
                                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                    EmptyDataText="هیچ درخواستی پیدا نشد."
                                    OnRowCommand="grdRequestList_RowCommand" OnRowDataBound="grdRequestList_RowDataBound" OnRowDeleting="grdRequestList_RowDeleting">
                                    <HeaderStyle CssClass=" text-center bg-primary" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="شماره درخواست" DataField="ID" />
                                        <asp:BoundField HeaderText="شماره دانشجویی" DataField="issuerID" />
                                        <asp:BoundField HeaderText="رشته تحصیلی" DataField="nameresh" />
                                        <asp:TemplateField HeaderText="وضعیت">
                                            <ItemTemplate>
                                                <asp:Image ID="imgStatus" runat="server" Width="25px" Height="25px" ImageAlign="Middle" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="تاریخ ثبت" DataField="issue_time" />
                                        <asp:BoundField HeaderText="متقاضی" DataField="issuerName" />
                                        <asp:BoundField HeaderText="توضیحات" DataField="note" />
                                        <asp:TemplateField HeaderText="عملیات">
                                            <ItemTemplate>
                                                <asp:Button ID="btnCheckRequest" Text="بررسی" runat="server" CssClass="btn btn-primary btn-block" CommandName="checkReq" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                                        <asp:TemplateField HeaderText="رد درخواست">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDenyRequest" Text="رد" runat="server" CssClass="btn btn-danger btn-block" CommandName="deny" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="مشاهده">
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowDateTime" Text="نمایش" CssClass="btn btn-success" CommandName="showInfo" CommandArgument='<%# Eval("ID") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="تاریخچه">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings NextPageText="بعدی" PreviousPageText="قبلی" FirstPageText="صفحه اول" LastPageText="صفحه آخر" Mode="Numeric" Position="Bottom" />
                                    <PagerStyle CssClass="cssPager" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="modal fade" id="historyModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="background-color: aqua;">
                    <div class="modal-dialog" role="document" style="width: 70%">
                        <div class="modal-content bg-info border-dark">

                            <div class="modal-header" dir="rtl">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                                    <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                                    </span>
                                </button>
                                <div class="modal-header bg-orange" dir="rtl">
                                    <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                                </div>
                            </div>
                            <div class="modal-body">
                                <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border-bottom-color: black">
                                    <tr class="bg-blue-sky">
                                        <th>نام کاربر</th>
                                        <th>تاریخ</th>
                                        <th>ساعت</th>
                                        <th>وضعیت</th>
                                        <th>توضیحات</th>
                                    </tr>

                                    <asp:ListView ID="lst_history" runat="server">
                                        <ItemTemplate>
                                            <tr class="bg-blue" style="text-align: center;">
                                                <td>
                                                    <%#Eval("Name") %>
                                                </td>
                                                <td>
                                                    <%#Eval("LogDate") %>
                                                </td>
                                                <td>
                                                    <%#Eval("LogTime") %>
                                                </td>
                                                <td>
                                                    <%#Eval("EventName") %>
                                                </td>
                                                <td>
                                                    <%#Eval("Description") %>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </table>

                            </div>

                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>




        <div style="float: left; margin-left: 10px; margin-top: 2%">
            <asp:ImageButton ID="bt1ExportExcle" runat="server" ToolTip="خروجی اکسل" Width="50" ImageUrl="../Images/microsoft excel.png" OnClick="bt1ExportExcle_OnClick" />
        </div>


    </div>

    <telerik:RadWindow ID="RadWindow1" AutoSize="false" Height="600" runat="server" Width="1050" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>

                    <br />
                    <div class="panel panel-primary" style="text-align: right; direction: rtl; width: 95%; margin: auto;">
                        <div class="panel-heading">اطلاعات درخواست</div>
                        <div class="panel-body" style="margin: auto;">

                            <div class="panel panel-default" style="text-align: right; direction: rtl; width: 11%; margin: auto; float: right; margin-left: 3%; text-align: center;">
                                <div class="panel-heading bg-green">شماره</div>
                                <div class="panel-body">
                                    <asp:Label runat="server" ID="lblRequestNumber" />
                                </div>
                            </div>

                            <div class="panel panel-default" style="text-align: right; direction: rtl; width: 30%; margin: auto; float: right; margin-left: 3%; text-align: center;">
                                <div class="panel-heading bg-green">درخواست کننده</div>
                                <div class="panel-body">
                                    <asp:Label runat="server" ID="lblIssuerName" />
                                </div>
                            </div>

                            <div class="panel panel-default" style="text-align: right; direction: rtl; width: 50%; margin: auto; float: right; margin-left: 3%; text-align: center;">
                                <%--   <div class="panel-heading bg-green">رشته درخواست کننده</div>
                                <div class="panel-body">
                                    <asp:Label runat="server" ID="lblResh" />
                                </div>--%>
                                <div class="panel-heading bg-green">دانشکده درخواست کننده</div>
                                <div class="panel-body">
                                    <asp:Label runat="server" ID="lblDanesh" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="panel panel-primary" runat="server" visible="False" style="text-align: right; direction: rtl; width: 95%; margin: auto;">
                        <div class="panel-heading">وضعیت کلاس های قابل تخصیص</div>
                        <div class="panel-body" style="margin: auto;">
                            <div class="row" style="text-align: right; direction: rtl; width: 95%; margin: auto;">
                                <div>
                                    <asp:GridView ID="grdResourceState" runat="server"
                                        CssClass="table table-bordered table-condensed table-stripted table-responsive border-blue"
                                        OnRowDataBound="grdResourceState_OnRowDataBound"
                                        AutoGenerateColumns="False">
                                        <HeaderStyle CssClass="bg-warning" />
                                        <EditRowStyle CssClass="GridViewEditRow" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ردیف">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="نام کلاس" DataField="name" />
                                            <asp:BoundField HeaderText="شماره درخواست" DataField="requestId" />
                                            <asp:BoundField HeaderText="دانشجو استفاده کننده" DataField="issuerName" />
                                            <asp:BoundField HeaderText="تاریخ" DataField="Date" />
                                            <asp:TemplateField HeaderText="ساعت شروع">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label44" Text='<%# Eval("startTime").ToString().TimeToTicks() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ساعت پایان">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label55" Text='<%# Eval("endTime").ToString().TimeToTicks() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="وضعیت">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl66" runat="server" Text='<%#Eval("status").ToString().StatuseCondition()%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnResourceState" Value='<%#Eval("status") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>

                                </div>

                            </div>
                        </div>
                    </div>


                    <div class="alert alert-danger" style="margin-left: 2.5%; margin-right: 2.5%">
                        <h6 style="text-align: center;">تذکر بسیار مهم: حتما در ابتدا، کلاس هایی رو تخصیص دهید که با  دیگر دانشکده ها به صورت مشترک استفاده نمی شوند در صورت پر بودن کلاس های غیر اشتراکی کلاس اشتراکی را تخصیص دهید
                        </h6>

                    </div>

                    <div class="panel panel-primary" style="text-align: right; direction: rtl; width: 95%; margin: auto;">
                        <div class="panel-heading">تاریخ ها و ساعات درخواستی</div>
                        <div class="panel-body" style="margin: auto;">
                            <div class="container" style="overflow: visible" dir="rtl">
                                <div class="row">
                                    <div class="col-sm-9" style="overflow: visible !important">
                                        <asp:GridView ID="grdDateTime" runat="server"
                                            DataKeyNames="datetimeid"
                                            OnRowDataBound="grdDateTime_RowDataBound"
                                            OnRowCommand="grdDateTime_OnRowCommand"
                                            OnRowCancelingEdit="grdDateTime_RowCancelingEdit"
                                            OnRowEditing="grdDateTime_RowEditing"
                                            OnRowUpdating="grdDateTime_RowUpdating"
                                            AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-stripted table-responsive border-blue">
                                            <HeaderStyle CssClass="bg-primary" />
                                            <EditRowStyle CssClass="GridViewEditRow" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ردیف">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="تاریخ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<asp:TextBox ID="txtDate" Text='<%# Eval("Date").ToString() %>' runat="server" ReadOnly="true" />--%>
                                                        <asp:Label Text='<%# Eval("Date").ToString() %>' runat="server" ID="lblDate" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ساعت شروع">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                </telerik:RadTimePicker>--%>
                                                        <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" ID="lblStartTime" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ساعت پایان">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<asp:RequiredFieldValidator ErrorMessage="لطفا زمان پایان را مشخص کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker2" runat="server" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                </telerik:RadTimePicker>--%>
                                                        <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" ID="lblEndTime" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="کلاس پیشنهادی">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpResource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpResource_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="dateTimeId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblDateTimeId" Text='<%# Eval("DateTimeId") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="عملیات مورد نیاز">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnRegister" Text="ثبت" runat="server" ControlStyle-CssClass="btn btn-sm btn-warning" CommandName="Register" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DateTimeId") %>' />
                                                        <asp:Button ID="btnCancle" Text="لغو" runat="server" ControlStyle-CssClass="btn btn-sm btn-warning" CommandName="Cancle" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DateTimeId") %>' />
                                                        <asp:Button ID="btnEdi" Text="ویرایش" runat="server" ControlStyle-CssClass="btn btn-sm btn-warning" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"DateTimeId") %>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="reqid" DataField="RequestId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <%--                                       <asp:CommandField EditText="ویرایش" ShowEditButton="true" HeaderText="عملیات" UpdateText="ثبت" CancelText="لغو" ControlStyle-CssClass="btn btn-sm btn-warning" />--%>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnfReqId" runat="server" />
                                    </div>

                                    <div class="col-sm-3" style="overflow: visible">
                                        <div id="dvOperation" runat="server" class="panel panel-danger">
                                            <div class="panel-heading" style="text-align: center">
                                                عملیات
                                            </div>
                                            <div class="panel-body">
                                                <div runat="server" visible="False">
                                                    <div id="dvSuggestResource" runat="server" visible="false">
                                                        <p>تخصیص کلاس به صورت دسته ای</p>
                                                        <asp:DropDownList ID="drpCandidateResource" runat="server" OnSelectedIndexChanged="drpCandidateResource_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <br />
                                                    </div>
                                                </div>

                                                <div id="dvCancelRequest" runat="server">
                                                    <table style="max-width: 100%;" class="center-margin">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnSubmitFinal" CssClass="btn btn-success" ValidationGroup="submit" Text="تایید نهایی" runat="server" />
                                                            </td>
                                                            <td runat="server" visible="False">
                                                                <asp:Button ID="btnCancelRequest" OnClick="btnCancelRequest_Click" CssClass="btn btn-danger" Text="رد درخواست" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:CustomValidator ID="vldSubmitDatetime" ForeColor="Red" ErrorMessage="لطفا برای تمامی روز ها یک کلاس مناسب اختصاص دهید" ValidationGroup="submit" OnServerValidate="vldSubmitDatetime_ServerValidate" ClientValidationFunction="validateGrid" runat="server" Display="Dynamic" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>
                            <script type="text/javascript">
                                function GetRadWindow() {
                                    var oWindow = null;
                                    if (window.radWindow)
                                        oWindow = window.radWindow;
                                    else if (window.frameElement && window.frameElement.radWindow)
                                        oWindow = window.frameElement.radWindow;
                                    return oWindow;
                                }


                            </script>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindow2" AutoSize="true" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="heading bg-danger" style="padding: 5px">
                            <h3 class="text-danger">رد درخواست</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-10">
                                <asp:Label ID="Label6" Text="دلیل لغو/رد درخواست:" runat="server" />
                                <asp:TextBox ID="txtDenyMessage" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="لطفا دلیل لغو/رد درخواست را ذکر کنید" ForeColor="Red" Display="Dynamic" ValidationGroup="deny" ControlToValidate="txtDenyMessage" runat="server" />
                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnDenyRequest" Text="لغو/رد درخواست" OnClientClick="sure(this);return false;" OnClick="btnDenyRequest_Click" ValidationGroup="deny" CssClass="btn btn-danger" runat="server" />
                    </div>
                    <asp:HiddenField ID="hdnfDenyReqId" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

    <telerik:RadWindow ID="RadWindow3" runat="server" Width="850px" Height="600px" Skin="Glow" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl">
                        <div class="heading2 bg-green" style="padding: 5px">
                            <h3>جزئیات درخواست :
                                <asp:Image ID="imgStatus2" runat="server" Width="30px" /></h3>

                        </div>
                        <br />
                        <div class="row center-margin">
                            <div style="border: 5px solid #A9A9A9; margin-left: 5%; margin-right: 5%;">
                                <div style="margin: 5%;">
                                    <table class="table table-responsive" style="font-weight: bolder;">
                                        <tr>
                                            <td>
                                                <strong>موضوع جلسه دفاع : </strong>
                                                <asp:Label ID="lblDarkhast" CssClass="text-primary" runat="server" />
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <strong>وضعیت : </strong>
                                                        <asp:Label ID="lblStatue" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-3">
                                                        <strong>شماره درخواست : </strong>
                                                        <asp:Label ID="lblRequestId" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>تاریخ ثبت درخواست : </strong>
                                                        <asp:Label ID="lbldateOfRequest" CssClass="text-primary" runat="server" />
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <strong>تاریخ درخواستی برگزاری جلسه:</strong>
                                                        <asp:Label ID="lblRequest" CssClass="text-primary" runat="server" />

                                                    </div>
                                                    <div class="col-md-3">
                                                        <strong>ساعت شروع: </strong>
                                                        <asp:Label ID="lblTime1" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <strong>ساعت پایان: </strong>
                                                        <asp:Label ID="lblTime2" CssClass="text-primary" runat="server" />

                                                    </div>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr class="hidden">
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <strong>استفاده از رایانه شخصی: </strong>
                                                        <asp:Label ID="lblOwnSysytem" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <strong>دفاع آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineDef" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-3">
                                                        <strong>پخش آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineShow" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    
                                                    <div class="col-md-3">
                                                        <strong>برگزاری جلسه دفاع به صورت آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineDoingMeeting" CssClass="text-primary"                                       runat="server" />
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>


                                        <%--                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>توضیحات: </strong>
                                                        <asp:Label ID="lblTozieh" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>
                                                            <asp:Literal ID="litDenyNot" runat="server" Visible="False" Text=""></asp:Literal>

                                                        </strong>
                                                        <asp:Label ID="lblDenyNot" Visible="False" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>
                                                            <asp:Literal ID="lblheader" runat="server" Visible="False" Text=""></asp:Literal></strong>
                                                        <asp:Label ID="lblDateOfResponse" Visible="False" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>


                                        </tr>--%>



                                        <%--<tr>
                                            <td style="width: 55%;">
                                                <div runat="server" id="tblRangeOfDate" visible="False" class="table-responsive text-center">
                                                    <div class="bg-blue-sky">
                                                        <h4 class="text-center">زمانهای درخواستی فعلی</h4>
                                                    </div>
                                                    <asp:GridView ID="grdOldDateTime" HorizontalAlign="Center" CssClass="table table-responsive backGroundForGrdDate text-center" runat="server" AutoGenerateColumns="false">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="تاریخ" DataField="date">
                                                                <ItemStyle ForeColor="#29343B"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="ساعت شروع">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" ForeColor="#29343B" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ساعت پایان">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" ForeColor="#29343B" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:GridView ID="grdFacilities" CssClass="table table-responsive backGroundForGrdDate text-center" runat="server" AutoGenerateColumns="false">
                                                    <HeaderStyle CssClass="bg-blue-sky" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ردیف">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ForeColor="#29343B" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="امکانات درخواستی" DataField="Name">
                                                            <ItemStyle ForeColor="#29343B"></ItemStyle>
                                                        </asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                            </td>


                                        </tr>--%>
                                    </table>
                                </div>
                            </div>


                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>


    <script type="text/javascript">
                                function validateGrid(sender, args) {
                                    args.IsValid = true;

                                    $('#<%=grdDateTime.ClientID%> tr').each(function () {
                                        // action to perform.  Use $(this) for changing each cell
                                        var tr = $(this)
                                        if (tr.hasClass("bg-danger")) {
                                            args.IsValid = false;
                                            return;
                                        }

                                        tr.find('td').each(function () {

                                            if ($(this).find('option:selected').val() == 0) {
                                                args.IsValid = false;
                                                return;
                                            }
                                        });
                                    });
                                }

                                var HTMLtbl =
                                    {
                                        getData: function (table) {
                                            var data = [];
                                            table.find('tr').not(':first').each(function (rowIndex, r) {
                                                var cols = [];
                                                $(this).find('td').each(function (colIndex, c) {

                                                    if ($(this).children(':text,:hidden,textarea').length > 0)
                                                        cols.push($(this).children('input,textarea', 'select').val().trim());

                                                    //if dropdown text is needed then uncomment it and remove SELECT from above IF condition//
                                                    else if ($(this).children('select').length > 0)
                                                        cols.push($(this).find('option:selected').val());

                                                    else if ($(this).children(':checkbox').length > 0)
                                                        cols.push($(this).children(':checkbox').is(':checked') ? 1 : 0);
                                                    else
                                                        cols.push($(this).text().trim());
                                                });
                                                data.push(cols);
                                            });
                                            return data;
                                        }
                                    }


                                $("body").on("click", "#<%=btnSubmitFinal.ClientID%>", function () {

                                    var IsValid = true;
                                    // Do client side button click stuff here.
                                    $('#<%=grdDateTime.ClientID%> tr').each(function () {
                                                // action to perform.  Use $(this) for changing each cell
                                        var tr = $(this);
                                                if (tr.hasClass("bg-danger")) {
                                                    IsValid = false;
                                                    return false;
                                                }

                                                tr.find('td').each(function () {

                                                    if ($(this).find('option:selected').val() == 0) {
                                                        IsValid = false;
                                                        return false;
                                                    }
                                                });
                                            });

                                            if (IsValid) {

                                                var data = HTMLtbl.getData($('#<%=grdDateTime.ClientID%>'));  // passing that table's ID //
                                                var parameters = {};
                                                parameters.array = data;

                                                $.ajax({
                                                    type: "POST",
                                                    url: "OfficeStudentReview.aspx/btnSubmitFinalClick",
                                                    data: JSON.stringify(parameters),
                                                    contentType: "application/json; charset=utf-8",
                                                    dataType: "json",
                                                    success: function (msg) {
                                                        if (msg.d == "success")
                                                            radalert("کلاس با موفقیت تایید شد.", 300, 100, "پیام", closeRadWindow, "");
                                                        else {
                                                            if (msg.d == "error")
                                                                radalert("امکان ثبت وجود ندارد.", 300, 100, "پیام", null, "");
                                                            else
                                                                radalert("امکان ثبت به دلیل تداخل با جلسه ای دیگر وجود ندارد.", 400, 100, "پیام", null, "");

                                                        }
                                                    },
                                                    error: function (msg) {
                                                        alert("خطا!!!");
                                                    }
                                                });
                                            }

                                });

                                function closeRadWindow() {
                                    var window = $find('<%=RadWindow1.ClientID %>');
                                    window.close();
                                    refresgGrid();
                                }

                                function closeRadWindow2() {
                                    var window = $find('<%=RadWindow2.ClientID %>');
                                            window.close();
                                            refresgGrid();
                                        }

                                        function sure(button) {
                                            function aspButtonCallbackFn(arg) {
                                                if (arg) {
                                                    __doPostBack(button.name, "");
                                                }
                                            }
                                            radconfirm("آیا مطمئن هستید که می خواهید این درخواست را رد کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
                                        }
    </script>
</asp:Content>
