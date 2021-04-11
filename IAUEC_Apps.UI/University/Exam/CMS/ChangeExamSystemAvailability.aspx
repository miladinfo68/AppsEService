<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ChangeExamSystemAvailability.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ChangeExamSystemAvailability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <div dir="rtl">
        <asp:Panel ID="pnl_Main" runat="server" BorderColor="Black" BorderStyle="Groove" ForeColor="Black" Font-Size="Medium" Font-Bold="False">

            <table style="width: 800px;">
                <tr>
                    <td style="font-weight: bold">تغییر وضعیت آپلود سوالات امتحان</td>
                </tr>
                <tr>
                    <td>تغییر وضعیت کلی:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Status" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_Sabt" runat="server" Text="ثبت" BackColor="#9B59B6" Width="100px" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" OnClick="btn_Sabt_Click" />
                    </td>
                </tr>
            </table>
            <hr />

            <table>
                <tr style="font-weight: bold">
                    <td>باز کردن آپلود برای یک استاد
                    </td>
                </tr>
                <tr>
                    <td>کد استاد:
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CodeOs" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btn_Save" runat="server" Text="ثبت" BackColor="#9B59B6" Width="100px" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White" OnClick="btn_Save_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
