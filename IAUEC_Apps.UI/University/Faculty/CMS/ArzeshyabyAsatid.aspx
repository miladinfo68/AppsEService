<%@ Page Title="" Language="C#" MasterPageFile="~/University/Faculty/MasterPages/FacultyMasterPage.Master" AutoEventWireup="true" CodeBehind="ArzeshyabyAsatid.aspx.cs" Inherits="IAUEC_Apps.UI.University.Faculty.CMS.ArzeshyabyAsatid" %>
<%@ Register Assembly="Stimulsoft.Report.Web, Version=2014.3.0.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="text-align:center; font-family:Tahoma; font-size:small;">
        <asp:panel id="pnl_main" runat="server" BorderStyle="Groove" Width="70%" HorizontalAlign="Center" Direction="RightToLeft" BackColor="SkyBlue">
    <div>
        <table align="center">
            <tr>
                <td>
                    <asp:Label ID="lbl_Term" Text="ترم:" runat="server" div="center"></asp:Label>
                </td>
                <td>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                             <ContentTemplate>
                                  <asp:DropDownList ID="ddl_Term" runat="server" AutoPostBack="true" Width="160px" OnSelectedIndexChanged="ddl_Term_SelectedIndexChanged"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Term" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                   <asp:Label ID="lbl_CodeOstad" Text="کداستاد :" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_CodeOstad" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_Select" runat="server" Text="انتخاب کنید" OnClick="btn_Select_Click" />
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="lbl_CpdeGroup" Text="گروه :" runat="server"></asp:Label>
                </td>
                <td>
                      <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                             <ContentTemplate>
                               <asp:DropDownList ID="ddl_CodeGroup" runat="server" Width="160px" OnSelectedIndexChanged="ddl_CodeGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_CodeGroup" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_CodeDars" runat="server" Text="درس :"></asp:Label>
                </td>
                <td>
                   <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                             <ContentTemplate>
                                <asp:DropDownList ID="ddl_CodeDras" runat="server" Width="160px" OnSelectedIndexChanged="ddl_CodeDras_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                             </ContentTemplate>
                             <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="ddl_CodeDras" EventName="SelectedIndexChanged" />
                             </Triggers>
                       </asp:UpdatePanel>
                   
                </td>
            </tr>

            </table>
        <%--<HR style="text-align:center; width:50%; border:thick;" />--%>
        <br />
        <br />
        <table >
            <tr>
                <td style="padding-left:70%">
                    <asp:Button ID="btn_EvalutionProf" runat="server" Text="ارزشیابی اساتید" Width="110px" OnClick="btn_EvalutionProf_Click" />
                </td>
                </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_EvalutionProfByLesson" runat="server" Text="ارزشیابی اساتید به تفکیک مشخصه" Width="215px" OnClick="btn_EvalutionProfByLesson_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_EvalutionProfByItemLesson" runat="server" Text="ارزشیابی اساتید به تفکیک درس ، استاد ، دانشکده" OnClick="btn_EvalutionProfByItemLesson_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_EvalutionAll" runat="server" Text="ارزشیابی اساتید به استاد، درس، رشته، سوال" OnClick="btn_EvalutionAll_Click" />
                </td>
            </tr>
            </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btn_EvalutionProfByItem" runat="server" Text="ارزشیابی اساتید به تفکیک استاد، درس ، رشته" Width="305px" OnClick="btn_EvalutionProfByItem_Click" />
                </td>
            </tr>
        </table>
<%--        <table style="padding-left:20%">
            <tr>
                <td>                 
                    <asp:Button ID="btn_EvalutionProfbyItem" runat="server" Text="ارزشیابی اساتید به تفکیک استاد،درس،رشته" Width="305px" OnClick ="btn_EvalutionProfbyItem_Click"/>
                </td>
                </tr>
            <tr><td></td></tr>
            <tr>
                <td>
                    <asp:Button ID="btn_EvalutionProfbyItemLesson" runat="server" Text="ارزشیابی اساتید به تفکیک درس،استاد،دانشکده" Width="305px" OnClick="btn_EvalutionProfbyItemLesson_Click"/>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_EvalutionAll" runat="server" Text="ارزشیابی اساتید به تفکیک استاد،رشته،درس، سوال" Width="310px" OnClick="btn_EvalutionAll_Click"/>
                </td>
            </tr>--%>

        <%--</table>--%>
    </div>
            </asp:panel>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
             <cc1:StiWebViewer ID="StiWebViewer1" runat="server" Height="100%" Width="100%" ViewMode="OnePage" RenderMode="AjaxWithCache" ShowZoom="False" ToolbarAlignment="Right" ShowParametersButton="False" />
            
           </div>
</asp:Content>
