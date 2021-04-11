<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="MergeClass.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.MergeClass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../css/js-persian-cal.css" rel="stylesheet" />
    <script src="../js/js-persian-cal.min.js"></script>
    <style>
        .marginTop {
            margin-top: 20px;
        }

        .marginTop5 {
            margin-top: 5px;
        }

        .margin5 {
            margin-bottom: 5px;
            margin-right: 5px;
            margin-left: 5px;
        }

        .marginTop10 {
            margin-top: 10px;
        }

        .marginRight {
            margin-right: 10px;
        }

        .marginRight60 {
            margin-right: 60px;
        }

        .marginRight34 {
            margin-right: 34px;
        }

        .paddingRight {
            padding-right: 55px;
        }

        .paddingbottom10 {
            padding-bottom: 10px;
        }

        .paddingRight7 {
            padding-right: 7px;
        }

        .imgDescription {
            position: absolute;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: -100px;
            background: rgba(29, 106, 154, 0.9);
            color: #fff;
            height: 500px;
            width: 200px;
            z-index: 1000;
            visibility: hidden;
            opacity: 0;
            /*remove comment if you want a gradual transition between states
  -webkit-transition: visibility opacity 0.2s;
  */
        }

        .imgWrap:hover .imgDescriptionterm, .imgWrap:hover .imgDescription {
            visibility: visible;
            opacity: 1;
        }
    </style>
    <script type="text/javascript">
        function confirmAspButton(button) {
            if (Page_ClientValidate("merge")) {
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="font-family: 'B Yekan'; font-size: medium;">
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <div class="row">
            <div class="col-md-3">
                کد کلاس:
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3">
                نام کلاس:
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnSearch" Text="جستجو" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                <asp:Button ID="btnCancel" Text="لغو" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                <asp:Button ID="btnExcel" Text="تبدیل به فایل اکسل" runat="server" CssClass="btn btn-success" OnClick="btnExcel_Click" />
            </div>
        </div>
        <br />
        <div id="divclasses" runat="server" class="row" visible="false">
            <div class="col-md-8">
                <asp:UpdatePanel ID="upnlInfo" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid ID="grdInfo" runat="server" AllowPaging="true" PageSize="15" AllowFilteringByColumn="true" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin" OnItemCommand="grdInfo_ItemCommand" OnNeedDataSource="grdInfo_NeedDataSource" OnItemDataBound="grdInfo_ItemDataBound">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" Text="<%#grdInfo.CurrentPageIndex * grdInfo.PageSize + Container.ItemIndex + 1 %>" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="کد کلاس" DataField="codeclass" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="کد درس" DataField="codedars" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="نام  درس" DataField="namedars" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="کد ادغام" DataField="Merge_Code" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="ساعت برگزاری" DataField="saatklass" AllowFiltering="false"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="نام استاد" DataField="MergeOstad" AllowFiltering="true"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="افزودن" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="افزودن" CommandName="Add" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"codeclass") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="حذف ادغام" AllowFiltering="false">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="حذف" OnClientClick="confirmAspButton1(this); return false;" CommandName="del" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"codeclass") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdInfo" EventName="ItemCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-4">
                <div id="divtitle" visible="false" runat="server" class="row" style="border: solid; border-color: #009a12; font-size: medium; color: black">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-6" style="margin-right: 10px;">
                        لیست کلاسهای ادغامی
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <asp:UpdatePanel ID="upnlList" runat="server">
                                <ContentTemplate>
                                    <telerik:RadGrid ID="grdList" runat="server" MasterTableView-ShowHeadersWhenNoRecords="true" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin" OnItemCommand="grdList_ItemCommand" OnNeedDataSource="grdList_NeedDataSource">
                                        <MasterTableView>
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="ردیف">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" Text="<%# Container.ItemIndex + 1 %>" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="کد کلاس" DataField="ClassCode"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="نام  درس" DataField="CourseName"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="حذف">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="حذف" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"classCode") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grdList" EventName="ItemCommand" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <div class="row marginTop10" style="margin-right: 41px;">
                                <div class="col-md-12 ">
                                    کد درس:
                                <asp:RequiredFieldValidator ID="rfvClassCode" runat="server" Text="*" ErrorMessage="کد درس را وارد کنید" Font-Bold="true" Font-Size="large" ControlToValidate="txtCodeDars" ForeColor="Red" ValidationGroup="merge"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtCodeDars" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row marginTop10" style="margin-right: 41px;">
                                <div class="col-md-12 ">
                                    کد ادغام:
                                <asp:RequiredFieldValidator ID="rfvMerge" runat="server" Text="*" ErrorMessage="کد ادغام را وارد کنید" Font-Bold="true" Font-Size="large" ControlToValidate="txtmergeCode" ForeColor="Red" ValidationGroup="merge"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtmergeCode" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row marginTop10">
                                <div class="col-md-12 ">
                                    تاریخ اولین جلسه:
                                <asp:RequiredFieldValidator ID="rfvFirstSession" runat="server" Text="*" Font-Bold="true" Font-Size="large" ErrorMessage="تاریخ اولین جلسه را وارد کنید" ControlToValidate="txtFirstSession" ForeColor="Red" ValidationGroup="merge"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtFirstSession" runat="server"></asp:TextBox>
                                    <script>
                                        var objCal1 = new AMIB.persianCalendar('<%=txtFirstSession.ClientID%>',
                                            { extraInputID: '<%=txtFirstSession.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                                    </script>
                                </div>
                            </div>
                            <div class="row marginTop10">
                                <div class="col-md-12 " style="margin-right: 14px;">
                                    تعداد جلسات:
                                <asp:RequiredFieldValidator ID="rfvSessionCount" runat="server" Text="*" Font-Bold="true" Font-Size="large" ControlToValidate="txtSessionCount" ErrorMessage="تعداد جلسات را وارد کنید" ForeColor="Red" ValidationGroup="merge"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="rvSessionCount" runat="server" ControlToValidate="txtSessionCount" ValidationGroup="merge" Text="*" ErrorMessage="تعداد جلسات باید بین 1 تا 20 باشد" Font-Bold="true" Font-Size="large" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtSessionCount" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row marginTop10">
                                <div class="col-md-12 " style="margin-right: 50px;">
                                    نام استاد:
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" Font-Bold="true" Font-Size="large" ControlToValidate="txtProf" ErrorMessage="نام استاد را انتخاب کنید" ForeColor="Red" ValidationGroup="merge"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtProf" runat="server" ReadOnly="true"></asp:TextBox>
                                    <asp:Button ID="btnProf" runat="server" Text="انتخاب" CssClass="btn btn-info" OnClick="btnProf_Click" />
                                </div>
                            </div>
                            <div class="row marginTop10">
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-10">
                                    <asp:UpdatePanel ID="pnlDay" runat="server">
                                        <ContentTemplate>
                                            روز تشکیل کلاس:
                                        <asp:RangeValidator ID="rfvDay" runat="server" Text="*" Font-Bold="true" Font-Size="large" ControlToValidate="drpDay" MinimumValue="1" MaximumValue="7" ErrorMessage="روز هفته را انتخاب کنید" ForeColor="Red" ValidationGroup="merge"></asp:RangeValidator>
                                            <asp:DropDownList ID="drpDay" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpDay_SelectedIndexChanged">
                                                <asp:ListItem Text="--انتخاب کنید--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="شنبه" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="یکشنبه" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="دوشنبه" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="سه شنبه" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="چهارشنبه" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="پنجشنبه" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="جمعه" Value="7"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="drpDay" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                            <div class="row marginTop10">
                                <div class="col-md-6">
                                    ساعت شروع:
                                <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                    <telerik:RadTimePicker ID="RadTimePicker1" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                        <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                        </TimeView>
                                        <DateInput ID="Stime" runat="server">
                                            <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                        </DateInput>
                                    </telerik:RadTimePicker>
                                </div>
                                <div class="col-md-6">
                                    ساعت پایان:
                                 <asp:RequiredFieldValidator ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
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
                            <div class="row marginTop10">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnMerge" runat="server" Text="ثبت" OnClientClick="confirmAspButton(this); return false;" CssClass="btn btn-success marginRight marginTop5" ValidationGroup="merge" OnClick="btnMerge_Click" />
                                </div>
                                <div class="col-md-5">
                                </div>
                            </div>
                            <div class="row marginTop10">
                                <asp:ValidationSummary ID="vlds" runat="server" ValidationGroup="merge" ForeColor="Red" HeaderText="لطفا به موارد زیر توجه کنید" ShowSummary="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <telerik:RadWindow runat="server" MinHeight="300px" MinWidth="950px" MaxHeight="300px" MaxWidth="950px" Width="950px" Height="300px" ID="radConfirm" VisibleOnPageLoad="false">
            <ContentTemplate>
                <div dir="rtl">
                    |<div class="row bg-danger" style="padding: 5px">
                        <div class="col-md-5"></div>
                        <div class="col-md-4">
                            <h5>تایید ادغام کلاس</h5>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <asp:Label ID="Label4" Font-Names="tahoma" Font-Size="Medium" Text="کد ادغام وارد شده قبلا ثبت شده است آیا میخواهید کلاس های وارد شده را با کلاس های قبلی این کد ادغام کنید؟ " runat="server" />
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <asp:Label ID="Label1" Font-Names="tahoma" ForeColor="Red" Font-Size="Small" Text="توجه داشته باشید که تاریخ اولین جلسه، تعداد جلسات و نام استاد برابر با کلاس های قبلی این کد ثبت می شود" runat="server" />
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                    <div>
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-5">
                                <asp:Button ID="btnConfirmCancel" runat="server" Text="لغو" CssClass="btn btn-danger" OnClick="btnConfirmCancel_Click" />
                                <asp:Button ID="btnConfirmOk" runat="server" Text="تایید" CssClass="btn btn-success" OnClick="btnConfirmOk_Click" />
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                    </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindow runat="server" ID="rdwProf" VisibleOnPageLoad="false" RenderMode="Lightweight" MinHeight="650px" Height="650px" MinWidth="1000px" Width="1000px">
            <ContentTemplate>
                <div class="container-fluid" dir="rtl">
                    <div class="row bg-info" style="border: solid; border-color: cornflowerblue; padding: 5px">
                        <div class="col-md-5"></div>
                        <div class="col-md-4">
                            <h5>جستجو استاد</h5>
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadGrid ID="grdProf" runat="server" AllowPaging="true" PageSize="10" MasterTableView-ShowHeadersWhenNoRecords="true" AllowFilteringByColumn="true" AutoGenerateColumns="false" EnableEmbeddedSkins="False" Skin="MyCustomSkin" 
                                OnNeedDataSource="grdProf_NeedDataSource" OnItemCommand="grdProf_ItemCommand">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" Text="<%#grdProf.CurrentPageIndex * grdProf.PageSize + Container.ItemIndex + 1 %>" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="کد استاد" DataField="code_ostad" AllowFiltering="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="نام" DataField="name" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="نام خانوادگی" DataField="family" AllowFiltering="true"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="نام پدر" DataField="namep" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="کد ملی" DataField="idd_meli" AllowFiltering="false"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="عملیات" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Button ID="btnChoose" runat="server" CssClass="btn btn-success" Text="انتخاب" CommandName="Choose" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"code_ostad") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>
</asp:Content>
