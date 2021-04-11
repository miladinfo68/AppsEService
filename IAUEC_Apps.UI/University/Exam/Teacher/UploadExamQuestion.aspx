<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadExamQuestion.aspx.cs"
    Inherits="IAUEC_Apps.UI.University.Exam.CMS.UploadExamQuestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../MasterPages/css/custom.css" rel="stylesheet" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.8.3.js"></script>
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

        <asp:Panel ID="pnlMainQuestion_Att_Uploaded" runat="server">
            <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.2); border-radius: 5px; margin-bottom: 1%;">
                <div class="row">
                    <div class="col-md-12">
                        <p style="color: #CC0000; font-size: 14px"><span style="font-size: 18px; font-weight: bold; color: red">*</span> سوالات  و پیوست باید به فرمت pdf یا  jpg باشد</p>
                        <p style="color: #CC0000; font-size: 14px"><span style="font-size: 18px; font-weight: bold; color: red">*</span>استاد گرامی در صورتیکه سوالات شما دارای ضمیمه می باشد آن را در قسمت آپلود پیوست سوالات آزمون ارسال نمایید در غیر اینصورت ارسال فایل پیوست<span style="font-size: 20px; color: red"> الزامی نمی باشد </span></p>
                    </div>
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
                                ErrorMessage=" فایل نامعتبر می باشد ،نوع فایل انتخابی فقط می تواند یکی از دو فرمت pdf  , jpg باشد "
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
                                ErrorMessage=" فایل نامعتبر می باشد ،نوع فایل انتخابی فقط می تواند یکی از دو فرمت pdf  , jpg باشد "
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
                        <a id="A1" runat="server" class="btn btn-success col-md-2" onserverclick="btn_Save_Click">ثبت و ارسال</a>
                    </div>
                </div>
            </div>
            <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); padding: 1%; border-radius: 5px; margin-bottom: 1%;">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="col-sm-6">
                            <object>
                                <embed id="viewer" />
                            </object>
                        </div>

                        <div class="col-sm-6">
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
                    <div class="alert alert-warning text-center">
                        <h4>استاد گرامی درصورت تغییر عنوان سوال در این مرحله فایل شما به صورت نامرتب نشان داده می شود .در این صورت با رد سوال امکان بارگذاری مجدد فراهم می باشد</h4>
                    </div>
                </div>
                <div class="row ">
                    <div class="col-sm-12 display_flex_center">
                        <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-success col-md-2" OnClick="btnConfirm_Click" Text="ثبت و ادامه" />
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

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>

    </form>
</body>

</html>
