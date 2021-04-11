<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ResourceControl/MasterPages/CMSMasterResourceControl.Master" CodeBehind="OfficeUserReplyRequest.aspx.cs" Inherits="ResourceControl.PL.Forms.OfficeUserReplyRequest" %>

<asp:Content ContentPlaceHolderID="PageTitle" runat="server">
    <h3>فرم بررسی و ارسال درخواست توسط کاربر اداری</h3>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container rtl">
        <br />
        <div class="row">
            <div class="col-md-12 table-responsive">
                <table class="table table-bordered table-striped tbl target tblcurrentreq table-condensed">
                    <tr>
                        <td colspan="9" class="bg-danger">
                            <h3>درخواست جاری</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>شماره درخواست</td>
                        <td>متقاضی</td>
                        <td>دسته بندی کلاس ها</td>
                        <td>ظرفیت</td>
                        <td>امکانات درخواستی :</td>
                        <td>توضیحات</td>
                        <td>زمان درخواستی</td>
                        <td>
                            <p>تاریخ استفاده از کلاس</p>
                        </td>
                        <td>عملیات رد درخواست 
                        <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnswerNote" ErrorMessage="لطفا دلیل رد درخواست را ذکر کنید" ForeColor="Red" ValidationGroup="deny"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRequestID" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblIssuer" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblCategory" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="lblCapacity" runat="server" />
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chblOptions" runat="server" CssClass="checkbox" RepeatColumns="2" Enabled="False" />
                        </td>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" />
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
                            <%--<asp:Button ID="btnEditRequest" Text="ویرایش درخواست" runat="server" CssClass="btn btn-warning btn-block " OnClick="btnEditRequest_Click" />
                        <br />--%>
                            <asp:TextBox ID="txtAnswerNote" runat="server" CssClass="form-control" />
                            <asp:Button Text="رد درخواست" ID="btnDenyRequest" runat="server" CssClass="btn btn-block btn-danger" OnClick="btnDenyRequest_Click" ValidationGroup="deny" />
                        </td>
                    </tr>
                </table>

            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-12" dir="rtl">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3>فهرست کلاس های مطابق با امکانات درخواست شده</h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <asp:GridView ID="grdResourceList" runat="server" DataKeyNames="ID" AutoGenerateColumns="false" EmptyDataText="متاسفانه هیچ کلاسی مطابق با امکانات درخواستی پیدا نشد" OnRowDataBound="grdResourceList_RowDataBound" CssClass=" table table-bordered table-striped rtl table-condensed tbl" OnRowCommand="grdResourceList_RowCommand">
                                <HeaderStyle CssClass="rtl bg-blue-sky" />
                                <Columns>
                                    <asp:TemplateField HeaderText="نام کلاس" ItemStyle-CssClass="rtl">
                                        <ItemTemplate>
                                            <asp:Label Text="نام :" runat="server" Font-Bold="true" />
                                            <asp:Label ID="lblname" Text='<%#Bind("name") %>' runat="server" CssClass="rtl"></asp:Label>
                                            <br />
                                            <asp:Label Text="محل :" runat="server" Font-Bold="true" />
                                            <asp:Label ID="lblLocation" Text='<%#Bind("location") %>' runat="server" CssClass="rtl"></asp:Label>
                                            <br />
                                            <asp:Label Text="امکانات :" Font-Bold="true" runat="server" />
                                            <asp:BulletedList runat="server" ID="bltResOptions" BulletStyle="Disc"></asp:BulletedList>
                                            <span>ظرفیت : </span>
                                            <asp:Label ID="lblResCapacity" Text='<%#Bind("capacity") %>' runat="server" />
                                            <span>نفر</span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                                        <HeaderTemplate>درخواست های پیشنهاد شده ولی تایید نشده برای این کلاس</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:GridView ID="grdRequestPendingList" runat="server" DataKeyNames="ID" CssClass="table table-bordered table-condensed table-hover rtl" AutoGenerateColumns="false" OnRowDataBound="grdPendingRequestList_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="شماره" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                    <asp:BoundField DataField="issue_time" HeaderText="تاریخ ثبت درخواست" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                    <asp:BoundField DataField="issuerName" HeaderText="متقاضی" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                    <asp:BoundField DataField="capacity" HeaderText="ظرفیت" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                    <asp:BoundField DataField="note" HeaderText="توضیحات" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                    <asp:BoundField DataField="sessionstart_time" HeaderText="ساعت شروع" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                    <asp:BoundField DataField="sessionend_time" HeaderText="ساعت پایان" ItemStyle-CssClass="rtl " HeaderStyle-CssClass="rtl bg-primary" />
                                                </Columns>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="تایید این کلاس">
                                        <ItemTemplate>
                                            <asp:Button runat="server" ID="btnAddResource" Text="تایید درخواست" CommandName="approve" CommandArgument='<%#Eval("ID")%>' CssClass="btn btn-success" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="درخواستهای تایید شده برای این کلاس">
                                        <ItemTemplate>
                                            <asp:GridView ID="grdApprovedRequestList" DataKeyNames="ID" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-condensed rtl">
                                                <Columns>
                                                    <asp:BoundField DataField="ID" HeaderText="شماره" HeaderStyle-CssClass="rtl bg-success" />
                                                    <asp:BoundField DataField="subject" HeaderText="عنوان" HeaderStyle-CssClass="rtl bg-success" />
                                                    <asp:BoundField DataField="issuerName" HeaderText="متقاضی" HeaderStyle-CssClass="rtl bg-success" />
                                                    <asp:BoundField DataField="sessionstart_time" HeaderText="ساعت شروع" HeaderStyle-CssClass="rtl bg-success" />
                                                    <asp:BoundField DataField="sessionend_time" HeaderText="ساعت پایان" HeaderStyle-CssClass="rtl bg-success" />
                                                </Columns>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <%--<telerik:RadWindow runat="server" ID="RadWindow2" VisibleOnPageLoad="false">
        <ContentTemplate>
            <div dir="rtl">
                <asp:Image ID="Image1" runat="server" Width="35px" Height="35px" ImageUrl="~/ResourceControl/Images/Info.png" />
                <asp:Label ID="lblSuccess" Text="متن پیام" runat="server" />
            </div>
            <br />
            <asp:Button ID="btnSubmitMsg" runat="server" Text="تایید" CssClass="btn btn-success" OnClick="btnSubmitMsg_Click" />
        </ContentTemplate>
    </telerik:RadWindow>--%>
</asp:Content>
