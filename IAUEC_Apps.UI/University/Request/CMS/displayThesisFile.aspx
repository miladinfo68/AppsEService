<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" CodeBehind="displayThesisFile.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.displayThesisFile" %>

<%@ Register Src="~/CommonUI/AccessControl.ascx" TagPrefix="uc1" TagName="AccessControl" %>

<asp:Content runat="server" ID="c1" ContentPlaceHolderID="HeaderplaceHolder">


    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>

</asp:Content>
<asp:Content runat="server" ID="c2" ContentPlaceHolderID="PageTitle">
    <asp:Literal ID="pt" runat="server"></asp:Literal>

</asp:Content>
<asp:Content runat="server" ID="c3" ContentPlaceHolderID="ContentPlaceHolder1">


    <%--<telerik:RadWindowManager ID="rwm" runat="server" Width="800px" Height="600px"></telerik:RadWindowManager>--%>

    <div class="container" dir="rtl">
        <div class="panel panel-warning">
            <div class="panel-heading ">
                <asp:Label Text="جستجو" runat="server"></asp:Label>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-1 "></div>
                    <div class="col-md-5">
                        <span>دانشکده</span>
                        <telerik:RadComboBox ID="ddlDaneshkade" runat="server" CheckBoxes="true" Filter="Contains" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="انتخاب همه" EmptyMessage="انتخاب دانشکده" AutoPostBack="true" OnItemChecked="ddlDaneshkade_ItemChecked" OnCheckAllCheck="ddlDaneshkade_CheckAllCheck"></telerik:RadComboBox>
                    </div>
                    <div class="col-md-5">

                        <span>رشته تحصیلی</span>
                        <telerik:RadComboBox ID="ddlField" Width="50%" AllowCustomText="true" runat="server" CheckBoxes="true" Filter="Contains" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="انتخاب همه" EmptyMessage="انتخاب رشته تحصیلی" AutoPostBack="true"></telerik:RadComboBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1 "></div>
                <div class="col-md-5 ">
                    <span>نام خانوادگی</span>
                    <asp:TextBox ID="txtFamily" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-5">
                    <span>کد دانشجویی</span>
                    <asp:TextBox ID="txtStcode" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="جستجو" CssClass="btn btn-warning" OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
        <div class="panel panel-success" runat="server">
            <div class="panel-heading">
                <span>لیست پایان نامه ها</span>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <asp:ImageButton ImageUrl="~/CommonUI/images/Excel02.jpg" ID="exportExcel" OnClick="exportExcel_Click" runat="server" />
                    </div>
                </div>
                <telerik:RadGrid ID="grdThesis" runat="server" AllowFilteringByColumn="true" MasterTableView-ShowHeadersWhenNoRecords="false" AutoGenerateColumns="false" OnExcelMLWorkBookCreated="grdThesis_ExcelMLWorkBookCreated" OnNeedDataSource="grdThesis_NeedDataSource" AllowPaging="true" PageSize="100" OnItemCommand="grdThesis_ItemCommand">
                    <MasterTableView NoMasterRecordsText="با این فیلترینگ، داده ای جهت نمایش وجود ندارد">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-Font-Bold="False">
                                <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="stcode" HeaderText="شماره دانشجویی" EmptyDataText=" - "></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="name" HeaderText="نام" EmptyDataText=" - "></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="family" HeaderText="نام خانوادگی" EmptyDataText=" - "></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="false" DataField="namedanesh" HeaderText="دانشکده" EmptyDataText=" - "></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="reshtegerayesh" HeaderText="رشته_گرایش" EmptyDataText=" - "></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="نمایش" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                <ItemTemplate>
                                    <asp:Button ID="btnShowThesisFile" Text="مشاهده پایان نامه" runat="server" CommandName="showThesis" CommandArgument='<%#Eval("thesis_pdf") %>' CssClass="btn btn-danger" />
                                    <asp:Button ID="btnShowThesisWordFile" Text="مشاهده فایل word پایان نامه" runat="server" CommandName="showThesis" CommandArgument='<%#Eval("thesis_doc") %>' CssClass="btn btn-info" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>

                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>

    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rbSubmitChanges">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rwShowThesis" />
                    <telerik:AjaxUpdatedControl ControlID="loadingPanelWrapper" LoadingPanelID="ralpConfiguration" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadCodeBlock ID="blk" runat="server">
        <script type="text/javascript">
            function openWin(id) {
                setTimeout(function () {
                    var oWnd = $find("<%=rwShowThesis.ClientID%>");
                    oWnd.setUrl('../../../CommonUI/showFile_Unprintable.aspx?q=' + id);
                    oWnd.show();
                    oWnd.center();
                }, 200);
            }
            function closeWin() {
                var oWnd = $find("<%=rwShowThesis.ClientID%>");
            oWnd.hide();
        }
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel runat="server" ID="ralpConfiguration"></telerik:RadAjaxLoadingPanel>

    <telerik:RadWindowManager ID="RWMThesis" runat="server">
        <Windows>
            <telerik:RadWindow ID="rwShowThesis" runat="server" MinWidth="920px" Height="750px"></telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <uc1:AccessControl runat="server" ID="AccessControl" />

</asp:Content>
