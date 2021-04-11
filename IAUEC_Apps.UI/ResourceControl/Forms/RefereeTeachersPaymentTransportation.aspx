<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="RefereeTeachersPaymentTransportation.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.RefereeTeachersPaymentTransportation" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
            background-color:none
        }
    </style>
    <link href="../../University/Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .RadGrid .rgFilterRow > td, .RadGrid_MyCustomSkin .rgAltRow td {
            border: solid #00C851;
            border-width: 0 0 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgFilterRow > td:first-child {
            border-width: 0px 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgRow > td:first-child, .RadGridRTL_MyCustomSkin .rgAltRow > td:first-child {
            border-right-width: 1px;
        }

        .searchBox {
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 10px 15px;
            margin: 0 5px 15px;
        }

        .form-control {
            padding: 4px 12px;
        }

        .RadWindow_Default, .RadWindow table.rwTable {
            max-height: 100%;
        }

        .RadGrid_MyCustomSkin th.rgSorted {
            background-color: #10396c;
        }

        .RadGrid_MyCustomSkin .rgHeader a {
            color: white;
        }



        .btnExport {
            width: 50px;
            height: 48px;
            border-radius: 8px;
        }

        .drpRefereePayment-listitems {
            padding-right: 10px;
            color: #000;
        }

        .btnReject {
            width: 101px;
        }

        .pad-5 {
            padding: 5px !important;
        }

        .lbl-req {
            color: red;
            font-size: 20px;
            font-weight: bold;
        }

        #lightBox {
            display: none;
            position: fixed;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(0,0,0,0.5);
            z-index: 3000;
        }

        .drpMartabe {
            max-width: 103px;
        }
    </style>

    <script type="text/javascript">   

        //===========================================
        var isValidRequest = true;
        //var afterValidation = false;
        function onClickBtnPaymentHasDone(event) {
            debugger;
            if (isValidRequest) {
                event.preventDefault();
                var btnElement = event.currentTarget
                if (window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) {
                    //var marhtabe = $(this).closest("td").closest("tr").find("td:nth-child(8)").find("select").find("option:selected").val();        
                    var martabe = $(btnElement).closest("td").closest("tr").find("td:nth-child(8)").find("select").val();
                    var blackList = ["-1", "-2", "0", "6", "8"];
                    for (var i = 0; i < blackList.length - 1; i++) {
                        if (blackList[i].includes(martabe)) {
                            isValidRequest = false;
                            //afterValidation = true;
                            break;
                        }
                    }
                }
                if (!isValidRequest) {
                    alert("مرتبه استاد نامعتبر می باشد (مرتبه استاد فقط می تواند یکی از موارد دانشیار,استادیار یا استاد باشد)");
                    $(btnElement).closest("tr").find("td:nth-child(11)").text("0");//salary="0"   
                    isValidRequest = true;
                    return false;
                } else {
                    __doPostBack(btnElement.name, "");
                }
                isValidRequest = true; 
            }

        }
        function closeRadWindow1() {
            var window = $find('<%=RadWindow1.ClientID %>');
            window.close();

        }

        function showLightBox() {
            $('#lightBox').show();
            $('#<%=txtRejectRequest.ClientID%>').text("");
        }

        function refresgGrid() {
            $("#<%=btnRefreshGrid.ClientID%>").click();
        }

        function hideLightBox() {
            $('#lightBox').hide();
        }

        function openModal() {
            setTimeout(function () { $('#historyModal').modal('show'); }, 200);
        }


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl runat="server" ID="AccessControl" />

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" CssClass="radWindow">
    </telerik:RadWindowManager>

    <div class="row" dir="rtl" style="margin-right: 0px; margin-top: 15px;">
        <div class="col-md-3">
            <asp:DropDownList ID="drpRefereeWageStatus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpRefereeWageStatus_SelectedIndexChanged">
                <asp:ListItem Text="داورانی که هزینه ایاب وذهاب دریافت نکرده اند" Value="0" />
                <asp:ListItem Text="داورانی که هزینه ایاب وذهاب دریافت کرده اند" Value="1" />
            </asp:DropDownList>
        </div>

        <div class="col-md-2 col-md-pull-1">
            <asp:DropDownList ID="drpTerms" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpTerms_SelectedIndexChanged">
            </asp:DropDownList>
        </div>

        <div class="col-md-2 col-md-pull-5">
            <div>
                <asp:ImageButton ID="bt1ExportExcle" runat="server" ToolTip="خروجی اکسل" ImageUrl="../Images/microsoft excel.png" OnClick="bt1ExportExcle_Click" CssClass="btnExport" />
                <asp:ImageButton ID="btnExportText" runat="server" ToolTip="خروجی متن" ImageUrl="../Images/note.png" OnClick="btnExportText_Click" CssClass="btnExport" />
            </div>
        </div>
    </div>

    <div class="row" dir="rtl" style="margin-top: 2% !important;">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <telerik:RadGrid
                        HorizontalAlign="Center"
                        ID="grdRefereePayment"
                        runat="server"
                        AllowPaging="True"
                        AutoGenerateColumns="False"
                        PageSize="20"
                        AllowSorting="True"
                        OnNeedDataSource="grdRefereePayment_NeedDataSource"
                        OnItemCommand="grdRefereePayment_ItemCommand"
                        OnItemDataBound="grdRefereePayment_ItemDataBound"
                        AllowFilteringByColumn="True"
                        Skin="MyCustomSkin"
                        EnableEmbeddedSkins="false"
                        EnableLinqExpressions="False">

                        <MasterTableView  CssClass="table table-responsive">
                            <NoRecordsTemplate>
                                <div class="alert alert-danger" style="text-align: right; margin-top: 5%">هیچ درخواستی وجود ندارد</div>
                            </NoRecordsTemplate>
                            <ItemStyle />
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn AllowFiltering="false" DataField="RequestDate" AllowSorting="True" HeaderText="تاریخ">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="false" DataField="DavarMobile" AllowSorting="True" HeaderText="شماره تماس داور">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="fullNameDavar" AllowSorting="True" HeaderText="نام داور">
                                </telerik:GridBoundColumn>

                                  <telerik:GridBoundColumn AllowFiltering="false" DataField="MartabeOstadDavar" AllowSorting="True" HeaderText="مرتبه استاد">
                                </telerik:GridBoundColumn>    

                                <telerik:GridBoundColumn AllowFiltering="false" DataField="SibaOstadDavar" HeaderText="شماره حساب">
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn AllowFiltering="false" DataField="wage" HeaderText="مبلغ">
                                </telerik:GridBoundColumn>

<%--                                <telerik:GridBoundColumn AllowFiltering="False" DataField="RefereePayment" HeaderText="حق الزحمه">
                                </telerik:GridBoundColumn>--%>


                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                    <ItemTemplate>
                                        <div class="row">
                                            <asp:Button ID="btnPaymentHasDone" runat="server" CommandName="PaymentHasDone"
                                                CommandArgument='<%# Eval("RequestDate").ToString()+","+Eval("id_os").ToString()
                                                    +","+ Eval("DavarMobile").ToString() %>'
                                                Text="پرداخت انجام شد" ToolTip="پرداخت انجام شد" CssClass="btn btn-info btnPaymentHasDone"
                                                OnClientClick="if (!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) return false ;" Visible="false" />
                               

                                            <asp:Button ID="btnRejectDefenseRequest" runat="server" CommandName="RejectDefenseRequest"
                                                CommandArgument='<%# Eval("RequestDate").ToString()+","+Eval("id_os").ToString()
                                                     +","+ Eval("DavarMobile").ToString()%>' 
                                                Text="رد درخواست" ToolTip="رد درخواست" CssClass="btn btn-danger btnReject" Visible="false" />

                                        </div>
                                        <div class="row">
                                            <asp:Button ID="btnPaymentNotDone" runat="server" CommandName="PaymentNotDone"
                                               CommandArgument='<%# Eval("RequestDate").ToString()+","+Eval("id_os").ToString() 
                                                     +","+ Eval("DavarMobile").ToString()%>' 
                                                Text="پرداخت انجام نشد" ToolTip="پرداخت انجام نشد" CssClass="btn btn-danger "
                                                OnClientClick="if (!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) return false ;" Visible="false" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="lightBox"></div>

    <telerik:RadWindow ID="RadWindow1" Height="350" Width="500" AutoSizeBehaviors="HeightProportional" runat="server" OnClientClose="hideLightBox">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <h4 class="alert alert-danger" style="text-align: center">متن زیر جهت اطلاع برای استاد ارسال می گردد.</h4>
                                <h5 id="Label61">علت لغو درخواست: 
                                    <asp:Label ID="lblRejRequest" Text="" runat="server" CssClass="lbl-req" />

                                </h5>
                                <asp:Label ID="lblalertMessage" Visible="False" ForeColor="red" runat="server" Text="علت رد درخواست شمار : "></asp:Label>
                                <asp:TextBox ID="txtRejectRequest" TextMode="MultiLine" CssClass="form-control pad-5" runat="server" />
                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnRejectRequest" Text="لغو درخواست" OnClick="btnRejectRequest_Click" CssClass="btn btn-success" runat="server" />
                        <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="RefreshGrid" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

</asp:Content>

