<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="ReportsForm.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ReportsForm" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .radfont, .rgHeader, h4 {
            font-family: 'B Titr' !important;
            font-weight: bold;
        }

        .rwContentRow {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
        }


        .buttonSubmit {
            margin: 60px;
        }

        .triangle-isosceles {
            position: relative;
            padding: 15px;
            margin: 1em 1em 3em;
            color: #000;
            background: #f3961c;
            border-radius: 10px;
            background: linear-gradient(top, #f9d835, #f3961c);
        }

            .triangle-isosceles:after {
                content: "";
                display: block; /* reduce the damage in FF3.0 */
                position: absolute;
                bottom: -15px;
                left: 50px;
                width: 0;
                border-width: 15px 15px 0;
                border-style: solid;
                border-color: #f3961c transparent;
            }
    </style>
    <script type="text/javascript">
        (function (global, undefined) {
            function confirmAspButton(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید که می خواهید این درخواست را ارسال کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }
            global.confirmAspButton = confirmAspButton;
        })(window);

        function skipSpace(sender, eventArgs) {
            debugger;
            var e = eventArgs.get_domEvent();
            var keyCode = e.keyCode || e.which;
            var shouldMarkFirstMatch = (keyCode !== 32);

            sender.set_markFirstMatch(shouldMarkFirstMatch);
        }
    </script>
    <style>
        .dashboard_graph {
            direction: rtl;
        }

        .fieldTitle {
            line-height: 30px;
            padding-right: 10px;
        }

        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .RadComboBox .form-control {
            height: 28px;
        }

        .RadComboBox {
            margin-top: 2px;
        }

        div.rcbSlide {
            height: 100px !important;
        }

        div.RadComboBoxDropDown {
            height: 100px !important;
        }

        div.rcbWidth {
            height: 100px !important;
        }
    </style>


    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" OnClientBeforeShow="raiseAlertFlag"></telerik:RadWindowManager>
    <script type="text/javascript" src="../../../CommonUI/js/daterangepicker.js"></script>
    <div class="container">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h4>گزارشات آماده سیستمی</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3" style="padding:5px">
                        <asp:Button ID="btnReportCodeBayegani" runat="server" CssClass="btn btn-success form-control" Text="خروجی اکسل کد بایگانی سیدا" OnClick="btnReportCodeBayegani_Click"  />
                    </div>
                    <div class="col-md-3" style="padding:5px">
                        <asp:Button ID="btnGraduateDocument" runat="server" CssClass="btn btn-success form-control" Text="خروجی اکسل کد بایگانی مدارک" OnClick="btnGraduateDocument_Click"  />
                    </div>
                    <div class="col-md-3" style="padding:5px">
                        <asp:Button ID="btnDocArchive" runat="server" CssClass="btn btn-success form-control" Text="خروجی اکسل کد فیش و تمبر" OnClick="btnDocArchive_Click"  />
                    </div>
                    <div class="col-md-3" style="padding:5px">
                        <asp:Button ID="btnDocArchive_Nagh" runat="server" CssClass="btn btn-success form-control" Text="خروجی اکسل دانشجویانی که فیش یاتمبر ندارند" OnClick="btnDocArchive_Nagh_Click"  />
                    </div>
                </div>
            </div>
        </div>


        <div class="panel panel-info">
            <div class="panel-heading">
                <h4>گزارش ساز</h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <br />
                    <div class="col-md-3">
                        <div class="col-md-3">
                            <asp:Label Text="دانشکده :" runat="server" CssClass="fieldTitle" />
                        </div>
                        <div class="col-md-9">

                            <telerik:RadComboBox RenderMode="Lightweight" ID="drpUniversity" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" InputCssClass="form-control" Height="25px"
                                Filter="Contains" MarkFirstMatch="true" EmptyMessage="جستجو نمایید" Localization-CheckAllString="همه موارد" Localization-AllItemsCheckedString="همه موارد انتخاب شده" Width="100%" Display="Dynamic">
                            </telerik:RadComboBox>


                        </div>

                    </div>
                    <div class="col-md-3">
                        <div class="col-md-2">
                            <asp:Label Text="رشته :" runat="server" CssClass="fieldTitle" />
                        </div>
                        <div class="col-md-10">
                            <telerik:RadComboBox RenderMode="Lightweight" ID="drpFild" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" Height="25px"
                                Filter="Contains" EmptyMessage="جستجو نمایید" Localization-CheckAllString="همه موارد" Localization-AllItemsCheckedString="همه موارد انتخاب شده" InputCssClass="form-control">
                            </telerik:RadComboBox>


                            <%--<asp:DropDownList ID="drpFild" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                        </div>
                        <%--  <div>
                    <telerik:RadComboBox ID="RadComboBoxField" runat="server" OnSelectedIndexChanged="RadComboBoxField_SelectedIndexChanged" AutoPostBack="true" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Culture="(Default)" Height="300px"></telerik:RadComboBox>
                </div>--%>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-3">
                            <asp:Label ID="lblMadrak" Text="نوع مدرک :" runat="server" CssClass="fieldTitle" />
                        </div>
                        <div class="col-md-9">
                            <telerik:RadComboBox RenderMode="Lightweight" ID="drpMadrak" runat="server" Width="80%" Height="25px" OnSelectedIndexChanged="drpMadrak_SelectedIndexChanged" AutoPostBack="true"
                                Filter="Contains" MarkFirstMatch="true" InputCssClass="form-control">
                            </telerik:RadComboBox>


                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-4">
                            <asp:Label ID="lblVahed" Text="واحد صادر کننده :" runat="server" CssClass="fieldTitle" />
                        </div>
                        <div class="col-md-8">
                            <%-- <asp:DropDownList ID="drpVahed" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="drpVahed" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="100%" Height="25px"
                                Filter="Contains" MarkFirstMatch="true" EmptyMessage="جستجو نمایید" Localization-CheckAllString="همه موارد" Localization-AllItemsCheckedString="همه موارد انتخاب شده" InputCssClass="form-control">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                </div>

                <br />
                <br />
                <br />

                <div id="radioSpace" runat="server" style="border: solid 3px #349494; padding: 15px">
                    <div class="row">
                        <div class="col-md-4">
                            &nbsp<asp:RadioButton ID="rdvoroodKartabl" Text="بر اساس تاریخ ورود پرونده به کارتابل معاونت دانشجویی" runat="server" GroupName="ST" AutoPostBack="true" OnCheckedChanged="rdvoroodKartabl_CheckedChanged" />

                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-1">
                            <p id="fromDate" runat="server" visible="false">از تاریخ :</p>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFromKartabl" runat="server" CssClass="pdate" Visible="false"></asp:TextBox>
                            &nbsp<asp:CustomValidator ID="CustomValidator7" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator7_ServerValidate" ValidationGroup="submit" ControlToValidate="txtFromKartabl" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <p id="toDate" runat="server" visible="false">تا تاریخ :</p>

                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtToKartabl" runat="server" CssClass="pdate" Visible="false" />
                            &nbsp<asp:CustomValidator ID="CustomValidator2" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="submit" ControlToValidate="txtToKartabl" runat="server" />
                        </div>
                        <br />
                    </div>

                    <hr style="border-bottom: 1px solid #ccc;" />

                    <div class="row">
                        <div class="col-sm-2">
                            &nbsp<asp:RadioButton ID="rdExitMadrak" Text="بر اساس تاریخ خروج پرونده " runat="server" GroupName="ST" AutoPostBack="true" OnCheckedChanged="rdExitMadrak_CheckedChanged" />
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-1">
                            <p id="fromDateExit" runat="server" visible="false">از تاریخ  :</p>

                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFromDateExit" runat="server" CssClass="pdate" Visible="false" />
                            &nbsp<asp:CustomValidator ID="CustomValidator1" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="submit" ControlToValidate="txtFromDateExit" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <p id="toDateExit" runat="server" visible="false">تا تاریخ :</p>
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox ID="txtToDateExit" runat="server" CssClass="pdate " Visible="false" />
                            &nbsp
                        <asp:CustomValidator ID="CustomValidator8" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="submit" ControlToValidate="txtToDateExit" runat="server" />
                        </div>
                    </div>

                    <hr style="border-bottom: 1px solid #ccc;" />

                    <div class="row">
                        <div class="col-sm-12">
                            &nbsp<asp:RadioButton ID="rdSodoor" Text="بر اساس تاریخ صدور مدرک توسط واحد همکار" runat="server" GroupName="ST" AutoPostBack="true" OnCheckedChanged="rdSodoor_CheckedChanged" />
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-1">
                            <p id="fromDateSodoor" runat="server" visible="false">از تاریخ  :</p>

                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="TxtFromSodoor" runat="server" CssClass="pdate" Visible="false" />
                            &nbsp<asp:CustomValidator ID="CustomValidator4" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="submit" ControlToValidate="TxtFromSodoor" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <p id="toDateSodoor" runat="server" visible="false">تا تاریخ :</p>
                        </div>
                        <div class="col-md-2">

                            <asp:TextBox ID="txtToSodoor" runat="server" CssClass="pdate" Visible="false" />
                            &nbsp
                        <asp:CustomValidator ID="CustomValidator3" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="submit" ControlToValidate="txtToSodoor" runat="server" />
                        </div>
                    </div>

                    <hr style="border-bottom: 1px solid #ccc;" />

                    <div class="row">
                        <div class="col-sm-2">
                            &nbsp<asp:RadioButton ID="rdVoroodMadrak" Text="بر اساس تاریخ ورود مدرک به واحد" runat="server" GroupName="ST" AutoPostBack="true" OnCheckedChanged="rdVoroodMadrak_CheckedChanged" />
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-1">
                            <p id="fromDatevoroodmadrak" runat="server" visible="false">ازتاریخ  :</p>

                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFromVoroodMadrak" runat="server" CssClass="pdate" Visible="false" />
                            &nbsp
                        <asp:CustomValidator ID="CustomValidator6" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator6_ServerValidate" ValidationGroup="submit" ControlToValidate="txtFromVoroodMadrak" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <p id="toDateVoroodMadrak" runat="server" visible="false">تا تاریخ :</p>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtToDateVoroodMadrak" runat="server" CssClass="pdate " Visible="false" />
                            &nbsp<asp:CustomValidator ID="CustomValidator5" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator5_ServerValidate" ValidationGroup="submit" ControlToValidate="txtToDateVoroodMadrak" runat="server" />
                        </div>

                    </div>
                </div>

                <br />
                <br />
                <div class="row">
                    <asp:Label ID="lblStatusCase" runat="server" Text="وضعیت پرونده"></asp:Label>
                    &nbsp&nbsp
            <asp:DropDownList runat="server" ID="drpStatusCase" OnSelectedIndexChanged="drpStatusCase_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0">همه موارد</asp:ListItem>
                <asp:ListItem Value="7">پرونده در انتظار بررسی</asp:ListItem>
                <asp:ListItem Value="1">پرونده بررسی شده</asp:ListItem>
                <asp:ListItem Value="2">پرونده ناقص</asp:ListItem>
                <asp:ListItem Value="3">پرونده خارج شده</asp:ListItem>
                <asp:ListItem Value="4">پرونده آماده ارسال</asp:ListItem>
                <asp:ListItem Value="5">پرونده جاری</asp:ListItem>
                <asp:ListItem Value="6">مدرک وارد نشده</asp:ListItem>
            </asp:DropDownList>

                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
           
            <asp:Label ID="lblStatusMadrak" runat="server" Text="وضعیت مدارک"></asp:Label>
                    &nbsp&nbsp
            <asp:DropDownList runat="server" ID="drpStatusMadrak" OnSelectedIndexChanged="drpStatusMadrak_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0">همه موارد</asp:ListItem>
                <asp:ListItem Value="1">مدرک صادر شده</asp:ListItem>
                <asp:ListItem Value="2">مدرک وارد شده</asp:ListItem>
                <asp:ListItem Value="3">مدرک وارد نشده</asp:ListItem>
                <asp:ListItem Value="4">مدرک عودت شده</asp:ListItem>
                <asp:ListItem Value="5">مدرک تحویل شده</asp:ListItem>
                <asp:ListItem Value="6">مدرک تحویل نشده</asp:ListItem>
                <asp:ListItem Value="7">مدرک پرداخت شده</asp:ListItem>
                <asp:ListItem Value="8">مدرک پرداخت نشده</asp:ListItem>
            </asp:DropDownList>
                </div>

                <div class="row" dir="ltr">
                    <asp:Button ID="btnshow" OnClick="btnshow_Click" Text="نمایش" ValidationGroup="submit" runat="server" CssClass="btn btn-primary buttonSubmit" />
                </div>
            </div>
            <div class="panel-footer">
                
    <asp:Button ID="ExcleExportBtn" runat="server" Text="خروجی اکسل" Visible="false" CssClass="btn btn-warning btnWith" OnClick="ExcleExportBtn_Click" />
    <telerik:RadGrid ID="grd_Info" runat="server" Visible="false" OnExcelMLWorkBookCreated="grd_Info_ExcelMLWorkBookCreated" AutoGenerateColumns="false" EnableEmbeddedSkins="false" Skin="MyCustomSkin" AllowFilteringByColumn="True" AllowPaging="True" OnNeedDataSource="grd_Info_NeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate>
                        <%# (CurrentPage.CurrentPageNumberValue*CurrentPage.PageSizeValue)+(Container.ItemIndex + 1) %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="name" HeaderText="نام"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="شماره درخواست"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="namedanesh" HeaderText="دانشکده"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="vahed" HeaderText="محل صدور مدرک"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="sodoormadrak" HeaderText="تاریخ صدور مدرک"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="voroodmadrak" HeaderText="تاریخ ورود مدرک"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="madrakdeliver" HeaderText="تاریخ تحویل مدرک"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DateErsalRizNomre" HeaderText="تاریخ ارسال مدرک"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>


            </div>
        </div>
    </div>


    <script>
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

    <script type="text/javascript">
        var alertIsShown;
        function raiseAlertFlag(sender, args) {
            if (sender._isPredefined) {
                alertIsShown = true;
            }
        }

        function checkAlertFlag() {
            alert('f');
        }
        //        $(document).ready(function () {
        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_txtFromKartabl',
        //       { extraInputID: 'ContentPlaceHolder1_txtFromKartabl', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_txtToKartabl',
        //      { extraInputID: 'ContentPlaceHolder1_txtToKartabl', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_TxtFromSodoor',
        //      { extraInputID: 'ContentPlaceHolder1_TxtFromSodoor', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_txtToSodoor',
        //      { extraInputID: 'ContentPlaceHolder1_txtToSodoor', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_txtFromVoroodMadrak',
        //      { extraInputID: 'ContentPlaceHolder1_txtFromVoroodMadrak', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_txtToDateVoroodMadrak',
        //      { extraInputID: 'ContentPlaceHolder1_txtToDateVoroodMadrak', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_rdExitMadrak',
        //     { extraInputID: 'ContentPlaceHolder1_rdExitMadrak', extraInputFormat: 'yyyy/mm/dd' });

        //            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_txtToDateExit',
        //{ extraInputID: 'ContentPlaceHolder1_txtToDateExit', extraInputFormat: 'yyyy/mm/dd' });
        //        }
        //   );
    </script>


</asp:Content>
