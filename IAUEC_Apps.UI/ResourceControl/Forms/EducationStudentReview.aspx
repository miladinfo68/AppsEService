<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationStudentReview.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationStudentReview" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<%@ Register TagPrefix="cc1" Namespace="Stimulsoft.Report.Web" Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.HtmlControls" Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
ipt>
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
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
        /*-----------------------------*/
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

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td, .RadGrid .rgFilterRow > td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
            padding-left: 20px !important;
        }

        .btn {
            padding: 6px 0 !important;
        }

        .btnInfo {
            background-color: #647ed1 !important;
            border-color: #293875;
            border-radius: 10px;
        }

        .drp {
            border-radius: 10px;
            border: 1px solid #106062;
        }
    </style>


    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
 
            function confirmAspButton(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید؟", aspButtonCallbackFn, 250, 100, null, "Confirm");
            }

            function confirmAspButton1(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("با انتخاب لغو درخواست دانشجو موظف است مجددا درخواست جدیدی ثبت نماید آیا مطمئن هستید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
            }

            function refresgGrid() {
                document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
            }
            function closeRadWindow3() {

                var window = $find('<%=RadWindow2.ClientID %>');
            window.close();
            var window1 = $find('<%=RadWindow21.ClientID %>');
                window1.close();
                refresgGrid();
            }
            function closeRadWindow4() {

                 var window1 = $find('<%=rdwInsertScore.ClientID %>');
                 window1.close();
                 refresgGrid();
             }
            

            function closeRadWindow2() {

                var window = $find('<%=RadWindow2.ClientID %>');
                window.close();
                refresgGrid();
            }

            function closeRadWindowAverageIsLessThan14() {

                var window = $find('<%=rwAverageIsLessThan14.ClientID %>');
                window.close();
            }

            function closeRadWindowAverageIsLessThan14_2() {

                var window = $find('<%=rwAverageIsLessThan14_2.ClientID %>');
                window.close();
            }

            function closeRadWindowConfirmPopup() {

                var window = $find('<%=rdwConfirmPopup.ClientID %>');
                window.close();
            }

            function getControl() {

                var masterTable = $find('<%=grdDefenceList.ClientID%>').get_masterTableView();
                var btnApprove = masterTable.get_dataItems()[0].findControl('btnApprove');// Here 0 represents the index of crow in editmode
                return btnApprove;
            }

            $("body").on("click", getControl(), function () {

                var IsValid = true;
                // Do client side button click stuff here.
                $('#<%=grdDefenceList.ClientID%> tr').each(function () {
                // action to perform.  Use $(this) for changing each cell
                var tr = $(this)
                if (tr.hasClass("bg-danger")) {
                    IsValid = false;
                    return false;
                }

                tr.find('td').each(function () {

                    if ($(this).find('option:selected').val() == 0) {
                        args.IsValid = false;
                        return;
                    }
                });
            });

            if (IsValid) {

                var data = HTMLtbl.getData($('#<%=grdDefenceList.ClientID%>'));  // passing that table's ID //
                var parameters = {};
                parameters.array = data;

                $.ajax({
                    type: "POST",
                    url: "TechnicalStudentReview.aspx/btnSubmitFinalClick",
                    data: JSON.stringify(parameters),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        radalert("کلاس با موفقیت تایید شد.", 300, 100, "پیام", closeRadWindow, "");
                    },
                    error: function (msg) {
                        alert("خطا!!!");
                    }
                });
            }

        });

            function closeRadWindow() {
                var window = $find('<%=grdDefenceList.ClientID %>');
                window.close();
                refresgGrid();
            }
            function openModal() {
                $('#historyModal').modal('show');
            }

            </script>
    </telerik:RadScriptBlock>
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

        <div class="modal fade" id="ModalAlert" style="opacity:0.9;text-align:right" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModalAlert" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblTitle" CssClass="alert" Font-Bold="true" Font-Size="25px" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                             <p><asp:Label ID="lblAlert" font-size=16px Font-Names="BTitr" runat="server" Text="Label"></asp:Label></p>
                            </div>
                    
                  
                    <div class="modal-footer">
                        <button class="btn btn-info " data-dismiss="modal" aria-hidden="true" style="padding:10px !important">تایید</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>




    <telerik:RadWindow ID="RadWindow2" AutoSize="true" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_OnClick" CssClass="hidden" Text="refreshGrid" runat="server" />


                    <%--                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="heading bg-danger" style="padding: 5px">
                            <h3 class="text-danger">رد درخواست</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-10">
                                <asp:Label ID="Label6" Text="دلیل لغو/رد درخواست:" runat="server" />
                                <asp:TextBox ID="txtDenyMessage" TextMode="MultiLine" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="لطفا دلیل لغو/رد درخواست را ذکر کنید" ForeColor="Red" Display="Dynamic" ValidationGroup="deny" ControlToValidate="txtDenyMessage" runat="server" />
                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnDenyRequest" Text="لغو/رد درخواست" OnClientClick="sure(this);return false;" OnClick="btnDenyRequest_OnClick" ValidationGroup="deny" CssClass="btn btn-danger" runat="server" />
                    </div>--%>
                    <asp:HiddenField ID="hdnfDenyReqId" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="rdwPrint" runat="server" Width="1200px" Height="700px" Skin="Glow" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updateProgress" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="container">
                        <%--                        <div class="row" style="margin: 0 40%;">
                            <asp:Button runat="server" ID="btnPrintReport" Text="پرینت" CssClass="btn btn-info btnInfo" OnClick="btnPrintReport_OnClick" Width="90" Height="40" />
                            <asp:Button runat="server" ID="btnSaveReport" Text="ذخیره سازی" CssClass="btn btn-info btnInfo" OnClick="btnSaveReport_OnClick" Width="90" Height="40" />
                            <asp:HiddenField runat="server" Visible="False" ID="hdnRequestId" />
                            <asp:HiddenField runat="server" Visible="False" ID="hdnStudentCode" />
                        </div>--%>
                        <div class="row">
                            <iframe src="SessionPrint.aspx" style="width: 99%; height: 100%; min-height: 620px;"></iframe>
                            <%--                            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Width="100%" ShowZoom="False" ShowParametersButton="False" ShowViewMode="false" ShowBookmarksButton="false" ShowPrevButton="false" Visible="false" ShowCurrentPage="False" ShowFirstButton="False" ShowLastButton="False" ShowNextButton="False" ShowSave="False" ShowPrintButton="False" BackColor="#242e35" />--%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindow4" AutoSize="true" AutoSizeBehaviors="HeightProportional" runat="server">
    </telerik:RadWindow>

    <div class="row" dir="rtl" style="margin-top: 2%">
        <div class="col-md-3" style="margin-right: 2%;">
            <asp:DropDownList ID="drpRequestTypeList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="drpRequestTypeList_OnSelectedIndexChanged">
                <asp:ListItem Text="لیست درخواستهای منتظر تایید دانشکده" Value="1" />
                <asp:ListItem Text="لیست درخواستهای تایید شده توسط دانشکده" Value="2" />
                <asp:ListItem Text="لیست درخواستهای تایید شده توسط اداری" Value="3" />
                <asp:ListItem Text="لیست درخواستهای از دست رفته" Value="4" />
                <asp:ListItem Text="لیست درخواست های لغو شده توسط دانشکده" Value="6" />
                <asp:ListItem Text="لیست درخواست های موجود در کارتابل استاد " Value="7" />
                <asp:ListItem Text="لیست درخواست های موجود در کارتابل مالی " Value="9" />
                <asp:ListItem Text="لیست درخواست های موجود در کارتابل فنی " Value="8" />
                <asp:ListItem Text="لیست کلیه درخواستها" Value="5" />
            </asp:DropDownList>
        </div>
        <div class="col-md-3 col-md-pull-6">
            <div style="float: left; margin-left: 45px; margin-top: -10px;">
                <asp:ImageButton ID="bt1ExportExcle" runat="server" ToolTip="خروجی اکسل" Width="50" ImageUrl="../Images/microsoft excel.png" OnClick="bt1ExportExcle_OnClick" />
            </div>
        </div>
    </div>
    <div class="row" dir="rtl" style="margin-left: 0.01% !important; margin-right: 0.01% !important; margin-bottom: 5% !important; margin-top: 2% !important;">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                        <ProgressTemplate>
                            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="../Images/loading-svg-spinning-4.gif" AlternateText="درحال بارگزاری ..." ToolTip="درحال بارگزاری ..." Style="padding: 10px; position: fixed; top: 28%; left: 38%;" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <telerik:RadGrid
                        HorizontalAlign="Center"
                        ID="grdDefenceList"
                        runat="server"
                        AllowPaging="True"
                        AutoGenerateColumns="False"
                        PageSize="20"
                        AllowSorting="True"
                        OnItemDataBound="grdDefenceList_OnItemDataBound"
                        OnItemCommand="grdDefenceList_OnItemCommand"
                        
                        AllowFilteringByColumn="True" Skin="MyCustomSkin"
                        EnableEmbeddedSkins="false"
                        OnNeedDataSource="grdDefenceList_OnNeedDataSource">
                        <MasterTableView DataKeyNames="StudentCode">

                            <NoRecordsTemplate>
                                <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                    <h5>هیچ درخواستی وجود ندارد</h5>
                                </div>
                            </NoRecordsTemplate>
                            <ItemStyle />
                            <HeaderStyle HorizontalAlign="Center" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                    <ItemTemplate>
                                        <%# Container.ItemIndex + 1 %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="ID" HeaderText="شماره درخواست">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="issue_time" HeaderText="زمان ثبت درخواست">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="RequestDate" HeaderText="زمان برگزرای جلسه دفاع">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="nameresh" HeaderText="رشته تحصیلی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StartTime" HeaderText="ساعت شروع">
                                </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" DataField="NezamName" HeaderText="نظام وظیفه">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HiddenStatusValue" runat="server" Value='<%#Eval("Status")%>' />
                                        <asp:HiddenField ID="hd_Average" runat="server" />

                                        <div class="row">
                                            <div class="col-md-6" runat="server" id="divShowDetail">
                                                <asp:Button ID="btnShowDetail" Width="65" runat="server" OnClick="btnShowDetail_OnClick" Text="نمایش" CssClass="btn btn-success" />

                                            </div>
                                            <div class="col-md-6" runat="server" id="divEdit">
                                                <asp:Button ID="btnEdit" Width="65" runat="server" OnClick="btnEdit_OnClick" ToolTip="ویرایش" Text="ویرایش" CssClass="btn btn-warning" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6" runat="server" id="divApprove">
                                                <%--<asp:Button ID="btnApprove" Width="65" runat="server" OnClick="btnApprove_OnClick" ToolTip="تایید" Text="تایید" CssClass="btn btn-primary" OnClientClick="confirmAspButton(this); return false;" />--%>
                                                <asp:Button ID="btnApprove" Width="65" runat="server" CommandName="Confirm" CommandArgument='<%#Eval("ID")+"-"+Eval("StudentCode")+"-"+Eval("RequestDate") %>' ToolTip="تایید" Text="تایید" CssClass="btn btn-primary Approved" />
                                                <asp:Button ID="btnPrint" Width="65" runat="server" ToolTip="صورتجلسه" Text="صورتجلسه" CssClass="btn btn-primary" CommandName="print" CommandArgument='<%#Eval("ID")+"-"+Eval("StudentCode")+"-"+Eval("RequestDate") %>' />
                                              
                                               
                                            </div>
                                            <div class="col-md-6" runat="server" id="divAvoid">
                                                <asp:Button ID="btnAvoid" Width="65" runat="server" OnClick="btnDenyRequest_OnClick" ToolTip="لغو" Text="لغو" CssClass="btn btn-danger" OnClientClick="confirmAspButton1(this); return false;" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6"  runat="server" id="divchkFinal">
                                             <p > <asp:CheckBox ID="ChkFinal" CssClass="form-check-input" Visible="false" runat="server" Text=" صورتجلسه نهایی " />    </p>
                                                </div>
                                            <div class="col-md-6"  runat="server" id="divScore">
                                         
                                                <asp:Button ID="btnScore" Width="65" runat="server" CommandName="InsertScore" CommandArgument='<%#Eval("ID")+"-"+Eval("StudentCode")+"-"+Eval("RequestDate") %>' ToolTip="درج نمره" Text="درج نمره" CssClass="btn btn-dark"  OnClick="btnScore_Click" />
                                                                                         
                                        </div>
                                        <%-- <div class="row">
                                            <div class="col-xs-12">
                                                <asp:DropDownList runat="server" ID="drpDefenceDate" CssClass="form-control drp">
                                                    <asp:ListItem Value="0" Selected="True">تاریخ دفاع</asp:ListItem>
                                                    <asp:ListItem Value="1">1397/10/27</asp:ListItem>
                                                    <asp:ListItem Value="2">1397/10/28</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                        </div>
                                         <div class="row">
                                            <div class="col-md-12"  runat="server" id="divSelectUnit">
                                             <p > <telerik:RadCheckBox ID="chkSelectUnit" CssClass="form-check-input"   Visible="true"  runat="server" Text="تاییدیه انتخاب واحد"   CommandName="SelectUnit"   />    </p>
                                                </div>
                 

                                        </div>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="تاریخچه">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" OnClick="btnHistory_OnClick" ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                            </div>
                                        </div>
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

    <telerik:RadWindow ID="RadWindow3" runat="server" Width="1200px" Height="650px" Skin="Glow" Modal="True">
        <ContentTemplate>
            <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl">
                        <div class="heading2 bg-green" style="padding: 5px">
                            <h3>
                            جزئیات درخواست :
                        </div>
                        <br />
                        <div class="row center-margin">
                            <div style="border: 5px solid #A9A9A9; margin-left: 5%; margin-right: 5%;">
                                <div style="margin: 5%;">
                                    <table class="table table-responsive" style="font-weight: bolder;">
                                        <tr>
                                            <td>
                                                <strong>موضوع جلسه دفاع : </strong>
                                                <asp:Label ID="lblDarkhast" CssClass="text-primary" runat="server" />
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>وضعیت : </strong>
                                                        <asp:Label ID="lblStatue" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>شماره درخواست : </strong>
                                                        <asp:Label ID="lblRequestId" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>تاریخ ثبت درخواست : </strong>
                                                        <asp:Label ID="lbldateOfRequest" CssClass="text-primary" runat="server" />
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>تاریخ درخواستی برگزاری جلسه:</strong>
                                                        <asp:Label ID="lblRequest" CssClass="text-primary" runat="server" />

                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>ساعت شروع: </strong>
                                                        <asp:Label ID="lblTime1" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>ساعت پایان: </strong>
                                                        <asp:Label ID="lblTime2" CssClass="text-primary" runat="server" />

                                                    </div>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr runat="server" id="divFirstOnlineTeacher">
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>نام اولین استاد آنلاین : </strong>
                                                        <asp:Label ID="lblfirstTeacherName" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>شماره موبایل:</strong>
                                                        <asp:Label ID="lblfirstTeacherMobile" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>ایمیل:</strong>
                                                        <asp:Label ID="lblfirstTeacherEmail" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>
                                        </tr>
                                        <tr runat="server" id="divSecondOnlineTeacher">
                                            <td>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <strong>نام دومین استاد آنلاین : </strong>
                                                        <asp:Label ID="lblSecondTeacherName" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <strong>شماره موبایل:</strong>
                                                        <asp:Label ID="lblSecondTeacherMobile" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-4">
                                                        <strong>ایمیل:</strong>
                                                        <asp:Label ID="lblSecondTeacherEmail" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>
                                        </tr>

                                        <tr class="hidden">
                                            <td>
                                                <div class="row ">
                                                    <div class="col-md-3">
                                                        <strong>استفاده از رایانه شخصی: </strong>
                                                        <asp:Label ID="lblOwnSysytem" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <strong>دفاع آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineDef" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-3">
                                                        <strong>پخش آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineShow" CssClass="text-primary" runat="server" />
                                                    </div>
                                                    
                                                    <div class="col-md-3">
                                                        <strong>برگزاری جلسه دفاع به صورت آنلاین:</strong>
                                                        <asp:Label ID="lblOnlineDoingMeeting" CssClass="text-primary"                                       runat="server" />
                                                    </div>
                                                </div>

                                            </td>

                                        </tr>

                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <%--                                                    <div class="col-md-4">
                                                        <strong>توضیحات: </strong>
                                                        <asp:Label ID="lblTozieh" CssClass="text-primary" runat="server" />
                                                    </div>--%>
                                                    <div class="col-md-6">
                                                        <strong>
                                                            <asp:Literal ID="litDenyNot" runat="server" Visible="False" Text=""></asp:Literal>

                                                        </strong>
                                                        <asp:Label ID="lblDenyNot" Visible="False" CssClass="text-primary" runat="server" />

                                                    </div>

                                                    <div class="col-md-6">
                                                        <strong>
                                                            <asp:Literal ID="lblheader" runat="server" Visible="False" Text=""></asp:Literal></strong>
                                                        <asp:Label ID="lblDateOfResponse" Visible="False" CssClass="text-primary" runat="server" />
                                                    </div>

                                                </div>

                                            </td>


                                        </tr>
                                        <tr>
                                            <td runat="server" id="tdGrdResult" visible="False" colspan="2">
                                                <div class="table-responsive text-center">
                                                    <asp:GridView ID="grdResult" runat="server" DataKeyNames="datetimeid" AutoGenerateColumns="false" CssClass="table table-responsive backGroundForGrdDate text-center">
                                                        <HeaderStyle CssClass="bg-blue-sky" />
                                                        <RowStyle ForeColor="#29343B"></RowStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="تاریخ">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtDate" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ساعت شروع">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:RequiredFieldValidator ErrorMessage="لطفا زمان آغاز را مشخص کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker1" runat="server" />
                                                                    <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Culture="en-GB" TimeView-HeaderText="زمان آغاز" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                        <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                                        </TimeView>
                                                                    </telerik:RadTimePicker>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ساعت پایان">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:RequiredFieldValidator ErrorMessage="لطفا زمان پایان را مشخص کنید" ForeColor="Red" Text="*" Display="Dynamic" ControlToValidate="RadTimePicker2" runat="server" />
                                                                    <telerik:RadTimePicker ID="RadTimePicker2" runat="server" Culture="en-GB" TimeView-HeaderText="زمان پایان" DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
                                                                        <TimeView ID="TimeView4" Interval="00:30:00" runat="server" TimeFormat="HH:mm" Columns="4" StartTime="07:00" EndTime="23:00">
                                                                        </TimeView>
                                                                    </telerik:RadTimePicker>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="کلاس">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClassName" Text='<%# Eval("ClassName") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="dateTimeId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblDateTimeId" Text='<%# Eval("DateTimeId") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="reqid" DataField="RequestId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td runat="server" id="tdInfoOstad" visible="true" colspan="2">
                                                <div class="table-responsive text-center">
                                                    <asp:GridView ID="GrdInfoOstad" runat="server" DataKeyNames="Id"                                   AutoGenerateColumns="false" CssClass="table table-responsive backGroundForGrdDate text-center">
                                                        <HeaderStyle CssClass="bg-orange" />
                                                        <RowStyle ForeColor="#29343B"></RowStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ردیف">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="کد استاد">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("Id").ToString() %>' runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderText="نام استاد">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("FullName").ToString() %>' runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                                                                                                                                                    <asp:TemplateField HeaderText="موبایل">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("Mobile").ToString() %>' runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                         <asp:TemplateField HeaderText="نوع ">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("TypeOS").ToString() %>' runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                                                                     <asp:TemplateField HeaderText=" امضا">
                                                                <ItemTemplate>
                                                                    <asp:Image Width="100px" Height="100px" src='<%# Eval("AddressSignature").ToString() %>' runat="server"/>
                                                                  
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                                                </div>
                                            </td>

                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindow ID="RadWindow21" Height="440" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel31"  AsyncPostBackTimeout="300" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="heading bg-danger" style="padding: 5px; text-align: center; border-radius: 2px;">
                            <h3 class="text-danger">لغو درخواست</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <h6 class="alert alert-info" style="text-align: center">متن زیر جهت اطلاع برای دانشجو ارسال می گردد.</h6>
                                <h5 id="Label61">علت لغو درخواست:</h5>
                                <asp:TextBox ID="txtDenyMessage1" TextMode="MultiLine" CssClass="form-control" runat="server" />


                                <asp:Label ID="lblalertMessage" Visible="False" ForeColor="red" runat="server" Text="لطفا دلیل لغو درخواست را ذکر کنید"></asp:Label>

                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnDenyRequest1" Text="لغو درخواست" OnClientClick="validateDenyField()" OnClick="btnDenyRequest1_OnClick" CssClass="btn btn-danger" runat="server" ValidationGroup="Deny" />
                    </div>
                    <asp:HiddenField ID="hdnfDenyReqId1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>
            <telerik:RadWindow ID="rdwInsertScore" Height="440" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel2" AsyncPostBackTimeout="300" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="heading bg-danger" style="padding: 5px; text-align: center; border-radius: 2px;">
                            <h3 class="text-danger">درج نمره</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <h6 class="alert alert-info" style="text-align: center">نمره زیر توسط اساتید مربوطه تایید می‌گردد.</h6>
                                <h5 id="lblScore">نمره :</h5>
                                <asp:TextBox ID="txtScore"   CssClass="form-control" runat="server"  MaxLength="5" />


                                <asp:Label ID="lblScoreValid" Visible="False" ForeColor="red" runat="server" Text=""></asp:Label>

                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnInsertScore" Text="درج نمره" OnClientClick="validateInsertScore()" OnClick="btnInsertScore_Click" CssClass="btn btn-success" runat="server"  />
                    </div>
                    <asp:HiddenField ID="hdnRequetIdScore" runat="server" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>




    <telerik:RadWindow ID="rwAverageIsLessThan14" Height="300" Width="400" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanelAvg" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <h4 class="alert alert-danger" style="text-align: center">معدل دانشجوی مورد نظر کمتر از 14 می باشد.</h4>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <asp:Button ID="btnShowRadWindowSooratJalaseh" Text="تایید" OnClick="btnShowRadWindowSooratJalaseh_Click" CssClass="btn btn-success" runat="server" Style="width: 100%;" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnCancleShowRadWindowSooratJalaseh" Text="لغــو" OnClick="btnCancleShowRadWindowSooratJalaseh_Click" CssClass="btn btn-default" runat="server" Style="width: 100%;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>



    <telerik:RadWindow ID="rwAverageIsLessThan14_2" Height="300" Width="400" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel42" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <h4 class="alert alert-danger" style="text-align: center">معدل دانشجوی مورد نظر کمتر از 14 می باشد.</h4>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <asp:Button ID="btnShowPopupAvgLassThan14" Text="تایید" OnClick="btnShowPopupAvgLassThan14_Click" CssClass="btn btn-success" runat="server" Style="width: 100%;" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnCancleShowPopupAvgLassThan14" Text="لغــو" OnClick="btnCancleShowPopupAvgLassThan14_Click" CssClass="btn btn-default" runat="server" Style="width: 100%;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadWindow ID="rdwConfirmPopup" Height="300" Width="400" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel_rdwConfirmPopup" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <h4 class="alert alert-danger" style="text-align: center">آیا مطمئن هستید؟ </h4>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-sm-6">
                                    <asp:Button ID="btnApprovedInConfirmPopup" Text="تایید" OnClick="btnApprovedInConfirmPopup_Click" CssClass="btn btn-success" runat="server" Style="width: 100%;" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnCancleInConfirmPopup" Text="لغــو" OnClick="btnCancleInConfirmPopup_Click" CssClass="btn btn-default" runat="server" Style="width: 100%;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>





</asp:Content>

