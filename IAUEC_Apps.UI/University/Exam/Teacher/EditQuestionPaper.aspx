<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditQuestionPaper.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.EditQuestionPaper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../MasterPages/css/custom.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../CommonUI/js/jquery-1.8.3.js"></script>

    <script type='text/javascript'>        //<![CDATA[


        $(window).load(function () {
            $('#viewer').hide();
            $('#viewerAtt').hide();
            //------------------------------------------------------
            function readURL(input) {
                //$viewer.attr('src', '');//null=='' //4خط زیر معادل این دستورند
                var $viewer = $('embed#viewer');
                var parent = $viewer.parent();
                $viewer.remove();
                var $newViewer = $("<embed  id='viewer'>");
                parent.append($newViewer);

                var file = (input.files && input.files[0]) ? input.files[0] : null; //e.target.files[0];
                if (file !== '') {
                    if (fileValidation(input)) {
                        $('#viewer').hide();
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            qFileUrl = URL.createObjectURL(input.files[0]);
                            $newViewer.attr('width', '100%');
                            $newViewer.attr('height', '500');
                            $newViewer.attr('src', qFileUrl);
                            $newViewer.show();
                        }
                        reader.readAsDataURL(input.files[0]);
                    }
                }
            }
            //------------------------------------------------------
            function readURLATT(input) {
                //$viewerAtt.attr('src', '');//null=='' //4خط زیر معادل این دستورند
                var $viewerAtt = $('embed#viewerAtt');
                var parent = $viewerAtt.parent();
                $viewerAtt.remove();
                var $newViewerAtt = $("<embed  id='viewerAtt'>");
                parent.append($newViewerAtt);

                var attFile = (input.files && input.files[0]) ? input.files[0] : null;
                if (attFile !== '') {
                    if (fileValidation(input)) {
                        $('#viewerAtt').hide();
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            AttFileUrl = URL.createObjectURL(input.files[0]);
                            $newViewerAtt.attr('width', '100%');
                            $newViewerAtt.attr('height', '500');
                            $newViewerAtt.attr('src', AttFileUrl);
                            $newViewerAtt.show();
                        }
                        reader.readAsDataURL(input.files[0]);
                    }
                }

            }
            //------------------------------------------------------
            $("#fileUploader").change(function (e) {
                debugger;
                readURL(this);
            });

            $("#AttachUploader").change(function (e) {
                readURLATT(this);
            });
        });//]]> 


        function fileValidation(fileInput) {
            var flag = true;
            var filePath = fileInput.value;
            var allowedExtensions = /(\.jpg|\.pdf)$/i;
            if (!allowedExtensions.exec(filePath)) {
                alert('استاد گرامی فرمت فایل انتخابی فقط می تواند یکی از دو فرمت pdf ,jpg باشد');
                fileInput.value = "";
                flag = false;
            } else if (fileInput.files[0].size / 1024 / 1024 > 2) {//convert to mb                
                alert("استاد گرامی فایل انتخابی شما نمی تواند حجمی بیش از 2 مگابایت داشته باشد");
                fileInput.value = "";
                flag = false;
            }
            return flag;
        }


    </script>
    <style type="text/css">
        .rreedd {
            color: red;
            background-color: #f5eceb;
            margin: 4px 2px;
            padding: 1px;
            display: inline-block;
        }

         .w100 {
            width: 100%;
        }

        .h500 {
            min-height: 500px;
        }

        .object-height {
            min-height: 750px;
        }

        .display_flex_center {
            display: flex;
            justify-content: center;
            align-items: center;
        }
    </style>
</head>
<body class="nav-md" style="background: #F7F7F7">
    <form id="form1" runat="server">
        <script>
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
                return oWindow;
            }



            function CloseModal(args) {

                setTimeout(function () {
                    GetRadWindow().BrowserWindow.refreshGrid(args);
                    GetRadWindow().close();

                }, 0);

            }

        </script>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>

        <asp:Panel runat="server" ID="pnl_Qoptions">
            <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding: 1%">
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-info">
                            <span>استاد گرامی در صورتي كه سوالات شما داراي پاسخنامه چهارگزينه ايي است لطفا در بخش پيوست بارگذاري و گزينه پاسخگويي در برگه سوالات را انتخاب نمائيد.</span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="color: #000">
                        <div class="col-md-2">استفاده از ماشین حساب مجاز</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddl_Cal" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">استفاده از جزوه مجاز</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddl_Note" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">مدت زمان آزمون(دقیقه)</div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txt_ExamTime" runat="server" Width="30px"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="row" id="book_pnl" runat="server" visible="false">
                    <div class="col-md-12" style="color: #000">
                        <div class="col-md-2">استفاده از کتاب قانون</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="rdb_book" runat="server" CssClass="form-control input-sm">
                                <asp:ListItem Text="انتخاب نمایید..." Value="0"></asp:ListItem>
                                <asp:ListItem Text="مجاز" Value="1"></asp:ListItem>
                                <asp:ListItem Text="غیر مجاز" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">نام کتاب قانون قید شود:</div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txt_book" runat="server" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.2); border-radius: 5px; margin: 1%; padding: 1%">
                    <div class="row">
                        <div class="col-md-12" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgb(255, 255, 255); border-radius: 5px; margin-right: 1%; margin-left: 1%; margin-bottom: 1%; padding: 1%">
                            انتخاب نحوه پاسخگویی به سوالات
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="color: #000">

                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="col-md-2" style="padding-right: 5px">
                                        <asp:CheckBox ID="chk_ans1" runat="server" OnCheckedChanged="chk_ans1_CheckedChanged" AutoPostBack="True" />  پاسخ گویی در برگه سوالات
                                    </div>
                                    <div class="col-md-2" style="padding-right: 5px">
                                        <asp:CheckBox ID="chk_ans2" runat="server" OnCheckedChanged="chk_ans2_CheckedChanged" AutoPostBack="True" />   استفاده از پاسخ نامه تشریحی  
                                    </div>

                                    <div class="col-md-8" style="color: #000">
                                        <asp:Label ID="lblChk3" runat="server" Text="استاد محترم حتما پاسخ نامه تستی را نیز بارگذاری نمایید ." Visible="false"></asp:Label>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chk_ans1" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="chk_ans2" EventName="CheckedChanged" />
                                    <%--  <asp:AsyncPostBackTrigger ControlID="chk_ans3" EventName="CheckedChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin: 6px;">
                    <div style="border: 1px solid rgba(183, 28, 28,0.7); background-color: rgba(183, 28, 28,0.1); border-radius: 5px; padding: 10px">
                        <p style="color: #CC0000">
                            <span style="font-size: large; font-weight: bold;">*</span>
                            <span>استاد گرامی لطفا شرایط دیگر آزمون را در ادامه سوالات خود مکتوب نمایید</span>
                            <br />
                            <span style="font-size: large; font-weight: bold;">*</span>
                            <span>استاد گرامی چنانچه استفاده از ماشین حساب مجاز می باشد لطفا نوع ماشین حساب اعم از ساده یا مهندسی را در ادامه سوالات مکتوب نمایید</span>

                        </p>
                    </div>
                </div>
            </div>

            <div class="row" style="margin-top: 1%;">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <asp:Button ID="btnRegisterAndContinue" runat="server" Text="ثبت و ادامه" OnClick="btnRegisterAndContinue_Click" BackColor="#11CE9F" Font-Bold="True" BorderWidth="0" BorderStyle="None" ForeColor="White" Font-Names="B Nazanin" Width="100%" Height="40px" Font-Size="Medium" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btn_DL" runat="server" Text="دانلود فرمت سوالات" OnClick="btn_DL_Click" BackColor="#66CCFF" Font-Bold="True" BorderWidth="0" BorderStyle="None" ForeColor="White" Font-Names="B Nazanin" Width="100%" Height="40px" Font-Size="Medium" />
                    </div>
                </div>
            </div>
        </asp:Panel>


        <asp:Panel runat="server" ID="pnl_UploadQ_Att">
            <div class="container-fluid" dir="rtl"  visible="false" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.2); border-radius: 5px; margin-bottom: 1%;">
                <div class="col-md-12">
                    <div class="alert alert-info">
                        <span>استاد گرامی در صورتي كه سوالات شما داراي پاسخنامه چهارگزينه ايي است لطفا در بخش پيوست بارگذاري و گزينه پاسخگويي در برگه سوالات را انتخاب نمائيد.</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <p style="color: #CC0000; font-size: 14px"><span style="font-size: 18px; font-weight: bold; color: red">*</span> سوالات  و پیوست باید به فرمت  jpg, pdf باشند</p>
                        <p style="color: #CC0000; font-size: 14px"><span style="font-size: 18px; font-weight: bold; color: red">*</span>استاد گرامی در صورتیکه سوالات شما دارای ضمیمه می باشد آن را در قسمت آپلود پیوست سوالات آزمون ارسال نمایید در غیر اینصورت ارسال فایل پیوست<span style="font-size: 20px; color: red"> الزامی نمی باشد </span></p>
                    </div>
                </div>

                <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); padding: 1%; border-radius: 5px; margin-bottom: 1%;">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <div class="col-sm-6" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgb(255, 255, 255); border-radius: 5px; color: #000">آپلود سوالات آزمون:</div>
                                <div class="col-sm-6">
                                    <asp:FileUpload ID="fileUploader" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-6" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgb(255, 255, 255); border-radius: 5px; color: #000">آپلود پیوست سوالات آزمون:</div>
                                <div class="col-sm-6">
                                    <asp:FileUpload runat="server" ID="AttachUploader" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6 ">
                                <asp:RegularExpressionValidator ID="rexpFileUploader" runat="server" ControlToValidate="fileUploader" CssClass="rreedd"
                                    ErrorMessage=" فایل نامعتبر می باشد ،نوع فایل انتخابی فقط می تواند یکی از دو فرمت pdf , jpeg , jpg باشد "
                                    ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Dd][Ff]))">
                                </asp:RegularExpressionValidator>

                                <asp:CustomValidator ID="cvfileUploader" runat="server"
                                    Text="حداکثر حجم فایل باید 4 مگابایت باشد" ToolTip="حداکثر حجم فایل باید 4 مگابایت باشد"
                                    ErrorMessage="حداکثر حجم فایل باید 4 مگابایت باشد"
                                    ControlToValidate="fileUploader"
                                    OnServerValidate="cvfileUploader_OnServerValidate" ForeColor="red" />
                            </div>
                            <div class="col-sm-6">
                                <asp:RegularExpressionValidator ID="rexpAttachUploader" runat="server" ControlToValidate="AttachUploader" CssClass="rreedd"
                                    ErrorMessage=" فایل نامعتبر می باشد ،نوع فایل انتخابی فقط می تواند یکی از دو فرمت pdf , jpeg , jpg باشد "
                                    ValidationExpression="(.+\.([Jj][Pp][Gg])|.+\.([Pp][Dd][Ff]))">
                                </asp:RegularExpressionValidator>

                                <asp:CustomValidator ID="cvAttachUploader" runat="server"
                                    Text="حداکثر حجم فایل باید 4 مگابایت باشد" ToolTip="حداکثر حجم فایل باید 4 مگابایت باشد"
                                    ErrorMessage="حداکثر حجم فایل باید 4 مگابایت باشد" CssClass="boldred"
                                    ControlToValidate="AttachUploader"
                                    OnServerValidate="cvAttachUploader_OnServerValidate" ForeColor="red" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <a id="btnSaveQ_Att" runat="server" class="btn btn-success col-md-2" onserverclick="btnSaveQ_Att_Click">ثبت و ارسال</a>
                            <a id="btnBackToOptionPanel" runat="server" class="btn btn-info col-md-2" onserverclick="btnBackToOptionPanel_ServerClick">ویرایش شرایط آزمون</a>
                        </div>
                    </div>

                </div>
            </div>

            <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); padding: 1%; border-radius: 5px; margin-bottom: 1%;">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-6 ">
                            <object>
                                <embed id="viewer" />

                            </object>
                        </div>

                        <div class="col-sm-6 ">
                            <object>
                                <embed id="viewerAtt" />

                            </object>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>


        <asp:Panel runat="server" ID="pnl_GeneratedPdf_MergedQuestion_Att" Visible="false">
            <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); padding: 1%; border-radius: 5px; margin-bottom: 1%;">
                <div class="row ">
                    <div class="alert alert-error text-center">
                        <h4>استاد گرامی چنانچه هدر فایل را در مرحله قبل دستکاری کرده باشید فایل شما احتمالا با فرمت صحیح نشان داده نمی شود .در این صورت با رد سوال امکان بارگذاری مجدد فراهم می باشد</h4>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-sm-12 display_flex_center">
                        <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-success col-md-2" OnClick="btnConfirm_Click" Text="تایید سوال " />
                        <asp:Button ID="btnReject" runat="server" CssClass="btn btn-danger col-md-2" OnClick="btnReject_Click" Text="رد سوال" />
                    </div>
                </div>
                <div class="row object-height">
                    <div class="col-sm-12">
                        <object id="mergFileID" data="<%= PdfFileSource %>" type='<%= PdfMimeType %>' class="object-height w100"></object>
                    </div>
                </div>
            </div>
        </asp:Panel>








        <telerik:RadEditor ID="RadEditor1" runat="server" Visible="False">
        </telerik:RadEditor>
    </form>
</body>
</html>
