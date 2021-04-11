<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ExamPresentList.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ExamPresentList" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .window {
            top: 0px;
        }

        .main_container{position:relative;}
        .loadingWrapper {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            background: rgba(0,0,0,0.7);
            direction: rtl;
            display: none;
            z-index:9999;
        }

        .loadingInner {
            width: 300px;
            height: 300px;
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            color: #fff;
            text-align: center;
            margin: 150px auto;
        }

            .loadingInner img {
                width: 200px;
                height: 200px;
                text-align: center;
            }

            .loadingInner div {
                margin-top: 20px;
                font-size: 26px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
    <div id="ttlMsg">
        <h4>گزارش گیری تعداد دانشجویان بر اساس لیست صورت جلسه حوزه امتحانی</h4>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwmValidations" runat="server"></telerik:RadWindowManager>
    <br />
    <div id="container">
        <div id="div_Main" style="padding-left: 15%; padding-right: 5%;">
            <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="100%" Direction="RightToLeft">
                <div id="content-section">
                    <div id="main" dir="rtl">
                        <fieldset>
                            <%--    <legend style="color: #000000">صورتجلسه امتحان </legend>--%>

                            <table style="width: 100%">
                                <tr>
                                    <td style="font-family: 'B Nazanin'; font-size: medium; color: #000000; font-weight: bold; width: 20%">*دانشکده را انتخاب نمایید:
                                    </td>
                                    <td style="width: 25%">
                                        <asp:DropDownList ID="ddl_Danesh" runat="server" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="Label1" runat="server" Text="*ترم :" Font-Bold="true" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Exam" runat="server" Text="*محل امتحان" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="ddl_ExamPlace" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%">
                                        <asp:Label ID="lbl_ExamDay" runat="server" Text="روز " Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label></td>
                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddl_ExamDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_ExamDate_SelectedIndexChanged" ForeColor="Black" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_ExamSaat" runat="server" Text=" ساعت " Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddl_ExamTime" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Fields" runat="server" Text="رشته" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Field" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Prof" runat="server" Text="استاد" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Professor" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_Moshakhase" runat="server" Text=" مشخصه کلاس" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Class" runat="server" ForeColor="Black" CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </td>


                                </tr>
                            </table>
                            <br />
                            <uc1:AccessControl ID="AccessControl1" runat="server" />
                            <br />
                            <asp:Button ID="btn_ExamPresent" runat="server" OnClick="ButtonExamPresent_Click" Text="گزارش" CssClass="btn btn-exam" />


                        </fieldset>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <div>
            <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Visible="false" Width="100%" RenderMode="AjaxWithCache" Theme="Windows7" ViewMode="OnePage"
                ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" CssClass="rptstyle" />

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
    </div>
        </div>
    
    <div class="loadingWrapper">
        <div class="loadingInner">
            <img src="../MasterPages/images/loading.gif" />
            <div>لطفاً منتظر بمانید ...</div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('.btn-exam').click(function (e) {
                $('.loadingWrapper').show();
            });
        });
        $(window).load(function () {
            $('.loadingWrapper').hide();
        });
    </script>
</asp:Content>

