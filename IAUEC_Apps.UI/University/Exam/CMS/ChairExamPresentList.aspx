<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ChairExamPresentList.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ChairExamPresentList" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <div id="ttlMsg">
        <h4> گزارش گیری بر اساس لیست صندلی های هرحوزه امتحانی</h4>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwmValidations" runat="server">
    </telerik:RadWindowManager>
    <br />
    <div id="content-section">
        <div id="div_Main" style="padding-left: 15%; padding-right: 5%;">
            <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="80%" Direction="RightToLeft">
                <div id="main" dir="rtl">
                    <%--  <center>--%>
                    <fieldset>
                        <%-- <legend style="color: #000000">لیست شماره صندلی</legend>--%>
                        <%-- <center>--%>
                        <table style="width: 100%; margin-bottom: 5px">
                            <tr>
                                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold;">دانشکده را انتخاب نمایید:
                                </td>
                                
                                <td>
                                    <asp:DropDownList ID="ddl_Danesh" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                                
                                <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold;">*ترم :
                                </td>
                                
                                <td>
                                    <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_ExamPlace" runat="server" Text="*محل امتحان:" Font-Bold="true" ForeColor="Black" Font-Names="B Nazanin" Font-Size="Medium"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ExamPlace2" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>


                                 <td>
                                    <asp:Label ID="lbl_ExamDay" runat="server" Text="روز امتحان:" Font-Bold="true" ForeColor="Black" Font-Names="B Nazanin" Font-Size="Medium"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ExamDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_ExamDate_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td></td>

                               <td>
                                    <asp:Label ID="lbl_ExamTime" runat="server" Text="ساعت امتحان:" Font-Bold="true" ForeColor="Black" Font-Names="B Nazanin" Font-Size="Medium"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_ExamTime" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </td>
                               
                            </tr>

                        </table>
                    </fieldset>
                    <asp:Button ID="btn_ChairExamListByPlace" runat="server" OnClick="btnChairExamListByPlace_Click" Text="نمایش اطلاعات" CssClass="btn btn-exam" />
                </div>
            </asp:Panel>
        </div>
        <div>
            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="WholeReport" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />
        </div>

        <uc1:AccessControl ID="AccessControl1" runat="server" />
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
            </center>
    </div>
    <div>
    </div>
    </div>

</asp:Content>

