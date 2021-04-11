<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="ResearchUserReview.aspx.cs" Inherits="ResourceControl.PL.Forms.ResearchUser" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h3>سامانه رزرواسیون کلاس های حضوری</h3>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container rtl">
        <div class="col-md-12">
            <table class="table table-bordered table-condensed rtl">
                <tr class="bg-primary">
                    <td>
                        <h4>لیست درخواستهای ثبت شده</h4>
                    </td>
                    <td>
                        <h4 style="display: inline">وضعیت درخواست</h4>
                        <asp:DropDownList ID="drpRequestStatus" runat="server" CssClass="dropdown" ForeColor="#666666" AutoPostBack="True" OnSelectedIndexChanged="drpRequestStatus_SelectedIndexChanged">
                            <asp:ListItem Text="درخواستهای منتظر تایید" Value="1" />
                            <asp:ListItem Text="درخواستهای تایید شده" Value="2" />
                            <asp:ListItem Text="درخواستهای رد شده" Value="3" />
                            <asp:ListItem Text="تمامی درخواستها" Value="4" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" dir="rtl">
                        <div class="table-responsive" style="max-height: 800px; overflow-y: scroll">
                            <asp:GridView ID="grdProfessorReview" runat="server" CssClass="table table-bordered table-condensed " OnPageIndexChanging="grdProfessorReview_PageIndexChanging" ShowHeaderWhenEmpty="true" EmptyDataText="درخواستی پیدا نشد" AutoGenerateColumns="false" OnRowDataBound="grdProfessorReview_RowDataBound">
                                <HeaderStyle CssClass="bg-blue-sky" />
                                <RowStyle CssClass="text-center" />
                                <EditRowStyle CssClass="text-center" />
                                <Columns>
                                    <asp:BoundField HeaderText="شماره درخواست" DataField="ID" />
                                    <asp:BoundField HeaderText="مشخصه کلاس" DataField="CourseDID" />
                                    <asp:BoundField HeaderText="عنوان" DataField="courseName" />
                                    <asp:TemplateField HeaderText="وضعیت">
                                        <ItemTemplate>
                                            <asp:Image ID="imgStatus" runat="server" Width="25px" Height="25px" ImageAlign="Middle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="تاریخ ثبت" DataField="issue_time" />
                                    <asp:BoundField HeaderText="پاسخ" DataField="answernote" />
                                    <asp:BoundField HeaderText="زمان پاسخ" DataField="answer_time" />
                                </Columns>
                                <PagerSettings NextPageText="بعدی" PreviousPageText="قبلی" FirstPageText="First Page" LastPageText="Last Page" Mode="Numeric" Position="Bottom" />
                                <PagerStyle CssClass="cssPager" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAddRequest" Text="ثبت درخواست جدید" runat="server" CssClass="btn btn-danger" OnClick="txtAddReques_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
