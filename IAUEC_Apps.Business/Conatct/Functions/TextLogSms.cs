
using System;
using System.Runtime.InteropServices;

namespace IAUEC_Apps.Business.Conatct.Functions
{
    public static class TextLogSms
    {
        public static string GetTextLogSms(string stName, string stCode)
        {
            return " استاد گرامی شما دارای پیام جدید در اتاق گفتگو  دانشجو " + stName + " به شماره دانشجویی "
             + stCode + "  می‌باشید جهت مشاهده به سامانه خدمات بخش امورپژوهشی  مراجعه نمایید  "
             + "https://service.iauec.ac.ir/" + Environment.NewLine
             + "دانشگاه آزاد اسلامی-واحدالکترونیکی";

        }
        public static string GetTextSmsForAcceptedCommitmentDefencceOs(string stName, string dateDef,
                                                            string timeDef, string endDate, string endTime)
        {
            return " استاد گرامی زمان دفاع دانشجو " + stName + " در تاریخ " + dateDef + " ساعت " + timeDef + " تعیین گردیده است . "
                + " در صورت مخالفت با این زمان ضمن مراجعه به سامانه خدمات بخش امورپژوهشی پنل هماهنگی دفاع , مخالفت خود را تا ساعت "
                + endTime + " تاریخ " + endDate + " اعلام نمایید. ";

        }
        public static string GetEditTextSmsForAcceptedCommitmentDefencceOs(string stName, string dateDef,
                                                          string timeDef, string endDate, string endTime)
        {
            return " اصلاحیه:استاد گرامی " + "  زمان دفاع دانشجو " + stName + " در تاریخ " + dateDef + " ساعت " + timeDef + " تعیین گردیده است . "
                + " در صورت مخالفت بااین زمان ضمن مراجعه به سامانه خدمات بخش امورپژوهشی پنل هماهنگی دفاع , مخالفت خود را تا ساعت "
                + endTime + " تاریخ " + endDate + " اعلام نمایید. ";

        }
        public static string GetTextSmsForRejectDefenceSt(string osName,string typeOs) {
            typeOs = typeOs.Replace("اول", "").Replace("داخلی", "").Replace("خارجی", "");
            return " دانشجوی گرامی درخواست دفاع شما مورد تایید استاد " + typeOs
                + " جناب آقای /سرکارخانم ، " + osName + " قرار نگرفته است. لطفا برای ثبت درخواست دفاع مجدد به سامانه خدمات بخش امورپژوهشی پنل هماهنگی دفاع مراجعه نماييد. ";
        }
        public static string GetTextSmsStudentForRejectTechnichal()
        {
            return " دانشجوي محترم متقاضي دفاع از پايان نامه، " +
                " بدينوسيله به اطلاع مي رساند درخواست سركار/ جناب عالي بدليل عدم تاييد شرايط فني استاد متقاضي حضور آنلاين در جلسه دفاع تاييد نگرديد."
                + " لطفاً جهت كسب اطلاعات بيشتر به سامانه خدمات الكترونيكي، بخش هماهنگي جلسات دفاع مراجعه نماييد. "
                + Environment.NewLine
                + " واحد الکترونیکی دانشگاه آزاد اسلامی ";
        }
        public static string GetTextSmsOstadForRejectTechnichal(string stName="")
        {
            return "استاد محترم،"
               + " با عنايت به عدم تاييد الزامات فني حضور آنلاين یکی از ارکان دفاع در جلسه دفاع از پايان نامه دانشجو " + " , "
                + stName
                + " ، به استحضار مي رساند برگزاري جلسه مذكور در تاريخ درخواستي مورد تاييد واقع نگرديد  " + " . "
                + Environment.NewLine
                + " واحد الکترونیکی دانشگاه آزاد اسلامی " + Environment.NewLine
                + " امور فني دفاعيه ها 42863777 -021 ";
        }

        public static string GetTextSmsAcceptRequestByTechnical(bool IsStudent=false)
        {
            string text = "";
            if (IsStudent == true)
                text = " دانشجو محترم ";
            else
                text = " استاد محترم ";
            text += " با توجه به برگزاری جلسه دفاع به صورت آنلاین , در اسرع وقت با مراجعه به آدرس ";
            text += " http://iauec.ac.ir/technical ";
            text += " ضمن مطالعه فایل راهنما و نصب نرم‌افزارهای مورد نیاز ,آمادگی لازم جهت برگزاری جلسه دفاع آنلاین را کسب نمایید ";
            return text;


        }
        public static string  GetTextSmsAcceptRequestByEducation(string stCode,string stName, string typeOs,string dateDef,string TimeDef )
        {
            typeOs = typeOs.Replace("اول", "").Replace("داخلی", "").Replace("خارجی", "");

            return " استاد گرامی جلسه دفاع دانشجو "
                + stCode + " " + stName
                + " در تاریخ "
                + dateDef
                + " ساعت "
                + TimeDef
                + " شما به عنوان استاد "
                + typeOs
                + " ایشان انتخاب شده اید "
                + " ورود به جلسه دفاع آنلاین از سامانه خدمات الکترونیکی  " 
                + " service.iauec.ac.ir "
                + " بخش امور پژوهشی قسمت برگزاری دفاع‌های آنلاین امکانپذیر می‌باشد ";
        }
        public static string GetTextSmsAcceptRequestByEducation()
        {
            return "با توجه به برگزاری جلسه دفاع به صورت آنلاین , حضور تمامی ارکان دفاع شامل اساتید , راهنما , مشاور و داور (داوران) در جلسه دفاع الزامی می‌باشد";
        }

    }
}
