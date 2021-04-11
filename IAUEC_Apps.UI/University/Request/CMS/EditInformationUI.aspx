<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSEditInfoRequest.Master" AutoEventWireup="true" CodeBehind="EditInformationUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.EditInformationUI" %>

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
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "ConfirmEditPersonalInformationUI.aspx"
        }
    </script>
    <div class="col-md-12" style="text-align: center; font-family: Tahoma">
        <asp:Panel ID="Confirmpnl" runat="server" Visible="false">
            <div class="col-md-3" style="border: 1px solid #B90000; border-radius: 5px; padding-bottom: 8px;">
                <p style="color: #FF0000; font-weight: bold; font-family: Tahoma">آیا از تغییرات مطمئن هستید؟</p>
                <telerik:RadButton ID="btn_Conf" runat="server" Text="بله" Font-Names="Tahoma" OnClick="conf_Click" Skin="Windows7"></telerik:RadButton>
                <telerik:RadButton ID="btn_NotConf" runat="server" Text="خیر" Font-Names="tahoma" OnClick="btn_NotConf_Click" Skin="Windows7"></telerik:RadButton>

            </div>
        </asp:Panel>
    </div>



    <telerik:RadWindowManager ID="rwm_message" runat="server"></telerik:RadWindowManager>
    <div class="col-md-12">
        <div class="col-md-5">
            <telerik:RadListView ID="rdlv_New" runat="server">

                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                </LayoutTemplate>

                <ItemTemplate>
                    <div id="vlightbox3" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center; direction:rtl;">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_New" Text="اطلاعات جدید" runat="server" Font-Bold="True"></asp:Label>
                            <br />


                        </div>

                        <br />
                        <asp:Label ID="lbl_EditeTypeName" runat="server" Font-Bold="true" Visible="true" Text='<%#Eval("EditeTypeName")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_EditeTypeName1" runat="server" Font-Bold="true" Visible="false" Text=""></asp:Label>
                        <asp:Label ID="lbl_NewContent" runat="server" Visible="true"></asp:Label>
                        <asp:DropDownList ID="ddlCity" runat="server" Visible="false"></asp:DropDownList>
                        <asp:HiddenField ID="hdf_NewContent" runat="server" Value='<%#Eval("NewContent")%>' />
                        <asp:HiddenField ID="hdnStudentRequestID" runat="server" Value='<%#Eval("StudentRequestID")%>' />
                        <asp:HiddenField ID="hdnEditPersonalInformation" runat="server" Value='<%#Eval("EditePersonalInformationID")%>' />
                        <br />


                        <asp:Label ID="lbl_EditedID" runat="server" Visible="false" Text='<%#Eval("EditedID")%>'></asp:Label>
                        <asp:RadioButton ID="rdb_ConfNewInfo" runat="server" Checked="true" Text=" قابل قبول" GroupName="c3" Width="100%" ForeColor="#000000" AutoPostBack="True" />
                        <asp:RadioButton ID="rdb_NewInfo" runat="server" Text="رد" Width="100%" GroupName="c3" ForeColor="Red" AutoPostBack="True" />
                        <asp:TextBox ID="txt_RadEdit" runat="server" Visible="false" />



                    </div>
                </ItemTemplate>
            </telerik:RadListView>
            <br />
        </div>




        <div class="col-md-5">
            <telerik:RadListView ID="rdlv_previous" runat="server">
                <ItemTemplate>
                    <div id="vlightbox1" class="col-md-12" style="margin-bottom: 2px; margin-left: 0px; border: 1px solid rgba(33, 178, 187,0.6); padding: 2% 1% 2% 2%; box-shadow: rgba(33, 178, 187,0.2) 5px 5px inset; border-radius: 5px; text-align: center; direction:rtl;">
                        <div class="col-md-12" style="background-color: rgba(126, 208, 223,0.5); margin-bottom: 2px">
                            <asp:Label ID="lbl_Prev" Text="اطلاعات قبلی" runat="server" Font-Bold="True"></asp:Label>
                            <br />
                        </div>
                        <asp:Label ID="lbl_PersonalImgPrev" Text="عکس پرسنلی" runat="server" Visible="false"></asp:Label>
                        <telerik:RadBinaryImage ID="img_PersonalPicPrev" runat="server" Height="150px" ResizeMode="Fit" Width="124px" Visible="false" />
                        <br />
                        <asp:Label ID="lbl_Tell" runat="server" Visible="true" Text="شماره تلفن:"></asp:Label>
                        <asp:Label ID="lbl_TellPrev" runat="server" Visible="true" Text='<%#Eval("homePhone")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_moblbl" runat="server" Visible="true" Text="شماره همراه:"></asp:Label>
                        <asp:Label ID="lbl_MobilePrev" runat="server" Visible="true" Text='<%#Eval("mobile") %>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_CodePosti" runat="server" Visible="true" Text="کدپستی:"></asp:Label>
                        <asp:Label ID="lbl_CodePostiPrev" runat="server" Visible="true" Text='<%#Eval("home_postalCode")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_OstanPrev" runat="server" Visible="true" Text="استان:"></asp:Label>
                        <asp:Label ID="lbl_Ostan" runat="server" Visible="true" Text='<%#Eval("home_state_name")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_ShahrestanPrev" runat="server" Visible="true" Text="شهرستان:"></asp:Label>
                        <asp:Label ID="lbl_Shahrestan" runat="server" Visible="true" Text='<%# string.IsNullOrEmpty(Eval("home_city_name").ToString()) ? "---" : Eval("home_city_name")%>'></asp:Label>
                        <br />
                        <asp:Label ID="lbl_Address" runat="server" Visible="true" Text="آدرس:"></asp:Label>
                        <asp:Label ID="lbl_AddressPrev" runat="server" Visible="true" Text='<%#Eval("homeAddress")%>'></asp:Label>
                </ItemTemplate>
            </telerik:RadListView>
        </div>
    </div>


    <div class="col-md-12">
        <asp:Button ID="btn_Taeid" runat="server" CssClass="btn btn-info" OnClick="btn_taeid_Click" Text="اعمال تغییرات" /></div>

</asp:Content>
