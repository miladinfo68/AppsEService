<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/PageRequestMaster.Master" AutoEventWireup="true" CodeBehind="RequestGovahiUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.RequestGovahi" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style>
        .mmargin {
            margin-top: 1.5%;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function postRefId(refIdValue) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
        function RedirectToGovahiVaziatUI() {
            
            var currentLocation = window.location.href;
            var nextLocation = currentLocation.replace('RequestGovahiUI', 'GovahiVaziatUI');
            window.location = nextLocation;
        }

        function walletPaymentCallback() {
            window.location.href = window.location.href;
        }
    </script>

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadWindowManager ID="rwm_Validations" runat="server">
    </telerik:RadWindowManager>

    <h3>درخواست ارسال گواهی اشتغال به تحصیل از طریق پست</h3>
    <br />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "GovahiVaziatUI.aspx";
        }
    </script>
    <script>
        function CallBackConfirm1(arg) {
            if (arg)
                window.location.href = "EditPersonalInformationUI.aspx";
        }
    </script>
    <script>
        function CallBackConfirm2(arg) {
            if (arg)
                window.location.href = "../../../CommonUI/IntroPage.aspx";
        }
    </script>
    <div style="width: 100%">
    </div>

    <div style="width: 100%; padding-left: 2%; padding-right: 2%">
        <telerik:RadGrid ID="grd_AcceptEdit" runat="server" AutoGenerateColumns="false" OnItemCommand="grd_AcceptEdit_ItemCommand" OnNeedDataSource="grd_AcceptEdit_NeedDataSource" BackColor="#5BC0DE">
            <MasterTableView>
                <ItemStyle Font-Names="tahoma" />
                <HeaderStyle Font-Names="tahoma" />
                <AlternatingItemStyle Font-Names="tahoma" />
                <Columns>


                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="mahal" HeaderText="ارائه به" AllowFiltering="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate></HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                        <ItemTemplate>
                            <asp:Button ID="btn_Pay" Text="حذف" runat="server" CommandArgument='<%# Container.ItemIndex %>' CommandName="del"></asp:Button>

                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>

        </telerik:RadGrid>
    </div>
    <div style="width: 100%; padding-left: 2%; padding-right: 2%">
        <asp:Panel ID="pnl_main" runat="server" Direction="RightToLeft" HorizontalAlign="Center" BorderStyle="Groove">
            <table id="tbl_Main" runat="server" style="direction: rtl; text-align: right;">
                <tr id="tr_msg" runat="server" visible="false">
                    <td colspan="6" style="color: #FF0000">&nbsp;&nbsp; &nbsp;* چنانچه متقاضی دریافت بیش از یک گواهی اشتغال به تحصیل&nbsp; می باشید، به ترتیب نام هر محل را در کادر زیر وارد نموده و کلید
               <asp:Image ID="Image1" runat="server" ImageUrl="~/University/Request/Files/plus.png" />
                        &nbsp;را بزنید. نام محل های وارد شده در جدول بالا نمایش داده می شود. در صورت تمایل می توانید محل مورد نظر را حذف نمایید.</td>
                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_JahateErae" runat="server" Text="*جهت ارائه به:"></asp:Label>
                    </td>
                    <td colspan="6" style="vertical-align: middle;">

                        <asp:TextBox ID="txt_JahateErae" runat="server" TextMode="MultiLine" Width="295px"></asp:TextBox>
                        &nbsp;
                <asp:ImageButton ID="img_btn" runat="server" Visible="false" ImageUrl="~/University/Request/Files/plus.png" Width="50px" OnClick="img_btn_Click" Height="50px" ImageAlign="Bottom" CssClass="mmargin" />

                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Name" runat="server" Text="نام:" Font-Bold="True"></asp:Label>

                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_NamePrev" runat="server"></asp:Label>

                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Family" runat="server" Text="نام خانوادگی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_FamilyPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_ShomareDaneshju" runat="server" Text="شماره دانشجویی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_ShomareDaneshjuPrev" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_SalVorud" runat="server" Text="سال ورود:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_SalVorudPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Vorudi" runat="server" Text="ورودی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_VorudiPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Maghta" runat="server" Text="مقطع:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_MaghtaPrev" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;&nbsp;&nbsp; &nbsp;</td>

                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_ReshteTahsili" runat="server" Text="رشته تحصیلی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_ReshteTahsiliPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Gerayesh" runat="server" Text="گرایش:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_GerayeshPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Posti" runat="server" Text="کدپستی یا صندوق پستی:" Font-Bold="True"></asp:Label>

                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_PostiPrev" runat="server"></asp:Label>

                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Phone" runat="server" Text="تلفن:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_PhonePrev" runat="server"></asp:Label>
                    </td>

                    <td style="width: 5%">
                        <asp:Label ID="lbl_Mobile" runat="server" Text="موبایل:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_MobilePrev" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;&nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_AddressPosti" runat="server" Text="آدرس کامل پستی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lbl_AddressPrev" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>

                    <td style="width: 5%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>

    </div>

    <br />
    <div style="text-align: center; margin-bottom: 10px">
        <asp:Button ID="btn_Taeid" runat="server" CssClass="btn btn-info" Text="تایید" Width="110px" OnClick="btn_taeid_Click" />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
