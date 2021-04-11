using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class ProfessorEditRequestDTO
    {
        public ProfessorEditRequestDTO()
        {

        }

        public int Id { get; set; }

        public int Code_Ostad { get; set; }

        public int RequestTypeID { get; set; }

        public int RequestLogID { get; set; }

        public string Createdate { get; set; }

        public string Note { get; set; }

        public string Term { get; set; }

        public string ProfessorMessage { get; set; }

        public string Erae_Be { get; set; }

        public bool isDeleted { get; set; }

        public string ScanImageUrl { get; set; }

        public Dictionary<int, ImageStructure> ScanList { get; set; }

        public int HR_InfoPeople_Id { get; set; }

        public IList<ChangedInfoDTO> ChangeList { get; set; }

        public ProfessorHokmDTO Hokm { get; set; }

        public int ChangeSet { get; set; }

        public string FullName { get; set; }
        public string hdn_fullName { get; set; }

    }

    #region Enums
    public enum RequestLogId
    {
        //5	درخواست شما رد شده است
        //6	در حال بررسي
        //7	تاييد درخواست
        //8	بررسي شده


        /// <summary>
        /// رد شده
        /// </summary>
        denied = 5,
        /// <summary>
        /// در حال بررسی
        /// </summary>
        submitted = 6,

        /// <summary>
        /// تایید شده
        /// </summary>
        approved = 7,

        /// <summary>
        /// بررسی شده
        /// </summary>
        reviewed = 8
    }

    public enum ChangeState
    {
        //وارد شده = 0 ،
        //1 = در حال بررسی ،
        //2 = تایید کارگزینی و انتقال به سیدا ،
        //3 = رد کارگزینی

        entered = 0,
        submitted = 1,
        approved = 2,
        denied = 3
    }

    public enum RequestTypeId
    {
        //17	درخواست ويرايش مشخصات فردي اساتيد
        //18	درخواست ويرايش نوع همکاري اساتيد
        //19	درخواست بروز رساني حکم جديد

        EditPersonalInfo = 17,
        EditContactInfo = 18,
        EditHokm = 19,
        EditCooperation = 20
    }

    public struct ImageStructure
    {
        public byte[] image;
        public string imageUrl;
    }

    #endregion
}
