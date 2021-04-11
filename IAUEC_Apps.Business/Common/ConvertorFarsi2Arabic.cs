using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.Common
{
    public static class ConvertorFarsi2Arabic
    {
        public static string Parse_Farsi2_Arabic(string strInput)
        {
            string res = "";
            res = strInput.Replace('ک', 'ك');
            res = strInput.Replace('ی', 'ي');
            res = strInput.Replace('ی', 'ي');
            //res = strInput.Replace(  'ی'  ,  'ئ' );
            //res = strInput.Replace(  'ا'  ,  'آ' );
            //res = strInput.Replace(  'ا'  ,  'أ' );
            //res = strInput.Replace(  'ا'  ,  'إ' );
            //res = strInput.Replace(  'ه'  ,  'ۀ' );
            //res = strInput.Replace(  'ه'  ,  'ة' );
            //res = strInput.Replace(  'و'  ,  'ؤ' );
            //res = strInput.Replace(   ''  ,  'ء' );
            return res;
        }
    }
}
