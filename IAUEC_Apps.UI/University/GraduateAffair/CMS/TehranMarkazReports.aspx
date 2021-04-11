<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="TehranMarkazReports.aspx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.TehranMarkazReports" %>

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

    <link href="../../Theme/js/Picker/jspc-peach.css" rel="stylesheet" />
    <script src="../../Theme/js/Picker/js-persian-cal.min.js"></script>

    <div class="container-fluid" dir="rtl" style="border: 1px solid rgba(255, 167, 14,0.7); background-color: rgba(231, 230, 230,0.3); padding: 1%; border-radius: 5px; margin-bottom: 1%;">
        <div class="row">
            <div class="col-md-12" style="padding: 1%">

                <div class="col-md-12">


                    <p id="dateofsession">بازه تاریخ :</p>

                    <%--  <asp:TextBox ID="pcal1" runat="server" CssClass="pdate form-control" Enabled="true" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>--%>
                    <div class="col-md-5">

                        <span style="margin-right: 13%;">از تاریخ:</span>
                        <%-- <input type="text" name="SearchDate"  id="pcal1" />
                                  
                    <input type="text" name="SearchDate"  id="pcal2" />--%>

                        <asp:TextBox ID="pcal1" runat="server"></asp:TextBox>

                        <span>تا تاریخ:</span>
                        <asp:TextBox ID="pcal2" runat="server"></asp:TextBox>
                        <%-- <asp:TextBox ID="pcal2" runat="server" CssClass="pdate form-control" Enabled="true" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="pcal2" ErrorMessage="لطفا تاریخ استفاده را صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ValidationGroup="submit">*</asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-md-7">
                        <asp:Button ID="CustomFilterBtn" CssClass="btn btn-primary" runat="server" Text="جستجو" OnClick="CustomFilterBtn_Click" Height="30" />

                        <asp:ImageButton ID="imgBtnExcel" runat="server" ImageUrl="../../Theme/images/Excel_ExcelML.png" OnClick="imgBtnExcel_Click" AlternateText="Convert To Excel" Width="30" ImageAlign="Left" />

                    </div>
                    <br />

                    <br />

                </div>

                <telerik:RadGrid ID="grd_view" runat="server" AllowPaging="true"
                    PageSize="30" OnItemCreated="grd_view_ItemCreated" OnHTMLExporting="grd_view_HTMLExporting"
                    AutoGenerateColumns="false" OnNeedDataSource="grd_view_NeedDataSource" CellSpacing="0" GridLines="None" SortingSettings-EnableSkinSortStyles="false"  AllowSorting="True" EnableEmbeddedSkins="false" Skin="MyCustomSkin">
                    <MasterTableView>
                        <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-ForeColor="White">
                                <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ConvertDate" AllowSorting="true" HeaderText="تاریخ دریافت فایل Excell" HeaderStyle-ForeColor="White"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="stcode" AllowSorting="false" HeaderText="شماره دانشجویی" HeaderStyle-ForeColor="White"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowSorting="false" HeaderStyle-ForeColor="White"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowSorting="false" HeaderStyle-ForeColor="White"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="rsh"  AllowSorting="True" HeaderText="رشته" HeaderStyle-ForeColor="White"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="sal_vorod" HeaderText="سال ورود" AllowSorting="false" HeaderStyle-ForeColor="White"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

            <%-- <div class="row">
             <div class="col-md-12" style="padding:1%">
                 
                 <telerik:RadGrid ID="grd_view" runat="server" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="false" Skin="MyCustomSkin" AllowFilteringByColumn="true" OnNeedDataSource="grd_view_NeedDataSource" >
                     <MasterTableView>
                         <Columns>
                             <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="false"></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="false"></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="rsh" HeaderText="رشته"></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="sal_vorod" HeaderText="سال ورود"></telerik:GridBoundColumn>
                       
                         </Columns>
                     </MasterTableView>
                 </telerik:RadGrid>

             </div>
            </div>--%>
        </div>
    </div>
    <script type="text/javascript">

        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1');
        var objCal2 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal2');
    </script>

    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
