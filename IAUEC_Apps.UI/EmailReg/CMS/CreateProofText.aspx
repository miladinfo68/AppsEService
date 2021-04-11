<%@ Page Title="" Language="C#" MasterPageFile="~/EmailReg/MasterPages/CMSEmailMaster.Master" AutoEventWireup="true" CodeBehind="CreateProofText.aspx.cs" Inherits="IAUEC_Apps.UI.EmailReg.CMS.CreateProofText" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
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
                  window.location.href = "CreateProofText.aspx";
          }
    </script>
    <div>
        <label>متن دلیل</label>
        <asp:TextBox ID="txt_ProofText" Text="" runat="server" Width="359px" ></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btn_Create" Text="ایجاد متن" runat="server" OnClick="btnCreate_Click" CssClass="Orange"/>
        <uc1:AccessControl ID="AccessControl1" runat="server" />
    </div>    
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Vista"></telerik:RadWindowManager>
</asp:Content>
