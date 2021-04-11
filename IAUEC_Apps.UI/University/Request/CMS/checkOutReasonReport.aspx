<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" CodeBehind="checkOutReasonReport.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.checkOutReasonReport" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>


<asp:Content ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
</asp:Content>


<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>

</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .RadGrid_MyCustomSkin .rgRow td {
            background-color: rgba(0, 153, 0, 0.37) !important;
        }
    </style>
    <div class="container">
        <br /><br />
        <div class="row">
            <div class="col-md-2">
                <span>نوع گزارش</span>
            </div>
            <div class="col-md-3">
                <asp:DropDownList Width="100%" ID="drpReportType" runat="server" OnSelectedIndexChanged="drpReportType_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="علت انصراف هر دانشجو" Value="st"></asp:ListItem>
                    <asp:ListItem Text="فراوانی در هر دسته بندی " Value="rsn"></asp:ListItem>
                    <asp:ListItem Text="فراوانی به تفکیک دانشکده " Value="dnsh"></asp:ListItem>
                    <asp:ListItem Text="فراوانی به تفکیک مقطع " Value="lvl"></asp:ListItem>
                    <asp:ListItem Text="فراوانی به تفکیک سال ورودی " Value="ent"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <%--<asp:Button ID="btnShow" Width="100%" CssClass="btn btn-success" runat="server" Text="نمایش" />--%>
            </div>
            <div class="col-md-5 left">
                <asp:ImageButton ID="btnExcel" OnClick="btnExcel_Click" AlternateText="گزارش اکسلی"  Width="50px" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                
                <telerik:RadGrid ID="grdReason" runat="server" CssClass="table table-bordered" 
                        MasterTableView-Dir="RTL" HeaderStyle-BackColor="#009900" Skin="MyCustomSkin" EnableEmbeddedSkins="false">
                    <MasterTableView AllowSorting="true" AllowFilteringByColumn="true">
                       
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                
                <telerik:RadGrid ID="grdEnter" runat="server" CssClass="table table-bordered" Width="100%"
                        MasterTableView-Dir="RTL" HeaderStyle-BackColor="#009900" Skin="MyCustomSkin" EnableEmbeddedSkins="false">
                    <MasterTableView AllowSorting="true"  >
                        
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                
                <telerik:RadGrid ID="grdDanesh" runat="server" CssClass="table table-bordered"
                        MasterTableView-Dir="RTL" HeaderStyle-BackColor="#009900" Skin="MyCustomSkin" EnableEmbeddedSkins="false">
                    <MasterTableView AllowSorting="true">
                        
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                
                <telerik:RadGrid ID="grdLevel" runat="server" CssClass="table table-bordered"
                        MasterTableView-Dir="RTL" HeaderStyle-BackColor="#009900" Skin="MyCustomSkin" EnableEmbeddedSkins="false">
                    <MasterTableView AllowSorting="true">
                        
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
        <div class="row" id="dvReasonIteration">
            <div class="col-md-12 text-center">
                <div style="margin-left: auto; margin-right: auto; width: 60%">
                    <telerik:RadGrid ID="grdReport" runat="server" CssClass="table table-bordered"
                        MasterTableView-Dir="RTL" HeaderStyle-BackColor="#009900" Skin="MyCustomSkin" EnableEmbeddedSkins="false">
                        <MasterTableView>
                           
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
