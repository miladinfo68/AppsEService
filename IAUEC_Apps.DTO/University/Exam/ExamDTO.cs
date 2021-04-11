using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    class ExamDTO
    {
        /// <summary>
        /// این متغیر برای نگهداری رنج شروع بازه استفاده می گردد
        /// </summary>
        public int Startrangeid;
        /// <summary>
        /// این متغیر برای نگهداری رنج پایان بازه استفاده می گردد
        /// </summary>
        public int Endrangeid;
        /// <summary>
        /// این متغیر برای نگهداری آخرین بازه انتخاب شده استفاده می گردد
        /// </summary>
        public int MaxEndRange;
        /// <summary>
        /// این متغیر برای نگهداری ظرفیت پرشده استغاده می گردد
        /// </summary>
        public int Porshode;
        /// <summary>
        /// این متغیر برای تعیین ظرفیت باقیمانده استفاده می گردد
        /// </summary>
        public static int Start;
        /// <summary>
        ///این متغیر برای نگهداری نام شهر استفاده می گردد
        /// </summary>
        public static string city;
        /// <summary>
        /// این متغیر برای نگهداری کد درس استفاده می گردد
        /// </summary>
        public string coursecode;
        /// <summary>
        /// این متغیر برای نگهداری ظرفیت استفاده می گردد
        /// </summary>
        public static string zarfiat;
    }
}
