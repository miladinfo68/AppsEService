<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="ResearchAddResource.aspx.cs" Inherits="ResourceControl.PL.Forms.ResearchAddResource" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container rtl">
        <h3>فرم درخواست منبع توسط کاربر پژوهش</h3>
        <div class="col-md-12">
            <table class="table table-bordered table-hover table-striped tbl">
                <tr>
                    <td class="bg-danger " colspan="9">
                        <h3>درخواست جاری</h3>
                    </td>
                </tr>
                <tr>
                    <td>شماره درخواست</td>
                    <td>دسته بندی منابع :</td>
                    <td>موضوع درخواست :</td>
                    <td>محل درخواستی</td>
                    <td>امکانات درخواستی :</td>
                    <td>توضیحات  :</td>
                    <td>زمان درخواستی :</td>
                    <td>
                        <p>تاریخ :</p>
                    </td>
                    <td>عملیات
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswerNote" ErrorMessage="لطفا دلیل رد درخواست را ذکر کنید" ForeColor="Red" ValidationGroup="deny"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRequestID" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblCategory" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblSubject" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblLocation" runat="server" />
                    </td>
                    <td>
                        <asp:CheckBoxList ID="chblOptions" runat="server" CssClass="checkbox" RepeatColumns="1" Enabled="False" />
                    </td>
                    <td>
                        <asp:Literal ID="lblDescription" runat="server" />
                    </td>
                    <td>
                        <p>ساعت شروع :</p>
                        <asp:Label ID="lblSessionStart" runat="server" />
                        <p>ساعت پایان :</p>
                        <asp:Label ID="lblSessionEnd" runat="server"></asp:Label>

                    </td>
                    <td>
                        <asp:Label ID="pcal1" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtAnswerNote" runat="server" CssClass="form-control" />
                        <asp:Button Text="رد درخواست" ID="btnDenyRequest" runat="server" CssClass="btn btn-block btn-danger" OnClick="btnDenyRequest_Click" ValidationGroup="deny" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="col-md-12" dir="rtl">
            <table class="table table-bordered table-hover table-striped tbl rtl">
                <tr>
                    <td class="bg-primary">
                        <h3>فهرست منابع موجود در این دسته بندی</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdResourceList" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" OnRowDataBound="grdResourceList_RowDataBound" EmptyDataText=" متاسفانه هیچ منبعی مطابق با امکانات درخواستی پیدا نشد " CssClass=" table table-bordered table-hover table-striped rtl" OnRowCommand="grdResourceList_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="نام منبع" HeaderStyle-CssClass="rtl bg-primary">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" Text='<%#Bind("name") %>' runat="server" CssClass="rtl"></asp:Label>
                                        <asp:BulletedList runat="server" ID="bltResOptions" BulletStyle="Disc"></asp:BulletedList>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-CssClass="rtl bg-primary">
                                    <HeaderTemplate>دیگر درخواست های فرستاده شده ولی تایید نشده برای این منبع</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:GridView ID="grdRequestPendingList" runat="server" DataKeyNames="ID" CssClass="table table-bordered table-striped table-hover rtl" EmptyDataText="هیچ درخواستی برای این منبع منتظر تایید نیست" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="شماره" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                                <asp:BoundField DataField="subject" HeaderText="عنوان" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                                <asp:BoundField DataField="note" HeaderText="توضیحات" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                                <asp:BoundField DataField="issue_time" HeaderText="تاریخ ثبت درخواست" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                                <asp:BoundField DataField="issuerName" HeaderText="متقاضی" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                                <asp:BoundField DataField="sessionstart_time" HeaderText="ساعت شروع" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                                <asp:BoundField DataField="sessionend_time" HeaderText="ساعت پایان" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-warning" />
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="پیشنهاد این منبع" HeaderStyle-CssClass="rtl bg-primary">
                                    <ItemTemplate>
                                        <asp:Button runat="server" ID="btnAddResource" Text="ارسال درخواست" CommandName="Add" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-primary" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="درخواستهای تایید شده برای این منبع" HeaderStyle-CssClass="rtl bg-primary">
                                    <ItemTemplate>
                                        <asp:GridView ID="grdApprovedRequestList" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" EmptyDataText="هیچ درخواستی در زمان درخواستی برای این منبع تایید نشده است" CssClass="table table-bordered table-striped table-hover rtl">
                                            <Columns>
                                                <asp:BoundField DataField="subject" HeaderText="عنوان" HeaderStyle-CssClass="rtl bg-success" />
                                                <asp:BoundField DataField="issuerName" HeaderText="استاد" HeaderStyle-CssClass="rtl bg-success" />
                                                <asp:BoundField DataField="sessionstart_time" HeaderText="ساعت شروع" HeaderStyle-CssClass="rtl bg-success" />
                                                <asp:BoundField DataField="sessionend_time" HeaderText="ساعت پایان" HeaderStyle-CssClass="rtl bg-success" />
                                            </Columns>
                                        </asp:GridView>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
