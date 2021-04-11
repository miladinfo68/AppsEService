<%--<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="CooperationRequest.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.CooperationRequest" %>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="CooperationRequest.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.CooperationRequest" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3 style="color: blue">لیست متقاضیان تدریس
    </h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function closeCustomConfirm1() {
            $find("<%=RadWindow1.ClientID %>").close();
        }
    </script>

    <div class="container">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="col-md-1">
                    <span style="color: red; font-size: small;">
                        <sup></sup>
                    </span>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lbl_Daneshkade" runat="server" Text=" :دانشکده *" Width="100px"></asp:Label>
                </div>
                <div class="col-md-4" style="direction: rtl">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_Daneshkade" runat="server" Width="180px" Height="36px" OnSelectedIndexChanged="ddl_Daneshkade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_Daneshkade" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="col-md-1">
                    <span style="color: red; font-size: small;">
                        <sup></sup>
                    </span>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lbl_EducationGroup" runat="server" Text=": گروه آموزشی *" Width="100px"></asp:Label>
                </div>
                <div class="col-md-4" style="direction: rtl">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <telerik:RadComboBox ID="RCB" runat="server" CheckBoxes="true" CssClass="form-control input-sm"
                                Width="180px" EnableCheckAllItemsCheckBox="true" AutoPostBack="false" OnItemChecked="RCB_ItemChecked"
                                OnSelectedIndexChanged="RCB_SelectedIndexChanged" OnItemsRequested="RCB_ItemsRequested">
                            </telerik:RadComboBox>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-2"></div>
        </div>
        <div class="row" style="padding-top: 10px;">
            <div class="col-md-4">
            </div>
            <div class="col-md-1" style="margin-right: 84px;">
                <asp:Button ID="btn_Show" runat="server" OnClick="btn_Show_Click" Text="نمایش اطلاعات" CssClass="btn btn-info" />
            </div>
            <div class="col-md-1" style="margin-right: 10px;">
                <asp:Button ID="btn_excel" runat="server" OnClick="btn_excel_Click" Enabled="false" Text="تبدیل به اکسل" CssClass="btn btn-success" />
            </div>
        </div>
    </div>
    <div dir="rtl">
        <telerik:RadGrid ID="grd_Show" runat="server" PageSize="50" BorderWidth="10px"
            AutoGenerateColumns="false" HorizontalAlign="Center" AllowPaging="true"
            OnItemCommand="grd_Show_ItemCommand"
            EnableEmbeddedSkins="false" AllowFilteringByColumn="True" OnExcelMLWorkBookCreated="grd_Show_ExcelMLWorkBookCreated" Skin="MyCustomSkin" OnNeedDataSource="grd_Show_NeedDataSource">
            <MasterTableView>
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <%# Container.ItemIndex + 1 %>
                        </ItemTemplate>

                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true" Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="namecoding" HeaderText="رشته" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sanavat_tadris" HeaderText="سنوات تدریس" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="نمایش" HeaderStyle-HorizontalAlign="Center" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:Button ID="btn_Show" runat="server" CommandName="Select2" CommandArgument='<%#Eval("ID")+","+Eval("name")+","+Eval("family")+","+Eval("namecoding")+","+Eval("sanavat_tadris")+","+Eval("mobile")+","+Eval("idd_Meli") %>' CssClass="btn btn-success" Width="150px" Text="مشاهده جزئیات" />
                        </ItemTemplate>

                        <ItemStyle></ItemStyle>
                    </telerik:GridTemplateColumn>

                    <telerik:GridTemplateColumn HeaderText="عملیات" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:Button ID="btn_Detail" runat="server" CommandName="Select" CommandArgument='<%#Eval("ID")+","+Eval("name")+","+Eval("family")+","+Eval("namecoding")+","+Eval("sanavat_tadris")+","+Eval("mobile")+","+Eval("idd_Meli") %>' CssClass="btn btn-success" Width="150px" Text="تأیید و انتقال به سیدا" />
                        </ItemTemplate>

                        <ItemStyle></ItemStyle>
                    </telerik:GridTemplateColumn>






                </Columns>
            </MasterTableView>


        </telerik:RadGrid>
    </div>
    <telerik:RadWindowManager ID="rwd" runat="server">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close, Move" Height="200px" Modal="true" VisibleStatusbar="false" Width="300px">
                <ContentTemplate>
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
                </ContentTemplate>
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="Lbl_Status" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_Final" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_CodeOstad" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_CodeMeli" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_mobile" runat="server" Visible="false"></asp:Label>
</asp:Content>
