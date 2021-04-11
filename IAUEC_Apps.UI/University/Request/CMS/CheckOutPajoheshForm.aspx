<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutPajoheshForm.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutPajoheshForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .CenteralText {
            text-align: center;
            font-size: 16px;
        }

        a.pcalBtn {
            vertical-align: middle !important;
            position: relative !important;
            margin-right: -22px !important;
        }

        .middlelabel {
            margin-top: 6px !important;
        }

        .form-control {
            border-radius: 5px;
            display: inline-block !important;
            width: initial !important;
            text-align: center !important;
        }
        .multiformcontrol{
            width:65% !important;
            height:30% !important;
            text-align:right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>سامانه تسویه حساب غیر حضوری - پنل پژوهش</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openModal_ChangeDefPoint() {
            $('#modalChangeDefPoint').modal('show');
        }
        function closeModal_ChangeDefPoint() {
            $('#modalChangeDefPoint').modal('hide');
        }
    </script>
    <div dir="rtl">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div>
                    <asp:Image ImageUrl="~/University/Theme/images/animatedEllipse.gif" Height="35px" Width="35px" runat="server" />
                    <span>در حال بارگذاری...</span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row form-inline" dir="rtl">
                <div class="col-xs-3" style="margin-top: 10px;">
                    <asp:Label CssClass="control-label" Text="شماره دانشجویی" runat="server" />
                    <asp:TextBox CssClass="form-control" ID="txtStcode" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="btn btn-success" OnClick="btnSearch_Click" ValidationGroup="search" />
                </div>
                <div class="col-xs-9">
                    <div id="divlblSysMsg" runat="server" visible="false" class="col-md-6 alert alert-warning CenteralText" style="margin-right: 6%;">
                        <asp:Label ID="lblSysMsg" runat="server" CssClass="CenteralText" />
                    </div>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="لطفا شماره دانشجویی را وارد نمایید" ValidationGroup="search" ControlToValidate="txtStcode" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            <div id="dvcontainar" runat="server" class="container" dir="rtl">
                <table id="tblStudentInfo" runat="server" visible="false" class="table table-bordered table-responsive text-center">
                    <tr class="bg-primary">
                        <td>شماره دانشجویی</td>
                        <td>نام و نام خانوادگی</td>
                        <td>رشته تحصیلی</td>
                        <td>مقطع تحصیلی</td>
                        <td>سال ورود</td>
                        <td>نام پدر</td>
                        <td>شماره ملی</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStcode" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" /></td>
                        <td>
                            <asp:Label ID="lblReshte" runat="server" /></td>
                        <td>
                            <asp:Label ID="lblMaghta" runat="server" /></td>
                        <td>
                            <asp:Label ID="lblSalVorood" runat="server" /></td>
                        <td>
                            <asp:Label ID="lblFatherName" runat="server" /></td>
                        <td>
                            <asp:Label ID="lblCodeMeli" runat="server" />
                        </td>

                    </tr>
                </table>

                <div class="col-md-12 form-group">
                    <table id="tblInfo" runat="server" visible="false" class="table table-bordered table-responsive table-striped text-center">
                        <tr class=" bg-green">
                            <td></td>
                            <td>اطلاعات پایان نامه</td>
                            <td>عملیات</td>

                        </tr>
                        <tr id="isDefa" runat="server" style="width: 100%;">

                            <td style="width: 50%;">
                                <div class="row">
                                    <div class="col-xs-6 middlelabel">
                                        <div style="float: left">
                                            تاریخ برگزاری جلسه دفاع:
                                        </div>

                                    </div>
                                    <div class="col-xs-6">
                                        <div style="float: right">
                                            <asp:TextBox ID="pcal1" runat="server" ValidationGroup="submit1" Enabled="false" CssClass="form-control" />
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="لطفا تاریخ دفاع را مشخص کنید" ControlToValidate="pcal1" ForeColor="Red" ValidationGroup="submit1" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ را با فرمت صحیح وارد کنید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit1" Display="Dynamic"></asp:RegularExpressionValidator>--%>

                                        </div>

                                    </div>
                                </div>

                            </td>

                            <td style="width: 50%;">
                                <div class="row">
                                    <div class="col-xs-6 middlelabel">
                                        <div style="float: left">
                                            تاریخ ارسال مدارک به حوزه معاونت پژوهشی :
                                        </div>

                                    </div>
                                    <div class="col-xs-6">
                                        <div style="float: right">
                                            <asp:TextBox ID="pcal5" runat="server" ValidationGroup="submit1" CssClass="form-control" Enabled="false" />
                                          <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="لطفا تاریخ ارسال مدارک را مشخص کنید" ControlToValidate="pcal5" ForeColor="Red" ValidationGroup="submit1" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="pcal5" ErrorMessage="لطفا تاریخ را با فرمت صحیح وارد کنید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit1" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                        </div>

                                    </div>
                                </div>

                            </td>


                            <td>
                                <asp:Button ID="btnSubmitDefDate" Text="ثبت" runat="server" Enabled="false" CssClass="btn btn-danger" ValidationGroup="submit1" OnClick="btnSubmitDefDate_Click" />
                            </td>

                        </tr>

                        <tr id="trCancelPap" runat="server" style="width: 100%;">

                            <td style="width: 50%;">
                                <span class="middlelabel">آیا فرم انصراف از مقاله در پک ارسالی می باشد؟
                                </span>
                            </td>
                            <td style="width: 50%;">
                                <div class="row">
                                    <asp:RadioButton ID="rdYes1" runat="server" Text="بله" AutoPostBack="true" OnCheckedChanged="rdYes1_CheckedChanged" />
                                    <asp:RadioButton ID="rdNo1" runat="server" Text="خیر" AutoPostBack="true" OnCheckedChanged="rdNo1_CheckedChanged" />
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnHasCancelForm" Text="ثبت" runat="server" CssClass="btn btn-danger" Enabled="false" OnClick="btnHasCancelForm_Click" />
                            </td>

                        </tr>

                        <tr id="trReqPap" runat="server">
                            <td>
                                <span>آیا دانشجو متقاضی ارائه مقاله می باشد؟
                                </span>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdYes2" GroupName="editThes" runat="server" Text="بله" AutoPostBack="true" OnCheckedChanged="rdYes2_CheckedChanged" />
                                <asp:RadioButton ID="rdNo2" runat="server" GroupName="editThes" Text="خیر" AutoPostBack="true" OnCheckedChanged="rdNo2_CheckedChanged" />
                                <br />
                                <div class="row">
                                    <div>
                                        <asp:Label ID="lblDeadLine" runat="server" Text="مهلت ارسال مقاله :" Visible="false"></asp:Label>
                                        <asp:TextBox ID="pcal6" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                         &nbsp
                                        <asp:Label ID="lblCancelDate" runat="server" Text="تاریخ انصراف از مقاله :" Visible="false"></asp:Label>
                                        <asp:TextBox ID="pcal7" runat="server" Visible="false" CssClass="form-control" placeHolder="تاریخ انصراف از مقاله"></asp:TextBox>
                                    </div>

                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnReqPaper" Text="ثبت" runat="server" CssClass="btn btn-danger" Enabled="false" OnClick="btnReqPaper_Click" />
                            </td>

                        </tr>
                        <tr id="trEditThes" runat="server">
                            <td>
                                <span>آیا پایان نامه دارای اصلاحات می باشد؟
                                </span>
                            </td>
                            <td> 
                                <div class="row">
                                    <div>
                                <asp:RadioButton ID="RdYes3" runat="server" Text="بله" AutoPostBack="true" OnCheckedChanged="RdYes3_CheckedChanged" />
                                        
                                <asp:RadioButton ID="RdNo3" runat="server" Text="خیر" AutoPostBack="true" OnCheckedChanged="RdNo3_CheckedChanged" />
                                       &nbsp&nbsp
                                
                               
                                        <asp:Label ID="lblDetail" runat="server" Text="/  توضیحات اصلاح پایان نامه :" Visible="false"></asp:Label>
                                       
                                        <asp:TextBox ID="txtEditThes" TextMode="MultiLine"  runat="server" CssClass="form-control multiformcontrol" Visible="false"></asp:TextBox><br />
                                         <asp:CheckBox ID="chkbReciveEdit" runat="server" Text="دریافت فرم اصلاح پایان نامه" Visible="false" OnCheckedChanged="chkbReciveEdit_CheckedChanged" AutoPostBack="true" Checked="false"/>
                                           
                                        
                                    </div>
                                </div>
                            </td>
                            <td>
                                <asp:Button ID="btnEditThes" Text="ثبت" runat="server" CssClass="btn btn-danger" Enabled="false" OnClick="btnEditThes_Click" />
                            </td>

                        </tr>


                        <tr id="trPoint" runat="server">
                            <td>نمره پایان نامه :
                                
                                <asp:TextBox ID="txtDefPoint" runat="server" ValidationGroup="point" CssClass="form-control" Enabled="false" />
                                <asp:Label ID="lblTxtSidaDefPoint" runat="server" Visible="false" Text="نمره پایان نامه در سیدا:"></asp:Label>
                                <asp:Label ID="lblSidaDefPoint" runat="server" Visible="false" Text=""></asp:Label>
                            </td>


                            <td style="width: 50%;">
                                <div class="row">
                                    <div class="col-xs-6 middlelabel">
                                        <div style="float: left">
                                            تاریخ ارسال مدارک به حوزه معاونت آموزشی :
                                        </div>

                                    </div>

                                    <div class="col-xs-6">
                                        <div style="float: right">
                                            <asp:TextBox ID="pcal4" runat="server" ValidationGroup="submit4" Enabled="false" CssClass="form-control" OnTextChanged="pcal4_TextChanged" />
                                        </div>
                                    </div>

                                </div>
                            </td>

                            <td>
                                <asp:Button ID="btnSubmitDefPoint" Text="ثبت" runat="server" CssClass="btn btn-danger" Enabled="false" OnClick="btnSubmitDefPoint_Click" ValidationGroup="point" />
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="row">
                    <div style="clear: both"></div>
                    <div id="dvFinal" runat="server" class="alert alert-info" style="width: 47.8%;" visible="false">
                        <h4>تایید نهایی</h4>
                        <p>برای اینکه دانشجو بتواند درخواست تسویه حساب خود را ثبت کند باید اطلاعات او را تایید نهایی کنید.</p>
                        <p>توجه : بعد از تایید نهایی امکان ویرایش اطلاعات برای شما وجود نخواهد داشت.</p>
                        <asp:Button ID="btnFinalizer" Text="تایید نهایی" runat="server" CssClass="btn btn-primary" OnClick="btnFinalizer_Click" ValidationGroup="finalValidation" />
                        <div id="dvErrorMsg" runat="server" visible="false" class="alert alert-danger">
                            <p>موارد مورد نیاز جهت تایید نهایی</p>
                            <ol>
                                <li>آیا فرم انصراف از مقاله در پک ارسالی می باشد؟</li>
                                <li>آیا پایان نامه دارای اصلاحات می باشد؟</li>
                                <li>نمره پایان نامه</li>
                                <li>تاریخ پذیرش یا انصراف از مقاله </li>
                                <li>تاریخ ارسال صورت جلسه دفاع به آموزش</li>
                                <li>دریافت فرم اصلاح پایان نامه</li>
                                <li>تاریخ ارسال به پژوهش باید قبل از تاریخ ارسال به معاونت آموزشی باشد</li>
                            </ol>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <div class="modal fade" id="modalChangeDefPoint" tabindex="-1" role="dialog" aria-labelledby="">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="background-color:greenyellow; border-radius:5px 5px 0px 0px; padding:1%; color:black">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="confirmMessage" runat="server" Text="نمره پایان نامه در سیدا با سامانه خدمات متفاوت است. آیا مایلید نمره پایان نامه در سامانه خدمات به نمره دانشجو در سیدا بروز رسانی شود؟" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnYes_DefPointUpdate" runat="server" OnClick="btnYes_DefPointUpdate_Click" Text="بله">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="btnNo_DefPointUpdate" runat="server" OnClick="btnNo_DefPointUpdate_Click" Text="خیر">
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
