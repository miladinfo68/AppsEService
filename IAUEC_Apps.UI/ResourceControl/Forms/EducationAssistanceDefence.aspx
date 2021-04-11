<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationAssistanceDefence.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationAssistanceDefence" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }
    </style>
        <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
     <link href="../Style/js-persian-cal.css" rel="stylesheet" />
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
    <script>
        function openModal() {
            $('#historyModal').modal('show');
        }
        function closeRadWindow3() {

            var window = $find('<%=RadWindow2.ClientID %>');
                        window.close();
                        var window1 = $find('<%=RadWindow21.ClientID %>');
                        window1.close();
                        refresgGrid();
        }
        function refresgGrid() {
            document.getElementById("<%=btnRefreshGrid.ClientID %>").click();
                   }
        function closeRadWindow4() {

            var window = $find('<%=RadWindow2.ClientID %>');
                        window.close();
                        var window1 = $find('<%=RadWindow22.ClientID %>');
                        window1.close();
                        refresgGrid();
                    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
       <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadWindowManager ID="RadWindowManager2" runat="server" CssClass="radWindow">
    </telerik:RadWindowManager>

    <telerik:RadWindow ID="RadWindow2" AutoSize="true" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Button ID="btnRefreshGrid" OnClick="btnRefreshGrid_Click" CssClass="hidden" Text="refreshGrid" runat="server"  />


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
    <div class="container">
        <asp:HiddenField ID="hdnIdAssistance" runat="server" />
        <asp:HiddenField ID="hdnStcode" runat="server" />
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">

                    <div class="list-group-item">
                        <asp:DropDownList  ID="drpAssistanceDefenceTypeList" OnSelectedIndexChanged="drpRequestTypeList_OnSelectedIndexChanged" runat="server" CssClass="dropdown" Width="200px"
                            AutoPostBack="True">
                            <asp:ListItem Text="لیست مساعدت‌های تایید شده " Value="1" />
                            <asp:ListItem Text="لیست مساعدت‌های رد شده" Value="2" />
                            <asp:ListItem Text="لیست مساعدت‌های درحال  بررسی" Value="0" />
                            <asp:ListItem Text="لیست کلیه مساعدت ها" Value="-1" />
                        </asp:DropDownList>
                        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                            <AlertTemplate>
                                <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                                    <div class="rwDialogContent" style="text-align: center">
                                        <div style="color: black; font-size: 13px;" class="rwDialogMessage">
                                            {1}
                                        </div>
                                    </div>
                                    <br />
                                    <div class="rwDialogButtons text-center">
                                        <input type="button" value="تایید" style="width: 100px" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                                    </div>
                                </div>
                            </AlertTemplate>
                        </telerik:RadWindowManager>


                        <img src="../fonts/oprator.png" style="width: 32px; margin-right: 10px; margin-left: 10px; float: right" alt="" />
                        <h5 class="header-inline-display">مشاهده دانشجویان دارای درخواست مساعدت:</h5>
                        <div id="OpPanle" class="row bg-green" style="border-radius: 5px; margin-top: 5px">


                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">


                                            <telerik:RadGrid ID="grdDisplayAssistanceDefence" runat="server" AutoGenerateColumns="False"
                                                BackColor="#ffcccc"
                                                PageSize="20"
                                                OnNeedDataSource="grdDisplayAssistanceDefence_NeedDataSource"
                                                CssClass="table table-responsive table-stripted backColortable"
                                                EnableEmbeddedSkins="false"
                                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                                AllowPaging="True"
                                                AllowFilteringByColumn="True" Skin="MyCustomSkin"
                                                >

                                                <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">
                                                    <NoRecordsTemplate>
                                                        <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                                            <h5>هیچ رکوردی وجود ندارد</h5>
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                                                                                <telerik:GridTemplateColumn HeaderText="شماره درخواست " HeaderStyle-VerticalAlign="Middle" Visible="false">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblid" runat="server"
                                                                        Text='<%#Eval("id")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="کد دانشجویی " HeaderStyle-VerticalAlign="Middle" Visible="false">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblstcode" runat="server"
                                                                        Text='<%#Eval("StudentCode")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="id" HeaderText="شماره درخواست">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="fullName" HeaderText="نام و نام خانوادگی">
                                                        </telerik:GridBoundColumn>
                                                                                                                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="RequestDate" HeaderText="تاریخ پیشنهادی">
                                                        </telerik:GridBoundColumn>
                                                                                                                <telerik:GridBoundColumn AllowFiltering="false" FilterControlWidth="70px" DataField="RequestTime" HeaderText="ساعت پیشنهادی">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="عملیات" UniqueName="operator">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="HiddenStatusValue" runat="server" Value='<%#Eval("Status")%>' />
          <div class="row">
                                                                    <div class="col-md-6" runat="server" id="divApprove">
                              
                                                                        <asp:Label ID="lblAccept" runat="server" Text="مساعدت تایید شد" CssClass="label label-success" Visible='<%#int.Parse(Eval("Status").ToString())==1?true:false%>' Font-Size="Large" > </asp:Label>                                                            <asp:Button ID="btnApprove" Width="65" runat="server" CommandName="Confirm" CommandArgument='<%#Eval("ID")+"-"+Eval("StudentCode") %>' ToolTip="تایید" Text="تایید" CssClass="btn btn-primary" OnClick="btnApprove_Click"
                                                       Visible='<%#int.Parse(Eval("Status").ToString())==0?true:false%>'                                                                                />
                                                       <asp:Label ID="lblReject" runat="server" Font-Size="Large"  CssClass="label label-danger" Text="مساعدت رد شد"  Visible='<%#int.Parse(Eval("Status").ToString())==2?true:false%>'></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-6" runat="server" id="divAvoid">
                                                                        <asp:Button ID="btnAvoid" Width="65" runat="server" ToolTip="لغو" Text="لغو" CssClass="btn btn-danger" OnClientClick="confirmAspButton1(this); return false;" OnClick="btnDenyRequest_OnClick" Visible='<%#int.Parse(Eval("Status").ToString())==0?true:false%>'                    />
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn AllowFiltering="False" AllowSorting="False" HeaderText="تاریخچه درخواست های ثبت شده">
                                                            <ItemTemplate>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه " Visible="true" runat="server" ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" OnClick="btnHistory_OnClick" />
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>

                                                </MasterTableView>

                                            </telerik:RadGrid>

                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

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
                                             <th>شماره درخواست</th>
                                            <th>نام دانشجو</th>
                                            <th>تاریخ درخواست دفاع</th>
                                            <th>ساعت دفاع</th>
                                            <th>وضعیت</th>
                                        </tr>
                                         <asp:ListView ID="lst_history" runat="server">
                                            <ItemTemplate>
                                                <tr class="bg-blue" style="text-align: center;">
                                                    <td>
                                                        <%#Eval("RequestId") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("StudentFullName") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("RequestDate") %>
                                                    </td>
                                                    <td>
                                                        <%#Eval("StartTime") %>
                                                    </td>
                                                     <td>
                                                        <%#Eval("statusDsc") %>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                       
                                    </table>

                                </div>

                            </div>

                        </div>
                    </div>

     <telerik:RadWindow ID="RadWindow21" Height="440" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
  <%--          <asp:UpdatePanel ID="UpdatePanel31" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>--%>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="heading bg-danger" style="padding: 5px; text-align: center; border-radius: 2px;">
                            <h3 class="text-danger">لغو درخواست دفاع</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <h6 class="alert alert-info" style="text-align: center">متن زیر جهت اطلاع برای دانشجو ارسال می گردد.</h6>
                                <h5 id="Label61">علت لغو درخواست:</h5>
                                <asp:TextBox ID="txtDenyMessage1" TextMode="MultiLine" CssClass="form-control" runat="server" />


                                <asp:Label ID="lblalertMessageDeny" Visible="False" ForeColor="red" runat="server" Text="لطفا دلیل لغو درخواست را ذکر کنید"></asp:Label>

                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnDenyRequest1" Text="لغو درخواست" OnClientClick="validateDenyField()" OnClick="btnDenyRequest1_Click" CssClass="btn btn-danger" runat="server" ValidationGroup="Deny" />
                    </div>
                    <asp:HiddenField ID="hdnfDenyReqId1" runat="server" />
                </ContentTemplate>
<%--            </asp:UpdatePanel>
        </ContentTemplate>--%>
    </telerik:RadWindow>
    <telerik:RadWindow ID="RadWindow22" Height="440" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <%--<asp:UpdatePanel ID="UpdatePanel32" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>--%>
                    <div class="container" dir="rtl" style="padding: 10px">
                        <div class="heading bg-success" style="padding: 5px; text-align: center; border-radius: 2px;">
                            <h3 class="text-success">تایید درخواست دفاع</h3>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-12">
                                <h6 class="alert alert-info" style="text-align: center">متن زیر جهت اطلاع برای دانشجو ارسال می گردد.</h6>
                                <h5 id="label62">علت تایید درخواست:</h5>
                                <asp:TextBox ID="txtAcceptRequest" TextMode="MultiLine" CssClass="form-control" runat="server" />


                                <asp:Label ID="lblAlertMessageAccept" Visible="False" ForeColor="red" runat="server" Text="لطفا دلیل تایید درخواست را ذکر کنید"></asp:Label>

                            </div>
                        </div>
                        <br />
                        <asp:Button ID="btnAccecptRequest" Text="تایید مساعدت" OnClientClick="validateDenyField()" OnClick="btnApprove1_Click" CssClass="btn btn-success" runat="server"  ValidationGroup="Deny" />
                    </div>
                 
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </ContentTemplate>
    </telerik:RadWindow>



<%--    <script>          
        function confirmAspButton1(button) {
            function aspButtonCallbackFn(arg) {
                if (arg) {
                    __doPostBack(button.name, "");
                }
            }
            radconfirm("آیا با رد دفاع دانشجو موافقید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
        }
    </script>--%>

</asp:Content>
