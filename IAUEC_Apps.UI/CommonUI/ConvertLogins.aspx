<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertLogins.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.ConvertLogins" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="btnConvertStudent" Text="Convert Students" OnClick="btnConvert_Click" />
            <asp:Button runat="server" ID="btnConvertProfessors" Text="Convert Professors" OnClick="btnConvertProfessors_Click" />
            <div>
                <asp:TextBox runat="server" ID="txtInput"></asp:TextBox>
                <asp:Label runat="server" ID="lblOutput"></asp:Label>
                <asp:Button runat="server" ID="btnManual" Text="Manual" OnClick="btnManual_Click" />
            </div>
            <asp:Label runat="server" ID="lblResult"></asp:Label>
        </div>
        <div >
            <asp:Button ID="btndoSomthing" runat="server" Text="گرفتن فایلها جهت تبدیل به عکس" OnClick="btndoSomthing_Click" />
        </div>
    </form>
</body>
</html>
