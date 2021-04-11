<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPicture.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ShowPicture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Education/BootStrap/bootstrap.min.css" rel="stylesheet" />
    <title>نمایش عکس</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
       <div class="row">
           <div class="col-md-1">

           </div>
           <div class="col-md-10">
           <telerik:RadBinaryImage runat="server" ID="RadBinaryImage1" AutoAdjustImageControlSize="false" ResizeMode="Fit"/>
           </div>
           <div class="col-md-1">          
           </div>
       </div>
        <div class="row">
            <div class="col-md-11">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
