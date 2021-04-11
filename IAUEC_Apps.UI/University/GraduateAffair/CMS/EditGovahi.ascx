<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditGovahi.ascx.cs" Inherits="IAUEC_Apps.UI.University.GraduateAffair.CMS.EditGovahi" %>
<%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="../../Theme/css/StyleSheetCalendar.css" type="text/css" rel="stylesheet" />
<%--<link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />--%>
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
    CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
    ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
    SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
</pdc:PersianDateScriptManager>
<div dir="rtl">
    <table dir="rtl" style="width:100%">
        <tr>
            <td>نوع گواهی:
            </td>
            <td>
                <asp:DropDownList ID="ddl_Govahi" runat="server"></asp:DropDownList>
            </td>
            <td>ارائه به:
            </td>
            <td>
                <asp:TextBox ID="txt_Koja" runat="server"></asp:TextBox>
            </td>
            <td>شماره نامه دریافتی:
            </td>
            <td>
                <asp:TextBox ID="txt_LtterNo" runat="server"></asp:TextBox>
            </td>
            <td>تاریخ نامه دریافتی:
            </td>
            <td>
                <pdc:PersianDateTextBox ID="txt_Date" runat="server" DefaultDate="1394/01/01"
                    IconUrl="~/University/Theme/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="100%"></pdc:PersianDateTextBox>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="btn_Save" CommandName="Update" CssClass="btn btn-success" runat="server" Text="ثبت ویرایش" />
            </td>
        </tr>
    </table>
</div>
<br />




