<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="AddMessage.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.AddMessage" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-responsive" style="width:100%">
        <tr>
            <td >وضعیت:</td>
            <td >
                <asp:DropDownList ID="ddl_Status" runat="server">
                    <asp:ListItem Value="1">ارسال درخواست</asp:ListItem>
                    <asp:ListItem Value="2">رد درخواست</asp:ListItem>
                    <asp:ListItem Value="3">تایید درخواست</asp:ListItem>
                    <asp:ListItem Value="4">فعال کردن پست الکترونیک</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">متن پست الکترونیک:</td>
            <td>
                <telerik:RadEditor ID="Emailtxt" Runat="server" ContentAreaCssFile="~/css/EditorContentArea_RTL.css" Height="300px" Width="500px">
             <Tools>
                 <telerik:EditorToolGroup>
					<telerik:EditorTool Name="Bold" />
					<telerik:EditorTool Name="Italic" />
					<telerik:EditorTool Name="Underline" />
					<telerik:EditorSeparator />
					<telerik:EditorTool Name="ForeColor" />
					<telerik:EditorTool Name="BackColor" />
					<telerik:EditorSeparator />
					<telerik:EditorTool Name="FontName" />
					<telerik:EditorTool Name="RealFontSize" />
                    <telerik:EditorSeparator />
                    <telerik:EditorTool name="JustifyLeft" />
                    <telerik:EditorTool name="JustifyCenter" />
                    <telerik:EditorTool name="JustifyRight" />
                    <telerik:EditorTool name="JustifyFull" />
                    <telerik:EditorTool name="JustifyNone" />
				</telerik:EditorToolGroup>
             </Tools>
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">متن پیامک:</td>
            <td>
                <asp:TextBox ID="txt_Sms" runat="server" TextMode="MultiLine" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td>
              
                <asp:Button ID="Btn_Reg" CssClass="Orange"  runat="server" Text="درج" OnClick="BtnReg_Click" />
                <uc1:AccessControl ID="AccessControl1" runat="server" />
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Metro">
                </telerik:RadWindowManager>
            </td>
        </tr>
    </table>
</asp:Content>
