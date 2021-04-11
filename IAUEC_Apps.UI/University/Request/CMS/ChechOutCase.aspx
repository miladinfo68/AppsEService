<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="ChechOutCase.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.ChechOutCase" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
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
        /*body {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }*/

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

   <%--     function validator1() {
            var txtnumber = $find("<%=txtNumber.ClientID%>");
            args.IsValid = upload.length != 0;
        }--%>

        function grdReload(sender, args) {
            refresgGrid();
        }

        function openRadWindow2() {
            var oManager = GetRadWindow().get_windowManager();
            setTimeout(function () {
                oManager.open("CheckOutReview.aspx", "Radwindow2");
            }, 0);
        }

        function winDetailClosing(sender, e) {
            e.set_cancel(true);
        }

    </script>
    <script type="text/javascript" id="telerikClientEvents1">
        //<![CDATA[
        function RadWindow2_Close(sender, args) {
            document.getElementById("ctl00_ContentPlaceHolder1_RadWindow2_C_txtMsg").innerHTML = "";
        }
        //]]>


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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnl">
        <ProgressTemplate>
            <div class="ProgressTemplatemodal" style="margin: auto; text-align: center">
                <div class="ProgressTemplatecenter">
                    <img alt="" src="../../Theme/images/loader.gif" />
                    <br/><br/>
                    <b style="text-align: center; font-size: xx-large;">...در حال بارگذاری</b>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="pnl" runat="server">
        <ContentTemplate>
            <div class="col-md-12 bg-danger" dir="rtl" style="padding: 1%; color: #CC0000">
                <p>
                    <img src="../../Theme/images/Userstatus.png" />با نگه داشتن اشاره گر ماوس بر روی این آیکون اولین و آخرین نیمسال ثبت نام و وضعیت دانشجو نمایش داده می شود.
                </p>
                <p>
                    <img src="../../Theme/images/mail-t.png" height="32" width="32" />در صورتیکه دانشجو برای درخواست خود پیام گذاشته باشد این آیکون نمایش داده می شود و با قرار گرفتن اشاره گر ماوس بر روی آیکون پیام نمایش داده می شود.
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
                <p style="color: #000"><span style="color: #CC0000">*</span>دانشجویانی که وضعیت نظام وظیفه آنان مشمول است رنگ زمینه سطر درخواستشان بنفش است.</p>

            </div>
            <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass=" hidden" Text="refreshGrid" runat="server" />
            <div id="parent" runat="server" class="container text-right hidden-print" dir="rtl">
                <asp:DropDownList ID="drpUserRoles" runat="server" CssClass=" dropdown text-right" Visible="false" Enabled="false" OnSelectedIndexChanged="drpUserRoles_SelectedIndexChanged" 
                    AutoPostBack="true"></asp:DropDownList>
                <asp:Label ID="lblMessage" Text="" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                <telerik:RadGrid ID="grd_CheckOutList" runat="server" AllowFilteringByColumn="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False" Skin="MyCustomSkin"
                     OnItemCommand="grd_CheckOutList_ItemCommand" OnItemDataBound="grd_CheckOutList_ItemDataBound" OnNeedDataSource="grd_CheckOutList_NeedDataSource" 
                    EnableLinqExpressions="False" AllowPaging="True" PageSize="100" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top">
                    <%--<GroupingSettings CollapseAllTooltip="Collapse all groups" />--%>
                    <MasterTableView>
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" Text="<%# Container.ItemIndex + 1 %>" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Image ID="imgSenMessage" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="نوع" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Image ID="imgTypeRequest" runat="server" AlternateText='نوع درخواست:<%#Eval("RequestTypeName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="شماره درخواست" DataField="StudentRequestID" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="تاریخ  ثبت درخواست" DataField="CreateDate" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StCode"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="نام دانشجو" DataField="name" AllowFiltering="true"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="تاریخ دفاع" DataField="Def_Date" AllowFiltering="false"></telerik:GridBoundColumn>


                            <telerik:GridBoundColumn HeaderText="نام دانشکده" DataField="namedanesh" AllowFiltering="true"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="سایر اطلاعات" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="idd_meli" runat="server" Value='<%#Eval("idd_meli") %>' />
                                    <asp:HiddenField ID="mobile" runat="server" Value='<%#Eval("mobile") %>' />
                                    <asp:Label ID="lblNezam" Text="" runat="server" />
                                    <div class="col-md-12">
                                        <div class="col-md-2" style="margin-left: 7%">
                                            <div class="imgWrap">
                                                <asp:Image ID="imgtermVorood" runat="server" ImageUrl="~/University/Theme/images/Userstatus.png" Width="26" Height="26" />
                                               <p class="imgDescriptionterm">
                                                    نام رشته :<%#Eval("nameresh") %><br />اولین نیمسال ثبت نام:<%#Eval("termVorood") %><br />آخرین نیمسال ثبت نام:<%#Eval("lastnimsal") %><br />وضعیت قبلی دانشجو:<%#Eval("Note") %><br />کد بایگانی:<%#Eval("codebayegan") %></p>
                                                </p>
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
                <div class="table-responsive">
                    <%--<asp:GridView ID="grdCheckOutList" runat="server" CssClass="table table-borderd table-condensed table-striped text-center" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="درخواستی با این وضعیت پیدا نشد" OnRowCommand="grdCheckOutList_RowCommand" OnRowDataBound="grdCheckOutList_RowDataBound">
                        <HeaderStyle CssClass="bg-blue text-center" />
                        <Columns>
                            <asp:TemplateField HeaderText="ردیف">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" Text="<%# Container.DataItemIndex + 1 %>" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="شماره درخواست" DataField="StudentRequestID" />
                            <asp:BoundField HeaderText="تاریخ  ثبت درخواست" DataField="CreateDate" />
                            <asp:BoundField HeaderText="شماره دانشجویی" DataField="StCode" />
                            <asp:BoundField HeaderText="نام دانشجو" DataField="name" />
                            <asp:BoundField HeaderText="تاریخ دفاع" DataField="Def_Date" />
                            <asp:BoundField HeaderText="نوع درخواست تسویه" DataField="RequestTypeName" />
                            <asp:BoundField HeaderText="نوع درخواست" DataField="RequestTypeID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="نام دانشکده" DataField="namedanesh" />
                            <asp:BoundField HeaderText="اولین نیمسال ثبت نام" DataField="termVorood" />
                            <asp:BoundField HeaderText="آخرین نیمسال ثبت نام" DataField="lastnimsal" />
                            <asp:BoundField HeaderText="وضعیت" DataField="Note" />
                            <asp:TemplateField HeaderText="مشمول خدمت">
                                <ItemTemplate>
                                    <asp:HiddenField ID="idd_meli" runat="server" Value='<%#Eval("idd_meli") %>' />
                                    <asp:HiddenField ID="mobile" runat="server" Value='<%#Eval("mobile") %>' />
                                    <asp:Label ID="lblNezam" Text="" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاریخ انتخاب واحد" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblEntekhabVahedDate" Text="" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="مدت زمان حضور دانشجو (دقیقه)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHozoorHour" Text="" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" Text="تایید" runat="server" CommandName="approve" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-success" OnClientClick="confirmAspButton(this); return false;" />                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ارسال پیام">
                                <ItemTemplate>
                                    <asp:Button ID="btnSendMsg" Text="ارسال پیام" runat="server" CommandName="msg" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-dark" />
                                    <asp:Label ID="lblUserMessage" Text="" runat="server" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="پیام دانشجو" DataField="StudentMessage" />                            
                            <asp:TemplateField HeaderText="عملیات مالی" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnRefah" Text="درج وضعیت" runat="server" CommandName="refah" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-success" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="نقص پرونده" Visible="false">
                                <ItemTemplate>
                                    <asp:Button Text="درج نقص" runat="server" CommandName="naghs" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-warning" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تایید مشمول" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnTaeedMashmool" Text="تایید مشمول" Enabled="false" Visible="false" runat="server" CommandName="mashmoolApprove" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-warning" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاریخچه" Visible="true">
                                <ItemTemplate>
                                    <asp:Button ID="btnHistory" Text="تاریخچه"  Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-primary" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>--%>
                </div>
            </div>

            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>


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

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefreshGrid" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grd_CheckOutList" EventName="ItemCommand" />
            <asp:AsyncPostBackTrigger ControlID="grd_CheckOutList" EventName="ItemDataBound" />
            <asp:AsyncPostBackTrigger ControlID="grd_CheckOutList" EventName="NeedDataSource" />
            <asp:AsyncPostBackTrigger ControlID="drpUserRoles" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

   <%-- <script type="text/javascript">
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
    </script>--%>
</asp:Content>
