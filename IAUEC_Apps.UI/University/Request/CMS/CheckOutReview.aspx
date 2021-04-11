<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master"
    AutoEventWireup="true" CodeBehind="CheckOutReview.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutReview" %>

<%@ Import Namespace="IAUEC_Apps.UI.University.GraduateAffair.CMS" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <script type="text/javascript">
        function openCheckoutReasonFrequency() {
            $('#rwReasonFrequency').modal('show');
        }</script>
    <style>
        .radWinForStu {
            min-width: 400px !important;
            background-color: aliceblue;
            color: #fff;
            font-size: large;
            text-align: center;
        }
        /*th {
            text-align: center;
            border: 1px solid #808080;
            font-family: 'B Titr',Arial,'B Yekan' !important;
        }

        #ContentPlaceHolder1_grdCheckOutList td {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
            color: black;
        }

        .table-condensed > thead > tr > th, .table-condensed > tbody > tr > th, .table-condensed > tfoot > tr > th, .table-condensed > thead > tr > td, .table-condensed > tbody > tr > td, .table-condensed > tfoot > tr > td {
            padding: 3px !important;
        }

        .radfont, .rgHeader {
            font-family: 'B Titr' !important;
            font-weight: bold;
        }

        .rwContentRow {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
        }*/
        .imgWrap {
            position: relative;
        }

        .imgDescriptionterm {
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(29, 106, 154, 0.72);
            color: #fff;
            height: 150px;
            width: 200px;
            visibility: hidden;
            opacity: 0;
            z-index: 100;
            /*remove comment if you want a gradual transition between states
  -webkit-transition: visibility opacity 0.2s;
  */
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

    <style type="text/css">
        .ProgressTemplatemodal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=80);
            opacity: 0.8;
            -moz-opacity: 0.8;
        }

        .ProgressTemplatecenter {
            z-index: 1000;
            margin: 300px auto;
            /*padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;*/
        }

            .ProgressTemplatecenter img {
                height: 128px;
                width: 128px;
            }

        .pnlUserMessage {
            position: fixed;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(128, 128, 128, 0.8);
            z-index: 999;
            direction: rtl;
            padding: 15px;
        }

            .pnlUserMessage > div {
                width: 50%;
                height: 50vh;
                position: relative;
                top: 0;
                right: 0;
                left: 0;
                bottom: 0;
                margin: auto;
                background: rgb(255, 255, 234);
                border: 3px solid #fff;
                border-radius: 5px;
            }

                .pnlUserMessage > div > input[type="submit"] {
                    position: absolute;
                    left: -15px;
                    top: -15px;
                    background: #ce3e3e;
                    border: none;
                    border-radius: 15px;
                    display: block;
                    width: 30px;
                    height: 30px;
                    color: #fff;
                    font-family: tahoma;
                }
    </style>



    <script type="text/javascript">
        function openModal() {
                $('#exampleModal').modal('show');
            }
    </script>


    <script type="text/javascript">
        function confirmAspButton(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید که می خواهید این درخواست را تایید کنید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }

            function confirmAspButton2(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا معدل این دانشجو بررسی شده است؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }

            function confirmAspButton1(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید که می خواهید این سطر را پاک کنید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }
            function sure(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید که می خواهید انتخاب واحد این دانشجو را حذف کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }

            function validator1() {
                var txtnumber = $find("<%=txtNumber.ClientID%>");
                args.IsValid = upload.length != 0;
            }

            function grdReload(sender, args) {
                refresgGrid();
            }

            function openrwMessage() {
                var oManager = GetRadWindow().get_windowManager();
                setTimeout(function () {
                    oManager.open("CheckOutReview.aspx", "rwMessage");
                }, 0);
            }
            function openrwReasonFrequency() {
                debugger;
                var oManager = GetRadWindow().get_windowManager();
                setTimeout(function () {
                    oManager.open("CheckOutReview.aspx", "rwReasonFrequency");
                }, 0);
            }

            function winDetailClosing(sender, e) {
                e.set_cancel(true);
            }
            function Close(args) {
                GetRadWindow().close();
            }
    </script>
    <script type="text/javascript" id="telerikClientEvents1">

        function rwMessage_Close(sender, args) {
                document.getElementById("ctl00_ContentPlaceHolder1_rwMessage_C_txtMsg").innerHTML = "";
            }
    </script>
    <script type="text/javascript" id="telerikClientEvents2">

        function rwReasonFrequency_Close(sender, args) {
                document.getElementById("ctl00_ContentPlaceHolder1_rwReasonFrequency_C_txtMsg").innerHTML = "";
            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">

    <h3>فرم تسویه حساب غیر حضوری دانشجویان</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function refresgGrid() {
                document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
            }
    </script>
    <asp:UpdateProgress ID="updateprogress1" runat="server" AssociatedUpdatePanelID="pnl">
        <ProgressTemplate>
            <div class="progresstemplatemodal" style="margin: auto; text-align: center">
                <div class="progresstemplatecenter">
                    <img alt="" src="../../theme/images/loader.gif" />
                    <br />
                    <br />
                    <b style="text-align: center; font-size: xx-large;">...در حال بارگذاری</b>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <telerik:RadGrid ID="rdGridExcel" Visible="false" runat="server" AllowFilteringByColumn="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False" Skin="MyCustomSkin"
        OnNeedDataSource="rdGridExcel_NeedDataSource"
        EnableLinqExpressions="False" CellSpacing="-1" GridLines="Both"
        GroupPanelPosition="Top" SortingSettings-EnableSkinSortStyles="false" OnExcelMLWorkBookCreated="rdGridExcel_ExcelMLWorkBookCreated">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn HeaderText="نوع درخواست" DataField="RequestTypeName"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="شماره درخواست" DataField="StudentRequestID"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="تاریخ  ثبت درخواست" DataField="CreateDate"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StCode"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="کد ملی" DataField="StCode"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="نام دانشجو" DataField="name"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="تاریخ دفاع" DataField="Def_Date"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="تاریخ ورود به کارتابل" DataField="LogDate"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="نام رشته" DataField="nameresh"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="نام دانشکده" DataField="namedanesh"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="تاریخ آپلود پایان نامه" DataField="dateUploadThesis"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="اولین نیمسال ثبت نام" DataField="termVorood"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="آخرین نیمسال ثبت نام" DataField="lastnimsal"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="شماره تماس" DataField="mobile"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="پیام ها" DataField="msg"></telerik:GridBoundColumn>

            </Columns>

        </MasterTableView>
        <FilterMenu EnableEmbeddedSkins="False">
        </FilterMenu>
        <HeaderContextMenu EnableEmbeddedSkins="False">
        </HeaderContextMenu>
    </telerik:RadGrid>
    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <div class="col-md-12 bg-danger" dir="rtl" style="padding: 1%; color: #CC0000">
                <p>
                    <img src="../../Theme/images/Userstatus.png" />با نگه داشتن اشاره گر ماوس بر روی این آیکون اولین و آخرین نیمسال ثبت نام و وضعیت دانشجو نمایش داده می شود.
                </p>
                <p>
                    <img src="../../Theme/images/mail-t.png" height="32" width="32" />در صورتیکه دانشجو برای درخواست خود پیام گذاشته باشد این آیکون نمایش داده می شود و با قرار گرفتن اشاره گر ماوس بر روی آیکون پیام نمایش داده می شود.
                </p>
                <p>
                    <img src="../../Theme/images/sendmail.png" height="32" width="32" />برای ارسال پیام به دانشجو بر روی این گزینه کلیک نمایید<span style="color: #000; font-size: 16px">(تعداد ارسال پیام نامحدود می باشد و برای مشاهده پیام هایی که ارسال نموده اید بر روی گزینه تاریخچه کلیک نمایید)</span>.
                </p>
                <p>
                    <img src="../../Theme/images/History.png" height="32" width="32" />با کلیک بر روی این گزینه تاریخچه درخواست نمایش داده می شود.
                </p>
                <p id="malidesc" runat="server" visible="false">
                    <img src="../../Theme/images/refah.png" height="32" width="32" />جهت درج وضعیت بدهی بر روی این گزینه کلیک نمایید
                </p>
                <p id="naghsdesc" runat="server" visible="false">
                    <img src="../../Theme/images/faileddoc.png" height="32" width="32" />جهت درج نقص بر روی این گزینه کلیک نمایید
                </p>
                <p id="hozur" runat="server" visible="false">
                    <img src="../../Theme/images/hour.png" height="32" width="32" />با نگه داشتن اشاره گر ماوس بر روی این آیکون مدت زمان حضور دانشجو نمایش داده می شود
                </p>
                <p id="entekhabvahed" runat="server" visible="false">
                    <img src="../../Theme/images/date.png" height="32" width="32" />با نگه داشتن اشاره گر ماوس بر روی این آیکون تاریخ انتخاب واحد نمایش داده می شود
                </p>
                <p>
                    <img src="../../Theme/images/tasvie-new.png" height="32" width="32" />برای دانشجویانی که بررسی نشده اند این آیکون نمایش داده می شود.
                </p>
                <p>
                    <img src="../../Theme/images/faileddoc.png" height="32" width="32" />برای دانشجویانی که نقص داشته اند این آیکون نمایش داده می شود.
                </p>
                <p style="color: #000"><span style="color: #CC0000">*</span>دانشجویانی که وضعیت نظام وظیفه آنان مشمول است رنگ زمینه سطر درخواستشان بنفش است.</p>
                <p style="color: #000"><span style="color: #CC0000">*</span>دانشجویانی که وام بلند مدت وزارت علوم را تسویه نکرده اند، رنگ زمینه سطر درخواستشان نارنجی است.</p>
                <p style="color: #000"><span style="color: #CC0000">*</span>با کلیک بر روی سر ستون هایی که به رنگ مشکی وجود دارند، میتوانید جدول را بر اساس آنها مرتب کنید.</p>

            </div>
            <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass=" hidden" Text="refreshGrid" runat="server" />
            <% %>

            <asp:Panel ID="pnlUserMessage" runat="server" CssClass="pnlUserMessage" Visible="false">
                <div>

                    <asp:Label runat="server" ID="lblUserMessage"></asp:Label>
                    <br />

                    <asp:Button runat="server" ID="btnCloseMsg" Text="X" OnClick="btnCloseMsg_Click" />

                </div>
            </asp:Panel>


            <div id="parent" runat="server" class="container text-right hidden-print" dir="rtl" style="font-size: larger">
                <asp:Label ID="lblKartabl" Text="انتخاب کارتابل:" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                <asp:DropDownList ID="drpUserRoles" runat="server" CssClass=" dropdown text-right" Visible="false" Enabled="false" OnSelectedIndexChanged="drpUserRoles_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>

                <asp:Label ID="lblThesisType" Text="وضعیت پایان نامه:" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlThesisType" runat="server" CssClass=" dropdown text-right" Visible="false" Enabled="true" OnSelectedIndexChanged="ddlThesisType_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="بدون پایان نامه" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="پایان نامه رد شده" Value="0"></asp:ListItem>
                    <asp:ListItem Text="فایل جهت بررسی" Value="1"></asp:ListItem>
                </asp:DropDownList>
                <asp:ImageButton ID="btnDlExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png" OnClick="btnDlExcel_Click" />

                <asp:Label ID="lblMessage" Text="" runat="server" ForeColor="Red" Visible="false"></asp:Label>

                <telerik:RadGrid ID="grd_CheckOutList" runat="server" AllowFilteringByColumn="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False" Skin="MyCustomSkin"
                    OnItemCommand="grd_CheckOutList_ItemCommand" OnItemDataBound="grd_CheckOutList_ItemDataBound" OnNeedDataSource="grd_CheckOutList_NeedDataSource"
                    EnableLinqExpressions="False" AllowPaging="True" PageSize="25" CellSpacing="-1" AllowSorting="true" GridLines="Both"
                    GroupPanelPosition="Top" SortingSettings-EnableSkinSortStyles="false">
                    <%--<GroupingSettings CollapseAllTooltip="Collapse all groups" />--%>

                    <MasterTableView NoMasterRecordsText="داده ای جهت نمایش وجود ندارد" NoDetailRecordsText="داده ای جهت نمایش وجود ندارد">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" Text="<%# Container.ItemIndex + 1 %>" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Image ID="imgSenMessage" runat="server" Visible="false" />
                                    <asp:Image ID="imgHasNaghs" runat="server" Visible="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="RequestTypeName" AllowSorting="true" HeaderText="نوع"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn DataField="RequestTypeName" AllowFiltering="false">

                                <ItemTemplate>
                                    <asp:Image ID="imgTypeRequest" runat="server" AlternateText='نوع درخواست:<%#Eval("RequestTypeName") %>' />

                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridBoundColumn HeaderText="شماره درخواست" DataField="StudentRequestID" AllowFiltering="false"></telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="شماره درخواست" AllowFiltering="true">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblReqId" Text='<%# Eval("StudentRequestID") %>'></asp:Label>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="تاریخ  ثبت درخواست" DataField="CreateDate" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StCode"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="کد ملی" DataField="idd_meli"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="نام دانشجو" DataField="name" AllowFiltering="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="تاریخ دفاع" DataField="Def_Date" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="تاریخ ورود به کارتابل" DataField="LogDate" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="نقص" DataField="HasNaghs" Visible="false" AllowFiltering="false"></telerik:GridBoundColumn>

                            <telerik:GridBoundColumn HeaderText="نام دانشکده" DataField="namedanesh" AllowFiltering="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="تاریخ آپلود پایان نامه" DataField="dateUploadThesis" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="thesisfile" Visible="false"></telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn HeaderText="سایر اطلاعات" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="idd_meli" runat="server" Value='<%#Eval("idd_meli") %>' />
                                    <asp:HiddenField ID="mobile" runat="server" Value='<%#Eval("mobile") %>' />
                                    <asp:HiddenField ID="naghsHidden" runat="server" Value='<%#Eval("HasNaghs") %>' />
                                    <asp:Label ID="lblNezam" Text="" runat="server" />
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="margin-left: 7%">
                                            <div class="imgWrap">
                                                <asp:Image ID="imgtermVorood" runat="server" ImageUrl="~/University/Theme/images/Userstatus.png" Width="26" Height="26" />
                                                <p class="imgDescriptionterm">
                                                    نام رشته :<%#Eval("nameresh") %><br />
                                                    اولین نیمسال ثبت نام:<%#Eval("termVorood") %><br />
                                                    آخرین نیمسال ثبت نام:<%#Eval("lastnimsal") %><br />
                                                    وضعیت قبلی دانشجو:<%#Eval("Note") %><br />
                                                    کد بایگانی:<%#Eval("codebayegan") %><br />
                                                    واحد صادر کننده مدرک:<%# (Eval("IdVahedSodoor").ToString()).VahedSodoor() %>
                                                </p>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-md-2" style="margin-left: 2%">
                                            <div class="imgWrap">
                                                <asp:ImageButton ID="btnDownloadThesis" title="دانلود پایان نامه"
                                                    AlternateText="دانلود پایان نامه" Visible="false" runat="server" CommandName="dlThesis"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>'
                                                    ImageUrl="~/University/Theme/images/downloadThesis.png />" Width="40" Height="40" />
                                            </div>
                                            <div class="imgWrap">
                                                <asp:ImageButton ID="btnDownloadThesisDoc" title="دانلود پایان نامه ورد"
                                                    AlternateText="دانلود پایان نامه" Visible="true" runat="server" CommandName="dlThesisDoc"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>'
                                                    ImageUrl="~/University/Theme/images/dlThesis_Doc.png" Width="40" Height="40" />
                                            </div>
                                            <div class="imgWrap">
                                                <asp:ImageButton ID="btnDownloadThesisPDF" title="دانلود پایان نامه پی دی اف"
                                                    AlternateText="دانلود پایان نامه" Visible="true" runat="server" CommandName="dlThesisPDF"
                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>'
                                                    ImageUrl="~/University/Theme/images/dlThesis_PDF.png" Width="40" Height="40" />
                                            </div>
                                        </div>

                                        <div class="col-md-2" style="margin-left: 2%">
                                            <div class="imgWrap">
                                                <asp:ImageButton ID="imgEntekhabVahedDate" runat="server" ImageUrl="~/University/Theme/images/date.png" Visible="false" Width="26" Height="26" />
                                                <p class="imgDescriptionterm">تاریخ انتخاب واحد:<%#Eval("datesabtv") %></p>
                                            </div>
                                        </div>
                                        <div class="col-md-2" style="margin-left: 2%">
                                            <div class="imgWrap">
                                                <asp:ImageButton ID="imgHozoorHour" runat="server" ImageUrl="~/University/Theme/images/hour.png" Visible="false" Width="26" Height="26" />
                                                <asp:Label ID="lblhour" runat="server" CssClass="imgDescriptionterm"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="عملیات" AllowFiltering="false">
                                <ItemTemplate>

                                    <asp:ImageButton ID="btnApprove" ToolTip="تایید" AlternateText="تایید" runat="server" CommandName="approve" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' OnClientClick="confirmAspButton(this); return false;" ImageUrl="~/University/Theme/images/tick.png" Width="40" Height="40" />

                                    <asp:ImageButton ID="btnSendMsg" ToolTip="ارسال پیام" Text="ارسال پیام" runat="server" CommandName="msg" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' ImageUrl="~/University/Theme/images/sendmail.png" Width="40" Height="40" />
                                    <div class="imgWrap">
                                        <asp:ImageButton ID="Msg" ToolTip="نمایش پیام" runat="server" AlternateText='<%#Eval("StudentMessage") %>'
                                            ImageUrl="~/University/Theme/images/mail-t.png" Visible="false" Width="40" Height="40"
                                            CommandName="StudentMessage" CommandArgument='<%#Eval("studentMessage") %>' />
                                        <%--imgDescription--%>
                                    </div>
                                    <asp:ImageButton ID="btnRefah" Visible="false" ToolTip="درج وضعیت" AlternateText="درج وضعیت" runat="server" CommandName="refah" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' ImageUrl="~/University/Theme/images/refah.png" Width="40" Height="40" />

                                    <asp:ImageButton ID="btnnaghs" Visible="false" ToolTip="درج نقص" AlternateText="درج نقص" runat="server" CommandName="naghs" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' ImageUrl="~/University/Theme/images/faileddoc.png" Width="40" Height="40" />
                                    <asp:ImageButton ID="btnReady" Visible="false" ToolTip="آمادگی برای ارسال" AlternateText="آمادگی برای ارسال" runat="server" CommandName="Ready" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' ImageUrl="~/University/Theme/images/Ready.png" Width="40" Height="40" />
                                    <asp:ImageButton ID="imgIsReady" Visible="false" ToolTip="آماده ارسال" AlternateText="آماده ارسال" runat="server" ImageUrl="~/University/Theme/images/IsReady.png" Width="40" Height="40" />

                                    <%--<asp:ImageButton ID="btnTaeedMashmool" AlternateText="تایید مشمول" Enabled="false" Visible="false" runat="server" CommandName="mashmoolApprove" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' ImageUrl="~/University/Theme/images/mashmul.png" Width="40" Height="40" />--%>
                                    <asp:ImageButton ID="btnHistory" ToolTip="نمایش تاریخچه" AlternateText="تاریخچه" Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' ImageUrl="~/University/Theme/images/History.png" Width="40" Height="40" />
                                    <asp:ImageButton ID="btnDenyThesis" title="رد پایان نامه" ToolTip="رد پایان نامه"
                                        AlternateText="رد پایان نامه" Visible="true" runat="server" CommandName="denyThesis"
                                        CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>'
                                        ImageUrl="~/University/Theme/images/denyThesis.png" Width="40" Height="40" />
                                    <asp:ImageButton ID="btnShowCheckoutReason" runat="server" ImageUrl="~/University/Theme/images/checkout_reason.png" ToolTip="نمایش علت تسویه حساب" CommandName="checkoutReason" Width="40" Height="40" AlternateText="نمایش علت تسویه حساب" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' />
                                    <asp:HiddenField ID="hdnField" runat="server" Visible="false" Value='<%# Eval("RequestLogID") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="نوع درخواست" DataField="RequestTypeID" AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                <HeaderStyle CssClass="hidden"></HeaderStyle>

                                <ItemStyle CssClass="hidden"></ItemStyle>
                            </telerik:GridBoundColumn>
                        </Columns>

                        <EditFormSettings>
                            <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                            </EditColumn>
                        </EditFormSettings>

                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                </telerik:RadGrid>
            </div>

            <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            </telerik:RadWindowManager>

            <telerik:RadWindow ID="winStudentMessage" runat="server" AutoSize="true" Modal="true" Font-Names="B Titr">
                <ContentTemplate>

                    <div style="width: 480px; height: 200px; left: 0; right: 0; margin: auto; text-align: center;">
                        <div>
                            <h2 class="bg-primary"><span style="margin: 1%">پیام دانشجو</span></h2>
                        </div>

                        <div style="text-align: right; padding: 1%;">
                            <asp:Label runat="server" ID="lblMsgStudent" Font-Size="Larger" />
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow ID="RadWindow1" runat="server" Width="1146" OnClientClose="grdReload" AutoSize="true" AutoSizeBehaviors="HeightProportional" Modal="true" CssClass="radfont" EnableEmbeddedBaseStylesheet="false" EnableEmbeddedSkins="false" Font-Names="B Titr">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div dir="rtl" class="container">
                                <h4>وضعیت دانشجو</h4>
                                <div>
                                    <div class="row alert alert-info">
                                        <div class="col-sm-6">
                                            <strong>نام :</strong>
                                            <asp:Label ID="lblStName" ViewStateMode="Enabled" runat="server" />
                                            &nbsp&nbsp&nbsp&nbsp&nbsp
                                            <strong>شماره دانشجویی :</strong>
                                            <asp:Label ID="lblStCode" Text="" runat="server" />
                                            &nbsp&nbsp&nbsp&nbsp&nbsp
                                    <br></br>
                                            <strong>کد ملی :</strong>
                                            <asp:Label ID="lbl_meli" Text="" runat="server" />
                                            &nbsp&nbsp&nbsp&nbsp&nbsp
                                       <strong>شماره موبایل :</strong>
                                            <asp:Label ID="lbl_mobile" Text="" runat="server" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnDeleteTerm" Text="حذف انتخاب واحد دانشجو" runat="server" OnClick="btnDeleteTerm_Click" CssClass="btn btn-danger" OnClientClick="sure(this); return false;" Visible="false" />
                                            <asp:Button ID="btnShowLastMaghta" Text="نمایش اطلاعات مقطع پیشین" runat="server" OnClick="btnShowLastMaghta_Click" CssClass="btn btn-danger" Visible="false" />
                                        </div>
                                        <div id="dvBedehiSida" runat="server" visible="false" class="col-sm-6">
                                            <strong>مبلغ بدهی/بستانکاری در سیدا :</strong>
                                            <asp:Label ID="lblBedehiSida" runat="server" DataFormatString="{0:N0}ريال" />
                                        </div>
                                        <div id="dvNewStCode" runat="server" class="col-sm-6" visible="false">
                                            <strong>شماره دانشجویی جدید :</strong>
                                            <asp:Label ID="lblNewStCode" runat="server" />
                                        </div>
                                    </div>
                                    <br />
                                    <h4>لیست بدهی های دانشجو </h4>

                                    <telerik:RadGrid ID="grdDebit" runat="server" RenderMode="Classic" BorderWidth="2px" CssClass="table table-bordered table-condensed table-striped text-center" MasterTableView-ShowHeadersWhenNoRecords="true"
                                        OnItemDataBound="grdDebit_ItemDataBound" OnItemCommand="grdDebit_ItemCommand" OnCancelCommand="grdDebit_CancelCommand" OnUpdateCommand="grdDebit_UpdateCommand" AutoGenerateColumns="false" EnableEmbeddedSkins="false">
                                        <HeaderStyle CssClass="bg-green text-center" HorizontalAlign="Center" />

                                        <MasterTableView BorderWidth="1px" DataKeyNames="RefID" ShowHeadersWhenNoRecords="true" EditMode="InPlace" BorderStyle="Solid" BorderColor="Black" CssClass="tabel table-bordered table-condensed">
                                            <ItemStyle BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center" />
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="نوع بدهی" ReadOnly="true">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpDebitType" runat="server" Enabled="false">
                                                            <asp:ListItem Text="وام وزارت علوم" Value="1" />
                                                            <asp:ListItem Text="چک برگشتی" Value="2" />
                                                            <asp:ListItem Text="وام ازدواج" Value="3" />
                                                            <asp:ListItem Text="وام تامین شهریه" Value="4" />
                                                            <asp:ListItem Text="وام کمک هزینه تحصیلی" Value="5" />
                                                            <asp:ListItem Text="وام مسکن" Value="6" />
                                                            <asp:ListItem Text="وام ضروری" Value="7" />
                                                            <asp:ListItem Text="بستانکاری" Value="8" />
                                                            <asp:ListItem Text="شهریه" Value="9" />
                                                            <asp:ListItem Text="وام بلند مدت" Value="10" />
                                                            <asp:ListItem Text="پرداخت بدهی در آموزشیار" Value="11" />
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="شماره / تعداد" DataField="DebitNumber" ReadOnly="true" />
                                                <%--<telerik:GridBoundColumn HeaderText="مجموع مبلغ" DataField="DebitAmount" DataFormatString="ريال{0:n0}" ReadOnly="true" />--%>
                                                <telerik:GridTemplateColumn HeaderText="مجموع مبلغ">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDebit" Text='<%# String.Format("{0:n0}ريال", Convert.ToDecimal(Eval("DebitAmount"))) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="TB_DebitAmount" Text='<%# Eval("DebitAmount") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="مبلغ برگه تسویه" DataField="TotalLoanAmount" DataType="System.Decimal" DataFormatString="{0:C2}" Visible="false" />
                                                <%--<telerik:GridTemplateColumn HeaderText="مبلغ برگه تسویه">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTotal" Text='<%# String.Format("{0:n0}ريال", Convert.ToInt32(Eval("TotalLoanAmount"))) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn HeaderText="شماره فیش / پیگیری" DataField="FishNumber" />
                                                <telerik:GridBoundColumn HeaderText="تاریخ فیش" DataField="FishDate" ItemStyle-CssClass="pcal" />
                                                <telerik:GridBoundColumn HeaderText="توضیحات" DataField="Note" />
                                                <telerik:GridEditCommandColumn HeaderText="ویرایش" EditText="ویرایش" ItemStyle-Font-Size="Medium" ItemStyle-ForeColor="#F0AD4E" UpdateText="ثبت" CancelText="لغو"></telerik:GridEditCommandColumn>

                                                <telerik:GridTemplateColumn HeaderText="حذف بدهی" ReadOnly="true">
                                                    <ItemTemplate>
                                                        <asp:Button CommandName="deleteDebit" ID="btnDelete" Text="حذف" CssClass="btn btn-danger" runat="server" OnClientClick="confirmAspButton1(this); return false;" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>

                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <br />
                                    <div>
                                        <h4>درج بدهی جدید</h4>
                                        <table class="table table-bordered table-condensed">
                                            <tr class="bg-primary">
                                                <th>نوع بدهی</th>
                                                <th>شماره / تعداد</th>
                                                <th>مجموع مبالغ</th>
                                                <th>مبلغ برگه تسویه</th>
                                                <th>شماره فیش</th>
                                                <th>تاریخ فیش</th>
                                                <th>توضیحات</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="drpDebitType" runat="server">
                                                        <asp:ListItem Text="وام وزارت علوم" Value="1" />
                                                        <asp:ListItem Text="چک برگشتی" Value="2" />
                                                        <asp:ListItem Text="وام ازدواج" Value="3" />
                                                        <asp:ListItem Text="وام تامین شهریه" Value="4" />
                                                        <asp:ListItem Text="وام کمک هزینه تحصیلی" Value="5" />
                                                        <asp:ListItem Text="وام مسکن" Value="6" />
                                                        <asp:ListItem Text="وام ضروری" Value="7" />
                                                        <asp:ListItem Text="بستانکاری" Value="8" />
                                                        <asp:ListItem Text="شهریه" Value="9" />
                                                        <asp:ListItem Text="وام بلند مدت" Value="10" />
                                                        <asp:ListItem Text="پرداخت بدهی در آموزشیار" Value="11" />

                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumber" runat="server" CssClass="form-control" />

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" />

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTasvieAmount" runat="server" CssClass="form-control" />

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFishNumber" runat="server" CssClass="form-control" />
                                                </td>
                                                <td class="pcal">
                                                    <asp:TextBox ID="txtFishDate" CssClass="form-control" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class=" text-center">
                                                    <asp:Button ID="btnDebitSubmit" Text="ثبت و بروزرسانی" runat="server" OnClick="btnDebitSubmit_Click" CssClass="btn btn-primary" />
                                                    <asp:Label ID="lblDebitError" ForeColor="Red" Text="" runat="server" />
                                                    <p class="text-primary"><span class="text-danger">*توجه:</span>در صورتی که نوع و شماره/تعداد بدهی با یکی از بدهی های موجود در لیست بالا یکی باشد بقیه موارد بدهی بروزرسانی می شوند.</p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <script type="text/javascript">
                                function GetRadWindow() {
                                        var oWindow = null; if (window.radWindow)
                                            oWindow = window.radWindow; else if (window.frameElement.radWindow)
                                            oWindow = window.frameElemenwt.radWindow; return oWindow;
                                    }
                            </script>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </telerik:RadWindow>

            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">

                <div class="modal-dialog" role="document" style="margin: 100px;">
                    <div class="modal-content" style="width: 1000px;">
                        <div class="modal-header" dir="rtl">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;
                                </span>
                            </button>
                            <div class="modal-header" dir="rtl">
                            </div>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                        </div>
                        <div class="modal-body">
                            <div dir="rtl" style="font-size: medium">
                                <div id="dd" runat="server"></div>
                                <div class="row">

                                    <div class="col-md-3">
                                        <div id="info1" runat="server"></div>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="info2" runat="server"></div>
                                    </div>
                                    <div class="col-md-3">
                                        <div id="info3" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" dir="rtl">
                            <div class="row" style="border: 1px solid rgba(59,131,255,0.9); background-color: rgba(59,131,255,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        نام کاربر
                                    </div>
                                    <div class="col-md-2">
                                        تاریخ
                                    </div>
                                    <div class="col-md-2">
                                        ساعت
                                    </div>
                                    <div class="col-md-3">
                                        وضعیت
                                    </div>
                                    <div class="col-md-3">
                                        توضیحات
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="border: 1px solid rgba(59,131,255,0.7); background-color: rgba(59,131,255,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">

                                <asp:ListView ID="lst_history" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-12" style="border-style: none none solid none; border-width: 0px 0px 1px 1px; border-color: rgba(59,131,255,0.7);">
                                            <div class="col-md-2">
                                                <%#Eval("Name") %>
                                            </div>
                                            <div class="col-md-2">
                                                <%#Eval("LogDate") %>
                                            </div>
                                            <div class="col-md-2">
                                                <%#Eval("LogTime") %>
                                            </div>
                                            <div class="col-md-3">
                                                <%#Eval("EventName") %>
                                            </div>
                                            <div class="col-md-3">
                                                <%#Eval("Description") %>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                    </div>
                </div>
            </div>

            <telerik:RadWindow runat="server" ID="RadWindowLastMaghta" VisibleOnPageLoad="false" Width="950" Height="550" Modal="true">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div dir="rtl">
                                <br />
                                <h4>مشخصات مقاطع تحصیلی قبلی</h4>
                                <asp:GridView ID="grdPastMaghtaInfo" runat="server" OnRowCommand="grdPastMaghtaInfo_RowCommand" OnRowDataBound="grdPastMaghtaInfo_RowDataBound" CssClass="table table-bordered table-striped table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="مقطعی برای این دانشو ثبت نشده است.">
                                    <HeaderStyle CssClass="bg-blue" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" Text="<%# Container.DataItemIndex + 1 %>" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="نام دانشگاه" DataField="UniName" />
                                        <asp:BoundField HeaderText="نام رشته" DataField="FieldName" />
                                        <asp:BoundField HeaderText="نوع دوره" DataField="UniType" />
                                        <asp:BoundField HeaderText="مقطع تحصیلی" DataField="maghta" />
                                        <asp:BoundField HeaderText="سال فراغت" DataField="FeraghatYear" />
                                        <asp:BoundField HeaderText="نمیسال فراغت" DataField="nimsal" />
                                        <asp:BoundField HeaderText="نوع مدرک" DataField="feraghatType" />
                                        <asp:BoundField HeaderText="وضعیت تسویه" DataField="CheckOutStatus" />
                                        <asp:BoundField HeaderText="تاریخ فراغت" DataField="FeraghatDate" />
                                        <asp:BoundField HeaderText="نظام وظیفه" DataField="Ismashmool" />
                                        <asp:BoundField HeaderText="مبلغ وام" DataField="LoanAmount" />
                                        <asp:TemplateField HeaderText="اسکن مدرک">
                                            <ItemTemplate>
                                                <asp:Button ID="btnGetScan" Text="دریافت" CssClass="btn btn-info" CommandName="download" CommandArgument='<%# Eval("MadrakURL") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table class="table table-bordered table-condensed table-striped">
                                    <tr class="bg-blue">
                                        <td colspan="4">
                                            <h5>آدرس</h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>استان :</td>
                                        <td>
                                            <asp:Label ID="lblProvince" Text="" runat="server" />
                                        </td>
                                        <td>شهر :</td>
                                        <td>
                                            <asp:Label ID="lblCity" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>خیابان :</td>
                                        <td>
                                            <asp:Label ID="lblStreet" Text="" runat="server" />
                                        </td>
                                        <td>کوچه :</td>
                                        <td>
                                            <asp:Label ID="lblAlley" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>پلاک :</td>
                                        <td>
                                            <asp:Label ID="lblPelak" Text="" runat="server" />
                                        </td>
                                        <td>کد پستی :</td>
                                        <td>
                                            <asp:Label ID="lblZipCode" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>شماره تلفن ثابت :</td>
                                        <td>
                                            <asp:Label ID="lblPhone" Text="" runat="server" />
                                        </td>
                                        <td>شماره تلفن همراه :</td>
                                        <td>
                                            <asp:Label ID="lblMobile" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>پست الکترونیکی :</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblEmail" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>شماره تلفن ثابت رابط :</td>
                                        <td>
                                            <asp:Label ID="lblRabetPhone" Text="" runat="server" />
                                        </td>
                                        <td>شماره تلفن همراه رابط :</td>
                                        <td>
                                            <asp:Label ID="lblRabetMobile" Text="" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Button ID="DownloadFile" CssClass="hidden" runat="server" />
                            <asp:HiddenField ID="hidClientField" runat="server" />
                            <%--<span id="imagelink" runat="server" style="display: none"></span>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow runat="server" ID="rwMessage">
                <ContentTemplate>
                    <div dir="rtl">
                        <div class="bg-green" style="padding: 5px">
                            <h4>ارسال پیام به دانشجو</h4>
                        </div>
                        <br />
                        <asp:Label ID="Label1" Text="متن پیام :" runat="server" />
                        <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" MaxLength="999" Width="100%"></asp:TextBox>
                        <br />
                        <div>
                            <asp:UpdatePanel ID="pnl2" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmitMsg" runat="server" Text="ثبت و ارسال پیام" CssClass="btn btn-success" OnClick="btnSubmitMsg_Click" />

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSubmitMsg" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>


            <telerik:RadWindow runat="server" ID="rwSendMsg_Thesis">
                <ContentTemplate>
                    <div dir="rtl">
                        <div class="bg-green" style="padding: 5px">
                            <h4>ارسال پیام به دانشجو</h4>
                        </div>
                        <br />
                        <asp:Label ID="Label3" Text="متن پیام :" runat="server" />
                        <asp:TextBox ID="txtMsgThesis" runat="server" TextMode="MultiLine" MaxLength="999" Width="100%"></asp:TextBox>
                        <br />
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnMsgThesis" runat="server" Text="ثبت و ارسال پیام" CssClass="btn btn-success" OnClick="btnMsgThesis_Click" />

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnMsgThesis" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow ID="RadWindowNaghs" runat="server" OnClientClose="grdReload" Width="950" Height="500" Modal="true">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div dir="rtl">
                                <div class="bg-primary" style="padding: 5px">
                                    <h4 style="font-family: 'B Titr'">نقص های پرونده دانشجو</h4>
                                </div>
                                <br />
                                <asp:Label ID="lblNaghsMessage" CssClass="text-danger" runat="server" />
                                <br />
                                <telerik:RadGrid ID="grdNaghs1" runat="server" OnItemDataBound="grdNaghs1_ItemDataBound" EnableEmbeddedSkins="false" AutoGenerateColumns="false" OnItemCommand="grdNaghs1_ItemCommand">
                                    <HeaderStyle CssClass="bg-blue" BorderWidth="1px" />
                                    <MasterTableView DataKeyNames="NaghsId" CssClass="table table-bordered">
                                        <HeaderStyle BorderColor="Gray" BorderWidth="1px" CssClass="bg-blue" />
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="ردیف">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Container.ItemIndex+1 %>" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderText="شماره درخواست" DataField="StudentRequestID" />
                                            <telerik:GridBoundColumn HeaderText="فرستنده" DataField="RequestLogID" />
                                            <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="stcode" />
                                            <telerik:GridBoundColumn HeaderText="گیرنده" DataField="Erae_Be" />
                                            <telerik:GridBoundColumn HeaderText="تاریخ ثبت" DataField="SubmitDate" />
                                            <telerik:GridBoundColumn HeaderText="توضیحات" DataField="NaghsMessage" />
                                            <telerik:GridBoundColumn HeaderText="وضعیت" DataField="IsResolved" />
                                            <telerik:GridBoundColumn HeaderText="تاریخ رفع نقص" DataField="ResolveDate" />
                                            <telerik:GridBoundColumn HeaderText="توضیحات رفع نقص" DataField="ResolveMessage" />
                                            <telerik:GridClientDeleteColumn HeaderText="حذف" Text="حذف" ConfirmText="آیا مطمئن هستید که می خواهید این سطر را حذف کنید ؟"></telerik:GridClientDeleteColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <br />
                                <div class="bg-green" style="padding: 5px">
                                    <h4 style="font-family: 'B Titr'">درج نقص جدید</h4>
                                </div>
                                <div class="form-inline">

                                    <div class="form-group">
                                        <label for="txtDescription">توضیحات نواقص پرونده :</label>
                                        <asp:TextBox ID="txtNaghsDescription" CssClass="form-control" TextMode="MultiLine" runat="server" />
                                    </div>
                                    <asp:Button ID="btnSubmitNaghs" Text="ثبت نقص" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitNaghs_Click" />
                                    <asp:Button ID="btnCloseNaghs" Text="خروج" runat="server" CssClass="btn btn-success" OnClick="btnCloseNaghs_Click" />
                                </div>
                                <asp:HiddenField ID="hdnfReqId" runat="server" />
                                <asp:HiddenField ID="hdnfStcode" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </telerik:RadWindow>


            <telerik:RadWindow ID="RadWindowVahed" runat="server" OnClientClose="grdReload" Width="950" Height="500" Modal="true">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div dir="rtl">
                                <div class="bg-primary" style="padding: 5px">
                                    <h4 style="font-family: 'B Titr'">محل صدور مدرک دانشجو</h4>
                                </div>
                                <br />

                                <div class="bg-green" style="padding: 5px">
                                    <h4 style="font-family: 'B Titr'">انتخاب محل صدور مدرک</h4>
                                </div>
                                <div class="form-inline">

                                    <div class="form-group">
                                        <label for="txtDescription">محل صدور مدرک :</label>
                                        <asp:DropDownList ID="drpMahaleSodooreMadrak" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:Button ID="btnSubmit" Text="ثبت و تایید" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" Text="خروج" runat="server" CssClass="btn btn-success" OnClick="btnCancel_Click" />
                                </div>
                                <asp:HiddenField ID="HdnReq" runat="server" />
                                <asp:HiddenField ID="HdnSt" runat="server" />

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow ID="rwCheckStudentAverage" runat="server" Width="500" Height="250" Modal="true">
                <ContentTemplate>
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div dir="rtl">
                                <div class="bg-primary" style="padding: 5px">
                                    <h4 style="font-family: 'B Titr'">معدل دانشجو مورد تایید است ؟</h4>
                                </div>
                                <br />
                                <asp:Button ID="btnAvgIsAuthorized" Text="بله" runat="server" CssClass="btn btn-primary" OnClick="btnAvgIsAuthorized_Click" />
                                <asp:Button ID="btnAvgIsUnacceptable" Text="خیر" runat="server" CssClass="btn btn-success" OnClick="btnAvgIsUnacceptable_Click" />
                            </div>


                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </telerik:RadWindow>

            <telerik:RadWindow runat="server" ID="rwReasonFrequency" Width="500px" MinHeight="350px" MaxHeight="450px" MinWidth="500px" MaxWidth="700px">
                <ContentTemplate>
                    <div dir="rtl" style="padding: 10px; font-size: larger">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="alert alert-danger">
                                    <div class="row">
                                        <div class="col-md-12 ">
                                            <span>دلیل انصراف دانشجو:</span>
                                            <br />
                                            <asp:Label ID="lblStudentCheckoutReason" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="alert alert-success">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <span>دلیل تسویه حساب در کدام دسته پرتکرار قرار گیرد؟</span>
                                        </div>
                                    </div>
                                    <div class="row" dir="rtl">
                                        <div class="col-md-12">
                                            <asp:DropDownList CssClass="form-control" ID="ddlFrequencyReasons" ForeColor="Gray" runat="server" Width="60%"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReason" ControlToValidate="ddlFrequencyReasons" runat="server" InitialValue="0" ErrorMessage="حتما یکی از موارد باید انتخاب شود"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="row" dir="rtl">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnSaveFrequencyReason" OnClick="btnSaveFrequencyReason_Click" runat="server" Text="ثبت در دسته پرتکرار" CssClass="btn btn-dark" />

                                        </div>

                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSaveFrequencyReason" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefreshGrid" EventName="Click" />
            <%--<asp:PostBackTrigger ControlID="btnDownloadThesisPDF" />grd_CheckOutList--%>
            <asp:AsyncPostBackTrigger ControlID="grd_CheckOutList" EventName="ItemDataBound" />
            <asp:AsyncPostBackTrigger ControlID="grd_CheckOutList" EventName="NeedDataSource" />
            <asp:PostBackTrigger ControlID="btnDlExcel" />
            <asp:AsyncPostBackTrigger ControlID="drpUserRoles" EventName="SelectedIndexChanged" />

        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">
            function downloadfile() {
                document.getElementById("<%=DownloadFile.ClientID %>").click();
            }

            // Get a PageRequestManager reference.
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            // Hook the _initializeRequest event and add our own handler.
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            function InitializeRequest(sender, args) {
                // Check to be sure this async postback is actually 
                //   requesting the file download.
                if (sender._postBackSettings.sourceElement.id == "<%=DownloadFile.ClientID %>") {
                // Create an IFRAME.
                var iframe = document.createElement("iframe");

                // Get the desired region from the dropdown.
                var imgadd = $("#" + '<%= hidClientField.ClientID %>').val();

                    // Point the IFRAME to GenerateFile, with the
                    //   desired region as a querystring argument.
                    iframe.src = "GenerateFile.aspx?image=" + imgadd;

                    // This makes the IFRAME invisible to the user.
                    iframe.style.display = "none";

                    // Add the IFRAME to the page.  This will trigger
                    //   a request to GenerateFile now.
                    document.body.appendChild(iframe);
                }
            }

            function EndRequest(sender, args) {

                var idArray = [];
                $('.pcal input').each(function () {
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

    </script>
</asp:Content>
