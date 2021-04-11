<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Agreement.ascx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.UserControls.Agreement" %>

<script type="text/javascript">
    $(function () {
        $('#<%= hdnDynamicHight.ClientID %>').val($('.dynamicContent').height());
        //setTimeout(function () { alert() }, 100)
        //if ($('.dynamicContent').height() > 200) {
        //    $('.fp2').html($('.lp1').html());
        //    $('.lp1').html('');
        //    $('.fp3').html($('.lp2').html());
        //    $('.lp2').html('');
        //}
    });
</script>
<input type="hidden" runat="server" id="hdnDynamicHight" value="-1" />

<asp:HiddenField ID="hdnAgreementFile" runat="server" />
<asp:Panel ID="pnlAgreement" runat="server">
    <div class="contractPages">
        <div class="pageHeader">
            <div class="hLeft">
                <asp:Image runat="server" ID="imgLogo" src="/University/CooperationRequest/Image/logo.png" Width="120px" />
            </div>
            <div class="hLine"></div>
            <div class="clear"></div>
        </div>
        <br />
        <div class="content-block">
            <div class="pageWrapper">
                <h3 class="topTitle" style="text-align: center">بسمه تعالی</h3>
                <br />
                <h3 class="topTitle" style="text-align: center">تفاهم نامه پژوهشی با استادان راهنمای خارج از واحد دانشگاهي</h3>

            </div>
            <div class="dynamicContent">
                <p>
                    <span>برای روشن شدن چگونگی همکاری 

                    </span>
                    <asp:Label ID="lblProfGender" runat="server" Text="آقای/خانم"></asp:Label>
                    <asp:Label runat="server" ID="lblProfName" Text="......................"></asp:Label>
                    <span>به عنوان استاد راهنمای پایان نامه/رساله دانشجويان با دانشگاه آزاد اسلامی
                    </span>
                    <span style="font-weight: bold">واحد الكترونيكي
                    </span><span>در انتشار مقاله های مستخرج از پایان نامه/رساله و بهره مندی از سایر نتایج پژوهشی آن این تفاهم نامه بین استاد (استادان) راهنما و رياست واحد تنظیم می شود
                    </span>
                </p>
                <p style="text-align: right">
                    <span>1-دریافت بودجه از محل پایان نامه/ رساله
                    </span>
                    <br />
                    <span>&nbsp;&nbsp;&nbsp;&nbsp; - هیچ بودجه ای به جز حق الزحمه راهنمایی از دانشگاه آزاد اسلامی دریافت نخواهد شد</span>


                </p>
                <p style="text-align: right">
                    <span>2-	در فرایندهای پژوهشی پایان نامه/رساله و انتشار نتایج آن
                    </span>
                    <br />
                    <span>&nbsp;&nbsp;&nbsp;&nbsp;- نویسنده اول مقاله پژوهشی دانشجو با آدرس رسمی دانشگاه آزاد اسلامی است و نویسنده مسئول استاد و یا استادان راهنما هستند</span>

                    <br />
                    <span>&nbsp;&nbsp;&nbsp;&nbsp;- آدرس موسسه محل خدمت نویسنده مسئول (استاد راهنما) در مقاله درج خواهد شد</span>

                    <br />
                    <span>&nbsp;&nbsp;&nbsp;&nbsp;- آدرس دانشگاه آزاد اسلامی یا آدرس موسسه محل خدمت استاد راهنما هر دو به عنوان آدرس نویسنده مسئول در مقاله درج خواهد شد</span>

                    <br />
                </p>
                <p style="text-align: right">
                    <span>3-در سایر دستاوردهای پژوهشی پایان نامه/رساله مانند ثبت اختراع، تولید دانش فنی و غیره

                    </span>
                    <br />
                    <span>&nbsp;&nbsp;&nbsp;&nbsp;- معتقد به اجرای   </span>

                    <span style="font-weight: bold">بخشنامه آیین نامه مالکیت فکری دانشگاه آزاد اسلامی

                    </span>

                    <span>همراه با درج آدرس دانشگاه در آن هستم
                    </span>

                </p>
                <p style="text-align: right">
                    <span style="font-weight: bold">تذکر 1:</span>
                    <span>پس از ارسال این فرم همراه با پیشنهاد موضوع پایان نامه/رساله، واحد دانشگاهی مربوط در شورای آموزشی – پژوهشی خود حسب مورد برای پذیرش و یا رد آن اقدام می کند.
                    </span>
                    <br />

                    <span style="font-weight: bold">تذکر 2:</span>
                    <span>در صورتی که همه هزینه های انجام پایان نامه/رساله از سوی دانشگاه آزاد اسلامی در قالب بودجه پایان نامه/رساله یا طرح پژوهشی و یا هر دو آن ها تامین شود، چگونگی انتشار محصولات پژوهشی آن با رعایت حق تالیف و مالکیت فکری دانشجو، استاد (استادان) راهنما و دانشگاه محل خدمت ایشان در قالب مقررات داخلی دانشگاه آزاد اسلامی صورت می پذیرد.
                    </span>

                </p>
                <div class="signatureWrapper">
                    <div class="signatureBox">
                        <div>
                            <asp:Label CssClass="Sign1" runat="server" ID="lblSignatureName1"></asp:Label></div>

                        <div>
                            <asp:Image CssClass="imgSign1" runat="server" ID="imgSignature1" Height="150" Width="150" /></div>
                    </div>
                    <div class="signatureBox" id="sBox2">
                        <div>
                            <asp:Label CssClass="Sign2" runat="server" ID="lblSignatureName2"></asp:Label>
                        </div>
                        <div class="dvimgSign2">
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="pageFooter">
            <div>
                <asp:Label runat="server" ID="lblAddressFooter"></asp:Label>

            </div>
            <div>
                <asp:Label runat="server" ID="lblEmailFooter"></asp:Label>
            </div>
        </div>
    </div>

</asp:Panel>

<asp:Panel ID="pnlFromDB" runat="server" Visible="false" CssClass="contractPages">
</asp:Panel>
