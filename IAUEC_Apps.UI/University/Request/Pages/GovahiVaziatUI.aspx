<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/PageRequestMaster.Master" AutoEventWireup="true" CodeBehind="GovahiVaziatUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.GovahiVaziatUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">

    <link href="../../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .RadGrid .rgFilterRow > td, .RadGrid_MyCustomSkin .rgAltRow td {
            border: solid #00C851;
            border-width: 0 0 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgFilterRow > td:first-child {
            border-width: 0px 1px 1px;
        }

        .RadGridRTL_MyCustomSkin .rgRow > td:first-child, .RadGridRTL_MyCustomSkin .rgAltRow > td:first-child {
            border-right-width: 1px;
        }


        .RadGrid_MyCustomSkin th.rgSorted {
            background-color: #3498DB;
        }

        .RadGrid_MyCustomSkin .rgHeader a {
            color: white;
        }

        .RadGrid .rgRow > td, .RadGrid .rgAltRow > td, .RadGrid .rgEditRow > td, .RadGrid .rgFooter > td, .RadGrid .rgFilterRow > td, .RadGrid .rgHeader, .RadGrid .rgResizeCol, .RadGrid .rgGroupHeader td {
            padding-left: 20px !important;
        }

        .RadGrid .rgFilterRow input {
            height: 25px;
        }

        .btn-width {
            width: 106px;
        }

        .payBtn {
            float: left;
            margin-left: 7%;
            width: 106px;
            position: relative;
        }

        .header-inline-display {
            display: inline-block;
        }

        .inlineTextbox {
            border-radius: 5px;
            height: 40px;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script language="javascript" type="text/javascript">
        function postRefId(refIdValue) {
            var form = document.createElement("form");
            form.setAttribute("method", "POST");
            form.setAttribute("action", "<%= PgwSite %>");
            form.setAttribute("target", "_self");
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("name", "RefId");
            hiddenField.setAttribute("value", refIdValue);
            form.appendChild(hiddenField);
            document.body.appendChild(form);
            form.submit();
            document.body.removeChild(form);
        }
    </script>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>

    <h3>درخواست ارسال گواهی اشتغال به تحصیل از طریق پست</h3>
    <br />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="main" style="background: #fff no-repeat scroll 0% 0%">

        <div class="row" style="margin: 0 auto;">
            <div class="alert alert-warning" style="text-align: right; font-size: 16px;">
                <img src="../Files/warning (4).png" style="width: 2%; margin-left: 2px;" id="imgIcon1" alt="" />
                <div class="header-inline-display" style="margin-bottom: 2%">در صورتیکه که درخواست شما نياز به ويرايش داشته باشد، می توانید محل ارائه را ویرایش نمایید و مجددا آن را ارسال نمایید. <u style="color: red">در این صورت هزینه ای از شما اخذ نخواهد شد.</u></div>
                <br />

                <img src="../Files/warning (4).png" style="width: 2%; margin-left: 2px;" id="imgIcon1" alt="" />
                <div class="header-inline-display">در صورتیکه درخواست شما به علت <u style="color: red">عدم ثبت نام در ترم جاری</u> رد گردید،می توانید پس از ثبت نام درخواست جدید ثبت نمایید.<u style="color: red"> در این صورت هزینه ای از شما اخذ نخواهد شد.</u></div>


            </div>
        </div>

        <telerik:RadGrid ID="grd_GovahiRequestState"
            AutoGenerateColumns="false" runat="server"
            OnItemCommand="grd_GovahiRequestState_ItemCommand"
            Visible="false" OnItemDataBound="grd_GovahiRequestState_ItemDataBound"
            OnNeedDataSource="grd_GovahiRequeststate_NeedDataSource"
            Skin="MyCustomSkin"
            EnableEmbeddedSkins="False">
            <MasterTableView DataKeyNames="RequestId">
                <NoRecordsTemplate>
                    <div class="alert alert-danger" style="text-align: center; margin-top: 2%; margin-left: 1%; margin-right: 1%;">
                        <h5>هیچ درخواستی وجود ندارد</h5>
                    </div>
                </NoRecordsTemplate>
                <ItemStyle />
                <HeaderStyle Font-Names="b nazanin" HorizontalAlign="Center" />

                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate>
                            <%# Container.ItemIndex + 1 %>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn DataField="era" HeaderText="ارائه به" />
                    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">
                        <HeaderTemplate>وضعیت</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button ID="btn_Pay" Visible="false" Text="پرداخت" runat="server" CommandArgument='<%#Eval("RequestId") +","+Eval("era") %>' CommandName="pay"></asp:Button>
                            <asp:Label ID="lbl_vaziat" Visible="false" Text='<%#Eval("vaziat")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <HeaderTemplate>کد مرسوله پستی</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_codeposti" runat="server" Enabled="false"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <HeaderTemplate>علت رد یا ویرایش درخواست</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_DalileRad" runat="server" Enabled="false" TextMode="MultiLine"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="عملیات" UniqueName="operator">
                        <ItemTemplate>
                            <div class="form-inline">
                                <asp:Button ID="btn_Del" CommandArgument='<%#Eval("RequestId")+","+Eval("PaymentID") %>' CommandName="Del" CssClass="btn btn-danger btn-width" Visible="false" runat="server" Text="حذف درخواست" />
                                <asp:TextBox ID="txt_EditEraeBe" CssClass="form-control inlineTextbox" runat="server" Visible="false"></asp:TextBox>
                                <asp:Button ID="btn_Sabt" CssClass="btn btn-success btn-width" runat="server" Visible="false" Text="ثبت ویرایش" CommandName="EditEraeBe" CommandArgument='<%#Eval("RequestId")%>' />
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
        <telerik:RadGrid ID="grd_pay" CssClass="RadGrid_Silk" EnableEmbeddedSkins="false" AutoGenerateColumns="false" runat="server" OnItemCommand="grd_GovahiRequestState_ItemCommand" Visible="false" OnNeedDataSource="grd_pay_NeedDataSource">
            <MasterTableView>
                <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" HorizontalAlign="Center" />
                <ItemStyle BackColor="White" />
                <AlternatingItemStyle BackColor="#eeeeee" />
                <Columns>
                    <telerik:GridBoundColumn DataField="mablagh" HeaderText="مبلغ قابل پرداخت(ریال)" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black" />
                    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center">

                        <ItemTemplate>
                            <asp:Button ID="btn_Pay" Visible="true" Text="پرداخت" runat="server" CommandName="pay" CssClass="btn btn-info payBtn btn-width"></asp:Button>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>

        <telerik:RadWindowManager ID="rwm_Validations" runat="server">
        </telerik:RadWindowManager>
    </div>
</asp:Content>
