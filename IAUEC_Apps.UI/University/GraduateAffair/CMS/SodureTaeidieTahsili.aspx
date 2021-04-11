<%@ Page Title="" Language="C#" MasterPageFile="~/University/ReportCMS/masterpage/ReportCMS.Master" AutoEventWireup="true" CodeBehind="SodureTaeidieTahsili.aspx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.SodureTaeidieTahsili" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <link href="../../Theme/css/StyleSheetCalendar.css" type="text/css" rel="stylesheet" />
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>


    <style>
        .label {
            font-weight: bold;
            font-family: 'B Nazanin';
            font-size: medium;
        }
    </style>


    <style>
        .onvan {
            font-weight: bold;
            font-family: 'B Nazanin';
            font-size: small;
            color: #2C3E50;
        }
    </style>


    <asp:Literal ID="pt" runat="server"></asp:Literal>

    <asp:Panel ID="pnl_Main" Font-Names="b nazanin" runat="server" DefaultButton="btn_Enter">
        <div class="container" style="background-color: #2C3E50; color: #3498DB; font-family: 'B Nazanin'; font-weight: bold; font-size: medium;" dir="rtl">
            <div class="row">
                <div class="col-md-4">
                    شماره دانشجویی:
                <asp:TextBox ID="txt_StudentNumber" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;
                 <a id="a_Search" runat="server" onserverclick="a_Search_ServerClick" class="btn btn-info">جستجو <i class="fa fa-search"></i></a>
                    <asp:Button runat="server" BorderStyle="None" ID="btn_Enter" OnClick="btn_Enter_Click" BackColor="#2C3E50" />
                    &nbsp;
                </div>
            </div>
            <div class="row>">
                <div class="col-md-4">
                    نام:
                   <asp:Label ID="lbl_Name" CssClass="label" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    نام خانوادگی:
                 <asp:Label ID="lbl_LastName" CssClass="label" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    شماره شناسنامه:
                 <asp:Label ID="lbl_Id" CssClass="label" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    نام پدر:
                 <asp:Label ID="lbl_FatherName" CssClass="label" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    رشته تحصیلی:
                  <asp:Label ID="lbl_Reshte" CssClass="label" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    مقطع و سیستم:
                  <asp:Label ID="lbl_maghta" CssClass="label" runat="server"></asp:Label>
                </div>
            </div>
            <br />
        </div>
    </asp:Panel>

    <br />


    <div class="container" style="font-family: 'B Nazanin';" dir="rtl">
        <telerik:RadGrid ID="grd_Govahi" runat="server" OnUpdateCommand="grd_Govahi_UpdateCommand" OnNeedDataSource="grd_Govahi_NeedDataSource" OnItemCommand="grd_Govahi_ItemCommand" AutoGenerateColumns="False" EnableEmbeddedSkins="false">
            <MasterTableView>

                <HeaderStyle Font-Size="Medium" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" BackColor="#0C84E4" />

                <Columns>

                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:HiddenField Value='<%#Eval("type_govahi") %>' ID="hd_Field" runat="server"></asp:HiddenField>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemStyle HorizontalAlign="Center" ForeColor="Black" Font-Size="Medium" />
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="TypeGovahi" HeaderText="نوع گواهی" AllowFiltering="true" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="name_bekoja" HeaderText="گیرنده" AllowFiltering="true" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="num_namehaz" HeaderText="شماره نامه دریافتی" AllowFiltering="true" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="date_namehaz" HeaderText="تاریخ نامه" AllowFiltering="true" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="date_sabt" HeaderText="تاریخ ثبت" AllowFiltering="true" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="time_sabt" HeaderText="ساعت ثبت" AllowFiltering="true" ItemStyle-Font-Size="Medium" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="Black">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Button ID="btn_taeid" Text="مشاهده گواهی" CssClass="btn alert-success" Font-Names="b nazanin" runat="server" CommandName="ShowReport" CommandArgument='<%#Eval("id")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Button ID="btn_Delete" Text="حذف" CssClass="btn alert-danger" Font-Names="b nazanin" runat="server" CommandName="DeleteGovahi" CommandArgument='<%#Eval("id")%>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridEditCommandColumn UniqueName="EditCommandColumn" EditText="ویرایش">
                        <ItemStyle Font-Names="b nazanin" CssClass="btn-link" ForeColor="Blue" />
                    </telerik:GridEditCommandColumn>
                </Columns>
                <EditFormSettings UserControlName="EditGovahi.ascx"
                    EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <div class="panel " id="div_Sabt" runat="server" visible="false" dir="rtl">
        <div class="panel-heading bg-blue-sky " style="font-weight: bold; font-size: medium; font-family: 'B Nazanin'">ثبت گواهی جدید</div>
        <div class="panel-body bg-warning">
            <div class="container " dir="rtl">
                <div class="row">
                    <div class="col-md-4 onvan">
                        <div class="col-md-3">
                            نوع گواهی:
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddl_Govahi" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <div class="col-md-4 onvan">
                        <div class="col-md-2">
                            ارائه به:
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="txt_Koja" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                    </div>
                    <div class="col-md-4 onvan">
                        <div class="col-md-3 onvan">
                            شماره نامه دریافتی:
                        </div>
                        <div class="col-md-3 onvan">
                            <asp:TextBox ID="txt_LtterNo" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3 onvan">
                        </div>

                    </div>

                </div>
                <br />
                <%-- <div class="row">
                    <div class="col-md-4 onvan">
                        <asp:CheckBox ID="chk_NamehNo" AutoPostBack="true" OnCheckedChanged="chk_NamehNo_CheckedChanged" runat="server" />
                        ایجاد شماره نامه
                    </div>

                    <div class="col-md-4 onvan">
                        <div class="col-md-2 onvan">
                            شماره نامه:
                        </div>
                        <div class="col-md-2 onvan" dir="rtl">
                            <asp:TextBox ID="txt_LetterNo" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3 onvan">
                        </div>


                    </div>

                    <div class="col-md-4 onvan">
                        <div class="col-md-3 onvan">
                            تاریخ نامه:
                        </div>
                        <div class="col-md-3 onvan">
                            <asp:TextBox  ID="txt_DateLetter" runat="server"></asp:TextBox>
                        </div>
                    </div>



                </div>--%>
                <br />
                <div class="row">
                    <div class="col-md-4 onvan" style="float;">
                        تاریخ نامه دریافتی:
                        <pdc:PersianDateTextBox ID="txt_Date" runat="server" DefaultDate="1394/01/01"
                            IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>

                    </div>
                    <div class="col-md-4 onvan">


                        <%-- <asp:CheckBox ID="GraduateInfo" AutoPostBack="true" OnCheckedChanged="GraduateInfo_CheckedChanged" runat="server" />
                        بدون مشخصات فارغ التحصیلی
                        --%>

                        <%-- <asp:CheckBox ID="chk_Nezam" AutoPostBack="true" OnCheckedChanged="chk_Nezam_CheckedChanged" runat="server" />
                        چاپ نظام وظیفه--%>
                    </div>

                    <div class="col-md-4 onvan">

                        <%-- <asp:CheckBox ID="chk_Codemelli" AutoPostBack="true" OnCheckedChanged="chk_Codemelli_CheckedChanged" runat="server" />
                        نمایش کدملی
                        --%>
                    </div>



                </div>
                <br />
                <div class="row">
                    <%--<div class="col-md-4 onvan">

                        <asp:CheckBox ID="chk_address" AutoPostBack="true" OnCheckedChanged="chk_address_CheckedChanged" runat="server" />
                        بدون آدرس
                       
                    </div>--%>
                </div>
                <br />
                <div class="row">

                    <%--<div class="col-md-6 onvan">
                        شرح:
                         <asp:TextBox ID="txt_Sharh" Width="500px" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>--%>
                    <%--<div class="col-md-6 onvan">
                        آدرس گیرنده:
                        <asp:TextBox ID="txt_Address" Width="500px" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>--%>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="col-md-4 onvan">
                    </div>
                    <div class="col-md-4 onvan">
                        <div class="col-md-4 onvan">
                        </div>
                        <div class="col-md-4 onvan">
                            <asp:Button ID="btn_Save" OnClick="btn_Save_Click" CssClass="btn btn-success" runat="server" Text="ثبت" />
                        </div>
                        <div class="col-md-4 onvan">
                        </div>
                    </div>
                    <div class="col-md-4 onvan">
                    </div>
                </div>

            </div>

        </div>

    </div>
    <div>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />


    </div>
</asp:Content>
