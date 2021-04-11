<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="GraduateForms.aspx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.GraduateForms" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <script src="../../Theme/js/js-persian-cal.min.js"></script>
    <link href="../../Request/MasterPages/css/js-persian-cal.css" rel="stylesheet" />

    <style>
        .NoMargin {
            margin: 0px;
        }

        .marginItem {
            margin-right: 20px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .marginMid {
            margin-right: 5px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .grid td, .grid th {
            text-align: center;
        }

        .drpText {
            text-align: center;
        }

        a.pcalBtn {
            margin-bottom: 3px;
            margin-right: 3px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="pnlSearch" BackColor="#ffffff" DefaultButton="btnSearch">
        <div class="container-fluid" dir="rtl">


            <div class="row">
                <div class="col-md-3" style="width: 230px; margin-top: 5px;">
                    <asp:Label runat="server" ID="lblStCode" CssClass="marginItem" Text="شماره دانشجویی: " Font-Size="Small" ForeColor="#474747"></asp:Label>
                    <asp:TextBox runat="server" ID="txtStCode" CssClass="marginMid" Width="100" MaxLength="14" direction="ltr"></asp:TextBox>
                </div>
                <div class="col-md-3" style="width: 230px; margin-top: 5px;">
                    <asp:Label runat="server" ID="lblFamilySearch" CssClass="marginItem" Text="نام خانوادگی: " Font-Size="Small" ForeColor="#474747"></asp:Label>
                    <asp:TextBox runat="server" ID="txtFamily" CssClass="marginMid" Width="120"></asp:TextBox>
                </div>
                <div class="col-md-3" style="width: 230px; margin-top: 5px;">
                    <asp:Label runat="server" ID="lblIDD" CssClass="marginItem" Text="کد ملی: " Font-Size="Small" ForeColor="#474747"></asp:Label>
                    <asp:TextBox runat="server" ID="txtIddMeli" AutoCompleteType="None" CssClass="marginMid" Width="120" MaxLength="10" direction="ltr"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="btn btn-primary marginMid" OnClick="btnSearch_Click" />
                    <asp:Button runat="server" ID="btnCancel" Text="پاک کردن صفحه" CssClass="btn btn-warning marginMid" OnClick="btnCancel_Click" />
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="grdResults" AutoGenerateColumns="false" runat="server" ShowHeaderWhenEmpty="True" Width="1000px" CssClass="grid" CellPadding="4" HorizontalAlign="Center" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdResults_PageIndexChanging" OnRowDataBound="grdResults_RowDataBound" OnSelectedIndexChanged="grdResults_SelectedIndexChanged" OnRowCommand="grdResults_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>

                                <asp:Button runat="server" ID="btnView" CssClass="btn-success" Text="انتخاب" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="btnView" />
                            </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="رديف" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" Text='<%# (Container.DataItemIndex + 1) %>' runat="server" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Button" Text="انتخاب" HeaderText="انتخاب" Visible="false" />
                        <asp:BoundField DataField="شماره دانشجویی" HeaderText="شماره دانشجویی" />
                        <asp:BoundField DataField="نام" HeaderText="نام" />
                        <asp:BoundField DataField="نام خانوادگی" HeaderText="نام خانوادگی" />
                        <asp:BoundField DataField="نام پدر" HeaderText="نام پدر" />
                        <asp:BoundField DataField="کد ملی" HeaderText="کد ملی" />
                        <asp:BoundField DataField="رشته - گرایش" HeaderText="رشته - گرایش" />
                        <asp:BoundField DataField="مقطع" HeaderText="مقطع" />
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </div>
            <br />
            <asp:Panel runat="server" ID="pnlInfo" BackColor="#EEEEEE">
                <br />
                <div class="row">
                    <div class="col-md-3" style="margin-right: 50px; padding-left: 0px; padding-right: 0px">
                        <asp:Label runat="server" ID="lblNameCaption" CssClass="marginItem" Text="نام: " Font-Size="Medium" ForeColor="#474747"></asp:Label>
                        <asp:Label runat="server" ID="lblName" CssClass="marginMid" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="col-md-5" style="padding-left: 0px; padding-right: 0px">
                        <asp:Label runat="server" ID="lblFamilyCaption" CssClass="marginItem" Text="نام خانوادگی: " Font-Size="Medium" ForeColor="#474747"></asp:Label>
                        <asp:Label runat="server" ID="lblFamily" CssClass="marginMid" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="col-md-3" style="padding-left: 0px; padding-right: 0px">
                        <asp:Label runat="server" ID="lblSSNCaption" CssClass="marginItem" Text="کد ملی: " Font-Size="Medium" ForeColor="#474747"></asp:Label>
                        <asp:Label runat="server" ID="lblSSN" CssClass="marginMid" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="margin-right: 50px; padding-left: 0px; padding-right: 0px">
                        <asp:Label runat="server" ID="lblFatherNameCaption" CssClass="marginItem" Text="نام پدر: " Font-Size="Medium" ForeColor="#474747"></asp:Label>
                        <asp:Label runat="server" ID="lblFatherName" CssClass="marginMid" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="col-md-5" style="padding-left: 0px; padding-right: 0px">
                        <asp:Label runat="server" ID="lblReshteCaption" CssClass="marginItem" Text="رشته: " Font-Size="Medium" ForeColor="#474747"></asp:Label>
                        <asp:Label runat="server" ID="lblReshte" CssClass="marginMid" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    </div>
                    <div class="col-md-3" style="padding-left: 0px; padding-right: 0px">
                        <asp:Label runat="server" ID="lblMaghtaCaption" CssClass="marginItem" Text="مقطع: " Font-Size="Medium" ForeColor="#474747"></asp:Label>
                        <asp:Label runat="server" ID="lblMaghta" CssClass="marginMid" Font-Size="Medium" ForeColor="Black"></asp:Label>
                    </div>
                </div>
                <br />
                <br />
                <asp:UpdatePanel runat="server" ID="UpdatePanel">
                    <ContentTemplate>
                        <div class="row marginItem" style="color: #474747; font-size: medium;">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-6 ">
                                <div class="row">
                                    <div class="col-md-6">
                                        فرم مورد نظر خود را انتخاب کنید:
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="drpForms" runat="server" Width="220px" CssClass="drpText form-control" AutoPostBack="true" OnSelectedIndexChanged="drpForms_SelectedIndexChanged">
                                            <asp:ListItem Text="-- انتخاب کنید ---" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="فرم شماره یک سازمان مرکزی" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="فرم پیش نویس فراغت از تحصیل" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="فرم ریز نمرات" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="فرم دروس گذرانده" Value="4"></asp:ListItem>
                                            <%--<asp:ListItem Text="استعلام گواهی موقت" Value="5"></asp:ListItem>--%>
                                            <%--<asp:ListItem Text="استعلام دانشنامه" Value="6"></asp:ListItem>--%>
                                            <asp:ListItem Text="گواهینامه موقت پایان تحصیلات" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="دانشنامه تحصیلی" Value="8"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="row" visible="false" runat="server" id="dvPayanShowType">
                                    <div class="col-md-6">
                                        وضعیت نمایش پایان نامه
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddl_PayanType" runat="server">
                                            <asp:ListItem Text="تمام پایان نامه ها و تمدید ها" Selected="True" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="آخرین پایان نامه" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row" visible="false" runat="server" id="dvEstelam">
                                    <div class="col-md-6 ">
                                        <asp:CheckBox ID="chkRizNomre" CssClass="form-control" runat="server" Visible="false" Checked="false" Text="ریز نمرات" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="drpInquiry" CssClass="drpText form-control" runat="server"></asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divLoanInfo" runat="server" class="row NoMargin" style="background-color: #FFF193; color: #383838;" visible="false">
                            <div class="row marginMid">
                                <div class="col-md-4">
                                    <asp:CustomValidator ID="revPayDate" runat="server" ErrorMessage="فرمت تاریخ صحیح نمی باشد" ControlToValidate="txtPayDate" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:CustomValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="تاریخ باز پرداخت بدهی را وارد کنید" ControlToValidate="txtPayDate" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:RequiredFieldValidator>
                                    باز پرداخت بدهی از تاریخ:                            
                    <asp:TextBox ID="txtPayDate" runat="server"></asp:TextBox>
                                    <script>
                                        var objCal1 = new AMIB.persianCalendar('<%=txtPayDate.ClientID%>',
                                            { extraInputID: '<%=txtPayDate.ClientID%>', extraInputFormat: 'yyyy/mm/dd' });
                                    </script>
                                </div>
                                <div class="col-md-4" style="padding-right: 35px;">
                                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="مبلغ پرداختی را وارد کنید" ControlToValidate="txtAmount" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:RequiredFieldValidator>
                                    مبلغ:
                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvAcountNumber" ErrorMessage="شماره حساب را وارد کنید" runat="server" ControlToValidate="txtAcountNumber" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:RequiredFieldValidator>
                                    شماره حساب جاری:
                    <asp:TextBox ID="txtAcountNumber" Width="197px" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row marginMid">
                                <div class="col-md-4" style="padding-right: 107px;">
                                    <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ErrorMessage="نام بانک را وارد کنید" ControlToValidate="txtBankName" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:RequiredFieldValidator>
                                    نام بانک:
                        <asp:TextBox ID="txtBankName" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" ErrorMessage="نام شعبه را وارد کنید" ControlToValidate="txtBranchName" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:RequiredFieldValidator>
                                    شعبه بانک:
                    <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="آدرس ارسال رسید را وارد کنید" ControlToValidate="txtAddress" ValidationGroup="ok" Text="*" ForeColor="Red" Font-Size="Medium"></asp:RequiredFieldValidator>
                                    آدرس ارسال رسید:
                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row marginMid">
                                <div class="row">
                                    <asp:ValidationSummary ID="vsError" runat="server" ForeColor="Red" ValidationGroup="ok" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpForms" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <div class="row">
                    <div class="col-md-5">
                    </div>
                    <div class="col-md-2">
                        <asp:Button runat="server" ID="btnOk" Text="تایید" ValidationGroup="ok" CssClass="btn btn-success marginMid" OnClick="btnOk_Click" />
                    </div>
                    <div class="col-md-5">
                    </div>
                </div>
            </asp:Panel>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
