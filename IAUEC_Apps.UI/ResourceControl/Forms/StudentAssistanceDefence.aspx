<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageConatctSt.Master" AutoEventWireup="true" CodeBehind="StudentAssistanceDefence.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.StudentAssistanceDefence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <link href="../Style/js-persian-cal.css" rel="stylesheet" />
        <script>

            $(document).ready(function () {
                var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate',
                    { extraInputID: 'ContentPlaceHolder1_txtDate', extraInputFormat: 'yyyy/mm/dd' });

        });

            function onLoadRadTimePicker1(sender, args) {
                txtTime = sender;
            }

    </script>
    <style>
        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .RadPicker .rcSelect {
            left: 1px;
        }

        .rcTimePopup {
            border-right: 1px solid #cdcdcd;
            border-left: 1px solid #cdcdcd;
        }

        .RadInput .RadInput_Default .RadInputRTL .RadInputRTL_Default {
            border-right: 1px solid #cdcdcd;
        }

        .RadPicker .RadInput > input {
            float: right !important;
        }

        .RadPicker .riTextBox {
            padding-left: 4.286em !important;
            text-align: center !important;
        }

        img {
            margin-left: 2px;
        }

        .buttonmargin {
            margin-bottom: initial !important;
        }

        .textBoxSearch {
            border-radius: 5px !important;
        }

        .textAlignCenter {
            text-align: center;
            color: black;
        }

        .marginBtn {
            margin-bottom: initial !important;
        }

        .tableBorder {
            border: 2px solid #73879c !important;
            background-color: #1a82c3 !important;
            color: aliceblue !important;
        }

            .tableBorder th {
                border: 1px solid #73879c;
            }

        .backColortable {
            background-color: #1A82C3;
        }

        .table-hover {
            background-color: #c5dbf3 !important;
        }

            .table-hover > tbody > tr:not(.tableBorder):hover {
                background-color: #038677 !important;
                color: whitesmoke;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="modal fade modal-lg" id="myModal" style="margin-left:25%" role="dialog">
             <div class="modal-dialog" style="width: 90%; margin: 25%">
                <div class="modal-content" >
                    <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h5 class="modal-title"><%=msgHeaderModal%></h5>
                                </button>
                            </div>
                            <div class="modal-body">
                                <p><%=msgBodyHeader%></p>
                                <p><%=msgBodyModal%></p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnAlert" runat="server" Text="برگشت به صفحه امورپژوهشی" OnClick="btnAlert_Click" />
                                
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    <div class="container">

            <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <AlertTemplate>
            <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                <div class="rwDialogContent" style="text-align: center">
                    <div style="color: black; font-size: 13px;" class="rwDialogMessage">
                        {1}
                    </div>
                </div>
                <br />
                <div class="rwDialogButtons text-center">
                    <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                </div>
            </div>
        </AlertTemplate>
    </telerik:RadWindowManager>
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group" >
                <div class="panel panel panel-primary">
                    <div class="bg-purple">
                        <img src="../fonts/commitment.png" style="width: 32px; float: right" alt="" />
                        <h5 class="header-inline-display" style="font-family: 'B Titr'">درخواست مساعدت دفاع:</h5>
                    </div>
                    <div class="list-group-item" style="background: #a4935312; height: 200px">

                        <span>اینجانب علیرغم پیگیری های فراوان در اتاق گفتگو سامانه خدمات موفق نشده ام زمان جلسه دفاع را بین ارکان دفاع هماهنگ کنم. لطفا مساعدت نمایید
                            
                        </span>
                         <div class="row">
                                    <div class="col-md-6 ">

                                        <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                
                                      
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="pdate " MaxLength="9" ToolTip="تاریخ پیشنهادی دفاع" />
                                        <asp:RequiredFieldValidator ID="RFVtxtDate" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <img src="../fonts/stopwatch.png" style="width: 32px" alt="" />
                                     
                                        <telerik:RadTimePicker ID="txtTime" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="fa-IR" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm" CssClass="pdate">
                                            <TimeView ID="TimeView4" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="2" StartTime="08:00" EndTime="19:00">
                                            </TimeView>
                                            <DateInput ID="DateInput1" runat="server">
                                                <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                            </DateInput>
                                        </telerik:RadTimePicker>
                                        <asp:RequiredFieldValidator ID="RFVtxtTime" runat="server" ValidationGroup="register" CssClass="alert" ControlToValidate="txtTime" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا زمان مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>


                                    </div>
                                </div>
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="ثبت" ValidationGroup="register"  Width="100px" OnClick="btnSave_Click" class="btn btn-success" Style="float: left" />
                        <asp:Label ID="lblInfo" runat="server" Text="درخواست شما درحال بررسی توسط کارشناسان دانشکده می باشد" CssClass="label" Style="float: left"></asp:Label>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
            var showConfirm = '<%=ShowConfirm%>';

            if (showConfirm === 'True') {
                $('#myModal').modal({ backdrop: 'static', keyboard: false });
                $('#myModal').modal('show');
            }
            $(".collapse").collapse('show');
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate',
                { extraInputID: 'ContentPlaceHolder1_txtDate', extraInputFormat: 'yyyy/mm/dd' });


        });


    </script>

      <script src="../Scripts/js-persian-cal.min.js"></script>
</asp:Content>
