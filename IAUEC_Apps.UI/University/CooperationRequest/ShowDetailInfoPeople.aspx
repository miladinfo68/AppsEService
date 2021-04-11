<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="ShowDetailInfoPeople.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ShowDetailInfoPeople" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ShowDetailInfoPeople.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ShowDetailInfoPeople" %>

<%--<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="css/js-persian-cal.css" rel="stylesheet" />
    <script src="Scripts/js-persian-cal.min.js"></script>
    <style>
        .well {
            margin-bottom: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>--%>
    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
        function closeModal() {
            $('#exampleModal').modal('hide');
        }
        function radModal() {
            $('#rad_modal').modal('show');
        }
        function closeradModal() {
            $('#rad_modal').modal('hide');
        }
        function CantRegModal() {
            $('#cantReg_modal').modal('show');
        }

        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = sender.getElementById();
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
        $(document).ready(function () {

            $("input:radio[name='rdblGender']").click(function () {

                var selectedRadio = $("input:radio[name='rdblGender']:checked").val();
                var ddlVazife = document.getElementById('<%=drpNezam.ClientID%>');
                switch (selectedRadio.value) {
                    case 1:
                        (ddlVazife).enable = true;
                        break;
                    case 2:
                        ddlVazife.options[ddlVazife.SelectedValue] = 0;
                        (ddlVazife).enable = false;
                        break;
                }
                //Code to fetch complex datatype
                //$.ajax({
                //    type: "POST",
                //    url: "/Samples.aspx/GetStatesWithAbbr",
                //    dataType: "json",
                //    data: "{ id :'" + selectedRadio + "'}",
                //    contentType: "application/json; charset=utf-8",
                //    success: function (msg) {
                //        //alert(msg.d);
                //        $("#ddlStates").get(0).options.length = 0;
                //        $("#ddlStates").get(0).options[0] = new Option("-- Select state --", "-1");

                //        $.each(msg.d, function (index, item) {
                //            $("#ddlStates").get(0).options[$("#ddlStates").get(0).options.length] = new Option(item.Name, item.Abbreviation);
                //        });
                //    },
                //    error: function () {
                //        alert('error');
                //    }
                //});

            });

        });
    </script>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="confirmMessage" runat="server" Text="" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="rbConfirm_OK1" runat="server" OnClick="rbConfirm_OK1_Click" Text="بله">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="rbConfirm_Cancel1" runat="server" OnClientClicked="closeCustomConfirm1" Text="خیر">
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="rad_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H1">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="MsgConf" runat="server" Text="" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="RadButton1" runat="server" OnClick="RadButton_Click" Text="بله">
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RadButton2" OnClientClicked="closeradModal()" runat="server" Text="خیر">
                                    </telerik:RadButton>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="cantReg_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="HT">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="msgIncorrectScan" runat="server" Text="" Mode="PassThrough" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                </div>
            </div>
        </div>
    </div>

    <div>
        <div class="">
            <div class="page-header" style="direction: rtl">
                <h2>درخواست همکاری اساتید</h2>
            </div>
            <div style="direction: rtl">
                <div id="dvPersonInfo" class="panel panel-primary">
                    <div class="panel-heading">مشخصات فردی</div>
                    <div class="panel-body well">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label1" Text="کد ملی" runat="server" />
                                        <asp:TextBox ID="txtCodeMeli" ReadOnly="true" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label2" Text="نام" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredName" ForeColor="Red" runat="server" ErrorMessage="لطفا نام را وارد فرمایید" ControlToValidate="txtFirstName" ValidationGroup="vg" Text="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[\u0600-\u06FF\s]+$" ControlToValidate="txtFirstName" ForeColor="Red" Text="*" ValidationGroup="vg" ErrorMessage="لطفا یرای نام فقط از حروف فارسی استفاده فرمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label3" Text="نام خانوادگی" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFamily" ErrorMessage="لطفا نام خانوادگی را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" ControlToValidate="txtFamily" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[\u0600-\u06FF\s]+$" ControlToValidate="txtFamily" ForeColor="Red" Text="*" ValidationGroup="vg" ErrorMessage="لطفا برای نام خانوادگی فقط از حروف فارسی استفاده فرمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label4" Text="نام پدر" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFather" ErrorMessage="لطفا نام پدر را وارد فرمایید" ForeColor="Red" ValidationGroup="vg" Text="*" ControlToValidate="txtFatherName" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^[\u0600-\u06FF\s]+$" ControlToValidate="txtFatherName" ForeColor="Red" Text="*" ValidationGroup="vg" ErrorMessage="لطفا برای نام پدر فقط از حروف فارسی استفاده فرمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label5" Text="شماره شناسنامه" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="لطفا شماره شناسنامه را وارد فرمایید" ForeColor="Red" ValidationGroup="vg" Text="*" ControlToValidate="txtShCode" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^([\d]+)$" ControlToValidate="txtShCode" ForeColor="Red" Text="*" ValidationGroup="vg" ErrorMessage="لطفا برای شناسنامه فقط عدد وارد فرمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtShCode" runat="server" CssClass="form-control" MaxLength="10" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label7" Text="سال تولد" runat="server" />
                                        <asp:RangeValidator ID="RangeValidator2" ErrorMessage="سال تولد باید عددی بین 1300 تا 1400 باشد" ControlToValidate="txtYearBorn" runat="server" ValidationGroup="vg" Text="*" ForeColor="Red" MaximumValue="1400" MinimumValue="1300" Type="Integer" />
                                        <asp:RequiredFieldValidator ID="RequiredValidator11" runat="server" ControlToValidate="txtYearBorn" Text="*" ForeColor="Red" ErrorMessage="لطفا سال تولد را درج فرمایید" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtYearBorn" runat="server" CssClass="form-control" MaxLength="4" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label6" Text="جنسیت" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvBime" ControlToValidate="rdblGender" runat="server" ValidationGroup="vg" InitialValue="" ErrorMessage="لطفا جنسیت را مشخص فرمایید" ForeColor="Red" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <asp:RadioButtonList ID="rdblGender" runat="server" CssClass="radio" OnSelectedIndexChanged="rdblGender_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="مرد" Value="1" />
                                                    <asp:ListItem Text="زن" Value="2" />
                                                </asp:RadioButtonList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label8" Text="محل تولد" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ErrorMessage="لطفا محل تولد را مشخص فرمایید" InitialValue="0" ControlToValidate="drpBirthCity" ForeColor="Red" Display="Dynamic" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="drpBirthCity" CssClass="form-control dropdown" Height="40px" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpBirthCity" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label9" Text="محل صدور" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ErrorMessage="لطفا محل صدور شناسنامه را مشخص فرمایید" InitialValue="0" ControlToValidate="drpMahalSodoor" ForeColor="Red" Display="Dynamic" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="drpMahalSodoor" CssClass="form-control dropdown" Height="40px"></asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpMahalSodoor" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label10" Text="وضعیت نظام وظیفه" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ErrorMessage="لطفا وضعیت نظام وظیفه را مشخص فرمایید" InitialValue="-1" ControlToValidate="drpNezam" ForeColor="Red" Display="Dynamic" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpNezam" runat="server" CssClass="form-control" Height="40px"></asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpNezam" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label11" Text="وضعیت تاهل" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="rdblMarriage" runat="server" ValidationGroup="vg" InitialValue="" ErrorMessage="لطفا وضعیت تاهل را مشخص فرمایید" ForeColor="Red" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                        <asp:RadioButtonList ID="rdblMarriage" runat="server" RepeatDirection="Horizontal" CssClass="radio">
                                            <asp:ListItem Text="مجرد" Value="1" />
                                            <asp:ListItem Text="متاهل" Value="2" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label12" Text="آخرین وضعیت تحصیلی" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpLastMaghta" runat="server" CssClass="form-control form-inline dropdown" Height="40px">
                                                    <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                    <asp:ListItem Text="کارشناسی ارشد پیوسته" Value="9" />
                                                    <asp:ListItem Text="کارشناسی ارشد ناپیوسته" Value="10" />
                                                    <asp:ListItem Text="دانشجوی دکتری بعد از گذراندن امتحان جامع" Value="13" />
                                                    <asp:ListItem Text="دانشجوی دکتری قبل از گذراندن امتحان جامع" Value="12" />
                                                    <asp:ListItem Text="دکتری تخصصی" Value="2" />
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpLastMaghta" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label13" Text="رشته تحصیلی" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="drpReshte" runat="server" ValidationGroup="vg" InitialValue="0" ErrorMessage="لطفا رشته تحصیلی را مشخص فرمایید" ForeColor="Red" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpReshte" runat="server" CssClass="form-control" AutoPostBack="true" Height="40px" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpReshte" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:Label ID="Label30" Text="درخواست همکاری در" runat="server" />
                                                <asp:CheckBoxList ID="chk_Cooperation" runat="server" CssClass="checkbox" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="true">
                                                    <asp:ListItem Text="آموزش" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="پژوهش" Value="2"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="chk_Cooperation" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label14" Text="سال اخذ مدرک" runat="server" />
                                        <asp:RangeValidator ID="RangeValidator1" ErrorMessage="سال اخذ مدرک باید عددی بین 1300 تا 1400 باشد" ControlToValidate="txtYearGetMadrak" runat="server" ValidationGroup="vg" Text="*" ForeColor="Red" MaximumValue="1400" MinimumValue="1300" Type="Integer" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtYearGetMadrak" Text="*" ForeColor="Red" ErrorMessage="لطفا سال اخذ آخرین مدرک را درج فرمایید" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtYearGetMadrak" CssClass="form-control" Width="60px" MaxLength="4" />
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="Label15" Text="سنوات تدریس" runat="server" />
                                        <asp:RangeValidator ID="RangeValidator3" ErrorMessage="سنوات تدریس باید عددی بین 0 تا 99 سال باشد" ControlToValidate="txtSanavat" Type="Integer" ForeColor="Red" MinimumValue="0" MaximumValue="99" Text="*" ValidationGroup="vg" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtSanavat" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" ErrorMessage="لطفا سنوات تدریس را درج فرمایید"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSanavat" runat="server" CssClass="form-control" Width="60px" MaxLength="2" />
                                        <asp:Label ID="lbl_Sal" runat="server" Text="سال"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label16" Text="کشور محل اخذ مدرک" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="drpCountry" runat="server" ValidationGroup="vg" InitialValue="-1" ErrorMessage="لطفا کشور محل اخذ مدرک را مشخص فرمایید" ForeColor="Red" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpCountry" runat="server" CssClass="form-control" Height="40px" Width="260px"></asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpCountry" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label17" Text="نام دانشگاه محل اخذ آخرین مدرک تحصیلی" runat="server" Width="280px" />
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ControlToValidate="drpUniName" InitialValue="-1" ErrorMessage="لطفا برای نام دانشگاه محل تحصیل یکی از موارد را انتخاب فرمایید" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpUniName" runat="server" CssClass="form-control" Height="40px" Width="260px"></asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpUniName" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label32" Text="نوع دانشگاه محل اخذ آخرین مدرک تحصیلی" runat="server" Width="280px" />
                                        <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ControlToValidate="drpUniversityType" InitialValue="0" ErrorMessage="لطفا برای نوع  دانشگاه محل تحصیل یکی از موارد را انتخاب فرمایید" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpUniversityType" runat="server" CssClass="form-control" Height="40px" Width="260px">
                                                    <asp:ListItem Text="انتخاب کنید" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="دولتی" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="آزاد" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="حوزه" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="خارج از کشور" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="سایر" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpUniversityType" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7" style="margin-top: 50px">
                                        <asp:Button ID="btn_ShowPDF" runat="server" OnClick="btn_ShowPDF_Click" Text="دانلود رزومه" CssClass="btn btn-info" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="pnl" runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:CheckBox ID="chk_IsRetired" runat="server" Text="بازنشسته" CssClass="checkbox" OnCheckedChanged="chk_IsRetired_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-2">
                                        <%--<asp:Label Text="وضعیت بیمه" runat="server" />--%>
                                        <asp:RequiredFieldValidator ErrorMessage="لطفا وضعیت بیمه بودن یا نبودن را مشخص فرمایید" Display="None" ForeColor="Red" ValidationGroup="vg" Text="*" ControlToValidate="rdblBimehStatus" runat="server" />
                                        <asp:RadioButtonList ID="rdblBimehStatus" OnSelectedIndexChanged="rdblBimehStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="radio" runat="server">
                                            <asp:ListItem Text="دارای سابقه بیمه" Value="1" />
                                            <asp:ListItem Text="بدون سابقه بیمه" Value="2" Selected="True" />
                                        </asp:RadioButtonList>
                                        <asp:Label Text="نوع بیمه" runat="server" />
                                        <asp:RequiredFieldValidator ID="valBimehType" ErrorMessage="لطفا نوع بیمه را مشخص فرمایید" InitialValue="0" ControlToValidate="drpBimehType" ForeColor="Red" Display="Dynamic" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:DropDownList ID="drpBimehType" Enabled="false" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="انتخاب کنید" Value="0" />
                                            <asp:ListItem Text="مشمول بیمه" Value="1" />
                                            <asp:ListItem Text="لشکری" Value="2" />
                                            <asp:ListItem Text="کشوری" Value="3" />
                                            <asp:ListItem Text="خدمات درمانی" Value="4" />
                                            <asp:ListItem Text="سلامت" Value="5" />
                                            <asp:ListItem Text="تامین اجتماعی" Value="6" />
                                            <asp:ListItem Text="بازنشسته" Value="7" />
                                            <asp:ListItem Text="سایر موارد" Value="8" />

                                            <%--<asp:ListItem Text=" " Value="10" />--%>
                                        </asp:DropDownList>
                                        <asp:Label ID="Label26" Text="شماره بیمه" runat="server" />
                                        <asp:RegularExpressionValidator ErrorMessage="شماره بیمه وارد شده باید 10 رقمی باشد" ControlToValidate="txtInsuranceNumber" ValidationExpression="^[0-9]{10}$" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtInsuranceNumber" ErrorMessage="لطفاشماره بیمه را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:TextBox ID="txtInsuranceNumber" Enabled="false" runat="server" CssClass="form-control" EnableViewState="True" MaxLength="10" />
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label18" Text="دانشکده مورد نظر جهت تدریس" runat="server" />
                                        <asp:CheckBoxList ID="chbkDaneshkade" runat="server" CssClass="checkbox" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chbkDaneshkade_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="Label42" Text="گروههای آموزشی همکاری" runat="server" />
                                        <asp:CheckBoxList ID="chbkGroup" runat="server" CssClass="checkbox"></asp:CheckBoxList>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chbkGroup" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chbkDaneshkade" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <asp:Panel ID="pnlSabeghe" runat="server" Enabled="false">
                    <div id="dvHeiatElmi" class="panel panel-success" runat="server" visible="false">
                        <div class="panel-heading">سوابق فعالیت</div>
                        <div class="panel-body well">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <asp:Label ID="Label19" Text="مرتبه دانشگاهی" runat="server" />
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="drpMartabe" ErrorMessage="لطفا مرتبه دانشگاهی را انتخاب فرمایید." InitialValue="-1" ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                                    <asp:DropDownList ID="drpMartabe" runat="server" CssClass="form-control" Height="40px">
                                                        <asp:ListItem Text="انتخاب کنید" Value="-1" Selected="True" />
                                                        <asp:ListItem Text="فاقد مرتبه علمی" Value="8" />
                                                        <asp:ListItem Text="مربی" Value="1" />
                                                        <asp:ListItem Text="دانشیار" Value="2" />
                                                        <asp:ListItem Text="استادیار" Value="3" />
                                                        <asp:ListItem Text="استاد" Value="4" />
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="drpMartabe" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-5">
                                            <asp:Label ID="Label20" Text="پایه" runat="server" />
                                            <asp:RangeValidator ID="RangeValidator4" ErrorMessage="مقدار پایه باید عددی بین 0 و 50 باشد" Type="Integer" MinimumValue="0" MaximumValue="50" ControlToValidate="txtPaye" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtPaye" ErrorMessage="لطفا پایه خدمتی وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                            <asp:TextBox ID="txtPaye" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <asp:Label ID="Label21" Text="نوع استخدام" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" ControlToValidate="drpHireType" runat="server" ValidationGroup="vg" InitialValue="0" ErrorMessage="لطفا نحوه همکاری را مشخص فرمایید" ForeColor="Red" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpHireType" runat="server" CssClass="form-control" Height="40px">
                                                        <asp:ListItem Text="انتخاب کنید" Selected="True" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="رسمی" Value="1" />
                                                        <asp:ListItem Text="آزمایشی" Value="2" />
                                                        <asp:ListItem Text="قراردادی" Value="3" />
                                                        <asp:ListItem Text="مامور به خدمت" Value="4" />
                                                        <asp:ListItem Text="موقت" Value="5" />
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="drpHireType" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label43" Text="نوع دانشگاه محل خدمت" runat="server" Width="280px" />
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="vg" ControlToValidate="ddlPastUniType" InitialValue="0" ErrorMessage="لطفا برای نوع دانشگاه محل خدمت یکی از موارد را انتخاب فرمایید" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlPastUniType" runat="server" CssClass="form-control" Height="40px" Width="260px">
                                                        <asp:ListItem Text="انتخاب کنید" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="دولتی" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="آزاد" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="حوزه" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="خارج از کشور" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="سایر" Value="5"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlPastUniType" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <asp:Label ID="Label22" Text="دانشگاه محل خدمت" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="drpPastUni" runat="server" ValidationGroup="vg" InitialValue="-1" ErrorMessage="لطفا نام دانشگاه محل خدمت را مشخص فرمایید" ForeColor="Red" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpPastUni" runat="server" CssClass="form-control" Height="40px"></asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="drpPastUni" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label23" Text="تاریخ صدور حکم کارگزینی" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ErrorMessage="لطفا تاریخ صدور حکم کارگزینی را وارد فرمایید" ControlToValidate="txtDateSodoorHokm" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />
                                            <asp:TextBox ID="txtDateSodoorHokm" runat="server" CssClass="form-control form-inline" />
                                            <script>
                                                var objCal1 = new AMIB.persianCalendar('<%=txtDateSodoorHokm.ClientID%>',
                                                    { extraInputID: '<%=txtDateSodoorHokm.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                                            </script>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label24" Text="تاریخ اجرای حکم" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ErrorMessage="لطفا تاریخ اجرای حکم را وارد فرمایید" ControlToValidate="txtDateEjraHokm" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />
                                            <asp:TextBox ID="txtDateEjraHokm" runat="server" CssClass="form-control" />
                                            <script>
                                                var objCal1 = new AMIB.persianCalendar('<%=txtDateEjraHokm.ClientID%>',
                                                    { extraInputID: '<%=txtDateEjraHokm.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                                            </script>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label25" Text="شماره حکم" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="لطفا شماره حکم را وارد فرمایید" ControlToValidate="txtHokmNumber" ForeColor="Red" Text="*" ValidationGroup="vg" runat="server" />
                                            <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:CheckBox ID="chk_Boundhour" runat="server" Text="متقاضی تکمیل ساعت موظفی" CssClass="checkbox" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                    </div>
                                    <div class="col-md-10">
                                        <asp:Label ID="Label28" Text="نحوه همکاری در دانشگاه مبدا" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="rdblHireType" ErrorMessage="لطفا نحوه همکاری در دانشگاه مبدا را انتخاب فرمایید." InitialValue="" ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:RadioButtonList ID="rdblHireType" runat="server" RepeatColumns="2" CellPadding="4" CellSpacing="8" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="تمام وقت 32 ساعت" Value="1" />
                                            <asp:ListItem Text="نیمه وقت" Value="2" />
                                            <asp:ListItem Text="ساعتی" Value="3" />
                                            <asp:ListItem Text="تمام وقت طرح مشمولان" Value="4" />
                                            <asp:ListItem Text="بورسیه دکتری" Value="5" />
                                            <asp:ListItem Text="کارمند" Value="6" />
                                            <asp:ListItem Text="تمام وقت 44 ساعت" Value="7" />
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-10">
                                        <span>مبلغ حکم</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtMablaghHokm" ErrorMessage="لطفا مبلغ حکم را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:TextBox ID="txtMablaghHokm" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />

                <div id="dvContactInfo" class=" panel panel-info">
                    <div class="panel-heading">اطلاعات تماس</div>
                    <div class="panel-body well">
                        <div class="row">
                            <div class="col-md-4 ">
                                <div class="row">
                                    <div class="col-md-7 ">
                                        <asp:Label ID="Label29" Text="تلفن منزل" runat="server" />
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="(^(0[1-8]{2}[2-8][0-9]{7})$)?" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtHomePhone" ErrorMessage="لطفا شماره تلفن منزل را صحیح وارد فرمایید" Text="*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtHomePhone" ErrorMessage="لطفاشماره تلفن منزل را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:TextBox ID="txtHomePhone" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7 ">
                                        <asp:Label ID="Label31" Text="تلفن محل کار" runat="server" />
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="(^(0[1-8]{2}[2-8][0-9]{7})$)?" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtWorkPhone" ErrorMessage="لطفا شماره تلفن محل کار  را صحیح وارد فرمایید" Text="*"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-control form-inline" MaxLength="11" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label33" Text="تلفن همراه" runat="server" />
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="09[\d+]{9}" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtMobileNumber" ErrorMessage="لطفا شماره تلفن همراه را صحیح وارد فرمایید" Text="*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtMobileNumber" ErrorMessage="لطفا شماره تلفن همراه را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" MaxLength="11" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label34" Text="پست الکترونیک" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ErrorMessage="لطفاآدرس پست الکترونیک را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" ControlToValidate="txtEmail" runat="server" />
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="\w+([-_.]?\w+)?@\w+([-_.]?\w+)?\.\w+([-_.]?\w+)?" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="لطفا ایمیل را صحیح وارد فرمایید" Text="*"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control text-left " MaxLength="55" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label27" Text="شماره حساب سیبا" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtSiba" ErrorMessage="لطفا شماره حساب را وارد فرمایید." ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ErrorMessage="شماره حساب باید 13 رقم باشد" ControlToValidate="txtSiba" ValidationExpression="^[0-9]{13}$" runat="server" Text="*" ForeColor="Red" ValidationGroup="vg" />
                                        <asp:TextBox ID="txtSiba" runat="server" CssClass="form-control" EnableViewState="True" MaxLength="13" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8 col-md-pull-1">
                                <div class="row">
                                    <div class="col-md-10">
                                        <h4>آدرس محل سکونت</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1">
                                        <asp:Label ID="Label35" Text="استان" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="drpProvince1" ErrorMessage="لطفا استان محل سکونت را انتخاب فرمایید." InitialValue="0" ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>
                                                <asp:CustomValidator ID="rfv_HomeState" runat="server" ValidationGroup="vg" ControlToValidate="drpProvince1" ErrorMessage="لطفا استان محل سکونت را انتخاب فرمایید" ForeColor="Red" Text="*" OnServerValidate="rfv_HomeState_ServerValidate"></asp:CustomValidator>
                                                <asp:DropDownList ID="drpProvince1" runat="server" CssClass=" form-control" AutoPostBack="true" Height="40px" OnSelectedIndexChanged="drpProvince1_SelectedIndexChanged" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpProvince1" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label36" Text="شهر" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="drpLivingCity" ErrorMessage="لطفا شهر محل سکونت را انتخاب فرمایید." InitialValue="0" ForeColor="Red" ValidationGroup="vg" Text="*" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:CustomValidator ID="rfv_HomeCity" runat="server" ValidationGroup="vg" ForeColor="Red" ControlToValidate="drpLivingCity" ErrorMessage="لطفا شهر محل سکونت را انتخاب فرمایید" OnServerValidate="rfvHomeCity_ServerValidate" Text="*"></asp:CustomValidator>
                                                <asp:DropDownList ID="drpLivingCity" runat="server" CssClass="form-control" Height="40px" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpLivingCity" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Label ID="Label38" Text="کد پستی" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtLivingZipCode" ErrorMessage="لطفا کد پستی را وارد فرمایید" Text="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator runat="server" ValidationExpression="^([1-9][0-9]{9})$" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtLivingZipCode" ErrorMessage="لطفا کد پستی محل سکونت را به طور صحیح وارد فرمایید" Text="*"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtLivingZipCode" runat="server" CssClass="form-control" MaxLength="10" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Label ID="Label37" Text="آدرس" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vg" ForeColor="Red" ControlToValidate="txtLivingAddress" ErrorMessage="لطفا آدرس محل سکونت را وارد فرمایید" Text="*"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtLivingAddress" runat="server" CssClass=" form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-md-offset-1">
                                        <h4>آدرس محل کار</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1">
                                        <asp:Label ID="Label40" Text="استان" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <asp:CustomValidator ID="rfv_WorkState" runat="server" ValidationGroup="vg" ForeColor="Red" ControlToValidate="drpProvince2" ErrorMessage="لطفا استان محل کار را انتخاب فرمایید" Text="*" OnServerValidate="rfv_WorkState_ServerValidate"></asp:CustomValidator>
                                                <asp:DropDownList ID="drpProvince2" runat="server" CssClass=" form-control" AutoPostBack="true" OnSelectedIndexChanged="drpProvince2_SelectedIndexChanged" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpProvince2" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label39" Text="شهر" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <asp:CustomValidator ID="rfv_WorkCity" runat="server" ValidationGroup="vg" ForeColor="Red" ControlToValidate="drpWorkingCity" OnServerValidate="rfv_WorkCity_ServerValidate" ErrorMessage="لطفا شهر محل کار را انتخاب فرمایید" Text="*"></asp:CustomValidator>
                                                <asp:DropDownList ID="drpWorkingCity" runat="server" CssClass="form-control" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpWorkingCity" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Label ID="Label41" Text="آدرس" runat="server" />
                                        <asp:TextBox ID="txtWorkingAddress" runat="server" CssClass=" form-control" />
                                    </div>
                                    <div class="col-md-3"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="Div1" class="panel panel-danger" runat="server" visible="false">
                    <div class="panel-heading">مدارک فرد</div>
                    <div class="panel-body well">
                        <div class="row">
                            <telerik:RadListView runat="server" ID="rlv" OnItemDataBound="rlv_ItemDataBound" OnItemCommand="rlv_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-4" style="border: groove">
                                        <div class="row">
                                            <div class="col-md-6" style="color: dodgerblue; margin-bottom: 20px;">
                                                <asp:Label ID="lbl_Name" runat="server"></asp:Label>
                                            </div>
                                            <div style="margin: 10px; max-height: 150px; max-width: 70px; min-height: 150px; min-width: 70px;">
                                                <asp:Button ID="btn_Madrak" runat="server" Visible="false" Text='<%#Eval("document_name") %>' CommandArgument='<%#Eval("doc_type")%>' CommandName="Select" />
                                                <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" Visible="false" AutoAdjustImageControlSize="false" ResizeMode="Fit" Width="80px" Height="70px" />
                                                <asp:Button ID="btn_ShowPicture" runat="server" Visible="false" Text="بزرگنمایی" OnClientClick="target='_blank';" CommandArgument='<%#Eval("doc_type")%>' CommandName="ShowPic" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <asp:Panel ID="pnlMadrakOperation" runat="server">


                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>

                                                        <div class="col-md-4">
                                                            <asp:RadioButton ID="rdb_Taeed" Text="تأیید مدرک" runat="server" ValidationGroup="0" GroupName="gh" AutoPostBack="true" OnCheckedChanged="rbtnDocStatus_CheckedChange" />


                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:RadioButton ID="rdb_Naghs" Text="نقص مدرک" runat="server" ValidationGroup="1" GroupName="gh" AutoPostBack="true" OnCheckedChanged="rbtnDocStatus_CheckedChange" />

                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lbl_Sharh" Text="علت رد:" runat="server"></asp:Label>
                                                            <asp:TextBox ID="txt_Sharh" runat="server" MaxLength="300" Width="200" Height="100" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadListView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-danger" style="background-color: azure" id="dvEditByProfessor" runat="server" visible="true">
        <div class="panel-heading" dir="rtl">ویرایش مدارک</div>
        <div class="row" dir="rtl">
            <div style="margin-right: 30px;">
                <span>آیااطلاعات فردی از طریق استاد ویرایش شود؟</span>
                <%--<asp:RadioButtonList ID="RdbEditingPeopleInfo" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                    OnSelectedIndexChanged="RadioButtonPeopleEditing_SelectedIndexChanged" >
                    <asp:ListItem Value="yes">بله</asp:ListItem>
                    <asp:ListItem Value="no">خیر</asp:ListItem>
                </asp:RadioButtonList>--%>
                <br />
                <asp:RadioButton ID="rbtnYes" runat="server" GroupName="RdbEditingPeopleInfo" Text="بله" OnCheckedChanged="rbtn_CheckedChanged" AutoPostBack="true" />
                <asp:RadioButton ID="rbtnNo" runat="server" GroupName="RdbEditingPeopleInfo" Text="خیر" OnCheckedChanged="rbtn_CheckedChanged" AutoPostBack="true" />

            </div>
        </div>
        <div class="row" style="margin-right: 25px;" dir="rtl">
            <asp:UpdatePanel ID="UpdatePanelEditingPeopleInfo" runat="server">
                <ContentTemplate>
                    <div id="divEditingPeopleInfo">
                        <asp:Label ID="lblEditingPeopleInfo" Text="لطفا موارد مورد نیاز ویرایش را ذکر بفرمایید:" runat="server" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtEditingPeopleInfo" runat="server" MaxLength="800" Width="400" Height="200" TextMode="MultiLine" Visible="False"></asp:TextBox>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="RdbEditingPeopleInfo" EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>
            <br />
        </div>
    </div>

    <div class="panel panel-warning" style="background-color: azure">
        <div class="row">
            <div class="col-md-3">
                <div class="row">
                    <div class="col-md-4 text-center">
                        <asp:Button ID="btn_ChangeInfo" runat="server" Text="ثبت تغییراطلاعات" OnClientClick="target='_self';" CssClass="btn btn-warning form-control" OnClick="btn_ChangeInfo_Click" ValidationGroup="vg" />
                    </div>
                    <div class="col-md-4 text-center">
                        <asp:Button ID="btn_TaeedParvande" runat="server" OnClick="btn_TaeedParvande_Click" OnClientClick="target='_self';" Text="ثبت وضعیت مدارک" CssClass="btn btn-info form-control" />
                    </div>
                </div>
            </div>
            <div class="col-md-2"></div>
            <div class="col-md-7 ">
                <div class="row">
                    <div class="col-md-6"></div>
                    <div class="col-md-2 text-center">
                        <asp:Button ID="btn_Taeed" Text="تأیید" runat="server" CssClass="btn btn-success form-control" OnClick="btn_Taeed_Click" OnClientClick="target='_self';" />
                    </div>

                    <div class="col-md-2 text-center">
                        <asp:Button ID="btn_Rad" Text="رد" runat="server" CssClass="btn btn-danger form-control" OnClick="btn_Rad_Click" OnClientClick="target='_self';" />
                    </div>
                    <div class="col-md-2 text-center">
                        <asp:Button ID="btnClose" Text="بستن" runat="server" CssClass="btn btn-primary form-control" OnClick="btnClose_Click" OnClientClick="target='_self';" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row" dir="rtl">
            <div class="col-md-12" dir="rtl">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="لطفا خطاهای زیر را رفع نمایید :" ForeColor="Red" ValidationGroup="vg" />
            </div>
        </div>
    </div>

    <telerik:RadWindowManager ID="rwd" runat="server">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close, Move" Height="200px" Modal="true" VisibleStatusbar="false" Width="300px">
                <ContentTemplate>
                    <%--<div class="rwDialogPopup radconfirm">
                        <div class="rwDialogText">
                            <asp:Literal ID="confirmMessage" runat="server" Text="" />
                        </div>
                        <div>
                            <telerik:RadButton ID="rbConfirm_OK1" runat="server" OnClick="rbConfirm_OK1_Click" Text="بله">
                            </telerik:RadButton>
                            <telerik:RadButton ID="rbConfirm_Cancel1" runat="server" OnClientClicked="closeCustomConfirm1" Text="خیر">
                            </telerik:RadButton>
                        </div>
                    </div>--%>
                </ContentTemplate>
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close, Move" Modal="true" VisibleStatusbar="false">
                <ContentTemplate>
                    <%--<div class="rwDialogPopup radconfirm">
                        <div class="rwDialogText">
                            <asp:Literal ID="MsgConf" runat="server" Text="" />
                        </div>
                        <div>
                            <telerik:RadButton ID="RadButton1" runat="server" OnClick="RadButton1_Click" Text="بله">
                            </telerik:RadButton>
                            <telerik:RadButton ID="RadButton2" OnClientClicked="closeradModal()" runat="server" Text="خیر">
                            </telerik:RadButton>
                        </div>
                    </div>--%>
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Label ID="lbl_Code" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="Lbl_Status" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_HeiatElmi" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Taeed" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Taeed2" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_BimeNumber" runat="server" Visible="false"></asp:Label>
</asp:Content>
