<%@ Page Title="" Language="C#" MasterPageFile="~/Contact/MasterPage/MasterPageContactOS.Master" AutoEventWireup="true" CodeBehind="ContactOstads.aspx.cs" Inherits="IAUEC_Apps.UI.Contact.ContactOstad.ConatctOstads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        var dtContantSt = Session["ContactStudent"] as System.Data.DataTable;


    %>

    <asp:Label ID="LblIdUser" runat="server" CssClass="hide" Text="Label"></asp:Label>

    <table class="table table-hover   table-striped " id="table-rowSt">
        <thead class="thead-dark">
            <tr>
                <th class="text-center" scope="col">نام دانشجو</th>
                <th class="text-center" scope="col">مسئولیت استاد</th>
                <th class="text-center" scope="colgroup">گفتگو </th>

                <%-- <th class="text-center" scope="col"> خوانده نشده </th>--%>
            </tr>
        </thead>
        <tbody>

            <%for (int i = 0; i < dtContantSt.Rows.Count; ++i)
                { %>

            <tr class="RowTableStudent1">

                <td class="text-center" style="width: 33%"><%=dtContantSt.Rows[i]["FullName"].ToString()%></td>
                <td class="text-center" style="width: 33%"><%=dtContantSt.Rows[i]["NameTypeOstad"].ToString()%></td>
                <td class="text-center" style="width: 34%">

                    <button type="button" class="btn btn-primary" onclick="location.href='<%= string.Format("ContactByStudent.aspx?Flag_Grp={0}&IdGrpOrPerson={1}", false, dtContantSt.Rows[i]["stcode"].ToString())%>'">
                        خصوصی
                    <span style="float: right; margin-left: 4px"><i class="fa fa-user" aria-hidden="true"></i></span>
                        <span class="badge badge-light  badgeMsg" data-toggle="tooltip" data-placement="top" title="شخصی" style="background-color: #ffffff">
                            <%=((dtContantSt.Rows[i]["countUnreadPerson"]!=null&&dtContantSt.Rows[i]["ID_Sender"]!=null&&dtContantSt.Rows[i]["countUnreadPerson"].ToString().Trim()!=""
                           &&dtContantSt.Rows[i]["ID_Sender"].ToString().Trim()!="")?dtContantSt.Rows[i]["countUnreadPerson"].ToString().Trim():"0")%></span>

                    </button>


                    <button type="button" class="btn btn-success" onclick="location.href='<%= string.Format("ContactByStudent.aspx?Flag_Grp={0}&IdGrpOrPerson={1}", true,dtContantSt.Rows[i]["stcode"].ToString())%>'">
                        دفاع
                       <span style="float: right; margin-left: 4px"><i class="fa fa-users" aria-hidden="true"></i></span>
                        <span class="badge  badge-light badgeMsg" style="background-color: #ffffff" data-toggle="tooltip" data-placement="top" title="دفاع">
                            <%=((dtContantSt.Rows[i]["countUnreadGroup"]!=null&&dtContantSt.Rows[i]["ID_Grp"]!=null&&dtContantSt.Rows[i]["countUnreadGroup"].ToString().Trim()!=""
                           &&dtContantSt.Rows[i]["ID_Grp"].ToString().Trim()!="")?dtContantSt.Rows[i]["countUnreadGroup"].ToString().Trim():"0")%></span>



                    </button>
                </td>

                <%--   <td class="text-center"> 
         
                   <span class="badge badge-secondary badgeMsg" data-toggle="tooltip" data-placement="top" title="کل" style="background-color:#007bff">
                   <%=((dtContantSt.Rows[i]["totalMsg"]!=null?dtContantSt.Rows[i]["totalMsg"].ToString().Trim():"0"))%></span> </td>--%>
            </tr>
            <%} %>
        </tbody>
    </table>

</asp:Content>
