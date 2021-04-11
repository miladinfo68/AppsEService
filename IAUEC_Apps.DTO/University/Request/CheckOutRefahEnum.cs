using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class CheckOutRefahEnum
    {
        public enum DebitType
        {
            Vezarat_Loan =1 ,
            Cheque=2,
            Marriage_Loan=3,
            Tamin_Loan=4,
            Help_Loan=5,
            Maskan_Loan = 6,
            Emergency_Loan =7 ,
            Bestankar = 8,
            sharhie = 9 ,
            amozeshyar=11

        //    وام_وزارت_علوم=1,
        //    چک_برگشتی=2 ,
        //    وام ازدواج=3,
        //    وام تامین اجتماعی=4,
        //    وام کمک هزینه تحصیلی=5,
        //    وام مسکن=6,
        //    وام ضروری=7 
        //    بستانکار = 8
        }
    }
}
