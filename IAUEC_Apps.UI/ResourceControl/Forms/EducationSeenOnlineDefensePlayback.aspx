<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationSeenOnlineDefensePlayback.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationSeenOnlineDefensePlayback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Style/EducationOnlineDefence.css" rel="stylesheet" />
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


    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <link rel="stylesheet" href="../../CommonUI/css/js-persian-cal.css" />
    <script src="../../CommonUI/js/js-persian-cal.min.js"></script>
    <script src="../Scripts/EducationOnlineDefensePlayback.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">

    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <link href="../Style/bootstrap-rtl.min.css" rel="stylesheet" />
    <link href="../Style/StudentStyle.css" rel="stylesheet" />


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container">
              
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">

                    <div class="list-group-item">
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
                                        <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                                    </div>
                                </div>
                            </AlertTemplate>
                        </telerik:RadWindowManager>


                        <img src="../fonts/oprator.png" style="width: 32px; margin-right: 10px;" alt="" />
                        <h5 class="header-inline-display">مشاهده دفاع‌ها:</h5>
                          
                        <button class="btn btn-danger btnLinkTesti">جلسه تستی1</button>
                        <button class="btn btn-danger btnLinkTesti2">جلسه تستی2</button>
                        <button class="btn btn-danger btnLinkTesti3">جلسه تستی3</button>
                        <button class="btn btn-danger btnLinkTesti4">جلسه تستی4</button>
                        <button class="btn btn-danger btnLinkTesti5">جلسه تستی5</button>
                        <button class="btn btn-danger btnLinkTesti6 hidden" >جلسه تستی6</button>
                        <div id="OpPanle" class="row bg-green" style="border-radius: 5px; margin-top: 5px">
                            <div class="col-lg-4">
                            
                               <h4> <asp:Literal ID="litCollege" runat="server" Visible="false"></asp:Literal></h4>
                             
                                <asp:DropDownList ID="drpCollegeId" runat="server" CssClass="form-control" AutoPostBack="True" ForeColor="Black" OnSelectedIndexChanged="drpCollegeId_SelectedIndexChanged" Visible="false"> 
                                </asp:DropDownList>
                            </div>

                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">


                                            <telerik:RadGrid ID="grdDsiplayDefence" runat="server" AutoGenerateColumns="False"
                                                BackColor="#ffcccc"
                                                PageSize="10"
                                                OnNeedDataSource="grdDsiplayDefence_NeedDataSource"
                                                CssClass="table table-responsive table-stripted backColortable" 
                                              EnableEmbeddedSkins="false"
                                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                                AllowPaging="True" ClientSettings-EnablePostBackOnRowClick="true"
                                                 AllowFilteringByColumn="True" Skin="MyCustomSkin">
                                                <ClientSettings EnablePostBackOnRowClick="True">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL"
                                                    >

                                                    <Columns>
                                                                                           <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی">
                                </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="کد دانشجویی" HeaderStyle-VerticalAlign="Middle"
                                                            AllowFiltering="true" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstudentcode" runat="server"
                                                                    Text='<%#Eval("studentcode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="آی دی وضعیت برگزاری" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVazId" runat="server"
                                                                    Text='<%#Eval("vazId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="نام و نام خانوادگی" HeaderStyle-VerticalAlign="Middle"
                                                             AllowFiltering="true" Visible="false">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblName" runat="server"
                                                                        Text='<%#Eval("StudentFullName")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="موضوع" HeaderStyle-VerticalAlign="Middle"
                                                             AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <div class="text-center DefenceSubject">
                                                                    <asp:Label ID="lblDefenceSubject" runat="server"
                                                                        Text='<%#Eval("DefenceSubject")%>' CssClass="text-center "
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="رشته" HeaderStyle-VerticalAlign="Middle"
                                                             AllowFiltering="false">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnameresh" runat="server"
                                                                    Text='<%#Eval("nameresh")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="محل برگزاری"
                                                             AllowFiltering="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocName" runat="server"
                                                                    Text='<%#Eval("LocName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="ساعت برگزاری" HeaderStyle-VerticalAlign="Middle" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstartTime" runat="server"
                                                                    Text='<%#Eval("startTime")%>'></asp:Label>

                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="وضعیت" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvaz" runat="server"
                                                                    Text='<%#Eval("vazName")%>'></asp:Label>

                                                                <img src="../Images/<%#Eval("vazSrc")%>" />

                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false">
                                                         <ItemTemplate>

                                                                <%--<asp:Button ID="lnkLinkDefence" runat="server" CssClass="btn btn-success" Text=" ورود به جلسه دفاع  " OnClick="lnkLinkDefence_Click" Width="120px" />--%>
                                                                <a href="#" class="btn btn-success btnLinkDefence" 
                                                                    data-vazId='<%#Eval("vazId")%>' data-studentCode='<%#Eval("studentcode")%>'
                                                                    data-resLink='<%#Eval("resLink")%>' >ورود به جلسه دفاع</a>
                                                                <asp:Label ID="resLink" runat="server" Text='<%#Eval("resLink")%>' Visible="false"></asp:Label>
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
    <telerik:RadWindow RenderMode="Lightweight" runat="server" ID="RadWindow1">

        <ContentTemplate>
            <div dir="rtl" style="padding: 5px" class="rwDialog rwAlertDialog">
                                    <div class="rwDialogContent" style="text-align: center">
                                        <div style="color: black; font-size: 13px;" class="rwDialogMessage">
                                            "salam"
                                        </div>
                                    </div>
                                    <br />
                                    <div class="rwDialogButtons text-center">
                                        <input type="button" value="تایید" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                                    </div>
                                </div>
        </ContentTemplate>
    </telerik:RadWindow>
      <div class="modal fade mymodaldef" id="mymodaldef" style="opacity:0.9" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document" style="opacity:1">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">پیغام سیستم</h5>

      </div>

                        <div class="modal-body">
                    <p id="msgAlert"></p>

                </div>
 
      <div class="modal-footer">
      <button type="button" class="btn btn-secondary " data-dismiss="modal">تایید</button>
      </div>
    </div>
  </div>
</div>
    <script>
        $(function () {
           
            $('.btnLinkDefence').on('click', function (e) {

                e.preventDefault();
             
                var btn = $(this);
              
                //alert(btn.attr('data-vazId') + '...' + btn.attr('data-studentCode') + '...' + btn.attr('data-resLink'));
                $.ajax({
                    url: '/ResourceControl/Forms/EducationSeenOnlineDefensePlayback.aspx/GetDefenceLink',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({
                        vazid: btn.attr('data-vazId'), studentCode: btn.attr('data-studentCode'), resLink: btn.attr('data-resLink'), userId: <%= "'"+ Session[sessionNames.userID_Karbar].ToString()+"', userName:'"+Session[sessionNames.userName_Karbar].ToString()+"'"  %>  }),
                    success: function (r) {
                        
                        if (r.d == "false") {
                            $("#msgAlert").text("جلسه دفاع در حال حاضر در دسترس نیست");
                            $('.mymodaldef').modal('show');
                     
                        }
                        else {
                            window.open(r.d, '_blank');
                        }
                    }
                });
            });
            $('.btnLinkTesti').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({  userId: <%= "'"+ Session[sessionNames.userID_Karbar].ToString()+"', userName:'"+Session[sessionNames.userName_Karbar].ToString()+"'"  %>   }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti2').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti2',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                    success: function (r) {

                        if (r.d == "false") {
                            $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                            $('.modalDef').modal('show');
                        }
                        else {
                            window.open(r.d, '_blank');
                        }
                    }
                });
                        });
            $('.btnLinkTesti3').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti3',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                    success: function (r) {

                        if (r.d == "false") {
                            $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                            $('.modalDef').modal('show');
                        }
                        else {
                            window.open(r.d, '_blank');
                        }
                    }
                });
            });
            $('.btnLinkTesti4').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti4',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti5').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti5',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                        success: function (r) {

                            if (r.d == "false") {
                                $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                                $('.modalDef').modal('show');
                            }
                            else {
                                window.open(r.d, '_blank');
                            }
                        }
                    });
            });
            $('.btnLinkTesti6').on('click', function (e) {
                e.preventDefault();

                $.ajax({
                    url: '/ResourceControl/Forms/EducationOnlineDefensePlayback.aspx/BtnLinkTesti6',
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ userId: <%= Session[sessionNames.userID_Karbar].ToString() %> }),
                    success: function (r) {

                        if (r.d == "false") {
                            $('#msgAlert').text('جلسه دفاع در حال حاضر در دسترس نیست');
                            $('.modalDef').modal('show');
                        }
                        else {
                            window.open(r.d, '_blank');
                        }
                    }
                });
            });
        });
    </script>
</asp:Content>
