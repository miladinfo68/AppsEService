<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageContactOS.Master" AutoEventWireup="true" CodeBehind="TeacherOnlineDefensePlayback.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.TeacherOnlineDefensePlayback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .circles {
            margin-bottom: -10px;
        }

        .circle {
            width: 100px;
            display: inline-block;
            position: relative;
            text-align: center;
            line-height: 1.2;
        }

            .circle canvas {
                vertical-align: top;
            }

            .circle strong {
                position: absolute;
                top: 30px;
                left: 0;
                width: 100%;
                text-align: center;
                line-height: 40px;
                font-size: 30px;
            }

                .circle strong i {
                    font-style: normal;
                    font-size: 0.6em;
                    font-weight: normal;
                }

            .circle span {
                display: block;
                color: #aaa;
                margin-top: 12px;
            }

        img {
            margin-left: 2px;
        }

        #myProgress {
            width: 100%;
            background-color: #dedede;
            border-radius: 5px;
            /*border: 1px solid;*/
        }

        #myBar {
            width: 1%;
            height: 20px;
            background-color: green;
            border-radius: 5px;
        }

        .tableBorder {
            border: 2px solid #73879c !important;
            background-color: #1a82c3 !important;
            color: aliceblue !important;
        }

            .tableBorder th {
                border: 1px solid #73879c;
            }

        .backColortable {
            background-color: #1A82C3;
        }

        .table-hover {
            background-color: #c5dbf3 !important;
        }

            .table-hover > tbody > tr:not(.tableBorder):hover {
                background-color: #038677 !important;
                color: whitesmoke;
            }
         .DefenceSubject {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 250px !important;
}
    </style>
    <link href="../Style/bootstrap-rtl.min.css" rel="stylesheet" />
    <link href="../Style/StudentStyle.css" rel="stylesheet" />
    <style>
        .tableBorder {
            border: 2px solid whitesmoke;
        }

            .tableBorder th {
                border: 1px solid whitesmoke;
            }

        .backColortable {
            background-color: #1A82C3;
        }

        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }

        .rgHeader {
            text-align: center !important;
            font-size: 14px;
        }

        .RadPicker .rcSelect {
            left: 1px;
        }

        .rcTimePopup {
            border-right: 1px solid #cdcdcd;
            border-left: 1px solid #cdcdcd;
        }

        .RadInput .RadInput_Default .RadInputRTL .RadInputRTL_Default {
            border-right: 1px solid #cdcdcd;
        }

        .RadPicker .RadInput > input {
            float: right !important;
        }

        .RadPicker .riTextBox {
            padding-left: 4.286em !important;
            text-align: center !important;
        }

        .radWindow {
            text-align: center;
        }
    </style>
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
                        <h5 class="header-inline-display" style="font-family:'B Titr' ">مشاهده دفاع‌ها:</h5>
                         <asp:Button ID="btnTesti" runat="server" Text="جلسه تستی1  " Visible="false" OnClick="btnTesti_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti2" runat="server" Text="جلسه تستی2  " Visible="false"  OnClick="btnTesti2_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti3" runat="server" Text="جلسه تستی3  " Visible="false" OnClick="btnTesti3_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti4" runat="server" Text="جلسه تستی4  " Visible="false" OnClick="btnTesti4_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti5" runat="server" Text="جلسه تستی5  " Visible="false" OnClick="btnTesti5_Click"   class="btn btn-danger"  />
                         <asp:Button ID="btnTesti6" runat="server" Visible="false" Text="جلسه تستی6  " OnClick="btnTesti6_Click"   class="btn btn-danger"  />

                        <div id="OpPanle" class="row bg-green" style="border-radius: 5px; margin-top: 5px">
                          


                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
                                            <telerik:RadGrid ID="grdDsiplayDefence" runat="server" AutoGenerateColumns="False"
                                                BackColor="#ff9999"
                                                PageSize="10"
                                                ForeColor="Black"
                                                ActiveItemStyle-ForeColor="Black"
                                                ActiveItemStyle-VerticalAlign="Middle"
                                                AlternatingItemStyle-VerticalAlign="Middle"
                                                EnableHeaderContextMenu="true"
                                                OnNeedDataSource="grdDsiplayDefence_NeedDataSource"
                                                CssClass="table table-responsive  backColortable"
                                                EmptyDataText="رکوردی برای این جدول وجود ندارد"
                                                AllowPaging="True">
                                                <ClientSettings>
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView DataKeyNames="Id" AutoGenerateColumns="false" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL">

                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderStyle-VerticalAlign="Middle" HeaderText="آی دی وضعیت برگزاری" Visible="false">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblVazId" runat="server"
                                                                        Text='<%#Eval("vazId")%>' ForeColor="Black" CssClass="text-center"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                                                                  <telerik:GridTemplateColumn HeaderStyle-VerticalAlign="Middle" HeaderText="شماره دانشجویی" >
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblstudentcode" runat="server"
                                                                        Text='<%#Eval("studentcode")%>' ForeColor="Black" CssClass="text-center"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="نام و نام خانوادگی" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblName" runat="server"
                                                                        Text='<%#Eval("StudentFullName")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="موضوع" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate >
                                                                <div class="text-center DefenceSubject" >
                                                                    <asp:Label ID="lblDefenceSubject" runat="server"
                                                                        Text='<%#Eval("DefenceSubject")%>' CssClass=" text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                               <telerik:GridTemplateColumn HeaderText="رشته" HeaderStyle-VerticalAlign="Middle">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnameresh" runat="server" CssClass="text-center"
                                                                    Text='<%#Eval("nameresh")%>' ForeColor="Black" ></asp:Label>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="محل برگزاری" Visible="false" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblLocName" runat="server"
                                                                        Text='<%#Eval("LocName")%>' CssClass="text-center"
                                                                        ForeColor="Black"></asp:Label>
                                                                </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                                                                                            <telerik:GridTemplateColumn HeaderText="ساعت برگزاری" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                <asp:Label ID="lblstartTime" ForeColor="Black" CssClass="text-center" runat="server"
                                                                    Text='<%#Eval("startTime")%>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn HeaderText="وضعیت" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblvaz" runat="server"
                                                                        Text=<%#Eval("vazName")%>
                                                                        ForeColor="Black" CssClass="text-center"></asp:Label>
                               <img src="../Images/<%#Eval("vazSrc")%>" />
                                                                </div>
                                                            </ItemTemplate>

                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="اعمال" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Button ID="lnkLinkDefence" runat="server" CssClass="btn btn-success" Text=" ورود به جلسه دفاع " OnClick="lnkLinkDefence_Click" />

                                                                    <asp:Label ID="resLink" runat="server" CssClass="text-center" Text='<%#Eval("resLink")%>' Visible="false" ForeColor="Black"></asp:Label>
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


</asp:Content>
