<%@ Page Title="" Language="C#" MasterPageFile="~/University/WelfareAffairs/MasterPages/StudentLoanRequestMaster.Master" AutoEventWireup="true"
    CodeBehind="StudentLoanRequest.aspx.cs" Inherits="IAUEC_Apps.UI.University.WelfareAffairs.Pages.StudentLoanRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .qs-mark {
            vertical-align: middle;
            font-size: 30px;
        }

        .tooltip-msg {
            display: none;
            position: absolute;
        }

        .rtl {
            direction: rtl;
            color: #353a61;
            /*font-family:W_yekan,'B Badr','2  Aseman','B Baran',Arial ,Tahoma;*/
        }

        .tableWrapper table {
            width: 100%;
        }

            .tableWrapper table > tbody > tr > th {
                background-color: #293f54; /*00007f    293f54*/
                color: #fff;
            }

            .tableWrapper table > tbody > tr:nth-child(odd) {
                background-color: #5280ad;
            }



            .tableWrapper table > tbody > tr > td {
                vertical-align: middle;
                text-align: center;
            }

            .tableWrapper table tr td:nth-child(1), .tableWrapper table tr td:nth-child(3) {
                width: 15% !important;
            }


            .tableWrapper table tr td:nth-child(2) {
                width: 20% !important;
            }


            .tableWrapper table tr td:nth-child(4) {
                width: 50% !important;
            }

        .tableWrapper .RadUpload {
            width: auto;
        }

        .fieldTitle {
            line-height: 30px;
        }

        .guarantorRadioBox input[type="radio"] {
            margin-left: 5px;
        }

        span.c1 {
            padding-left: 20px;
        }


        input[type="file"] {
            background-color: #56d058;
            border-radius: 5px;
            padding: 10px;
            width: 230px;
        }

        section h2 {
            margin-right: 11px;
        }

        .msg-result {
            display: block;
        }

        .setHeader {
            text-align: center;
            font-size: 15px;
            background-color: #00007f !important; /* 9B59B6  00007f*/
            color: white;
            padding: 5px;
        }

        .note-padding {
            padding: 20px;
            background-color: antiquewhite;
            font-family: tahoma;
            font-weight: bold;
        }

        .ul-titr {
            color: #a92727;
        }

        #ulDesc {
            list-style: disc;
        }

            #ulDesc li {
                margin-top: 10px;
                color: #dc0806;
            }
        /* rad window icons */
        ul.rwControlButtons {
            width: 155px !important;
        }


        #spn_redirect {
            margin-right: 50px;
            color: #000;
            text-decoration: underline;
        }


        .rad_window_dir_rtl {
            direction: rtl;
        }

        .RadWindow .rwTitlebarControls {
            direction: rtl;
        }
    </style>

    <script type="text/javascript">

        $(function () {
            $("#<%=grdvLoadRecords.ClientID%> tr th").addClass("setHeader");
            $("#<%=grdvLoadRecords.ClientID%> tr").css({ "text-align": "center", "color": "#010102" });
            $("#<%=grdvLoadRecords.ClientID%> tr:nth-child(2)").css({ "background-color": "#bbf9a2f5" });

            $(".qs-mark").on("mouseenter", function (e) {
                debugger;
                var tooltipMsg = $('.tooltip-msg');
                tooltipMsg.html("");
                tooltipMsg.html("<p>" + $(this).data('info') + "</p>");
                var height_tooltipMsg = tooltipMsg.height();
                var top_ttm = $(this).position().top - height_tooltipMsg - 40 + "px";
                tooltipMsg.css({ "display": "block", "top": top_ttm, "min-width": "300px", "background": "#000", "color": "#fff" });
            });

            $(".qs-mark").on("mouseleave", function (e) {
                $('.tooltip-msg').css({ "display": "none" });
            });
        });

        //##############################


        function CheckValidation(uploaderSRC, destImgTag) {
            var isValid = true;
            var validFiles = ["jpg"];
            var source = uploaderSRC.value;
            var fileSize = uploaderSRC.files[0].size;
            var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
            if (ext == validFiles[0] || ext == validFiles[1]) {
                if (fileSize > 1024 * 1024) {//more than 1 mb
                    alert("سایز فایل انتخابی باید کمتر از یک مگابایت باشد");
                    document.getElementById(uploaderSRC.id).value = "";
                    isValid = false;
                }
            } else {
                var msg = " فرمت فایل انتخابی تنها می تواند ";
                //msg += " jpeg ";
                //msg += " یا  ";
                msg += " jpg ";
                msg += " باشد ";
                alert(msg);
                document.getElementById(uploaderSRC.id).value = "";
                isValid = false;
            }
            if (isValid) {
                getDataUri(uploaderSRC, destImgTag);
            }
            return isValid;
        }


        //##############################

        function CheckFormValidation() {
            debugger;
            uploders = $('input[type="file"]');
            var stop = false;
            for (var i = 0; i < uploders.length; i++) {
                uploder = $('#' + uploders[i].id)
                //if fileuplode is enable ===>hasn't disabled property
                if (!uploder.prop('disabled')) {
                    //get full file name ie file,name and it path
                    var val = uploder.val();
                    if (val == "") {
                        stop = true;
                        break;
                    } else {
                        var fileSize = uploders[i].files[0].size;
                        if (fileSize < 1) {
                            stop = true;
                            break;
                        }
                    }
                }
            }
            if (stop) {
                alert('دانشجوی گرامی لازم است ابتدا تمامی مدارک مورد نیار وام مربوطه را بارگذاری نموده ، سپس دکمه ارسال مدارک را بزنید');
                return false;
            }
        }

        //##############################
        function getDataUri(uploaderSRC, destImgTag) {

            if (uploaderSRC.files && uploaderSRC.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    debugger;
                    //get img's tag that it's id is conyained destImgTag
                    var $img = $("img[id*='" + destImgTag + "']");
                    $img.attr('src', '');
                    $img.attr('width', 230);
                    $img.attr('height', 100);
                    $img.attr('src', e.target.result);
                }
                reader.readAsDataURL(uploaderSRC.files[0]);
            }
        }


        //##############################

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>ثبت درخواست وام دانشجویی</h2>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwmValidations" runat="server"></telerik:RadWindowManager>

    <div class="rtl">
        <section id="Sec_LoanSelection" runat="server" visible="false">
            <%--    <h1>ثبت درخواست وام</h1>
            <hr />--%>
            <div class="note-padding alert alert-danger">
                <h2 class="ul-titr">دانشجوی گرامی رعایت  تمامی نکات زیر الزامی می باشد </h2>
                <ul id="ulDesc">
                    <li>قبل از انتخاب نوع وام  فایل راهنمای مربوط به دریافت وام را از منوی کناری دانلود کرده و با دقت مطالعه فرمایید </li>
                    <li>پس از مطالعه ی فایل راهنما فرم وام مربوطه را از منوی کناری دانلود کرده و پس از تکمیل کردن بار گذاری نمایید .</li>
                    <li>تنها وامی که به ضامن نیار ندارد وام کوتاه مدت می باشد که بصورت پیش فرض  انتخاب شده است .</li>
                    <li>منظور از ضامن فردی است که کارمند رسمی قرادادی  پیمانی یا بازنشته یک سازمان یا نهاد شناخته شده باشد .</li>
                    <li>چنانچه ضامن مورد تایید دارید گزینه ضامن دارم را انتخاب کرده و نوع وام خود را انتخاب نمایید .</li>
                    <li>بارگذاری تمامی مدارک مربوط به هر نوع وام الزامی می باشد .</li>
                    <li>فرمت فایل انتخابی تنها jpg یا jpeg می تواند باشد .</li>
                    <li>حداکثر حجم هر فایل 2مگابایت می باشد .</li>
                    <li>...............................................................................................................</li>
                    <li>دانشجوی گرامی تا قبل از تایید اولیه وام انتخابی از ارسال اصل مدرک چک به دایره مالی خوداری فرمایید.</li>
                </ul>
            </div>
            <br />
            <br />

            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <section id="Sec_Guarantor" runat="server" visible="false" class="row">
                                <div class="col-sm-4">
                                    <span>آیا ضامن دارید </span>
                                    <i class="fa fa-question-circle qs-mark" aria-hidden="true" data-info="منظور از ضامن فردی است که کارمند رسمی قرادادی  پیمانی یا بازنشته یک سازمان یا نهاد شناخته شده باشد  "></i>
                                    <div class="tooltip-msg alert alert-danger"></div>
                                </div>
                                <div class="col-sm-8 ">
                                    <asp:RadioButtonList runat="server" ID="rdbList_GuarantorType" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdbList_GuarantorType_SelectedIndexChanged" AutoPostBack="true"
                                        CssClass="guarantorRadioBox">
                                        <asp:ListItem class="c1" Text="ضامن ندارم" Value="1" Selected="True" />
                                        <asp:ListItem class="c1" Text="ضامن دارم " Value="2" />
                                    </asp:RadioButtonList>
                                </div>
                            </section>
                        </div>
                        <div class="col-md-6">
                            <section id="Sec_LoanType" runat="server" visible="false" class="row">
                                <div class="col-sm-5">
                                    <asp:Label Text="  نوع وام خود را انتخاب نمایید" runat="server" CssClass="fieldTitle" />
                                </div>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="DrpLoanType" runat="server" OnSelectedIndexChanged="DrpLoanType_SelectedIndexChanged"
                                        AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem Text="انتخاب کنید" Value="-1" Selected="True" />
                                        <%-- <asp:ListItem Text="کوتاه مدت" Value="1" />--%>
                                        <asp:ListItem Text="میان مدت ( تامین شهریه ) " Value="2" />
                                        <%--<asp:ListItem Text="بلند مدت" Value="3" />--%>
                                        <asp:ListItem Text="بانک قرض الحسنه مهر ایران ( وزارت علوم )" Value="4" />
                                    </asp:DropDownList>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <asp:Panel runat="server" ID="pnlFirtConfirm" Visible="false">
            <br />
            <h2 class="alert alert-success">دانشجوی گرامی وام شما در حال بررسی می باشد لطفا تا قبل از تایید اولیه مدارک ونوع وام انتخابی از ارسال اصل مدرک چک  یا تحویل حضوری ان به دایره مالی دانشگاه خودداری فرمایید .</h2>
        </asp:Panel>


        <div class="tableWrapper">
            <section id="Sec_ShortTime" runat="server" class="row" visible="false">
                <h2>نوع وام انتخاب شده  <strong>: کوتاه مدت ( بدون ضامن )  </strong></h2>
                <br />
                <table id="tblShortTime" class="table table-bordered table-condensed">
                    <tr>
                        <th>نام مدرک </th>
                        <th>بارگذاری مدرک</th>
                        <th>وضعیت مدرک</th>
                        <th>توضیحات</th>
                    </tr>

                    <tr>
                        <td>فرم کوتاه مدت</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderShortTime_Form" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR"
                                MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="ShortTime_Form">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderShortTime_Form" runat="server" data-DocName="ShortTime_Form" onchange='return CheckValidation(this,"img_UploaderShortTime_Form");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderShortTime_Form" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblShortTime_Form" data-DocName="ShortTime_Form" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblShortTime_Form_msg" data-DocType="1" />
                        </td>
                    </tr>

                    <tr>
                        <td>چک</td>
                        <td>
                            <%--                           <telerik:RadAsyncUpload ID="UploaderShortTime_Check" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="ShortTime_Check">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>


                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderShortTime_Check" runat="server" data-DocName="ShortTime_Check" onchange='return CheckValidation(this,"img_UploaderShortTime_Check");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderShortTime_Check" />
                                    </div>
                                </div>
                            </div>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblShortTime_Check" data-DocName="ShortTime_Check" />
                        </td>

                        <td>
                            <asp:Label runat="server" ID="lblShortTime_Check_msg" data-DocType="12" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnShortTime" Text="ارسال مدارک" runat="server" CssClass="btn btn-success btn-lg" OnClientClick="return CheckFormValidation();" OnClick="btnShortTime_Click" />
                        </td>
                    </tr>

                </table>

            </section>

            <section id="Sec_MidTime" runat="server" class="row" visible="false">
                <h2>نوع وام انتخاب شده  <strong>: میان مدت ( تامین شهریه )  </strong></h2>
                <br />
                <table id="tblMidTime" class="table table-bordered table-condensed ">
                    <tr>
                        <th>نام مدرک </th>
                        <th>بارگذاری مدرک</th>
                        <th>وضعیت مدرک</th>
                        <th>توضیحات</th>
                    </tr>

                    <tr>
                        <td>فرم میان مدت</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Form" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Form">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Form" runat="server" data-DocName="MidTime_Form" onchange='return CheckValidation(this,"img_UploaderMidTime_Form");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Form" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Form" data-DocName="MidTime_Form" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_msg" data-DocType="2" />
                        </td>

                    </tr>

                    <tr>
                        <td>چک</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Check" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Check">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Check" runat="server" data-DocName="MidTime_Check" onchange='return CheckValidation(this,"img_UploaderMidTime_Check");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Check" />
                                    </div>
                                </div>
                            </div>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Check" data-DocName="MidTime_Check" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Check_msg" data-DocType="12" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی شناسنامه دانشجو</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Stu_ID" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Stu_ID">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Stu_ID" runat="server" data-DocName="MidTime_Stu_ID" onchange='return CheckValidation(this,"img_UploaderMidTime_Stu_ID");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Stu_ID" />
                                    </div>
                                </div>
                            </div>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Stu_ID" data-DocName="MidTime_Stu_ID" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Stu_ID_msg" data-DocType="5" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی کارت ملی دانشجو</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Stu_NationalCard" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Stu_NationalCard">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Stu_NationalCard" runat="server" data-DocName="MidTime_Stu_NationalCard" onchange='return CheckValidation(this,"img_UploaderMidTime_Stu_NationalCard");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Stu_NationalCard" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Stu_NationalCard" data-DocName="MidTime_Stu_NationalCard" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Stu_NationalCard_msg" data-DocType="6" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی شناسنامه ضامن</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Guarantor_ID" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Guarantor_ID">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Guarantor_ID" runat="server" data-DocName="MidTime_Guarantor_ID" onchange='return CheckValidation(this,"img_UploaderMidTime_Guarantor_ID");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Guarantor_ID" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_ID" data-DocName="MidTime_Guarantor_ID" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_ID_msg" data-DocType="7" />
                        </td>

                    </tr>

                    <tr>
                        <td>کپی کارت ملی ضامن</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Guarantor_NationalCard" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Guarantor_NationalCard">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Guarantor_NationalCard" runat="server" data-DocName="MidTime_Guarantor_NationalCard" onchange='return CheckValidation(this,"img_UploaderMidTime_Guarantor_NationalCard");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Guarantor_NationalCard" />
                                    </div>
                                </div>
                            </div>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_NationalCard" data-DocName="MidTime_Guarantor_NationalCard" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_NationalCard_msg" data-DocType="8" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی حکم کارگزینی ضامن</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Guarantor_Kargozini" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Guarantor_Kargozini">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Guarantor_Kargozini" runat="server" data-DocName="MidTime_Guarantor_Kargozini" onchange='return CheckValidation(this,"img_UploaderMidTime_Guarantor_Kargozini");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderMidTime_Guarantor_Kargozini" />
                                    </div>
                                </div>
                            </div>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_Kargozini" data-DocName="MidTime_Guarantor_Kargozini" />
                        </td>

                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_Kargozini_msg" data-DocType="9" />
                        </td>
                    </tr>

                    <tr>
                        <td>فیش حقوقی ضامن</td>
                        <td>
                            <%--                            <telerik:RadAsyncUpload ID="UploaderMidTime_Guarantor_Fish" runat="server" AllowedFileExtensions="jpg,jpeg" Culture="fa-IR" MaxFileSize="30720" UploadedFilesRendering="BelowFileInput" Skin="Silk" MaxFileInputsCount="1" data-DocType="MidTime_Guarantor_Fish">
                                <Localization Select="انتخاب" Remove="حذف" />
                            </telerik:RadAsyncUpload>--%>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderMidTime_Guarantor_Fish" runat="server" data-DocName="MidTime_Guarantor_Fish" onchange='return CheckValidation(this,"img_MidTime_Guarantor_Fish");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_MidTime_Guarantor_Fish" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_Fish" data-DocName="MidTime_Guarantor_Fish" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMidTime_Guarantor_Fish_msg" data-DocType="10" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnMidTime" Text="ارسال مدارک" runat="server" CssClass="btn btn-success btn-lg" OnClientClick="return CheckFormValidation();" OnClick="btnMidTime_Click" />
                        </td>

                    </tr>

                </table>

            </section>

            <%--   <section id="Sec_LongTime" runat="server" class="row" visible="false">
                <h2>نوع وام انتخاب شده  <strong>: بلند مدت  </strong></h2>
                <br />
                <table id="tblLongTime" class="table table-bordered table-condensed">
                    <tr>
                        <th>نام مدرک </th>
                        <th>بارگذاری مدرک</th>
                        <th>وضعیت مدرک</th>
                        <th>توضیحات</th>
                    </tr>

                    <tr>
                        <td>فرم بلند مدت</td>
                        <td>                       
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Form" runat="server" data-DocName="LongTime_Form" onchange='return CheckValidation(this,"img_UploaderLongTime_Form");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Form" />
                                    </div>
                                </div>
                            </div>

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Form" data-DocName="LongTime_Form" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Form_msg" data-DocType="3" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی شناسنامه دانشجو</td>
                        <td>               

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Stu_ID" runat="server" data-DocName="LongTime_Stu_ID" onchange='return CheckValidation(this,"img_UploaderLongTime_Stu_ID");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Stu_ID" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Stu_ID" data-DocName="LongTime_Stu_ID" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Stu_ID_msg" data-DocType="5" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی کارت ملی دانشجو</td>
                        <td>          

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Stu_NationalCard" runat="server" data-DocName="LongTime_Stu_NationalCard" onchange='return CheckValidation(this,"img_UploaderLongTime_Stu_NationalCard");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Stu_NationalCard" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Stu_NationalCard" data-DocName="LongTime_Stu_NationalCard" />
                        </td>

                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Stu_NationalCard_msg" data-DocType="6" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی شناسنامه ضامن</td>
                        <td>             

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Guarantor_ID" runat="server" data-DocName="LongTime_Guarantor_ID" onchange='return CheckValidation(this,"img_UploaderLongTime_Guarantor_ID");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Guarantor_ID" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_ID" data-DocName="LongTime_Guarantor_ID" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_ID_msg" data-DocType="7" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی کارت ملی ضامن</td>
                        <td>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Guarantor_NationalCard" runat="server" data-DocName="LongTime_Guarantor_NationalCard" onchange='return CheckValidation(this,"img_UploaderLongTime_Guarantor_NationalCard");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Guarantor_NationalCard" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_NationalCard" data-DocName="LongTime_Guarantor_NationalCard" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_NationalCard_msg" data-DocType="8" />
                        </td>
                    </tr>

                    <tr>
                        <td>کپی حکم کارگزینی ضامن</td>
                        <td>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Guarantor_Kargozini" runat="server" data-DocName="LongTime_Guarantor_Kargozini" onchange='return CheckValidation(this,"img_UploaderLongTime_Guarantor_Kargozini");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Guarantor_Kargozini" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_Kargozini" data-DocName="LongTime_Guarantor_Kargozini" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_Kargozini_msg" data-DocType="9" />
                        </td>
                    </tr>

                    <tr>
                        <td>فیش حقوقی ضامن</td>
                        <td>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Guarantor_Fish" runat="server" data-DocName="LongTime_Guarantor_Fish" onchange='return CheckValidation(this,"img_UploaderLongTime_Guarantor_Fish");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Guarantor_Fish" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_Fish" data-DocName="LongTime_Guarantor_Fish" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Guarantor_Fish_msg" data-DocType="10" />
                        </td>
                    </tr>

                    <tr>
                        <td>تععدنامه محضری</td>
                        <td>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-8">
                                        <asp:FileUpload ID="UploaderLongTime_Recognizance" runat="server" data-DocName="LongTime_Recognizance" onchange='return CheckValidation(this,"img_UploaderLongTime_Recognizance");' />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Image ImageUrl="#" runat="server" ID="img_UploaderLongTime_Recognizance" />
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Recognizance" data-DocName="LongTime_Recognizance" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblLongTime_Recognizance_msg" data-DocType="11" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnLongTime" Text="ارسال مدارک" runat="server" CssClass="btn btn-success btn-lg" OnClientClick="return CheckFormValidation();" OnClick="btnLongTime_Click" />
                        </td>
                    </tr>

                </table>
            </section>--%>



            <section id="Sec_MehreIran" runat="server" class="row" visible="false">
                <h2>نوع وام انتخاب شده  <strong>:بانک قرض الحسنه مهر ایران ( وزارت علوم  )  </strong></h2>
                <br />
                <%--  <asp:Button ID="btnMehreIran" Text="ارسال مدارک" runat="server" CssClass="btn btn-success" OnClientClick="return CheckFormValidation();" OnClick="btnMehreIran_Click" />--%>
                <div class="alert alert-info">
                    <h4>دانشجوی گرامی جهت ثبت درخواست وام بانک قرض الحسنه مهر ایران ( وزارت علوم  ) لطفا بر روی لینک مقابل کلیک نمایید  <a href="http://bp.swf.ir"><span id="spn_redirect">پرتال دانشجویی صندوق رفاه  </span></a></h4>
                </div>
                <%--<asp:Panel runat="server" ID="pnlRedirect" Visible="false" class="alert alert-danger"></asp:Panel>--%>
            </section>
        </div>

        <asp:Panel runat="server" ID="pnlMsgFinalConfirm" Visible="false">
            <br />
            <h2 class="alert alert-success">دانشجوی گرامی درخواست وام شما با موفقیت تایید اولیه گردید  لطفا جهت تایید نهایی شدن درخواست  خود اصل چک ضمانت را حضورا به قسمت مالی دانشگاه تحویل دهید
            </h2>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlLoanRecord">
            <br />
            <h2>فهرست وام های شما</h2>
            <hr />
            <asp:GridView runat="server" ID="grdvLoadRecords" OnRowCommand="grdvLoadRecords_RowCommand" AutoGenerateColumns="False" BackColor="#3A4A5B" ForeColor="White" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="70px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:BoundField DataField="LoanId" HeaderText="شماره درخواست" ItemStyle-Width="100">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="loanType" HeaderText="نوع وام درخواستی" ItemStyle-Width="150">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Status" HeaderText="وضعیت وام " ItemStyle-Width="150">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Term" HeaderText="ترم " ItemStyle-Width="100">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="ReqDate" HeaderText="تاریخ درخواست" ItemStyle-Width="120">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Message" ItemStyle-Width="10" HeaderText=" توضیحات ">
                        <ItemStyle Width="400px"></ItemStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="مشاهده مدارک" ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="btnShowPopUp" Text="مشاهده مدارک" CommandName="show_popup" CommandArgument='<%#Eval("LoanId")%>' CssClass="btn btn-danger" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </asp:Panel>

        <br />
        <br />
        <br />
    </div>

    <script>
        function Rebind() {
            __doPostBack('RebindeGrid', null);
        }
    </script>
</asp:Content>

<%--  <telerik:RadWindowManager ID="rwm_Validations" runat="server" Height="500px" Modal="True" Width="650px">
    </telerik:RadWindowManager>--%>