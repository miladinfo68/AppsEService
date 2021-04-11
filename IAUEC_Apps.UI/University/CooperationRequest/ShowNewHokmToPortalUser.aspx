<%@ Page Language="C#" Title="مشاهده حکم های جدید" AutoEventWireup="true" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" CodeBehind="ShowNewHokmToPortalUser.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ShowNewHokmToPortalUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">

    <script src="Scripts/js-persian-cal.min.js"></script>
    <link href="css/js-persian-cal.css" rel="stylesheet" />
    <style>
        .currencyWrapper {
            position: relative;
        }

            .currencyWrapper > .form-control {
                padding-left: 35px;
            }

            .currencyWrapper > span {
                display: block;
                position: absolute;
                left: 0;
                top: 0;
                bottom: 0;
                margin: auto;
                height: 50%;
                width: 30px;
                color: #999;
            }

        .pcalWrapper {
            position: relative;
        }

            .pcalWrapper > a.pcalBtn {
                position: absolute;
                left: 7px;
                top: 0;
                bottom: 0;
                margin: auto;
            }

            .pcalWrapper > input {
                padding-left: 30px;
            }

        .pcalBtn.disabled {
            pointer-events: none;
            cursor: default;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="page-header center heading" style="direction: rtl">
            <h2>مشاهده حکم اساتید</h2>
        </div>
        <div style="direction: rtl">
            <div class="content">
                <div class="container">
                    <div class="col-md-12">
                        <div class="panel panel-success">

                            <div class="panel-heading">
                                <h4 class="text-info">جستجوی پیشرفته</h4>
                            </div>

                            <div class="panel-body">

                                <div class="container">
                                    <div class="row">

                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <span style="font-weight:lighter">تاریخ درج مدرک</span>
                                            </div>
                                            <div class="col-md-4">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-1">
                                                            <span>از  </span>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <div class="pcalWrapper">

                                                                <asp:TextBox ID="fromDate" runat="server" CssClass="form-control form-inline pcal" MaxLength="10" ToolTip="از تاریخ" />

                                                                <asp:HiddenField runat="server" ID="hdnFromDate" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-1">
                                                            <span>تا  </span>
                                                        </div>
                                                        <div class="col-md-10">
                                                            <div class="pcalWrapper">
                                                                <asp:TextBox ID="toDate" runat="server" CssClass="form-control form-inline pcal" MaxLength="10" ToolTip="تا تاریخ" />
                                                            </div>
                                                            <asp:HiddenField runat="server" ID="hdnToDate" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <span>مرتبه علمی</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="drpDegree" runat="server" Width="100%">
                                                    <asp:ListItem Text="انتخاب کنید" Value="-1" Selected="True" />
                                                    <asp:ListItem Text="مربی" Value="1" />
                                                    <asp:ListItem Text="دانشیار" Value="2" />
                                                    <asp:ListItem Text="استادیار" Value="3" />
                                                    <asp:ListItem Text="استاد" Value="4" />
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <span>نام خانوادگی</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtFamily" runat="server" ToolTip="نام خانوادگی استاد" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="col-md-4">
                                                <span>کد ملی</span>
                                            </div>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtNationalCode" runat="server" ToolTip="کد ملی استاد" MaxLength="10" Width="100%"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnSearch" ValidationGroup="vg" runat="server" CssClass="btn btn-success center" Text="جستجو" OnClick="btnSearch_Click" />

                                        </div>

                                    </div>
                                    
                                </div>

                                <div class="row">
                                    <div class="col-sm-4 col-sm-pull-5">
                                        <asp:ValidationSummary runat="server" DisplayMode="BulletList" ForeColor="Red" ValidationGroup="vg" />
                                    </div>
                                </div>
                            </div>
                            <asp:RegularExpressionValidator ValidationGroup="vg" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="fromDate" runat="server" />
                            <asp:RegularExpressionValidator ValidationGroup="vg" ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Text="*" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="toDate" runat="server" />
                            <asp:RegularExpressionValidator ValidationGroup="vg" ControlToValidate="txtFamily" runat="server" Text="*" ForeColor="Red" ErrorMessage="نام خانوادگی فقط میتواند شامل حروف فارسی شود" ValidationExpression="^[\u0600-\u06FF\s]+$"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ValidationGroup="vg" ControlToValidate="txtNationalCode" runat="server" Text="*" ForeColor="Red" ErrorMessage="لطفا برای کد ملی فقط عدد وارد فرمایید" ValidationExpression="[\d+]{10}"></asp:RegularExpressionValidator>


                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="panel">
                            <asp:GridView EmptyDataText="درخواستی پیدا نشد" ID="grdHokm" runat="server" AutoGenerateColumns="false" 
                                OnRowCommand="grdHokm_RowCommand" PageSize="50" AllowPaging="true" CssClass="table table-bordered table-striped table-condensed" 
                                OnPageIndexChanging="grdHokm_PageIndexChanging" OnDataBinding="grdHokm_DataBinding">
                                <HeaderStyle CssClass="bg-green" />
                                <Columns>
                                    <asp:BoundField HeaderText="ردیف" DataField="row" />
                                    <asp:BoundField HeaderText="کد استاد" DataField="code_ostad" />
                                    <asp:BoundField HeaderText="نام و نام خانوادگی" DataField="name" />
                                    <asp:BoundField HeaderText="کد ملی" DataField="nationalCode" />
                                    <asp:BoundField HeaderText="کد حکم" DataField="hokmId" />
                                    <asp:BoundField HeaderText="تاریخ بارگذاری حکم" DataField="date" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnOldHokm" CssClass="btn-round btn-warning" runat="server" Text="مشاهده تمام احکام استاد" CommandName="OldHokm" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"InfoPeopleId") %>' />
                                            <asp:Button ID="btnShow" CssClass="btn-round btn-success" runat="server" Text="مشاهده آخرین حکم کارگزینی" CommandName="showHokm" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"hokmId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <telerik:RadWindow ID="rwShowLastHokm" runat="server" Width="900" Height="500" CenterIfModal="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="width: 100%">

                        <div id="dvNewHokm" visible="false" runat="server" class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <label id="lblHokmId" runat="server"></label>
                                    <asp:HiddenField ID="hdnReqId" runat="server" />
                                </div>
                                <div class="panel-body" runat="server" id="dvHokmBody">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">کد استاد:</p>
                                            <asp:TextBox ID="txtCodeOstad" CssClass="form-control" runat="server" ReadOnly="true" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">شماره حکم:</p>
                                            <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" ReadOnly="true" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">تاریخ صدور حکم :</p>
                                            <asp:TextBox ID="txtDateSodoorHokm" runat="server" CssClass="form-control form-inline" ReadOnly="true" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">تاریخ اجرای حکم در دانشگاه مبدأ</p>
                                            <asp:TextBox ID="txtDateEjraHokm" runat="server" CssClass="form-control form-inline" ReadOnly="true" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">مرتبه دانشگاهی</p>
                                            <asp:DropDownList ID="drpMartabe" runat="server" CssClass="form-control" ReadOnly="true">
                                                <asp:ListItem Text="انتخاب کنید" Selected="True" />
                                                <asp:ListItem Text="دانشیار" Value="2" />
                                                <asp:ListItem Text="استادیار" Value="3" />
                                                <asp:ListItem Text="استاد" Value="4" />
                                                <asp:ListItem Text="مربی" Value="1" />
                                                <asp:ListItem Text="فاقد مرتبه علمی" Value="8" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">پایه :</p>
                                            <asp:TextBox ID="txtPaye" runat="server" CssClass="form-control" ReadOnly="true" />
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">نوع استخدام در دانشگاه مبدأ :</p>
                                            <asp:DropDownList ID="drpHireType" runat="server" CssClass="form-control" ReadOnly="true">
                                                <asp:ListItem Text="انتخاب نشده" Value="0" Selected="True" />
                                                <asp:ListItem Text="رسمی" Value="1" />
                                                <asp:ListItem Text="آزمایشی" Value="2" />
                                                <asp:ListItem Text="قراردادی" Value="3" />
                                                <asp:ListItem Text="مامور به خدمت" Value="4" />
                                                <asp:ListItem Text="موقت" Value="5" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">دانشگاه محل خدمت</p>
                                            <div>
                                                <asp:DropDownList ID="drpPastUni" runat="server" CssClass="form-control" ReadOnly="true"></asp:DropDownList>
                                                <%--<telerik:RadComboBox Enabled="false" ID="drpPastUni" runat="server" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Height="300px"></telerik:RadComboBox>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <p class="lblInput">نحوه همکاری در دانشگاه مبدأ :</p>
                                            <asp:RadioButtonList ID="rdblHireType" runat="server" CssClass="radio" RepeatDirection="Horizontal" ReadOnly="true" Enabled="false" RepeatColumns="3">
                                                <asp:ListItem Text="تمام وقت 32 ساعت" Value="1" />
                                                <asp:ListItem Text="نیمه وقت" Value="2" />
                                                <asp:ListItem Text="ساعتی" Value="3" />
                                                <asp:ListItem Text="تمام وقت طرح مشمولان" Value="4" />
                                                <asp:ListItem Text="بورسیه دکتری" Value="5" />
                                                <asp:ListItem Text="کارمند" Value="6" />
                                                <asp:ListItem Text="تمام وقت 44 ساعت" Value="7" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-4">
                                            <p class="lblInput">مبلغ حکم در دانشگاه مبدا :</p>
                                            <div class="row">
                                                <asp:TextBox ID="txtMablaghHokm" runat="server" CssClass="form-inline" Width="90%" ReadOnly="true" />
                                                <b style="width: 10%">ریال</b>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:CheckBox ID="chkBoundHour" runat="server" Text="متقاضی تکمیل ساعت موظفی" ReadOnly="true" />
                                        </div>
                                        <div class="col-sm-4">
                                            <a class="btn btn-warning" href="OpenScanImage.aspx" target="_blank" id="imgNewHokm" runat="server">نمایش فایل اسکن شده</a>
                                        </div>
                                    </div>
                                    <div class="row well" runat="server" id="dvSeen">
                                        <div class="form-inline" role="form" dir="rtl">
                                            <asp:CheckBox CssClass="danger " ForeColor="Red" ID="cbxSeen" runat="server" Checked="true" Text="در پرتال اعمال شد" />
                                            <asp:Button ID="btnSeen" Text="تایید" CssClass="btn btn-success" runat="server" OnClick="btnSeen_Click" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>

        </div>
        <div>
            <telerik:RadWindow ID="rwOldHokmList" runat="server" Width="900" Height="500" CenterIfModal="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="width: 100%">
                        <asp:GridView EmptyDataText="حکمی وجود ندارد" ID="grdOldHokm" runat="server" AutoGenerateColumns="false" OnRowCommand="grdOldHokm_RowCommand" CssClass="table table-bordered table-striped table-condensed">
                            <HeaderStyle CssClass="bg-warning" />
                            <Columns>
                                <asp:BoundField HeaderText="کد حکم" DataField="hokmid" />
                                <asp:BoundField HeaderText="تاریخ بارگزاری" DataField="dateUpload" />
                                <asp:BoundField HeaderText="تاریخ اجرای حکم" DataField="date_runhokm" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnShowOldHokm" CssClass="btn btn-warning" Text="نمایش" CommandName="showOldHokm" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"hokmId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </telerik:RadWindow>
        </div>
    </div>
    <script type="text/javascript">
        //document.getElementById("<%=dvNewHokm.ClientID %>").disabled = true;
        debugger;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_pageLoaded(function (sender, e) {
                SetDatePicker();
            })
        };

        function SetDatePicker() {
            debugger;
            var idArray = [];
            $('.pcal').each(function () {
                idArray.push(this.id);
            });

            if (idArray.length > 0) {
                for (var i = 0; i < idArray.length; i++) {
                    if (idArray[i] != 0) {
                        var x = new AMIB.persianCalendar(idArray[i],
                            { extraInputID: idArray[i], extraInputFormat: 'yyyy/mm/dd' });
                    }
                }
            }
        }

    </script>
</asp:Content>
