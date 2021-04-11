<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="classrangelistUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.CMS.classrangelistUI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../CommonUI/css/bootstrap.min.css" rel="stylesheet" />

    <link href="../MasterPages/css/custom.css" rel="stylesheet" />
    <style>
        .col-md-2 {
            min-width: 16.66666667%;
        }
    </style>
</head>
<body class="nav-md" style="background: #F7F7F7">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="container-fluid" id="div1" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); border-radius: 5px; margin-bottom: 1%; padding: 1%; color: #000">
            <div class="row">
                <div class="col-md-2">
                    <%--ظرفیت کلاس--%>تعداد نفرات باقیمانده
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lbl_Zarfiat" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    <%--ظرفیت پر شده--%>
                </div>
                <div class="col-md-2">
                    <%--<asp:Label ID="lbl_Porshode" runat="server"></asp:Label>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <span>تعداد صندلی باقیمانده</span>
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblCapacityLeft" Text="---"></asp:Label>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">

                    <div class="col-md-1">شماره کلاس:</div>
                    <div class="col-md-2">
                        <asp:DropDownList runat="server" ID="ddl_ClassNumber" CssClass="form-control input-sm" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True" Width="100px" ForeColor="Black">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">از شماره:</div>

                    <div class="col-md-1">
                        <asp:DropDownList ID="ddl_az" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ForeColor="Black">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-1">تا شماره:</div>
                <div class="col-md-1">
                    <asp:DropDownList ID="ddl_ta" runat="server" CssClass="form-control input-sm" AutoPostBack="True" ForeColor="Black">
                    </asp:DropDownList>
                </div>

                <div class="col-md-1">
                    <%--<asp:Button ID="btn_takhsis" runat="server" OnClick="Button3_Click" Text="تخصیص صندلی" CssClass="btn btn-success" Visible="false" />--%>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btn_sabt" runat="server" CssClass="btn btn-exam" OnClick="Button2_Click" Text="ثبت" /></div>

            </div>
        </div>
        <div class="container-fluid" id="div2" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); border-radius: 5px; margin-bottom: 1%; padding: 1%; color: #000">
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grd_Class" runat="server" OnItemCommand="grd_Class_ItemCommand" AutoGenerateColumns="false" EnableEmbeddedSkins="False" GridLines="Horizontal">
                        <MasterTableView DataKeyNames="did">
                            <ItemStyle />
                            <HeaderStyle HorizontalAlign="Center" />
                            <AlternatingItemStyle />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                    <ItemTemplate><%# Container.ItemIndex + 1 %></ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="did" AllowFiltering="false" HeaderText="شماره کلاس">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ExamPlace" HeaderText="سالن" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StartRange" HeaderText="شروع بازه" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EndRange" HeaderText="پایان بازه" AllowFiltering="false">
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn AllowFiltering="false">

                                    <HeaderTemplate>
                                        حذف بازه
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Button ID="btn_delete" runat="server" Enabled="true" CommandArgument='<%#Eval("IDExamClass") %>' CommandName="delete" CssClass="btn btn-success" Text="حذف" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
        <telerik:RadWindowManager ID="rwm" runat="server"></telerik:RadWindowManager>


    </form>
</body>
</html>
