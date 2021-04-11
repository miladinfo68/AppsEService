<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="Modir.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.Modir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>مدیریت سامانه ثبت نام اساتید</title>
    <script type="text/javascript">
        function openModal() {
            $('#historyModal').modal('show');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2>مدیریت سامانه ثبت نام اساتید</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" container" dir="rtl">
        <asp:UpdatePanel runat="server" ID="upModir" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <br />
                <div class="row">
                    <div class="form-horizontal">
                        <div class="form-group col-sm-5">
                            <asp:Label Text="کد ملی استاد :" AssociatedControlID="txtCodeMeli" runat="server" CssClass=" col-sm-3 control-label" />
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtCodeMeli" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-sm-6">
                        <asp:Label Text="نام خانوادگی استاد :" AssociatedControlID="txtCodeMeli" runat="server" CssClass=" col-sm-3 control-label" />
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        </div>

                    </div>
                    <div class="form-group col-sm-1">
                        <asp:Button ID="btnSearch" Text="جستجو" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <hr />
                <div>
                    <asp:GridView ID="grdProfessorStatus" runat="server" OnRowDataBound="grdProfessorStatus_RowDataBound" OnRowCommand="grdProfessorStatus_RowCommand" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table table-bordered table-condensed table-striped">
                        <HeaderStyle CssClass="bg-blue" />
                        <Columns>
                            <asp:BoundField HeaderText="ردیف" />
                            <asp:BoundField HeaderText="نام " DataField="name" />
                            <asp:BoundField HeaderText="نام خانوادگی" DataField="family" />
                            <asp:BoundField HeaderText="نام پدر" DataField="namep" />
                            <asp:BoundField HeaderText="کد ملی" DataField="idd_meli" />
                            <asp:BoundField HeaderText="رشته تحصیلی" DataField="reshte" />
                            <asp:TemplateField HeaderText="وضعیت">
                                <ItemTemplate>
                                    <asp:DropDownList ID="drpUserStatus" runat="server">
                                        <asp:ListItem Text="در حال بررسی" Value="0" />
                                        <asp:ListItem Text="تایید کارگزینی" Value="1" />
                                        <asp:ListItem Text="رد کارگزینی" Value="2" />
                                        <asp:ListItem Text="نقص کارگزینی" Value="3" />
                                        <asp:ListItem Text="تایید دانشکده" Value="4" />
                                        <asp:ListItem Text="تایید پژوهش" Value="5" />
                                        <asp:ListItem Text="رد پژوهش" Value="6" />
                                        <asp:ListItem Text="نقص پژوهش" Value="7" />
                                        <asp:ListItem Text="انتقالی از سیدا" Value="8" />
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="مدرک" DataField="madrak" />
                            <asp:BoundField HeaderText="تاریخ ثبت درخواست" DataField="shdate" />
                            <asp:BoundField HeaderText="تاریخ آخرین ویرایش" DataField="Last_Update" />
                            <asp:TemplateField HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button ID="btnResetStatus" Text="بازگردانی وضعیت " CommandName="send" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"idd_meli")  %>' runat="server" CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عملیات">
                                <ItemTemplate>
                                    <asp:Button ID="btnDetails" Text="مشاهده جزئیات " CommandName="Details" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")  %>' runat="server" CssClass="btn btn-success" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاریخچه">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnHistory" AlternateText="تاریخچه" Visible="true" runat="server" CommandName="History" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID") %>' ImageUrl="~/University/Theme/images/appHistory.png" Width="40" Height="40" CssClass="center-margin" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" id="historyModal" style="background-color: aqua;">

                    <div class="modal-dialog" role="document" style="width: 70%">
                        <div class="modal-content bg-info border-dark">
                            <div class="modal-header" dir="rtl">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="font-size: -webkit-xxx-large; float: left; float: left; margin-left: 1%;">
                                    <span aria-hidden="true" style="margin: auto; line-height: initial;">&times;
                                    </span>
                                </button>
                                <div class="modal-header bg-orange" dir="rtl">
                                    <h4 class="modal-title" id="exampleModalLabel">مشاهده تاریخچه فعالیت</h4>
                                    <h5 class="modal-title" id="TeacherName" runat="server"></h5>
                                </div>
                            </div>
                            <div class="modal-body">


                                <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border: black; border-width: thick">
                                    <tr class="bg-blue-sky" style="text-align: center, right;">
                                        <th></th>
                                        <th style="text-align:center">نام کاربر</th>
                                        <th style="text-align:center">تاریخ</th>
                                        <th style="text-align:center">ساعت</th>
                                        <th style="text-align:center">رویداد</th>
                                        <th style="text-align:center">توضیحات</th>
                                        <th style="text-align:center">کد درخواست</th>
                                    </tr>

                                    <asp:ListView ID="lst_history" runat="server" OnItemCommand="lst_history_ItemCommand">
                                        <ItemTemplate>

                                            <tr id="<%#DataBinder.Eval(Container.DataItem,"reqId") %>" class="bg-blue" style="text-align: center;">

                                                <td>
                                                    <span onclick="Edits(<%#DataBinder.Eval(Container.DataItem,"reqId") %>,<%#DataBinder.Eval(Container.DataItem,"rowId") %>)"  style="cursor: pointer;">+</span>

                                                </td>
                                                <td>
                                                    <%#Eval("Name") %>
                                                </td>
                                                <td>
                                                    <%#Eval("LogDate") %>
                                                </td>
                                                <td>
                                                    <%#Eval("LogTime") %>
                                                </td>
                                                <td>
                                                    <%#Eval("EventName") %>
                                                </td>
                                                <td>
                                                    <%#Eval("Description") %>
                                                </td>
                                                <td>
                                                    <%# Eval("reqId") %>
                                                </td>

                                            </tr>
                                            <tr class="row">

                                                <td class="col-md-12 teimoory" style="border: none; display: none" colspan="8" id="<%#DataBinder.Eval(Container.DataItem,"rowId") %>"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </table>


                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">بستن پنجره</button>
                            </div>
                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="uppModir" runat="server" AssociatedUpdatePanelID="upModir">
            <ProgressTemplate>
                <div id="wait" class="updateProgress" dir="rtl">
                    <div class="center">
                        <img alt="loading" src="Image/animatedEllipse.gif" />
                        <span>درحال بارگذاری...</span>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    </div>
    <style>
        .center {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 200px;
            background-color: White;
            border-radius: 5px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 46px;
                width: 46px;
            }

        .updateProgress {
            height: 100%;
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0,0,0,0.7);
        }
    </style>
    <style>
        .imgExpand{
            background:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAFS0lEQVRoQ+1aa0wcVRT+7sxuQxOwDxNLW6OkJVLFx9b2V4NlUaBrrdH6Sn81/PCfP7qp5aExprFoeMQEfzZCpcZEUzHtD9MsFnmJNQqLa9UY07UBygImLiylspuyc6+5d2aWHVjY3c7uyjadYZnnPed859xz7rn3DEGWbyTL5cedD6DWVfYEpdj4f1hKkhBodPT8shrvmBaovVS+gdFbTjBSBaAgGeGJZlRCjKQZJ8IY+J7kFgBhLURa19JY0TW7tO0yAFzjDLQXULW+KScfm9bnr8qTCyt+2s4x6ED0hkJw8aftOphV8MwEpzATmtJJBAgk+1KLGABEC79jkw3lhVXYudmWpMJS+/pf0x50edtxbcbDCS8DEQEguo2ywN8q2LPNgdceq0utJCapnfu1Ae4JF6cyQmSrTe9OEQDVrv3HCEjL1rxCOPe1mmSXnuYtl1/H5JyXd0Jns6P/I84lAqDGVfozANtRWz2Kt5SkRwKTVH//ewCfet7hVDxNjr7dSwEId2o8wP137W61nXYhXJOjTyg/2gJZAUDvRgSSjUekrANw+ieniEiMSvbmgz2qGfhW4yrNCgvcBZCIew/5OjAW4EFNdzOGBzY+ib3bX06k+arvZMQCA6OfwD8/omdFav7DGF545L3sAPDD2Fn458ciAPQM6NCud7MDwI/XP8PM/HUtSJNIFvps0dvmAQw6cW16hShEiISGym7TTAbHP0cgOG7oQowxVD5UY5r2x4PH4Z0ejh1GZSLjg8pvTTMZ9p1DIOgzAgDDM4XHTdNuHXoTV/3u2AAskgXvV3SZZuKZ+Ao3Qj5toOdDDQNlFPadx0zTbhs6IQBQSpYPZFbJivqKS6aZXJk8j7nQpDapISIKcQBP7XjDNO029wlc/ScGgFqXnVllK06VfxOXCY/z47NXIBMLZEmGRRwt6jWRIUV+6kDP+z8HQJkChYWhaMcw1a5pGPdveBx7EhgnzrirhQUUBUYLJAOgf6RVRBkuPBeadz1VeB2Iel8ikgoAFIoQdlFgDiTMrykHFEaYhvFScX1c5akAhqEobAmATjuzSutwqrwzLpHvRtowPT8mtB6t/QgQ/T6RAUJAqYKw0HxYCCzOowTX7x9OCEANvH43wncegCR8IHVdiFtC7VaEWPD8LjHbWnVbuQu57MwiW1GfgBN7/d9jau5P4azcD8RROK4kzoO3ZkDZwqIPaE6cl5Mv/IA7M3fqxXMFW3KLUHjvvnjyY0Un5vOBVIbRG6FJSNpQxicaaQ+jHEDqBrIOzIYmDItbGRnI0pNKiAU5MZilPZVITzKnAchEMsc9KBXLKsZ0WqhfWCDt6XSqABgnNKoF+P+smdAMjJ6B/99RMaFRc1HAKuXgYNFbccNkvBcyMif2+i9jcu4Pgyxb8x5OKM6vCQDxhDDzPCMWMCNgvLZ3AcTTULqfN/QdEWUnRZELPnyue3TZ2ujJp7/GemtuuuW4LfrBhZs42X1ItI21vH4ewIuvPlqHvdsdt8Ug3Y2GfC58+VsDZ3OhydF3mJ8slpgulpUSifbmWHJRt/+LNWcFrv2G/iMIhW8ChFU1Heg/awDAL6pdpT0EsPM62dHd9dgcp7yabo3r9LnwpwedWn0Mvc2OvjL9mbHMqlYqeY3Jxi1R8uArKL6vBNvuKcyUrAY+08EpuH0uDIx2qJoHPES22qML3ssL3SqIdu4PiUlNIOlFbiJpBe+oQreWS+gpNV+lpjy1Y1QstyRRub9AZGvV0mr9ih97VF8sK5UkWsUAXlVL+HODlT81WKzUJ6YY8dYIAXopldp5OSlWu/8A66fOXhS6ArcAAAAASUVORK5CYII=);
        }
        .imgCollapse{
            background:url(data:image/png;base64,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);
        }
    </style>

</asp:Content>
