<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeMainPage.aspx.cs" Inherits="IAUEC_Apps.UI.University.Research.CMS.EmployeeMainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../CommonUI/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../../../CommonUI/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <script>
        function CallBackConfirm(arg) {
            if (arg)
                window.location.href = "../../../CommonUI/IntroPage.aspx";
        }
    </script>


    <script type="text/javascript">
        function submitform() {
            document.forms["form1"].submit();
        }
    </script>

    <form id="form1" method="post" runat="server">


        <input type="hidden" runat="server" id="userCode" name="userCode" style="width: 200px;" />
        <input type="hidden" runat="server" id="e_Code" name="e_Code" style="width: 200px;" />
        <input type="hidden" runat="server" id="e_Status" name="e_Status" style="width: 200px;" />
        <%--<asp:TextBox ID="txt_stcode" runat="server"></asp:TextBox>--%>
        <%--<input type="text" name="stcode" runat="server" id="stcode" />--%>
        <%--<input type="text" id="txtFirstName" runat="server" name="txtFirstName"/>--%>
        <%--<button type="submit"  id="btnSend" value="stcode"   runat="server">Send</button>--%>
        <%--<input runat="server" id="btn_submit" type="submit" value="Submit"/>--%>

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" EnableViewState="false">
        </telerik:RadScriptManager>
        <br />

        <telerik:RadWindowManager ID="rwm_Message" runat="server" EnableViewState="false"></telerik:RadWindowManager>

        <div class="container" dir="rtl">
            <div class="row ">
                <div class="col-md-11" style="border:solid; background-color:lightyellow">
                        <br />
                    <div class="row">
                        <div class="col-md-5">

                            <asp:TextBox ID="txtStcode" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-7 text-center">
                            <asp:Button ID="btnPermit" runat="server" OnClick="btnPermit_Click" CssClass="btn btn-success" Text="اجازه ورود" />
                            <asp:Button ID="btnDontPermit" runat="server" OnClick="btnDontPermit_Click" CssClass="btn btn-danger" Text="عدم اجازه ورود" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-8 text-center align-content-center">
                            <asp:DataGrid ID="grdPermit" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="Gray" >
                                <HeaderStyle Width="200px"  BackColor="Blue" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundColumn DataField="stcode" HeaderText="کد دانشجویی"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="name" HeaderText="نام"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="family" HeaderText="نام خانوادگی" ></asp:BoundColumn>
                                    <asp:BoundColumn DataField="vaz" HeaderText="وضعیت در سیدا"></asp:BoundColumn>
                                </Columns>

                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnLoginPortal" Text="ورود به پرتال پژوهشی" runat="server" OnClick="btnLoginPortal_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
