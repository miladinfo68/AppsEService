<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamPaperDelivery.aspx.cs"
    MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master"
    Inherits="IAUEC_Apps.UI.University.Exam.CMS.ExamPaperDelivery" %>

<asp:Content ID="content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <style>
        .rtl {
            direction: rtl;
            text-align: center;
        }


        .setHeader {
            text-align: center;
            font-size: 16px;
            background-color: #9B59B6 !important;
            color: White;
            padding: 5px;
        }

        .msgWarning {
            padding: 20px;
            font-size: 25px;
            direction: rtl;
        }
    </style>
    <script type="text/javascript">

        $(function () {

            $("#<%=grdv.ClientID%> tr th").css("text-align", "center");
            $("#<%=grdv.ClientID%> tr:first").addClass("setHeader");

            //var keyCode = (window.event) ? e.which : e.keyCode;
            if (window.event.keyCode == 13) {//key press was enter
                //if (keyCode==13) {
                //$('#btnSearching').click(function (e) {

                document.getElementById("<%=btnSearching.ClientID %>").click();
                //});
            }

        });


        function conf(did) {
            var mssg = " کاربر گرامی آیا اطمینان دارید که برگه های با کد  مشخصه ";
            mssg += did;
            mssg += " را از استاد مربوطه دریافت نموده اید ؟";
            return confirm(mssg);
        }

    </script>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="PageTitle" runat="server">
    <h3>فرم تحویل برگه های امتحان</h3>
</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container ">
        <br />

        <div class="col-sm-12 row rtl">
            <div class="col-sm-3">
                <span>ترم : </span>
                <asp:DropDownList runat="server" ID="ddlTerm" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="col-sm-3">
                <span>کد استاد : </span>
                <input type="text" id="txtCodeOst" name="txtCodeOst" value="" runat="server" />
            </div>
            <div class="col-sm-4">
                <span>نام خانوادگی استاد : </span>
                <input type="text" id="txtOstFamily" name="txtOstFamily" value="" runat="server" />
            </div>

            <div class="col-sm-2">
                <asp:Button Text="جســـــتجو" runat="server" ID="btnSearching" OnClick="btnSearching_ServerClick" />
            </div>
        </div>

        <br />
        <br />
        <br />
    </div>

    <div class="container ">
        <div class="rtl ">
            <asp:GridView runat="server" ID="grdv" AutoGenerateColumns="False" OnRowDataBound="grdv_RowDataBound" BackColor="#3A4A5B" ForeColor="White">
                <Columns>
                    <asp:TemplateField HeaderText="ردیف" ItemStyle-Width="70">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>

                        <ItemStyle Width="70px"></ItemStyle>
                    </asp:TemplateField>

                    <asp:BoundField DataField="code_ostad" HeaderText="کد استاد " ItemStyle-Width="100">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="fullname" ItemStyle-Width="10" HeaderText="نام و نام خانوادگی استاد">
                        <ItemStyle Width="200px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="did" HeaderText="کد مشخصه درس " ItemStyle-Width="150">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="namedars" HeaderText="نام درس " ItemStyle-Width="300">
                        <ItemStyle Width="300px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="dateexam" HeaderText="تاریخ امتحان " ItemStyle-Width="100">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="mobile" HeaderText="شماره تماس استاد" ItemStyle-Width="120">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Status" ItemStyle-Width="10" HeaderText="وضعيت" Visible="false">
                        <ItemStyle Width="50px"></ItemStyle>
                    </asp:BoundField>

                    <asp:TemplateField ItemStyle-Width="200" HeaderText="وضعیت برگه امتحان">
                        <ItemTemplate>
                            <asp:Button ID="btnUpdateStatus" runat="server" CommandName="update"
                                Text="دریافت برگه امتحان" CommandArgument='<%#Eval("did") + ";" +Eval("code_ostad")%>' OnCommand="btnUpdateStatus_Command" Width="150px"
                                OnClientClick='<%# " if(conf(" +Eval("did") + " )) return true ; else return false; " %>' />
                        </ItemTemplate>
                        <ItemStyle Width="300px"></ItemStyle>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>
    </div>
    <div class="container msgWarning" runat="server" visible="false" id="msgWarning">
        <p class="alert alert-danger">برگه امتحانی برای استاد مورد نظر یافت نشد</p>
    </div>
</asp:Content>

