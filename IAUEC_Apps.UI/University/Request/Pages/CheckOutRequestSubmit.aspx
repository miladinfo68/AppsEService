<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/RequestCartMasterPage.Master" AutoEventWireup="true" CodeBehind="CheckOutRequestSubmit.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.CheckOutRequestSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .radio label, .checkbox label {
            margin-right: 20px;
            font-weight: bold;
        }

        th {
            text-align: center;
            font-size: 130%;
        }

        .well {
            margin-bottom: 0px;
        }

        .txtright {
            text-align: right !important;
        }

        .dashboard_graph .col-md-9,
        .dashboard_graph .col-md-3 {
            padding-right: 10px;
            padding-left: 10px;
        }

        .zindex999 {
            z-index: 999;
        }

        .rwContentRow {
            font-family: 'B Titr' !important;
        }

        .radfont {
            font-family: 'B Titr' !important;
        }

        .margin {
            margin: 10px;
        }
    </style>

    <script type="text/javascript">
        function openModal() {
            alert('d');
            $('#exampleModal').modal('show');
        }

    </script>
    <script type="text/javascript">
        function openModalStampPay() {
            jQuery('#dvStampPay').modal('show');
        }
    </script>
    <script type="text/javascript">
        function ShowWindow() {
            var oWnd = window.radopen('CheckOutRequestSubmit.aspx', 'window1');
        }
    </script>



    <link href="../MasterPages/css/js-persian-cal.css" rel="stylesheet" />
    <script src="../MasterPages/js/js-persian-cal.min.js"></script>
    <script type="text/javascript">

        $(function () {
            BindControlEvents();
        });
    </script>
    <script type="text/javascript">
        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
        }

        function walletPaymentCallback() {
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2 class="text-primary" runat="server" id="htitle">
        <asp:Label ID="lblTitle" runat="server" />
    </h2>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script lang="javascript" type="text/javascript">
        function postRefId(refIdValue) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
    <div dir="rtl">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <asp:Image ImageUrl="~/University/Theme/images/animatedEllipse.gif" Height="35px" Width="35px" runat="server" />
                <span>در حال بارگذاری...</span>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>







    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    ...
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>







    <div class="modal fade" id="dvStampPay" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="row">
                        <%--<div class="col-md-12">
                                    <div class="modal-title x_title">پرداخت تمبر</div>
                                    <div class="modal-title x_title">برگزاری دفاع دانشجو به صورت آنلاین</div>
                                </div>--%>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="container">

                        <div class="container-fluid py-3">

                            <div class="row">

                                <div class="col-md-12">
                                    <div id="pay-invoice" class="card">
                                        <div class="card-body">
                                            <div class="card-title">
                                                <h3 class="text-center">توجه :دانشجوی گرامی در این مرحله نسبت به پرداخت بدهی های زیر اقدامات لازم به عمل آورید 

                                                </h3>
                                            </div>
                                            <hr />
                                            <div class="form-group has-success" id="dvPayStamp" runat="server">
                                                <asp:CheckBox ID="chbStamp" Text="پرداخت هزینه تمبر(دو عدد)" runat="server" Checked="true" Enabled="false" />
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <input readonly id="txtStamp_payment" runat="server" name="cc-payment" type="text" class="form-control" value="" />

                                                    </div>
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-5">
                                                        <button id="BtnStampPay" runat="server" onserverclick="BtnStampPay_Click" class="btn btn-lg btn-success btn-block">
                                                            <i class="fa fa-lock fa-lg"></i>&nbsp;
                               
                                                            <span id="payment-button-amount_stmp">پرداخت</span>
                                                            <span id="payment-button-sending_stmp" style="display: none;">ارسال</span>
                                                        </button>
                                                    </div>
                                                </div>

                                            </div>
                                            <br />
                                            <hr />
                                            <br />
                                            <div class="form-group has-success" id="dvPayDefence" runat="server">
                                                <asp:CheckBox ID="chbDefence" Text="پرداخت هزینه برگزاری دفاع دانشجو به صورت آنلاین" runat="server" Checked="true" Enabled="false" />
                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <input readonly id="txtDefence_payment" runat="server" name="cc-payment" type="text" class="form-control" value="" />
                                                    </div>
                                                    <div class="col-md-2 "></div>
                                                    <div class="col-md-5">
                                                        <button id="btnDefencePay" runat="server" onserverclick="btnDefencePay_ServerClick" class="btn btn-lg btn-success btn-block">
                                                            <i class="fa fa-lock fa-lg"></i>&nbsp;
                               
                                            <span id="payment-button-amount_Def">پرداخت</span>
                                                            <span id="payment-button-sending_Def" style="display: none;">ارسال</span>
                                                        </button>
                                                    </div>


                                                </div>
                                            </div>
                                            <%--<div class="form-group">
                                                <label for="cc-name" class="control-label mb-1">جمع مبلغ قابل پرداخت</label>
                                                <input readonly id="txtTotalPay" runat="server" name="cc-payment" type="text" class="form-control" value="" />

                                            </div>--%>

                                            <div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmitCheckOutRequest" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnUploadThesis" EventName="Click" />

        </Triggers>


        <ContentTemplate>





            <script type="text/javascript">
                function refresgGrid() {
                    document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
                }
               
            </script>

            <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="refreshGrid" runat="server" />
            <asp:Label ID="lblHasThes" runat="server" Visible="false" Style="border: 1px solid rgba(59,131,255,0.9); padding: 1px; margin-top: 10px 0; margin-bottom: 10px 0; background-color: #ffd800;" Font-Size="12"></asp:Label><br />

            <div id="dvReqType" runat="server">
                <span>لطفا نوع درخواست تسویه حساب خود را مشخص نمایید :</span>
                <asp:DropDownList ID="drpCheckOutType" runat="server" CssClass="dropdown" OnSelectedIndexChanged="drpCheckOutType_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا نوع تسویه حساب را مشخص نمایید" Text="*" ForeColor="Red" ControlToValidate="drpCheckOutType" InitialValue="انتخاب کنید..."></asp:RequiredFieldValidator>
                <asp:Button ID="btnSubmitCheckOutRequest" Text="ثبت درخواست" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitCheckOutRequest_Click" OnClientClick="confirmAspButton(this); return false;" />
            </div>
            <div id="pnlAddMode" runat="server" class="alert" visible="false">


                <asp:Panel ID="pnlGraduate" runat="server" Visible="false">
                    <div id="dvPajoohesh" class="fa-border" runat="server">
                        <h4>تذکرات لازم جهت اطلاع
                            
                             دانشجو</h4>
                        <p class="text-danger"><strong>*توجه :</strong>دانشجوی گرامی ، به منظور شروع روند تسویه حساب فارغ التحصیلی شما ، باید ابتدا اطلاعات مربوط به دفاع و همچنین انصراف و یا پذیرش ارائه مقاله شما توسط پژوهش در سامانه ثبت شده باشد.</p>
                        <p>همچنین برای تسویه حساب پژوهشی می بایستی مدارک زیر را به حوزه پژوهش ارسال نمایید:</p>
                        <ol>
                            <li>در صورت درج اصلاحات در صورتجلسه دفاع، اخذ تأیید اساتید (راهنما، مشاور و داور) در فرم مربوطه و تحویل آن به پژوهش الزامیست.</li>


                            <asp:Label ID="lblPlzUplLoad" Text="***دانشجوی محترم لطفا پس از رسیدن فرایند تسویه حساب شما به بخش پژوهش،  نسبت به آپلود فایل پایان نامه با دو فرمت word و pdf اقدام نمایید.  نیازی به تحویل سی دی پایان نامه به حوزه معاونت پژوهشی نمی­باشد.***" runat="server" CssClass="text-success h5" Visible="true" />

                        </ol>
                    </div>

                    <%--         <a href="../Pages/PDF/arshad.pdf" target="_blank" style="color: red">مدارک مورد نیاز جهت فارغ التحصیلی ارشد</a><br />
                    <a href="../Pages/PDF/karshenasi.pdf" target="_blank" style="color: red">مدارک مورد نیاز جهت فارغ التحصیلی کارشناسی</a>--%>
                </asp:Panel>

                <div id="pnlEnseraf" runat="server" visible="false">
                    <div>
                        <h4>تذکرات لازم جهت انصراف :</h4>
                        <p>لطفا بخشنامه مربوط به انصراف ، اخراج و یا وقفه های تحصیلی را با دقت مطالعه نمایید.</p>
                        <a href="../Files/Enseraf.pdf" style="color: red">بخشنامه امور مربوط به وقفه تحصیلی ، انصراف از تحصیل و...</a>
                        <div class=" alert" id="dvGovahiEshteghal" runat="server" visible="false">
                            <p>توجه : دانشجوی گرامی در صورت قبولی در دیگر واحدهای دانشگاه آزاد تصویر گواهی اشتغال به تحصیل خود در آن واحد را بارگذاری نمایید.</p>
                            <p class="text-danger">حداکثر حجم 100 کیلوبایت</p>

                            <telerik:RadAsyncUpload ID="flpGovahiEshteghal" runat="server" OnClientValidationFailed="OnClientValidationFailed" Visible="false" OnFileUploaded="flpGovahiEshteghal_FileUploaded" TargetFolder="EshteghalBeTahsil" MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,gif,png" MaxFileSize="100000" Localization-Cancel="لغو" Localization-Remove="حذف" Localization-Select="انتخاب">
                            </telerik:RadAsyncUpload>
                        </div>
                        <br />
                        <asp:CheckBox runat="server" ID="chkAcceptTrems" Text="بخشنامه را مطالعه و شرایط آن را قبول دارم" />
                    </div>
                </div>

                <div id="pnlEnteghali" runat="server" visible="false">
                    <div>
                        <h4>تذکرات لازم جهت انصراف :</h4>
                        <p>لطفا بخشنامه مربوط به انصراف ، اخراج و یا وقفه های تحصیلی را با دقت مطالعه نمایید.</p>
                        <a href="../Files/Enseraf.pdf" style="color: red">بخشنامه امور مربوط به وقفه تحصیلی ، انصراف از تحصیل و...</a>
                        <br />
                        <asp:CheckBox runat="server" ID="CheckBox1" Text="بخشنامه را مطالعه و شرایط آن را قبول دارم" />
                    </div>
                </div>


            </div>
            <asp:Label ID="lblMessage" runat="server" Visible="false" Style="border: 1px solid #ff8500; padding: 1px; margin-top: 10px 0; margin-bottom: 10px 0; background-color: #ff9900; display: block; color: #000;" Font-Size="12"></asp:Label><br />
            <br />
            <asp:Label ID="lblmsg2" runat="server" Visible="false" Style="border: 1px solid rgba(59,131,255,0.9); padding: 1px; margin-top: 10px 0; line-height: 34px; margin-bottom: 10px 0; background-color: #ffd800;" Font-Size="12">
            </asp:Label><br />
            <br />
            <div id="dveditThes" runat="server" visible="false" style="border: 1px solid rgba(59,131,255,0.9); padding: 10px; margin: 10px 0; background-color: #ffd800;">
                <span>توضیحات مربوط به اصلاح پایان نامه : </span>
                <asp:Label ID="lblEditThes" runat="server" Visible="false" ForeColor="#666666" Font-Size="11"></asp:Label><br />
            </div>
            <asp:Panel runat="server">
                <div id="lnkCancelArticle" runat="server" visible="false" style="background-color: aqua; padding: 10px; margin-bottom: 10px;">
                    <a href="../Files/CancelForm.pdf" target="_blank" style="color: #5c824e">دانلود فرم انصراف از مقاله</a><br />
                </div>
                <div id="lnkEditThes" runat="server" visible="false" style="background-color: aqua; padding: 10px; margin-bottom: 10px;">
                    <a href="../Files/EditThes.pdf" target="_blank" style="color: #5c824e">دانلود فرم اصلاح پایان نامه</a>
                </div>
            </asp:Panel>

            <asp:Panel ID="Panel2" runat="server" Visible="false">
                <asp:Label ID="lblTicketing" runat="server" Text="">

                </asp:Label>
                <asp:Panel runat="server">
                    <div id="dvHelpGraduate" runat="server" style="background-color: lightyellow; padding: 10px; margin-bottom: 10px;">
                       <%-- <a href="../Files/arshad1.pdf" target="_blank" style="color: blue">مدارک مورد نیاز جهت فارغ التحصیلی مقطع کارشناسی ارشد</a><br />
                        <a href="../Files/karshenasi1.pdf" target="_blank" style="color: blue">مدارک مورد نیاز جهت فارغ التحصیلی مقطع کارشناسی</a>--%>
                        <a href="PDF/بخشنامه تعرفه هزینه.pdf" target="_blank" style="color: blue">بخشنامه تعرفه هزینه</a>
                        <br />
                        <a href="PDF/مدارک لازم جهت فارغ التحصیلی.docx" target="_blank" style="color: blue">مدارک لازم جهت فارغ التحصیلی</a>
                    </div>
                </asp:Panel>
                <asp:GridView ID="grdPreviousReq" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowDataBound="grdPreviousReq_RowDataBound" OnRowCommand="grdPreviousReq_RowCommand">
                    <HeaderStyle CssClass=" bg-blue text-center" />
                    <Columns>
                        <asp:BoundField HeaderText="تاریخ ثبت درخواست" ItemStyle-CssClass="text-center" DataField="CreateDate" />
                        <asp:TemplateField HeaderText="وضعیت درخواست" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBoxList ID="chkStatus" runat="server" RepeatColumns="4" CssClass="checkbox" Enabled="false" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                <asp:Label ID="lblDeny" runat="server" Text="" ForeColor="Red" Font-Size="Medium" Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="پیام سیستم" DataField="Message" ItemStyle-HorizontalAlign="Center" />

                        <asp:TemplateField HeaderText="عملیات">

                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdnReqId" Value='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>'></asp:HiddenField>
                                <asp:Button ID="btnSubmitMsg" runat="server" Text="ارسال پیام" CssClass="btn btn-info" CommandName="msg" CommandArgument='<%# Eval("StudentRequestID") %>' Visible="false" />

                                <asp:Button ID="btnHistory" runat="server" Text="تاریخچه پیام ها" CssClass="btn btn-info" CommandName="history" CommandArgument='<%# Eval("StudentRequestID") %>' />
                            </ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <asp:Label ID="lblTakmil" Text="دانشکده در حال تکمیل پرونده میباشد" runat="server" CssClass="text-danger h3" Visible="false" />

                <div class="row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnNewRequest" runat="server" Visible="false" CssClass="btn btn-success" Text="درخواست جدید" OnClick="btnNewRequest_Click" />
                    </div>
                    <div class="col-md-5">
                    </div>
                </div>

                <br />

            </asp:Panel>
            <br />

            <asp:Panel ID="pnlThesisInfo" runat="server" CssClass="fa-border panel-success" Visible="false">
                <div class="container">
                    <table class="table col-md-6 table-bordered table-striped">
                        <tr class="bg-green">
                            <td colspan="2">آپلود پایان نامه</td>
                        </tr>
                        <tr>
                            <td>
                                <p>لطفا آیین نامه نگارش پایان نامه را با دقت مطالعه نمایید:</p>
                                <%-- <a href="../Files/IAUEC Msc Template.docx" target="_blank" style="color: red">نمونه فایل پایان نامه</a><br />
                                <a href="../Files/WriteBuffer.pdf" target="_blank" style="color: red">آیین نامه نگارش پایان نامه</a><br />--%>
                                <a href="PDF/آيين نامه نگارش پایان نامه ها.docx" target="_blank" style="color: red">آيين نامه نگارش پایان نامه ها</a><br />
                                <a href="PDF/آيين نگارش پايان نامه براي رشته كارشناسي ارشد  زبان و ادبيات انگليسي.docx" target="_blank" style="color: red">آيين نگارش پايان نامه براي رشته كارشناسي ارشد  زبان و ادبيات انگليسي</a><br />
                                <a href="PDF/راهنماي نگارش پايان نامه رشته آموزش زبان انگليسي.docx" target="_blank" style="color: red">راهنماي نگارش پايان نامه رشته آموزش زبان انگليسي</a><br />
                                <a href="PDF/راهنماي نگارش پايان نامه رشته مترجمي زبان انگليسي.docx" target="_blank" style="color: red">راهنماي نگارش پايان نامه رشته مترجمي زبان انگليسي</a><br />
                                <a href="PDF/تعهد نامه اصالت رساله یا پایان نامه.docx" target="_blank" style="color: red">تعهد نامه اصالت رساله یا پایان نامه</a><br />
                                <a href="PDF/فرم تایید استاد راهنما  قبل از 95.docx" target="_blank" style="color: red">فرم تایید استاد راهنما  قبل از 95</a><br />
                                <a href="PDF/فرم تاييد استاد راهنما  مهر 95 به بعد.docx" target="_blank" style="color: red">فرم تاييد استاد راهنما  مهر 95 به بعد</a><br />
                                <a href="PDF/منشور اخلاقی.docx" target="_blank" style="color: red">منشور اخلاقی</a><br />
                                <a href="PDF/نمونه پايان نامه برای  رشته های حقوق.docx" target="_blank" style="color: red">نمونه پايان نامه برای  رشته های حقوق</a><br />
                                <a href="PDF/نمونه پايان نامه.docx" target="_blank" style="color: red">نمونه پايان نامه</a><br />
                                <p>توجه : دانشجوی گرامی در صورت مطالعه آیین نامه نگارش پایان نامه و رعایت مندرجات فایل پایان نامه خود را بارگذاری نمایید.</p>
                                <asp:CheckBox runat="server" ID="ChkUpProp" Text="آیین نامه نگارش پایان نامه را مطالعه و شرایط آن را قبول دارم" OnCheckedChanged="ChkUpProp_CheckedChanged" AutoPostBack="true" />
                                <div class="row" id="divUploadThesis" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <h5>جهت بارگذاري فايل پايان نامه ابتدا فايل را با نام انگليسي ذخيره نموده و سپس نسبت به بارگذاري آن در سامانه اقدام نماييد</h5>
                                                <h5 style="color: red">لطفا تا زمان سبز شدن چراغ چشمک زن آپلود فایل منتظر بمانید.
                                                    در صورتی که رنگ دایره قرمز است، امکان آپلود فایل وجود ندارد(به فرمت فایل دقت فرمایید) در صورت زرد بودن دایره، منتظر بمانید تا فایل به صورت کامل آپلود شود و به رنگ سبز درآید(سرعت اینترنت شماو حجم فایل در حال آپلود در سریعتر آپلود شدن فایل شما موثر است).  </h5>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-md-3"><span>فایل پایان نامه با فرمت doc یا docx</span></div>
                                            <div class="col-md-6">
                                                <telerik:RadAsyncUpload ID="thesisDocUpload" runat="server" OnClientValidationFailed="OnClientValidationFailed" OnFileUploaded="thesisDocUpload_FileUploaded" TargetFolder="PayanNameh" MaxFileInputsCount="1" AllowedFileExtensions="doc,docx" Localization-Cancel="لغو" Localization-Remove="حذف" Localization-Select="انتخاب">
                                                </telerik:RadAsyncUpload>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">فایل پایان نامه با فرمت pdf</div>
                                            <div class="col-md-6">
                                                <telerik:RadAsyncUpload ID="thesisPdfUpload" runat="server" OnClientValidationFailed="OnClientValidationFailed" OnFileUploaded="thesisPdfUpload_FileUploaded" TargetFolder="PayanNameh" MaxFileInputsCount="1" AllowedFileExtensions="pdf" Localization-Cancel="لغو" Localization-Remove="حذف" Localization-Select="انتخاب">
                                                </telerik:RadAsyncUpload>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-6"></div>
                                    <div class="col-md-6"></div>
                                    <%--<p class="text-danger">حداکثر حجم 500 کیلوبایت</p>--%>
                                    <asp:Button runat="server" ID="btnUploadThesis" Text="ارسال پایان نامه" CssClass="btn-success" OnClick="btnUploadThesis_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlBestankar" runat="server" Visible="false" CssClass="fa-border">
                <div dir="rtl" class="col-md-6 form-group">
                    <div>
                        <asp:Label runat="server" CssClass="text-danger">دانشجوی گرامی : شما از دانشگاه بستانکار می باشید ، لطفا به منظور عودت وجه اطلاعات زیر را تکمیل نمایید</asp:Label>

                    </div>
                    <table class="table col-md-6 table-bordered table-striped">
                        <tr class="bg-blue">
                            <td colspan="2">مشخصات حساب شما</td>
                        </tr>
                        <tr>
                            <td>نوع حساب :</td>
                            <td>
                                <asp:RadioButtonList runat="server" ID="rdblBankType" AutoPostBack="true" OnSelectedIndexChanged="rdblBankType_SelectedIndexChanged">
                                    <asp:ListItem Text="بانک ملی" Value="1" />
                                    <asp:ListItem Text="دیگر بانک ها" Value="2" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="dvBankeMeli" runat="server" visible="false">
                            <td>
                                <asp:Label Text="شماره حساب سیبا بانک ملی :" runat="server" /></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtBankMeliID" CssClass="form-control" />
                                <br />
                                <asp:RequiredFieldValidator ErrorMessage="ثبت شماره حساب الزامی است" ControlToValidate="txtBankMeliID" runat="server" ValidationGroup="hesab" Display="Dynamic" />
                                <asp:RegularExpressionValidator ErrorMessage="شماره حساب بانک ملی باید 13 رقم باشد." ForeColor="Red" ValidationGroup="hesab" ControlToValidate="txtBankMeliID" ValidationExpression="^[0-9]{13}$" runat="server" />
                            </td>
                        </tr>
                        <tr id="dvBankOther" runat="server" visible="false">
                            <td>
                                <asp:Label Text="شماره شبا حساب بانکی :" runat="server" />
                                <br />
                                <br />
                                <br />
                                <asp:Label Text="نام بانک :" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtSheba" CssClass="form-control" />
                                <span>مثال : IR123456789123456789123456</span>
                                <asp:RequiredFieldValidator ErrorMessage="درج شماره شبا الزامی است" ControlToValidate="txtSheba" runat="server" ValidationGroup="hesab" ForeColor="Red" Display="Dynamic" />
                                <asp:RegularExpressionValidator ErrorMessage="شماره شبا وارد شده اشتباه می باشد" ControlToValidate="txtSheba" ValidationExpression="^[I][R][0-9]{24}$" runat="server" ValidationGroup="hesab" ForeColor="Red" Display="Dynamic" />
                                <br />
                                <asp:TextBox runat="server" ID="txtBankName" CssClass="form-control" />
                                <asp:RequiredFieldValidator ErrorMessage="وارد کردن نام بانک الزامی می باشد." ControlToValidate="txtBankName" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="hesab" />
                                <asp:RegularExpressionValidator ErrorMessage="نام بانک وارد شده دارای کاراکتر غیر مجاز می باشد." ControlToValidate="txtBankName" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="hesab" ValidationExpression="^[\u0600-\u06FF\s]{1,100}$" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="نام صاحب حساب :" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtAcountOwner" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ErrorMessage="وارد کردن نام صاحب حساب الزامی است." ControlToValidate="txtAcountOwner" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="hesab" />
                                <asp:RegularExpressionValidator ErrorMessage="نام وارد شده شامل کاراکتر غیر مجاز است" ControlToValidate="txtAcountOwner" runat="server" ValidationExpression="^[\u0600-\u06FF\s]{1,100}$" ForeColor="Red" Display="Dynamic" ValidationGroup="hesab" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label Text="شماره تلفن همراه :" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ErrorMessage="درج شماره تلفن الزامی می باشد" ControlToValidate="txtPhoneNumber" runat="server" ForeColor="Red" Display="Dynamic" ValidationGroup="hesab" />
                                <asp:RegularExpressionValidator ErrorMessage="شماره تلفن همراه وارد شده اشتباه است " ControlToValidate="txtPhoneNumber" runat="server" ForeColor="Red" Display="Dynamic" ValidationExpression="^09\d{9}$" ValidationGroup="hesab" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="text-center">
                                <asp:Button Text="ثبت حساب" runat="server" ID="btnSubmitAcount" OnClick="btnSubmitAcount_Click" ValidationGroup="hesab" CssClass="btn btn-danger" /></td>
                        </tr>
                    </table>
                </div>

            </asp:Panel>
            <asp:Panel ID="pnlBedehkar" runat="server" Visible="false">
                <div class=" container">
                    <div>
                        <asp:Label runat="server" CssClass="text-danger">*دانشجوی گرامی : شما به شرح جدول زیر به دانشگاه بدهکار می باشید ، لطفا به منظور ادامه روند تسویه حساب خود نسبت به تسویه این بدهی ها اقدام نمایید.</asp:Label>
                        <p class="text-danger">*توجه : در صورت بدهي به صندوق رفاه بابت وام هاي دريافتي از صندوق (همه وامها به استثنا وام وزارت علوم)مبلغ بدهي خود را به<span class="text-primary"> حساب شماره 0105409699003 </span>بنام واحدالكترونيكي پرداخت نماييد . لازم به ذكر است درج شناسه پرداخت بروي فيش بانكي ضروري مي باشد.</p>

                    </div>
                    <br />
                    <asp:GridView ID="grdBedehi" CssClass="table table-bordered table-condensed " AutoGenerateColumns="false" runat="server" OnRowCommand="grdBedehi_RowCommand" OnRowDataBound="grdBedehi_RowDataBound">
                        <HeaderStyle CssClass="bg-primary text-center" />
                        <RowStyle CssClass="text-center" />
                        <Columns>
                            <asp:BoundField HeaderText="نوع بدهی" DataField="DebitTypeID" />
                            <asp:BoundField HeaderText="شماره / تعداد " DataField="DebitNumber" />
                            <asp:BoundField HeaderText="مبلغ بدهی" DataField="DebitAmount" />
                            <asp:BoundField HeaderText="شماره فیش" DataField="FishNumber" />
                            <asp:BoundField HeaderText="تاریخ فیش" DataField="FishDate" />
                            <asp:BoundField HeaderText="توضیحات" DataField="Note" />
                            <asp:TemplateField HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button ID="btnSubmitFish" Text=" ثبت فیش" CommandName="submitFish" CommandArgument='<%#Eval("RefID") %>' CssClass="btn btn-danger" ValidationGroup="fish" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <div class="container" id="dvVezaratLoan" runat="server" visible="false" onload="dvVezaratLoan_Load">
                <div class="container">
                    <h4 class="text-danger">*توجه :دانشجوی گرامی شما به دلیل استفاده از وام وزارت علوم باید مشخصات تمامی مقاطع تحصیلی قبلی خود بعد از دیپلم را در جدول زیر درج نمایید.</h4>
                    <h4 class="text-danger">لطفا به موارد زیر توجه نمایید : </h4>
                    <ul>
                        <li>دانشجوياني كه از وام بلند مدت وزارت علوم استفاده نموده اند:
                            <ol>
                                <li>دانشجویانی که از وام وزارت علوم استفاده کرده اند می بایست 10% مبلغ وام را پرداخت نمایند</li>
                                <li>براي كليه پرداخت هاي مربوط به وزارت علوم  به سايت ذيل قسمت پورتال دانشجويي مراجعه نمايند.كد كاربري و كلمه عبور كد ملي دانشجو (با خط تيره) مي باشد.
                                    <a href="http://www.Swf.ir" style="color: blue">www.Swf.ir</a>
                                </li>
                                <li>دفترچه اقساط حداقل 48 ساعت بعد از پرداخت 10% صادر مي گردد</li>
                                <li>دانشجويان انصرافي و اخراجي مبلغ وام را به همراه كارمزد يكجا تسويه نمايند</li>
                                <li>حداكثر تا سه ماه بعد از تاريخ فراغت از تحصيل ، جهت مشخص نمودن وضعيت وام بلند مدت فرصت داريد.</li>
                                <li>شروع اقساط وام بلند مدت 9 ماه بعد از فارغ التحصيلي مي باشد وبراي دانشجويان مشمول 9 ماه پس از پايان خدمت سربازي مي باشد.در صورتيكه دانشجويان مشمول تمايل به شروع قسط داسته باشند طي درخواست كتبي براي آنان غيرمشمول ثبت مي شود.</li>
                            </ol>
                        </li>
                        <li>تذكر: دانشجوياني كه براي ضمانت وام چك تحويل داده اند بعد از تسويه وام جهت دريافت چك به صندوق رفاه مراجعه نمايند.</li>
                    </ul>
                    <div id="dvMaghtaBTN" runat="server" class="panel panel-danger">
                        <div class="panel-heading">
                            <h4>مشخصات مقاطع تحصیلی پیشین</h4>
                        </div>
                        <div class="panel-body well">
                            <p class="text-danger">اطلاعات ذيل مربوط به مقاطع تحصيلي قبلي مي باشد.(مسئوليت صحت اطلاعات پر شده به عهده دانشجو مي باشد.)</p>
                            <asp:GridView ID="grdPastMaghta" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                <HeaderStyle CssClass=" bg-primary  " />
                                <Columns>
                                    <asp:BoundField DataField="UniName" HeaderText="نام دانشگاه" />
                                    <asp:BoundField DataField="FieldName" HeaderText="نام رشته" />
                                    <asp:BoundField DataField="UniType" HeaderText="نوع دوره" />
                                    <asp:BoundField DataField="maghta" HeaderText="مقطع تحصیلی" />
                                    <asp:BoundField DataField="FeraghatYear" HeaderText=" سال فراغت" />
                                    <asp:BoundField DataField="nimsal" HeaderText="نیمسال فراغت" />
                                    <asp:BoundField DataField="feraghatType" HeaderText="نوع پایان تحصیل" />
                                    <asp:BoundField DataField="CheckOutStatus" HeaderText=" نوع تسویه" />
                                    <asp:BoundField DataField="FeraghatDate" HeaderText="تاریخ فراغت" />
                                    <asp:BoundField DataField="Ismashmool" HeaderText="وضعیت نظام وظیفه" />
                                    <asp:BoundField DataField="LoanAmount" HeaderText=" مبلغ وام" />
                                </Columns>
                            </asp:GridView>
                            <div>
                                <asp:Button ID="btnShowMaghat" Text="ثبت مقطع" CssClass="btn btn-danger" OnClick="btnShowMaghat_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="dvAddress" runat="server" class="container">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h4>آدرس فعلی</h4>
                        </div>
                        <div class="panel-body well">

                            <div class="container">
                                <p class="text-success">لطفا آدرس فعلی خود را به طور کامل در فرم زیر درج نمایید.</p>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="استان :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="drpProvince" ForeColor="Red" ValidationGroup="address" Display="Dynamic" InitialValue="انتخاب کنید" runat="server" ErrorMessage="لطفا استان محل تحصیل قبلی خود را انخاب کنید"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="drpProvince" runat="server" CssClass="form-control dropdown" OnSelectedIndexChanged="drpProvince_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="شهر :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="drpCity" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="انتخاب کنید" ErrorMessage="لطفا شهر محل تحصیل قبلی خود را انتخاب کنید"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="drpCity" CssClass="form-control dropdown" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <asp:Label Text="خیابان :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtStreet" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="درج خیابان اجباری است"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="^[A-Za-z0-9\u0600-\u06FF\s _]*[A-Za-z0-9\u0600-\u06FF\s][A-Za-z0-9\u0600-\u06FF\s _]{1,200}$" ControlToValidate="txtStreet" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="آدرس خیابان فقط می تواند شامل حروف و اعداد باشد"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtStreet" CssClass=" form-control" runat="server" ToolTip="مثال : خیابان آزادی خیابان یادگار امام" MaxLength="150" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="کوچه :" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[A-Za-z0-9\u0600-\u06FF\s _]*[A-Za-z0-9\u0600-\u06FF\s][A-Za-z0-9\u0600-\u06FF\s _]{0,100}$" ControlToValidate="txtAlley" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="آدرس کوچه فقط می تواند شامل حروف و اعداد باشد"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtAlley" runat="server" CssClass=" form-control" MaxLength="100" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="پلاک :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtPelak" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="درج پلاک الزامی است"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationExpression="^[A-Za-z0-9\u0600-\u06FF\s _]*[A-Za-z0-9\u0600-\u06FF\s][A-Za-z0-9\u0600-\u06FF\s _]{0,100}$" ControlToValidate="txtPelak" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="پلاک فقط می تواند شامل حروف و اعداد باشد"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtPelak" runat="server" CssClass=" form-control" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="کد پستی :" runat="server" />
                                    <asp:RequiredFieldValidator Display="Dynamic" ErrorMessage="لطفا کد پستی محل سکونت خود را وارد نمایید" ControlToValidate="txtZipCode" runat="server" ForeColor="Red" ValidationGroup="address" />
                                    <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="کد پستی باید 10 رقم عدد بدون هیچ کاراکتر اضافی باشد" ControlToValidate="txtZipCode" runat="server" ForeColor="Red" ValidationGroup="address" ValidationExpression="^[0-9]{10}$" />
                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass=" form-control" ToolTip="مثال : 134567890" MaxLength="10" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="شماره تلفن ثابت(به همراه کد شهر) :" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtTel" ValidationExpression="^0[1-9]{1,3}[0-9]{5,8}$" ForeColor="Red" ValidationGroup="LastUni" Display="Dynamic" runat="server" ErrorMessage="شماره تلفن ثابت وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtTel" ForeColor="Red" ValidationGroup="address" Display="Dynamic" runat="server" ErrorMessage="درج شماره تلفن ثابت الزامی است"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtTel" runat="server" CssClass=" form-control" ToolTip="مثال : 02184326000" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="شماره تلفن همراه :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtMobile" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="درج تلفن همراه الزامی است"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationExpression="^09\d{9}$" ControlToValidate="txtMobile" ValidationGroup="LastUni" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="تلفن همراه وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass=" form-control" ToolTip="مثال : 09121234567" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="پست الکترونیکی :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtEmail" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="درج پست الکترونیکی الزامی است"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ControlToValidate="txtEmail" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="پست الکترونیکی وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass=" form-control" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="شماره تلفن همراه رابط :" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txtRabetMobile" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="درج تلفن همراه الزامی است"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationExpression="^09\d{9}$" ControlToValidate="txtRabetMobile" ValidationGroup="address" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="تلفن همراه وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtRabetMobile" runat="server" CssClass=" form-control" />
                                </div>
                                <div class="form-group col-md-3">
                                    <asp:Label Text="شماره تلفن ثابت رابط(به همراه کد شهر) :" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtRabetTel" ValidationExpression="^0[1-9]{1,3}[0-9]{5,8}$" ForeColor="Red" ValidationGroup="address" Display="Dynamic" runat="server" ErrorMessage="شماره تلفن ثابت وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtRabetTel" ForeColor="Red" ValidationGroup="address" Display="Dynamic" runat="server" ErrorMessage="درج شماره تلفن ثابت الزامی است"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtRabetTel" runat="server" CssClass=" form-control" />
                                </div>
                            </div>

                            <asp:Button ID="btnPastUniInfoSubmit" Text="ثبت اطلاعات" runat="server" CssClass="btn btn-success" OnClick="btnPastUniInfoSubmit_Click" ValidationGroup="address" />
                        </div>

                    </div>
                </div>
            </div>


            <asp:Panel ID="pnlFeraghatTahsil" runat="server" Visible="false">
                <br />
                <div class=" fa-border" style="padding: 10px">
                    <div class="row bg-blue" style="padding: 5px">
                        <h4>وضعیت مدارک شما</h4>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                            <table class="table table-bordered table-condensed">
                                <tr class="bg-green">
                                    <td>نام مدرک</td>
                                    <td>وضعیت</td>
                                </tr>
                                <tr>
                                    <td>گواهی موقت</td>
                                    <td>
                                        <asp:Label ID="lblGovahiMovaghat" Text="در دست تهیه" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>دانشنامه</td>
                                    <td>
                                        <asp:Label ID="lblDaneshNameh" Text="در دست تهیه" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>ریز نمرات</td>
                                    <td>
                                        <asp:Label ID="lblRizNomarat" Text="در دست تهیه" runat="server" />
                                    </td>
                                </tr>


                            </table>
                        </div>
                    </div>
                </div>
                <br />

                <br />
                </div>
            </asp:Panel>
            <div></div>

            <div id="divTel" runat="server" style="background-color: #B1E5FF; padding: 10px; margin-bottom: 10px;" visible="false">
                <div class="row margin">
                    <h5 style="margin-right: 10px; color: #0051FD;">قابل توجه دانشجویان عزیز در شرف فراغت از تحصیل، نظر به لزوم کنترل، بررسی و تأیید مدارک متعدد قبل از اعلام فارغ التحصیلی از منظر آموزشی، مهلتی برای انجام امور فوق الذکر در نظر گرفته شده است. لذا خواهشمند است در صورت عدم ارسال پرونده به اداره فارغ التحصیلان واحد طی مدت 25 روز کاری از زمان ثبت درخواست، با شماره های ذیل جهت پیگیری پرونده خویش تماس حاصل فرمایید.</h5>
                    <h4 style="margin-right: 10px; color: #0051FD;">شماره تماس داخلی دانشکده ها:</h4>
                </div>
                <div class="row margin">
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label13" Text="*دانشکده فنی و مهندسی" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label11" Text="فارغ التحصیلان:283" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label12" Text="بایگانی :163" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label7" Text="پژوهش :393" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <br />
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label2" Text="* دانشکده مدیریت" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label14" Text="فارغ التحصیلان:211" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label15" Text="بایگانی:162" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label8" Text="پژوهش :391" Style="color: #0051FD;"></asp:Label>
                    </div>

                    <br />

                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label3" Text="*دانشکده علوم انسانی" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label6" Text="فارغ التحصیلان:644" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label16" Text="بایگانی :161" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label9" Text="پژوهش :395" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <br />
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label17" Text="* دانشکده علوم پایه" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label18" Text="فارغ التحصیلان:352" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label19" Text="بایگانی:232" Style="color: #0051FD;"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label runat="server" ID="label10" Text="پژوهش :393" Style="color: #0051FD;"></asp:Label>
                    </div>
                </div>

            </div>
            <div id="divDetails" class="bg-danger" style="padding: 10px" runat="server">
                <div style="margin: 10px;" class="text-danger">
                    <asp:Literal runat="server" ID="lblRequestState1">
                        * توجه : مرحله ای که با کادر قرمز مشخص شده مرحله فعلی درخواست شما می باشد .در صورت نیاز به ارسال پیام برای کارشناس مربوطه از 
                    </asp:Literal>
                    <asp:HyperLink ID="ticketLink" runat="server" NavigateUrl="http://support.iauec.ac.ir/">سامانه تیکتینگ</asp:HyperLink>
                    <asp:Literal runat="server" ID="Literal1">
                        استفاده نمایید
                    </asp:Literal>
                    <%--   <asp:Label ID="321" Text="* توجه : مرحله ای که با کادر قرمز مشخص شده مرحله فعلی درخواست شما می باشد .در صورت نیاز به ارسال پیام برای کارشناس مربوطه از سامانه تیکتینگ استفاده کنید" runat="server" CssClass="text-danger" />--%>
                </div>
                <div style="margin: 10px;">
                    <asp:Label ID="lblEkhrajEnseraf" CssClass="text-danger" Text="" runat="server" Visible="false" />
                </div>
                <div style="margin: 10px;">
                    <asp:Label ID="lbldaneshjooyi" CssClass="text-danger" Text="" runat="server" />
                </div>
                <div style="margin: 10px;">
                    <asp:Label ID="lblNezamVazife" Visible="false" Text="*توجه: چنانچه از معافیت تحصیلی استفاده نموده اید نامه لغو معافیت شخصا به خود دانشجو پس از اخذ رسید داده می شود." CssClass="text-danger" runat="server" />
                </div>
                <div style="margin: 10px;">
                    <asp:Label ID="Label20" Text="* قابل توجه دانشجويان محترم داراي درخواست فراغت از تحصيل، نظر به لزوم كنترل، بررسي و تاييد مدارك قبل از اعلام فارغ التحصيلي از لحاظ آموزشي، مهلتي براي انجام امور فوق الذكر در نظر گرفته شده است. لذا خواهشمند است در صورت عدم ارسال پرونده به اداره فارغ التحصيلان واحد طي مدت 25 روز كاري از زمان ثبت درخواست، با بخش موردنظر صرفاً از طريق تيكت در ارتباط باشيد." runat="server" CssClass="text-danger" />
                </div>

            </div>
            <div dir="ltr">
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" ViewStateMode="Disabled" Height="50px" Width="345px">
                    <Windows>
                        <telerik:RadWindow ID="RadWindow1" runat="server" RegisterWithScriptManager="true" Width="375" Height="475" OnClientShow="SetDatePicker" Modal="true" VisibleOnPageLoad="false">
                            <ContentTemplate>
                                <div class="container" dir="rtl">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div class="alert alert-info">
                                                <span>نوع بدهی :</span>
                                                <asp:Label ID="lblDebitTypeName" runat="server" />
                                                &nbsp&nbsp&nbsp&nbsp&nbsp
                                            <span>مبلغ بدهی :</span>
                                                <asp:Label ID="lblAmount" runat="server" />
                                                <asp:HiddenField ID="hfDebit" runat="server" />
                                            </div>
                                            <div class="form-group">
                                                <p>شماره فیش :</p>
                                                <asp:TextBox ID="txtFishNumber" runat="server" CssClass="form-control" />

                                                <p>تاریخ فیش :</p>
                                                <asp:TextBox ID="txtFishDate" runat="server" CssClass="form-control pcal" />

                                                <p>توضیحات :</p>
                                                <asp:TextBox ID="txtDebitNote" runat="server" CssClass="form-control" />
                                                <asp:Label ID="lblFishError" Visible="false" ForeColor="Red" runat="server" />
                                                <asp:Button ID="btnSubmitFish" Text="ثبت فیش" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitFish_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <telerik:RadWindow runat="server" ID="RadWindow2" RegisterWithScriptManager="true" VisibleOnPageLoad="false" OnClientClose="RadWindow2_Close">
                            <ContentTemplate>
                                <div dir="rtl" style="margin: 0px auto;">
                                    <asp:Label ID="Label1" Text="متن پیام" runat="server" />
                                    <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" MaxLength="999"></asp:TextBox>
                                    <asp:Button ID="btnSubmitMsg" runat="server" Text="ثبت و ارسال پیام" CssClass="btn btn-success" OnClick="btnSubmitMsg_Click" />
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <telerik:RadWindow runat="server" MinHeight="300px" MinWidth="650px" MaxHeight="300px" MaxWidth="650px" Width="650px" Height="300px" ID="radConfirm" VisibleOnPageLoad="false">
                            <ContentTemplate>
                                <div dir="rtl">
                                    |<div class="row bg-danger" style="padding: 5px">
                                        <div class="col-md-5"></div>
                                        <div class="col-md-4">
                                            <h5>تایید درخواست تسویه</h5>
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-1"></div>
                                        <div class="col-md-10">
                                            <asp:Label ID="Label4" Font-Size="Medium" Text="دانشجوی گرامی شما درخواست تسویه " runat="server" />
                                            <asp:Label ID="lblConfirmMessage" Font-Size="Large" Text="" runat="server" />
                                            <asp:Label ID="Label5" Font-Size="Medium" Text=" را دارید. آیا مطمئن هستید؟" runat="server" />
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                    <div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-md-5"></div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnConfirmCancel" runat="server" Text="لغو" CssClass="btn btn-danger" />
                                                <asp:Button ID="btnConfirmOk" runat="server" Text="تایید" CssClass="btn btn-success" OnClick="btnConfirmOk_Click" />
                                            </div>
                                            <div class="col-md-3"></div>
                                        </div>
                                    </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                        <telerik:RadWindow ID="RadWindow3" runat="server" RegisterWithScriptManager="true" Width="970" Height="650" OnClientShow="SetDatePicker" ViewStateMode="Disabled" EnableViewState="false" Modal="true" VisibleOnPageLoad="false">
                            <ContentTemplate>
                                <div class="container" dir="rtl" style="padding: 10px">
                                    <div style="margin: 5px;">
                                        <div class="bg-blue-sky">
                                            <h3>مشخصات مقاطع قبلی</h3>
                                        </div>
                                        <div style="padding: 5px;">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <span>نام مرکز آموزشی :</span>
                                                    <asp:RequiredFieldValidator ErrorMessage="درج نام مرکز آمزشی قبلی الزامی می باشد" ControlToValidate="txtUniName" runat="server" ForeColor="Red" ID="vldUniName" ValidationGroup="LastUni" Display="Dynamic" />
                                                    <asp:RegularExpressionValidator ErrorMessage="نام دانشگاه دارای کارکتر غیر مجاز می باشد" ValidationExpression="^[A-Za-z0-9\u0600-\u06FF\s _]*[A-Za-z0-9\u0600-\u06FF\s][A-Za-z0-9\u0600-\u06FF\s _]{2,200}$" ControlToValidate="txtUniName" runat="server" ForeColor="Red" ID="vldUniNameEx" ValidationGroup="LastUni" Display="Dynamic" />
                                                    <asp:TextBox ID="txtUniName" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="col-md-6">
                                                    <span>نام رشته :</span>
                                                    <asp:RequiredFieldValidator ErrorMessage="درج نام رشته پیشین الزامی می باشد" ControlToValidate="txtFieldName" runat="server" ForeColor="Red" ID="vldFieldName" ValidationGroup="LastUni" Display="Dynamic" />
                                                    <asp:RegularExpressionValidator ErrorMessage="نام رشته دارای کارکتر غیر مجاز می باشد" ValidationExpression="^[A-Za-z0-9\u0600-\u06FF\s _]*[A-Za-z0-9\u0600-\u06FF\s][A-Za-z0-9\u0600-\u06FF\s _]{2,200}$" ControlToValidate="txtFieldName" runat="server" ForeColor="Red" ID="RegularExpressionValidator12" ValidationGroup="LastUni" Display="Dynamic" />
                                                    <asp:TextBox ID="txtFieldName" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-md-4">
                                                    <asp:Label Text="نوع دوره :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdblLastUniType" ForeColor="Red" ErrorMessage="نوع دوره قبلی را انتخاب کنید" ValidationGroup="LastUni"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblLastUniType" runat="server" CssClass="radio" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="روزانه" Value="1" />
                                                        <asp:ListItem Text="شبانه" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label Text="مقطع تحصیلی :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="مقطع تحصیلی قبلی را انتخاب کنید" ForeColor="Red" ControlToValidate="rdblMaghta" ValidationGroup="LastUni" ViewStateMode="Disabled"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblMaghta" runat="server" CssClass="radio" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="4">
                                                        <asp:ListItem Text="کاردانی" Value="2" />
                                                        <asp:ListItem Text="کارشناسی" Value="1" />
                                                        <%--<asp:ListItem Text="کارشناسی ناپیوسته" Value="3" />--%>
                                                        <asp:ListItem Text="کارشناسی ارشد پیوسته" Value="4" />
                                                        <%--<asp:ListItem Text="کارشناسی ارشد ناپیوسته" Value="5" />--%>
                                                        <%--<asp:ListItem Text="دکتری تخصصی" Value="7" />--%>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div id="dvMadrakType" runat="server" class="form-group col-md-4">
                                                    <asp:Label Text="نوع مدرک :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="rdblMadrakType" ForeColor="Red" ErrorMessage="نوع مدرک قبلی را انتخاب کنید" ValidationGroup="LastUni"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblMadrakType" runat="server" RepeatDirection="Horizontal" CssClass="radio">
                                                        <asp:ListItem Text="پیوسته" Value="1" />
                                                        <asp:ListItem Text="ناپیوسته" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="form-group col-md-3 ">
                                                    <asp:Label Text="سال فراغت از تحصیل :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="درج سال فراغت از تحصیل الزامی است" ForeColor="Red" ControlToValidate="txtFareghYear" ValidationGroup="LastUni" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtFareghYear" ValidationGroup="LastUni" ForeColor="Red" ValidationExpression="^[0-9]{4}$" runat="server" ErrorMessage="سال وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtFareghYear" runat="server" CssClass="form-control" ToolTip="مثال : 1394" />
                                                </div>
                                                <div class="form-group col-md-3  ">
                                                    <asp:Label Text="نیمسال فراغت از تحصیل :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red" ControlToValidate="rdblNimsalFeraghat" Display="Dynamic" ValidationGroup="LastUni" ErrorMessage="نیمسال فراغت از تحصیل را انتخاب کنید"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblNimsalFeraghat" runat="server" CssClass="radio" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="نیمسال اول" Value="1" />
                                                        <asp:ListItem Text="نمیسال دوم" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-3 ">
                                                    <asp:Label Text="نوع پایان تحصیل :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rdblStudyEndType" ForeColor="Red" ValidationGroup="LastUni" Display="Dynamic" runat="server" ErrorMessage="نوع پایان تحصیل را انتخاب کنید"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblStudyEndType" runat="server" CssClass="radio" RepeatDirection="Horizontal" RepeatColumns="2">
                                                        <asp:ListItem Text="فارغ التحصيل" Value="1" />
                                                        <asp:ListItem Text="انصراف" Value="2" />
                                                        <asp:ListItem Text="اخراج" Value="3" />
                                                        <asp:ListItem Text="ترك تحصيل" Value="4" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-3 ">
                                                    <asp:Label Text="وضعیت تسویه :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rdblCheckOutTypeLastUni" ValidationGroup="LastUni" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="وضعیت تسویه حساب را انتخاب کنید"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblCheckOutTypeLastUni" runat="server" CssClass="radio" RepeatDirection="Horizontal" RepeatColumns="2">
                                                        <asp:ListItem Text="تسويه دارد" Value="1" />
                                                        <asp:ListItem Text="بدهي دارد" Value="2" />
                                                        <asp:ListItem Text="بدهي ندارد" Value="3" />
                                                        <asp:ListItem Text="دفترچه اقساط" Value="4" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="form-group col-md-2">
                                                    <asp:Label Text="تاریخ پایان تحصیل :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtDateStudyEnd" ValidationGroup="LastUni" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="درج تاریخ فارغ التحصیلی دوره قبلی الزامی است"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtDateStudyEnd" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="LastUni" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="فرمت تاریخ پایان تحصیل وارد شده اشتباه است"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtDateStudyEnd" runat="server" MaxLength="10" CssClass="form-control pcal" ToolTip="مثال : 1394/11/12" />
                                                </div>
                                                <div class="form-group col-md-3 ">
                                                    <asp:Label Text="وضعیت پایان خدمت :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="rdblPayanKhedmat" ValidationGroup="LastUni" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="وضعیت پایان خدمت را مشخص کنید"></asp:RequiredFieldValidator>
                                                    <asp:RadioButtonList ID="rdblPayanKhedmat" CssClass="radio" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                                        <asp:ListItem Text="مشمول" Value="1" />
                                                        <asp:ListItem Text="غیر مشمول" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <asp:Label Text="مبلغ کل بدهی وام بلند مدت بدون احتساب پرداختی (به ریال) :" runat="server" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtVezaratLoanAmount" ForeColor="Red" ValidationGroup="LastUni" Display="Dynamic" runat="server" ErrorMessage="وارد کردن مبلغ کل بدهی وام الزامی است"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtVezaratLoanAmount" ValidationExpression="^[0-9]{1,11}$" ValidationGroup="LastUni" ForeColor="Red" Display="Dynamic" runat="server" ErrorMessage="مبلغ بدهی وارد شده نامعتبر است"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtVezaratLoanAmount" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <asp:Label Text="تصویر اسکن شده مدرک:" runat="server" />
                                                    <span class="text-danger">در صورت وجود</span>
                                                    <div>
                                                        <telerik:RadAsyncUpload ID="flpMadrakImage" runat="server" OnClientValidationFailed="OnClientValidationFailed" Width="100%" MaxFileInputsCount="1" AllowedFileExtensions="jpeg,jpg,,pdf" MaxFileSize="512000" Localization-Cancel="لغو" Localization-Remove="حذف" Localization-Select="انتخاب"></telerik:RadAsyncUpload>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row well text-center">
                                                <asp:Button ID="btnSubmitMaghta" Text="ثبت اطلاعات" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitMaghta_Click" ValidationGroup="LastUni" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>

                        <telerik:RadWindow RenderMode="Lightweight" ID="modalPopup" runat="server" Width="1030px" Height="260px" Modal="true" OffsetElementID="main" Style="z-index: 100001;">
                            <ContentTemplate>

                                <div role="document">
                                    <div class="modal-content" style="width: 1000px;">
                                        <div class="modal-header" dir="rtl">
                                            <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;
                                                </span>
                                            </button>
                                            <div class="modal-header" dir="rtl">
                                            </div>--%>
                                            <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
                                            <h4 class="modal-title">مشاهده تاریخچه پیام</h4>
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
                                                        فرستنده
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
                                                                <%# GetReqNote(Convert.ToInt32(Eval("UserId"))) %>
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
                                    <%--<div class="modal-footer">
                                        <button type="button" class="btn btn-default" onclick="closeHistoryPopup();">بستن پنجره</button>

                                    </div>--%>
                                </div>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <telerik:RadWindow ID="rwCheckoutReason" runat="server" Width="250" Height="250" Modal="true">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl">
                        <div class="row">
                            <div class="col-md-12">
                                <label>لطفا علت انصراف را(حداکثر در 150 کاراکتر) ذکر فرمایید</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <span>لطفا علت انصراف را ذکر فرمایید</span>
                                <%--<asp:RegularExpressionValidator ID="revCheckoutReason"  runat="server"
                                    ControlToValidate="txtCheckoutReason" ErrorMessage="ذکر علت الزامی است" ValidationExpression=""></asp:RegularExpressionValidator>--%>
                                <asp:TextBox runat="server" ID="txtCheckoutReason" TextMode="MultiLine" MaxLength="150"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Button ID="btnSaveReason" runat="server" Text="ذخیره علت و ادامه" CssClass="btn purple" OnClick="btnSaveReason_Click" />
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>


    <script type="text/javascript">
        function sure(button) {
            if (Page_ClientValidate() == true) {
                //Call My Functions 
                __doPostBack(button.name, "");
            }
            return
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    BindControlEvents();
                }
            });
        };

        function OnClientValidationFailed(sender, args) {
            var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
            if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                    radalert("فرمت فایل انتخابی صحیح نمی باشد!", 0, 100, "هشدار", "");
                }
                else {
                    radalert("حجم فایل انتخابی بیش از اندازه مجاز می باشد!", 0, 100, "هشدار", "");
                }
            }
            else {
                radalert("فرمت صحیح نمی باشد!", 0, 100, "هشدار", "");
            }
        }

        var $ = $telerik.$;

        function submitPage() {
            //finds all file uploads that are currently in progress
            var uploadingRows = $(".RadAsyncUpload").find(".ruUploadProgress");
            //iterates and checks is there any file uploads that are successfully completed or failed and if yes - pop-up an alert box and prevent page submitting 
            for (var i = 0; i < uploadingRows.length; i++) {
                if (!$(uploadingRows[i]).hasClass("ruUploadCancelled") && !$(uploadingRows[i]).hasClass("ruUploadFailure") && !$(uploadingRows[i]).hasClass("ruUploadSuccess")) {
                    radalert("لطفا تا اتمام بارگذاری تمامی فایل ها صبر کنید", 0, 100, "هشدار", "");
                    return false;
                }
            }

            return true;
        }

        //On UpdatePanel Refresh.

        function SetDatePicker() {
            var idArray = [];
            $('.pcal').each(function () {
                idArray.push(this.id);
            });

            if (idArray.length > 0) {
                for (var i = 0; i < idArray.length; i++) {
                    if (idArray[i] != 0) {
                        var i = new AMIB.persianCalendar(idArray[i],
                            { extraInputID: idArray[i], extraInputFormat: 'yyyy/mm/dd' });
                    }
                }
            }
        }

        function BindControlEvents() {
            $("#current").parent().css("border", "1px solid red");
        }
        function RadWindow2_Close(sender, args) {
            document.getElementById("ctl00_ContentPlaceHolder1_RadWindow2_C_txtMsg").innerHTML = "";
        }
        function closeHistoryPopup(sender, args) {
            var window = $find('<%=modalPopup.ClientID %>');
            window.close();
        }

        function redirectToDefault() {
            window.reloadpage();
        }

        function reloadpage() {
            location.reload();
        }
    </script>
</asp:Content>
