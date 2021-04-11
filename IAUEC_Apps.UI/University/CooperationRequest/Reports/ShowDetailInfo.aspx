<%@ Page Language="C#"  MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="ShowDetailInfo.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Reports.ShowDetailInfo1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                        <asp:TextBox ID="txtCodeMeli" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label2" Text="نام" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredName" ForeColor="Red" runat="server" ErrorMessage="نام خود را وارد نمایید" ControlToValidate="txtFirstName" ValidationGroup="info" Text="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[\u0600-\u06FF\s]+$" ControlToValidate="txtFirstName" ForeColor="Red" Text="*" ValidationGroup="info" ErrorMessage="لطفا فقط از حروف فارسی استفاده نمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label3" Text="نام خانوادگی" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFamily" ErrorMessage="نام خانوادگی خود را وارد نمایید" ForeColor="Red" ValidationGroup="info" Text="*" ControlToValidate="txtFamily" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[\u0600-\u06FF\s]+$" ControlToValidate="txtFamily" ForeColor="Red" Text="*" ValidationGroup="info" ErrorMessage="لطفا فقط از حروف فارسی استفاده نمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFamily" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label4" Text="نام پدر" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFather" ErrorMessage="نام پدر خود را وارد نمایید" ForeColor="Red" ValidationGroup="info" Text="*" ControlToValidate="txtFatherName" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^[\u0600-\u06FF\s]+$" ControlToValidate="txtFatherName" ForeColor="Red" Text="*" ValidationGroup="info" ErrorMessage="لطفا فقط از حروف فارسی استفاده نمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label5" Text="شماره شناسنامه" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="شماره شناسنامه خود را وارد نمایید" ForeColor="Red" ValidationGroup="info" Text="*" ControlToValidate="txtShCode" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^([\d]+)$" ControlToValidate="txtShCode" ForeColor="Red" Text="*" ValidationGroup="info" ErrorMessage="لطفا فقط از حروف فارسی استفاده نمایید"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtShCode" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="Label7" Text="سال تولد" runat="server" />
                                        <asp:RangeValidator ID="RangeValidator2" ErrorMessage="سال اخذ مدرک باید عددی بین 1300 تا 1400 باشد" ControlToValidate="txtYearGetMadrak" runat="server" ValidationGroup="info" Text="*" ForeColor="Red" MaximumValue="1400" MinimumValue="1300" Type="Integer" />
                                        <asp:RequiredFieldValidator ID="RequiredValidator11" runat="server" ControlToValidate="txtYearGetMadrak" Text="*" ForeColor="Red" ErrorMessage="لطفا سال اخذ آخرین مدرک خود را درج کنید" ValidationGroup="info"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtYearBorn" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label ID="Label6" Text="جنسیت" runat="server" />
                                        <asp:RadioButtonList ID="rdblGender" runat="server" CssClass="radio">
                                            <asp:ListItem Text="مرد" Value="1" />
                                            <asp:ListItem Text="زن" Value="2" />
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label8" Text="محل تولد" runat="server" />
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
                                                    <asp:ListItem Text="انتخاب کنید" />
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
                            </div>
                            <div class="col-md-4">
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label14" Text="سال اخذ مدرک" runat="server" />
                                        <asp:RangeValidator ID="RangeValidator1" ErrorMessage="سال اخذ مدرک باید عددی بین 1300 تا 1400 باشد" ControlToValidate="txtYearGetMadrak" runat="server" ValidationGroup="info" Text="*" ForeColor="Red" MaximumValue="1400" MinimumValue="1300" Type="Integer" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtYearGetMadrak" Text="*" ForeColor="Red" ErrorMessage="لطفا سال اخذ آخرین مدرک خود را درج کنید" ValidationGroup="info"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" ID="txtYearGetMadrak" CssClass="form-control" Width="60px" />
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Label ID="Label15" Text="سنوات تدریس" runat="server" />
                                        <asp:RangeValidator ID="RangeValidator3" ErrorMessage="سنوات تدریس باید بین 0 تا 99 سال باشد" ControlToValidate="txtSanavat" Type="Integer" MinimumValue="0" MaximumValue="99" Text="*" ValidationGroup="info" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtSanavat" ForeColor="Red" Text="*" ValidationGroup="info" runat="server" ErrorMessage="لطفا سنوات تدریس خود را درج کنید"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSanavat" runat="server" CssClass="form-control" Width="60px" />
                                        <asp:Label ID="lbl_Sal" runat="server" Text="سال"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <asp:Label ID="Label16" Text="کشور محل اخذ مدرک" runat="server" />
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
                                    <div class="col-md-7" style="margin-top: 50px">
                                        <%--<img src="Image/Untitled.png" />--%>
                                        <asp:Button ID="btn_ShowPDF" runat="server" OnClick="btn_ShowPDF_Click" Text="دانلود رزومه" CssClass="btn btn-info" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label18" Text="دانشکده مورد نظر جهت تدریس" runat="server" />
                                <asp:CheckBoxList ID="chbkDaneshkade" runat="server" CssClass="checkbox" RepeatColumns="1" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text="دانشکده فنی و مهندسی" Value="2" />
                                            <asp:ListItem Text="دانشکده علوم انسانی" Value="1" />
                                            <asp:ListItem Text="دانشکده مدیریت" Value="3" />
                                            <asp:ListItem Text="علوم پايه و فناوري هاي نوين" Value="8" />
                                </asp:CheckBoxList>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Label42" Text="گروههای آموزشی تدریس" runat="server" />
                                <asp:CheckBoxList ID="chbkGroup" runat="server" RepeatColumns="4" CssClass="checkbox" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label30" Text="درخواست همکاری در" runat="server" />
                                <asp:CheckBoxList ID="chk_Cooperation" runat="server" CssClass="checkbox" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text="آموزش" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="پژوهش" Value="2"></asp:ListItem>
                                </asp:CheckBoxList>

                            </div>
                            
                                    <div class="col-md-3">
                                        <asp:Label Text="وضعیت بیمه" runat="server" />
                                        <asp:RadioButtonList ID="rdblBimehStatus" RepeatDirection="Horizontal" CssClass="radio" runat="server">
                                            <asp:ListItem Text="دارای سابقه بیمه" Value="1" />
                                            <asp:ListItem Text="بدون سابقه بیمه" Value="2" Selected="True" />
                                        </asp:RadioButtonList>
                                        <asp:Label Text="نوع بیمه" runat="server" />
                                        <asp:DropDownList ID="drpBimehType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="انتخاب کنید" Value="0" />
                                            <asp:ListItem Text="تامین اجتماعی" Value="1" />
                                            <asp:ListItem Text="خدمات درمانی" Value="2" />
                                            <asp:ListItem Text="نیروهای مسلح" Value="3" />
                                            <asp:ListItem Text="بازنشسته" Value="4" />
                                        </asp:DropDownList>
                                        <asp:Label ID="Label32" Text="شماره بیمه" runat="server" />
                                        <asp:TextBox ID="txtInsuranceNumber" runat="server" CssClass="form-control" EnableViewState="True" MaxLength="10" />
                                    </div>
                        </div>
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
                                                    <asp:DropDownList ID="drpMartabe" runat="server" CssClass="form-control" Height="40px">
                                                        <asp:ListItem Text="انتخاب کنید" Selected="True" />
                                                        <asp:ListItem Text="مربی" Value="1" />
                                                        <asp:ListItem Text="دانشیار" Value="2" />
                                                        <asp:ListItem Text="استادیار" Value="3" />
                                                        <asp:ListItem Text="استاد" Value="4" />
                                                        <%--<asp:ListItem Text="فاقد مرتبه علمی" Value="8" />--%>
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
                                            <asp:RangeValidator ID="RangeValidator4" ErrorMessage="مقدار پایه باید عددی بین 0 و 50 باشد" Type="Integer" MinimumValue="0" MaximumValue="50" ControlToValidate="txtPaye" ForeColor="Red" Text="*" ValidationGroup="info" runat="server" />
                                            <asp:TextBox ID="txtPaye" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-10">
                                            <asp:Label ID="Label21" Text="نوع استخدام" runat="server" />
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpHireType" runat="server" CssClass="form-control" Height="40px">
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
                                        <div class="col-md-10">
                                            <asp:Label ID="Label22" Text="دانشگاه محل خدمت" runat="server" />
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
                                            <asp:TextBox ID="txtDateSodoorHokm" runat="server" CssClass="form-control form-inline" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label24" Text="تاریخ اجرای حکم" runat="server" />
                                            <asp:TextBox ID="txtDateEjraHokm" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:Label ID="Label25" Text="شماره حکم" runat="server" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="لطفا شماره حکم خود را وارد کنید" ControlToValidate="txtHokmNumber" ForeColor="Red" Text="*" ValidationGroup="info" runat="server" />
                                            <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:CheckBox ID="chk_IsRetired" runat="server" Text="بازنشسته" CssClass="checkbox" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="col-md-10">
                                        <asp:Label ID="Label28" Text="نحوه همکاری در دانشگاه مبدا" runat="server" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <br />

                <div id="dvContactInfo" class=" panel panel-info">
                    <div class="panel-heading">اطلاعات تماس</div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3 col-md-offset-1">
                                <div class="row">
                                    <div class="col-md-7 col-md-offset-1">
                                        <asp:Label ID="Label29" Text="تلفن منزل" runat="server" />
                                        <asp:TextBox ID="txtHomePhone" runat="server" CssClass="form-control form-inline" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7 col-md-offset-1">
                                        <asp:Label ID="Label31" Text="تلفن محل کار" runat="server" />
                                        <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-control form-inline" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label33" Text="تلفن همراه" runat="server" />
                                        <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label34" Text="پست الکترونیک" runat="server" />
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="row">
                                    <div class="col-md-10">
                                        <h4>آدرس محل سکونت</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3 col-md-offset-1">
                                        <asp:Label ID="Label35" Text="استان" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="drpProvince1" runat="server" CssClass=" form-control" AutoPostBack="true" Height="40px" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="drpProvince1" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="Label36" Text="شهر" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
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
                                        <asp:TextBox ID="txtLivingZipCode" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <asp:Label ID="Label37" Text="آدرس" runat="server" />
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
                                                <asp:DropDownList ID="drpProvince2" runat="server" CssClass=" form-control" AutoPostBack="true" />
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
                                <div class="row">
                                    <div class="col-md-10">
                                        <asp:Label ID="Label27" Text="شماره حساب سیبا" runat="server" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ErrorMessage="شماره حساب باید 13 رقم باشد" ControlToValidate="txtSiba" ValidationExpression="^[0-9]{13}$" runat="server" Text="*" ForeColor="Red" ValidationGroup="info" />
                                        <asp:TextBox ID="txtSiba" runat="server" CssClass="form-control" Width="110px" />
                                    </div>
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
                                                <asp:Button ID="btn_ShowPicture" runat="server" Visible="false" Text="بزرگنمایی"  OnClientClick="target='_blank';" CommandArgument='<%#Eval("doc_type")%>' CommandName="ShowPic" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                    <ContentTemplate>
                                                        <asp:RadioButton ID="rdb_Taeed" Text="تأیید مدرک" runat="server" ValidationGroup="0" GroupName="gh" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="rdb_Taeed" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:RadioButton ID="rdb_Naghs" Text="نقص مدرک" runat="server" ValidationGroup="1" GroupName="gh" AutoPostBack="true" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="rdb_Naghs" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="lbl_Sharh" Text="علت رد:" runat="server"></asp:Label>
                                                <asp:TextBox ID="txt_Sharh" runat="server" MaxLength="300" Width="200" Height="100" TextMode="MultiLine"></asp:TextBox>
                                            </div>
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

    <div class="panel panel-warning" style="background-color: azure">
        <div class="row">
            <div class="col-md-3">
            </div>
            <div class="col-md-9">
                <div class="col-md-4">
                </div>
                <div class="col-md-2">
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-1">
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnClose"  OnClientClick="target='_self';" Text="بستن" runat="server" CssClass="btn btn-primary" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
    </div>
    <telerik:RadWindowManager ID="rwd" runat="server"></telerik:RadWindowManager>
    <asp:Label ID="lbl_Code" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="Lbl_Status" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_HeiatElmi" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Taeed" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Taeed2" runat="server" Visible="false"></asp:Label>
</asp:Content>