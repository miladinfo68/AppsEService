<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="EducationManualDateDefence.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.EducationManualDateDefence" %>
<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
            <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>

    </title>
          <link href="../Style/js-persian-cal.css" rel="stylesheet" />
          <link href="../../University/Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <script>
    
        $(document).ready(function () {
            var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtDate',
                { extraInputID: 'ContentPlaceHolder1_txtDate', extraInputFormat: 'yyyy/mm/dd' });
        });
    </script>
        <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function closeRadWindowAverageIsLessThan14() {

                var window = $find('<%=rwAverageIsLessThan14.ClientID %>');
                window.close();
            }

</script>
    </telerik:RadScriptBlock>

     <style>
        a.pcalBtn {
            position: relative;
            margin-right: -22px;
            vertical-align: middle;
        }
        .modal{  opacity:1}
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

        img {
            margin-left: 2px;
        }

        .buttonmargin {
            margin-bottom: initial !important;
        }

        .textBoxSearch {
            border-radius: 5px !important;
        }

        .textAlignCenter {
            text-align: center;
            color: black;
        }

        .marginBtn {
            margin-bottom: initial !important;
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
            .btn{
                padding:2px !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
        <h3>
          <asp:Literal runat="server" ID="pt" Visible="true"></asp:Literal>
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
          <uc1:AccessControl runat="server" ID="AccessControl" />
    <telerik:RadWindow ID="RadWindow1" Height="300" Width="400" AutoSizeBehaviors="HeightProportional" runat="server">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
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
                                    <asp:Button ID="Button1" Text="تایید" OnClick="btnShowRadWindowSooratJalaseh_Click" CssClass="btn btn-success" runat="server" Style="width: 100%;" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="Button2" Text="لغــو" OnClick="btnCancleShowRadWindowSooratJalaseh_Click" CssClass="btn btn-default" runat="server" Style="width: 100%;" />
                                </div>
                            </div>
                        </div>
                    </div>
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
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
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
    <div class="container">

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
        <div class="row" dir="rtl" style="margin-right: 1%; margin-left: 1%">
            <div class="panel-group">
                <div class="panel panel panel-primary">
                    <div class="bg-purple">
                   <img src="../Images/notebook.png" style="width: 32px; float: right" alt=""/>
                        <h5 class="header-inline-display" style="font-family: 'B Titr'">ثبت دستی تاریخ دفاع پایان نامه</h5>
                    </div>
                    <div class="list-group-item" style="background: #a4935312">

                        <div style="border: 1px solid green; padding: 10px">
                            <div class="row">

                                <div class="col-md-4 ">
                                    
                                    <span style="font-size: 14px">شماره دانشجویی:
                                    </span>

                                    <asp:TextBox ID="txtstcode" runat="server" ValidationGroup="register"
                                        CssClass="pdate " MaxLength="14" ToolTip="شماره دانشجویی" />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtstcode" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا شماره دانشجویی  را وارد نمایید"></asp:RequiredFieldValidator>
                             
                                </div>
                                <div class="col-md-4 ">
                                    <img src="../fonts/calendar.png" style="width: 32px" alt="" />
                                    <span style="font-size: 14px">تاریخ صورتجلسه
                                    </span>

                                    <asp:TextBox ID="txtDate" runat="server"  CssClass="pdate" MaxLength="9" ToolTip=" تاریخ صورتجلسه " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="register" runat="server" CssClass="alert" ControlToValidate="txtDate" ForeColor="red" Display="Dynamic" ErrorMessage="لطفا تاریخ مورد درخواست را وارد نمایید"></asp:RequiredFieldValidator>
                                </div>
           
                                           <div class="col-md-4 ">
                                                    <p > <asp:CheckBox ID="ChkFinal" CssClass="form-check-input"  runat="server" Text=" صورتجلسه نهایی " />    </p>
                                </div>


                                <br />
                                <div class="row">
                                    <asp:Button ID="btnSave" runat="server" Text="صورتجلسه" ValidationGroup="register" Width="100px" OnClick="btnSave_Click" class="btn btn-success" Style="float: left; margin-top: 10px; margin-left: 20px" />
                                </div>
                                <br />
                                <br />
                            </div>
                        </div>
                 



                    </div>

                </div>
            </div>
        </div>
    </div>
       
        <script src="../Scripts/js-persian-cal.min.js"></script>
</asp:Content>
