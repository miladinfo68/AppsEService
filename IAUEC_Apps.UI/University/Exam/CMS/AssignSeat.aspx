<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="AssignSeat.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.AssignSeat" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="content-section">
        <div id="div_Main" style="padding-left: 15%; padding-right: 5%;">
            <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="100%" Direction="RightToLeft">

                <center>
            <div id="main" dir="rtl">
                <fieldset>
                    <legend style="color: #000000">تخصیص شماره صندلی </legend>
                    <center>
                        <table style="width:100%" id="tbl_ExamDate">
                            <tr>
                                <td  style="font-family: 'B Nazanin'; font-size: medium; color: #000000;text-align:center">
                                   <asp:Label ID="lbl_DayExam" runat="server" Text="روز امتحان" ForeColor="Black" Font-Bold="True"></asp:Label>
                                     </td>
                                <td style="font-family: 'B Titr'; font-size: medium">
                                    <asp:DropDownList ID="ddl_ExamDate" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                                 <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000;text-align:center">
                                    <asp:Label ID="lbl_SaatExam" runat="server" Text="ساعت امتحان" ForeColor="Black" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="font-family: 'B Titr'; font-size: medium">
                                    <asp:DropDownList ID="ddl_ExamTime" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000;text-align:center">
                                    <asp:Label ID="lblCity" runat="server" Text="شهر" ForeColor="Black" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="font-family: 'B Titr'; font-size: medium">
                                    <asp:DropDownList ID="ddlCity" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div>
                        <asp:Button runat="server" onclick="btn_AssignSeat_Click" Text="تخصیص صندلی" CssClass="btn btn-success"  ID="btn_AssignSeat" />
                            </div>
                        <br />
                        <hr />
                        <div>
                             <legend style="color: #000000">حذف شماره صندلی </legend>
                            <table style="width:100%">
                                <tr>
                                <td  style="font-family: 'B Nazanin'; font-size: medium; color: #000000;text-align:center">
                                   <asp:Label ID="lbl_Day2" runat="server" Text="روز امتحان" ForeColor="Black" Font-Bold="True"></asp:Label>
                                     </td>
                                <td style="font-family: 'B Titr'; font-size: medium">
                                    <asp:DropDownList ID="ddl_Day2" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                                 <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000;text-align:center">
                                    <asp:Label ID="lbl_Time2" runat="server" Text="ساعت امتحان" ForeColor="Black" Font-Bold="True"></asp:Label>
                                      </td>
                                <td style="font-family: 'B titr'; font-size: medium">
                                    <asp:DropDownList ID="ddl_Time2" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                                    <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000;text-align:center">
                                    <asp:Label ID="lblCityDelete" runat="server" Text="شهر" ForeColor="Black" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="font-family: 'B Titr'; font-size: medium">
                                    <asp:DropDownList ID="ddlCityDelete" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                            </tr>
                            </table>
                               <br />
                            <div>

                             <asp:Button ID="btn_Delete" runat="server" OnClick="btn_Delete_Click"  Text="حذف"  CssClass="btn btn-danger" />
                     </div>
                        </div>
                        
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </center>
                </fieldset>
            </div>
        </center>
            </asp:Panel>
        </div>
    </div>


</asp:Content>



