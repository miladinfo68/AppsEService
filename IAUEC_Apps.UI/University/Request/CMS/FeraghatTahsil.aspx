<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="FeraghatTahsil.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.FeraghatTahsil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
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

        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .buttonSubmit {
            margin: 60px;
        }

        .triangle-isoscelesViolet {
            position: relative;
            padding: 15px;
            margin: 1em 1em 3em;
            color: #000;
            background: #be68ba;
            border-radius: 10px;
            background: linear-gradient(top, #f9d835, #f3961c);
        }

        .triangle-isoscelesRed {
            position: relative;
            padding: 15px;
            margin: 1em 1em 3em;
            color: #000;
            background: #ff0000;
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

        #RadWindowWrapper_ctl00_ContentPlaceHolder1_rdwReceipt {
            min-width: 100% !important;
            min-height: 100% !important;
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
    </script>
    <script src="js/jquery-3.3.1.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2>سامانه فارغ التحصیلان
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) {
            debugger;
            var oControl = args.get_postBackElement();
            oControl.disabled = true;
        }

    </script>
    <div class="container" dir="rtl">

        <div class="row well">
            <div class="form-horizontal">
                <div class="form-group">
                    <div class="col-sm-3" style="width: 190px;">
                        جستجو بر اساس شماره دانشجویی :
                    </div>
                    <div class="col-sm-2" style="margin-left: 20px;">
                        <asp:TextBox ID="txtStcode" runat="server" />
                    </div>
                    <div class="col-sm-3" style="width: 190px;">
                        جستجو بر اساس نام خانوادگی :
                    </div>
                    <div class="col-sm-2" style="margin-left: 20px;">
                        <asp:TextBox ID="txtFamily" runat="server" />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="btnSearch" Text="جستجو" runat="server" ValidationGroup="search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="Button2" Text="لغو" runat="server" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <telerik:RadGrid ID="grd_Info" runat="server" Visible="false" AutoGenerateColumns="false" EnableEmbeddedSkins="false" Skin="MyCustomSkin" 
            AllowFilteringByColumn="True" AllowPaging="True" OnNeedDataSource="grd_Info_NeedDataSource" OnItemCommand="grd_Info_ItemCommand" OnItemDataBound="grd_Info_ItemDataBound">
            <MasterTableView>
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="Label1" Text="<%# Container.ItemIndex + 1 %>" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="namep" HeaderText="نام پدر"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="resh" HeaderText="رشته - گرایش"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="magh" HeaderText="مقطع"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="افزودن" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnAdd" CommandName="Add" Text="افزودن" CssClass="btn btn-success" runat="server" OnClientClick="confirmAspButton(this); return false;" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <div class="row">
            <div class="col-md-3">
                <asp:Button ID="btnExportExcel_Baygani" Visible="true" runat="server" Text="خروجی اکسل کد بایگانی مدارک" CssClass="btn btn-success" OnClick="btnExportExcel_Baygani_Click" />
            </div>
        </div>
        <div class="row" dir="rtl">
            <telerik:RadGrid AllowSorting="true" ID="grd_Students" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                AllowFilteringByColumn="true" PageSize="100" EnableEmbeddedSkins="false" Skin="MyCustomSkin"
                OnItemCommand="grd_Students_ItemCommand" OnNeedDataSource="grd_Students_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Label ID="Label1" Text="<%# Container.ItemIndex + 1 %>" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="شماره درخواست" AllowFiltering="true" AllowSorting="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StCode" HeaderText="شماره دانشجویی" AllowFiltering="true" AllowSorting="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی" AllowFiltering="true" AllowSorting="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام دانشجو" AllowFiltering="true" AllowSorting="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته" AllowFiltering="true" AllowSorting="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="far_code" HeaderText="کد بایگانی" AllowFiltering="true" AllowSorting="false"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="وضعیت مدارک" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Button ID="btnShowMadarek" CommandName="show" CommandArgument='<%# Eval("StudentRequestId") %>' Text="نمایش" CssClass="btn btn-success" runat="server" />
                                <asp:Button ID="btnReturn" CommandName="return" CommandArgument='<%#string.Format("{0}-{1}",Eval("StudentRequestId"),Eval("StCode") )%>' Text="عودت" CssClass="btn btn-success" runat="server" />


                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <%--<telerik:GridTemplateColumn HeaderText="پیام" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Button Text="ارسال پیام" CommandName="message" CommandArgument='<%# Eval("StudentRequestId") %>' CssClass="btn btn-dark" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>

        </div>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

    <telerik:RadWindow ID="RadWindowNaghs" runat="server" Width="950" Height="500" Modal="true">
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
                        <telerik:RadGrid ID="grdNaghs1" runat="server" OnItemDataBound="grdNaghs1_ItemDataBound1" EnableEmbeddedSkins="false" AutoGenerateColumns="false" OnItemCommand="grdNaghs1_ItemCommand1">
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
                                    <telerik:GridClientDeleteColumn HeaderText="حذف" Text="حذف" ConfirmText="آیا مطمئن هساید که می خواهید این سطر را حذف کنید ؟"></telerik:GridClientDeleteColumn>
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
                            <%--    <asp:Button ID="btnCloseNaghs" Text="خروج" runat="server" CssClass="btn btn-success" OnClick="btnCloseNaghs_Click" />--%>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="hdnfStcode" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindow ID="rdwReceipt" runat="server" Skin="Glow" Modal="True" InitialBehavior="Maximize" Style="text-align: center; height: 100%; width: 100%;">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updateProgress" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>


                    <iframe src="../../../CommonUI/Signature/Signature.aspx" style="min-height: 1000px; min-width: 100%"></iframe>

                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindow ID="statusPopup" Width="1200" Height="600" AutoSize="false" runat="server">

        <ContentTemplate>


            <div class="container" dir="rtl" style="padding: 10px">
                <asp:HiddenField ID="hdnReqID" runat="server" />
                <div class="bg-green">
                    <h4>وضعیت مدارک دانشجو</h4>
                </div>
                <div id="mashmoolBanner" runat="server" visible="false" class="triangle-isoscelesViolet col-md-3">
                    <span>دانشجو مشمول میباشد</span>
                </div>

                <div id="vamdarBanner" runat="server" visible="false" class="triangle-isoscelesRed col-md-3">
                    <span>دانشجو وامدار میباشد</span>
                </div>
                <br />
                <hr />
                <br />

                <div class="row">
                    <div class="col-md-12">
                        <div>
                            <asp:Label Text="شماره دانشجویی :" runat="server" />
                            <asp:Label ID="lblStcode" runat="server" />
                        </div>

                        <div>
                            <asp:Label Text="نام دانشجو :" runat="server" />
                            <asp:Label ID="lblStName" runat="server" />
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-sm-2">
                        <asp:CheckBox ID="chbkGovahiMovaghat" Text="گواهی موقت" runat="server" />
                    </div>
                    <div class="col-md-3 red">

                        <label id="lblArchiveMovaghat_Text">کد بایگانی مدرک موقت: </label>
                        <asp:Label ID="lblArchiveMovaghat" runat="server" Text="-"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnCreateArchiveCode_MadrakMovaghat" runat="server" Text="تخصیص کد بایگانی مدرک موقت" CssClass="btn btn-warning" OnClick="btnCreateArchiveCode_MadrakMovaghat_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <span id="dateSodoorGovahiMovaghat">تاریخ صدور گواهی موقت  :</span>


                        <asp:TextBox ID="TxtSodoorGovahiMovaghat" runat="server" CssClass="pdate" />
                        <asp:CustomValidator ID="CustomValidator4" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator4_ServerValidate" ValidationGroup="submit" ControlToValidate="TxtSodoorGovahiMovaghat" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <span id="dateGovahiMovaghat">تاریخ تحویل گواهی موقت :</span>
                        <asp:TextBox ID="txtGovahiMovaghat" runat="server" CssClass="pdate" />
                        &nbsp
                        <asp:CustomValidator ID="CustomValidator3" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator3_ServerValidate" ValidationGroup="submit" ControlToValidate="txtGovahiMovaghat" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="btnReceiptGovahi" Width="80" runat="server" ToolTip="رسید تحویل" Text="رسید تحویل" CssClass="btn btn-primary" OnClick="btnReceiptGovahi_Click" />
                    </div>
                    <div class="col-md-4" runat="server" id="dvSignatureGovahi">
                        <img src="" id="signatureGovahi" width="100px" height="100px" />

                        <asp:HiddenField runat="server" ID="hdnGovahi" />
                        <script>
                            var hidden = '#' + '<%= hdnGovahi.ClientID %>';
                            var baseString = $(hidden).val().trim();
                            if (baseString.substring(0, 4) != "data") {
                                baseString = "data:image/png;base64," + baseString;
                            }
                            $("#signatureGovahi").prop('src', baseString);

                        </script>
                    </div>
                </div>

                <hr style="border-bottom: 1px solid #ccc;" />


                <div class="row">
                    <div class="col-sm-2">
                        &nbsp<asp:CheckBox ID="chbkDaneshNameh" Text="دانشنامه" runat="server" />
                    </div>
                    <div class="col-md-2 red">

                        <label id="lblArchiveDanesh_Text">کد بایگانی دانشنامه: </label>
                        <asp:Label ID="lblArchiveDanesh" runat="server" Text="-"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnCreateArchiveCode_Daneshname" runat="server" Text="تخصیص کد بایگانی دانشنامه" CssClass="btn btn-warning" OnClick="btnCreateArchiveCode_Daneshname_Click" />
                    </div>

                </div>
                <div class="row">

                    <div class="col-md-4">
                        <span id="dateSodoorDaneshname">تاریخ صدور دانشنامه  :</span>
                        <asp:TextBox ID="txtSodoorDaneshname" runat="server" CssClass="pdate" />
                        &nbsp
                        <asp:CustomValidator ID="CustomValidator6" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator6_ServerValidate" ValidationGroup="submit" ControlToValidate="txtSodoorDaneshname" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <span id="dateDaneshname">تاریخ تحویل دانشنامه :</span>

                        <asp:TextBox ID="txtDaneshname" runat="server" CssClass="pdate " />
                        &nbsp<asp:CustomValidator ID="CustomValidator5" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator5_ServerValidate" ValidationGroup="submit" ControlToValidate="txtDaneshname" runat="server" />
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="btnReceiptDanesh" Width="80" runat="server" ToolTip="رسید تحویل" Text="رسید تحویل" CssClass="btn btn-primary" OnClick="btnReceiptDanesh_Click" />
                    </div>
                    <div class="col-md-4" runat="server" id="dvSignatureDaneshname">
                        <img src="" id="signatureDaneshname" width="100px" height="100px" />

                        <asp:HiddenField runat="server" ID="hdnDaneshname" />
                        <script>
                            var hidden = '#' + '<%= hdnDaneshname.ClientID %>';
                            var baseString = $(hidden).val().trim();
                            if (baseString.substring(0, 4) != "data") {
                                baseString = "data:image/png;base64," + baseString;
                            }
                            $("#signatureDaneshname").prop('src', baseString);

                        </script>
                    </div>
                </div>

                <hr style="border-bottom: 1px solid #ccc;" />

                <div class="row">
                    <div class="col-md-2">
                        &nbsp<asp:CheckBox ID="chbkRizNomarat" Text="ریز نمرات" runat="server" />

                    </div>
                    <div class="col-md-3 red">

                        <label id="lblArchiveRiz_Text">کد بایگانی ریزنمره: </label>
                        <asp:Label ID="lblArchiveRiz" runat="server" Text="-"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnCreateArchiveCode_Riznomre" runat="server" Text="تخصیص کد بایگانی ریزنمره" CssClass="btn btn-warning" OnClick="btnCreateArchiveCode_Riznomre_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <span id="dateErsalRizNomarat">تاریخ ارسال ریز نمرات :</span>

                        <asp:TextBox ID="txtErsalRizNomre" runat="server" CssClass="pdate" />
                        &nbsp<asp:CustomValidator ID="CustomValidator7" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator7_ServerValidate" ValidationGroup="submit" ControlToValidate="txtErsalRizNomre" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <span id="dateSodoorRizNomarat">تاریخ صدور ریز نمرات :</span>


                        <asp:TextBox ID="txtSodoorRizNomre" runat="server" CssClass="pdate" />
                        &nbsp<asp:CustomValidator ID="CustomValidator2" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="CustomValidator2_ServerValidate" ValidationGroup="submit" ControlToValidate="txtSodoorRizNomre" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <span id="dateRizNomarat">تاریخ تحویل ریز نمرات :</span>
                        <asp:TextBox ID="txtRizNomre" runat="server" CssClass="pdate" />
                        &nbsp<asp:CustomValidator ID="vldTxtRizNomreTahvil" ErrorMessage="تاریخ معتبر نیست" ForeColor="Red" Display="Dynamic" OnServerValidate="vldTxtRizNomreTahvil_ServerValidate" ValidationGroup="submit" ControlToValidate="txtRizNomre" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="btnReceiptRiz" Width="80" runat="server" ToolTip="رسید تحویل" Text="رسید تحویل" CssClass="btn btn-primary" OnClick="btnReceiptRiz_Click" />
                    </div>
                    <div class="col-md-4" runat="server" id="dvSignatureRiznomre">
                        <img src="" id="signatureRiznomre" width="100px" height="100px" />

                        <asp:HiddenField runat="server" ID="hdnRiznomre" />
                        <script>
                            var hidden = '#' + '<%= hdnRiznomre.ClientID %>';
                            var baseString = $(hidden).val().trim();
                            if (baseString.substring(0, 4) != "data") {
                                baseString = "data:image/png;base64," + baseString;
                            }
                            $("#signatureRiznomre").prop('src', baseString);

                        </script>
                    </div>
                </div>

                <div class="row">
                </div>

            </div>
            <asp:Button ID="btnSubmitStatus" OnClick="btnSubmitStatus_Click" Text="ثبت وضعیت" ValidationGroup="submit" runat="server" CommandArgument="0" CssClass="btn btn-danger" />
            <asp:HiddenField ID="hdnfReqId" runat="server" />
            <asp:HiddenField ID="hdnfFeraghatId" runat="server" />
            </div>
           
            
        </ContentTemplate>
    </telerik:RadWindow>

    <script type="text/javascript">
        function GetRadWindow() {
            debugger;
            var oWindow = null; if (window.radWindow)
                oWindow = window.radWindow; else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; return oWindow;
        }
    </script>
    <script>
        var alertIsShown;
        function raiseAlertFlag(sender, args) {
            debugger;
            if (sender._isPredefined) {
                alertIsShown = true;
            }
        }
        function closeStatusPopup() {
            debugger;
            var window = $find('<%=statusPopup.ClientID %>');
            window.close();
            //CloseAndRebind(true);
        }

        function CloseAndRebind(args) {
            debugger;
            GetRadWindow().BrowserWindow.refreshGrid(args);
        }
        function closeradwindow4() {
            debugger;
            var window = $find('<%=statusPopup.ClientID %>');
            window.show();
        }
        function checkAlertFlag() {
            debugger;
            alert('f');
        }
        function redirectToLast() {
            debugger;
            window.history.back();
        }

        function GetRadWindow() {
            debugger;
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginreq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginreq(sender, args) {
            debugger;
            postbackElement = args.get_postBackElement();
            $("#wait").css("display", "block");
        }
        function endReq(sender, args) {
            debugger;
            document.getElementById(postbackElement.id).focus();
            $("#wait").css("display", "none");
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_txtRizNomre',
                { extraInputID: 'ContentPlaceHolder1_txtRizNomre', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_txtGovahiMovaghat',
                { extraInputID: 'ContentPlaceHolder1_txtGovahiMovaghat', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_txtDaneshname',
                { extraInputID: 'ContentPlaceHolder1_txtDaneshname', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_txtSodoorRizNomre',
                { extraInputID: 'ContentPlaceHolder1_txtSodoorRizNomre', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_TxtSodoorGovahiMovaghat',
                { extraInputID: 'ContentPlaceHolder1_TxtSodoorGovahiMovaghat', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_txtSodoorDaneshname',
                { extraInputID: 'ContentPlaceHolder1_txtSodoorDaneshname', extraInputFormat: 'yyyy/mm/dd' });
            var objCal1 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_statusPopup_C_txtErsalRizNomre',
                { extraInputID: 'ContentPlaceHolder1_txtErsalRizNomre', extraInputFormat: 'yyyy/mm/dd' });

        }
        );
    </script>
</asp:Content>
