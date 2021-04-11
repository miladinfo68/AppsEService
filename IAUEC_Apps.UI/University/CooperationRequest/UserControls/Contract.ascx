<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contract.ascx.cs" Inherits="IAUEC_Apps.UI.University.CooperationRequest.UserControls.Contract" %>
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
                <h6 class="topTitle">قرار داد آموزشي</h6>
                <p>
                    <span>اين قرارداد بين دانشگاه آزاد اسلامي واحد الکترونيکي که در اين قرارداد به اختصار دانشگاه ناميده مي شود با نمايندگي آقاي </span>
                    <asp:Label runat="server" ID="lblAgentName"></asp:Label>
                    <span>سرپرست دانشگاه، به نشاني: </span>
                    <asp:Label runat="server" ID="lblUniversityAddress"></asp:Label>
                    <span>- تلفن </span>
                    <asp:Label runat="server" ID="lblUniversityPhone"></asp:Label>
                    <span>شماره ثبت </span>
                    <asp:Label runat="server" ID="lblUniversityRegNo"></asp:Label>
                    <span>کد پستي </span>
                    <asp:Label runat="server" ID="lblUniversityPostalCode"></asp:Label>
                    <span>، از يک طرف و </span>
                    <asp:Label runat="server" ID="lblProfGender" CssClass="boldFont"></asp:Label>
                    <asp:Label runat="server" ID="lblFullName"></asp:Label>
                    <span>با مشخصات زير که در اين قرارداد به اختصار مدرس ناميده مي شود از طرف ديگر به شرح ذيل منعقد گرديد و طرفين ملزم به اجراي مفاد آن مي باشند.
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">مشخصات مدرس:</span>
                    <span class="divBlock dynamicContent">
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
                        <span>وضعيت شغلي: </span>
                        <asp:Label runat="server" ID="lblJobStatus" CssClass="sSpace"></asp:Label>
                        <%--<span>(در صورت اشتغال نحوه اشتغال و محل اشتغال و نوع اشتغال مشخص شود)</span>--%>
                        <br />
                        <span>آدرس پست الکترونيکي:</span><asp:Label runat="server" ID="lblEmail" CssClass="mSpace"></asp:Label>
                        <span>صاحب حساب شماره:</span><asp:Label runat="server" ID="lblBankAccount" CssClass="mSpace"></asp:Label>
                        <span class="boldFont">نزد بانک  ملي</span>
                        <br />
                        <span>سابقه تدريس (با ارائه مستندات مربوطه) :</span><asp:Label runat="server" ID="lblTeachYears" CssClass="sSpace"></asp:Label>
                        <br />
                        <span>(در صورت تغيير نشاني مدرس مکلف است ظرف مدت 15 روز کتباً از نشاني جديد دانشگاه را مطلع نمايد در غير اين صورت کليه مکاتبات به نشاني فوق ابلاغ شده تلقي مي گردد)</span>

                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 1– موضوع قرارداد:</span>
                    <span class="divBlock">
                        <span>موضوع قرارداد عبارت است از آموزش و تدريس واحد هاي درسي مطابق ابلاغيه هاي هر نيمسال تحصيلي در واحد دانشگاهي الکترونيکي طبق مقررات دانشگاه آزاد اسلامي و مصوبات واحد الکترونيکي.
                        </span>
                        <br />
                        <span>تبصره: ابلاغيه هر نيمسال تحصيلي، جزء مفاد قرارداد مي باشد.
                        </span>
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 2– مدت قرارداد:</span>
                    <span class="divBlock">
                        <span>مدت قرارداد از تاريخ  </span>
                        <asp:Label runat="server" ID="lblFromDate" CssClass="sSpace"></asp:Label>
                        <span>لغايت  </span>
                        <asp:Label runat="server" ID="lblToDate" CssClass="sSpace"></asp:Label>
                        <span>به مدت  يک نيمسال تحصيلي مي باشد.</span>
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 3–  مبلغ قرارداد:</span>
                    <span class="divBlock">
                        <span>3-1  مبلغ حق التدريس براي هر ساعت تدريس در هر نيمسال تحصيلي بر اساس مبلغ مصوب بخشنامه سازمان مرکزي و مصوبات واحد الکترونيکي براي آن نيمسال تحصيلي  تعيين و توافق گرديد.
                        </span>
                        <br />
                        <span>تبصره 1: پايه و مرتبه علمي مدرس، بر اساس آخرين مدارک ارائه شده به کارگزيني هيات علمي لحاظ مي گردد. 
                        </span>
                        <br />
                        <span>تبصره 2: حق الزحمه دروس پروژه و کارآموزي، مطابق بخشنامه سازمان مرکزي محاسبه و پرداخت مي گردد. 
                        </span>
                        <br />
                        <span>3-2 مبلغ قرارداد بصورت ناخالص بوده و پس از کسر کليه کسورات قانوني به مدرس پرداخت خواهد شد.
                        </span>
                    </span>
                </p>
                <div class="lp1">
                    <p>
                        <span class="divBlock boldFont">ماده 4– تعهدات دانشگاه:</span>
                        <span class="divBlock">
                            <span>4-1  دانشگاه متعهد به پرداخت حق التدريس برابر مبلغ توافق شده به مدرس پس از طي مراحل اداري و ايفاي کامل تعهدات  قراردادي مي باشد. 
                            </span>
                            <br />
                            <span>4-2 دانشگاه ملزم به ارائه سر فصلهاي درسي، مقررات آموزشي و اداري به مدرس به منظور آگاهي و آمادگي لازم براي انجام وظايف قراردادي مي باشد. 
                            </span>
                        </span>
                    </p>
                </div>
            </div>
            <div class="pageWrapper">
                <div class="fp2"></div>
                <p>
                    <span class="divBlock boldFont">ماده 5– تعهدات مدرس:</span>
                    <span class="divBlock">
                        <span>5-1 مدرس متعهد به رعايت سر فصل دروس مطابق آيين نامه و دستور العمل هاي آموزشي دانشگاه مي باشد.
                        </span>
                        <br />
                        <span>5-2   مدرس تعهد مي نمايد حضور به موقع در کلاس درس داشته باشد و در صورت عدم تشکيل کلاس به هر علت اعم از تعطيل رسمي، بيماري و ...، در اولين فرصت ممکن با هماهنگي مدير گروه مربوطه، کلاس جبراني آن درس را تشکيل دهد.
                        </span>
                        <br />
                        <span>5-3   مدرس ملزم به حضور در جلسات برگزاري امتحانات مي باشد و عدم حضور غيبت تلقي مي شود.
                        </span>
                        <br />
                        <span>5-4 مدرس مکلف است پس از برگزاري امتحان پايان ترم، نسبت به تصحيح و تحويل به موقع اوراق امتحاني و تسليم ليست کامل نمرات امتحان، حداکثر ظرف مدت هفت روز اقدام نمايد. در غير صورت به ازاء هر روز تاخير  5 درصد از کل مبلغ حق التدريس مربوط به آن درس بعنوان جريمه تاخير کسر خواهد شد.
                        </span>
                        <br />
                        <span>5-5 مدرس نميتواند موضوع قرارداد را جزئاً يا کلاً به غير واگذار نمايد و شخصاً ملزم به اجراي تعهدات قراردادي مي باشد.
                        </span>
                        <br />
                        <span>5-6  مدرس نميتواند تحت هيچ عنوان تعهدات خويش را انجام ندهد يا تعليق نمايد حتي در زمان بروز اختلاف، در غير اين صورت معادل دو برابر مبلغ کل قرارداد، بعنوان جريمه عدم انجام تعهد، کسر خواهد شد.
                        </span>
                        <br />
                        <span>5-7  مدرس متعهد به رعايت شئونات شغلي ، اخلاقي و ديني در طول مدت همکاري با دانشگاه مي باشد.
                        </span>
                        <br />
                        <span>5-8 در صورتي که مدرس حداکثر تا پنج جلسه به علل غير موجه کلاس را تشکيل نداده و يا درکلاس حضور نيابد موظف است با هماهنگي قبلي با دانشگاه جلسات تشکيل نشده را جبران نمايد، که در اين صورت براي اين گونه جلسات جبراني فقط 50% حق التدريس مقرر پرداخت خواهد شد و در صورت عدم تشکيل جلسات جبراني نه تنها حق التدريس ساعات تشکيل نشده پرداخت نمي گردد بلکه به ميزان دو برابر مبلغ حق التدريس ساعات موصوف از کل مبلغ حق التدريس نيز کسر مي گردد.
                        </span>
                        <br />
                        <span>تبصره : چنانچه عدم تشکيل کلاس يا عدم حضور مدرس به تشخيص دانشگاه با عذر موجه باشد 100% حق التدريس مقرر در صورت تجديد جلسه پرداخت خواهد شد و در صورت عدم تشکيل جلسات جبراني علاوه بر عدم پرداخت مبلغ حق التدريس ساعات تشکيل نشده به ميزان  50% مبلغ حق التدريس ساعات موصوف از مبلغ حق التدريس کسر مي گردد.
                        </span>
                        <br />
                        <span>5-9 حضور مدرس در جلساتي که عنداللزوم از طرف دانشگاه در ارتباط با دروس موضوع اين قرارداد تشکيل مي شود اجباري و جزء وظايف وي محسوب مي شود.
                        </span>
                        <br />
                        <span>5-10 مدرس تعهد نمود که مجموع ساعات تدريس وي در اين دانشگاه و ساير دانشگاهها و موسسات آموزش عالي از حداکثر ساعات مجاز مندرج در آيين نامه حق التدريس دانشگاهها تجاوز ننمايد مگر در صورتيکه نسبت به اخذ مجوز از هيات رييسه دانشگاه اقدام نمايد.
                        </span>
                        <br />
                        <span>5-11 اين قرارداد در هيچ مورد مبناي استخدام مدرس به صورت قطعي يا آزمايشي نبوده و از موجبات تبديل وضعيت ايشان بصورت عضو هيات علمي رسمي قطعي يا آزمايشي دانشگاه تلقي نمي گردد. مدرس در طول مدت قرار داد تنها مدرس حق التدريس دانشگاه محسوب مي شود.
                        </span>
                        <br />
                        <span>5-12 در صورت اشتغال به کار مدرس ارايه آخرين  حکم کارگزيني الزامي مي باشد.
                        </span>
                        <br />
                        <span>5-13 دانشگاه مجاز به هرگونه استفاده و بهره برداري از توليدات نرم افزاري حاصل از برگزاري کلاس ها اعم از فايل هاي صوتي و نوشتاري مي باشد و مدرس هيچگونه ادعايي در اين خصوص نخواهد داشت.
                        </span>
                        <br />
                        <span>5-14 مجموعه قوانين و مقررات معاونت آموزشي و تحصيلات تکميلي دانشگاه حاکم بر قرارداد مي باشد.
                        </span>
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 6– ناظر قرارداد:</span>
                    <span>
                        <span>معاون آموزشي دانشگاه ناظر قرارداد حاضرخواهد بود بنحوي که پس از تاييدکارکرد اساتيد توسط ناظر، معاونت اداري و مالي نسبت به پرداخت مبلغ حق التدريس اساتيد اقدام خواهد نمود.
                        </span>
                    </span>
                </p>
                <div class="lp2">
                    <p>
                        <span class="divBlock boldFont">ماده 7– فسخ:</span>
                        <span>
                            <span>دانشگاه مي تواند در صورت صلاحديد و با اطلاع کتبي قبلي يک ماهه، اين قرارداد را فسخ نمايد. ليکن مدرس تحت هيچ عنوان حق فسخ قرارداد را نخواهد داشت. همچنين مدرس حق هرگونه اعتراضي را نسبت به فسخ قرارداد از خود ساقط مي نمايد.
                            </span>
                        </span>
                    </p>
                </div>
            </div>
            <div class="pageWrapper">
                <div class="fp3"></div>
                <p>
                    <span class=" divBlock boldFont">ماده 8– فورس ماژور:</span>
                    <span>
                        <span>8-1 منظور از «فورس ماژور» در قرارداد بروز حوادث غير قابل پيشبيني و غير قابل احتراز است كه خارج از كنترل طرفي مي باشد كه قادر به انجام تعهدات خود نشده است، مانند زلزله، آتش‌سوزي و ساير عللي است كه ماهيت مشابه دارند.
                        </span>
                        <br />
                        <span>8-2 هرگاه بعلت فورس ماژور، مدرس نتواند تعهدات خود را طبق قرارداد انجام دهد قرارداد منعقده کماکان بقوت خود باقي است و پس از رفع شرايط فوق، مدرس متعهد به ادامه انجام تعهدات مي باشد. چنانچه پس از مدت يک ماه، شرايط فورس ماژور برطرف نگرديد دو طرف نسبت به اقاله يا ادامه قرارداد تصميم گيري خواهند نمود.
                        </span>
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 9– حل اختلاف:</span>
                    <span>
                        <span>در صورت بروز هر گونه اختلاف ناشي از تعبير و تفسير مفاد اين قرارداد، موضوع از طريق مذاکره و توافق حل و فصل خواهدشد و در صورت عدم حصول توافق، اداره کل امور حقوقي سازمان مرکزي دانشگاه آزاد به عنوان داور خواهد بود که راي صادره براي طرفين قاطع و لازم الاجرا خواهد بود.
                        </span>
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 10– نشاني طرفين:</span>
                    <span>
                        <span>اقامتگاه قانوني طرفين همان است که در صدر اين قرارداد ذکر شده و کليه ابلاغيه ها، اخطاريه ها و مکاتباتي که توسط دانشگاه به اقامتگاه اعلامي يا آدرس پست الکترونيکي (ايميل) اعلامي از سوي مدرس ارسال گردد درحکم ابلاغ قانوني محسوب مي گردد. ضمناً مدرس موظف است هرگونه تغيير اقامتگاه خود را فوراً و کتباً به اطلاع دانشگاه برساند.
                        </span>
                    </span>
                </p>
                <p>
                    <span class="divBlock boldFont">ماده 11– نسخ قرارداد:</span>
                    <span>
                        <span>قرارداد حاضر در11 ماده، 4 تبصره و 4 نسخه که هرکدام داراي حکم واحد مي باشند، تنظيم و به امضاي طرفين رسيد و تنها مندرجات تايپي اسناد قرارداد حاضر داراي اعتبار بوده و هرگونه خط خوردگي، لاك گرفتگي، دست نوشته (به غير از امضا و نام امضا کنندگان) معتبر نخواهد بود.
                        </span>
                    </span>
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
                            <%--<asp:Image CssClass="imgSign3" runat="server" ID="imgSignature3" Height="200" Width="200" />--%>
                        </div>
                    </div>
                    <div class="signatureBox" id="sBox4">
                        <div>
                            <asp:Label CssClass="Sign4" runat="server" ID="lblSignatureName4"></asp:Label>
                        </div>
                        <div class="dvimgSign4">
                            <%--<asp:Image CssClass="imgSign4" runat="server" ID="imgSignature4" Height="200" Width="200" />--%>
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
