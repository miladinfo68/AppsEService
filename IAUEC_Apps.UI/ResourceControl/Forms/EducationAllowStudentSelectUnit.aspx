<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationAllowStudentSelectUnit.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationAllowStudentSelectUnit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>
        <asp:Literal ID="pt" runat="server"></asp:Literal>
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="tab-content col-md-12">
            <div class="row" dir="rtl" style="margin-right: 1%; margin-bottom: 2%">
                <div class="form-inline">
                    <div class="form-group">
                        <label>شماره دانشجویی:</label>
                        <asp:TextBox ID="txtStCode" OnTextChanged="txtStCode_TextChanged"  AutoPostBack="true" CssClass="form-control textBoxSearch textAlignCenter" runat="server" MaxLength="14"></asp:TextBox>
                            <label>نـام دانـشـجـو:</label>
                        <asp:Label ID="LblnameStudent" Width="150px" CssClass="form-control" runat="server"></asp:Label>
                    </div>


                </div>
            </div>

            <div class="row" dir="rtl" style="margin-right: 1%; margin-bottom: 2%">
                <div class="form-inline">

                    <asp:Button ID="btnSave" CssClass="btn btn-success " Width="100px" OnClick="btnSave_Click" runat="server" Text="ثبت" />


                </div>
            </div>
        </div>
    </div>
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
                                        <input type="button" value="تایید" style="width: 100px" class="rwOkBtn btn btn-danger" onclick="$find('{0}').close(true); return false;" />
                                    </div>
                                </div>
                            </AlertTemplate>
                        </telerik:RadWindowManager>


                        <img src="../fonts/oprator.png" style="width: 32px; margin-right: 10px; margin-left: 10px; float: right" alt="" />
                        <h5 class="header-inline-display">مشاهده دانشجویان  مجاز به انتخاب واحد :</h5>
                        <div id="OpPanle" class="row bg-green" style="border-radius: 5px; margin-top: 5px">


                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">


                                            <telerik:RadGrid ID="grdDisplayStundetDefenceOnline" runat="server" AutoGenerateColumns="False"
                                                BackColor="#ffcccc"
                                                PageSize="20"
                                                OnNeedDataSource="grdDisplayStundetDefenceOnline_NeedDataSource"
                                                
                                                CssClass="table table-responsive table-stripted backColortable" 
                                                EnableEmbeddedSkins="false"
                                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                                AllowPaging="True" 
                                                                      AllowFilteringByColumn="True" Skin="MyCustomSkin">

                                                <MasterTableView DataKeyNames="Id" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">
                                                                                <NoRecordsTemplate>
                                <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                    <h5>هیچ رکوردی وجود ندارد</h5>
                                </div>
                            </NoRecordsTemplate>
                                                    <Columns>
                                   <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="StudentCode" HeaderText="شماره دانشجویی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="StudentFullName" HeaderText="نام و نام خانوادگی">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="Dsc" HeaderText="نوع دفاع">
                                </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="آیدی" HeaderStyle-VerticalAlign="Middle" Visible="false" AllowFiltering="false">
 
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblid" runat="server" 
                                                                    Text='<%#Eval("Id")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="کد دانشجویی" HeaderStyle-VerticalAlign="Middle" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstudentcode" runat="server"
                                                                    Text='<%#Eval("StudentCode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>


                                                                                                                <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false">
                                                            <ItemTemplate>
                                                                <%--      <button type="button" class="btn btn-info btnChangeDate"
                                                                    a_starttime='<%#Eval("startTime")%>'
                                                                    a_endtime='<%#Eval("endTime")%>'
                                                                    a_date='<%#Eval("RequestDate")%>'
                                                                    a_id='<%#Eval("id")%>'>
                                                                    ویرایش تاریخ
                                                                </button>--%>

                                                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text=" حذف  " OnClick="btnDelete_Click" Width="120px"
                                                                    OnClientClick="confirmAspButton1(this); return false;"/>
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
        <script>          
            function confirmAspButton1(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا با حذف دانشجو موافقید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
            }
        </script>
</asp:Content>

