<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gameCenter.aspx.cs" Inherits="IAUEC_Apps.UI.CommonUI.gameCenter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form runat="server" style="align-items: center; background-color: orange; padding-left: 10%; padding-right: 10%;">
        <div >
            <asp:DataList ID="dlGames" runat="server" RepeatColumns="3" ItemStyle-HorizontalAlign="Center" CellPadding="20" BackColor="Transparent" RepeatDirection="Horizontal">
                <ItemTemplate>
                    <div class="row">
                        <asp:ImageButton CommandArgument='<%#Eval("gameID") %>' ImageUrl='<%#Eval("gameImage") %>' id="img" style="width: 300px; height: 300px" runat="server" onclick="btnLink_Click" />
                       
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="align-items: center; align-content: center">
                            <asp:ImageButton CommandArgument='<%#Eval("gameID") %>' ToolTip="ورود به بازی" ImageUrl="images/Play-Games-icon64.png" ID="btnLink" runat="server" OnClick="btnLink_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>

        </div>
    </form>
</body>
</html>
