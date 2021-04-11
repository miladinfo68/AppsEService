using System;
using System.Globalization;
using System.Reflection;

namespace System
{
    public static class PersianDateExtensionMethods
    {
        private static CultureInfo _Culture;
        public static CultureInfo GetPersianCulture()
        {
            if (_Culture == null)
            {
                _Culture = new CultureInfo("fa-IR");
                DateTimeFormatInfo formatInfo = _Culture.DateTimeFormat;
                formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
                formatInfo.DayNames = new[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
                var monthNames = new[]
                {
                    "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                    "اسفند",
                    ""
                };
                formatInfo.AbbreviatedMonthNames =
                    formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
                formatInfo.AMDesignator = "ق.ظ";
                formatInfo.PMDesignator = "ب.ظ";
                formatInfo.ShortDatePattern = "yyyy/MM/dd";
                formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
                formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
                System.Globalization.Calendar cal = new PersianCalendar();

                FieldInfo fieldInfo = _Culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                    fieldInfo.SetValue(_Culture, cal);

                FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(formatInfo, cal);

                _Culture.NumberFormat.NumberDecimalSeparator = "/";
                _Culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
                _Culture.NumberFormat.NumberNegativePattern = 0;
            }
            return _Culture;
        }

        public static string ToPeString(this DateTime date, string format = "yyyy/MM/dd")
        {
            return date.ToString(format, GetPersianCulture());
        }

        public static bool isPersianDateCorrect(this string persianDateString)
        {
            if (!persianDateString.Contains("/"))
                return false;
            string[] dateArr = persianDateString.Split('/');
            if (dateArr.Length != 3)
                return false;
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\d+");
            for (int i = 0; i < 3; i++)
                if (!rgx.IsMatch(dateArr[i]))
                    return false;
            try
            {
                System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
                DateTime dte = pDate.ToDateTime(Convert.ToInt32(dateArr[0]), Convert.ToInt32(dateArr[1]), Convert.ToInt32(dateArr[1]), 0, 0, 0, 0);
                if (dte > DateTime.Now)
                    return false;
            }
            catch { return false; }
            return true;
        }

        public static bool isPersianYearCorrect(this string persianYearString)
        {
            return (persianYearString + "/1/1").isPersianDateCorrect();
        }

        public static DateTime ToMiladi(this string strPeDate, string format = "yyyy/MM/dd")
        {
            CultureInfo enCulture = new CultureInfo("en-US");
            int pYear = Convert.ToInt32(strPeDate.Substring(0, 4));
            int pMonth = Convert.ToInt32(strPeDate.Substring(5, 2));
            int pDay = Convert.ToInt32(strPeDate.Substring(8, 2));
            // DateTime shamsi =new DateTime();
            DateTime tempDate = DateTime.ParseExact(strPeDate, "yyyy/MM/dd", GetPersianCulture());
            return tempDate;
        }

        public static DateTime ToGregorian(this string persianDate, string format = "yyyy/MM/dd")
        {
            var dateList = persianDate.Split('/');
            if (dateList[0].Length <= 2)
            {
                dateList[0] = "13" + dateList[0];
            }
            return new DateTime(Convert.ToInt32(dateList[0]), Convert.ToInt32(dateList[1]), Convert.ToInt32(dateList[2]), new PersianCalendar());

        }
    }
    public static class Extensions
    {
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }
}