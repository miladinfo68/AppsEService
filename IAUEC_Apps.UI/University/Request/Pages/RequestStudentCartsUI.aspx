<%@ Page Title="" Language="C#" MasterPageFile="~/University/Request/MasterPages/PageRequestMaster.Master" AutoEventWireup="true"
    CodeBehind="RequestStudentCartsUI.aspx.cs" Inherits="IAUEC_Apps.UI.University.Request.Pages.RequestStudentCartsUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderplaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    درخواست ارسال کارت دانشجویی از طریق پست
     <script language="javascript" type="text/javascript">
         function postRefId(refIdValue) {
             var form = document.createElement("form");
             form.setAttribute("method", "POST");
             form.setAttribute("action", "<%= PgwSite %>");
             form.setAttribute("target", "_self");
             var hiddenField = document.createElement("input");
             hiddenField.setAttribute("name", "RefId");
             hiddenField.setAttribute("value", refIdValue);
             form.appendChild(hiddenField);
             document.body.appendChild(form);
             form.submit();
             document.body.removeChild(form);
         }
         function walletPaymentCallback() {
             window.location.href = window.location.href;
         }
     </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:Label ID="lblPardakht" Font-Bold="True" Font-Size="large" runat="server" Text="<span style='color:red'>توجه:</span>  دانشجوی گرامی برای دریافت کارت دانشجویی باید هزینه پست جهت ارسال پرداخت شود. با زدن دکمه پرداخت میتوانید هزینه پست را بپردازید" Visible="false"></asp:Label>
    <script>
        function CallBackConfirm2(arg) {
            if (arg)
                window.location.href = "../../../CommonUI/IntroPage.aspx"
        }
        function CallBackConfirm() {
            window.location.href = "../../../CommonUI/IntroPage.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="rwm_Validations" runat="server" Height="500px" Modal="True" Width="650px">
    </telerik:RadWindowManager>

    <div style="width: 100%">

        <telerik:RadGrid ID="grd_CartRequestState" CssClass="RadGrid_Silk" OnItemCommand="grd_CartRequestState_ItemCommand" OnItemDataBound="grd_CartRequestState_ItemDataBound" AutoGenerateColumns="false" runat="server" Visible="false" OnNeedDataSource="grd_CartRequeststate_NeedDataSource" ActiveItemStyle-HorizontalAlign="Center">
            <MasterTableView>
                <HeaderStyle BackColor="Orange" ForeColor="Black" HorizontalAlign="Right" Font-Size="Small" />
                <ItemStyle HorizontalAlign="Center" />
                <Columns>
                    <telerik:GridBoundColumn DataField="PaymentID" HeaderText="شناسه پرداخت" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true" />
                    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Right">
                        <HeaderTemplate>وضعیت درخواست</HeaderTemplate>
                        <HeaderStyle HorizontalAlign="Right" Font-Bold="true" />
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="hdnAppStatus" Value='<%#Eval("AppStatus")%>' />
                            <asp:Label ID="lbl_vaziat" Visible="false" Text='<%#Eval("vaziat")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="AppStatusText" HeaderText="وضعیت پرداخت" ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="true" />
                    <telerik:GridBoundColumn DataField="postnumber" HeaderText="کد مرسوله پستی" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" />
                    <telerik:GridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="عملیات" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:Button ID="btn_Pay" Visible="false" Text="پرداخت" runat="server" CommandArgument='<%#Eval("RequestId")+","+Eval("PaymentID") %>' CommandName="pay"></asp:Button>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                </Columns>
            </MasterTableView>

        </telerik:RadGrid>
    </div>
    <div style="width: 100%">


        <div style="background-color: rgba(255, 255, 255,0.5); padding: 1%; box-shadow: 10px 10px 10px 10px rgba(128, 128, 128,0.1); margin-top: 5px; height: 209px;">
            <asp:Panel ID="pnl_tazakor" runat="server" HorizontalAlign="Justify">
                <div style="margin-bottom: 15px">
                    <b>* 
                    <asp:Label ID="lbl_Tazakor" runat="server" Text=" لطفا قبل از ارسال این تقاضا، حتما نسبت به "></asp:Label></b>
                    <asp:HyperLink ID="hl_Edit" runat="server" ForeColor="Blue" NavigateUrl="~/University/Request/Pages/EditPersonalInformationUI.aspx">اصلاح و ویرایش آدرس و کد پستی </asp:HyperLink>
                    <b>
                        <asp:Label ID="Label1" runat="server" Text=" اقدام نمایید"></asp:Label>.<br />
                        <asp:Label ID="lbl_Tazakor1" runat="server" Text="<span style='color:#53a2b8'>تذکر1:</span> هزینه پست سفارشی برای ارسال کارت به عهده دانشجو بوده و مبلغ آن بر اساس تعرفه اداره پست از دانشجو اخذ خواهد گردید" Font-Bold="True" Font-Size="Small"></asp:Label>
                </div>
                <div style="margin-bottom: 15px">
                    <asp:Label ID="lbl_Tazakor2" Font-Bold="True" Font-Size="Small" runat="server" Text="<span style='color:#53a2b8'>تذکر 2:</span> کارت دانشجویی فقط یک بار برای دانشجو ارسال خواهد گردید،  و اگر به هر دلیلی کارت دانشجویی ارسالی برگشت بخورد فقط بصورت حضوری به دانشجو تحویل داده خواهد شد."></asp:Label>
                </div>
                <div style="margin-bottom: 15px">
                    <asp:Label ID="lbl_Tazakor3" Font-Bold="True" Font-Size="Small" runat="server" Text="<span style='color:#53a2b8'>تذکر3:</span> دانشگاه کارت دانشجویی را به آدرسی که خود دانشجو اعلام می نماید ارسال می کند، لذا هیچ گونه مسئولیتی در قبال عدم دریافت، مفقودی و یا هر گونه سواستفاده احتمالی از آن را ندارد"></asp:Label>
                </div>
                <div style="margin-bottom: 15px">
                    <asp:Label ID="Label2" Font-Bold="True" Font-Size="Small" runat="server" Text="<span style='color:red'>تذکر مهم:</span> در صورتیکه اعتبار کارت دانشجویی شما به اتمام رسیده است لطفا از این سامانه استفاده نفرمایید و به سایت اصلی دانشگاه http://www.iauec.ac.ir/  ،معاونت دانشجویی ،اداره کل خدمات رفاهی مراجعه و اطلاعیه های مربوطه را مطالعه فرمایید.
در صورت واریز وجه به هیچ عنوان عودت داده نمی شود.
"></asp:Label><br />
                </div>

                <asp:CheckBox ID="chk_Taeid" runat="server" Style="float: right" Text="مطالب بالا مورد تایید اینجانب می باشد" />
                <br />
            </asp:Panel>
        </div>
        <table style="width: 100%;" class="table-responsive">

            <tr>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>
                <td style="width: 5%">&nbsp;</td>

                <td>
                    <asp:Button ID="btn_pardakht" runat="server" Style="margin-left: 0px" Text="پرداخت" CssClass="btn btn-info" Width="105px" OnClick="btn_pardakht_Click" Visible="false" />
                </td>
            </tr>
        </table>


        <div style="background-color: rgba(255, 255, 255,0.5); padding: 1%; box-shadow: 10px 10px 10px 10px rgba(128, 128, 128,0.1); margin-top: 15px; margin-bottom: 15px">
            <table style="width: 100%; text-align: right;" dir="rtl" class="table-responsive">


                <tr>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>

                    <td style="width: 5%">&nbsp;</td>

                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>

                </tr>
                <tr>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_Name" runat="server" Text="نام:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_NamePrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_Family" runat="server" Text="نام خانوادگی:" Font-Bold="True"></asp:Label>
                    </td>

                    <td style="width: 5%">
                        <asp:Label ID="lbl_FamilyPrev" runat="server"></asp:Label>
                    </td>

                    <td style="width: 5%">
                        <asp:Label ID="lbl_ShomareDaneshju" runat="server" Text="شماره دانشجویی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_ShomareDaneshjuPrev" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>

                    <td style="width: 5%"></td>

                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>

                </tr>
                <tr>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_SalVorud" runat="server" Text="سال ورود:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_SalVorudPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Vorudi" runat="server" Text="ورودی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_VorudiPrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_Maghta" runat="server" Text="مقطع:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_MaghtaPrev" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%"></td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%"></td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_ReshteTahsili" runat="server" Text="رشته تحصیلی:" Font-Bold="True"></asp:Label>

                    </td>

                    <td style="width: 5%">
                        <asp:Label ID="lbl_ReshteTahsiliPrev" runat="server"></asp:Label>
                        <br />
                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Gerayesh" runat="server" Text="گرایش:" Font-Bold="True"></asp:Label>

                    </td>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_GerayeshPrev" runat="server"></asp:Label>

                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_Posti" runat="server" Text="کدپستی یا صندوق پستی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_CodepostiPrev" runat="server"></asp:Label>

                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;</td>

                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 5%">

                        <asp:Label ID="lbl_Phone" runat="server" Text="تلفن:" Font-Bold="True"></asp:Label>

                    </td>

                    <td style="width: 5%">
                        <asp:Label ID="lbl_PhonePrev" runat="server"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_Mobile" runat="server" Text="موبایل:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_MobilePrev" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;</td>

                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>

                </tr>
                <tr>
                    <td style="width: 5%">
                        <asp:Label ID="lbl_Address" runat="server" Text="آدرس کامل پستی:" Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:Label ID="lbl_AddressPrev" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>

                    <td style="width: 5%">&nbsp;</td>

                    <td style="width: 5%">&nbsp;</td>

                </tr>
                <tr>
                    <td style="width: 5%">&nbsp;</td>
                    <td style="width: 5%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <div style="text-align: center">
                <asp:Button ID="btn_Taeid" runat="server" Style="margin-left: 0px" Text="تایید" CssClass="btn btn-info" Width="105px" OnClick="btn_taeid_Click" />


            </div>
        </div>

    </div>
</asp:Content>

