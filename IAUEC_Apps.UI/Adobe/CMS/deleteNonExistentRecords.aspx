<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" CodeBehind="deleteNonExistentRecords.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.deleteNonExistentRecords" %>


<%@ Register Src="../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
  <link rel="stylesheet" type="text/css" media="all" href="../css/aqua/theme.css" title="Aqua" />

    <script type="text/javascript" src="../js/jalali.js"></script>

    <!-- import the calendar script -->
    <script type="text/javascript" src="../js/calendar.js"></script>

    <!-- import the calendar script -->
    <script type="text/javascript" src="../js/calendar-setup.js"></script>

    <!-- import the language module -->
    <script type="text/javascript" src="../js/lang/calendar-fa.js"></script>
</asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table" style="width: 100%">
        <div class="row">
            <div class="col-md-2">
                <span class="left">ترم را انتخاب فرمایید:
                </span>
            </div>
            <div class="col-md-2">
                <telerik:RadComboBox CssClass="form-control" ID="cmbTerm" runat="server" AutoPostBack="True" EmptyMessage="انتخاب نمایید" OnSelectedIndexChanged="cmbTerm_SelectedIndexChanged"></telerik:RadComboBox>
            </div>
            <div class="col-md-2">از تاریخ:</div>
            <div class="col-md-2">
                <div class="example">
                    <input id="txtFromDate" type="text" runat="server" />
                    <img id="date_btn_1" src="../images/cal.png" style="vertical-align: top;" />
                    <script type="text/javascript">

                        Calendar.setup({
                            inputField: "ContentPlaceHolder1_txtFromDate",   // id of the input field
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
            </div>
            <div class="col-md-2">تا تاریخ:</div>
            <div class="col-md-2">
                <div>
                    <input id="txtToDate" type="text" runat="server" />
                    <img id="date_btn_2" src="../images/cal.png" style="vertical-align: top;" />
                    <script type="text/javascript">

                        Calendar.setup({
                            inputField: "ContentPlaceHolder1_txtToDate",   // id of the input field
                            button: "date_btn_2",   // trigger for the calendar (button ID)
                            ifFormat: "%Y/%m/%d",       // format of the input field
                            dateType: 'jalali',
                            weekNumbers: false
                        });
                    </script>
                </div>
                <uc1:AccessControl ID="AccessControl1" runat="server" />
            </div>

        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4 text-center">
                <asp:Button ID="btnShowFolders" runat="server" Font-Bold="True" CssClass="btn btn-danger form-control" OnClick="btnShowFolders_Click" Text="نمایش" />
            </div>
            <div class="col-md-4"></div>

        </div>
    </div>
    <div class="table">
        <div class="row">

            <div class="col-md-12 form-control" style="background-color: #e0dfdf; border-radius: 5px; border: 1px solid #808080; padding: 5px; margin: 5px 0px 5px 0px">
                <p style="font-weight: bold">فایل های ناموجود</p>
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="حذف فایل های ناموجود" Font-Names="tahoma" Font-Size="Small" Font-Bold="True" />


                <telerik:RadGrid ID="grd_NonExistFolder" runat="server" AutoGenerateColumns="false" AllowMultiRowSelection="True" OnNeedDataSource="grd_NonExistFolder_NeedDataSource" Skin="Sunset" AllowSorting="True">
                    <MasterTableView>
                        <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" />
                        <CommandItemStyle Font-Names="tahoma" />
                        <ItemStyle HorizontalAlign="Center" />
                        <AlternatingItemStyle HorizontalAlign="Center" />
                        <Columns>
                            <telerik:GridTemplateColumn ItemStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Width="20px" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="headerChkbox_CheckedChanged"
                                        AutoPostBack="True" />
                                </HeaderTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>

                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="term" HeaderText="ترم" AllowSorting="false"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="class_code" HeaderText="شناسه کلاس"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fileDate" HeaderText="تاریخ"></telerik:GridBoundColumn>


                            <telerik:GridTemplateColumn Visible="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="filedate" Value='<%#Eval("FileDate") %>' runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

        </div>
    </div>

    <uc1:AccessControl ID="AccessControl" runat="server" />

</asp:Content>
