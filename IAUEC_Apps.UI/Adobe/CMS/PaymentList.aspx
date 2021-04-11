<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="PaymentList.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.PaymentList" %>

<%@ Register Src="../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../University/Theme/css/Grid.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Input.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <link href="../../University/Theme/css/Ajax.MyCustomSkin.css" rel="stylesheet" />
    <!-- import the Jalali Date Class script -->
    <link rel="stylesheet" type="text/css" media="all" href="../css/aqua/theme.css" title="Aqua" />
    <script type="text/javascript" src="../js/jalali.js"></script>

    <!-- import the calendar script -->
    <script type="text/javascript" src="../js/calendar.js"></script>

    <!-- import the calendar script -->
    <script type="text/javascript" src="../js/calendar-setup.js"></script>

    <!-- import the language module -->
    <script type="text/javascript" src="../js/lang/calendar-fa.js"></script>
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal>
    </title>
    <style>
        .GridViewEditRow input[type=text] {
            width: 100px;
            text-align: center;
            background-color: darkgray;
            text-decoration-color: aquamarine;
        }

        
        /* size textboxes */
        .GridViewEditRow select {
            width: 100px;
            text-align: center;
            background-color: darkslateblue;
            text-shadow: initial;
            border-bottom-color: aquamarine;
            text-decoration-color: ivory;
        }
        /* size drop down lists */

        .cssPager span {
            background-color: #cccccc !important;
            color: black !important;
            font-family: 'B Yekan' !important;
            font-size: 24px !important;
            padding-left: 5px;
            padding-right: 5px;
        }

        .cssPager a {
            background-color: #ffffff !important;
            color: #808080 !important;
            font-family: 'B Yekan' !important;
            font-size: 22px !important;
            padding-left: 5px;
            padding-right: 5px;
        }
    </style>
    <style>
        .marginItem {
            margin: 10px;
        }

        .paddingItem {
            padding: 20px;
        }

        .centerItem {
            text-align: center !important;
        }
        .adelDirection{
            direction: rtl;
        }
    </style>

</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AccessControl ID="AccessControl1" runat="server" />
    <table dir="rtl" style="width: 100%">
        <tr>
            <td>جمع در تاریخ انتخاب شده:</td>
            <td>
                <asp:Label ID="lblsum" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>از تاریخ:</td>
            <td>
                <div class="example">
                    <input id="date_input_1" type="text" runat="server" /><img id="date_btn_1" src="../images/cal.png" style="vertical-align: top;" />
                    <script type="text/javascript">
                        Calendar.setup({
                            inputField: "ContentPlaceHolder1_date_input_1",   // id of the input field
                            button: "date_btn_1",   // trigger for the calendar (button ID)
                            ifFormat: "%Y/%m/%d",       // format of the input field
                            dateType: 'jalali',
                            weekNumbers: false
                        });
                    </script>
                    <script type="text/javascript">
                        setActiveStyleSheet(document.getElementById("defaultTheme"), "Aqua");
                    </script>
                </div>
            </td>
            <td>تا تاریخ:</td>
            <td>
                <div>
                    <input id="date_input_2" type="text" runat="server" /><img id="date_btn_2" src="../images/cal.png" style="vertical-align: top;" />
                    <script type="text/javascript">
                        Calendar.setup({
                            inputField: "ContentPlaceHolder1_date_input_2",   // id of the input field
                            button: "date_btn_2",   // trigger for the calendar (button ID)
                            ifFormat: "%Y/%m/%d",       // format of the input field
                            dateType: 'jalali',
                            weekNumbers: false
                        });
                    </script>
                </div>
            </td>
            <td>
                <asp:Button ID="btn_select" runat="server" Text="نمایش" OnClick="btn_select_Click" /></td>
            <td>
                <asp:ImageButton ID="ExportToExcelImg1" runat="server" ImageUrl="~/Adobe/images/Excel02.jpg"
                    AlternateText="ExcelML" OnClick="ExportToExcelImg_Click" /></td>
        </tr>
    </table>
    <telerik:RadGrid ID="grd_PaymentList" runat="server" AllowPaging="True" PageSize="30" Skin="MyCustomSkin" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" OnNeedDataSource="grd_PaymentList_NeedDataSource" OnItemCommand="grd_PaymentList_ItemCommand">
        <MasterTableView AutoGenerateColumns="false">
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
            <EditFormSettings>
                <EditColumn CancelImageUrl="Cancel.gif" InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif">
                </EditColumn>
            </EditFormSettings>
            <ItemStyle Font-Names="tahoma" Font-Size="Small" />
            <AlternatingItemStyle Font-Names="tahoma" Font-Size="Small" />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="stcode" HeaderText="شماره دانشجویی" FilterListOptions="VaryByDataType" UniqueName="studentCode">
                </telerik:GridBoundColumn>

                <telerik:GridBoundColumn DataField="OrderId" HeaderText="شماره سفارش" AllowFiltering="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Amount" HeaderText="مبلغ(ریال)" DataFormatString="{0:N0}" AllowFiltering="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PersianDate" HeaderText="تاریخ" AllowFiltering="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TraceNumber" HeaderText="شماره پیگیری" AllowFiltering="False">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AppStatus" HeaderText="وضعیت پرداخت" AllowFiltering="False">

                    <ItemStyle ForeColor="#0099ff" />
                </telerik:GridBoundColumn>

                <telerik:GridTemplateColumn DataField="Detail" HeaderText="جزئیات پرداخت" AllowFiltering="False">
                    <ItemTemplate>
                        <%--<asp:Button ID="btnDetail" runat="server" Text="جزئیات پرداخت"  OnClick="btnDetail_Click"/>--%>
                        <telerik:RadButton Text="جزئیات پرداخت" CommandArgument='<%# Eval ( "OrderId" ) %>' CommandName="detail" runat="server"></telerik:RadButton>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <div></div>

    <telerik:RadWindow ID="RadWindow1" AutoSize="false" Height="600" runat="server" Width="1050" CssClass="adelDirection">
        <ContentTemplate>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
                <ContentTemplate>
                    <div class="container" style="padding: 10px; margin: 10px; overflow: visible" dir="rtl">
                        <div class="heading">
                            <h4>جزئیات پرداخت :</h4>
                        </div>
                        <div class="row">

                            <div class="col-sm-9" style="overflow: visible !important">
                                <asp:GridView ID="grdDateTime" runat="server" 
                                    
                                    DataKeyNames="PersianDate"
                                    OnRowDataBound="grdDateTime_RowDataBound"
                                    OnRowCancelingEdit="grdDateTime_RowCancelingEdit"
                                    OnRowEditing="grdDateTime_RowEditing"
                                    AutoGenerateColumns="false" CssClass="table table-bordered table-condensed table-stripted">
                                    <HeaderStyle CssClass="bg-primary" />
                                    <EditRowStyle CssClass="GridViewEditRow" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ردیف">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="تاریخ پرداخت">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" Text='<%# Eval("PersianDate").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="شماره سفارش ">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" Text='<%# Eval("OrderID").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="شناسه پرداخت">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" Text='<%# Eval("PayId").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="نام درس">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" Text='<%# Eval("namedars").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="تاریخ برگزاری">
                                            <ItemTemplate>
                                                <asp:Label ID="Label9" Text='<%# Eval("FileDate").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="زمان کلاس">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" Text='<%# Eval("saatklass").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ترم کلاس">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" Text='<%# Eval("tterm").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnfReqId" runat="server" />
                            </div>


                        </div>
                    </div>
                    <script type="text/javascript">
                        function GetRadWindow() {
                            var oWindow = null;
                            if (window.radWindow)
                                oWindow = window.radWindow;
                            else if (window.frameElement && window.frameElement.radWindow)
                                oWindow = window.frameElement.radWindow;
                            return oWindow;
                        }

                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>
