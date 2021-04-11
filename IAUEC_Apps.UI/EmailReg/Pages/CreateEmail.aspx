<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/PageEmailMaster.Master" AutoEventWireup="true" CodeBehind="CreateEmail.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.Pages.CreateEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <script src="../js/jquery1.js"></script>
    <script src="../js/jquery2.js"></script>
    <link href="../css/Style1.css" rel="stylesheet" />
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
    </telerik:RadWindowManager>
     <script type="text/javascript">
         $(document).ready(function () {

             $("[name='txt_Pass']").focusin(function () {
                 $("[name='txt_Pass']").tooltip('show');
             });
             //$("[name='txt_Name']").focusin(function () {
             //    $("[name='txt_Name']").tooltip('show');
             //});

         });
    </script>
     <script>
         function CallBackConfirmok(arg) {
             if (arg)
                 window.location.href = "EmailRequestStatus.aspx";
         }
         function CallBackConfirm(arg) {
             if (arg)
                 window.location.href = "Login.aspx";
         }
         function CallBackConfirm1(arg) {
             if (arg)
                 window.location.href = "CreateEmail.aspx";
         }
    </script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
                var text1 = $find("<%=txt_Pass.ClientID %>").get_textBoxValue();
                var text2 = $find("<%=txt_Rpass.ClientID %>").get_textBoxValue();

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
      <%--  <script type="text/javascript">
            $().ready(function () {
                $("input,textarea").each(function () {
                    $(this).attr("lang", "en");
                });
            });
  </script>--%>
    </telerik:RadCodeBlock>
    <asp:Label CssClass="UserControlDiv" ID="lbl_Student" runat="server"  Visible="false"  style="color:red"/> 
    <div class="col-md-2"></div>  
    <div class="col-md-8"  style="background-color: rgba(255, 255, 255,0.5);padding:2%;box-shadow:10px 10px 10px 10px rgba(128, 128, 128,0.1); margin-top: 15px;">
        <table class="table-responsive" style="width:100%">
        <tr>
            <td dir="ltr" style="text-align: right; width:15%">نام(لاتین):</td>
             <td dir="ltr" style="text-align: right;width:30%">
              <%--   <input type="text" runat="server" name="name2" lang="en"/>  --%>
                               <asp:TextBox ID="txt_Name" data-original-title="از حروف انگلیسی استفاده نمایید" runat="server"  onKeypress="if (event.keyCode < 65 || (event.keyCode > 91 && event.keyCode <97) || event.keyCode>122) event.returnValue = false;"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfv_name" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txt_Email" ErrorMessage="لطفا نام خود را وارد نمایید !"> </asp:RequiredFieldValidator>
            </td>
            <td dir="ltr" style="text-align: right; width:15%">نام خانوادگی(لاتین):</td>
            <td dir="ltr" style="text-align: right;width:30%">
                <asp:TextBox ID="txt_Family" runat="server" data-original-title="از حروف انگلیسی استفاده نمایید"  onKeypress="if (event.keyCode < 65 || (event.keyCode > 91 && event.keyCode <97) || event.keyCode>122) event.returnValue = false;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_Family" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txt_Email" ErrorMessage="لطفا نام خانوادگی خود را وارد نمایید !"> </asp:RequiredFieldValidator>
            </td>
            <td dir="rtl" style="text-align: left">&nbsp;</td>
        </tr>
        <tr>
            <td dir="rtl" style="text-align: left">پست الکترونیکی :</td>
            <td style="text-align: right" colspan="4">iauec.ac.ir @ <telerik:RadTextBox ID="txt_Email" runat="server" Width="185px" onKeypress="if ( (event.keyCode > 91 && event.keyCode <94 ) || event.keyCode>122 ) event.returnValue = false;"  ></telerik:RadTextBox> 
                 <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red" 
                        ControlToValidate="txt_Email" ErrorMessage="لطفا پست الکترونیکی خود را وارد نمایید !"> </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_Email" ID="RegularExpressionValidator1" ValidationExpression = "^[\s\S]{0,20}$" runat="server" ErrorMessage="ماکزیمم طول پست الکترونیکی 20 کاراکتر می باشد"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td dir="rtl" style="text-align: left">کلمه عبور :</td>
            <td style="text-align: right" colspan="4"> <telerik:RadTextBox runat="server" ID="txt_Pass" TextMode="Password" EnableSingleInputRendering="false" Height="20px" data-original-title="حداقل 8 کاراکتر وارد نمایید" ForeColor="Black" Width="270px" >
                        <PasswordStrengthSettings ShowIndicator="true"></PasswordStrengthSettings>
                    </telerik:RadTextBox></td>
        </tr>
        <tr>
            <td dir="rtl" style="text-align: left">تکرار کلمه عبور :</td>
            <td style="text-align: right" colspan="4">  <telerik:RadTextBox ID="txt_Rpass" Width="270px" LabelWidth="50%" runat="server" 
                        TextMode="Password" onkeyup="checkPasswordMatch()" EnableSingleInputRendering="false" Height="20px">
                        <ClientEvents></ClientEvents>
                    </telerik:RadTextBox> 
                <span id="CustomIndicator" style="text-align: right">&nbsp;</span> 
                <span id="PasswordRepeatedIndicator" class="Base L0"> &nbsp;</span>
            </td>
        </tr>
        <tr>
            <td dir="rtl">&nbsp;</td>
            <td colspan="4">  &nbsp;</td>
        </tr>
            <tr>
                <td colspan="5"><asp:Label ID="lbl_Note" runat="server" Text="جهت اطلاع از نتیجه درخواست ایجاد پست الکترونیکی، یکی از موارد زیر را انتخاب نمایید" Font-Bold="True" /></td>
            </tr>
    
      <tr>
          <td colspan="5">
              <table class="table-responsive" style="padding: 3%; border: 1px solid #9D9D9D; width:100%; border-radius:5px; margin-top: 10px;">
                    <tr>
            <td dir="rtl" style="text-align: left">نوع ارتباط ما با شما : </td>
            <td style="text-align: right"> <asp:RadioButtonList id="btn_SelectType" runat="server" >
                        <asp:ListItem Selected="True" Value="0">پست الکترونیکی</asp:ListItem>
                        <asp:ListItem Value="1">پیامک</asp:ListItem>
                        <asp:ListItem Value="2">پیامک و پست الکترونیکی</asp:ListItem>                     
                    </asp:RadioButtonList></td>
        </tr>
                <tr>
            
            <td dir="rtl" style="text-align: left">پست الکترونیکی دوم : </td>
            <td style="text-align: right"> <telerik:RadTextBox ID="txt_SEmail" runat="server" Width="265px" ></telerik:RadTextBox>
                 <asp:RegularExpressionValidator ID="rev1" runat="server" Display="Dynamic" ForeColor="Red"
                        ErrorMessage=" لطفا پست الکترونیکی را بصورت صحیح وارد نمایید  student@gmail.com" 
                        ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                        ControlToValidate="txt_SEmail"> </asp:RegularExpressionValidator>
                <%--  <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txt_SEmail" ErrorMessage="لطفا پست الکترونیکی خود را وارد نمایید !">
                    </asp:RequiredFieldValidator> --%>
            </td>
        </tr>
        <tr>
            <td dir="rtl" colspan="2" style="text-align: right; padding-right: 2%;"><asp:CheckBox id="chk_Email" runat="server"
                        AutoPostBack="false"                        
                        TextAlign="Right" Text=" پست الکترونیکی جایگزین پست الکترونیکی شما در سامانه خدمات الکترونیکی گردد"/></td>
        </tr>
        <tr>
            <td dir="rtl" style="text-align: left"> تلفن همراه : </td>
            <td style="text-align: right">  <telerik:RadTextBox ID="txt_Mobile" runat="server" Width="265px" ></telerik:RadTextBox>                
                   <%-- <asp:RegularExpressionValidator ID="rev2" runat="server" Display="Dynamic" ForeColor="Red"
                        ErrorMessage=" لطفا شماره موبایل خود را بصورت عددی و 11 رقم وارد نمایید" 
                        validationexpression="^[0-9]{11}$"
                        ControlToValidate="txt_Mobile"> </asp:RegularExpressionValidator> --%>

            </td>
        </tr>
        <tr>
            <td dir="rtl" colspan="2" style="text-align: right; padding-right: 2%;">   <asp:CheckBox id="chk_Mobile" runat="server"
                        AutoPostBack="false"
                        TextAlign="Right" Text=" تلفن همراه، جایگزین تلفن همراه شما در سامانه خدمات الکترونیکی گردد"/></td>
          
        </tr>
                  </table>
          </td>
      </tr>
    </table>
          
        <div class="col-md-12" style="margin-top: 20px">       
            <asp:Button ID="btn_CreateEmail" runat="server" OnClick="btn_CreateEmail_Click" Text="ثبت درخواست" CssClass="Orange" />
        </div> 	  
    </div>  
    <div class="col-md-2"></div>  
  
  
</asp:Content>
