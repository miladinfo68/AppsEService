using MD.PersianDateTime;
using System;
using System.Collections.Generic;

namespace IAUEC_Apps.Business.Conatct.Functions
{
 public  static class DatePersian
    {
        public static string GetTimeNow12()
        {
            DateTime date = DateTime.Now;
            string Time = PersianDateTime.Now.ToShortTimeString();
            if (date.Hour >= 12)
            {
                Time = Time.Replace('ق', 'ب');
            }
            return Time;
        }
        public static string GetDateNow()
        {
           return PersianDateTime.Now.ToShortDateString();
        }
        public static string GetTimeNow24()
        {
            string hour = DateTime.Now.Hour.ToString().Length < 2 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            
            string minute= DateTime.Now.Minute.ToString().Length < 2 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            return hour + ":" + minute;
        }
        public static int CompareDate(string date, string startTime, string endTime)
        {
            if (date.Trim() == "" || startTime.Trim() == "" || endTime.Trim() == "")
                return -2;

            //add nim saate start and end 
            DateTime startTimeStandarddt = DateTime.Parse(startTime);
            DateTime endTimeStandarddt = DateTime.Parse(endTime);

            string startTimeStandard = startTimeStandarddt.Minute>=45?
                ((startTimeStandarddt.Hour)+":"+(startTimeStandarddt.Minute-45)).ToString():
                ((startTimeStandarddt.Hour-1 ) + ":" + (startTimeStandarddt.Minute + 45)).ToString();


            string endTimeStandard = endTimeStandarddt.Minute < 45 ?
               ((endTimeStandarddt.Hour) + ":" + (endTimeStandarddt.Minute + 45)).ToString() :
               ((endTimeStandarddt.Hour + 1) + ":" + (endTimeStandarddt.Minute - 45)).ToString();



            startTimeStandard = startTimeStandard.Length < 5 ? "0" + startTimeStandard : startTimeStandard;
            endTimeStandard = endTimeStandard.Length < 5 ? "0" + endTimeStandard : endTimeStandard;
            string getDateNow = PersianToEnglish(GetDateNow());
            string getTimeNow= GetTimeNow24();


            if (date == getDateNow && (string.Compare(getTimeNow, startTimeStandard) >= 0)
                &&(string.Compare(getTimeNow, endTimeStandard) <= 0))
                return 0;
            if (string.Compare(getDateNow, date) > 0||(string.Compare(getDateNow, date)==0
                && string.Compare(getTimeNow, endTimeStandard) >0))
            {
                return 1;
            }
            if (string.Compare(getDateNow, date) < 0 || (string.Compare(getDateNow, date) == 0
            && string.Compare(getTimeNow, startTimeStandard) < 0))
            {
                return -1;
            }

            return -2;
        }
        public static string PersianToEnglish(this string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };
            foreach (var item in persianStr)
            {
                if(item!='/')
                    persianStr = persianStr.Replace(item, LettersDictionary[item]);

            }
            return persianStr;
        }

    }
}
