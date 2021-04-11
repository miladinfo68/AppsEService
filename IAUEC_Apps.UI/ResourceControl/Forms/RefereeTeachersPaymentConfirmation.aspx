<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master"
    AutoEventWireup="true" CodeBehind="RefereeTeachersPaymentConfirmation.aspx.cs"
    Inherits="ResourceControl.PL.Forms.RefereeTeachersPaymentConfirmation" %>


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
                isValidRequest = true; //reset flag  isValidRequest = true; //reset flag
            }

        }
        //===========================================

        //function onChangeDrpMartabe(e) {
        //    var salary = "0";
        //    var selectedVla = $(e).val();//$(this).find("option:selected").val();
        //    switch (selectedVla) {
        //        case "1": salary = "800000"; break;  //مربی
        //        case "2": salary = "1200000"; break; //دانشیار
        //        case "3": salary = "1000000"; break; //استادیار
        //        case "4": salary = "1500000"; break; //استاد
        //        default: break;//(-1 ,-2 ,0,8 )=فاقد مرتبه علمي     ,sayer=6

        //    }
        //    $(e).closest("tr").find("td:nth-child(11)").text(salary);
        //}
        //===========================================


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
                <asp:ListItem Text="داورانی که حق الزحمه های دریافت نکرده اند" Value="0" />
                <asp:ListItem Text="داورانی که حق الزحمه های دریافت کرده اند" Value="1" />
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

                        <MasterTableView DataKeyNames="StudentCode" CssClass="table table-responsive">
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
                                <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="30px" HeaderStyle-Width="30" DataField="RequestID" HeaderText="شماره درخواست">
                                </telerik:GridBoundColumn>


                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="داور اول " Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_ChkPaymentReferee1" runat="server" Value='<%#bool.Parse( Eval("ChkPaymentReferee1").ToString())%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="داور دوم " Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_ChkPaymentReferee2" runat="server" Value='<%#bool.Parse( Eval("ChkPaymentReferee2").ToString())%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="نوع داور" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_RefereeType" runat="server" Value='<%#Eval("RefereeType").ToString()%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="مرتبه داور راهنما" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_RefereeOrderValue" runat="server" Value='<%# Eval("RefereeOrderValue").ToString()%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی دانشجو">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="RequestDate" HeaderText="تاریخ دفاع">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="CollegeName" AllowSorting="True" HeaderText="دانشکده">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="RefereeMobile" AllowSorting="True" HeaderText="شماره تماس داور">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="RefereeFullName" AllowSorting="True" HeaderText="نام داور">
                                </telerik:GridBoundColumn>

                                  <telerik:GridBoundColumn AllowFiltering="true" DataField="RefereeOrder" AllowSorting="True" HeaderText="مرتبه استاد">
                                </telerik:GridBoundColumn>    

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn AllowFiltering="true" DataField="RefereeSiba" HeaderText="شماره حساب">
                                </telerik:GridBoundColumn>


                                <telerik:GridBoundColumn AllowFiltering="False" DataField="RefereePayment" HeaderText="حق الزحمه">
                                </telerik:GridBoundColumn>


                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                    <ItemTemplate>
                                        <div class="row">
                                            <asp:Button ID="btnPaymentHasDone" runat="server" CommandName="PaymentHasDone"
                                                CommandArgument='<%# int.Parse(Eval("RequestID").ToString())+","+Eval("StudentFullName").ToString()+","+Eval("RequestDate").ToString()+","+Eval("RefereeMobile").ToString()+","+Eval("RefereeFullName").ToString()+","+Eval("StudentCode").ToString()+","+Eval("RefereeType").ToString() + ","+Eval("RefereePayment").ToString()  %>'
                                                Text="پرداخت انجام شد" ToolTip="پرداخت انجام شد" CssClass="btn btn-info btnPaymentHasDone"
                                                OnClientClick="if (!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) return false ;" Visible="false" />
                                            <%--  Visible="false" OnClientClick="onClickBtnPaymentHasDone(event)" />--%>

                                            <asp:Button ID="btnRejectDefenseRequest" runat="server" CommandName="RejectDefenseRequest"
                                                CommandArgument='<%# int.Parse(Eval("RequestID").ToString())+","+Eval("StudentFullName").ToString()+","+Eval("RequestDate").ToString()+","+Eval("RefereeMobile").ToString()+","+Eval("RefereeFullName").ToString()+","+Eval("StudentCode").ToString()+","+Eval("RefereeType").ToString() + ","+Eval("RefereePayment").ToString()  %>'
                                                Text="رد درخواست" ToolTip="رد درخواست" CssClass="btn btn-danger btnReject" Visible="false" />

                                        </div>
                                        <div class="row">
                                            <asp:Button ID="btnPaymentNotDone" runat="server" CommandName="PaymentNotDone"
                                                CommandArgument='<%# int.Parse(Eval("RequestID").ToString())+","+Eval("StudentFullName").ToString()+","+Eval("RequestDate").ToString()+","+Eval("RefereeMobile").ToString()+","+Eval("RefereeFullName").ToString()+","+Eval("StudentCode").ToString()+","+Eval("RefereeType").ToString() + ","+Eval("RefereePayment").ToString()  %>'
                                                Text="پرداخت انجام نشد" ToolTip="پرداخت انجام نشد" CssClass="btn btn-danger "
                                                OnClientClick="if (!window.confirm('آیا مطمن هستید که تغیرات اعمال گردد')) return false ;" Visible="false" />
                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="تاریخچه">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnHistory" runat="server" CommandName="History"
                                            CommandArgument='<%# int.Parse(Eval("RequestID").ToString())+","+Eval("StudentFullName").ToString()+","+Eval("RequestDate").ToString()+","+Eval("RefereeMobile").ToString()+","+Eval("RefereeFullName").ToString()+","+Eval("StudentCode").ToString()+","+Eval("RefereeType").ToString() + ","+Eval("RefereePayment").ToString()  %>'
                                            AlternateText="تاریخچه" ToolTip="تاریخچه" ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <div class="modal fade" id="historyModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="background-color: aqua;">
                        <div class="modal-dialog" role="document" style="width: 70%">
                            <div class="modal-content bg-info border-dark">

                                <div class="modal-header" dir="rtl">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                                        <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                                        </span>
                                    </button>
                                    <div class="modal-header bg-orange" dir="rtl">
                                        <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border-bottom-color: black">
                                        <tr class="bg-blue-sky">
                                            <th>نام کاربر</th>
                                            <th>تاریخ</th>
                                            <th>ساعت</th>
                                            <th>وضعیت</th>
                                            <th>توضیحات</th>
                                        </tr>

                                        <asp:ListView ID="lst_history" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-blue" style="text-align: center;">
                                                    <td>
                                                        <%#Eval("Name") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("LogDate") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("LogTime") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("EventName") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("Description") %>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </table>

                                </div>

                            </div>

                        </div>
                    </div>
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

