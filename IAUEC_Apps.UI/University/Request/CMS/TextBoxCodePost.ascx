<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxCodePost.ascx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.TextBoxCodePost" %>
<style type="text/css">
    .auto-style1 {
        width: 100%;
        float: right;
    }
</style>


<telerik:RadWindowManager ID="rwm_Validations" runat="server">
</telerik:RadWindowManager>
<asp:Panel ID="pnl_mail" runat="server" DefaultButton="btn_Anjam">
    <table style="text-align: center" class="table-responsive">
        <tr>
            <td>
                <asp:Label ID="lbl_CodeRahgiri" runat="server" Text="کد پیگیری پست:" Width="106%" Style="text-align: right"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:TextBox ID="txt_CodePost" runat="server" Style="text-align: right; float: right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Button ID="btn_Anjam" runat="server" Text="ثبت" CssClass="btn btn-info" OnClick="btn_anjam_Click" />
            </td>
        </tr>
        <tr>
        </tr>
    </table>
</asp:Panel>


