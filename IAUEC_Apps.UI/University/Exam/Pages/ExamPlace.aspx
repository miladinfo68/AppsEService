<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/PagesMasterPage.Master" AutoEventWireup="true" CodeBehind="ExamPlace.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.Pages.ExamPlace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <link href="../../Theme/css/ExamGrid.CustomSkin.css" rel="stylesheet" />

    <link href="../../Theme/css/Menu.MyCustomSkin.css" rel="stylesheet" />
    <style>
        .azmoon {
            font-size: 22px;
            color: #000;
        }
    .btnPostToLMS{
    font-size: 18px;
    font-weight: 400;
    padding: 20px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" id="div_Main" runat="server" dir="rtl" style="border: 1px solid rgba(170, 102, 204,0.7); background-color: rgba(170, 102, 204,0.1); border-radius: 5px; margin-bottom: 1%; padding: 1%; color: #000">
        <br />
        <%--        <div class="alert alert-success text-center msgonline">
            <h>جهت شرکت در امتحانات ترم جاری به سامانه آزمون آنلاین به آدرس <a href="https://azmoon.iauec.ac.ir" class="azmoon">azmoon.iauec.ac.ir</a> مراجعه نمایید.</h>
        </div>--%>
        <div class="alert alert-success text-center msgonline">
            <h2>جهت شرکت در امتحانات ترم جاری به سامانه آزمون آنلاین مراجعه نمایید.</h2>
        </div>
        <%--<asp:Button runat="server" ID="btnPostToLMS" CssClass="btn btn-success col-sm-12 btnPostToLMS" OnClick="btnPostToLMS_Click" Text="جهت شرکت در امتحانات ترم جاری اینجا را کلیک نمایید." />--%>
        <div id="exm_plc">
            <div class="col-md-push-4">
                <asp:Label Style="font-size: large; font-weight: 200; font-family: Tahoma; float: right" runat="server">محل برگزاری امتحان:</asp:Label>
            </div>
            <div class="col-md-offset-1">
                <asp:Label runat="server" ID="lblExam" Font-Bold="true" Style="font-size: large; font-weight: 200; font-family: Tahoma; float: right"></asp:Label>
            </div>
        </div>

        <div id="pnlMsgs">
            <div class="">
                <div class="col-md-12">
                    <asp:Label ID="Label1" Style="font-size: large; font-weight: 200; font-family: Tahoma; float: right" runat="server">تذکر 1 :نمایش تخصیص صندلی تنها برای حوزه امتحانی شهر تهران انجام می پذیرد . </asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="Label2" Style="font-size: large; font-weight: 200; font-family: Tahoma; float: right" runat="server">تذکر 2 : با توجه به اینکه تخصیص صندلی به صورت اتوماتیک توسط سامانه، یک روز قبل از روز برگزاری امتحان صورت می گیرد، لازم است دانشجویان نسبت به <span style="color:red">بازبینی این صفحه پیش از حضور در حوزه امتحانی</span> اقدام نمایند. </asp:Label>
                </div>
            </div>
        </div>




        <div class="row" style="overflow-x: auto;">
            <div class="col-md-12">
                <telerik:RadGrid HorizontalAlign="Center" ID="grd_Class" runat="server" AutoGenerateColumns="False" Skin="MyCustomSkin" EnableEmbeddedSkins="False"
                    OnNeedDataSource="grd_Class_NeedDataSource" CellSpacing="0" GridLines="Horizontal">
                    <MasterTableView DataKeyNames="did" CssClass="table">
                        <ItemStyle />
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Large" />
                        <ItemStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Large" />
                        <AlternatingItemStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Large" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="ردیف">
                                <ItemTemplate>
                                    <%# Container.ItemIndex + 1 %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="did" HeaderText="کد کلاس">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="namedars" HeaderText="نام درس">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ostadname" HeaderText="نام استاد">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="dateexam" HeaderText="تاریخ امتحان">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="saatexam" HeaderText="ساعت امتحان">
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="ExamPlace" HeaderText="سالن امتحان">
                            </telerik:GridBoundColumn>--%>
                            <%--   <telerik:GridBoundColumn DataField="SeatNumber" HeaderText="شماره صندلی">
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn>
                            </EditColumn>
                        </EditFormSettings>

                    </MasterTableView>



                </telerik:RadGrid>
            </div>
        </div>
        <br />

    </div>


    <script>
        $(() => {
            $("#exm_plc").hide();
            $("#pnlMsgs").hide();
        });


    </script>
</asp:Content>
