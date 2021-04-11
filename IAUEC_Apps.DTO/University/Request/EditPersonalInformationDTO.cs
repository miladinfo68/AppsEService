using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class EditPersonalInformationDTO
    {
        /// <summary>
        /// این متغیر برای تشخیص اینکه آدرس دانشجو ویرایش شده یا خیر به کار می رود 
        /// </summary>
        public  Nullable<bool>  AddressChanged;
        /// <summary>
        /// این متغیر برای تشخیص اینکه پیش شماره تلفن دانشجو ویرایش شده یا خیر به کار می رود
        /// </summary>
        public Nullable<bool> PishShomareChanged;
        /// <summary>
        /// این متغیر برای تشخیص اینکه  شماره تلفن دانشجو ویرایش شده یا خیر به کار می رود
        /// </summary>
        public Nullable<bool> TelChanged;
        /// <summary>
        /// این متغیر برای تشخیص اینکه کد پستی دانشجو ویرایش شده یا خیر به کار می رود
        /// </summary>
        public Nullable<bool> PostCodeChanged;
        /// <summary>
        /// این متغیر برای تشخیص اینکه فایل عکس پرسنلی توسط دانشجو آپلود شده یا خیر به کار می رود
        /// </summary>
        public bool RadUploaderHasFile;
        /// <summary>
        /// این متغیر برای نگهداری آدرس جدید دانشجو به کار می رود
        /// </summary>
        public string NewAddress;
        /// <summary>
        /// این متغیر برای نگهداری شماره تلفن جدید دانشجو به کار می رود
        /// </summary>
        public string NewTel;
        /// <summary>
        /// این متغیر برای نگهداری کدپستی جدید دانشجو به کار می رود
        /// </summary>
        public string NewCodePosti;
        /// <summary>
        /// این متغیر برای نگهداری پیش شماره جدید دانشجو به کار می رود
        /// </summary>
        public string NewPishShomare;
        /// <summary>
        /// این آرایه برای نگهداری عکس دانشجو
        /// </summary>
        public byte[] bytes;
        /// <summary>
        /// این متغیر برای نگهداری نوع درخواست ویرایش به کار می رود
        /// </summary>
        public int EditType;
        /// <summary>
        /// این متغیر برای نگهداری وضعیت عکس پرسنلی به کار می رود
        /// </summary>
        public bool Picstatus;
        /// <summary>
        /// این متغیر برای نگهداری وضعیت موارد درخواست شده به کار می رود
        /// </summary>
        public bool NewInfStatus;
        /// <summary>
        /// این متغیر برای تشخیص اینکه شماره موبایل توسط دانشجو آپلود شده یا خیر به کار می رود
        /// </summary>
        public Nullable<bool> mobilechanged;
        public bool picchanged;
        public Nullable<bool> ShahrChanged;
        public Nullable<bool> OstanChanged;
        public string mobilerad;
        public string telrad;
        public string shahrrad;
        public string ostanrad;
        public string addressrad;
        public string codepostirad;


    }
}
