using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public static class CheckOutStatusEnum
    {
     
        public enum CheckOutAllStatusEnum
        {
            submited = 10,
            Moavenat_Amoozesh = 11,
            daneshkade = 12,
            amoozesh = 13,
            daneshjooyi = 14,
            pajohesh = 15,
            refah = 16,
            maali = 17,
            fani = 18,
            mashmulan = 19,
            archive = 20,
            takmil_parvande = 21,
            //dabirkhane =22,
            vrood_moavenat = 23,
            ersal_sodoor = 24,
            end = 25,
            stampPay = 31

        }



        public enum CheckOutType
        {
            taqir_reshte = 13,
            ekhraj = 14,
            fareq_tahsil = 15,
            enseraf = 16,
            enteqali = 17

        }

        public enum CheckOutTypeFarsi
        {
            تغییررشته = 13,
            اخراج = 14,
            فارغ‌_التحصیل = 15,
            انصراف = 16,
            انتقالی = 17

        }

        public enum FareghReqStatus
        {
            submited = 10,
            daneshkade_ok = 12,
            amoozesh_ok = 13,
            daneshjooyi_ok = 14,
            pajohesh_ok = 15,
            refah_ok = 16,
            maali_ok = 17,
            fani_ok = 18,
            mashmulan_ok = 19,
            archive_ok = 20,
            takmil_parvande_ok = 21,
            //dabirkhane_ok = 22,
            vrood_moavenat_ok = 23,
            ersal_sodoor_ok = 24,
            end = 25,
            stampPay = 31
        }
        public enum FareghReqStatusBacheLor
        {
            submited = 10,
            daneshkade_ok = 12,
            amoozesh_ok = 13,
            daneshjooyi_ok = 14,
            refah_ok = 16,
            maali_ok = 17,
            fani_ok = 18,
            mashmulan_ok = 19,
            archive_ok = 20,
            takmil_parvande_ok = 21,
            //dabirkhane_ok = 22,
            vrood_moavenat_ok = 23,
            end = 25
        }

        public enum EnserafReqStatus
        {
            submited = 10,
            Moavenat_Amoozesh_ok = 11,
            daneshkade_ok = 12,
            amoozesh_ok = 13,
            daneshjooyi_ok = 14,
            //pajohesh_ok = 15,
            refah_ok = 16,
            maali_ok = 17,
            fani_ok = 18,
            mashmulan_ok = 19,
            archive_ok = 20,
            end = 25
        }

        public enum EkhrajStatus
        {
            submited = 10,
            Moavenat_Amoozesh_ok = 11,
            daneshkade_ok = 12,
            amoozesh_ok = 13,
            daneshjooyi_ok = 14,
            refah_ok = 16,
            maali_ok = 17,
            fani_ok = 18,
            mashmulan_ok = 19,
            archive_ok = 20,
            end = 25
        }

        public enum TaghirReshteStatus
        {
            submited = 10,
            Moavenat_Amoozesh_ok = 11,
            daneshkade_ok = 12,
            amoozesh_ok = 13,
            daneshjooyi_ok = 14,
            refah_ok = 16,
            maali_ok = 17,
            mashmulan_ok = 19,     
            end = 25
        }


        public enum EnteghaliStatus
        {
            submited = 10,
            daneshkade_ok = 12,
            amoozesh_ok = 13,
            daneshjooyi_ok = 14,
            refah_ok = 16,
            maali_ok = 17,
            fani_ok = 18,
            mashmulan_ok = 19,
            archive_ok = 20,
            end = 25
        }
    }
}
