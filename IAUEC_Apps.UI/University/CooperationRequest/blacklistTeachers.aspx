<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" CodeBehind="blacklistTeachers.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.blacklistTeachers" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content runat="server" ID="h" ContentPlaceHolderID="HeaderplaceHolder">
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content runat="server" ID="p" ContentPlaceHolderID="PageTitle">
      <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ID="m" ContentPlaceHolderID="ContentPlaceHolder1">
    <telerik:RadWindowManager ID="rwm" runat="server" ></telerik:RadWindowManager>

    <div class="content" dir="rtl">
        <div class="panel panel-success">
            <div class="panel-heading">
                <span>اضافه کردن کد ملی به لیست غیر مجاز</span>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label runat="server" Text="کد ملی"></asp:Label>
                        <asp:TextBox runat="server" MaxLength="10" ID="txtIddMeli"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearchTeacher" Text="بررسی کد ملی" CssClass="btn btn-info" OnClick="btnSearchTeacher_Click" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button CssClass="btn btn-danger" Visible="false" ID="btnBlacklist" OnClick="btnBlacklist_Click" runat="server" Text="اضافه به لیست غیر مجاز"/>
                        <asp:Button CssClass="btn btn-success" Visible="false" ID="btnExitBlacklist" OnClick="btnExitBlacklist_Click" runat="server" Text="خروج از لیست غیر مجاز"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-danger">
            <div class="panel-heading">
                <span>لیست اساتید غیر مجاز</span>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <telerik:RadGrid runat="server" ID="grdBlacklist" AutoGenerateColumns="false" AllowSorting="true">
                            <MasterTableView>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی" AllowSorting="true"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="status" HeaderText="وضعیت" AllowSorting="true"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
