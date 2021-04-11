<%@ Page Title="" Language="C#" MasterPageFile="~/University/CooperationRequest/MasterPages/Cooperation.Master" AutoEventWireup="true" CodeBehind="CooperationRequestProfessors.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.CooperationRequestProfessors" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../Theme/css/CooperationGrid.CustomSkin.css" rel="stylesheet" />
    <link href="../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />

    <script type="text/javascript">
        function showModalChangeProfregStatus() {
            $('#mdChangeProfregStatus').modal('show');
        }
        $(document).on()
        $('#dvOpenProfregSystem')
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h2 style="color: blue">لیست متقاضیان 
    </h2>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .currencyWrapper {
            position: relative;
        }

            .currencyWrapper > .form-control {
                padding-left: 35px;
            }

            .currencyWrapper > span {
                display: block;
                position: absolute;
                left: 0;
                top: 0;
                bottom: 0;
                margin: auto;
                height: 50%;
                width: 30px;
                color: #999;
            }

        .pcalWrapper {
            position: relative;
        }

            .pcalWrapper > a.pcalBtn {
                position: absolute;
                left: 7px;
                top: 0;
                bottom: 0;
                margin: auto;
            }

            .pcalWrapper > input {
                padding-left: 30px;
            }

        .pcalBtn.disabled {
            pointer-events: none;
            cursor: default;
        }
    </style>
    <div class="modal fade" id="mdChangeProfregStatus" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <div class="panel panel-success" dir="rtl">
                    <div class="panel-heading">
                        <h4 class="modal-title">تغییر وضعیت سامانه ثبت اطلاعات اساتید</h4>
                    </div>
                    <div class="panel-body">
                        <div class="container-fluid" dir="rtl">
                            <div class="row" dir="rtl" style="border-radius: 5px 5px 0px 0px; padding: 1%;">
                                <div class="rwDialogPopup radconfirm">
                                    <div class="form-group">
                                        <div class="row">

                                            <span style="font-style: italic">وضعیت فعلی:  </span>
                                            <asp:Label ID="mdlblProfregStatus" runat="server" CssClass="alert alert-warning">سامانه باز است</asp:Label>

                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <h4>تغییر وضعیت سامانه ثبت اطلاعات اساتید:</h4>
                                            </div>
                                        </div>

                                        <div id="dvCloseProfregSystem" runat="server">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <span>از تاریخ</span>
                                                    <div class="pcalWrapper">
                                                        <asp:TextBox ID="txtChangeProfregStatus_Fromdate" runat="server" CssClass="form-control form-inline pcal"></asp:TextBox>
                                                    </div>
                                                    <asp:RegularExpressionValidator ID="dateValidatorFrom" ValidationExpression="\d{4}(?:/\d{2}){2}" ErrorMessage="تاریخ شروع معتبر نیست" ForeColor="Red" Display="Static" ValidationGroup="submit" ControlToValidate="txtChangeProfregStatus_Fromdate" runat="server"></asp:RegularExpressionValidator>


                                                </div>
                                                <div class="col-md-5">
                                                    <span>تا تاریخ</span>
                                                    <div class="pcalWrapper">
                                                        <asp:TextBox ID="txtChangeProfregStatus_Todate" CssClass="form-control form-inline pcal" runat="server"></asp:TextBox>
                                                    </div>

                                                    <asp:RegularExpressionValidator ID="dateValidatorTo" ValidationExpression="\d{4}(?:/\d{2}){2}" ErrorMessage="تاریخ پایان معتبر نیست" ForeColor="Red" Display="Dynamic" ValidationGroup="submit" ControlToValidate="txtChangeProfregStatus_Todate" runat="server"></asp:RegularExpressionValidator>

                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Button ID="mdbtnChangeProfregStatus" Text="ثبت و اعمال" ValidationGroup="submit" runat="server" CssClass="btn btn-danger btn-round" OnClick="mdbtnChangeProfregStatus_Click" />

                                                </div>
                                                <div class="col-md-2">
                                                    <button id="btnCancelClose" title="انصراف" data-dismiss="modal" runat="server" class="btn btn-success btn-round" onclick="mdbtnChangeProfregStatus_Click">انصراف</button>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="grd_Show">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="grd_Show"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="container" dir="rtl">
            <div class="row">
                <div class="col-md-4">
                    <span>وضعیت سامانه ثبت اطلاعات اساتید</span>
                    <br />
                    <asp:Label ID="lblProfregStatus" Font-Size="Larger" ForeColor="Red" runat="server" Text="تا تاریخ 1398/10/20 بسته است"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="btnShowProfregModal" OnClick="btnShowProfregModal_Click" Visible="false" runat="server" Text="تغییر وضعیت" CssClass="btn btn-round red" Width="100%" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnOpenProfReg" OnClick="btnOpenProfreg_Click" runat="server" Visible="true" Text="باز شدن سامانه ثبت اطلاعات اساتید" CssClass="btn btn-round green" Width="100%" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">
            </div>
            <div class="col-md-2">
                <asp:Button ID="btn_excel" runat="server" OnClick="btn_excel_Click" Enabled="true" Text="تبدیل به اکسل" CssClass="btn btn-success" />
            </div>
            <div class="col-md-5">
            </div>
        </div>
        <div dir="rtl">
            <telerik:RadGrid ID="grd_Show" runat="server" PageSize="50" BorderWidth="10px"
                AutoGenerateColumns="false" HorizontalAlign="Center" OnNeedDataSource="grd_Show_NeedDataSource" AllowPaging="true"
                OnItemCommand="grd_Show_ItemCommand"
                EnableEmbeddedSkins="false" OnExcelMLWorkBookCreated="grd_Show_ExcelMLWorkBookCreated" AllowFilteringByColumn="True" Skin="MyCustomSkin">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <MasterTableView DataKeyNames="ID">
                    <ItemStyle Font-Names="tahoma" HorizontalAlign="Center" BorderStyle="Ridge" />
                    <HeaderStyle Font-Names="tahoma" />
                    <AlternatingItemStyle Font-Names="tahoma" HorizontalAlign="Center" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف" HeaderStyle-Font-Bold="False">
                            <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="code_ostad" HeaderText="کد استاد در سیدا" AllowFiltering="true" Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="idd_meli" HeaderText="کدملی" AllowFiltering="true" Visible="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="name" HeaderText="نام" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="family" HeaderText="نام خانوادگی" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Field" HeaderText="رشته" AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="mobile" HeaderText="موبایل" AllowFiltering="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Cooperation" HeaderText="نوع همکاری" AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LastUpdate" HeaderText="تاریخ آخرین ویرایش" AllowFiltering="False" AllowSorting="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="regDate" HeaderText="تاریخ ثبت درخواست همکاری" AllowFiltering="False" AllowSorting="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="madrak" HeaderText="مدرک تحصیلی" AllowFiltering="False" Visible="true" AllowSorting="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="martabe_name" HeaderText="مرتبه علمی" Visible="true" AllowFiltering="False" AllowSorting="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="uniTypeKhedmat" HeaderText="نوع دانشگاه محل خدمت" Visible="true" AllowFiltering="False" AllowSorting="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="uniKhedmat" HeaderText="دانشگاه محل خدمت" Visible="true" AllowFiltering="False" AllowSorting="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="نمایش" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                            <ItemTemplate>
                                <asp:Button ID="btn_Detail" Text="مشاهده جزئیات" runat="server" CommandName="Detail" CommandArgument='<%#Eval("id")+","+Eval("name")+","+Eval("family")+","+Eval("mobile")+","+Eval("Field")+","+Eval("Cooperation")+","+Eval("status")+","+Eval("Date")+ ","+Eval("mobile")+","+Eval("martabeh") %>' CssClass="btn btn-success" Width="100px" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>








                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <asp:Label ID="lbl_Code" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lbl_Resault" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="Lbl_Status" runat="server"></asp:Label>
        <telerik:RadWindowManager ID="rwd" runat="server"></telerik:RadWindowManager>
    </div>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_pageLoaded(function (sender, e) {
                SetDatePicker();
            })
            //prm.add_endRequest(function (sender, e) {
            //    if (sender._postBackSettings.panelsToUpdate != null) {
            //        SetDatePicker();
            //    }
            //});
        };


        var pcalArray = [];
        function SetDatePicker() {
            var idArray = [];
            $('.pcal').each(function () {
                idArray.push(this.id);
            });

            if (idArray.length > 0) {
                for (var i = 0; i < idArray.length; i++) {
                    if (idArray[i] != 0) {
                        var x = new AMIB.persianCalendar(idArray[i],
                            { extraInputID: idArray[i], extraInputFormat: 'yyyy/mm/dd' });
                        pcalArray.push(x);
                    }
                }
            }

        }
    </script>
</asp:Content>
