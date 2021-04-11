<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/PageEmailMaster.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.Pages.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
  <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
     <script type="text/javascript">
       $(document).ready(function () {

           $("[name='txt_NewPass']").focusin(function () {
               $("[name='txt_NewPass']").tooltip('show');
           });
           $("[name='txt_NewPassRepeat']").focusin(function () {
               $("[name='txt_NewPassRepeat']").tooltip('show');
           });

       });
    </script>
     <script>
         function CallBackConfirmok(arg) {
             if (arg)
                 window.location.href = "../../CommonUI/IntroPage.aspx";
         }
         </script>
      <script type="text/javascript">
         function CalculatingStrength(sender, args) {
             if (args.get_passwordText() == "Enter Password") {
                 //Manually set strength Score depending on the input text.
                 args.set_indicatorText("Custom text");
                 args.set_strengthScore(0);
             }
             else {
                 var calculatedScore = args.get_strengthScore();
                 //Changing the indicator text depending on the calculated score.
                 args.set_indicatorText("Score: (" + calculatedScore + "/100)");
             }
         }
         function checkPasswordMatch() {
             var text1 = $find("<%=txt_NewPass.ClientID %>").get_textBoxValue();
                var text2 = $find("<%=txt_NewPassRepeat.ClientID %>").get_textBoxValue();

                if (text2 == "") {
                    $get("PasswordRepeatedIndicator").innerHTML = "";
                    $get("PasswordRepeatedIndicator").className = "Base L0";
                }
                else if (text1 == text2) {
                    $get("PasswordRepeatedIndicator").innerHTML = "";
                    $get("PasswordRepeatedIndicator").className = "Base L5";
                }
                else {
                    $get("PasswordRepeatedIndicator").innerHTML = "رمز عبور تطابق ندارد";
                    $get("PasswordRepeatedIndicator").className = "Base L1";
                }
            }

        </script>
      </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
     بازیابی رمز عبور پست الکترونیکی
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <div runat="server" id="div_Reset" visible="false">
       <p style="text-align:right;direction:rtl;font-family:Tahoma;border:groove; color: #FF0000;">تذکر: طول کارکترهای رمز عبور حداقل 8 کارکتر باشد، همچنین جهت افزایش امنیت از کارکترهایی نظیر(@و$) در رمز عبور استفاده نمایید </p>
        <br />
        <table style="width:50%;font-family:Tahoma;">
             <tr style="direction:rtl;">
                <td style="width:40%; color: #000000;">کدامنیتی ارسال شده:</td>
                <td ><telerik:RadTextBox runat="server" ID="txt_SecurityCode"  EnableSingleInputRendering="false" Height="20px"  ForeColor="Black" Width="40%" >
                            </telerik:RadTextBox>
                   
                    </td>
            </tr>
            <tr>
                <td>

                </td>
            </tr>
            <tr>
                <td style="width:40%; color: #000000;">رمز عبور جدید:</td>
                <td><telerik:RadTextBox runat="server" ID="txt_NewPass" TextMode="Password" EnableSingleInputRendering="false" Height="20px"  ForeColor="Black" Width="40%" >
                        <%--<PasswordStrengthSettings ShowIndicator="true"></PasswordStrengthSettings>--%>
                    </telerik:RadTextBox>
                   
                </td>
            </tr>
            <tr>
                <td>

                </td>
            </tr>
            <tr>
                  <td style="width:40%; color: #000000; height: 20px;">تکرار رمز عبور جدید:</td>
                <td class="toolbar-btn"> <telerik:RadTextBox runat="server" ID="txt_NewPassRepeat" TextMode="Password" Height="20px" EnableSingleInputRendering="false"   ForeColor="Black" Width="40%" >
                        <%--<PasswordStrengthSettings ShowIndicator="true"></PasswordStrengthSettings>--%>
                    </telerik:RadTextBox>
                </td>

            </tr>
            <tr>
                <td>

                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                <asp:Button ID="btn_Save" runat="server" Text="ثبت" CssClass="Orange" OnClick="btn_Save_Click" />
                    </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="div_NationalCode" visible="true">
    <table>
        <tr>
            <td style="color: #000000;">کدملی خود را وارد نمایید:</td>
            <td>
                <asp:TextBox ID="txt_NationalCode" runat="server"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td>
                <asp:Button ID="btn_SaveNationalCode" runat="server" CssClass="Orange" Text="ثبت" OnClick="btn_SaveNationalCode_Click" />
            </td>
        </tr>
        
    </table>
    <div style="direction:rtl;text-anchor:middle;">
         <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Visible="false" Text="دانشجو گرامی شماره همراه شما در سامانه موجود نمی باشد، لطفا در بخش  "></asp:Label>
        <asp:HyperLink ID="lnk_Edit" runat="server" NavigateUrl="~/University/Request/Pages/EditPersonalInformationUI.aspx"  Visible="False" ForeColor="#0033CC" Font-Overline="False" Font-Underline="True">ویرایش اطلاعات فردی </asp:HyperLink>
        <asp:Label ID="lbl_Message2" runat="server" ForeColor="Red" Visible="false" Text="آن را وارد نموده و پس از تایید آن توسط کارشناس مربوطه، درخواست تغییر رمز خود را ارسال نمایید"></asp:Label>
        <%--<asp:Label ID="lbl_Resault" runat="server" Visible="false" ></asp:Label>
        <asp:Label ID="lbl_Status" runat="server" Text="Label" Visible="false"></asp:Label>--%>
    </div>
    </div>
    <div id="div_lnkReqSecurityCode" runat="server" visible="false">
        <asp:LinkButton ID="lnk_ReqSeqCode" Text="درخواست ارسال مجدد کد امنیتی" runat="server" OnClick="lnk_ReqSeqCode_Click">درخواست ارسال مجدد کد امنیتی</asp:LinkButton>
    </div>
</asp:Content>
