<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="EditAdmin.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.EditAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <div>
        <h1>مدیریت درخواستهای ویرایش</h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-3">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-exclamation-triangle fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right text-right">
                                <div class="huge">
                                    <asp:Label ID="lblWaitingForSend" runat="server" />
                                </div>
                                <div>درخواست های ویرایش مشخصات فردی</div>
                            </div>
                        </div>
                    </div>
                    <a id="waitForSend" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-exclamation-triangle fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right text-right">
                                <div class="huge">
                                    <asp:Label ID="Label1" runat="server" />
                                </div>
                                <div>درخواست های بروزرسانی حکم</div>
                            </div>
                        </div>
                    </div>
                    <a id="A1" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-warning">
                    <div class="panel-heading">
                        <div class="row">
                            <div class="col-xs-4">
                                <i class="fa fa-exclamation-triangle fa-5x"></i>
                            </div>
                            <div class="col-xs-8 text-right text-right">
                                <div class="huge">
                                    <asp:Label ID="Label2" runat="server" />
                                </div>
                                <div>درخواست های ویرایش مشخصات فردی</div>
                            </div>
                        </div>
                    </div>
                    <a id="A2" runat="server">
                        <div class="panel-footer">
                            <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                            <span class="pull-right">نمایش جزئیات</span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
