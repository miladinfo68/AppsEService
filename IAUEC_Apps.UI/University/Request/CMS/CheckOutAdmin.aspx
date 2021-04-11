<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" AutoEventWireup="true" CodeBehind="CheckOutAdmin.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.CheckOutAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
     <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
      <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        th {
            text-align: center;
            border: 1px solid #808080;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
            </script>
    <script type="text/javascript">
        (function (global, undefined) {
            function confirmAspButton(button) {
                function aspButtonCallbackFn(arg) {
                    if (arg) {
                        __doPostBack(button.name, "");
                    }
                }
                radconfirm("آیا مطمئن هستید که می خواهید این درخواست را ارسال کنید ؟", aspButtonCallbackFn, 330, 180, null, "Confirm");
            }
            global.confirmAspButton = confirmAspButton;
        })(window);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>پنل مدیریت تسویه حساب</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <div class="row">
        <div class="col-sm-6">
            <div class="panel panel-primary " dir="rtl">
                <div class="panel-heading">
                    <h4>جستجو بر اساس شماره دانشجویی</h4>
                </div>
                <div class="panel-body">
                    <asp:Label Text="شماره دانشجویی :" runat="server" />
                    <asp:TextBox runat="server" ID="txtStCode" />
                    <asp:Button runat="server" ID="btnSreach" Text="جستجو" CssClass="btn btn-primary" OnClick="btnSreach_Click" ValidationGroup="a" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStCode" Display="Dynamic" ErrorMessage="لطفا شماره دانشجویی را وارد نمایید" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div id="dvRequestType" runat="server" class="col-sm-6">
            <div class="panel panel-primary" dir="rtl" style="height: 131px;">
                <div class="panel-heading">
                    <h4>نمایش لیست بر اساس نوع تسویه حساب</h4>
                </div>
                <div class="panel-body">

                    <asp:DropDownList runat="server" ID="drpCheckOutType" AutoPostBack="True" OnSelectedIndexChanged="drpCheckOutType_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-1">
            <asp:ImageButton ID="btnDlExcel" runat="server" ImageUrl="~/University/Theme/images/Excel_ExcelML.png" 
                OnClick="btnDlExcel_Click" AlternateText="دانلود فایل اکسل" ToolTip="دانلود فایل اکسل" />
        </div>
    </div>
    <div class="row" dir="rtl">
        <div class="table-responsive">
            <telerik:RadGrid ID="grd_CheckOutRequest" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="100" AllowFilteringByColumn="true"  EnableEmbeddedSkins="false" Skin="MyCustomSkin" OnItemCommand="grd_CheckOutRequest_ItemCommand" OnItemDataBound="grd_CheckOutRequest_ItemDataBound" OnNeedDataSource="grd_CheckOutRequest_NeedDataSource">
                <MasterTableView>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="ردیف" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label ID="Label1" Text="<%# Container.ItemIndex + 1 %>" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="StudentRequestID" HeaderText="شماره درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CreateDate" HeaderText="تاریخ درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StCode" HeaderText="شماره دانشجویی"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام دانشجو" />
                        <telerik:GridBoundColumn DataField="nameresh" HeaderText="رشته تحصیلی"  AllowFiltering="true"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RequestTypeID" HeaderText="کد وضعیت"  AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RequestLogID" HeaderText="وضعیت"  AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Erae_Be" HeaderText=" فعلی وضعیت"  AllowFiltering="false" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RequestTypeName" HeaderText="نوع درخواست" AllowFiltering="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Note" HeaderText="مرحله قبلی" AllowFiltering="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="message" HeaderText="پیام کاربر" AllowFiltering="false"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="StudentMessage" HeaderText="پیام دانشجو" AllowFiltering="false"></telerik:GridBoundColumn>
                         <telerik:GridTemplateColumn HeaderText="مرحله فعلی" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="drpReqStatus"></asp:DropDownList>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn UniqueName="act" HeaderText="عملیات"  AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" Text="ارسال" runat="server" CommandName="send" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-danger" OnClientClick="confirmAspButton(this); return false;" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="ارسال پیام"  AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnSendMsg" Text="ارسال پیام" runat="server" CommandName="msg" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-dark" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="تاریخچه"  AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnHistory" Text="تاریخچه" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-primary" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <%--<asp:GridView runat="server" ID="grdCheckOutRequest" CssClass="table table-bordered table-condensed table-striped"
                OnPageIndexChanging="grdCheckOutRequest_PageIndexChanging" DataKeyNames="StudentRequestID"
                AutoGenerateColumns="false" OnRowDataBound="grdCheckOutRequest_RowDataBound"
                OnRowCommand="grdCheckOutRequest_RowCommand">
                <HeaderStyle CssClass="bg-blue text-center" />
                <Columns>
                    <asp:TemplateField HeaderText="ردیف">
                        <ItemTemplate>
                            <asp:Label Text='<%#Container.DataItemIndex+1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="شماره درخواست" DataField="StudentRequestID" />
                    <asp:BoundField HeaderText="تاریخ درخواست" DataField="CreateDate" />
                    <asp:BoundField HeaderText="شماره دانشجویی" DataField="StCode" />
                    <asp:BoundField HeaderText="نام دانشجو" DataField="name" />
                    <asp:BoundField HeaderText="کد وضعیت" DataField="RequestTypeID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                    <asp:BoundField HeaderText="نوع درخواست" DataField="RequestTypeName" />
                    <asp:BoundField HeaderText="مرحله قبلی" DataField="Note" />
                    <asp:BoundField HeaderText=" پیام کاربر" DataField="message" />
                    <asp:BoundField HeaderText=" پیام دانشجو" DataField="StudentMessage" />
                    <asp:TemplateField HeaderText="مرحله فعلی">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="drpReqStatus"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="عملیات" Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" Text="ارسال" runat="server" CommandName="send" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-danger" OnClientClick="confirmAspButton(this); return false;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ارسال پیام">
                        <ItemTemplate>
                            <asp:Button ID="btnSendMsg" Text="ارسال پیام" runat="server" CommandName="msg" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-dark" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="تاریخچه">
                        <ItemTemplate>
                            <asp:Button ID="btnHistory" Text="تاریخچه" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"StudentRequestID") %>' CssClass="btn btn-sm btn-primary" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings NextPageText="بعدی" PreviousPageText="قبلی" FirstPageText="صفحه اول" LastPageText="صفحه آخر" Mode="Numeric" Position="Bottom" />
                <PagerStyle CssClass="cssPager" />
            </asp:GridView>--%>
        </div>
    </div>
    <telerik:RadWindow runat="server" ID="RadWindow2" VisibleOnPageLoad="false">
        <ContentTemplate>
            <div dir="rtl">
                <asp:Label ID="Label1" Text="متن پیام" runat="server" />
                <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" MaxLength="1000" ValidationGroup="b"></asp:TextBox>
                <asp:Button ID="btnSubmitMsg" runat="server" Text="ثبت و ارسال پیام" CssClass="btn btn-success" OnClick="btnSubmitMsg_Click" ValidationGroup="b" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMsg" ErrorMessage="لطفا پیام خود را وارد نمایید" ForeColor="Red" ValidationGroup="b"></asp:RequiredFieldValidator>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>
    
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document" style="margin: 100px;">
            <div class="modal-content" style="width: 1000px;">
                <div class="modal-header" dir="rtl">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه درخواست</h4>
                </div>
                <div class="modal-body">
                    <div dir="rtl" style="font-size: medium">
                        <div class="row">
                            <div class="col-md-3">
                                <div id="info1" runat="server"></div>
                            </div>
                            <div class="col-md-3">
                                <div id="info2" runat="server"></div>
                            </div>
                            <div class="col-md-3">
                                <div id="info3" runat="server"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid" dir="rtl">
                    <div class="row" style="border: 1px solid rgba(59,131,255,0.9); background-color: rgba(59,131,255,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                نام کاربر
                            </div>
                            <div class="col-md-2">
                                تاریخ
                            </div>
                            <div class="col-md-2">
                                ساعت
                            </div>
                            <div class="col-md-3">
                                وضعیت
                            </div>
                            <div class="col-md-3">
                                توضیحات
                            </div>
                           
                        </div>
                    </div>
                    <div class="row" style="border: 1px solid rgba(59,131,255,0.7); background-color: rgba(59,131,255,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">

                        <asp:ListView ID="lst_history" runat="server">
                            <ItemTemplate>
                                <div class="col-md-12" style="border-style: none none solid none; border-width: 0px 0px 1px 1px; border-color: rgba(59,131,255,0.7);">
                                    <div class="col-md-2">
                                        <%#Eval("Name") %>
                                    </div>
                                    <div class="col-md-2">
                                        <%#Eval("LogDate") %>
                                    </div>
                                    <div class="col-md-2">
                                        <%#Eval("LogTime") %>
                                    </div>
                                    <div class="col-md-3">
                                        <%#Eval("EventName") %>
                                    </div>
                                    <div class="col-md-3">
                                        <%#Eval("Description") %>
                                    </div>
                                 
                                </div>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>

            </div>
        </div>
    </div>
</asp:Content>
