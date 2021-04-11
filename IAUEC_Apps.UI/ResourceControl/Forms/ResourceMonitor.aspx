<%@ Page Title="" Language="C#" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" AutoEventWireup="true" CodeBehind="ResourceMonitor.aspx.cs" Inherits="IAUEC_Apps.UI.ResourceControl.Forms.ResourceMonitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .inlineTableDisplay {
            display: inline-table;
        }
        label {
    display: initial;
    max-width: 100%;
    margin-bottom: 5px;
    font-weight: bold;
    padding: 5px;
}
    </style>
    <div class="container rtl">
        <h2 class="hidden-print">لیست منابع موجود</h2>

        <div>
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <table class=" table table-bordered tbl rtl hidden-print">
                        <tr class="bg-primary">
                            <td>تاریخ</td>
                            <td>دسته بندی</td>
                            <td>محل</td>
                            <td>کلاس ها</td>
                            <td>عملیات</td>
                        </tr>
                        <tr>
                            <td>
                                <p>تاریخ :</p>
                                <asp:TextBox ID="pcal1" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pcal1" ErrorMessage="لطفا تاریخ را انتخاب نمایید" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="لطفا تاریخ را به صورت صحیح وارد نمایید" ForeColor="Red" ValidationExpression="\d{4}(?:/\d{1,2}){2}" ControlToValidate="pcal1">*</asp:RegularExpressionValidator>
                            </td>

                            <td>
                                <p>دسته بندی کلاس های :</p>
                                <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblLocation" Text="محل :" runat="server" />
                                <asp:DropDownList ID="drpLocation" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpLocation_SelectedIndexChanged"></asp:DropDownList>
                                <br style="line-height: 0.5"/>
                                <asp:CheckBox ID="chkbLocationByRole" Text="نمایش کلاس های تمام محل ها " runat="server" />
                                <br style="line-height: 0.5"/>
                                <asp:CheckBox ID="cbNotShowEmpty" Text="عدم نمایش کلاس های بدون درخواست" runat="server" />



                            </td>
                            <td style="text-align: center;">
                                <p>کلاس های موجود :</p>
                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox1" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                    Width="280" Label="انتخاب نمایید">
                                </telerik:RadComboBox>
                                <asp:CheckBoxList ID="chbkrResources" runat="server" RepeatColumns="1"></asp:CheckBoxList>

                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" Text="نمایش" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="خطا " />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView runat="server" ID="grdResourceList" DataKeyNames="ID" AutoGenerateColumns="false" CssClass=" table table-bordered table-condensed table-hover tbl rtl" OnRowDataBound="grdResourceList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="کلاس" HeaderStyle-CssClass="bg-primary col-sm-2" ItemStyle-CssClass="rtl">
                                <ItemTemplate>
                                    <asp:Label ID="lblname" Text='<%#Bind("name") %>' runat="server" CssClass="rtl"></asp:Label>
                                    <br />
                                    <asp:Label Text="محل :" runat="server" />
                                    <asp:Label ID="lblLocation" Text='<%#Bind("location") %>' runat="server" CssClass="rtl"></asp:Label>
                                    <br />
                                    <asp:Label Text="امکانات :" runat="server" />
                                    <asp:BulletedList runat="server" ID="bltResOptions" BulletStyle="Disc"></asp:BulletedList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="درخواستها" HeaderStyle-CssClass="bg-primary">
                                <ItemTemplate>
                                    <asp:GridView ID="grdRequestsPerResource" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="false">
                                        <HeaderStyle CssClass=" text-center bg-primary" />
                                        <Columns>
                                            <asp:BoundField HeaderText="شماره" DataField="ID" />
                                            <asp:BoundField HeaderText="نام درس" DataField="coursename" />
                                            <asp:BoundField HeaderText="درخواست کننده" DataField="issuername" />
                                            <asp:TemplateField HeaderText="وضعیت">
                                                <ItemTemplate>
                                                    <%--<asp:Image ID="imgStatus" runat="server" Width="25px" Height="25px" ImageUrl='<%# GetImage((int)Eval("status")) %>' ImageAlign="Middle" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ساعت شروع">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("StartTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ساعت پایان">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# TimeSpan.FromTicks(Convert.ToInt64(Eval("EndTime"))).ToString("hh\\:mm") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="drpLocation" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="drpCategory" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
    <script>
        var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1',
            { extraInputID: 'ContentPlaceHolder1_pcal1', extraInputFormat: 'yyyy/mm/dd' });
    </script>

</asp:Content>
