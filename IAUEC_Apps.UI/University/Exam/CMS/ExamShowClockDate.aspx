<%@ Page Title="" Language="C#" MasterPageFile="~/University/Exam/MasterPages/CMSExamMaster.Master" AutoEventWireup="true" CodeBehind="ExamShowClockDate.aspx.cs" Inherits="IAUEC_Apps.UI.University.Exam.MasterPages.ExamShowClockDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    <title>
        <asp:Literal ID="t" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <asp:Literal ID="pt" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

    </script>
    <center>
           
                 <table dir="rtl" >
                <tr>
                    <td>
                      <asp:Label ID="lbl_Term" runat="server" BorderColor="#CC00CC" Font-Names="B Nazanin" Font-Size ="Small" Font-Bold="true" ForeColor="Black"  dir="rtl" Text=" انتخاب ترم :" ></asp:Label>
                    </td>
                     <td>
                         <asp:DropDownList runat ="server" ID="ddl_Term" Font-Names ="tahoma" Font-Size ="Small" ForeColor="Black"  ></asp:DropDownList>
                     </td>
                </tr>
                <tr >
                    <td>
                    </td>
                    <td class="auto-style1" >
                        <asp:Button ID="btn_Search" runat="server"  OnClick ="btn_Search_Click"   Text="جستجو"  BackColor="#9B59B6" Width="100px" Font-Bold="True" Font-Names="B Nazanin" Font-Size="Medium" ForeColor="White"   />
                    </td>
                </tr>
            </table>   
            </center>
    <br />
    <br />
    <br />
    <br />
    <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
    </telerik:RadWindowManager>
    <br />
    <br />
    <br />
    <telerik:RadGrid ID="grd_ShowExam" runat="server" AutoGenerateColumns="False" ActiveItemStyleWidth="100%" CellSpacing="0" GridLines="None" EnableEmbeddedSkins="False" BackColor="#3A4A5B">
        <MasterTableView>
            <HeaderStyle Font-Names="tahoma" Font-Size="Small" HorizontalAlign="Center" CssClass="bg-purple" />
            <Columns>

                <telerik:GridBoundColumn DataField="family" HeaderText="نام استاد" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="did" HeaderText="کد مشخصه  " ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="dars" HeaderText="نام درس" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="saatexam" HeaderText="تاریخ امتحان" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="dateexam" HeaderText="ساعت امتحان" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="nameresh" HeaderText=" رشته" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="namedanesh" HeaderText="دانشکده" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
    <br />
</asp:Content>
