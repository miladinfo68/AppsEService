<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="requestInquiryDocument.aspx.cs" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.requestInquiryDocument" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server">

    <link href="../../../Adobe/css/aqua/theme.css" rel="stylesheet" title="Aqua" type="text/css" />
    <script src="../../../Adobe/js/jalali.js"></script>

    <!-- import the calendar script -->
    <script src="../../../Adobe/js/calendar.js"></script>

    <!-- import the calendar script -->
    <script src="../../../Adobe/js/calendar-setup.js"></script>

    <!-- import the language module -->
    <script src="../../../Adobe/js/lang/calendar-fa.js"></script>

</asp:Content>
<asp:Content ContentPlaceHolderID="PageTitle" runat="server"></asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function openModal_ConfirmNewInquiry() {
            $('#mdlConfirmNewInquiry').modal('show');
        }
        function closeModal_NewInquiry() {
            $('#mdlConfirmNewInquiry').modal('hide');
        }
        function openModal_ConfirmUpdateInquiry() {
            $('#mdlConfirmUpdateInquiry').modal('show');
        }
        function closeModal_UpdateInquiry() {
            $('#mdlConfirmUpdateInquiry').modal('hide');
        }
        function openModal_ConfirmDeleteInquiry() {
            $('#mdlConfirmDeleteInquiry').modal('show');
        }
        function closeModal_DeleteInquiry() {
            $('#mdlConfirmDeleteInquiry').modal('hide');
        }
        function openModal_ShowMessage() {
            $('#mdlShowMessage').modal('show');
        }
    </script>
    <div class="modal fade" id="mdlConfirmNewInquiry" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal runat="server" Text="کاربر گرامی؛ آیا از ثبت این تاییدیه اطمینان دارید؟" />
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-3">

                                        <asp:Button ID="mdlBtnConfirmNewInquiry" runat="server" CssClass="btn btn-success form-control" OnClick="mdlBtnConfirmNewInquiry_Click" Text="بله"></asp:Button>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-3">

                                        <asp:Button ID="mdlBtnDeclineNewInquiry" runat="server" CssClass="btn btn-danger form-control" OnClientClicked="closeModal_NewInquiry" Text="خیر"></asp:Button>
                                    </div>
                                    <div class="col-md-2"></div>

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
    <div class="modal fade" id="mdlConfirmUpdateInquiry" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal runat="server" Text="کاربر گرامی؛ آیا از ویرایش این تاییدیه اطمینان دارید؟" />
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-3">

                                        <asp:Button ID="mdlBtnConfirmUpdateInquiry" runat="server" CssClass="btn btn-success form-control" OnClick="mdlBtnConfirmUpdateInquiry_Click" Text="بله"></asp:Button>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-3">

                                        <asp:Button ID="mdlBtnDeclineUpdateInquiry" runat="server" CssClass="btn btn-danger form-control" OnClientClicked="closeModal_UpdateInquiry" Text="خیر"></asp:Button>
                                    </div>
                                    <div class="col-md-2"></div>

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
    <div class="modal fade" id="mdlConfirmDeleteInquiry" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal runat="server" Text="کاربر گرامی؛ آیا از حذف این تاییدیه اطمینان دارید؟" />
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2"></div>
                                    <div class="col-md-3">

                                        <asp:Button ID="mdlBtnConfirmDeleteInquiry" runat="server" CssClass="btn btn-success form-control" OnClick="mdlBtnConfirmDeleteInquiry_Click_Click" Text="بله"></asp:Button>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-3">

                                        <asp:Button ID="mdlBtnDeclineDeleteInquiry" runat="server" CssClass="btn btn-danger form-control" OnClientClicked="closeModal_DeleteInquiry" Text="خیر"></asp:Button>
                                    </div>
                                    <div class="col-md-2"></div>

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
    <div class="modal fade" id="mdlShowMessage" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText success">
                                    <asp:Literal runat="server" ID="ltrMsgText" />
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


    <div class="container" dir="rtl">

        <div class="panel">
            <div class="panel-heading">

                <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btnTabSearch" runat="server" CssClass="form-control btn-info" Text="جستجو" OnClick="btnTab_Click" CommandArgument="search" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnTabNew" runat="server" CssClass="form-control btn-success" Text="درج استعلام جدید" OnClick="btnTab_Click" CommandArgument="new" />
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btnTabEdit" runat="server" CssClass="form-control btn-warning" Text="ویرایش" OnClick="btnTab_Click" CommandArgument="edit" />
                    </div>
                </div>

            </div>
            <div class="panel-body">
                <div id="dvSearch" runat="server" visible="true" style="background-color: lightblue">
                    <br />
                    <div class="row">
                        <div class="col-md-1">
                            <span>شماره دانشجویی</span>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtStcode_Search" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1">
                            <span>کد ملی</span>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtNationalCode_Search" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1">
                            <span>نوع تاییدیه</span>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlConfirmationType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="همه موارد" Value="0"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه گواهی موقت" Value="1"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه دانشنامه" Value="2"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه دانشنامه و ریزنمرات" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="btn btn-info form-control" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadGrid runat="server" ID="grdConfirmation" AutoGenerateColumns="false" OnItemCommand="grdConfirmation_ItemCommand">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="stcode"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="نام و نام خانوادگی" DataField="studentName"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="نوع گواهی" DataField="typeName"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="جهت ارائه به" DataField="toPresentTo"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="شماره نامه" DataField="letterNumber"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="تاریخ درخواست" DataField="letterDate"></telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn HeaderText="تاریخ تایید مدرک" DataField="documentAcceptDate"></telerik:GridBoundColumn>--%>
                                        <telerik:GridBoundColumn HeaderText="توضیحات" DataField="note"></telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowInquiry" Text="مشاهده گواهی" CssClass="btn alert-success" Font-Names="b nazanin" runat="server" CommandName="ShowReport"
                                                    CommandArgument='<%#Eval("id")+","+Eval("inquiryType")+","+Eval("stcode")%>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
                        </div>
                    </div>
                    <br />
                </div>
                <div id="dvNew" runat="server" visible="false" style="background-color: lightgreen">
                    <br />
                    <div class="row">
                        <div class="col-md-1"><span>کد دانشجویی</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtStcode_Insert" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1"><span>نوع گواهی استعلام</span></div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlInquiryType_New" runat="server" CssClass="form-control">
                                <asp:ListItem Text="تاییدیه گواهی موقت" Value="1"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه دانشنامه" Value="2"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه دانشنامه و ریزنمرات" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>


                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1"><span>جهت ارائه به</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtToPresentTo_New" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1"><span>شماره نامه</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtLetterNumber_New" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1">
                            <span>تاریخ درخواست</span>

                        </div>
                        <div class="col-md-3" style="position: relative">
                            <input type="text" id="txtRequestDate_New" runat="server" class="form-control" />
                            <img id="date_btn_1" src="../../Theme/images/cal.png" style="vertical-align: top; position: absolute; left: 5px; top: 5px" class="left" />
                            <script type="text/javascript">

                                Calendar.setup({
                                    inputField: "ContentPlaceHolder1_txtRequestDate_New",   // id of the input field
                                    button: "date_btn_1",   // trigger for the calendar (button ID)
                                    ifFormat: "%Y/%m/%d",       // format of the input field
                                    dateType: 'jalali',
                                    weekNumbers: false
                                });
                            </script>
                            <script type="text/javascript">

                                setActiveStyleSheet(document.getElementById("defaultTheme"), "Aqua");
                            </script>

                        </div>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1"><span>توضیحات</span></div>
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtNote_New" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnSave" runat="server" Text="ذخیره" OnClick="btnSave_Click" CssClass="btn btn-success form-control" />
                        </div>
                    </div>
                    <br />

                </div>
                <div id="dvEdit" runat="server" visible="false" style="background-color: #ffe650; padding-right: 20px">
                    <br />

                    <div class="row">
                        <div class="col-md-1"><span>کد دانشجویی</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtStcode_Update" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1"><span>نوع گواهی استعلام</span></div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlInquiryType_Update" runat="server" CssClass="form-control">
                                <asp:ListItem Text="تاییدیه گواهی موقت" Value="1"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه دانشنامه" Value="2"></asp:ListItem>
                                <asp:ListItem Text="تاییدیه دانشنامه و ریزنمرات" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <asp:Button runat="server" ID="btnSearchToUpdate" CssClass="btn btn-warning form-control" Text="جستجو" OnClick="btnSearchToUpdate_Click" />
                        </div>

                    </div>
                    <br />
                    <hr />
                    <div class="row">

                        <div class="col-md-3">

                            <asp:DropDownList ID="ddlInquiries" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlInquiries_SelectedIndexChanged"></asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnDeleteInquiry" runat="server" Text="حذف مورد انتخابی" CssClass="btn btn-danger form-control" OnClick="btnDeleteInquiry_Click" />
                        </div>
                    </div>
                    <br />
                    <hr />
                    <asp:HiddenField ID="hdnInquiryID" runat="server" />
                    <div class="row">
                        <div class="col-md-1"><span>جهت ارائه به</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtToPresentTo_Update" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1"><span>شماره نامه</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtLetterNumber_Update" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1"><span>تاریخ درخواست</span></div>
                        <div class="col-md-3" style="position: relative">
                            <input id="txtRequestDate_Update" runat="server" class="form-control" />
                            <img id="date_btn_3" src="../../Theme/images/cal.png" style="vertical-align: top; position: absolute; left: 5px; top: 5px" class="left" />
                            <script type="text/javascript">

                                Calendar.setup({
                                    inputField: "ContentPlaceHolder1_txtRequestDate_Update",   // id of the input field
                                    button: "date_btn_3",   // trigger for the calendar (button ID)
                                    ifFormat: "%Y/%m/%d",       // format of the input field
                                    dateType: 'jalali',
                                    weekNumbers: false
                                });
                            </script>
                        </div>
                        <%-- <div class="col-md-1"><span>تاریخ تایید مدرک</span></div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDocAcceptDate_Update" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>--%>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-1"><span>توضیحات</span></div>
                        <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtNote_Update" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnUpdate" runat="server" Text="ذخیره" OnClick="btnUpdate_Click" CssClass="btn btn-warning form-control" />
                        </div>
                    </div>
                    <br />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
