<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageContactOS.Master" AutoEventWireup="true" CodeBehind="TeacherReview.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.TeacherReview" %>

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

        /*.btn {
            padding: 6px 0 !important;
        }*/

        /*.btnInfo {
            background-color: #647ed1 !important;
            border-color: #293875;
            border-radius: 10px;
        }*/

        /*.drp {
            border-radius: 10px;
            border: 1px solid #106062;
        }*/
        .pad{
                padding:10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">


        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">

                    <div class="list-group-item">
                            <asp:DropDownList ID="drpRequestTypeList" runat="server" CssClass="dropdown" Width="200px"
                                    AutoPostBack="True"  OnSelectedIndexChanged="drpRequestTypeList_SelectedIndexChanged" >
                                <asp:ListItem Text="لیست درخواستهای منتظر رد" Value="1" />
                                <asp:ListItem Text="لیست درخواستهای رد شده" Value="2" />
                                <asp:ListItem Text="لیست کلیه درخواستها" Value="0" />
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
                        <h5 class="header-inline-display">مشاهده دانشجویان برای تایید دفاع:</h5>
                        <div id="OpPanle" class="row bg-green" style="border-radius: 5px; margin-top: 5px">


                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">


                                            <telerik:RadGrid ID="grdDisplayStundetDefence" runat="server" AutoGenerateColumns="False"
                                                BackColor="#ffcccc"
                                                PageSize="20"
                                                OnNeedDataSource="grdDisplayStundetDefence_NeedDataSource"
                                                CssClass="table table-responsive table-stripted backColortable"
                                                EnableEmbeddedSkins="false"
                                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                                AllowPaging="True"
                                                AllowFilteringByColumn="True" Skin="MyCustomSkin">

                                                <MasterTableView DataKeyNames="RequestId" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">
                                                    <NoRecordsTemplate>
                                                        <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                                                            <h5>هیچ رکوردی وجود ندارد</h5>
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns>
                                                        <telerik:GridBoundColumn AllowFiltering="true" FilterControlWidth="70px" DataField="stcode" HeaderText="شماره دانشجویی">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AllowFiltering="true" DataField="FullName" HeaderText="نام و نام خانوادگی">
                                                        </telerik:GridBoundColumn>
                                                                         <telerik:GridBoundColumn AllowFiltering="true" DataField="RequestId" HeaderText="شماره دفاع">
                                                        </telerik:GridBoundColumn>
                                   <telerik:GridTemplateColumn HeaderText="شماره دفاع" HeaderStyle-VerticalAlign="Middle" Visible="false" AllowFiltering="false">
                                                                                                                                                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestId" runat="server"
                                                                    Text='<%#Eval("RequestId")%>'></asp:Label>
                                                            </ItemTemplate>
                                                          </telerik:GridTemplateColumn> 
                          <telerik:GridTemplateColumn HeaderText="مسئولیت استاد" HeaderStyle-VerticalAlign="Middle" Visible="true" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNameTypeOstad" runat="server"
                                                    Text='<%#Eval("NameTypeOstad")%>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                                                                                                        <telerik:GridTemplateColumn HeaderText="کد دانشجویی" HeaderStyle-VerticalAlign="Middle" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstudentcode" runat="server"
                                                                    Text='<%#Eval("stcode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="تاریخ دفاع" HeaderStyle-VerticalAlign="Middle" Visible="true" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrequestDate" runat="server"
                                                    Text='<%#Eval("requestDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                     <telerik:GridTemplateColumn HeaderText="ساعت دفاع" HeaderStyle-VerticalAlign="Middle"  Visible="true" AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrequestTime" runat="server"
                                                    Text='<%#Eval("requestTime")%>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                                                     <telerik:GridTemplateColumn HeaderText="توع استاد" HeaderStyle-VerticalAlign="Middle" Visible="false" AllowFiltering="false"  >                                                                                       <ItemTemplate>
                                                                <asp:Label ID="lblIdTypeOstad" runat="server"
                                                                    Text='<%#Eval("IdTypeOstad")%>'></asp:Label>
                                                            </ItemTemplate>
 
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="اعمال" AllowFiltering="false">
                                                            <ItemTemplate>

                                                                <asp:Button ID="btnReject" runat="server" CssClass="btn btn-danger" Text=" رد دفاع  " OnClick="btnReject_Click" Width="120px" Visible='<%#(int.Parse(Eval("status").ToString())==3?false:true)%>' OnClientClick="confirmAspButton1(this); return false;" />
                                                                    
                                                                <asp:Label ID="lblReject" runat="server" CssClass="label label-danger pad"  Text="دفاع رد شده است"      Width="120px" Height="36px" Font-Size="Small" Visible='<%#(int.Parse(Eval("status").ToString())!=3?false:true)%>'></asp:Label>
                                                                   
                                                                <asp:Button ID="btnAccept" Visible="false" runat="server" CssClass="btn btn-Success" Text=" تایید دفاع  " OnClick="btnAccept_Click" Width="120px"
                                                                    Enabled='<%#(bool)(Eval("FlagAccepted"))%>'
                                                                    OnClientClick="confirmAspButton1(this); return false;" />
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
            radconfirm("آیا با رد دفاع دانشجو موافقید؟", aspButtonCallbackFn, 550, 100, null, "Confirm");
        }
    </script>


</asp:Content>
