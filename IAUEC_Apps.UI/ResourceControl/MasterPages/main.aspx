<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Admin.main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>سامانه رزرواسیون کلاس های فیزیکی</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <hr />

    <div id="dvEducation" runat="server" class="row">
        <div id="dvWaitingForSend" runat="server" visible="false" class="col-lg-3 col-md-6">
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
                            <div>درخواست های منتظرارجاع</div>
                        </div>
                    </div>
                </div>
                <a id="waitForSend" runat="server">
                    <asp:LinkButton ID="btnWaitingForSend" runat="server" OnClick="btnWaitingForSend_Click">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>

                        <div class="clearfix"></div>
                   
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvSent" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-question-circle fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right">
                            <div class="huge">
                                <asp:Label ID="lblSent" runat="server" />
                            </div>
                            <div>درخواست های منتظر تایید</div>
                        </div>
                    </div>
                </div>
                <a id="sent" runat="server">
                    <asp:LinkButton ID="btnSend" runat="server" OnClick="btnSend_Click">
                    <div class="panel-footer">
                        
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvApproved" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-check-square fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right">
                            <div class="huge">
                                <asp:Label ID="lblApproved1" runat="server" />
                            </div>
                            <div>درخواست های تایید شده</div>
                        </div>
                    </div>
                </div>
                <a id="approved" runat="server">
                    <asp:LinkButton ID="btnApproved" runat="server" OnClick="btnApproved_Click">
                <div class="panel-footer">
                    <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                    <span class="pull-right">نمایش جزئیات</span>
                    <div class="clearfix"></div>
                </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvInformed" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-orange">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-bell-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblInformed" runat="server" />
                            </div>
                            <div>درخواست های اطلاع رسانی</div>
                        </div>
                    </div>
                </div>
                <a id="Informed" runat="server">
                    <asp:LinkButton ID="btnInformed" runat="server" OnClick="btnInformed_Click">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvDenied" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-remove fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblDenid1" runat="server" />
                            </div>
                            <div>درخواست های رد شده</div>
                        </div>
                    </div>
                </div>
                <a id="denied" runat="server">
                    <asp:LinkButton ID="btnDenied" runat="server" OnClick="btnDenied_Click">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <%--  panel denied 2 --%>
        <div id="dvDenied2" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-purple">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-frown-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblDenied2" runat="server" />
                            </div>
                            <div>درخواست های از دست رفته توسط دانشکده</div>
                        </div>
                    </div>
                </div>
                <a id="A1" runat="server">
                    <asp:LinkButton ID="btnDenied2" runat="server" OnClick="btnDenied2_Click">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <%--  panel denied 3 --%>
        <div id="dvDenied3" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-purple">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-frown-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblDenied3" runat="server" />
                            </div>
                            <div>درخواست های از دست رفته توسط اداری</div>
                        </div>
                    </div>
                </div>
                <a id="A2" runat="server">
                    <asp:LinkButton ID="btnDenied3" runat="server" OnClick="btnDenied3_Click">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>


        <div id="dvLost" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-purple">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-frown-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblLost" runat="server" />
                            </div>
                            <div>درخواست های از دست رفته</div>
                        </div>
                    </div>
                </div>

                <a id="lost" runat="server">
                    <asp:LinkButton ID="btnLost" runat="server" OnClick="btnLost_Click">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
    </div>



    <div id="dvEducation1"  runat="server" class="row">
<%--        <div style="border: 4px solid rgb(26, 130, 195);border-radius: 16px;background: #F5F7FA;padding: 7px 0;text-align: center; margin-bottom: 5px; margin-right: 1%; margin-left: 1%;">--%>
       
   <%--     </div>--%>
        
    <hr />

        <div id="dvWaitingForSend1" runat="server" visible="false" class="col-lg-4 col-md-7">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-exclamation-triangle fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblWaitingForSend1" runat="server" />
                            </div>
                            <div>درخواست های منتظرارجاع جلسه دفاع</div>
                        </div>
                    </div>
                </div>
                <a id="waitForSend1" runat="server">
                    <asp:LinkButton ID="btnWaitingForSend1" runat="server" OnClick="btnWaitingForSend1_OnClick">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>

                        <div class="clearfix"></div>
                   
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvSent1" runat="server" visible="false" class="col-lg-4 col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-question-circle fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right">
                            <div class="huge">
                                <asp:Label ID="lblSent1" runat="server" />
                            </div>
                            <div>درخواست های منتظر تایید جلسه دفاع</div>
                        </div>
                    </div>
                </div>
                <a id="sent1" runat="server">
                    <asp:LinkButton ID="btnSend1" runat="server" OnClick="btnSend1_OnClick">
                    <div class="panel-footer">
                        
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvApproved1" runat="server" visible="false" class="col-lg-4 col-md-7">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-check-square fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right">
                            <div class="huge">
                                <asp:Label ID="lblApproved11" runat="server" />
                            </div>
                            <div>درخواست های تایید شده جلسه دفاع</div>
                        </div>
                    </div>
                </div>
                <a id="approved1" runat="server">
                    <asp:LinkButton ID="btnApproved1" runat="server" OnClick="btnApproved1_OnClick">
                <div class="panel-footer">
                    <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                    <span class="pull-right">نمایش جزئیات</span>
                    <div class="clearfix"></div>
                </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvInformed1" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-orange">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-bell-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblInformed1" runat="server" />
                            </div>
                            <div>درخواست های اطلاع رسانی</div>
                        </div>
                    </div>
                </div>
                <a id="Informed1" runat="server">
                    <asp:LinkButton ID="btnInformed1" runat="server" OnClick="btnInformed1_OnClick">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <div id="dvDenied1" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-remove fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblDenid11" runat="server" />
                            </div>
                            <div>درخواست های رد شده</div>
                        </div>
                    </div>
                </div>
                <a id="denied1" runat="server">
                    <asp:LinkButton ID="btnDenied1" runat="server" OnClick="btnDenied1_OnClick">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <%--  panel denied 2 --%>
        <div id="dvDenied21" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-purple">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-frown-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblDenied21" runat="server" />
                            </div>
                            <div>درخواست های از دست رفته توسط دانشکده</div>
                        </div>
                    </div>
                </div>
                <a id="A11" runat="server">
                    <asp:LinkButton ID="btnDenied21" runat="server" OnClick="btnDenied21_OnClick">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
        <%--  panel denied 3 --%>
        <div id="dvDenied31" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-purple">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-frown-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblDenied31" runat="server" />
                            </div>
                            <div>درخواست های از دست رفته توسط اداری</div>
                        </div>
                    </div>
                </div>
                <a id="A21" runat="server">
                    <asp:LinkButton ID="btnDenied31" runat="server" OnClick="btnDenied31_OnClick">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>


        <div id="dvLost1" runat="server" visible="false" class="col-lg-3 col-md-6">
            <div class="panel panel-purple">
                <div class="panel-heading">
                    <div class="row">
                        <div class="col-xs-4">
                            <i class="fa fa-frown-o fa-5x"></i>
                        </div>
                        <div class="col-xs-8 text-right text-right">
                            <div class="huge">
                                <asp:Label ID="lblLost1" runat="server" />
                            </div>
                            <div>درخواست های از دست رفته</div>
                        </div>
                    </div>
                </div>

                <a id="lost1" runat="server">
                    <asp:LinkButton ID="btnLost1" runat="server" OnClick="btnLost1_OnClick">
                    <div class="panel-footer">
                        <span class="pull-left"><i class="fa fa-arrow-circle-left"></i></span>
                        <span class="pull-right">نمایش جزئیات</span>
                        <div class="clearfix"></div>
                    </div>
                    </asp:LinkButton>
                </a>
            </div>
        </div>
    </div>


</asp:Content>
