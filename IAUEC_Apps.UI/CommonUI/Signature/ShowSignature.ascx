<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowSignature.ascx.cs" Inherits="IAUEC_Apps.UI.CommonUI.Signature.ShowSignature" %>



<div>
    <img src="" id="base64Img" />

    <asp:HiddenField runat="server" ID="hdn" />
</div>
<script src="js/jquery-3.3.1.min.js"></script>
<script>
    var hidden = '#' + '<%= hdn.ClientID %>';
    var baseString = $(hidden).val().trim();
    if (baseString.substring(0, 4) != "data") {
        baseString = "data:image/png;base64," + baseString;
    }
    $("#base64Img").prop('src', baseString);
 
</script>