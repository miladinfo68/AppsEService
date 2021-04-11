<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageContactOS.Master" AutoEventWireup="true" CodeBehind="TeacherAcceptScore.aspx.cs" Inherits="IAUEC_Apps.UI.TeacherAcceptScore" %>
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

       <div class="modal fade" id="ModalAcceptScore" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <%--<asp:UpdatePanel ID="upModalAccept" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <asp:HiddenField ID="hdnStcode" runat="server" />
                <asp:HiddenField ID="hdnReqId" runat="server" />
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblModalTitle" CssClass="alert" Font-Bold="true" Font-Size="25px" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <div class="alert alert-success" style="text-align:center">
                            <p  style="font-size:20px">نمره اعلام شده</p>
                            <asp:Label ID="lblScore" runat="server" Font-Size="16px"  Text="Label"></asp:Label>
                        </div>
                        <asp:Panel ID="PanelMosh1" runat="server">
                        <div  style="margin:15px">
                     <p style="font-size:16px;margin-left:33px;float:right"> نمره اعلام شده مورد تایید است(استاد مشاور)</p><asp:CheckBox ID="chkMosh1" CssClass="form-check-input" Enabled="false" runat="server" />
                            </div>
                            </asp:Panel>
                        <asp:Panel ID="PanelMosh2" runat="server">
                                                <div style="margin:15px">
                     <p style="font-size:16px;margin-left: 11px;float:right"> نمره اعلام شده مورد تایید است(استاد مشاوردوم)</p><asp:CheckBox ID="chkMosh2" CssClass="form-check-input" runat="server" Enabled="false" />
                            </div>
                            </asp:Panel>
                            <asp:Panel ID="PanelRah1" runat="server">

                                                <div style="margin:15px">
                     <p style="font-size:16px;margin-left:33px;float:right"> نمره اعلام شده مورد تایید است(استاد راهنما)</p><asp:CheckBox ID="chkRah1" CssClass="form-check-input" runat="server" Enabled="false" />
                           </div> 
                                </asp:Panel>
                        <asp:Panel ID="PanelRah2" runat="server">
                        <div  style="margin:15px">
                     <p style="font-size:16px;margin-left:11px;float:right"> نمره اعلام شده مورد تایید است(استاد راهنمادوم)</p><asp:CheckBox ID="chkRah2" CssClass="form-check-input" runat="server" Enabled="false" />
                            </div>
                            </asp:Panel>
                           <asp:Panel ID="PanelDav1" runat="server">

                                                                            <div style="margin:15px">
                     <p style="font-size:16px;margin-left:40px;float:right"> نمره اعلام شده مورد تایید است(استاد داور )</p><asp:CheckBox ID="chkDav1" CssClass="form-check-input" runat="server" Enabled="false" />
                            </div>
                               </asp:Panel>
                            <asp:Panel ID="PanelDav2" runat="server">

                                                                            <div style="margin:15px">
                     <p style="font-size:16px;margin-left:17px;float:right"> نمره اعلام شده مورد تایید است(استاد داور دوم)</p><asp:CheckBox ID="chkDav2" CssClass="form-check-input" runat="server" Enabled="false" />
                            </div>
     </asp:Panel>
                            </div>
                  
                    <div class="modal-footer">
                        <asp:Button ID="btnAccept" CssClass="btn btn-success" runat="server" OnClick="btnAccept_Click"
                            Text="تایید" />
                        <asp:Button ID="btnRejectOrDisp" CssClass="btn btn-danger" runat="server" 
                            Text="انصراف" OnClick="btnRejectOrDisp_Click"/>
                             
                    </div>
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</div>
    <div class="modal fade" id="ModalAlert" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="upModalAlert" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><asp:Label ID="lblTitle" CssClass="alert" Font-Bold="true" Font-Size="25px" runat="server" Text=""></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                             <p><asp:Label ID="lblAlert" font-size=16px runat="server" Text="Label"></asp:Label></p>
                            </div>
                    
                  
                    <div class="modal-footer">
                        <button class="btn btn-info " data-dismiss="modal" aria-hidden="true">بستن</button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
    <div class="container">

        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    
                    <div class="list-group-item">

                          
                       
                        <img src="../fonts/oprator.png" style="width: 32px; margin-right: 10px;" alt="" />
                        تاییدیه نمره دفاع توسط اساتید


                        <div id="OpPanle" class="row "  style="border-radius: 5px; margin-top: 5px">
                          


                            <!-- Tab panes -->
                            <div class="tab-content col-md-12">
                                <div role="tabpanel" id="profile">
                                    <br />
                                    <div class="container" style="text-align: center">
                                        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
                                            <telerik:RadGrid ID="grdDsiplayDefence" runat="server" AutoGenerateColumns="False"
                                                BackColor="#f7f7f7"
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

                                                <MasterTableView DataKeyNames="Id" AutoGenerateColumns="false" NoMasterRecordsText="رکوردی برای این جدول وجود ندارد" Dir="RTL" >

                                                    <Columns>
                            <telerik:GridTemplateColumn HeaderStyle-VerticalAlign="Middle" HeaderText="شماره دفاع" >
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Label ID="lblRequestId" runat="server"
                                                                        Text='<%#Eval("RequestId")%>' ForeColor="Black" CssClass="text-center"></asp:Label>
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
                                                        
                                                                                                                            <telerik:GridTemplateColumn HeaderText="تاریخ برگزاری" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                <asp:Label ID="lblrequestdate" ForeColor="Black" CssClass="text-center" runat="server"
                                                                    Text='<%#Eval("requestdate")%>'></asp:Label>
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

                                                                      <telerik:GridTemplateColumn HeaderText="نمره اعلام شده" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                <asp:Label ID="lblScore" ForeColor="Black" CssClass="text-center" runat="server"
                                                                    Text='<%#Eval("Score") == null ||Eval("Score").ToString()==""|| decimal.Parse(Eval("Score").ToString())<0||decimal.Parse(Eval("Score").ToString())>20?"":Eval("Score")%>' Font-Bold="true"></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn HeaderText="اعمال" HeaderStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:Button ID="modalOpenScore" runat="server" CssClass="btn btn-success"
                                                                        Text='<%#(int)Eval("flagDisplay")==0?"تایید نمره":"نمایش "%>' Width="65"  CommandName="modalOpenScore" CommandArgument='<%#Eval("RequestId")%>' ToolTip='<%#(int)Eval("flagDisplay")==0?"تایید نمره":"نمایش "%>'
                                                                        OnClick="modalOpenScore_Click" />

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
                            <asp:HiddenField ID="hdnRequestId" runat="server" />

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
