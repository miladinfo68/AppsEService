<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" CodeBehind="NotacceptableRequest.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.NotacceptableRequest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>لیست اساتید رد شده</title>
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
        function closeModal() {
            $('#exampleModal').modal('hide');
        }
        function radModal() {
            $('#rad_modal').modal('show');
        }
        function closeradModal() {
            $('#rad_modal').modal('hide');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2 style="color: #006400">لیست اساتید رد شده
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                <div class="col-md-5">
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btn_excel" runat="server" OnClick="btn_excel_Click" Enabled="true" Text="تبدیل به اکسل" CssClass="btn btn-success" />
                </div>
                <div class="col-md-5">
                </div>
            </div>
    <div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="grd_Show">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grd_Show"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div dir="rtl">
            <telerik:RadGrid ID="grd_Show" runat="server" PageSize="50" BorderWidth="10px"
                AutoGenerateColumns="false" HorizontalAlign="Center" OnExcelMLWorkBookCreated="grd_Show_ExcelMLWorkBookCreated" OnNeedDataSource="grd_Show_NeedDataSource" AllowPaging="true"
                OnItemCommand="grd_Show_ItemCommand"
                EnableEmbeddedSkins="false" AllowFilteringByColumn="True" Skin="MyCustomSkin">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <MasterTableView DataKeyNames="ID">

                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-Font-Bold="False">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true" Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="نمایش" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Button ID="btn_Detail" Text="انتقال به در حال بررسی" runat="server" CommandName="Detail" CommandArgument='<%#Eval("idd_meli") %>' CssClass="btn btn-success" Width="150px" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>






                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            
        </div>


        <telerik:RadWindowManager ID="rwd" runat="server">
        </telerik:RadWindowManager>
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="exampleModalLabel">پیام</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid" dir="rtl">
                            <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                <div class="rwDialogPopup radconfirm">
                                    <div class="rwDialogText">
                                        <asp:Literal ID="confirmMessage" runat="server" Text="" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="rbConfirm_OK1" runat="server" OnClick="rbConfirm_OK1_Click" Text="بله">
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="rbConfirm_Cancel1" runat="server" OnClientClicked="closeCustomConfirm1" Text="خیر">
                                        </telerik:RadButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="rad_modal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H1">پیام</h4>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid" dir="rtl">
                            <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                <div class="rwDialogPopup radconfirm">
                                    <div class="rwDialogText">
                                        <asp:Literal ID="MsgConf" runat="server" Text="آیا از انتقال استاد به در حال بررسی اطمینان دارید؟ " />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="RadButton1" runat="server" OnClick="RadButton1_Click" Text="بله">
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="RadButton2" OnClientClicked="closeradModal()" runat="server" Text="خیر">
                                        </telerik:RadButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
