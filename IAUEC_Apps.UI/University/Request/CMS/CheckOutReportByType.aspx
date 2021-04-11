<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutReportByType.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutReportByType" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <script src="../Content/js-persian-cal.min.js"></script>
    <link href="../Content/js-persian-cal.css" rel="stylesheet" />
    <style>
        .rcbItem
    {
        font-family: tahoma;
      
    }
    .rcbHovered
{
    font-family: Tahoma;
    font-weight:bold;
}
        .AlignGrd {
            text-align: center;
        }
    </style>
    <style>
        .grid td, .grid th {
            text-align: center;
        }

        .marginR {
            margin-right: 2px;
        }

        .spacing {
            margin-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div dir="rtl">
        <div class="container">
            <div class="row" style="margin: 10px;">
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ForeColor="Red" Text="*" ValidationGroup="submit" ErrorMessage="تاریخ شروع را انتخاب کنید" ControlToValidate="txtSdate"></asp:RequiredFieldValidator>
                    <asp:Label ID="label1" runat="server" Font-Names="b yekan">از تاریخ:</asp:Label><asp:TextBox ID="txtSdate" runat="server" CssClass="marginR" MaxLength="9"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtSdate.ClientID%>',
                            { extraInputID: '<%=txtSdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>
                </div>
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ForeColor="Red" Text="*" ErrorMessage="تاریخ پایان را انتخاب کنید" ValidationGroup="submit" ControlToValidate="txtEdate"></asp:RequiredFieldValidator>
                    <asp:Label ID="label2" runat="server" Font-Names="b yekan">تا تاریخ:</asp:Label><asp:TextBox ID="txtEdate" CssClass="marginR" runat="server"></asp:TextBox>
                    <script>
                        var objCal1 = new AMIB.persianCalendar('<%=txtEdate.ClientID%>',
                            { extraInputID: '<%=txtEdate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                    </script>
                </div>
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ForeColor="Red" Text="*" ErrorMessage="وضعیت تسویه را انتخاب کنید" ValidationGroup="submit" ControlToValidate="cmbStatus"></asp:RequiredFieldValidator>
                    <asp:Label ID="label3" runat="server" Font-Names="b yekan">وضعیت:</asp:Label>
                    <telerik:RadComboBox ID="cmbStatus" runat="server" CssClass="marginR" Font-Names="Tahoma" Font-Size="Small" EmptyMessage="--انتخاب کنید--" Skin="Vista">
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="غیرحضوری" />
                            <telerik:RadComboBoxItem Value="2" Text="حضوری" />
                            <telerik:RadComboBoxItem Value="3" Text="همه موارد" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
                <div class="col-md-3">
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" ForeColor="Red" ErrorMessage="نوع تسویه را انتخاب کنید" Text="*" ValidationGroup="submit" ControlToValidate="cmbType"></asp:RequiredFieldValidator>
                    <asp:Label ID="label4" runat="server" Font-Names="b yekan">نوع درخواست:</asp:Label>
                    <telerik:RadComboBox ID="cmbType" runat="server" CssClass="marginR" Font-Names="Tahoma" Font-Size="Small" EmptyMessage="--انتخاب کنید--" Skin="Vista">
                        <Items>
                            <telerik:RadComboBoxItem Value="1" Text="فارغ التحصیل" />
                            <telerik:RadComboBoxItem Value="2" Text="تغییر رشته" />
                            <telerik:RadComboBoxItem Value="3" Text="انصراف" />
                            <telerik:RadComboBoxItem Value="4" Text="اخراج" />
                            <telerik:RadComboBoxItem Value="5" Text="همه موارد" />
                        </Items>
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row" style="margin-top: 20px; margin-bottom: 20px">
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
               
                <div class="col-md-12">
                    <div class="table-responsive">
                        <telerik:RadGrid ID="grdResult" runat="server" MasterTableView-ShowHeadersWhenNoRecords="true"  AutoGenerateColumns="false" AllowPaging="true" PageSize="100" AllowFilteringByColumn="true" EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnNeedDataSource="grdResult_NeedDataSource" OnExcelMLWorkBookCreated="grdResult_ExcelMLWorkBookCreated">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                                        <ItemTemplate>
                                            <%# Container.ItemIndex + 1 %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="شماره درخواست" AllowFiltering="false" ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreateDate" HeaderText="تاریخ درخواست" AllowFiltering="false" ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="StCode" HeaderText="شماره دانشجویی" AllowFiltering="false"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="name" HeaderText="نام دانشجو" AllowFiltering="false"/>
                                    <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته" ></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="namedanesh" HeaderText="دانشکده" ></telerik:GridBoundColumn>        
                                    <telerik:GridBoundColumn DataField="Onlinestatus" HeaderText="وضعیت" UniqueName="status" AllowFiltering="false"/>                            
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
              
            </div>
        </div>
        
    </div>

    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
