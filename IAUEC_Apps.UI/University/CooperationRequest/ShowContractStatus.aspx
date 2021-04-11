<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowContractStatus.aspx.cs" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" Inherits="IAUEC_Apps.UI.University.CooperationRequest.ShowContractStatus" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="direction: rtl;">
        <div class="row">
            <div class="col-md-3">
                <asp:DropDownList runat="server" ID="ddlContractType" ValidationGroup="search" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlContractType_SelectedIndexChanged">

                    <asp:ListItem Text="قراردادهای آموزشی" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="قرارداد مدیر و معاون گروه" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label Text="انتخاب ترم" runat="server"></asp:Label>

                <asp:DropDownList ID="ddlTerm" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-success" Text="اکسل" OnClick="btnExcel_Click" />
            </div>
        </div>
        <div>

            <telerik:RadGrid runat="server" ID="grdContract" PageSize="50" AutoGenerateColumns="false" AllowPaging="true"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="True" 
                OnNeedDataSource="grdContract_NeedDataSource"  OnItemCommand="grdContract_ItemCommand" OnDataBound="grdContract_DataBound"
                Skin="MyCustomSkin">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <%# Container.DataSetIndex + 1 %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="code_ostad" HeaderText="کد استاد"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کد ملی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام و نام خانوادگی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DarsStatus" HeaderText=" درس در ترم انتخابی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HodStatus" HeaderText="وضعیت در سال انتخابی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="contractStatus" HeaderText="وضعیت قرارداد در بازه انتخابی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="vazfal" HeaderText="وضعیت همکاری در بازه انتخابی"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="عملیات" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnShowContract" Text="نمایش" CssClass="btn btn-info" CommandName="ShowContract" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"hrId")  %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

    </div>
      <telerik:RadCodeBlock runat="server" ID="rdbScripts">
        <script type="text/javascript">

            function openWin(id) {
                setTimeout(function () {
                    var oWnd = $find("<%=rwShowContract.ClientID%>");
                    oWnd.setUrl('ShowContract.aspx?' + id);
                    oWnd.show();
                    oWnd.center();
                }, 200);
            }
            function closeWin() {
                var oWnd = $find("<%=rwShowContract.ClientID%>");
                oWnd.hide();
            }
            <%--function refreshGrid() {
                document.getElementById("<%=btn.ClientID %>").click();
            }--%>
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpConfiguration"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwShowContract" runat="server" MinWidth="920px" Height="750px"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <uc1:AccessControl runat="server" ID="AccessControl" />
</asp:Content>
