using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO
{

    public enum basicType
    {
        بدهی = 1,
        آدرس = 2,
        تلفن = 3,
        ثبت = 4,
        کدپستی = 5
    }
    public class contract
    {
        public enum contractType
        {
            educationContract = 1,
            agreement = 2,
            HeadOfDepartment = 3,
            DeputyGroup = 4
        }
         
        public const string educationContract = "EDU";
        public const string agreement = "AGR";
        public const string HeadOfDepartment = "HOD";
        public const string DeputyGroup = "DG";
    }

}
