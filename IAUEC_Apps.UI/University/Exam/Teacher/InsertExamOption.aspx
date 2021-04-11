<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertExamOption.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.InsertExamOption" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../MasterPages/css/custom.css" rel="stylesheet" />
    <style>
        .calcWarninig {
            margin-left: 15px;
            margin-right: 18px;
            padding: 10px;
            border: 1px solid rgba(183, 28, 28,0.7);
            background-color: rgba(183, 28, 28,0.1);
            color: #CC0000;
            border-radius: 5px;
            width:95.34%;
        }

        .btndiv {
            padding: 10px;
            color:white;
        }
    </style>
</head>
<body class="nav-md" style="background: #F7F7F7">
    <script>
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow; //IE (and Moz as well)
            return oWindow;
        }
        function CloseModal(args) {

            setTimeout(function () {
                GetRadWindow().BrowserWindow.refreshGrid(args);
                GetRadWindow().close();

            }, 0);

        }
    </script>

    <form id="form1" runat="server">
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>

        <div id="desc" runat="server" class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding: 1%">
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12">
                    <div class="col-md-3"></div>
                    <div class="col-md-6" style="color: #CC0000; font-weight: bold; text-align: center; font-size: 18px">
                        استاد محترم خواهشمند است قبل از بارگذاری سوالات، حتماً موراد زیر را مطالعه نمایید.
                    </div>
                    <div class="col-md-3"></div>
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    1-	ماکزیمم زمان امتحانات به دلیل محدودیت فضا90 دقیقه باشد، خواهشمند است سوالات بر اساس مدت زمان تعیین شده طرح گردد.

                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    2-	هیچ گونه کار تحقیقی اعم از سی دی، جزوه، گزارش کار آموزی و... از دانشجو در جلسه امتحان تحویل گرفته نخواهد شد.
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    3-	اساتید محترم دپارتمان حقوق (کلیه گرایش ها)، خواهشمند است  در صورت استفاده دانشجو از کتاب قانون ، نام کتاب و  نوع آن نیز حتماً انتخاب و درج گردد.
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    4-	سوالات حتماً دارای بارم نمره باشد.
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    5-	بارم نمرات امتحانات پایان ترم میبایست حداقل 12 نمره داشته باشد.
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    6-	لطفا در زمان ثبت نمرات( بخش اعشار نمره) فقط به صورت 0.25  یا  0.5  یا  0.75 درج گردد، در غیر اینصورت  به دلیل دستور العمل های آموزشی نمرات تایید نهایی نخواهند شد.
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    7-	در صورت لزوم استفاده دانشجویان از فرمول،خواهشمند است فرمول های مد نظر جنابعالی/ سرکارعالی  در بخش پایانی سوالات اضافه گردد. (لازم بذکر است اجازه ورود برگه مجزا به جلسه امتحان داده نخواهد شد)
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000">
                    8-	خواهشمند است جهت بارگذاری سوالات فقط از مرورگر گوگل کروم استفاده نمایید.
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-12" style="color: #000; text-align: center">
                    <asp:Button ID="btn_accept" runat="server" CssClass="btn btn-success" OnClick="btn_accept_Click" Text="موارد فوق را مطالعه نمودم" />
                </div>
            </div>

        </div>

        <div id="pnl" runat="server" visible="false" class="container-fluid" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding: 1%">
            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-info">
                    <span>استاد ارجمند در صورتي كه سوالات شما داراي پاسخنامه چهارگزينه ايي است لطفا در بخش پيوست بارگذاري و گزينه پاسخگويي در برگه سوالات را انتخاب نمائيد.</span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="color: #000">
                    <div class="col-md-2">استفاده از ماشین حساب مجاز</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddl_Calc" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">استفاده از جزوه مجاز</div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddl_Jozve" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">مدت زمان آزمون(دقیقه)</div>
                    <div class="col-md-1">
                        <asp:TextBox ID="txt_ExamTime" runat="server" Width="30px" MaxLength="3"></asp:TextBox>
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
                                    <asp:CheckBox ID="chk_ans1" runat="server" OnCheckedChanged="chk_ans1_CheckedChanged" AutoPostBack="True" />پاسخ گویی در برگه سوالات
                                </div>
                                <div class="col-md-2" style="padding-right: 5px">
                                    <asp:CheckBox ID="chk_ans2" runat="server" OnCheckedChanged="chk_ans2_CheckedChanged" AutoPostBack="True" />استفاده از پاسخ نامه تشریحی
                                </div>
<%--                                <div class="col-md-2" style="padding-right: 5px">
                                    <asp:CheckBox ID="chk_ans3" runat="server" OnCheckedChanged="chk_ans3_CheckedChanged" AutoPostBack="True" />استفاده از پاسخ نامه تستی
                                </div>--%>
                                <div class="col-md-8" style="color: #000">
                                    <asp:Label ID="lblChk3" runat="server" Text="استاد محترم حتما پاسخ نامه تستی را نیز بارگذاری نمایید ." Visible="false"></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chk_ans1" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chk_ans2" EventName="CheckedChanged" />
                               <%-- <asp:AsyncPostBackTrigger ControlID="chk_ans3" EventName="CheckedChanged" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>

            <div class="row">
                <div class="col-sm-10 calcWarninig">
                    <div>
                        <span style="font-size: large; font-weight: bold;">*</span>
                        <span>  استاد گرامی لطفا شرایط دیگر آزمون را در ادامه سوالات خود مکتوب نمایید </span>
                        <br />
                        <span style="font-size: large; font-weight: bold;">*</span>
                        <span>استاد گرامی چنانچه استفاده از ماشین حساب مجاز می باشد لطفا نوع ماشین حساب اعم از ساده یا مهندسی را در ادامه سوالات مکتوب نمایید</span>
                    </div>

                </div>
                <div class="col-sm-2  btndiv">  
                        <asp:Button ID="btn_Save" runat="server" Text="ثبت و ادامه" OnClick="btn_Save_Click" CssClass="btn btn btn-success btn-lg" />
                  
                </div>
            </div>
        </div>

        <script>
            function selectedIdDrp() {
        <%--    setTimeout(function () {
                var index = $("select[name='<%#ddl_Calc.ClientID%>'] option:selected").index();
                alert(index);
            }, 10);--%>



                 var ddlFruits = document.getElementById("<%=ddl_Calc.ClientID %>");
                 var selectedText = ddlFruits.options[ddlFruits.selectedIndex].innerHTML;
                 var selectedValue = ddlFruits.value;
                 alert("Selected Text: " + selectedText + " Value: " + selectedValue);
                 return false;

             }
        </script>
    </form>
</body>
</html>
