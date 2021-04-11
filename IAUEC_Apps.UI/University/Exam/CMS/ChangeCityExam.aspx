<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ChangeCityExam.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.ChangeCityExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
    <style>
        .studentInfoWrapper {
            background: #ededed;
            border: 1px solid #ddd;
            margin: 15px 0;
            text-align: right;
            padding: 0 15px;
        }

            .studentInfoWrapper .row {
                margin-top: 15px;
            }

        .d_flex_y_center {
            text-align: right;
            display: flex;
            align-items: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>
    <div style="direction: rtl;">
        <div id="div_Main" style="padding-left: 15%; padding-right: 20%;">
            <asp:Panel ID="pnl_Main" runat="server" HorizontalAlign="Center" Width="100%" Direction="RightToLeft">
                <fieldset>
                    <legend style="color: #000000">تغییر محل امتحان</legend>
                    <table style="text-align: right; width: 100%;" runat="server" dir="rtl">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_StNo" runat="server" Text="شماره دانشجویی:" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_StudentNumber" runat="server" ForeColor="Black" CssClass="form-control" MaxLength="9"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_City" CssClass="btn btn-success" runat="server" OnClick="Button2_Click" Text="نمایش اطلاعات" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--<asp:Label ID="lbl_CityName" runat="server" Text="شهر:" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>--%>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </fieldset>
                <asp:Panel runat="server" ID="pnlStudentInfo" Visible="false" CssClass="studentInfoWrapper">
                    <div class="row">
                        <div class="col-sm-3">نام و نام خانوادگی</div>
                        <div class="col-sm-3">
                            <asp:Label runat="server" ID="lblFullName"></asp:Label>
                        </div>
                        <div class="col-sm-3">شماره دانشجویی</div>
                        <div class="col-sm-3">
                            <asp:Label runat="server" ID="lblStcode"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">رشته تحصیلی</div>
                        <div class="col-sm-3">
                            <asp:Label ID="lblField" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-3">شهر فعلی</div>
                        <div class="col-sm-3">
                            <asp:Label ID="lbl_City" runat="server"></asp:Label>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-3">شهر</div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddl_City" runat="server" Font-Names="B Nazanin" Font-Size="Small" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                ForeColor="Black" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btn_taghir" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="اعمال تغییر" OnClientClick="if (!ConfirmChange()) return false;" />
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <br />
            </asp:Panel>
        </div>
        <div id="div1" style="padding-left: 15%; padding-right: 20%;">
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="100%" Direction="RightToLeft">
                <fieldset>
                    <legend style="color: #000000">تعداد در شهر</legend>
                    <div class="row d_flex_y_center">
                        <div class="col-sm-1">
                            <asp:Label ID="lbl_City2" runat="server" Text="شهر:" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Black"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddl_City2" runat="server" Font-Names="B Nazanin" Font-Size="Small" ForeColor="Black" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-1">
                            <span style="font-weight: bold; font-family: 'B Nazanin'; font-size: medium; color: #000;">ترم: </span>
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList runat="server" ID="ddlTerm" Font-Names="B Nazanin" Font-Size="Small" ForeColor="Black" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btn_ShowCount" runat="server" CssClass="btn btn-success" OnClick="Button4_Click" Text="نمایش تعداد" />
                        </div>
                        <div class="col-sm-2">
                            <asp:Label ID="lbl_Count" runat="server" Font-Bold="True" Visible="false" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="Red"></asp:Label>
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />

                </fieldset>
            </asp:Panel>
        </div>
    </div>
    <script type="text/javascript">
        function ConfirmChange() {
            var el = document.getElementById('<%= ddl_City.ClientID %>');
            if (el.value == 'انتخاب کنید') {
                alert('شهر محل امتحان را انتخاب کنید.');
                return false;
            }
            var r = confirm("آیا از تغییر محل امتحان اطمینان دارید؟");
            if (r == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>

