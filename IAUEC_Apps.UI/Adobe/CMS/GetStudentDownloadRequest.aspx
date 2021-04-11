<%@ Page Title="" Language="C#" MasterPageFile="~/Adobe/MastePage/CMSAdobeMaster.Master" AutoEventWireup="true" CodeBehind="GetStudentDownloadRequest.aspx.cs" Inherits="IAUEC_Apps.UI.Adobe.CMS.GetStudentDownloadRequest" %>
<%@ Register src="../../CommonUI/AccessControl.ascx" tagname="AccessControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
    
    <title><asp:Literal ID="t" runat="server"></asp:Literal></title>
    </asp:Content>
<asp:Content ID="contentTitle" ContentPlaceHolderID="PageTitle" runat="server"><asp:Literal ID="pt" runat="server"></asp:Literal></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwmValidations" runat="server">
    </telerik:RadWindowManager>
    <asp:Panel ID="p" runat="server" DefaultButton="btn_Search"> 
    <div class="col-md-12">
    <table runat="server" id="tbl_StudentDownloadRequest" class="col-md-12">
        <tr>
            <td>
                <asp:Label ID="lbl_StCode" runat="server" Text="شماره دانشجویی"></asp:Label>
                 </td>
            <td>
                <asp:TextBox ID="txt_StCode" runat="server"></asp:TextBox>
                </td>
           
            <td>
 <asp:Label ID="lbl_Term" runat="server" Text="ترم"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddl_term" runat="server" OnDataBinding="ddl_term_DataBinding">
                 </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btn_Search" runat="server" Text="جستجو" OnClick="btn_search_Click" />
            </td>
        </tr>
        </table>
        <br />
        <uc1:AccessControl ID="AccessControl1" runat="server" />

        <table class="col-md-12">
        <tr>
            <td style="text-align:center">
                <telerik:RadGrid ID="grd_StudentDownloadRequest" AllowPaging="true" PageSize="20" AutoGenerateColumns="false" runat="server" Visible="false">
                      <MasterTableView>
                     <HeaderStyle BackColor="Orange" ForeColor="Black" HorizontalAlign="Center" Font-Size="Small"/>
             <ItemStyle HorizontalAlign="Center" />
                <Columns >
                  
                     <telerik:GridBoundColumn DataField="stcode"  HeaderText="شماره دانشجویی" ItemStyle-HorizontalAlign="Center"  />
                     <telerik:GridBoundColumn DataField="fileDate"  HeaderText="جلسه" ItemStyle-HorizontalAlign="Center"  />
                     <telerik:GridBoundColumn DataField="Class_Code"  HeaderText="کد درس" ItemStyle-HorizontalAlign="Center"  />
                     <telerik:GridBoundColumn DataField="vaziat"  HeaderText="وضعیت پرداخت" ItemStyle-HorizontalAlign="Center"  />
                     <telerik:GridBoundColumn DataField="namedars"  HeaderText="نام درس" ItemStyle-HorizontalAlign="Center"  />
                     <telerik:GridBoundColumn DataField="saatklass"  HeaderText="کلاس" ItemStyle-HorizontalAlign="Center"  />
                    

                   
                            </Columns>
                </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
        </div>
        </asp:Panel>
</asp:Content>
