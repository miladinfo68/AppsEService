<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Tuitional/masterPages/tuitionalMasterpage.Master" CodeBehind="tuitionFormol.aspx.cs" Inherits="IAUEC_Apps.UI.University.Tuitional.tuitionFormol" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style type="text/css">
        .fullSizeRight {
            width: 90%;
            float: right
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
    function showMessage() {
        $('#modal_Message').modal('show');
        }
        function showConfirmModal() {
            $('#modal_Confirm').modal('show');
        }
        </script>
    <div class="modal fade" id="modal_Message" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="HT">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(19, 114, 88, 0.9); background-color: rgba(19, 114, 88, 0.70); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <h4>
                                    <asp:Literal ID="modalMsgText" runat="server" Text="" Mode="PassThrough" /></h4>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(132, 224, 199,0.7); background-color: rgb(132, 224, 199); margin-bottom: 1%; color: white; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">بستن پنجره</button>

                </div>
            </div>
        </div>
    </div>
     <div class="modal fade" id="modal_Confirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="HT">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(19, 114, 88, 0.9); background-color: rgba(19, 114, 88, 0.70); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <h4>
                                    <asp:Literal ID="modalConfirmText" runat="server" Text="" Mode="PassThrough" /></h4>
                                    <asp:HiddenField ID="hdnTypeInsert" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(132, 224, 199,0.7); background-color: rgb(132, 224, 199); margin-bottom: 1%; color: white; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button  id="btnConfirmToInsert" runat="server" class="btn btn-success" OnClick="btnConfirmToInsert_Click"  Text="تایید"></asp:Button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">انصراف</button>
                </div>
            </div>
        </div>
    </div>

    <div class="container" dir="rtl">


        <div class="panel panel-warning">
            <div class="panel panel-heading">
                <h3>درج فرمول شهریه برای ورودی جدید بر اساس ورودی سال قبل</h3>
            </div>
            <div class="panel panel-body">
                <br />
                <div class="row">
                    <div class="col-md-3 col-xs-12">
                        <div class="col-md-3 col-xs-6">
                            <span>ورودی سال</span>
                        </div>
                        <div class="col-md-9 col-xs-6">
                            <asp:TextBox ID="txtEntryYear" runat="server" Enabled="false" CssClass="fullSizeRight"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-12">
                        <div class="col-md-3 col-xs-6">
                            <span>بر اساس ورودی</span>
                        </div>
                        <div class="col-md-9 col-xs-6">
                            <asp:TextBox ID="txtByEntryYear" runat="server" Enabled="false" CssClass="fullSizeRight"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-12">
                        <div class="col-md-2 col-xs-6">
                            <span>مقطع</span>
                        </div>
                        <div class="col-md-10 col-xs-6">
                            <asp:DropDownList ID="ddlLevel" runat="server" Enabled="true" CssClass="fullSizeRight">
                                <asp:ListItem Text="کارشناسی" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="کاردانی" Value="2"></asp:ListItem>
                                <asp:ListItem Text="کارشناسی ناپیوسته" Value="3"></asp:ListItem>
                                <asp:ListItem Text="ارشد ناپیوسته" Value="5"></asp:ListItem>
                                <asp:ListItem Text="دکتری حرفه ای" Value="6"></asp:ListItem>
                                <asp:ListItem Text="دکتری تخصصی" Value="7"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-3 col-xs-12">
                            <span>درصد افزایش شهریه ثابت</span>
                            <asp:RegularExpressionValidator runat="server" ValidationExpression="[1-9][0-9]?" Display="Dynamic" ControlToValidate="txtFixTuition" ErrorMessage="فقط عدد بین 1 تا 99" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtFixTuition" runat="server" MaxLength="2" CssClass=""></asp:TextBox>
                            <span style="font-size:xx-small" class="">درصد</span>
                    </div>
                    <div class="col-md-3 col-xs-12">
                        <span>درصد افزایش شهریه متغیر</span>
                        <asp:RegularExpressionValidator runat="server" ValidationExpression="[1-9][0-9]?" Display="Dynamic" ControlToValidate="txtVarTuition_CurrentEntry" ErrorMessage="فقط عدد بین 1 تا 99" ForeColor="Red"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtVarTuition_CurrentEntry" runat="server" MaxLength="2"></asp:TextBox>
                        <span style="font-size:xx-small">درصد</span>
                    </div>
                    <div class="col-md-3 col-xs-12">
                        <div class="col-md-3">
                            <span>مبلغ بیمه</span>
                        </div>
                        <div class="col-md-9">
                            <asp:RegularExpressionValidator runat="server" ValidationExpression="[1-9][0-9]{3,5}" Display="Dynamic" ControlToValidate="txtInsurance" ErrorMessage="فقط عدد بین 1000 تا 999999 ریال" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtInsurance" runat="server" MaxLength="6" CssClass="fullSizeRight"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-12">
                        <div class="col-md-3">
                            <span>مبلغ خدمات</span>
                        </div>
                        <div class="col-md-9">
                            <asp:RegularExpressionValidator runat="server" ValidationExpression="[1-9][0-9]{3,7}" Display="Dynamic" ControlToValidate="txtService" ErrorMessage="فقط عدد بین 1000 تا 99999999 ریال" ForeColor="Red"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtService" runat="server" MaxLength="8" CssClass="fullSizeRight"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnIncrease_CurrentEntry" runat="server" Text="بررسی و اعمال روی تمام فیلدها" OnClick="btnIncrease_CurrentEntry_Click" CssClass="btn btn-warning" Width="30%" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-danger">
            <div class="panel panel-heading">
                <h3>افزایش فرمول شهریه متغیر برای نیمسال جدید ورودی سالهای قبل</h3>
            </div>
            <div class="panel panel-body">


                <div class="row">

                    <div class="col-md-3">
                        <span>ترم جاری</span>

                        <asp:TextBox ID="txtCurrentTerm" runat="server" Enabled="false"></asp:TextBox>
                    </div>



                    <div class="col-md-3">
                        <span>بر اساس ترم</span>
                        <asp:TextBox ID="txtLastTerm" runat="server" Enabled="false"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        <span>مقطع</span>

                        <asp:DropDownList ID="ddlLevel_OtherEntries" runat="server" Enabled="true">
                            <asp:ListItem Text="تمام مقاطع" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="کارشناسی" Value="1"></asp:ListItem>
                            <asp:ListItem Text="کاردانی" Value="2"></asp:ListItem>
                            <asp:ListItem Text="کارشناسی ناپیوسته" Value="3"></asp:ListItem>
                            <asp:ListItem Text="ارشد ناپیوسته" Value="5"></asp:ListItem>
                            <asp:ListItem Text="دکتری حرفه ای" Value="6"></asp:ListItem>
                            <asp:ListItem Text="دکتری تخصصی" Value="7"></asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div class="col-md-3">
                        <span>درصد افزایش شهریه متغیر</span>
                        <asp:RegularExpressionValidator runat="server" ValidationExpression="[1-9][0-9]?" Display="Dynamic" ControlToValidate="txtVarTuition_OtherEntries" ErrorMessage="فقط عدد بین 1 تا 99" ForeColor="Red"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtVarTuition_OtherEntries" runat="server" MaxLength="2" TextMode="Number"></asp:TextBox>
                        <span>درصد</span>

                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnIncrease_OtherEntries" runat="server" Text="بررسی و اعمال روی تمام فیلدها" OnClick="btnIncrease_OtherEntries_Click" CssClass="btn btn-danger" Width="30%" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
