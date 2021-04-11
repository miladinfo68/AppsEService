<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrawEditedFieldGrid.aspx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.DrawEditedFieldGrid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <table class="table table-responsive table-bordered table-head table-hover center-margin" dir="rtl" style="border-bottom-color: black">
                <tr class="bg-blue-sky" style="text-align: center;">
                    <th>نام فیلد</th>
                    <th>مقدار قدیم</th>
                    <th>مقدار جدید</th>
                </tr>

                <asp:ListView ID="detailTree" runat="server">
                    <ItemTemplate>
                        <tr class="bg-green" style="text-align: center;">
                            <td>
                                <%#Eval("fieldName") %>
                            </td>
                            <td>
                                <%#Eval("oldValue") %>
                            </td>
                            <td>
                                <%#Eval("newValue") %>
                            </td>

                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </table>

        </div>
    </form>
</body>
</html>
