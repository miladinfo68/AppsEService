<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ProfessorEditRequests.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ProfessorEditRequests" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

     </title>
    <script src="Scripts/js-persian-cal.min.js"></script>
    <link href="css/js-persian-cal.css" rel="stylesheet" />
    <style>
        /*.form-control {
            height: 40px !important;
        }*/

        .txtHeader {
            font-size: 16px !important;
        }

        .radfont, .rgHeader {
            font-family: 'B Titr' !important;
            font-weight: bold;
        }

        .rwContentRow {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
        }

        .rcbInner {
            height: 36px !important;
            border-top: 1px solid #cccccc !important;
            border-right: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-left: none !important;
            color: #555555 !important;
        }

        .rcbActionButton {
            height: 36px !important;
            background-color: white !important;
            background-image: none;
            border-top: 1px solid #cccccc !important;
            border-left: 1px solid #cccccc !important;
            border-bottom: 1px solid #cccccc !important;
            border-right: none !important;
        }

        .RadComboBox_Default .rcbActionButton {
            background-image: none !important;
        }

        .RadComboBox_Default .rcbInput {
            height: 32px !important;
            font-family: Yekan,'B Yekan' !important;
            font-size: 14px !important;
            font-weight: bold !important;
            padding-right: 11px !important;
            color: #555555 !important;
        }

        .rcbItem, rcbHovered {
            font-family: Yekan,'B Yekan' !important;
            font-size: 13px !important;
            font-weight: bold !important;
            color: #555555 !important;
        }

        .RadComboBoxDropDown_Default .rcbHovered {
            background-color: #2fa4e7 !important;
            color: white !important;
            font-family: Yekan,'B Yekan' !important;
            font-weight: bold !important;
        }

        .lblInput {
            padding-top: 5px !important;
        }

        .well {
            padding-bottom: 5px !important;
            padding-top: 5px !important;
            margin-bottom: 0px !important;
        }

        .panel-body {
            padding-bottom: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>بررسی درخواست های ویرایش اطلاعات اساتید</h3>
     <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" dir="rtl">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <script type="text/javascript">
                    function refreshGrid() {
                        document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
                    }

                    function confirmAspButton(button) {
                        function aspButtonCallbackFn(arg) {
                            if (arg) {
                                __doPostBack(button.name, "");
                            }
                        }
                        radconfirm("آیا مطمئن هستید که می خواهید این درخواست را تایید کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
                    }

                    function denyAspButton(button) {
                        function aspButtonCallbackFn(arg) {
                            if (arg) {
                                __doPostBack(button.name, "");
                            }
                        }
                        radconfirm("آیا مطمئن هستید که می خواهید این درخواست را رد کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
                    }
                    function toPending(button) {
                        debugger;
                        function aspButtonCallbackFn(arg) {
                            debugger;
                            if (arg) {
                                __doPostBack(button.name, "");
                            }
                        }
                        radconfirm("آیا مطمئن هستید که می خواهید این درخواست را به کارتابل در حال بررسی بفرستید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
                    }
                </script>
                <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="refreshGrid" runat="server" />
                <div class="row">
                    <div class="col-sm-6">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <span class="txtHeader">نمایش بر اساس نوع و وضعیت درخواست</span>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <span>نوع درخواست :</span>
                                        <asp:DropDownList ID="drpRequestType" runat="server" CssClass="form-control dropdown form-inline">
                                            <asp:ListItem Text="ویرایش اطلاعات فردی" Value="17" />
                                            <asp:ListItem Text="ویرایش اطلاعات تماس" Value="18" />
                                            <asp:ListItem Text="ویرایش اطلاعات حکم کارگزینی" Value="19" />
                                            <asp:ListItem Text="ویرایش نحوه همکاری" Value="20" />
                                            <asp:ListItem Text="تمام ویرایش ها" Value="21" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6">
                                        <span>وضعیت درخواست :</span>
                                        <asp:RadioButtonList ID="rdblStatus" CssClass="radio" runat="server" RepeatColumns="1">
                                            <asp:ListItem Text="منتظر بررسی" Value="6" />
                                            <asp:ListItem Text="تایید شده" Value="7" />
                                            <asp:ListItem Text="رد شده" Value="5" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row text-center">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnShowRequest" Text="نمایش" CssClass="btn btn-primary" OnClick="btnShowRequest_Click" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="panel panel-success">
                            <div class="panel-heading">
                                <span class="txtHeader">جستجو بر اساس کد استاد :</span>
                            </div>
                            <div class="panel-body">
                                <div class="col-sm-6">
                                    <span>کد استاد :</span>
                                    <asp:RegularExpressionValidator
                                        ErrorMessage="کد استاد درج شده اشتباه است."
                                        ControlToValidate="txtSearchByProfCode"
                                        ValidationExpression="^[0-9]{0,10}$"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="profCode"
                                        runat="server" />
                                    <asp:RequiredFieldValidator
                                        ErrorMessage="درج کد استاد الزامی است"
                                        ControlToValidate="txtSearchByProfCode"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="profCode"
                                        runat="server" />
                                    <asp:TextBox ID="txtSearchByProfCode" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-sm-6">
                                    <span>وضعیت درخواست :</span>
                                    <asp:RadioButtonList ID="rdblRequestState" CssClass="radio" runat="server" RepeatColumns="2">
                                        <asp:ListItem Text="منتظر بررسی" Value="6" />
                                        <asp:ListItem Text="تایید شده" Value="7" />
                                        <asp:ListItem Text="رد شده" Value="5" />
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator
                                        ErrorMessage="لطفا وضعیت درخواست را مشخص فرمایید"
                                        ControlToValidate="rdblRequestState"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="profCode"
                                        runat="server" />
                                </div>
                                <div class="row text-center">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSearchProf" OnClick="btnSearchProf_Click" ValidationGroup="profCode" Text="جستجو" runat="server" CssClass="btn btn-success" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:GridView ID="grdRequestList" runat="server"
                            ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false"
                            OnRowDataBound="grdRequestList_RowDataBound"
                            OnRowCommand="grdRequestList_RowCommand"
                            EmptyDataText="درخواستی پیدا نشد."
                            CssClass="table table-bordered table-condensed table-striped">
                            <HeaderStyle CssClass=" bg-purple" />
                            <Columns>
                                <asp:TemplateField HeaderText="ردیف">
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="کد استاد" DataField="code_ostad" />
                                <asp:BoundField HeaderText="نام استاد" DataField="FullName" />
                                <asp:BoundField HeaderText="شماره درخواست" DataField="Id" />
                                <asp:BoundField HeaderText="نوع درخواست" DataField="RequestTypeId" />
                                <asp:BoundField HeaderText="وضعیت" DataField="RequestLogId" />
                                <asp:BoundField HeaderText="تاریخ ثبت" DataField="CreateDate" />
                                <asp:TemplateField HeaderText="تغییرات">
                                    <ItemTemplate>
                                        <asp:Button Text="نمایش تغییرات" CommandName="show" CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-success btn-sm" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnfURL" runat="server" Value='<%#Eval("ScanImageUrl") %>' />
                                        <asp:HiddenField ID="hdnfReqType" runat="server" Value='<%#Eval("RequestTypeId") %>' />
                                        <asp:HiddenField ID="hdnfReqLog" runat="server" Value='<%#Eval("RequestLogId") %>' />
                                        <asp:HiddenField ID="hdnfInfPeo" runat="server" Value='<%#Eval("hr_infopeople_id") %>' />
                                        <asp:HiddenField ID="hdnfName" runat="server" Value='<%#Eval("hdn_fullName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        
        </asp:UpdatePanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    </div>
    <telerik:RadWindow ID="RadWindow1" runat="server" Width="900" Height="650" CenterIfModal="true">
        <ContentTemplate>
            <div class="container" dir="rtl">
                <asp:UpdatePanel UpdateMode="Always" runat="server" ID="upProf">
                    <ContentTemplate>
                        <div class="container">
                            <div class=" panel-success col-md-12">
                                <div class="col-md-3">
                                    <h5 style="float: right; color: gray">نام و نام خانوادگی:</h5>
                                    <h5 style="float: right; color: black" id="hName" runat="server"></h5>
                                </div>
                                <div class="col-md-2">
                                    <h5 style="float: right; color: gray">کد درخواست:</h5>
                                    <h5 style="float: right; color: black" id="hCode" runat="server"></h5>
                                </div>
                                <div class="col-md-3">
                                    <h5 style="float: right; color: gray">نوع درخواست:</h5>
                                    <h5 style="float: right; color: black" id="hReq" runat="server"></h5>
                                </div>

                                <div class="col-md-2">
                                    <a class="btn btn-info" visible="true" href="ShowDetailInfo.aspx" target="_blank" id="btnShowDetailInfo" runat="server">نمایش اطلاعات ثبت شده استاد</a>
                                    <%--<asp:Button ID="btnShowDetailInfo" runat="server" Text="نمایش اطلاعات ثبت شده استاد" CssClass="left btn-success"/>--%>
                              <%--      <asp:HiddenField runat="server" ID="hdnInfoPeople" />--%>
                                </div>
                            </div>

                        </div>
                        
                        <div id="dvPersonalInfo" visible="true" runat="server" class="container">
                            <div id="dvIdd" class="panel panel-primary" visible="true" runat="server">
                                <div class="panel-heading">
                                    <span class="txtHeader">تغییرات اطلاعات شناسنامه ای</span>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage11.aspx" target="_blank" id="btnScanPersonelly" runat="server">اسکن عکس پرسنلی</a>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage11.aspx" target="_blank" id="btnScanMelli" runat="server">اسکن کارت ملی</a>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage11.aspx" target="_blank" id="btnScanIdd" runat="server">اسکن شناسنامه</a>
                                    <%--<asp:Button ID="btnScanIdd" runat="server" Text="اسکن شناسنامه" CssClass="left btn-info" />--%>
                                </div>
                                <asp:GridView ID="grdIDD" runat="server"
                                    CssClass="table table-bordered table-striped table-condensed"
                                    OnRowEditing="grdChangeList_RowEditing"
                                    OnRowCancelingEdit="grdChangeList_RowCancelingEdit"
                                    OnRowUpdating="grdChangeList_RowUpdating"
                                    OnRowDataBound="grdIDD_RowDataBound"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id">
                                    <HeaderStyle CssClass="bg-blue-sky" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                        <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="مقدار جدید">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:DropDownList ID="drpNewValue" CssClass="form-control"  Visible="false" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                    ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    runat="server" />
                                                <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                    ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red" Display="Dynamic"
                                                    Enabled="false"
                                                    ErrorMessage="فرمت تاریخ وارد شده اشتباه است." Text="*" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                                <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnfControlToFieldId" runat="server" Value='<%#Eval("ControlToFieldId") %>' />
                                                <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField EditText="ویرایش" ShowEditButton="true" HeaderText="عملیات" UpdateText="ثبت" CancelText="لغو" ControlStyle-CssClass="btn btn-sm btn-warning" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="dvMadrak" class="panel panel-primary" visible="true" runat="server">
                                <div class="panel-heading">
                                    <span class="txtHeader">تغییرات اطلاعات مدرک تحصیلی</span>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanArzesh" runat="server">اسکن ارزشنامه تحصیلی</a>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanMadrak" runat="server">اسکن مدرک تحصیلی</a>

                                </div>
                                <asp:GridView ID="grdMadrak" runat="server"
                                    CssClass="table table-bordered table-striped table-condensed"
                                    OnRowEditing="grdChangeList_RowEditing"
                                    OnRowCancelingEdit="grdChangeList_RowCancelingEdit"
                                    OnRowUpdating="grdChangeList_RowUpdating"
                                    OnRowDataBound="grdMadrak_RowDataBound"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id">
                                    <HeaderStyle CssClass="bg-blue-sky" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                        <asp:BoundField HeaderText="مقدار قبلی" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="مقدار جدید">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" Visible="false" />
                                                <asp:DropDownList ID="drpNewValue" CssClass="form-control"  runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                    ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    runat="server" Text="*" ValidationGroup="vg" />
                                                <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                    ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red" Display="Dynamic"
                                                    Enabled="false"
                                                    ErrorMessage="فرمت تاریخ وارد شده اشتباه است." Text="*" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                                <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnfControlToFieldId" runat="server" Value='<%#Eval("ControlToFieldId") %>' />
                                                <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField EditText="ویرایش" ShowEditButton="true" HeaderText="عملیات" UpdateText="ثبت" CancelText="لغو" ControlStyle-CssClass="btn btn-sm btn-warning" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="dvBime" class="panel panel-primary" visible="true" runat="server">
                                <div class="panel-heading">
                                    <span class="txtHeader">تغییرات اطلاعات بیمه و بازنشستگی</span>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanBazneshaste" runat="server">اسکن حکم بازنشستگی</a>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanBime" runat="server">اسکن بیمه</a>

                                </div>
                                <asp:GridView ID="grdBime" runat="server"
                                    CssClass="table table-bordered table-striped table-condensed"
                                    OnRowEditing="grdChangeList_RowEditing"
                                    OnRowCancelingEdit="grdChangeList_RowCancelingEdit"
                                    OnRowUpdating="grdChangeList_RowUpdating"
                                    OnRowDataBound="grdBime_RowDataBound"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id">
                                    <HeaderStyle CssClass="bg-blue-sky" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                        <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="مقدار جدید">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                    ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    runat="server" Text="*" ValidationGroup="vg" />
                                                <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                    ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red" Display="Dynamic"
                                                    Enabled="false"
                                                    ErrorMessage="فرمت تاریخ وارد شده اشتباه است." Text="*" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                                <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnfControlToFieldId" runat="server" Value='<%#Eval("ControlToFieldId") %>' />
                                                <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField EditText="ویرایش" ShowEditButton="true" HeaderText="عملیات" UpdateText="ثبت" CancelText="لغو" ControlStyle-CssClass="btn btn-sm btn-warning" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="dvInf" class="panel panel-primary" visible="true" runat="server">
                                <div class="panel-heading">
                                    <span class="txtHeader">تغییرات اطلاعات تکمیلی</span>
                                    <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanInf" runat="server">اسکن نظام وظیفه</a>
                                    <%--<asp:Button ID="btnScanInf" runat="server" Text="اسکن نظام وظیفه" CssClass="left btn-info" />--%>
                                </div>
                                <asp:GridView ID="grdInf" runat="server"
                                    CssClass="table table-bordered table-striped table-condensed"
                                    OnRowEditing="grdChangeList_RowEditing"
                                    OnRowCancelingEdit="grdChangeList_RowCancelingEdit"
                                    OnRowUpdating="grdChangeList_RowUpdating"
                                    OnRowDataBound="grdInf_RowDataBound"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id">
                                    <HeaderStyle CssClass="bg-blue-sky" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                        <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="مقدار جدید">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                <asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                    ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    runat="server" Text="*" ValidationGroup="vg" />
                                                <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                    ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                    ControlToValidate="txtNewValue"
                                                    ForeColor="Red" Display="Dynamic"
                                                    Enabled="false"
                                                    ErrorMessage="فرمت تاریخ وارد شده اشتباه است." Text="*" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                                <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnfControlToFieldId" runat="server" Value='<%#Eval("ControlToFieldId") %>' />
                                                <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField EditText="ویرایش" ShowEditButton="true" HeaderText="عملیات" UpdateText="ثبت" CancelText="لغو" ControlStyle-CssClass="btn btn-sm btn-warning" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="container" id="dvChangeList" runat="server" visible="false">
                            <asp:GridView ID="grdChangeList" runat="server"
                                CssClass="table table-bordered table-striped table-condensed"
                                OnRowEditing="grdChangeList_RowEditing"
                                OnRowCancelingEdit="grdChangeList_RowCancelingEdit"
                                OnRowUpdating="grdChangeList_RowUpdating"
                                OnRowDataBound="grdChangeList_RowDataBound"
                                AutoGenerateColumns="false"
                                DataKeyNames="Id">
                                <HeaderStyle CssClass="bg-blue-sky" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ردیف">
                                        <ItemTemplate>
                                            <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                    <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="مقدار جدید">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                            <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                            <asp:CheckBoxList ID="chkDepNew_View" runat="server" CssClass="checkbox" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                            <div id="dvCheckBox" runat="server">
                                                <asp:CheckBoxList ID="chkDepNew" runat="server" CssClass="checkbox" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList>

                                            </div>
                                            <asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                ControlToValidate="txtNewValue"
                                                ForeColor="Red"
                                                Display="Dynamic"
                                                runat="server" Text="*" ValidationGroup="vg" />
                                            <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                ControlToValidate="txtNewValue"
                                                ForeColor="Red" Display="Dynamic"
                                                Enabled="false"
                                                ErrorMessage="فرمت تاریخ وارد شده اشتباه است." Text="*" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                            <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnfControlToFieldId" runat="server" Value='<%#Eval("ControlToFieldId") %>' />
                                            <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField EditText="ویرایش" ShowEditButton="true" HeaderText="عملیات" UpdateText="ثبت" CancelText="لغو" ControlStyle-CssClass="btn btn-sm btn-warning" />

                                </Columns>
                            </asp:GridView>
                            <%--<div class="row">
                                <div class="col-sm-4">
                                    <a class="btn btn-success btn-lg" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnShowImage" runat="server">نمایش فایل اسکن شده</a>
                                </div>
                            </div>--%>
                        </div>

                        <div id="dvNewHokm" visible="false" runat="server">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <h4>اطلاعات حکم کارگزینی جدید</h4>
                                </div>
                                <div class="panel-body">
                                    <asp:HiddenField ID="hdnfHokmId" runat="server" />
                                    <div class="row" runat="server" id="DivisHeiat">
                                        <div class="col-sm-12">
                                            <p class="lblInput">آیا دارای سابقه هیات علمی می باشید ؟</p>
                                            <asp:RadioButtonList runat="server" ID="rblIsHeiat" AutoPostBack="true" RepeatDirection="Horizontal"
                                                CssClass="radio isHeiat">
                                                <asp:ListItem Text="بله" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="خیر" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div runat="server" id="DivChangeHokm">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">کد استاد:</p>
                                            <asp:TextBox ID="txtCodeOstad" CssClass="form-control" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">شماره حکم:</p>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Text="*" ValidationGroup="vg" ErrorMessage="لطفا شماره حکم خود را وارد کنید" ControlToValidate="txtHokmNumber" ForeColor="Red" Display="Dynamic" runat="server" />
                                            <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">تاریخ صدور حکم :</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="درج تاریخ صدور حکم الزامی می باشد" ControlToValidate="txtDateSodoorHokm" ForeColor="Red" Display="Dynamic" runat="server" />
                                            <asp:RegularExpressionValidator Text="*" ValidationGroup="vg" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateSodoorHokm" runat="server" />
                                            <asp:TextBox ID="txtDateSodoorHokm" runat="server" CssClass="form-control form-inline pcal" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">تاریخ اجرای حکم در دانشگاه مبدأ</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="درج تاریخ اجرای حکم الزامی می باشد" ControlToValidate="txtDateEjraHokm" ForeColor="Red" Display="Dynamic" runat="server" />
                                            <asp:RegularExpressionValidator Text="*" ValidationGroup="vg" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateEjraHokm" runat="server" />
                                            <asp:TextBox ID="txtDateEjraHokm" runat="server" CssClass="form-control form-inline pcal" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">مرتبه دانشگاهی</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="لطفا مرتبه علمی را انتخاب کنید" InitialValue="0" ForeColor="Red" Display="Dynamic" ControlToValidate="drpMartabe" runat="server" />
                                            <asp:DropDownList ID="drpMartabe" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="انتخاب کنید" Value="0"  Selected="True" />
                                                <asp:ListItem Text="مربی" Value="1" />
                                                <asp:ListItem Text="دانشیار" Value="2" />
                                                <asp:ListItem Text="استادیار" Value="3" />
                                                <asp:ListItem Text="استاد" Value="4" />
                                                <asp:ListItem Text="فاقد مرتبه علمی" Value="8" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">پایه :</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="لطفا پایه را درج کنید" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPaye" runat="server" />
                                            <asp:RangeValidator Text="*" ValidationGroup="vg" ID="RangeValidator4" ErrorMessage="مقدار پایه باید عددی بین 0 و 50 باشد" Type="Integer" MinimumValue="0" MaximumValue="50" ControlToValidate="txtPaye" Display="Dynamic" ForeColor="Red" runat="server" />
                                            <asp:TextBox ID="txtPaye" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">نوع استخدام در دانشگاه مبدأ :</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="لطفا نوع استخدام را انتخاب کنید" InitialValue="0" ForeColor="Red" Display="Dynamic" ControlToValidate="drpHireType" runat="server" />
                                            <asp:DropDownList ID="drpHireType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                        <asp:ListItem Text="رسمی" Value="1" />
                                                        <asp:ListItem Text="آزمایشی" Value="2" />
                                                        <asp:ListItem Text="قراردادی" Value="3" />
                                                        <asp:ListItem Text="مامور به خدمت" Value="4" />
                                                        <asp:ListItem Text="موقت" Value="5" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">نام دانشگاه محل خدمت</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ID="valUniName" Display="Dynamic" ErrorMessage="لطفا نام دانشگاه محل خدمت را انتخاب کنید" ControlToValidate="drpPastUni" runat="server" InitialValue="جستجو و انتخاب کنید" ForeColor="Red" />
                                            <div>
                                                <telerik:RadComboBox ID="drpPastUni" runat="server" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Height="300px"></telerik:RadComboBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">نوع دانشگاه محل خدمت</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ID="RequiredFieldValidator1" Display="Dynamic" ErrorMessage="لطفا نوع دانشگاه محل خدمت را انتخاب کنید" ControlToValidate="ddlPastUniType" runat="server" InitialValue="0" ForeColor="Red" />
                                            <div>
                                                <asp:DropDownList ID="ddlPastUniType" runat="server" CssClass="form-control" Height="40px">
                                                    <asp:ListItem Text="انتخاب کنید" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="دولتی" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="آزاد" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="حوزه" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="خارج از کشور" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="سایر" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">نحوه همکاری در دانشگاه مبدأ :</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="لطفا نحوه همکاری  در دانشگاه مبدا را مشخص کنید" ControlToValidate="rdblHireType" runat="server" ForeColor="Red" Display="Dynamic" />
                                            <asp:RadioButtonList ID="rdblHireType" runat="server" CssClass="radio" RepeatDirection="Horizontal" RepeatColumns="3">
                                               <asp:ListItem Text="تمام وقت 32 ساعت" Value="1" />
                                                        <asp:ListItem Text="نیمه وقت" Value="2" />
                                                        <asp:ListItem Text="ساعتی" Value="3" />
                                                        <asp:ListItem Text="تمام وقت طرح مشمولان" Value="4" />
                                                        <asp:ListItem Text="بورسیه دکتری" Value="5" />
                                                        <asp:ListItem Text="کارمند" Value="6" />
                                                        <asp:ListItem Text="تمام وقت 44 ساعت" Value="7" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">مبلغ حکم در دانشگاه مبدا :</p>
                                            <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="درج مبلغ حکم الزامی است." ControlToValidate="txtMablaghHokm" ForeColor="Red" Display="Dynamic" runat="server" />
                                            <asp:TextBox ID="txtMablaghHokm" runat="server" ToolTip="ریال" CssClass="form-control form-inline " />
                                            <span>ریال</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:CheckBox ID="chkBoundHour" runat="server" Text="متقاضی تکمیل ساعت موظفی" />
                                        </div>
                                        <div class="col-sm-4">
                                            <a class="btn btn-success" href="OpenScanImage.aspx" target="_blank" id="imgNewHokm" runat="server">نمایش فایل اسکن شده</a>
                                        </div>
                                    </div>
                                    <div class="row well">
                                        <div class="form-inline" role="form">
                                            <div class="form-group">
                                                <span>تاریخ اجرای حکم در این واحد :</span>
                                                <asp:RequiredFieldValidator Text="*" ValidationGroup="vg" ErrorMessage="درج تاریخ اجرای حکم الزامی می باشد" ControlToValidate="txtDateRunHokmNew" ForeColor="Red" Display="Dynamic" runat="server" />
                                                <asp:RegularExpressionValidator Text="*" ValidationGroup="vg" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateRunHokmNew" runat="server" />
                                                <asp:TextBox ID="txtDateRunHokmNew" runat="server" CssClass="form-control form-inline pcal" />
                                            </div>
                                            <div class="checkbox">
                                                <span>مورد تایید می باشد.</span>
                                            </div>
                                        </div>
                                    </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <hr />
                        <div class="left col-md-12">
                            <div class="col-md-1">
                                <asp:Button ID="btnApprove" ValidationGroup="vg" CommandName="approve" OnClick="btnApprove_Click" CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-success" Text="تایید" OnClientClick="confirmAspButton(this); return false;" runat="server" />
                            </div>
                            <div class="col-md-1">

                                <asp:Button ID="btnDeny" CommandName="deny" OnClick="btnDeny_Click" CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-danger" OnClientClick="denyAspButton(this); return false;" Text="رد" runat="server" />
                            </div>
                            
                            <div class="col-md-1">

                                <asp:Button Visible="false" ID="btnSendToPending" CommandName="toPending" OnClick="btnSendToPending_Click" CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-warning" OnClientClick="toPending(this); return false;" Text="بازگشت به کارتابل در حال بررسی" runat="server" />
                            </div>
                        </div>
                        <div class="row" dir="rtl">
                            <div class="col-md-12" dir="rtl">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="لطفا خطاهای زیر را رفع نمایید :" ForeColor="Red" ValidationGroup="vg" />
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
                <asp:UpdateProgress ID="uppProfReq" runat="server" AssociatedUpdatePanelID="upProf">
                    <ProgressTemplate>
                        <div id="wait" class="updateProgress" dir="rtl">
                            <div class="center">
                                <img alt="loading" src="Image/animatedEllipse.gif" />
                                <span>درحال بارگذاری...</span>
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindow2" Width="400" Height="350" runat="server" CenterIfModal="false">
        <ContentTemplate>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="container" dir="rtl">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h4>علت رد درخواست ویرایش</h4>
                            </div>
                            <div class="panel-body">
                                <p>متن پیام:</p>
                                <div class="row text-center">
                                    <div class=" col-sm-12">
                                        <asp:TextBox ID="txtMessage" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server" />
                                    </div>
                                    <asp:Button ID="btnSubmitMessage" OnClientClick="target='_blank';" CssClass="btn btn-info" OnClick="btnSubmitMessage_Click" Text="ثبت و ادامه" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <script type="text/javascript">
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    if (prm != null) {
                        prm.add_pageLoaded(function (sender, e) {
                            SetDatePicker();
                        })
                    };

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
                                }
                            }
                        }
                    }

                    function CloseRadWindow1() {
                        var win = $find('<%=RadWindow1.ClientID %>');
            win.Close();
            refreshGrid();
        }

        function CloseRadWindow2() {
            var win = $find('<%=RadWindow2.ClientID %>');
            CloseRadWindow1();
            win.Close();
            refreshGrid();
        }
    </script>
    <%--<script type="text/javascript">
$(document).ready(function() {    
    $("#txtMessage").change(function () {
        if ($("#btnSubmitMessage").text!="")
            $("#btnSubmitMessage").removeAttr("disabled");
        else
            $("#btnSubmitMessage").attr("disabled");
    });
});
</script>--%>

    <style>
        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 200px;
            background-color: White;
            border-radius: 5px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 46px;
                width: 46px;
            }

        .updateProgress {
            height: 100%;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0,0,0,0.7);
        }
    </style>
    <uc1:AccessControl runat="server" ID="AccessControl" />

</asp:Content>
