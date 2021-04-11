<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSGovahiRequestMaster.Master" AutoEventWireup="true" CodeBehind="PaymentReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.PaymentReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
    <div dir="rtl">
        <asp:Panel ID="pnl_Main" runat="server">

            <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
                AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="true" />
            <telerik:RadGrid ID="grd_PayReport" FilterItemStyle-Height="23px" runat="server" AllowPaging="true" PageSize="20" AllowFilteringByColumn="True"
                AutoGenerateColumns="False" OnNeedDataSource="grd_PayReport_NeedDataSource"
                EnableEmbeddedSkins="False"  Skin="MyCustomSkin">
                <MasterTableView HeaderStyle-Font-Bold="true" Width="100%">
                    <FilterItemStyle HorizontalAlign="Center" />

                     <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" HorizontalAlign="Center" />
                    <CommandItemSettings AddNewRecordImageUrl="AddRecord.gif" ExportToCsvImageUrl="ExportToCsv.gif" ExportToExcelImageUrl="ExportToExcel.gif" ExportToPdfImageUrl="ExportToPdf.gif" ExportToWordImageUrl="ExportToWord.gif" RefreshImageUrl="Refresh.gif" />
                    <RowIndicatorColumn FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn CollapseImageUrl="SingleMinus.gif" ExpandImageUrl="SinglePlus.gif" FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" FilterImageUrl="Filter.gif" HeaderText="ردیف" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                            <ItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" DataField="StCode" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" HeaderText="شماره دانشجویی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" DataField="name" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" HeaderText="نام و نام خانوادگی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="nameresh" FilterImageUrl="Filter.gif" HeaderText="رشته" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="TraceNumber" FilterImageUrl="Filter.gif" HeaderText="شماره پیگیری" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="AmountTrans" FilterImageUrl="Filter.gif" HeaderText="مبلغ(ریال)" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="false" DataField="PersianDate" FilterImageUrl="Filter.gif" HeaderText="تاریخ پرداخت" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn CancelImageUrl="Cancel.gif" FilterImageUrl="Filter.gif" InsertImageUrl="Update.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" UpdateImageUrl="Update.gif">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerStyle FirstPageImageUrl="PagingFirst.gif" LastPageImageUrl="PagingLast.gif" NextPageImageUrl="PagingNext.gif" PrevPageImageUrl="PagingPrev.gif" />
                  
                </MasterTableView>
                <PagerStyle FirstPageImageUrl="PagingFirst.gif" LastPageImageUrl="PagingLast.gif" NextPageImageUrl="PagingNext.gif" PrevPageImageUrl="PagingPrev.gif" />
                <FilterMenu EnableEmbeddedSkins="False">
                </FilterMenu>
                <HeaderContextMenu EnableEmbeddedSkins="False">
                </HeaderContextMenu>
            </telerik:RadGrid>
        </asp:Panel>
    </div>
</asp:Content>
