<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSEditInfoRequest.Master" AutoEventWireup="true" CodeBehind="AcceptedEditReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.AcceptedEditReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
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
            <telerik:RadGrid ID="grd_AcceptEdit" runat="server" FilterItemStyle-Height="23px" AllowPaging="true" AllowFilteringByColumn="True" AutoGenerateColumns="False"
                OnNeedDataSource="grd_AcceptEdit_NeedDataSource" EnableEmbeddedSkins="False">
                <MasterTableView HeaderStyle-Font-Bold="true" Width="100%">
                    <EditFormSettings>
                        <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                        </EditColumn>
                    </EditFormSettings>

                    <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" HorizontalAlign="Center" />

                    <Columns>


                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" AllowFiltering="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" AllowFiltering="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idresh" HeaderText="رشته" AllowFiltering="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="magh" HeaderText="مقطع" AllowFiltering="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EditeTypeName" HeaderText="نوع ویرایش" FilterControlWidth="100px" FilterImageUrl="../../Theme/images/filter2.png" AllowFiltering="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NewContent" HeaderText="عبارت جدید" AllowFiltering="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Date" HeaderText="تاریخ درخواست" AllowFiltering="false"></telerik:GridBoundColumn>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
    </div>
</asp:Content>
