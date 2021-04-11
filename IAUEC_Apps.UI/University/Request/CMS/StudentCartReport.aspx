<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSRequestMaster.Master" AutoEventWireup="true" CodeBehind="StudentCartReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.StudentCartReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
    .RadGrid .rgFilterRow input {
            height:25px;
        }
    </style>
    <div dir="rtl">
      <asp:Panel ID="pnl_Main" runat="server">

        <asp:ImageButton ID="img_ExportToExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png"
             AlternateText="ExcelML" OnClick="img_ExportToExcel_Click" Visible="true" />
      <telerik:RadGrid ID="grd_CardReport" runat="server"  Width="100%" HorizontalAlign="Center" FilterItemStyle-Height="23px" AllowPaging="true" PageSize="20"  AllowFilteringByColumn="True"  AutoGenerateColumns="False"  OnNeedDataSource="grd_CardReport_NeedDataSource" EnableEmbeddedSkins="False"  BorderColor="#FFFF66">
         <MasterTableView HeaderStyle-Font-Bold="true"  Width="100%" >
              <EditFormSettings>
                  <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" FilterImageUrl="Filter.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </EditColumn>
              </EditFormSettings>
             
              <PagerStyle FirstPageImageUrl="PagingFirst.gif" LastPageImageUrl="PagingLast.gif" NextPageImageUrl="PagingNext.gif" PrevPageImageUrl="PagingPrev.gif" />
            <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin"  HorizontalAlign="Center" />
              
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
                  <telerik:GridBoundColumn AllowFiltering="true" DataField="STcode" FilterControlWidth="90px" FilterImageUrl="../../Theme/images/filter2.png" HeaderText="شماره دانشجویی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" DataField="name" HeaderText="نام و نام خانوادگی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="idresh" FilterImageUrl="Filter.gif" HeaderText="رشته" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="magh" FilterImageUrl="Filter.gif" HeaderText="مقطع" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="Erae_Be" FilterImageUrl="Filter.gif" HeaderText="محل ارائه" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="PostNumber" FilterImageUrl="Filter.gif" HeaderText="کد مرسوله پستی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="true" DataField="RequestLogName" FilterControlWidth="90px" FilterImageUrl="../../Theme/images/filter2.png" HeaderText="وضعیت درخواست" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
              </Columns>
            </MasterTableView>
            <FilterItemStyle Height="30px" Width="15px" />
            <PagerStyle FirstPageImageUrl="PagingFirst.gif" LastPageImageUrl="PagingLast.gif" NextPageImageUrl="PagingNext.gif" PrevPageImageUrl="PagingPrev.gif" />
          <FilterMenu EnableEmbeddedSkins="False">
          </FilterMenu>
          <HeaderContextMenu EnableEmbeddedSkins="False">
          </HeaderContextMenu>
            </telerik:RadGrid>
   </asp:Panel>
      </div>
</asp:Content>
