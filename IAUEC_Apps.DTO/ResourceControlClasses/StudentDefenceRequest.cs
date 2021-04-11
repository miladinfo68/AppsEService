using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
    /// <summary>
    /// this DTO use to add defence request for resource control
    /// </summary>
    public class StudentDefenceRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public int IssuerId { get; set; }
        public int Capacity { get; set; }
        public string DefenceSubject { get; set; }
        public int DaneshId { get; set; }
        public int CourseId { get; set; }
        public string RequestDate { get; set; }
        public long RequestStartTime { get; set; }
        public long RequestEndTime { get; set; }
        public string IssuerName { get; set; }
        public string UserId { get; set; }
        public string CourseName { get; set; }
        public bool IsEquippingResource { get; set; }

        public bool UseOwnPc { get; set; }

        public string OnlineTeacherRole { get; set; }
        public string OnlineFirstTeacherName { get; set; }
        public string OnlineSecondTeacherName { get; set; }
        public string OnlineFirstTeacherId { get; set; }
        public string OnlineSecondTeacherId { get; set; }

        public string Gender { get; set; }
        public string AcceptPropDate { get; set; }
        public bool IsEdited { get; set; }
    
        //sadeghsaryazdi
        public bool FlagDoingMeetingOnline { get; set; }
        public bool IsRequestEducation { get; set; }
        public static StudentDefenceRequest StaticStudentRequest()
        {
            return new StudentDefenceRequest
            {
                Subject = "StudentDefence",
                Capacity = 10
            };
        }
    }

    public enum Category
    {
        OnlineClass = 1,
        InPersonClass = 2,
        ConferenceHall = 3

    }
    public enum Location
    {
        Molasadra = 1,
        Raam = 2
    }

    public static class College
    {
        public static string GetCollegeName(int collegeName)
        {
            switch (collegeName)
            {
                case 1:
                    return "علـوم انـساني";
                case 2:
                    return "فني و مهندسي";
                case 3:
                    return "مديريت";

                case 5:
                    return "دوره هاي کوتاه مدت";
                case 6:
                    return "علوم تحقيقات";
                case 7:
                    return "تهران مرکزي";
                case 8:
                    return "علوم پايه و فناوري هاي نوين";
                default:
                    return "";
            }
        }

    }
    public class StudentInformation
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int DaneshId { get; set; }
        public int CourseId { get; set; }
    }

    public class logDetail
    {
        public string Name { get; set; }
        public string LogDate { get; set; }
        public string LogTime { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
    }





    public static class StudentRequestStatus
    {
        /// <summary>
        /// مقدار 0
        /// </summary>
        public static int RegisterStudentRequest { get { return 0; } }
        /// <summary>
        /// مقدار 1
        /// </summary>
        public static int CollegeAcceptRequest { get { return 1; } }
        /// <summary>
        /// مقدار 2
        /// </summary>
        public static int OfficeAcceptRequest { get { return 2; } }
        /// <summary>
        /// مقدار 3
        /// </summary>
        public static int RejectRequest { get { return 3; } }
        /// <summary>
        /// مقدار 4
        /// </summary>
        public static int wasteRequest { get { return 5; } }
        /// <summary>
        /// مقدار 5
        /// </summary>
        public static int TechnicalAcceptRequest { get { return 6; } }
        /// <summary>
        /// مقدار 6
        /// </summary>
        public static int DeleteRequest { get { return 7; } }
        /// <summary>
        /// مقدار 7
        /// </summary>
        public static int AllRequest { get { return 8; } }


        /// <summary>
        /// برگردانند متن فارسی وضعیت درخواست
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string StatusText(int status)
        {
            switch (status)
            {
                case 0:
                    return "ثبت درخواست";
                case 1:
                    return "تایید دانشکده";
                case 9:
                    return "تایید مالی";
                case 2:
                    return "تایید اداری";
                case 3:
                    return "رد درخواست";
                case 5:
                    return "از دست رفته";
                case 6:
                    return "تایید فنی";
                case 7:
                    return "حف درخواست";
                case 8:
                    return "همه درخواست ها";
                default:
                    return null;
            }
        }
        public static string NextStatusText(int status, bool IsTechnical,string strReasonReject="")
        {
            switch (status)
            {
                case 0:
                    return "دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد و در حال بررسی برای تایید دانشکده است.";
                case 9:
                    return "دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد و در حال بررسی برای تایید مالی است.";
                case 1:
                    if (IsTechnical)
                        if (strReasonReject == "")
                            return "دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد و در حال بررسی برای تایید فنی است.";
                        else
                            return strReasonReject;
                    else
                        return "دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد و در حال بررسی برای تایید اداری است.";
                case 2:
                    return "دانشجوی گرامی ، با درخواست رزرو جلسه دفاع شما موافقت گردیده شده.";
                case 3:
                    return "درخواست رزرو جلسه دفاع شما در گردش می باشد و برای ویرایش مجدد در حال بررسی در دانشکده است.";
                case 5:
                    return "دانشجوی گرامی ، به دلیل گذشتن زمانش از تاریخ کنونی از دست رفته می باشد.";
                case 6:
                    return "دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد و در حال بررسی برای تایید اداری است.";
                case 7:
                    return "درخواست رزرو جلسه دفاع شما حذف گردید است در صورت نیاز می توانید درخواست جدید ثبت نمایید.";
                case 8:
                    return "همه درخواست ها";

                default:
                    return null;
            }
        }

    }


    public class RequestDefInfo
    {
        public string StudentFullName { get; set; }
        public string issue_time { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }
        public int RequestId { get; set; }
        public string StudentCode { get; set; }
        public string DefenceSubject { get; set; }
        public string RequestDate { get; set; }
        public string fullNameRahnama1 { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }


    }
    [Serializable()]
    public class StudentDefenceRequestDTO
    {

        public int ID { get; set; }
        public string subject { get; set; }
        public string note { get; set; }
        public string answernote { get; set; }
        public string answer_time { get; set; }
        public string issue_time { get; set; }
        public int status { get; set; }
        public int issuerID { get; set; }
        public string send_time { get; set; }
        public int catID { get; set; }
        public int senderID { get; set; }
        public int replierID { get; set; }
        public bool isDeleted { get; set; }
        public string issuerName { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public string courseName { get; set; }
        public int courseID { get; set; }
        public int daneshID { get; set; }
        public string term { get; set; }
        //public int Id { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public string StudentField { get; set; }
        public bool studentGender { get; set; }
        public string GroupAcceptDate { get; set; }
        public string DefenceSubject { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int RequestId { get; set; }
        public int OnlineFirstTeacherId { get; set; }
        public int OnlineSecondTeacherId { get; set; }
        public string OnlineTeacherRole { get; set; }
        public bool IsEducateProfessor { get; set; }
        public bool IsEquippingResource { get; set; }
        public bool IsUseOwnSystem { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RequestDate { get; set; }
        public bool IsEdited { get; set; }
        public string nameresh { get; set; }

        public bool? DefenceHasDone { get; set; }
        //public bool? PayedDefence { get; set; } = false;
        public bool? ChkPaymentDavar1 { get; set; } = false;
        public bool? ChkPaymentDavar2 { get; set; } = false;
        //sadeghsaryazdi
        public bool FlagDoingMeetingOnline { get; set; } = false;
        public bool FlagAcceptDavin { get; set; }
        public bool FlagAcceptDavOut { get; set; }
        public bool FlagAcceptMosh1 { get; set; }
        public bool FlagAcceptMosh2 { get; set; }
        public bool FlagAcceptRah1 { get; set; }
        public bool FlagAcceptRah2 { get; set; }
        public DateTime? DateRegistration { get; set; }
        public int? NezamId { get; set; }
        public string NezamName { get; set; }
        public bool? IsRejectFinancial { get; set; }
        public bool? IsSendSmsFinancial { get; set; }
        public string MsgSendSmsFinancial { get; set; }
        public bool IsRequestEducation { get; set; }
    }


    public class FinancialPermission
    {
        public string StudentCode { get; set; }
        public string NationalCode { get; set; }
        public string StudentName { get; set; }
        public string PortalPermissionDate { get; set; }
        public string UnitSectionDate { get; set; }
        public bool? Permission { get; set; }
        public bool Debit { get; set; }

    }
}

