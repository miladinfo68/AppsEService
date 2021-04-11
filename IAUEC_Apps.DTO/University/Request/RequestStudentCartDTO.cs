using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class RequestStudentCartDTO
    {
        /// <summary>
        /// نام دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }
        /// <summary>
        /// نام خانوادگی دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The family.
        /// </value>
        public string family { get; set; }
        /// <summary>
        /// نام  رشته دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The nameresh.
        /// </value>
        public string nameresh { get; set; }
        /// <summary>
        /// نام گرایش رشته دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The namegeraesh.
        /// </value>
        public string namegeraesh { get; set; }
        /// <summary>
        /// مقطع تحصیلی دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The magh.
        /// </value>
        public int magh { get; set; }
        /// <summary>
        /// سال ورود دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The sal_vorod.
        /// </value>
        public string sal_vorod { get; set; }
        /// <summary>
        /// نیمسال ورود دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The nimsal_vorod.
        /// </value>
        public int nimsal_vorod { get; set; }
        /// <summary>
        /// آدرس دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The addressd.
        /// </value>
        public string addressd { get; set; }
        /// <summary>
        /// کدپستی دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The code_posti.
        /// </value>
        public string code_posti { get; set; }
        /// <summary>
        /// تلفن دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The tel.
        /// </value>
        public string tel { get; set; }
        /// <summary>
        /// شماره دانشجویی دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The stcode.
        /// </value>
        public string stcode { get; set; }
        /// <summary>
        /// نوع درخواست دانشجو در این متغیر ذخیره می گردد
        /// </summary>
        /// <value>
        /// The request type identifier.
        /// </value>
        public int RequestTypeID { get; set; }


    }
}
