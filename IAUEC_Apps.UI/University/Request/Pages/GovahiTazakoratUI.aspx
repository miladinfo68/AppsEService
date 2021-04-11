<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/PageRequestMaster.Master" AutoEventWireup="true" CodeBehind="GovahiTazakoratUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.GovahiTazakoratUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    درخواست ارسال گواهی اشتغال به تحصیل از طریق پست
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="rwm_Validations" runat="server">
    </telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-color: rgba(255, 255, 255,0.5); padding: 1%; box-shadow: 2px 2px 2px 2px rgba(128, 128, 128,0.1); margin-top: 5px;">
        <asp:Panel runat="server" ID="pnl_main" BorderStyle="Groove" HorizontalAlign="Justify">

            <div>
                <b>* 
                    <asp:Label ID="lbl_Tazakor" runat="server" Text="با توجه به اینکه صدور گواهی اشتغال به تحصیل، تنها از طریق ارسال پستی امکان پذیر می باشد لذا قبل از ارسال این تقاضا حتما نسبت به "></asp:Label></b>
                <asp:HyperLink ID="hl_Edit" runat="server" ForeColor="Blue" NavigateUrl="~/University/Request/Pages/EditPersonalInformationUI.aspx">اصلاح و ویرایش آدرس و کد پستی </asp:HyperLink>
                <b>
                    <asp:Label ID="Label1" runat="server" Text=" اقدام نمایید"></asp:Label>.<br />
                    * دانشجویان گرامی توجه داشته باشید:<br />
                    &nbsp;-درخواست گواهی اشتغال به تحصیل در راستای اخذ مجوز خروج از کشور فقط به صورت حضوری امکانپذیر می باشد.<br />
                    &nbsp;-همچنین درخواست گواهی اشتغال به تحصیل برای سفارت فقط به صورت حضوری امکانپذیر می باشد.</b>
                <br />
                <asp:Label ID="lbl_Tazakor2" Visible="true" runat="server" Text="*هزینه صدور و ارسال پستی برای یک گواهی اشتغال به تحصیل در سال  1397 مبلغ 140،000 ریال می باشد و به ازای صدور هر گواهی اشتغال به تحصیل دیگر که به طور همزمان درخواست داده شود، مبلغ 20،000 ریال اخذ می گردد."></asp:Label>
                <br />

            </div>
            <br />

        </asp:Panel>
    </div>
    &nbsp;<asp:Button ID="btn_Taeid" CssClass="btn btn-info" runat="server" Text="تایید و ادامه" OnClick="btn_taeid_Click" />
    &nbsp;

&nbsp; 
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
