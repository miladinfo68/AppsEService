<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" 
    AutoEventWireup="true" CodeBehind="ManagementExams.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ManagementExams" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <link rel="stylesheet" href="../../../CommonUI/css/js-persian-cal.css" />

    <style>
        .pcalWrapper {
            position: relative;
        }

            .pcalWrapper > input {
                padding-right: 30px;
            }

            /*.pcalBtn.disabled {
            pointer-events: none;
            cursor: default;
        }*/

            .pcalWrapper > a.pcalBtn {
                position: absolute;
                right: 7px;
                top: 0;
                bottom: 0;
                margin: auto;
            }

        .btnStudentPanel, .btnProfpannel {
            clear: both;
            float: right;
            color: white;
            background-color: green;
            margin-right: 10px;
        }

        .lblHeaders {
            float: right;
            font-size: 20px;
            padding-right: 20px;
        }

        .lblShowErrorMessage {
            /*float:right;           
            padding-right:20px;*/
            font-size: 20px;
            color: red;
        }

        .msgWar {
            color: blue;
            font-size: 20px;
        }

        .pnl_First {
            direction: rtl;
            text-align: center;
        }
    </style>

    <script src="../../../CommonUI/js/js-persian-cal.min.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    
    <script>   
        var RadTimePicker1;
        var RadTimePicker2;
        var RadTimePicker3;
        var RadTimePicker4;
      

        
        function checkCal_pnlSecond() {
            var pcal1 = $('#<%= txtPcal1.ClientID %>');
            var pcal2 = $('#<%= txtPcal2.ClientID %>');

            var txtcal1 = $('#<%= txtPcal1.ClientID %>');
            var txtcal2 = $('#<%= txtPcal2.ClientID %>');
<%--            var err = $('#<%= lblError.ClientID %>');--%>
            if (!isNullOrEmpty(txtcal1.val()) && !isNullOrEmpty(txtcal2.val())) {
                var date1 = txtcal1.val().trim();
                var date2 = txtcal2.val().trim();
                var x = strcmp(date1, date2);
                if (x == true) {
                    //err.show();
                    $('#spnErrorDate_Prof').show();
                }
                else {
                    //err.hide();
                    $('#spnErrorDate_Prof').hide();
                }
            }
        }


        function isNullOrEmpty(s) {
            return (s == null || s === "");
        }
        function strcmp(date1, date2) {
            return date2 < date1 ? true : false;
        }



        //==================================================
        function onLoadRadTimePicker1(sender, args) {
            RadTimePicker1 = sender;
        }

        function onLoadRadTimePicker2(sender, args) {
            RadTimePicker2 = sender;
        }

        function onLoadRadTimePicker3(sender, args) {
            RadTimePicker3 = sender;
        }

        function onLoadRadTimePicker4(sender, args) {
            RadTimePicker4 = sender;
        }
        //####################################################
        // costum validators

          function custmValid4Student(sender, args) {
            <%--var time1 = $find("<%=RadTimePicker1.ClientID %>").get_dateInput().get_value();
            //var time2 = $find("<%=RadTimePicker2.ClientID %>").get_dateInput().get_value();--%>
            var time1 = $('#<%=RadTimePicker1.ClientID %>').val();
            var time2 = $('#<%=RadTimePicker2.ClientID %>').val();
            if (time2 <= time1) {
                args.IsValid = false;
                $('#spnErrorTime_Student').show();
                return false;
            } else {
                args.IsValid = true;
                $('#spnErrorTime_Student').hide();
                return true;
            }
          }
        //====================================================
        function custmValid4Prof(sender, args) {
            var time3 = $find("<%=RadTimePicker3.ClientID %>").get_dateInput().get_value();
            var time4 = $find("<%=RadTimePicker4.ClientID %>").get_dateInput().get_value();

            if ((time4 <= time3)) {

                args.IsValid = false;
                $('#spnErrorTime_Prof').show();
                return;
            } else {
                args.IsValid = true;
                $('#spnErrorTime_Prof').hide();
                return;
            }
        }
        //####################################################

        $(document).ready(function () {     
            setTimeout(function () {
                Show_HideCalc();
            }, 300);

        });

        function Show_HideCalc() {
            var btnTextStudent = $('#<%=btnStudentPanel.ClientID%>').val();
            var btnTextProf = $('#<%=btnProfpannel.ClientID%>').val();
            var editText = "ویــــرایش";
            var sabtText = "ثبـــــت";

            if (btnTextStudent == editText) {
                $('#pcalWrapper1 a.pcalBtn').hide();
                $('#pcalWrapper2 a.pcalBtn').hide();
            }
            else {
                $('#pcalWrapper1 a.pcalBtn').show();
                $('#pcalWrapper2 a.pcalBtn').show();
            }

            if (btnTextProf == editText) {
                $('#pcalWrapper3 a.pcalBtn').hide();
                $('#pcalWrapper4 a.pcalBtn').hide();
            }
            else {
                $('#pcalWrapper3 a.pcalBtn').show();
                $('#pcalWrapper4 a.pcalBtn').show();
            }

        }
        //=================================================
        function checkCal() {
            var pcal1 = $('#<%= pcal1.ClientID %>');
            var pcal2 = $('#<%= pcal2.ClientID %>');

           <%-- var err = $('#<%= lblErorr_PnlFirst.ClientID %>');--%>
            if (!isNullOrEmpty(pcal1.val()) && !isNullOrEmpty(pcal2.val())) {
                var date1 = pcal1.val().trim();
                var date2 = pcal2.val().trim();
                var x = strcmp(date1, date2);
                if (x == true) {
                    //err.show();
                    $('#spnErrorDate_Student').show();
                }
                else {
                    //err.hide();
                    $('#spnErrorDate_Student').hide();
                }
            }
        }



    </script>
    <div id="content-section">
        <div id="div_Main" style="padding-left: 15%; padding-right: 5%;" class="pnl_First">

            <asp:Panel ID="pnl_First" runat="server" Enabled="false">
                <br />
                <br />
                <asp:Label CssClass="lblHeaders" Text="زمان بازشدن دسترسی دانشجو جهت انتخاب محل امتحانات" runat="server" />
                <div class="row">
                    <div class="col-sm-10 ">
                        <div class="table-responsive">
                            <table class="table table-bordered table-condensed table-striped  tbl">
                                <tr>
                                    <td id="dvTime" runat="server">
                                        <div class="row">

                                            <asp:ValidationSummary ID="submit" runat="Server" ShowMessageBox="true" ValidationGroup="submit" />

                                            <div id="dvStartDate" class="col-md-6">
                                                <p id="dateofsession">تاریخ شروع :</p>
                                                <div id="pcalWrapper1" class="pcalWrapper">
                                                    <asp:TextBox ID="pcal1" runat="server" CssClass="pdate form-control " />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal1" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                            </div>

                                            <div class="col-md-6">
                                                <p>ساعت آغاز :</p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                <asp:CustomValidator ID="startTime4Student" ErrorMessage="زمان آغازین باید کمتر اززمان پایان باشد" ValidationGroup="submit" ForeColor="Red" Display="None" ClientValidationFunction="custmValid4Student" ControlToValidate="RadTimePicker1" runat="server" />
                                                <telerik:RadTimePicker ID="RadTimePicker1" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView1" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00"></TimeView>
                                                    <DateInput ID="DateInput1" runat="server">
                                                        <ClientEvents OnLoad="onLoadRadTimePicker1"></ClientEvents>
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <span id="spnErrorDate_Student" style="display: none; color: red;">تاریخ پایان باید بیشتر از تاریخ شروع باشد</span>
                                            </div>
                                            <div class="col-md-6 ">
                                                <span id="spnErrorTime_Student" style="display: none; color: red;">زمان پایان باید بیشتر اززمان شروع باشد</span>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div id="dvEndDate" class="col-md-6">
                                                <p id="enddateofsession">تاریخ پایان :</p>
                                                <div id="pcalWrapper2" class="pcalWrapper">
                                                    <asp:TextBox ID="pcal2" runat="server" CssClass="pdate form-control" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="pcal2" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="pcal2" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>

                                            </div>

                                            <div class="col-md-6">
                                                <p>ساعت پایان :</p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                                <asp:CustomValidator ID="vldTimes" ErrorMessage="زمان پایان باید بیشتر اززمان آغاز باشد" ValidationGroup="submit" ForeColor="Red" Display="None" ClientValidationFunction="custmValid4Student" ControlToValidate="RadTimePicker2" runat="server" />
                                             
                                                <telerik:RadTimePicker ID="RadTimePicker2" RenderMode="Lightweight" runat="server" EnableTyping="false" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView2" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                    <DateInput ID="DateInput2" runat="server">
                                                        <ClientEvents OnLoad="onLoadRadTimePicker2"></ClientEvents>
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <br />
            <asp:Button Text="ویــــرایش" runat="server" ID="btnStudentPanel" OnClick="btnStudentPanel_Click" CssClass="btnStudentPanel" />

            <br />
            <br />           
            <asp:Label ID="lblShowErrorMessage" CssClass="lblShowErrorMessage" runat="server" Visible="false"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Panel ID="pnl_Second" runat="server" Enabled="false">
                <br />
                <br />
                <asp:Label CssClass="lblHeaders" Text="زمان بازشدن دسترسی استاد جهت بارگذاری سوالات امتحان" runat="server" />
                <br />
                <br />
                <div class="row">
                    <div class="col-sm-10 ">
                        <div class="table-responsive">
                            <table class="table table-bordered table-condensed table-striped  tbl">
                                <tr>
                                    <td id="Td2" runat="server">
                                        <div class="row">
                                            <div id="dvStartDate2" class="col-md-6">
                                                <p id="dateofsession2">تاریخ شروع :</p>
                                                <div id="pcalWrapper3" class="pcalWrapper">
                                                    <asp:TextBox ID="txtPcal1" runat="server" CssClass="pdate form-control" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="txtPcal1" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                            </div>
                                            <div class="col-md-6">
                                                <p>ساعت آغاز :</p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="لطفا زمان آغاز را مشخص کنید" ValidationGroup="submit" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker3" runat="server" />
                                                <asp:CustomValidator ID="cstmVal4ProfPanel2" ErrorMessage="زمان آغازین باید کمتر اززمان پایان باشد" ValidationGroup="submit" ForeColor="Red" Display="None" ClientValidationFunction="custmValid4Prof" ControlToValidate="RadTimePicker3" runat="server" />
                                                 <telerik:RadTimePicker ID="RadTimePicker3" EnableTyping="false" RenderMode="Lightweight" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView3" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00"></TimeView>
                                                    <DateInput ID="DateInput3" runat="server">
                                                        <ClientEvents OnLoad="onLoadRadTimePicker3"></ClientEvents>
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </div>
                                        </div>
                                    
                                        <div class="row">
                                            <div class="col-md-6">
                                                <span id="spnErrorDate_Prof" style="display: none; color: red;">تاریخ پایان باید بیشتر از تاریخ شروع باشد</span>
                                            </div>
                                            <div class="col-md-6 ">
                                                <span id="spnErrorTime_Prof" style="display: none; color: red;">زمان پایان باید بیشتر اززمان شروع باشد</span>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div id="dvEndDate2" class="col-md-6">
                                                <p id="enddateofsession2">تاریخ پایان :</p>                                         
                                                <div id="pcalWrapper4" class="pcalWrapper">
                                                    <asp:TextBox ID="txtPcal2" runat="server" CssClass="pdate form-control" />
                                                </div>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Text="*" ErrorMessage="لطفا تاریخ درخواست خود را مشخص کنید" ControlToValidate="txtPcal2" ForeColor="Red" ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPcal2" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>
                                              
                                            </div>
                                            <div class="col-md-6">
                                                <p>ساعت پایان :</p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ErrorMessage=" لطفا زمان پایان را مشخص کنید" Text="*" ValidationGroup="submit" ForeColor="Red" Display="Dynamic" ControlToValidate="RadTimePicker4" runat="server" />
                                                <asp:CustomValidator ID="custmValid4Prof" ErrorMessage="زمان پایان باید بیشتر از زمان شروع باشد" ValidationGroup="submit" ForeColor="Red" Display="None" ClientValidationFunction="custmValid4Prof" ControlToValidate="RadTimePicker4" runat="server" />
                                                  <telerik:RadTimePicker ID="RadTimePicker4" RenderMode="Lightweight" runat="server" EnableTyping="false" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                    <TimeView ID="TimeView4" Interval="01:00:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                    </TimeView>
                                                    <DateInput ID="DateInput4" runat="server">
                                                        <ClientEvents OnLoad="onLoadRadTimePicker4"></ClientEvents>
                                                    </DateInput>
                                                </telerik:RadTimePicker>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


            </asp:Panel>
            <br />
            <asp:Button Text="ویــــرایش" runat="server" ID="btnProfpannel" OnClick="btnProfpannel_Click" CssClass="btnProfpannel" />

            <telerik:RadWindowManager ID="radWindowManager" runat="server"></telerik:RadWindowManager>
        </div>
    </div>

</asp:Content>



