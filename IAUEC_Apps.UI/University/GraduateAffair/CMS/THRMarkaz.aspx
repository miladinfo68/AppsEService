<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="THRMarkaz.aspx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.THRMarkaz" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function radconfirm() {
            //debugger;
            $('#confirmMsg').modal('show');
        }
        function closeradModal() {
            //debugger;
            $('#confirmMsg').modal('hide');
        }

        function radAlert() {
            $('#alertMsg').modal('show');
        }
    </script>
    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%;">
        <div class="row">
            <div class="col-md-12" style="padding: 1%">
                <div class="col-md-2">شماره دانشجویی را وارد نمایید:</div>
                <div class="col-md-2">
                    <asp:TextBox ID="txt_stcode" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSearchStudent" OnClick="btnSearchStudent_Click" runat="server" Text="جستجو" CssClass="btn btn-primary" />
                </div>

            </div>
        </div>
        <div class="row">
            <div class="col-md-12" id="dvStudentInf" runat="server" visible="false">
                <div class="row box border-blue bg-white">
                    <telerik:RadGrid ID="grdShowStudentInf" Width="100%" OnItemCommand="grdShowStudentInf_ItemCommand" AutoGenerateColumns="false" EnableEmbeddedSkins="false" Skin="MyCustomSkin" PagerStyle-BackColor="#99FF66" runat="server" EmptyDataText="دانشجو با کد وارد شده یافت نشد" Font-Size="Larger" ItemStyle-BackColor="White">
                        <MasterTableView Dir="RTL" NoMasterRecordsText="دانشجوی فارغ التحصیلی با کد دانشجویی وارد شده یافت نشد" NoDetailRecordsText="دانشجوی فارغ التحصیلی با کد دانشجویی وارد شده یافت نشد">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="نام و نام خانوادگی" DataField="name"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="رشته تحصیلی" DataField="field"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="ثبت شده در تهران مرکز" DataField="regInDB"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="اکسل گرفته شده" DataField="inExcel"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="hdnInExcel" Visible="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="hdnRegInDB" Visible="false" HeaderText="hi header"></telerik:GridBoundColumn>
                                <telerik:GridButtonColumn ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" UniqueName="btnRemove_Excel" CommandName="removeExcel" ButtonType="PushButton" Text="افزودن به لیست اکسل جدید" ButtonCssClass="btn btn-warning"></telerik:GridButtonColumn>
                                <telerik:GridButtonColumn ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" UniqueName="btnRemove_DB" CommandName="removeThr" ColumnGroupName="thnList" ButtonType="PushButton" Text=" حذف از لیست تهران مرکزی" ButtonCssClass="btn btn-danger"></telerik:GridButtonColumn>
                                <telerik:GridButtonColumn ItemStyle-BorderStyle="None" HeaderStyle-BorderStyle="None" UniqueName="btnAdd_DB" CommandName="addThr" ColumnGroupName="thnList" ButtonType="PushButton" Text="افزودن به لیست تهران مرکزی" ButtonCssClass="btn btn-info"></telerik:GridButtonColumn>

                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="padding: 1%">
                <asp:Button ID="btn_excel" runat="server" CssClass="btn btn-success" OnClick="btn_excel_Click" Text="تبدیل به فایل اکسل" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="padding: 1%">

                <telerik:RadGrid ID="grd_view" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="false" Skin="MyCustomSkin" AllowFilteringByColumn="true" OnNeedDataSource="grd_view_NeedDataSource">
                    <MasterTableView>
                        <Columns>
                            <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="rsh" HeaderText="رشته"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="status" HeaderText="وضعیت"></telerik:GridBoundColumn>

                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

            </div>
        </div>


        <div class="modal fade" id="confirmMsg" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
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
                                        <asp:HiddenField ID="hdnConfirmWhat" runat="server" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="radBtnYes" runat="server" OnClick="radBtnYes_Click" Text="بله">
                                        </telerik:RadButton>
                                        <%--<telerik:RadButton ID="radBtnNo" OnClientClicked="closeradModal()" runat="server" Text="خیر">
                                    </telerik:RadButton>--%>
                                        <telerik:RadButton ID="radBtnNo" runat="server" Text="خیر">
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


        <div class="modal fade" id="alertMsg" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H1">پیام</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid" dir="rtl">
                            <div class="row" style="border: 1px solid rgba(255, 0, 0, 0.90); background-color: rgba(241, 108, 165, 0.70); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                <div class="rwDialogPopup radconfirm">
                                    <div class="rwDialogText">
                                        <asp:Literal ID="msgAlert" runat="server" Text="" />
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
    </div>

    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
