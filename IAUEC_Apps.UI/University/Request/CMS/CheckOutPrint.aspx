<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutPrint.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutPrint" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        th {
            text-align: center;
            border: 1px solid #808080;
        }
    </style>
    <script type="text/javascript">
        function getprompt(args) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = prompt("لطفا پیام را وارد کنید :");
            if (confirm_value.value != null) {
                document.forms[0].appendChild(confirm_value);
            }
        }

        function refreshGrid(arg) {
            if (!arg) {
                window.location.reload();
                GetRadWindow().close();
            }
        }

    </script>
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>لیست درخواستهای تایید شده</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div class="container text-right" dir="rtl">
        <div class="col-md-3">
            <asp:DropDownList ID="drpUserRoles" runat="server" CssClass=" dropdown text-right with-initial" Visible="false" Enabled="false" OnSelectedIndexChanged="drpUserRoles_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <asp:Label ID="lblMessage" Text="" runat="server" ForeColor="Red" Visible="false" />
        <telerik:RadGrid ID="grd_CheckOutList" runat="server" AutoGenerateColumns="false"
            EnableEmbeddedSkins="False" Skin="MyCustomSkin" OnItemDataBound="grd_CheckOutList_ItemDataBound"
            OnItemCommand="grd_CheckOutList_ItemCommand" OnNeedDataSource="grd_CheckOutList_NeedDataSource"
            AllowFilteringByColumn="True" AllowPaging="true"
            EnableLinqExpressions="false">
            <MasterTableView NoMasterRecordsText="درخواستی وجود ندارد.">
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                    <telerik:GridBoundColumn HeaderText="شماره درخواست" DataField="StudentRequestID"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="تاریخ درخواست" DataField="CreateDate" AllowFiltering="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StCode"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نام دانشجو" DataField="name"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نوع درخواست" DataField="RequestTypeName" AllowFiltering="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="وضعیت" DataField="Note" AllowFiltering="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نام دانشکده" DataField="namedanesh"></telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="تاریخ انتخاب واحد" Visible="false" UniqueName="DateVahed" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblEntekhabVahedDate" Text="" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="مدت زمان حضور دانشجو(دقیقه)" Visible="false" UniqueName="HourInTerm" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Label ID="lblHozoorHour" Text="" runat="server" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="عملیات چاپ" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnPrintCheckOut" Text="چاپ گواهی" runat="server" CommandName="print" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-success" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="چاپ گواهی بدهی وام وزارت علوم" Visible="false" UniqueName="prtloan" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnPrintVezaratLoan" Text="چاپ گواهی" runat="server" Enabled="false" CommandName="printBedehi" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-warning" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="چاپ اطلاعات حساب" Visible="true" UniqueName="prtinfo" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnPrintAcountInfo" Text="چاپ حساب" runat="server" CommandName="printAcountInfo" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-danger" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                   <%-- <telerik:GridTemplateColumn HeaderText="چاپ حضور در کلاس" Visible="true" UniqueName="prtClass" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnPrintAcountInfo1" Text="چاپ گزارش" runat="server" CommandName="printClassPresent" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-warning" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>--%>
                    <telerik:GridBoundColumn HeaderText="نوع درخواست" DataField="RequestTypeID" AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>

                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LoanStatus" AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>

                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="datesabtv" AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                        <HeaderStyle CssClass="hidden"></HeaderStyle>

                        <ItemStyle CssClass="hidden"></ItemStyle>
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
    </div>
    <div id="parent" runat="server"></div>
</asp:Content>
