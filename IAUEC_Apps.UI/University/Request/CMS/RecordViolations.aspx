<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/CMSEditInfoRequest.Master"
    AutoEventWireup="true" CodeBehind="RecordViolations.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.CMS.RecordViolations" %>

<%@ Register Src="../../../CommonUI/AccessControl.ascx" TagName="AccessControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/TasvieCustomskin.css" rel="stylesheet" />
    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />

    <style>
        .RadGrid .rgFilterRow input {
            height: 25px;
            color: #000;
        }

        .RadMenu_rtl ul.rmVertical {
            background: #eee;
            border: 1px solid #ddd;
        }

        .RadMenu_rtl .rmGroup .rmLink {
            padding: 3px 0;
        }

            .RadMenu_rtl .rmGroup .rmLink:hover {
                text-decoration: none;
                background: #ddd;
            }

        .RadGridRTL .rgNumPart a {
            border: 1px solid #ccc;
            padding: 0px 8px !important;
            margin: 0 3px !important;
            border-radius: 5px;
        }

        .RadGrid .rgNumPart a.rgCurrentPage {
            background: #ddd;
            color: #000;
        }

        .RadGrid td.rgPagerCell {
            border-top: 1px solid #ccc !important;
        }

        .RadGrid .rgHeader a {
            color: #fff;
        }
    </style>
    <style>
        input {
            text-align: right;
        }

        textarea {
            text-align: right;
        }
        .container-description{
            display:none;
        }
        .container-info{
            display:none;
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="float: right;">
        <button type="button" class="btn btn-success btn-lg" data-toggle="modal" data-target="#myModal">ثبت تخلف <i class="fa fa-plus" aria-hidden="true"></i></button>
    </div>
    <div class="row" style="direction: rtl;">
        <telerik:RadGrid ID="rdGridExcel" runat="server" AllowFilteringByColumn="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False" Skin="MyCustomSkin"
            OnNeedDataSource="rdGridExcel_NeedDataSource" OnItemDataBound="rdGridExcel_ItemDataBound" OnItemCommand="rdGridExcel_ItemCommand"
            EnableLinqExpressions="False" CellSpacing="-1" GridLines="Both"
            GroupPanelPosition="Top" SortingSettings-EnableSkinSortStyles="false" PageSize="50">
            <MasterTableView HorizontalAlign="Center" Width="100%">
                <PagerStyle PageSizeLabelText="تعداد در صفحه" ShowPagerText="false" Mode="NumericPages" />
                <HeaderStyle CssClass="bg-blue" Font-Names="b nazanin" Font-Bold="true" Font-Size="16px" />
                <FilterItemStyle CssClass="bg-blue" />
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                        <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn HeaderText="شماره دانشجویی" DataField="StudentCode" AllowFiltering="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نام" DataField="FirstName" AllowFiltering="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="نام خانوادگی" DataField="LastName" AllowFiltering="true"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="علت تخلف" DataField="Description" AllowFiltering="false"></telerik:GridBoundColumn>
                     <telerik:GridTemplateColumn AllowFiltering="false">
                         <ItemTemplate>
                             <asp:HiddenField runat="server" ID="hdnItemId" Value='<%# Eval("Id") %>' />
                             <asp:Button runat="server" ID="btnRecordViolation" Text="بازگردانی ثبت تخلف" CommandName="RecordViolation" CommandArgument='<%# Eval("DeleteDate") %>' />
                         </ItemTemplate>
                     </telerik:GridTemplateColumn>
                </Columns>

            </MasterTableView>
            <FilterMenu EnableEmbeddedSkins="False">
            </FilterMenu>
            <HeaderContextMenu EnableEmbeddedSkins="False">
            </HeaderContextMenu>
        </telerik:RadGrid>
    </div>

    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="text-align: right;">
                <div class="modal-header" style="margin-right: 95%;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="margin-bottom:7px;">
                        <div class="col-9 col-md-9">
                            <label for="studentCode">شماره دانشجویی</label>
                            <input type="text" class="form-control" id="studentCode" />
                        </div>
                        <div class="col-3 col-md-3" style="padding-top: 34px;padding-right: 5px;">
                            <button class="btn btn-success btn-search"><i class="fa fa-search" aria-hidden="true"></i></button>
                        </div>
                    </div>
                    <div class="row container-info" style="margin-bottom:7px;">
                        <div class="col-sm-6 col-md-6 col-lg-6 ">
                            نام: <span class="first-name-info"></span>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 ">
                            نام خانوادگی: <span class="last-name-info"></span>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 ">
                            نام پدر: <span class="father-name-info"></span>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 ">
                            کد ملی: <span class="national-code-info"></span>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 ">
                            نام رشته: <span class="field-name-info"></span>
                        </div>
                        <div class="col-sm-6 col-md-6 col-lg-6 ">
                            نام دانشکده: <span class="department-name-info"></span>
                        </div>
                    </div>
                    <div class="row container-description">
                        <div class="col-12">
                            <%--<label for="description">علت تخلف</label>--%>
                            <textarea id="description" rows="4" cols="50" class="form-control" placeholder="علت تخلف"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" id="btnInsert">ذخیره</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        var validate = true;
        $(".btn-search").click(function (e) {
            e.preventDefault();
            if ($("#studentCode").val() == "") {
                alert("کد دانشجویی را وارد نمایید");
                return;
            }
            var model = {
                StudentCode: $("#studentCode").val()
            }
            $.ajax({
                type: "POST",
                url: "/University/Request/CMS/RecordViolations.aspx/GetStudentInfo",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ recordViolationDTO: model }),
                success: function (res) {
                    console.log(res);
                    debugger;
                    if (res.d.Status) {
                        $(".container-info").css("display", "block");
                        $(".container-description").css("display", "block");
                        $(".first-name-info").text(res.d.FirstName);
                        $(".last-name-info").text(res.d.LastName);
                        $(".father-name-info").text(res.d.FatherName);
                        $(".national-code-info").text(res.d.NationalCode);
                        $(".field-name-info").text(res.d.FieldName);
                        $(".department-name-info").text(res.d.DepartmentName);
                    } else {
                        alert("اطلاعاتی جهت نمایش وجود ندارد.");
                    }

                },
                Error: function () {
                    $(".body-container").css("display", "none");
                    $(".container-error-message").css("display", "block");
                }
            });
        });

        $("#btnInsert").click(function () {
            validate = true
            validateError();
            if (!validate)
                return;
            if ($("#description").val() == "") {
                alert("علت تخلف را وارد نمایید.");
                return;
            }
            var model = {
                StudentCode: $("#studentCode").val(),
                FirstName: $(".first-name-info").text(),
                LastName: $(".last-name-info").text(),
                NationalCode: $('.national-code-info').text(),
                Description: $("#description").val(),
            }
            $.ajax({
                type: "POST",
                url: "/University/Request/CMS/RecordViolations.aspx/InsertViolation",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ recordViolationDTO: model }),
                success: function (res) {
                    debugger;
                    if (res.d) {
                        $("#myModal").toggle();
                        //alert("ثبت اطلاعات با موفقیت انجام شد");
                        //location.reload();
                        window.location.href = window.location.href;
                    } else {
                        alert("ثبت اطلاعات با موفقیت انجام نشد");
                    }

                },
                Error: function () {
                    $(".body-container").css("display", "none");
                    $(".container-error-message").css("display", "block");
                }
            });
        });

        function validateError() {
            $('input').each(function () {
                if ($(this).val() == "" && validate == true) {
                    var id = $(this).attr('id');
                    var txt = $('label[for="' + id + '"]').text();
                    if (txt != "") {
                        txt = txt + " را وارد نمایید.";
                        alert(txt);
                        validate = false;
                    }
                }
            });
        }
    </script>
</asp:Content>



