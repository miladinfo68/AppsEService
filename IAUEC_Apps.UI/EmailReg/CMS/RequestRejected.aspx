<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="RequestRejected.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.RequestRejected" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "RequestRejected_Act.aspx";
        }
    </script>
  
    <div>
        <fieldset dir="rtl">           
            <table class="table-responsive" style="width:100%; margin-bottom: 2%;">
                <tr>
                    <td style="text-align:left">                        
                        شماره دانشجویی   
                    </td>  
                    <td style="text-align:right">           
                        <asp:Label ID="lblStcode" Visible="true" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        پست الکترونیکی
                    </td>  
                    <td style="text-align:right">           
                        <asp:Label ID="lblEmail" Visible="true" runat="server" /> 
                    </td>
                </tr>

                <tr>
                    <td style="text-align:left">
                        
                        <asp:RadioButton ID="rbtnDefualt" runat="server" AutoPostBack="True" Checked="True" GroupName="a" OnCheckedChanged="rbtnDefualt_CheckedChanged" Text="دلیل پیش فرض" />
                    </td>  
                    <td style="text-align:right">           
                        <asp:DropDownList runat="server" ID="Dpl_Text" Width="232px" ></asp:DropDownList>  
                    </td> 
                </tr>  
                <tr>
                    <td style="text-align:left">
                        <asp:RadioButton ID="rbtnSpecial" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="rbtnSpecial_CheckedChanged" Text="دلیل خاص" />
                        </td>                    
                    <td style="text-align:right"> 
                        <asp:TextBox ID="txtNote" runat="server" Text="" Height="92px" TextMode="MultiLine" Width="207px" /></td> 
                </tr>              
                <tr>
                    <td style="text-align:left">
                        &nbsp;</td>                    
                    <td style="text-align:right"> 
        <asp:Button ID="btnSend" runat="server" Text="ثبت و ارسال دلایل" OnClick="btnSend_Click" />
                            <asp:Button ID="Btn_Cancel" runat="server" Text="بازگشت" OnClick="btnCancel_Click" />
                    </td> 
                </tr>              
            </table>
        </fieldset>
    </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista">
        </telerik:RadWindowManager>
</asp:Content>
