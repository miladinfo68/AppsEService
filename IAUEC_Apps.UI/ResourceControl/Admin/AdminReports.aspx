<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="AdminReports.aspx.cs" Inherits="ResourceControl.PL.Admin.AdminReports" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h1>فرم گزارش گیری مدیر</h1>
    <asp:Literal ID="pt" runat="server" Visible="false"></asp:Literal>
    <style>
        .marginItem {
            margin: 10px;
        }

        .paddingItem {
            padding: 20px;
        }

        .centerItem {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
    </script>
    <link href="../content/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../content/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />
    <div class="container-fluid rtl">

        <div class="row marginItem">

            <div class="col-md-4">
                <asp:UpdatePanel ID="udp" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="drpReportChoose" runat="server" CssClass="form-control" OnSelectedIndexChanged="drpReportChoose_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="درخواست های منتظر ارسال" Value="0"></asp:ListItem>
                            <asp:ListItem Text="درخواست های منتظر تایید" Value="1"></asp:ListItem>
                            <asp:ListItem Text="درخواست های تایید شده" Value="2"></asp:ListItem>
                            <asp:ListItem Text="درخواست های اطلاع رسانی شده" Value="4"></asp:ListItem>
                            <asp:ListItem Text="درخواست های رد شده" Value="3"></asp:ListItem>
                            <asp:ListItem Text="تمام درخواستها" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpReportChoose" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="col-md-3">
                تاریخ شروع:
                        <asp:TextBox ID="txtSdate" runat="server" CssClass="marginR" MaxLength="9"></asp:TextBox>
                <script>
                    var objCal1 = new AMIB.persianCalendar('<%=txtSdate.ClientID%>',
                        { extraInputID: '<%=txtSdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                </script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSdate" ErrorMessage="*" ForeColor="Red" ValidationGroup="eee">*</asp:RequiredFieldValidator>
            </div>
            <div class="col-md-3">
                تاریخ پایان:
                        <asp:TextBox ID="txtEdate" runat="server" CssClass="marginR" MaxLength="9"></asp:TextBox>
                <script>
                    var objCal1 = new AMIB.persianCalendar('<%=txtEdate.ClientID%>',
                        { extraInputID: '<%=txtEdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                </script>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEdate" ErrorMessage="*" ForeColor="Red" ValidationGroup="eee">*</asp:RequiredFieldValidator>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnAddRequest" Text="جستجو" runat="server" CssClass="btn btn-primary" OnClick="btnAddDate_Click" ValidationGroup="eee" />
            </div>
        </div>
        <div class="row table-responsive marginItem">
            <telerik:RadGrid ID="grdRequestLists" runat="server" MasterTableView-ShowHeadersWhenNoRecords="true" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="100" AllowFilteringByColumn="false" EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnNeedDataSource="grdRequestLists_NeedDataSource" OnItemCommand="grdRequestLists_ItemCommand">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="شماره درخواست"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="issue_time" HeaderText="تاریخ درخواست"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="issuerID" HeaderText="شماره درخواست کننده" Visible="false" />
                        <telerik:GridBoundColumn DataField="issuerName" HeaderText="نام درخواست کننده" />
                        <telerik:GridBoundColumn DataField="catID" HeaderText="نوع کلاس"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ClassName" HeaderText="نام کلاس"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="محل برگزاری"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="courseName" HeaderText="نام درس"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="date" HeaderText="تاریخ برگزاری"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="تاریخچه" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' ImageUrl="~/University/Theme/images/History.png" Width="40" Height="40" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document" style="margin: 100px;">
            <div class="modal-content" style="width: 1000px;">
                <div class="modal-header" dir="rtl">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;
                        </span>
                    </button>
                    <div class="modal-header" dir="rtl">
                    </div>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                </div>
                <div class="modal-body">
                    <div dir="rtl" style="font-size: medium">
                        <div id="dd" runat="server"></div>
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
</asp:Content>
