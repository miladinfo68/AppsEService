<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contract_HeadOfDepartment.ascx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.UserControls.Contract_HeadOfDepartment" %>
<script type="text/javascript">
    $(function () {
        $('#<%= hdnDynamicHight.ClientID %>').val($('.dynamicContent').height());
    });
</script>
<link href="../css/Contract.css" rel="stylesheet" />
<input type="hidden" runat="server" id="hdnDynamicHight" value="-1" />
<asp:HiddenField ID="hdnContractFile" runat="server" />
<asp:Panel runat="server" ID="pnlContractForm">
    <div class="contractPages">
        <div class="pageHeader">
            <div class="hRight">
                <span id="professorCode" runat="server" class="professorCode"></span>
                <asp:Image runat="server" ID="imgLogo" src="/University/CooperationRequest/Image/logo.png" />
            </div>
            <div class="hLeft">
                <div>
                    <span class="headInfoTitle">شماره:</span>
                    <span class="headInfo"></span>
                </div>
                <div>
                    <span class="headInfoTitle">تاريخ:</span>
                    <span class="headInfo"></span>
                </div>
                <div>
                    <span class="headInfoTitle">پيوست:</span>
                    <span class="headInfo"></span>
                </div>
            </div>
            <div class="hLine"></div>
            <div class="clear"></div>
        </div>
        <div class="content-block">
            <div class="pageWrapper">
                <h6 class="topTitle">قرار داد مدیر گروه آموزشی</h6>
                <p>
                    <span>این قرارداد بین دانشگاه آزاد اسلامی واحد الکترونیکی که د راین قرارداد به اختصار دانشگاه نامیده می شود با نمایندگی آقای </span>
                    <asp:Label runat="server" ID="lblAgentName"></asp:Label>
                    <span>سرپرست دانشگاه، به نشانی </span>
                    <asp:Label runat="server" ID="lblUniversityAddress"></asp:Label>
                    <span>کد پستی </span>
                    <asp:Label runat="server" ID="lblUniversityPostalCode"></asp:Label>
                    <span>تلفن </span>
                    <asp:Label runat="server" ID="lblUniversityPhone"></asp:Label>
                    <span>شماره ثبت </span>
                    <asp:Label runat="server" ID="lblUniversityRegNo"></asp:Label>
                    <%--<span>کد اقتصادی </span>--%>
                    <asp:Label runat="server" ID="lblUniversityEcuNumber"></asp:Label>
                    <span>، از یک طرف و </span>
                    <asp:Label runat="server" ID="lblProfGender" CssClass="boldFont"></asp:Label>
                    <span>" </span>
                    <asp:Label runat="server" ID="lblFullName"></asp:Label>
                    <span>" با مشخصات زیر که در این قرارداد به اختصار </span>
                    <span style="font-weight: bold">مدیر گروه </span>
                    <span>نامیده می شود از طرف دیگر به شرح ذیل منقعد گردید و طرفین ملزم به اجرای مفاد آن می باشند.</span>
                </p>
                <p>
                    <span class="boldFont divBlock dynamicContent">ماده 1- مشخصات مدیرگروه:</span>
                    <span>نام: </span>
                    <asp:Label runat="server" ID="lblFirstName" CssClass="sSpace"></asp:Label>
                    <span>نام خانوادگي: </span>
                    <asp:Label runat="server" ID="lblLastName" CssClass="mSpace"></asp:Label>
                    <span>نام پدر: </span>
                    <asp:Label runat="server" ID="lblFatherName" CssClass="sSpace"></asp:Label>
                    <span>شماره شناسنامه: </span>
                    <asp:Label runat="server" ID="lblIdd" CssClass="sSpace"></asp:Label>
                    <span>تاريخ تولد: </span>
                    <asp:Label runat="server" ID="lblBirthDate" CssClass="sSpace"></asp:Label>
                    <span>کدملي: </span>
                    <asp:Label runat="server" ID="lblNationalCode" CssClass="sSpace"></asp:Label>
                    <span>آخرين مدرک تحصيلي: </span>
                    <asp:Label runat="server" ID="lblCertificateTitle" CssClass="sSpace"></asp:Label>
                    <span>رشته تحصيلي: </span>
                    <asp:Label runat="server" ID="lblFieldTitle" CssClass="sSpace"></asp:Label>
                    <span>از دانشگاه: </span>
                    <asp:Label runat="server" ID="lblUniversity" CssClass="sSpace"></asp:Label>
                    <span>نشاني محل سکونت: </span>
                    <asp:Label runat="server" ID="lblAddress" CssClass="lSpace"></asp:Label>
                    <span>تلفن همراه: </span>
                    <asp:Label runat="server" ID="lblMobile" CssClass="mSpace"></asp:Label>
                    <span>تلفن محل سکونت: </span>
                    <asp:Label runat="server" ID="lblPhoneNo" CssClass="mSpace"></asp:Label>
                    <span>وضعیت تاهل: </span>
                    <asp:Label ID="lblMaritalStatus" CssClass="mSpace" runat="server"></asp:Label>
                    <span>مرتبه علمی: </span>
                    <asp:Label runat="server" ID="lblMartabe" CssClass="mSpace"></asp:Label>
                    <span>پایه: </span>
                    <asp:Label runat="server" ID="lblPaye" CssClass="mSpace"></asp:Label>
                    <span>وضعیت شغلی: </span>
                    <asp:Label ID="lblJobStatus" CssClass="mSpace" runat="server"></asp:Label>
                    <span id="dvJobDetail" runat="server" visible="false">
                        <span>نحوه اشتغال: </span>
                        <asp:Label runat="server" ID="lblJobType" CssClass="mSpace"></asp:Label>
                        <span>محل اشتغال: </span>
                        <asp:Label runat="server" ID="lblJobPlace" CssClass="mSpace"></asp:Label>
                        <span>نوع اشتغال: </span>
                        <asp:Label runat="server" ID="lblEmployType" CssClass="mSpace"></asp:Label>

                    </span>
                    <span>آدرس پست الکترونيکي:</span>
                    <asp:Label runat="server" ID="lblEmail" CssClass="mSpace"></asp:Label>
                    <span>صاحب حساب شماره:</span>
                    <asp:Label runat="server" ID="lblBankAccount" CssClass="mSpace"></asp:Label>
                    <span class="boldFont">نزد بانک  ملي</span>

                </p>
                <p>
                    <span class="boldFont divBlock">ماده 2- موضوع قرارداد:</span>
                    <span>موضوع قرارداد عبارت است از انجام امور آموزشی و پژوهشی دانشجویان گروه </span>
                    <asp:Label runat="server" ID="lblGroups" CssClass="mSpace"></asp:Label>
                    <span>منطبق با بخشنامه ها و آئین نامه های صادره از دانشگاه آزاد اسلامی</span>

                </p>
                <p>
                    <span class="boldFont divBlock">ماده 3- مدت قرارداد:</span>
                    <span>مدت این قرارداد از تاریخ </span>
                    <asp:Label runat="server" ID="lblFromDate" CssClass="mSpace"></asp:Label>
                    <span>تا مورخ </span>
                    <asp:Label runat="server" ID="lblToDate" CssClass="mSpace"></asp:Label>
                    <span>خواهد بود</span>
                </p>
                <div class="lp1">
                    <p>
                        <span class="boldFont divBlock">ماده 4- مبلغ قرارداد:</span>
                        <span>4-1- مبلغ قرارداد ماهیانه</span>
                        <asp:Label runat="server" ID="lbkSalary" CssClass="mSpace"></asp:Label>
                        <span>ريال تعیین و توافق گردید</span>
                        <br />
                        <span>4-2- پرداخت به صورت ماهیانه و با توجه به ساعت حضور موظفی و پس از تایید معاونت آموزشی واحد انجام خواهد شد.</span>
                        <br />
                        <span>4-3- مبلغ قرارداد به صورت ناخالص بوده و کسورات قانونی مربوطه به شرح ذیل اعمال خواهد شد:</span>
                        <br />
                        <span>الف- بر اساس قانون مالیات های مستقیم، معادل 10% مبلغ قرارداد به عنوان مالیات توسط دانشگاه کسر و به حساب وزارت امور اقتصادی و دارایی واریز خواهد نمود.</span>
                        <br />
                        <span>ب- مطابق قوانین و مقررات سازمان تامین احتماعی، کلیه کسورات بیمه ای متعلق به این قرارداد از هر پرداخت کسر و در صورت نیاز در وجه سازمان تامین اجتماعی واریز می گردد.</span>
                        <br />
                        <span>تبصره1- مدیران گروه بازنشسته و یا آن دسته از مدیر گروه هایی که به هر عنوان بیمه آنها به صورت کامل از محل دیگری پرداخت می گردد از این کسورات معاف می باشند.</span>
                    </p>
                </div>
            </div>
            <div class="pageWrapper">

                <p>
                    <span class="boldFont divBlock">ماده 5- تعهدات مدیر گروه:</span>
                    <br />
                    <span>5-1- مدیر گروه متعهد می گردد خدمات موضوع این قرارداد را عینا مطابق ماده 2 بدون تاخیر و بر طبق برنامه داده شده زیر نظر رئیس دانشگاه و وفق بخشنامه شماره 10/556 سازمان مرکزی دانشگاه به انجام برساند.</span>
                    <br />
                    <span>5-2- در صورتی که در اثنای قرارداد واحد متوجه بیان غیر واقعی در اظهارات مدیرگروه شود، واحد می تواند قرارداد را یک طرفه فسخ و تمام زیان حاصله را از مدیر گروه وصول نماید.</span>
                    <br />
                    <span>5-3- جهت تعامل بیشتر با دانشجویان و دانشکده مربوطه، مدیر گروه موظف به حضور فیزیکی هر هفته به مدت 10 ساعت و 2 ساعت به صورت آنلاین می باشد.</span>
                    <br />
                    <span>تبصره 2- منظور از ساعت حضور، ساعت خارج از زمان برگزاری کلاس های تدریس توسط مدیر گروه می باشد.</span>
                    <br />
                    <span>5-4- در صورتی که ارائه خدمات موضوع قرارداد به تشخیص دانشگاه از جهت خصوصیات و کیفیت مطلوب و یا برابر مقررات و ضوابط آموزش و پژوهشی واحد نباشد، مدیر گروه حقی بر دریافت حق الزحمه مقرر نخواهد داشت.</span>
                    <br />
                    <span>5-5- در صورت عدم اجرای تعهدات توسط مدیر گروه، دانشگاه می تواند قرارداد را یک طرفه فسخ نماید و مدیر گروه حق هر نوع اعتراض و ادعایی را از خود سلب نمود.</span>
                    <br />

                    <span>5-6- مدیر گروه متعهد به رعایت شئونات شغلی، اخلاقی و دینی در طول مدت همکاری با دانشگاه می باشد.</span>
                    <br />
                    <span>5-7- مدی رگروه حق واگذتری موضوع و تعهدات و تکالیف مندرج در این قرارداد را به غیر، به طور کلی یا جزئی را نخواهد داشت. تخلف از این موضوع، قرارداد را خود به خود منفسخ خواهد کرد.</span>
                    <br />
                    <span>5-8- مدیر گروه نمی تواند تحت هیچ عنوان، حتی در زمان بروز اختلاف، تعهدات خویش را انجام ندهد یا تعلیق نماید. در غیر این سورت معادل دو برابر کل قرارداد به عنوان جریمه عدم انجام تعهد، کسر خواهد شد.</span>
                </p>
                <p>
                    <span class="boldFont divBlock">ماده 6- تعهدات واحد:</span>
                    <br />
                    <span>دانشگاه منعهد می گردد به پرداخت کامل حق الزحمه برابر مبلغ توافق شده به مدیر گروه پس از طی مراحل اداری و ایفای کامل تعهدات قراردادی می باشد.</span>
                </p>
                <p>
                    <span class="boldFont divBlock">ماده 7- نظارت:</span>
                    <br />
                    <span>نظارت مستقیم و مستمر بر حسن اجرای این قرارداد به عهده سرپرست معاونت آموزشی و تحصیلی تکمیلی و با معرفی سرپرست مدیریت امور هیئت علمی به عنوان نماینده جهت پیگیری خواهد بود.</span>
                </p>
                <p>
                    <span class="boldFont divBlock">ماده 8- فورس ماژور </span>
                    <br />
                    <span>8-1- منظور از"فورس ماژور" در قرارداد بروز حخوادث غیر مترقبه پیش بینی و غیر قابل احتراز است که خارج از کنترل طرف می باشد که قادر به انجام تعهدات خود نشده است، مانند زلزله، آتش سوزی و سایر عللی است که ماهیت مشابه دارند.</span>
                    <br />
                    <span>8-2- هرگاه به علت فورس ماژور، مدرس نتواند تعهدات خود را طبق قرارداد انجام دهند قرارداد منعقده به قوت خود باقی است و پس از رفع شرایط فوق، مدرس متعهد به ادامه انجام تعهدات می باشند. چنانچه پس از مدت یک ماه، شرایط قفورس مازور برطرف نگردید دو طرف نسبت به اماله یا ادامه قرارداد تصمیم گیری خواهند نمود.</span>

                </p>
            </div>
            <div class="pageWrapper">
                <div class="lp2"></div>
                <p>
                    <span class="boldFont divBlock">ماده 9- حل اختلاف:</span>
                    <br />
                    <span>در صورت بروز هر گونه احتلاف یا دعوی ناشی از تفسیر یا اجرای مفاد قرارداد، موضوع اختلاف در ابتدا به طریق مسالمت آمیز با توافق طرفین حل و فصل خواهد شد. در غیر این صورت موضوع اختلاف به حکمیت و داوری اداره کل امور حقوقی دانشگاه آزاد اسلامی ارجاع خواهد شد و رای  داور برای طرفین قاطع و لازم الجرا می باشد.</span>
                    <br />
                    <span>طرف دوم در این شرایط حق تعلیق موضوع قرارداد را نداشته و می بایست موضوع قرارداد را به اتمام برساند.</span>
                    <br />
                    <span>تبصره 3- عدم شرکت هر یک از طرفین یا اعضا در جلسه داوری مانع تشکیل جلسه و اتخاذ تصمیم نبوده و جلسه داوری تشکیل و تصمیم مقتضی را اتخاذ خواهد نمود.</span>
                </p>
                <div class="lp3">
                    <p>
                        <span class="boldFont divBlock">ماده 10- فسخ قرارداد:</span>
                        <br />
                        <span>این قرارداد از جانب مدیر گروه لازم و از جانب دانشگاه جایز خواهد بود، لذا دانشگاه هر زمان که بخواهد می تواند قرارداد را فسخ نماید.</span>
                    </p>
                </div>

                <div class="fp3"></div>

                <p>
                    <span class="boldFont divBlock">ماده 11- اصلاح توافق نامه:</span>
                    <br />
                    <span>هرگونه اصلاح این توافق نامه فقط با توافق کتبی طرفین موثر خواهد بود.</span>
                </p>
                <p>
                    <span class="boldFont divBlock">ماده 12- تفکیک پذیری:</span>
                    <br />
                    <span>چنانچه بخشی از این قرارداد بر اساس رای مرجع رسیدگی، غیر قابل احرا یا غیر قانونی شناخته شود، صرفا آن بخش از قرارداد بلااثر شده ، لیکن سایر مواد همچنان لازم الاجرا خواهد بود.</span>
                </p>
                <p>
                    <span class="boldFont divBlock">ماده 13- یکپارچگی قرارداد:</span>
                    <br />
                    <span>قرارداد و پیوست های آن یک مجموعه جامع و یکپارچه ای را تشکیل می دهند که بین طرفین مورد توافق و تائید قرار گرفته است. قرارداد مذکور جایگزین کلیه قراردادها، ترتیبات، مکاتبات و ارتباطات(چه شفاهی و کتبی( قبلی که بین طرفین در ارتباط با موضوع قرارداد وجود داشته است، می گردد.</span>
                </p>
                <p>
                    <span class="boldFont divBlock">ماده 14- تاییدات طرفین:</span>
                    <br />
                    <span>14-1- تمام اسناد و مدارک این قرارداد را مطالعه نموده و از مفاد آن کاملا آگاه شده اند.</span>
                    <br />
                    <span>14-2- از قوانین و مقررات مرتبط با اجرای موضوع قرارداد که در تاریخ امضاء قرارداد معمول و مجری بوده است، کاملا مطلع بوده و متعهد هستند که همه آنها را رعایت کنند. مسئولیت عدم رعایت قوانین یاد شده بر عهده طرف خاطی خواهد بود</span>
                    <br />
                    <span>14-3- در زمان امضاء قرارداد ، نسبت به کلیه شرایط، لوازم، امکانات و هزینه های اجرای قرارداد مطالعات کافی را انجام داده و کاملا آگاهی یافته اند و هیچ موردی باقی نمانده است که بعد از امضاء قرارداد در مورد آن استناد به جهل خود نمایند.</span>
                </p>
                
            </div>
            <div class="pageWrapper">
                <p>
                    <span class="boldFont divBlock">ماده 15- نشانی طرفین:</span>
                    <br />
                    <span>اقامتگاه قانونی طرفین همان است که در صدر این قرارداد ذکر شده و کلیه ابلاغیه ها، اخطاریه ها و مکاتباتی که توسط دانشگاه به اقامتگاه اعلامی یا آدرس پست الکترونیکی)ایمیل( اعلامی از سوی مدیر گروه ارسال می گردد در حکم ابلاغ قانونی محسوب می گردد.
                        ضمنا مدیر گروه موظف است هرگونه  تغییر اقامتگاه خود را فورا و کتبا حداکثر ظرف مدت 7/هفت روز به اطلاع دانشگاه برساند. در غیر این صورت کلی مکاتبات ارسالی به نشانی های مذکور در این قرارداد ابلاغ شده و قانونی می باشد. </span>

                </p>
                <p>
                    <span class="boldFont divBlock">ماده 16- نسخ قرارداد:</span>
                    <br />
                    <span>قرارداد حاضر در 16 ماده، 3 تبصره،3 صفحه و 4 نسخه که کلیه نسخ دارای حکم، ارزش و اعتبار واحد می باشند، تنظیم و به امضای طرفین رسید و تنها مندرجات تایپی اسناد قرارداد حاضر دارای اعتبار بوده و هرگونه خط خوردگی، لاک گرفتگی، دست نوشته(به غیر از امضا و نام امضا کنندگان( معتبر نخواهد بود. </span>
                </p>


                <div class="signatureWrapper">
                    <div class="signatureBox">
                        <div>
                            <asp:Label CssClass="Sign1" runat="server" ID="lblSignatureName1"></asp:Label>
                        </div>
                        <div>
                            <asp:Image CssClass="imgSign1" runat="server" ID="imgSignature1" Height="150" Width="150" />
                        </div>
                    </div>
                    <div class="signatureBox" id="sBox2">
                        <div>
                            <asp:Label CssClass="Sign2" runat="server" ID="lblSignatureName2"></asp:Label>
                        </div>
                        <div class="dvimgSign2">
                            <%--<asp:Image CssClass="imgSign2" runat="server" ID="imgSignature2" Height="200" Width="200" />--%>
                        </div>
                    </div>
                    <div class="signatureBox" id="sBox3">
                        <div>
                            <asp:Label CssClass="Sign3" runat="server" ID="lblSignatureName3"></asp:Label>
                        </div>
                        <div class="dvimgSign3">
                        </div>
                    </div>
                    <div class="signatureBox" id="sBox4">
                        <div>
                            <asp:Label CssClass="Sign4" runat="server" ID="lblSignatureName4"></asp:Label>
                        </div>
                        <div class="dvimgSign4">
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
