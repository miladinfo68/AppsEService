<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSRequestMaster.Master" AutoEventWireup="true" CodeBehind="EditPersonalImage.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.EditPersonalImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
      <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;
        }

        function CancelEdit() {
            GetRadWindow().close();
        }
    </script>
    <div class="col-md-12" style="text-align: center; font-family: Tahoma">
        <asp:Panel ID="Confirmpnl" runat="server" Visible="false">
            <div class="col-md-3" style="border: 1px solid #B90000; border-radius: 5px; padding-bottom: 8px;">
                <p style="color: #FF0000; font-weight: bold; font-family: Tahoma">آیا از تغییرات مطمئن هستید؟</p>
                <telerik:RadButton ID="btn_Conf" runat="server" Text="بله" Font-Names="Tahoma" OnClick="conf_Click" Skin="Windows7"></telerik:RadButton>
                <telerik:RadButton ID="btn_NotConf" runat="server" Text=" خیر " Font-Names="tahoma" OnClick="notConf_Click" Skin="Windows7"></telerik:RadButton>

            </div>
        </asp:Panel>
    </div>
    <div class="col-md-12">
        <div class="col-md-3">
            <telerik:RadListView ID="rdl_PersPic" runat="server" Visible="False">

                <ItemTemplate>
                    <div id="vlightbox2" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_New" Text="اطلاعات جدید" runat="server" Font-Bold="True"></asp:Label>
                            <br />

                            <span style="font-weight: bold; font-size: x-small; color: #000000;">
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lbl_PersonalImg" Text="عکس پرسنلی:" Font-Bold="true" runat="server" Visible="true"></asp:Label>
                            <telerik:RadBinaryImage ID="img_PersonalPic" runat="server" Height="150px" ResizeMode="Fit" Width="124px" Visible="true" />
                            <asp:RadioButton ID="rdb_ConfPic" runat="server" Checked="true" Text="قابل قبول" GroupName="c1" Width="100%" ForeColor="#000000" AutoPostBack="True" />
                            <asp:RadioButton ID="rdb_FailedPic" runat="server" Text="رد" Width="100%" GroupName="c1" ForeColor="Red" AutoPostBack="True" />
                            <asp:TextBox ID="txt_RadPic" runat="server" Visible="false" />




                        </div>
                    </div>
                </ItemTemplate>
            </telerik:RadListView>
            <br />
        </div>

        <div class="col-md-3">

            <br />
        </div>




        <div class="col-md-3">
            <telerik:RadListView ID="rdlv_previous" runat="server">

                <ItemTemplate>
                    <div id="vlightbox1" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_Prev" Text="اطلاعات قبلی" runat="server" Font-Bold="True"></asp:Label>
                            <br />
                            <asp:Label ID="lbl_PersonalImgPrev" Text="عکس پرسنلی" runat="server" Visible="false"></asp:Label>
                            <telerik:RadBinaryImage ID="img_PersonalPicPrev" runat="server" Height="150px" ResizeMode="Fit" Width="124px" Visible="false" />
                            <br />

                            <asp:Label ID="lbl_Stcode" runat="server" Visible="true" Text="شماره دانشجویی:"></asp:Label>
                            <asp:Label ID="lbl_StcodePrev" runat="server" Visible="true" Text='<%#Eval("stcode")%>'></asp:Label>
                            <br />
                            <asp:Label ID="lbl_name" runat="server" Visible="true" Text="نام و نام خانوادگی:"></asp:Label>
                            <asp:Label ID="lbl_NamePrev" runat="server" Visible="true" Text='<%#Eval("FullName")%>'></asp:Label>
                </ItemTemplate>
            </telerik:RadListView>
        </div>

    </div>
    <asp:Button ID="btn_Taeid" runat="server" OnClick="btn_taeid_Click" CssClass="btn btn-info" Text="اعمال تغییرات" />
</asp:Content>
