using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
    public class DefenceInformation
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string RequestId { get; set; }
        public string StudentFullName { get; set; }
        public string StudentField { get; set; }           //
        public string studentGender { get; set; }          //
        public string GroupAcceptDate { get; set; }
        public string DefenceSubject { get; set; }

        public string CollegeId { get; set; }
        public string CollegeName { get; set; }

        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public string RequestDate { get; set; }


        public string FirstConsultantId { get; set; }
        public string SecondConsultantId { get; set; }

        public string FirstGuideId { get; set; }
        public string SecondGuideId { get; set; }

        public string FirstRefereeId { get; set; }
        public string SecondRefereeId { get; set; }

        public string FirstConsultantFullName { get; set; }
        public string SecondConsultantFullName { get; set; }

        public string FirstGuideFullName { get; set; }
        public string SecondGuideFullName { get; set; }

        public string FirstRefereeFullName { get; set; }
        public string SecondRefereeFullName { get; set; }

        public string FirstConsultantGender { get; set; }       //
        public string SecondConsultantGender { get; set; }      //
        public string FirstGuideGender { get; set; }          //
        public string SecondGuideGender { get; set; }         //
        public string FirstRefereeGender { get; set; }        //
        public string SecondRefereeGender { get; set; }       //


        public string FirstConsultantMobile { get; set; }
        public string SecondConsultantMobile { get; set; }

        public string FirstGuideMobile { get; set; }
        public string SecondGuideMobile { get; set; }

        public string FirstRefereeMobile { get; set; }
        public string SecondRefereeMobile { get; set; }


        public string FirstConsultantMail { get; set; }
        public string SecondConsultantMail { get; set; }

        public string FirstGuideMail { get; set; }
        public string SecondGuideMail { get; set; }

        public string FirstRefereeMail { get; set; }
        public string SecondRefereeMail { get; set; }
        public bool UseOwnPc { get; set; }

        public string OnlineTeacherRole { get; set; }
        public string OnlineFirstTeacherName { get; set; }
        public string OnlineSecondTeacherName { get; set; }
        public string OnlineFirstTeacherId { get; set; }
        public string OnlineSecondTeacherId { get; set; }
        public bool IsEdited { get; set; }
        public bool IsEquippingResource { get; set; }
        public string ResourceName { get; set; }
        //public string LocationName { get; set; }
     
       //sadeghsaryazdi
        public bool FlagDoingMeetingOnline { get; set; } 
        public bool FlagUpdateRegisterDate { get; set; }
        public bool IsRequestEducation { get; set; }



    }


    
}
