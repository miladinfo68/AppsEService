<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutPajoheshFinal.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutPajoheshFinal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .tbl {
            max-width: 1059px;
        }

        .tright {
            text-align: right;
        }

        .cssPager span {
            background-color: #cccccc !important;
            color: black !important;
            font-family: 'B Yekan' !important;
            font-size: 24px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        .cssPager a {
            background-color: #ffffff !important;
            color: #808080 !important;
            font-family: 'B Yekan' !important;
            font-size: 22px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        a.pcalBtn{
            vertical-align:middle !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>لیست دانشجویانی که در وضعیت نهایی قرار دارند</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" container" dir="rtl">
        <div class="form-inline">
            <div class="form-group">
                <asp:Label ID="Label1" Text="از تاریخ :" runat="server" />
                <asp:TextBox ID="txtStartDate" CssClass="form-control form-inline" runat="server" />
            </div>
            <span>-</span>
            <div class=" form-group">
                <asp:Label ID="Label2" Text="تا تاریخ :" runat="server" />
                <asp:TextBox ID="txtEndDate" CssClass="form-control form-inline" runat="server" />
            </div>
            <asp:Button ID="btnShowList" Text="نمایش" runat="server" CssClass="btn btn-primary" OnClick="btnShowList_Click" />
        </div>
        <div id="dvGridHolder" runat="server" class="table-responsive" visible="false">
            <asp:GridView ID="grdFinalStudentList" runat="server" OnRowDataBound="grdFinalStudentList_RowDataBound" AllowPaging="true" PageSize="50" OnPageIndexChanging="grdFinalStudentList_PageIndexChanging" CssClass="table table-bordered table-condensed table-striped tbl" AutoGenerateColumns="false" OnRowCommand="grdFinalStudentList_RowCommand">
                <HeaderStyle CssClass=" bg-blue text-center " />
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <asp:Label ID="Label3" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="شماره دانشجویی" DataField="StCode" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField HeaderText="تاریخ دفاع" DataField="Def_Date" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField HeaderText="نمره دفاع" DataField="Def_Point" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField HeaderText="تاریخ انصراف از مقاله" DataField="Date_Paper_Cancel" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField HeaderText="تاریخ دریافت مدارک مربوط به پذیرش مقاله" HeaderStyle-Wrap="true" DataField="Date_Recieve_Doc_Accept" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField HeaderText="تاریخ ارسال مدارک" DataField="Date_Send_Doc_Edu" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField HeaderText="نهایی شده" DataField="IsFinalize" HeaderStyle-CssClass="text-center" />
                    <asp:TemplateField HeaderText=" عملیات" HeaderStyle-CssClass="text-center" Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="btnSubmit" Text="ویرایش" runat="server" CommandName="submit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"stcode") %>' CssClass="btn btn-sm btn-info" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings NextPageText="بعدی" PreviousPageText="قبلی" FirstPageText="صفحه اول" LastPageText="صفحه آخر" Mode="Numeric" Position="Bottom" />
                <PagerStyle CssClass="cssPager" />
            </asp:GridView>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <%-- <telerik:RadWindow runat="server" ID="RadWindow2" VisibleOnPageLoad="false">
        <ContentTemplate>
            <div dir="rtl">
                <asp:Label ID="Label1" Text="تاریخ دریافت مدارک" runat="server" />
                <asp:TextBox ID="txtDatePaperCancel1" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" Text="تاریخ ارسال مدارک" runat="server"></asp:Label>
                <asp:TextBox ID="txtDateRecieveDocAccept1" runat="server"></asp:TextBox>
                <asp:Button ID="btnSubmitDates" runat="server" Text="ثبت" CssClass="btn btn-success" OnClick="btnSubmitDates_Click" />
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    --%>
    <script>
        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtStartDate', { extraInputID: 'ContentPlaceHolder1_txtStartDate', extraInputFormat: 'yyyy/mm/dd' });
        var objCal2 = new AMIB.persianCalendar('ContentPlaceHolder1_txtEndDate', { extraInputID: 'ContentPlaceHolder1_txtEndDate', extraInputFormat: 'yyyy/mm/dd' });
        //var objCal3 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_RadWindow2_C_txtDatePaperCancel1', { extraInputID: 'ctl00_ContentPlaceHolder1_RadWindow2_C_txtDatePaperCancel1', extraInputFormat: 'yyyy/mm/dd' });
        //var objCal4 = new AMIB.persianCalendar('ctl00_ContentPlaceHolder1_RadWindow2_C_txtDateRecieveDocAccept1', { extraInputID: 'ctl00_ContentPlaceHolder1_RadWindow2_C_txtDateRecieveDocAccept1', extraInputFormat: 'yyyy/mm/dd' });

    </script>
</asp:Content>
