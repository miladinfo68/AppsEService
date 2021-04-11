<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/TeacherMaster.Master" AutoEventWireup="true" CodeBehind="EditMain.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Teachers.EditMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .lblInput {
            padding-top: 5px !important;
        }

        .rwContentRow th {
            font-family: 'B Titr' !important;
            font-weight: bold;
        }

        .rwContentRow {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
        }
.center-block.custom{margin-left: auto;
    margin-right: auto;
    float: none;}
    </style>
    <script type="text/javascript">
        function RedirectToMain() {
            window.location = "../../../CommonUI/TeacherIntro.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2>مدیریت اطلاعات فردی</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" dir="rtl" style="overflow-x: scroll; padding: 15px;">
        <div class="row">
            <div id="dvEditPersonalInfo" runat="server" class="col-lg-3 col-md-6">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-info-circle fa-4x"></i>
                            </div>
                            <div class="col-xs-8 text-right ">
                                <div class="huge">
                                    <br />
                                </div>
                                <div>ویرایش اطلاعات فردی</div>

                            </div>
                        </div>
                    </div>
                    <a id="EditpersonalInfo" href="EditPersonalInfo.aspx" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>

            <div id="dvEditContact" runat="server" class="col-lg-3 col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-group fa-4x"></i>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">
                                    <br />
                                </div>
                                <div>ویرایش اطلاعات تماس</div>

                            </div>
                        </div>
                    </div>
                    <a id="editCooperationInfo" href="EditContactInfo.aspx" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>

            <div id="dvUpdateHokm" runat="server" class="col-lg-3 col-md-6">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-university fa-4x"></i>
                                <%--<i class="fa fa-graduation-cap fa-4x"></i>--%>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">
                                    <br />
                                </div>
                                <p>&nbsp;بارگذاری آخرین حکم کارگزینی</p>
                            </div>
                        </div>
                    </div>
                    <a id="UpdateHokm" href="EditWorkInfo.aspx" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>

            <div id="dvUpdateCooperation" runat="server" class="col-lg-3 col-md-6">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-briefcase fa-4x"></i>
                            </div>
                            <div class="col-xs-8 text-right">
                                <div class="huge">
                                    <br />
                                </div>
                                <div>ویرایش اطلاعات نحوه همکاری</div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <a id="A1" href="EditCooperationInfo.aspx" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>



        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4>ثبت اطلاعات</h4>
                    </div>
                    <div class="panel-body">
                        <p><span class="text-danger">استاد گرامی :</span>توجه داشته باشید که اطلاعات جدید شما بعد از تایید مسئول کارگزینی به روز رسانی خواهد شد.</p>
                        <p>وضعیت ویرایش</p>
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <script type="text/javascript">
                                    function refreshGrid() {
                                        document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
                                    }

                                    function denyAspButton(button) {
                                        function aspButtonCallbackFn(arg) {
                                            if (arg) {
                                                __doPostBack(button.name, "");
                                            }
                                        }
                                        radconfirm("آیا مطمئن هستید که می خواهید این درخواست را لغو کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
                                    }
                                </script>
                                <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="refreshGrid" runat="server" />
                                <div class="table-responsive">
                                    <asp:GridView ID="grdEditRequests" CssClass="table table-bordered table-condensed table-striped"
                                        OnRowDataBound="grdEditRequests_RowDataBound" OnRowCommand="grdEditRequests_RowCommand"
                                        AutoGenerateColumns="false" ShowHeader="true" EmptyDataText="درخواستی پیدا نشد."
                                        ShowHeaderWhenEmpty="true" runat="server">
                                        <HeaderStyle CssClass="bg-green" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ردیف">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%#Container.DisplayIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="تاریخ ثبت ویرایش" DataField="CreateDate" />
                                            <asp:BoundField HeaderText="نوع درخواست" DataField="RequestTypeId" />
                                            <asp:BoundField HeaderText="شماره درخواست" DataField="ProfessorRequestID" />
                                            <asp:BoundField HeaderText="وضعیت" DataField="RequestLogID" />
                                            <asp:BoundField HeaderText="پیام سیستم" DataField="ProfessorMessage" />
                                            <asp:TemplateField HeaderText="تغییرات">
                                                <ItemTemplate>
                                                    <asp:Button Text="نمایش" CssClass="btn btn-info btn-sm" CommandName="showchanges" CommandArgument='<%#Eval("ProfessorRequestID") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عملیات">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnCancel" UseSubmitBehavior="false" Text="لغو درخواست" CommandName="RemoveReq" CommandArgument='<%#Eval("ProfessorRequestID") %>' OnClientClick="return denyAspButton(this);" runat="server" CssClass="btn btn-sm btn-danger" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnfURL" runat="server" Value='<%#Eval("ScanImageUrl") %>' />
                                                    <asp:HiddenField ID="hdnfReqType" runat="server" Value='<%#Eval("RequestTypeId") %>' />
                                                    <asp:HiddenField ID="hdnfReqLog" runat="server" Value='<%#Eval("RequestLogId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <telerik:RadWindow ID="RadWindow1" runat="server" Width="900" Height="650" AutoSizeBehaviors="Height">

        <ContentTemplate>
            <div class="container" dir="rtl">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlViewEdits" Enabled="false">
                            <div class="row">
                                <div class="col-md-12">

                                    <div id="dvPersonalInfo" visible="true" runat="server" class="container">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div id="dvIdd" class="panel panel-primary" visible="true" runat="server">
                                                    <div class="panel-heading">
                                                        <span class="txtHeader">تغییرات اطلاعات شناسنامه ای</span>
                                                        <a class="btn btn-info left" visible="false" href="~/University/CooperationRequest/OpenScanImage.aspx" target="_blank" id="btnScanPersonelly" runat="server">اسکن عکس پرسنلی</a>
                                                        <a class="btn btn-info left" visible="false" href="~/University/CooperationRequest/OpenScanImage.aspx" target="_blank" id="btnScanMeli" runat="server">اسکن کارت ملی</a>
                                                        <a class="btn btn-info left" visible="false" href="~/University/CooperationRequest/OpenScanImage.aspx" target="_blank" id="btnScanIdd" runat="server">اسکن شناسنامه</a>
                                                    </div>
                                                    <asp:GridView ID="grdIDD" runat="server"
                                                        CssClass="table table-bordered table-striped table-condensed"
                                                        OnRowDataBound="grdIDD_RowDataBound"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="Id">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                                            <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="مقدار جدید">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                                                    <asp:CheckBoxList ID="chkDepNew_View" runat="server" CssClass="checkbox" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div id="dvMadrak" class="panel panel-primary" visible="true" runat="server">
                                                    <div class="panel-heading">
                                                        <span class="txtHeader">تغییرات اطلاعات مدرک تحصیلی</span>
                                                        <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanArzesh" runat="server">اسکن ارزشنامه تحصیلی</a>
                                                        <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanMadrak" runat="server">اسکن مدرک تحصیلی</a>

                                                    </div>
                                                    <asp:GridView ID="grdMadrak" runat="server"
                                                        CssClass="table table-bordered table-striped table-condensed"
                                                        OnRowDataBound="grdMadrak_RowDataBound"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="Id">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                                            <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="مقدار جدید">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <%--<asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                                        ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                                        ControlToValidate="txtNewValue"
                                                                        ForeColor="Red"
                                                                        Display="Dynamic"
                                                                        runat="server" />
                                                                    <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                                        ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                                        ControlToValidate="txtNewValue"
                                                                        ForeColor="Red" Display="Dynamic"
                                                                        Enabled="false"
                                                                        Text="فرمت تاریخ وارد شده اشتباه است."></asp:RegularExpressionValidator>--%>
                                                                    <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div id="dvBime" class="panel panel-primary" visible="true" runat="server">
                                                    <div class="panel-heading">
                                                        <span class="txtHeader">تغییرات اطلاعات بیمه و بازنشستگی</span>
                                                        <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanBazneshaste" runat="server">اسکن حکم بازنشستگی</a>
                                                        <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanBime" runat="server">اسکن بیمه</a>

                                                    </div>
                                                    <asp:GridView ID="grdBime" runat="server"
                                                        CssClass="table table-bordered table-striped table-condensed"
                                                        OnRowDataBound="grdBime_RowDataBound"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="Id">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                                            <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="مقدار جدید">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <%--<asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                                        ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                                        ControlToValidate="txtNewValue"
                                                                        ForeColor="Red"
                                                                        Display="Dynamic"
                                                                        runat="server" />
                                                                    <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                                        ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                                        ControlToValidate="txtNewValue"
                                                                        ForeColor="Red" Display="Dynamic"
                                                                        Enabled="false"
                                                                        Text="فرمت تاریخ وارد شده اشتباه است."></asp:RegularExpressionValidator>--%>
                                                                    <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div id="dvInf" class="panel panel-primary" visible="true" runat="server">
                                                    <div class="panel-heading">
                                                        <span class="txtHeader">تغییرات اطلاعات تکمیلی</span>
                                                        <a class="btn btn-info left" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnScanInf" runat="server">اسکن نظام وظیفه</a>
                                                    </div>
                                                    <asp:GridView ID="grdInf" runat="server"
                                                        CssClass="table table-bordered table-striped table-condensed"
                                                        OnRowDataBound="grdInf_RowDataBound"
                                                        AutoGenerateColumns="false"
                                                        DataKeyNames="Id">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                                            <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="مقدار جدید">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                                    <%--<asp:RequiredFieldValidator ID="vldDateRequired" Enabled="false"
                                                                        ErrorMessage="درج مقدار جدید الزامی می باشد."
                                                                        ControlToValidate="txtNewValue"
                                                                        ForeColor="Red"
                                                                        Display="Dynamic"
                                                                        runat="server" />
                                                                    <asp:RegularExpressionValidator ID="vldDateTime" runat="server"
                                                                        ValidationExpression="\d{4}(?:/\d{1,2}){2}"
                                                                        ControlToValidate="txtNewValue"
                                                                        ForeColor="Red" Display="Dynamic"
                                                                        Enabled="false"
                                                                        Text="فرمت تاریخ وارد شده اشتباه است."></asp:RegularExpressionValidator>--%>
                                                                    <asp:DropDownList ID="drpNewValue" Visible="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="container" id="dvChangeList" runat="server" visible="false">
                                        <asp:GridView ID="grdChangeList" runat="server"
                                            CssClass="table table-bordered table-striped table-condensed"
                                            OnRowDataBound="grdChangeList_RowDataBound"
                                            AutoGenerateColumns="false"
                                            DataKeyNames="Id">
                                            <HeaderStyle CssClass="bg-blue-sky" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ردیف">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="مشخصه" DataField="Description" ReadOnly="true" />
                                                <asp:BoundField HeaderText="مقدار قبلی" DataField="OldValue" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="مقدار جدید">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNewValue" Text='<%#Eval("NewValue") %>' runat="server" />
                                                        <asp:DropDownList ID="drpNewValue" CssClass="form-control" Visible="false" runat="server"></asp:DropDownList>
                                                        <div id="dvCheckBox" runat="server">
                                                            <asp:CheckBoxList ID="chkDepNew_View" runat="server" CssClass="checkbox" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList>

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnfCodingTypeId" runat="server" Value='<%#Eval("CodingId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <a class="btn btn-success btn-lg" visible="false" href="OpenScanImage.aspx" target="_blank" id="btnShowImage" runat="server">نمایش فایل اسکن شده</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="dvNewHokm" visible="false" runat="server">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <h4>اطلاعات حکم کارگزینی جدید</h4>
                                    </div>

                                    <div class="panel-body">
                                        <asp:HiddenField ID="hdnfHokmId" runat="server" />
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <p class="lblInput">آیا دارای سابقه هیات علمی می باشید ؟</p>
                                                <asp:RadioButtonList runat="server" ID="rblIsHeiat" AutoPostBack="true" RepeatDirection="Horizontal"
                                                    CssClass="radio isHeiat">
                                                    <asp:ListItem Text="بله" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="خیر" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div runat="server" id="DivInfoHokm">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <p class="lblInput">کد استاد:</p>
                                                    <asp:TextBox ID="txtCodeOstad" CssClass="form-control" runat="server" ReadOnly="true" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">شماره حکم:</p>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" ErrorMessage="لطفا شماره حکم خود را وارد کنید" ControlToValidate="txtHokmNumber" ForeColor="Red" Display="Dynamic" runat="server" />--%>
                                                    <asp:TextBox ID="txtHokmNumber" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">مرتبه دانشگاهی</p>
                                                    <%--<asp:RequiredFieldValidator ErrorMessage="لطفا مرتبه علمی خود را انتخاب کنید" InitialValue="انتخاب کنید" ForeColor="Red" Display="Dynamic" ControlToValidate="drpMartabe" runat="server" />--%>
                                                    <asp:DropDownList ID="drpMartabe" runat="server" CssClass="form-control">
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
                                                    <p class="lblInput">تاریخ صدور حکم :</p>
                                                    <%--                                                <asp:RequiredFieldValidator ErrorMessage="درج تاریخ صدور حکم الزامی می باشد" ControlToValidate="txtDateSodoorHokm" ForeColor="Red" Display="Dynamic" runat="server" />
                                                <asp:RegularExpressionValidator ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateSodoorHokm" runat="server" />--%>
                                                    <asp:TextBox ID="txtDateSodoorHokm" runat="server" CssClass="form-control form-inline pcal" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">تاریخ اجرای حکم در دانشگاه مبدأ</p>
                                                    <%--                                                <asp:RequiredFieldValidator ErrorMessage="درج تاریخ اجرای حکم الزامی می باشد" ControlToValidate="txtDateEjraHokm" ForeColor="Red" Display="Dynamic" runat="server" />
                                                <asp:RegularExpressionValidator ErrorMessage="فرمت تاریخ ورودی اشتباه است ، لطفا از تقویم موجود برای درج تاریخ استفاده کنید " ForeColor="Red" Display="Dynamic" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="txtDateEjraHokm" runat="server" />--%>
                                                    <asp:TextBox ID="txtDateEjraHokm" runat="server" CssClass="form-control form-inline pcal" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">دانشگاه محل خدمت</p>
                                                    <%--<asp:RequiredFieldValidator ID="valUniName" Display="Dynamic" ErrorMessage="لطفا نام دانشگاه محل تحصیل خود را انتخاب کنید" ControlToValidate="drpPastUni" runat="server" InitialValue="جستجو و انتخاب کنید" ForeColor="Red" />--%>
                                                    <div>
                                                        <telerik:RadComboBox ID="drpPastUni" runat="server" MarkFirstMatch="True" Filter="Contains" HighlightTemplatedItems="True" RenderMode="Lightweight" Width="100%" AllowCustomText="false" ExpandDirection="Down" Height="300px"></telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <p class="lblInput">پایه :</p>
                                                    <%--                                                <asp:RequiredFieldValidator ErrorMessage="لطفا پایه خود را درج کنید" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPaye" runat="server" />
                                                <asp:RangeValidator ID="RangeValidator4" ErrorMessage="مقدار پایه باید عددی بین 0 و 50 باشد" Type="Integer" MinimumValue="0" MaximumValue="50" ControlToValidate="txtPaye" Display="Dynamic" ForeColor="Red" runat="server" />--%>
                                                    <asp:TextBox ID="txtPaye" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">نوع استخدام در دانشگاه مبدأ :</p>
                                                    <%--<asp:RequiredFieldValidator ErrorMessage="لطفا نوع استخدام خود را انتخاب کنید" InitialValue="انتخاب کنید" ForeColor="Red" Display="Dynamic" ControlToValidate="drpHireType" runat="server" />--%>
                                                    <asp:DropDownList ID="drpHireType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="انتخاب کنید" />
                                                        <asp:ListItem Text="رسمی" Value="1" />
                                                        <asp:ListItem Text="آزمایشی" Value="2" />
                                                        <asp:ListItem Text="قراردادی" Value="3" />
                                                        <asp:ListItem Text="مامور به خدمت" Value="4" />
                                                        <asp:ListItem Text="موقت" Value="5" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">نوع دانشگاه محل خدمت</p>
                                                    <asp:DropDownList ID="ddlPastUniType" runat="server" CssClass="form-control" Height="40px">
                                                        <asp:ListItem Text="انتخاب کنید" Value="0" />
                                                        <asp:ListItem Text="دولتی" Value="1" />
                                                        <asp:ListItem Text="آزاد" Value="2" />
                                                        <asp:ListItem Text="حوزه" Value="3" />
                                                        <asp:ListItem Text="خارج از کشور" Value="4" />
                                                        <asp:ListItem Text="سایر" Value="5" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <p class="lblInput">نحوه همکاری در دانشگاه مبدأ :</p>
                                                    <%--<asp:RequiredFieldValidator ErrorMessage="لطفا نحوه همکاری خود در دانشگاه مبدا را مشخص کنید" ControlToValidate="rdblHireType" runat="server" ForeColor="Red" Display="Dynamic" />--%>
                                                    <asp:RadioButtonList ID="rdblHireType" runat="server" CssClass="radio" RepeatDirection="Horizontal" RepeatColumns="3">
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
                                                    <%--<asp:RequiredFieldValidator ErrorMessage="درج مبلغ حکم الزامی است." ControlToValidate="txtMablaghHokm" ForeColor="Red" Display="Dynamic" runat="server" />--%>
                                                    <asp:TextBox ID="txtMablaghHokm" runat="server" ToolTip="ریال" CssClass="form-control form-inline " />
                                                    <span>ریال</span>
                                                </div>
                                                <div class="col-sm-4">
                                                    <p class="lblInput">بازنشسته :</p>
                                                    <asp:CheckBox ID="chbIsRetired" runat="server" />
                                                </div>

                                            </div>

                                            <div>
                                                <div class="col-sm-4">
                                                </div>
                                                <div runat="server" id="DivimgNewHokm" class="col-sm-4">
                                                    <a class="btn btn-success" href="OpenScanImage.aspx" target="_blank" id="imgNewHokm" runat="server">نمایش فایل اسکن شده</a>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:CheckBox runat="server" ID="chkBoundHour" Text="متقاضی تکمیل ساعت موظفی در واحد الکترونیکی هستم" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>

    <div class="panel panel-info" style="margin-top: 20px;">
        <div class="panel-heading">
            <div class="row">
                <div class="col-sm-12">
                    <i class="fa fa-info fa-2x pull-right"></i>
                    <span class=" pull-right" style="font-size: 22px; font-weight: bold;">اطلاعات تماس</span>
                </div>
            </div>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-12">
                    <div style="line-height: 12px;"><i class="fa fa-phone fa-1x pull-right"></i><span class=" pull-right">42863288</span></div>
                    <div style="clear: both; height: 10px;"></div>
                    <div style="line-height: 12px;"><i class="fa fa-envelope fa-1x pull-right"></i><span class=" pull-right">ap@iauec.ac.ir</span></div>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
