using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class ProfessorHokmDTO
    {
        public ProfessorHokmDTO()
        {

        }
        public ProfessorHokmDTO(int infoPeopleId)
        {
            InfoPeopleId = infoPeopleId;
        }

        public int InfoPeopleId { get; set; }

        public int Code_Ostad { get; set; }

        public string HokmUrl { get; set; }

        public Int64 MablaghHokm { get; set; }

        public string Number_Hokm { get; set; }

        public string Date_RunHokm { get; set; }

        public string Date_Hokm { get; set; }

        public int Payeh { get; set; }

        public int Type_Estekhdam { get; set; }

        public int Uni_Khedmat { get; set; }

        public int Uni_KhedmatType { get; set; }

        public int Nahveh_Hamk { get; set; }

        public int NahveHamkariNew { get; set; }

        public string DateRunHokmHere { get; set; }

        public bool IsRetired { get; set; }

        public string DateUpload { get; set; }

        public int State { get; set; }

        public int Martabeh { get; set; }

        public int HokmId { get; set; }

        public int EditRequestId { get; set; }

        public bool BoundHour { get; set; }
    }


}
