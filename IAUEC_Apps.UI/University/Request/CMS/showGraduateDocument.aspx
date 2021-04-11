<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showGraduateDocument.aspx.cs" MasterPageFile="~/University/Request/MasterPages/CMSCheckOutRequestMaster.Master" Inherits="IAUEC_Apps.UI.University.Request.CMS.showGraduateDocument" %>

<asp:Content runat="server" ID="c2" ContentPlaceHolderID="HeaderplaceHolder">
</asp:Content>
<asp:Content runat="server" ID="c1" ContentPlaceHolderID="PageTitle">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ID="c3" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        input[readonly='readonly'] {
            background: #d6dee8;
            text-align: center;
        }



        input[disabled='disabled'] {
            background-color: #d6dee8;
            text-align: center;
        }

        .grid td, .grid th {
            text-align: center;
        }

        .anchorRight {
            right: 0;
        }

        .anchorLeft {
            left: 0;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#exampleModal').modal('show');
        }
        function closeModal() {
            $('#exampleModal').modal('hide');
        }
        function AddVahedSodur() {
            $('#modalVahedSodur').modal('show');
        }

        function closeVahedSodur() {
            $('#modalVahedSodur').modal('hide');
        }
        function grdReload(sender, args) {
            refresgGrid();
        }
    </script>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">پیام</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                            <div class="rwDialogPopup radconfirm">
                                <div class="rwDialogText">
                                    <asp:Literal ID="confirmMessage" runat="server" Text="آیا از تایید تغییرات انجام شده اطمینان دارید؟" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="rbConfirmed_RegisterChanges" runat="server" OnClick="rbConfirmed_RegisterChanges_Click" Text="بله">
                                    </telerik:RadButton>
                                    <%--<telerik:RadButton ID="rbConfirm_Cancel1" runat="server" OnClientClicked="closeCustomConfirm1" Text="خیر">
                                    </telerik:RadButton>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalVahedSodur" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">ثبت واحد صدور</h4>
                </div>
                <div class="modal-body">
                    <div class="container-fluid" dir="rtl">
                        <asp:UpdatePanel runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="row" style="border: 1px solid rgba(51, 181, 229,0.9); background-color: rgba(51, 181, 229,0.7); border-radius: 5px 5px 0px 0px; padding: 1%; color: #fff">
                                    <div class="rwDialogPopup radconfirm">
                                        <div class="form-inline">

                                            <div class="form-group">
                                                <label for="txtDescription">نام واحد :</label>
                                                <asp:DropDownList BackColor="Blue" ID="drpMahaleSodooreMadrak" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <asp:Button ID="btnSubmitVahedSorud" Text="ثبت و تایید" runat="server" CssClass="btn btn-primary" OnClick="btnSubmitVahedSorud_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); margin-bottom: 1%; color: #000; padding-left: 5px">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <telerik:RadWindow ID="RadWindowVahed" runat="server" Width="300" Height="300" Modal="true">
        <ContentTemplate>

            <div dir="rtl">
                <div class="bg-green" style="padding: 5px">
                    <h4 style="font-family: 'B Titr'">انتخاب محل صدور مدرک</h4>
                </div>


            </div>
        </ContentTemplate>

    </telerik:RadWindow>


    <div dir="rtl">
        <%-- <asp:UpdatePanel runat="server">
            <ContentTemplate>--%>
        <div class="container">

            <div class="panel panel-primary">
                <div class="panel panel-heading">
                    <h3>جستجوی دانشجو</h3>
                </div>
                <div class="panel panel-body">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label runat="server" Text="شماره دانشجویی"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtSearchStcode"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label runat="server" Text="نام خانوادگی"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtSearchFamnily"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnSearchStudent" CssClass="btn btn-primary" runat="server" Text="جستجو" OnClick="btnSearchStudent_Click" />
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnAddStudentAsGraduate" CssClass="btn btn-success" runat="server" Text="اضافه به عنوان فارغ التحصیل" Visible="false" OnClick="btnAddStudentAsGraduate_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" ID="lblNotFound" CssClass="label label-danger" Font-Size="Small" Text="دانشجوی مورد نظر وجود ندارد و یا درخواست فارغ التحصیلی به معاونت دانشجویی نرسیده است" Visible="false"></asp:Label>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="table-responsive">
                            <asp:GridView ID="grdResults" runat="server" ShowHeaderWhenEmpty="True" Width="100%" CssClass="grid" CellPadding="4" HorizontalAlign="Center" ForeColor="DarkBlue" GridLines="None" AllowPaging="True" OnPageIndexChanging="grdResults_PageIndexChanging" OnRowDataBound="grdResults_RowDataBound" OnSelectedIndexChanged="grdResults_SelectedIndexChanged" OnRowCommand="grdResults_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>

                                            <asp:Button runat="server" ID="btnView" CssClass="btn-primary" Text="انتخاب" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' CommandName="btnView" />
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رديف" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# (Container.DataItemIndex + 1) %>' runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Button" Text="انتخاب" HeaderText="انتخاب" Visible="false" />
                                </Columns>
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#99ffcc" />
                                <FooterStyle BackColor="#66ccff" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="DarkBlue" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="DarkBlue" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#33ccff" />
                                <SelectedRowStyle BackColor="#0066cc" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <%-- </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearchStudent" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAddStudentAsGraduate" EventName="Click" />

            </Triggers>

        </asp:UpdatePanel>--%>

        <div id="dvShowReport" runat="server" visible="false">

            <div class="panel panel-purple">
                <div class="panel panel-heading">
                    <h2>
                        <a class="btn btn-link" style="color: rebeccapurple; font-size: medium" data-toggle="collapse" data-target="#collapseStInf" aria-expanded="true" aria-controls="collapseStInf">مشخصات فردی و آموزشی دانشجو</a>
                    </h2>
                </div>
                <div class="panel panel-body">
                    <div id="collapseStInf" class="collapse show in " aria-labelledby="headingOne" data-parent="#dvShowReport">
                        <div class="container show">
                            <div class="table">
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="کد دانشجویی"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtStcode" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="جنسیت"></asp:Label>

                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlGender" runat="server" Enabled="false" Width="80%" BackColor="#d6dee8">
                                            <asp:ListItem Text="نامشخص" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="مرد" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="زن" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label runat="server" ForeColor="Purple" Text="مرحله فعلی تسویه حساب"></asp:Label>
                                        <asp:TextBox ID="txtRequestLog" ReadOnly="true" BackColor="Purple" ForeColor="White" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <telerik:RadBinaryImage ID="picStudent" runat="server" Width="90px" Height="120px" ResizeMode="Fill" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="نام"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtName" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="نام خانوادگی"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtFamily" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="کد ملی"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtIDDMeli" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="نام پدر"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtFatherName" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="رشته"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtReshte" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="گرایش"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtGerayesh" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="مقطع"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtMaghta" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="نیمسال آخر"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtLastTerm" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="شماره تماس"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtPhone" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-success">
                <div class="panel panel-heading">
                    <h2>
                        <a class="btn btn-link" style="color: darkgreen; font-size: medium" data-toggle="collapse" data-target="#collapseDossier" aria-expanded="true" aria-controls="collapseDossier">مشخصات پرونده ای دانشجو
                        </a>
                    </h2>
                </div>
                <div class="panel panel-body">
                    <div id="collapseDossier" class="collapse  show in " aria-labelledby="headingTwo" data-parent="#dvShowReport">
                        <div class="show container">
                            <div class="table">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ ورود پرونده به بخش دانشجویی"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateEnterStudentDep" ReadOnly="true" runat="server"></asp:TextBox>
                                        <label runat="server" id="lblCountEnterStudentDep" class="label label-danger" style="font-size: xx-small"></label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ عودت پرونده به دانشکده"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateRejToDep" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="علت عودت پرونده به دانشکده"></asp:Label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtResRejToDep" TextMode="MultiLine" Height="50px" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="نقص پرونده:" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="cbxFish" Text="فیش واریزی ندارد" runat="server" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="cbxTambr" Text="تمبر ندارد" runat="server" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="کد"></asp:Label>

                                        <asp:TextBox ID="txtDocArchiveCode" Width="100px" Enabled="false" runat="server" BackColor="#d6dee8"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="cbxVamdar" Enabled="false" Text="دانشجو وام دار است" runat="server" BackColor="#d6dee8" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="cbxMashmul" Text="دانشجو مشمول سربازی است" runat="server" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="کد بایگانی"></asp:Label>
                                        <asp:TextBox ID="txtBaygani" Width="100px" Enabled="false" runat="server" BackColor="#d6dee8"></asp:TextBox>
                                        <asp:TextBox ID="txtNewBaygani" Width="100px" Enabled="true" Visible="false" runat="server"></asp:TextBox>
                                        <asp:Button CssClass="btn btn-success" ID="btnCreateArchiveCode" runat="server" Text="ایجاد" Visible="false" OnClick="btnCreateArchiveCode_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="panel panel-warning">
                <div class="panel panel-heading">
                    <h2>
                        <a class="btn btn-link" style="color: orangered; font-size: medium" data-toggle="collapse" data-target="#collapseDocInf" aria-expanded="true" aria-controls="collapseDocInf">مشخصات مدرک دانشجو
                        </a></h2>
                    <asp:LinkButton ForeColor="#cc33ff" Font-Size="Larger" ID="lnkBtnEnterDate" OnClick="lnkBtnEnterDate_Click" runat="server" Text="ورود به صفحه وضعیت مدارک دانشجو "></asp:LinkButton>
                </div>
                <div class="panel panel-body">
                    <div id="collapseDocInf" class="collapse  show in " aria-labelledby="headingOne" data-parent="#dvShowReport">
                        <div class="show container">
                            <div class="table">
                                <div id="dvSerialAndDocNum" runat="server" visible="true">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label runat="server" Text="سریال مدرک گواهی موقت"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtSerialMovaghat" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label runat="server" Text="شماره مدرک گواهی موقت"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtDocNumMovaghat" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnPrintGovahi" Text="پرینت گواهی موقت" CssClass="btn btn-warning form-control" runat="server" OnClick="btnPrintGovahi_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label runat="server" Text="سریال مدرک دانشنامه"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtSerialDaneshname" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label runat="server" Text="شماره مدرک دانشنامه"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtDocNumDaneshname" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnPrintDaneshname" Text="پرینت دانشنامه" CssClass="btn btn-warning form-control" runat="server" OnClick="btnPrintDaneshname_Click" />
                                        </div>
                                    </div>
                                    <br />
                                </div>
                                <div class="row red">
                                    <div class="col-md-2">
                                        <asp:Label CssClass="anchorRight" runat="server" Text="کد بایگانی گواهی موقت"></asp:Label>
                                    </div>
                                    <div class="col-md-2">

                                        <asp:TextBox ID="txtArchiveCode_Govahi" ReadOnly="true" runat="server" Text="-"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="کد بایگانی دانشنامه"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtArchiveCode_Daneshname" ReadOnly="true" runat="server" Text="-"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="کد بایگانی ریزنمره"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtArchiveCode_Riznomre" ReadOnly="true" runat="server" Text="-"></asp:TextBox>
                                    </div>
                                </div>
                                <br />


                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="واحد صادر کننده مدرک"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtNameVahedSodur" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ ارسال پک به واحد صادر کننده"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateSendPackToVahedSodur" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:CheckBox Enabled="false" ID="cbxPaymentToVahedSodur" runat="server" Text="پرداخت به واحد صادر کننده مدرک" BackColor="#d6dee8"></asp:CheckBox>

                                    </div>
                                    <%--<div class="col-md-2"></div>--%>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ صدور گواهی موقت"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateSodurGovahi" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ ورود گواهی موقت"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateVorudGovahi" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ تحویل گواهی موقت"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateTahvilGovahi" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ صدور دانشنامه"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateSodurDanesh" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ ورود دانشنامه"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateVorudDanesh" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label runat="server" Text="تاریخ تحویل دانشنامه"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateTahvilDanesh" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="صدور ریز نمره"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateSodurRiznomre" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="ارسال ریز نمره"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateErsalRiznomre" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="ورود ریز نمره"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateVorudRiznomre" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="تحویل ریز نمره"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDateTahvilRiznomre" ReadOnly="true" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-1">
                                        <asp:Label runat="server" Text="موارد خاص"></asp:Label>
                                    </div>
                                    <div class="col-md-11">

                                        <asp:TextBox runat="server" TextMode="MultiLine" Height="50px" ID="txtMavaredKhas" Width="100%" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <%--            <asp:UpdatePanel runat="server">
                <ContentTemplate>--%>
            <div class="panel panel-danger">
                <div class="panel panel-heading">
                </div>
                <div class="panel panel-body">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Button ID="btnRegisterChanges" CssClass="btn btn-danger" runat="server" OnClick="btnRegisterChanges_Click" Text="ثبت تغییرات" />
                        </div>
                    </div>
                </div>
            </div>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRegisterChanges" EventName="Click" />
                </Triggers>

            </asp:UpdatePanel>--%>
        </div>
    </div>

</asp:Content>

