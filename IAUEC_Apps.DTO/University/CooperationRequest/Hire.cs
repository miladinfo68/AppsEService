using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hire
{
    public class Hire
    {
        public enum UserStatusEnum
        {
            registered = 0,
            infoSubmitted = 1,
            uploadedDocs = 2,
            //teachPermissionSubmit = 3,
            Approved = 4,
            Moved = 5,
            Denied = 6
        }

        public enum HeiatElmiStatus
        {
            registered = 0,
            infoSubmitted = 1,
            uploadedDocs = 2,

            Approved = 4,
            Moved = 5,
            Denied = 6
        }

        public enum DocType
        {
            صفحه_اول_شناسنامه = 1,
            صفحه_دوم_شناسنامه = 2,
            صفحه_سوم_شناسنامه = 3,
            آخرین_مدرک_تحصیلی = 4,
            اسکن_کارت_ملی = 5,
            اسکن_بیمه = 6,
            اسکن_کارت_پایان_خدمت = 7,
            رزومه = 8,
            آخرین_حکم_کارگزینی = 9,
            عکس_پرسنلی = 10,
            گواهی_امتحان_جامع = 11,
            پشت_کارت_ملی = 12,
            دانشنامه_دکتری = 13,
            ارزشنامه_تحصیلی_وزارت_علوم = 14,
            پروپزال = 15,
            کپی_صورتجلسه_دفاع = 16,
            گواهی_انشایی = 17,
            حکم_بازنشستگی=18
            //shFirstPage = 1,
            //shSecondPage = 2,
            //shThirdPage = 3,
            //lastMadrak = 4,
            //meliCard = 5,
            //bime = 6,
            //payanKhedmat = 7,
            //resume = 8,
            //lastHokm = 9,
            //personalPhoto = 10,
            //govahiJame = 11,
            //meliCardBehind = 12,
            //Daneshname = 13,
            //VezaratGovahi = 14,
            //Proposal = 15,
            //SuratJalaseDefa=16,
            //GovahiEnshayi=17
            //1	اسکن صفحه اول شناسنامه
            //2	اسکن صفحه دوم شناسنامه
            //3	اسکن صفحه سوم شناسنامه
            //4	اسکن آخرین مدرک تحصیلی
            //5	اسکن کارت ملی
            //6	اسکن بیمه
            //7	کارت پایان خدمت
            //8	اسکن رزومه
            //9	اسکن آخرین حکم کارگزینی
            //10	اسکن عکس
            //NULL	NULL
        }

        public enum DocStatusEnum
        {
            uploaded = 0,
            approved = 1,
            needEdit = 2
        }

        public enum UserType
        {
            NotHeiatElmi = 0,
            HeiatElmi = 1
        }

        public enum UserInfoStatus
        {
            submitted = 0,
            approved = 1,
            denied = 2,
            needEdit = 3,
            daneshkadeOK = 4,
            pajooheshOK = 5,
            pajooheshDenied = 6,
            needEditPajoohesh = 7

            //0 = در حال بررسی,
            //1 = تایید ,
            //2 = رد ,
            //3 = ویرایش,
            //4 =  تایید دانشکده ,
            //5 = تایید پژوهش,
            //6 = رد پژوهش,
            //7 = نقص پژوهش
        }

        public enum militaryStatus
        {
            معافيت_عنايت_رهبري = 1,
            خريد_خدمت = 2,
            داراي_دفترچه_آماده = 3,
            درحين_خدمت = 4,
            طلاب_حوزه_علميه = 5,
            غير_مشمول = 6,
            مشمول = 7,
            معافيت_پزشكي = 8,
            معافيت_تكفل = 9,
            تعهد_خدمت = 10,
            متولد_1337_و_قبل = 11,
            برگ_اعزام = 13,
            طبق_مجوز = 14,
            شاغل_رسمي_نيروهاي_مسلح = 15,
            معافيت_موقت = 16,
            معافيت_دائم = 17,
            پايان_خدمت = 18,
            معافيت_خاص = 19,
            بازنشسته_نيروهاي_مسلح = 20,
            سایر = 0

        }
        public enum MadrakType
        {
            ارشد_پیوسته=9,
            ارشد_ناپیوسته=10,
            دانشجوی_دکتری_بعد_امتحان_جامع=13,
            دانشجوی_دکتری_قبل_امتحان_جامع = 12,
            دکتری_تخصصی=2


        }

        public enum Cooperation
        {
            فقط_آموزشی=1,
            فقط_پژوهشی=2,
            هم_آموزشی_هم_پژوهشی=3
        }
    }
}