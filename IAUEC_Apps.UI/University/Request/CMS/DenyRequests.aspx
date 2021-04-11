<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="DenyRequests.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.DenyRequests" %>

<%@ Register src="../../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .MarginItem {
            margin-top: 10px;
            margin-bottom: 10px;
        }

        th {
            text-align: center;
            border: 1px solid #808080;
            font-family: 'B Titr',Arial,'B Yekan' !important;
        }

        #ContentPlaceHolder1_grdCheckOutList td {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
            color: black;
        }

        .table-condensed > thead > tr > th, .table-condensed > tbody > tr > th, .table-condensed > tfoot > tr > th, .table-condensed > thead > tr > td, .table-condensed > tbody > tr > td, .table-condensed > tfoot > tr > td {
            padding: 3px !important;
        }

        .radfont, .rgHeader {
            font-family: 'B Titr' !important;
            font-weight: bold;
        }

        .rwContentRow {
            font-family: 'B Yekan',Tahoma !important;
            font-size: 14px !important;
        }
    </style>
    <script type="text/javascript">
        function confirmAspButton(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
        }


        function openModal() {
            $('#exampleModal').modal('show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
     <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" dir="rtl">
        <div class="row MarginItem">
            <div class="col-md-3">
                <uc1:AccessControl ID="AccessControl1" runat="server" />
                شماره دانشجویی:
                <asp:RequiredFieldValidator ID="rfvStcode" runat="server" Text="*" ErrorMessage="لطفا شماره دانشجویی را وارد کنید" ControlToValidate="txtStcode" ForeColor="Red" ValidationGroup="search"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtStcode" runat="server" MaxLength="14" ValidationGroup="search"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" ValidationGroup="search" Text="جستجو" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="row">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="search" />
        </div>
        <div class="row">
            <asp:GridView ID="grdResult" runat="server" Width="1050px" AutoGenerateColumns="false" EmptyDataText="رکوردی پیدا نشد" CellPadding="4" ShowHeaderWhenEmpty="true" ForeColor="#333333" GridLines="None" OnRowCommand="grdResult_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="رديف" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# (Container.DataItemIndex + 1) %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="شماره درخواست" DataField="StudentRequestID" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField HeaderText="شماره دانشجویی" DataField="stcode" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField HeaderText="نام و نام خانوادگی" DataField="fullName" ItemStyle-HorizontalAlign="center" />
                    <asp:BoundField HeaderText="وضعیت فعلی" DataField="CurrentStatus" ItemStyle-HorizontalAlign="center" />
                    <asp:TemplateField HeaderText="تاریخچه" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnHistory" Text="تاریخچه" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-primary MarginItem" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="علت رد درخواست" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:RequiredFieldValidator ID="rfvTxtReason" runat="server" Text="*" ErrorMessage="لطفا علت رد درخواست را ذکر کنید" ValidationGroup="msg" ControlToValidate="txtDenyReason" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDenyReason" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="رد کردن درخواست" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnDeny" Text="رد درخواست" runat="server" CommandName="Deny" ValidationGroup="msg" CssClass="btn btn-danger MarginItem" OnClientClick="confirmAspButton(this); return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog" role="document" style="margin: 100px;">
                <div class="modal-content" style="width: 1000px;">
                    <div class="modal-header" dir="rtl">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                    </div>
                    <div class="modal-body">
                        <div dir="rtl" style="font-size: medium">
                            <div class="row">
                                <div class="col-md-3">
                                    <div id="info1" runat="server"></div>
                                </div>
                                <div class="col-md-3">
                                    <div id="info2" runat="server"></div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(59,131,255,0.9); background-color: rgba(59,131,255,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="col-md-12">
                                <div class="col-md-2">
                                    نام کاربر
                                </div>
                                <div class="col-md-2">
                                    تاریخ
                                </div>
                                <div class="col-md-2">
                                    ساعت
                                </div>
                                <div class="col-md-3">
                                    وضعیت
                                </div>
                                <div class="col-md-3">
                                    توضیحات
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(59,131,255,0.7); background-color: rgba(59,131,255,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">

                            <asp:ListView ID="lst_history" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-12" style="border-style: none none solid none; border-width: 0px 0px 1px 1px; border-color: rgba(59,131,255,0.7);">
                                        <div class="col-md-2">
                                            <%#Eval("Name") %>
                                        </div>
                                        <div class="col-md-2">
                                            <%#Eval("LogDate") %>
                                        </div>
                                        <div class="col-md-2">
                                            <%#Eval("LogTime") %>
                                        </div>
                                        <div class="col-md-3">
                                            <%#Eval("EventName") %>
                                        </div>
                                        <div class="col-md-3">
                                            <%#Eval("Description") %>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
