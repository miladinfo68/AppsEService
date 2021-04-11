<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSEditInfoRequest.Master" AutoEventWireup="true" CodeBehind="RejectImageEditReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.RejectImageEditReport" %>
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
      <telerik:RadGrid ID="grd_RejectEditImage"  runat="server" AllowPaging="true" PageSize="20"  AllowFilteringByColumn="True"  AutoGenerateColumns="False"  OnNeedDataSource="grd_RejectEditImage_NeedDataSource" EnableEmbeddedSkins="False">
         <MasterTableView HeaderStyle-Font-Bold="true"  Width="100%" >
              <EditFormSettings>
                  <EditColumn CancelImageUrl="Cancel.gif" FilterImageUrl="Filter.gif" InsertImageUrl="Update.gif" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif" UpdateImageUrl="Update.gif">
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
                  <telerik:GridBoundColumn AllowFiltering="true" DataField="stcode" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" HeaderText="شماره دانشجویی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="true" DataField="name" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" HeaderText="نام و نام خانوادگی" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="idresh" FilterImageUrl="Filter.gif" HeaderText="رشته" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="magh" FilterImageUrl="Filter.gif" HeaderText="مقطع" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn AllowFiltering="false" DataField="Tozihat" FilterImageUrl="Filter.gif" HeaderText="علت رد درخواست" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBoundColumn>
                  <telerik:GridBinaryImageColumn AllowFiltering="false" DataField="PersonalImage" FilterImageUrl="Filter.gif" HeaderText="عکس جدید" ImageHeight="60px" ImageWidth="60px" ResizeMode="Fit" SortAscImageUrl="SortAsc.gif" SortDescImageUrl="SortDesc.gif">
                  </telerik:GridBinaryImageColumn>
              </Columns>
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
