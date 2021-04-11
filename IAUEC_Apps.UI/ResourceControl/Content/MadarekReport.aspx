<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="MadarekReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.MadarekReport" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .marginRight {
            margin-right: 10px;
        }
    </style>
    <script type="text/javascript">
        function Check() {
            var riz = document.getElementById('chkRiz').Checked;
            var govahi = document.getElementById('chkGovahi').Checked;
            var danesh = document.getElementById('chkDanesh').Checked;

            if (riz && govahi && danesh) {
                var val = document.getElementById('valMadarek');
                val.IsValid = false;
            }
            return false;
        }
    </script>
    <script type="text/javascript">
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
        <div class="row" style="margin: 10px; font-family: 'B Yekan'">
            <div class="col-md-4">
                <asp:RequiredFieldValidator ID="rfvSDate" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Red" Text="*" ValidationGroup="submit" ErrorMessage="تاریخ شروع را انتخاب کنید" ControlToValidate="txtSdate"></asp:RequiredFieldValidator>
                از تاریخ درخواست:<asp:TextBox ID="txtSdate" runat="server" CssClass="marginR" MaxLength="9"></asp:TextBox>
                <script>
                    var objCal1 = new AMIB.persianCalendar('<%=txtSdate.ClientID%>',
                        { extraInputID: '<%=txtSdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                </script>
            </div>
            <div class="col-md-4">
                <asp:RequiredFieldValidator ID="rfvEDate" runat="server" Font-Bold="true" Font-Size="Medium" ForeColor="Red" Text="*" ErrorMessage="تاریخ پایان را انتخاب کنید" ValidationGroup="submit" ControlToValidate="txtEdate"></asp:RequiredFieldValidator>
                تا تاریخ درخواست:<asp:TextBox ID="txtEdate" CssClass="marginR" runat="server"></asp:TextBox>
                <script>
                    var objCal1 = new AMIB.persianCalendar('<%=txtEdate.ClientID%>',
                        { extraInputID: '<%=txtEdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                </script>
            </div>
            <div class="col-md-4">
                <asp:CustomValidator ID="valMadarek" runat="server" ErrorMessage="حداقل باید یکی از مدارک را انتخاب کنید" ValidationGroup="submit" Text="*" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:CustomValidator>
                مدارک:               
                <asp:CheckBox ID="chkRiz" runat="server" CssClass="marginRight" Checked="false" Text="ریز نمرات" TextAlign="Right" />
                <asp:CheckBox ID="chkGovahi" runat="server" CssClass="marginRight" Checked="false" Text="گواهی موقت" TextAlign="Right" />
                <asp:CheckBox ID="chkDanesh" runat="server" CssClass="marginRight" Checked="false" Text="دانشنامه" TextAlign="Right" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnSearch" runat="server" Text="جستجو" ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnExcel" runat="server" Text="تبدیل به فایل Excel" CssClass="btn btn-success" Visible="false" OnClick="btnExcel_Click" />
            </div>
            <div class="col-md-4">
            </div>
        </div>
        <div class="row bg-danger" style="margin-right: 10px">
            <asp:ValidationSummary ID="validSummary" runat="server" ForeColor="#d60000" ValidationGroup="submit" HeaderText="لطفا به موارد زیر دقت کنید" />
        </div>
        <div class="row">
            <div class="col-md-1">
            </div>
            <div class="col-md-10">
                <div class="table-responsive">
                    <telerik:RadGrid ID="grdResult" runat="server" MasterTableView-ShowHeadersWhenNoRecords="true" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="100" AllowFilteringByColumn="true" EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnItemDataBound="grdResult_ItemDataBound" OnNeedDataSource="grdResult_NeedDataSource" OnItemCommand="grdResult_ItemCommand" OnExcelMLWorkBookCreated="grdResult_ExcelMLWorkBookCreated">
                        <MasterTableView>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="شماره درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CreateDate" HeaderText="تاریخ درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StCode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="name" HeaderText="نام دانشجو" AllowFiltering="false" />
                                <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="namedanesh" HeaderText="دانشکده"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="تاریخچه" AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:Button ID="btnHistory" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-primary" Text="تاریخچه" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="col-md-1">
            </div>
        </div>
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
                            <div class="col-md-3">
                                <div id="info3" runat="server"></div>
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
    <uc1:AccessControl ID="AccessControl1" runat="server" />

</asp:Content>
