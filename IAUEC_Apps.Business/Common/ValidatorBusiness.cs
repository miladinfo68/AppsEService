using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.Business.Common
{
   public class ValidatorBusiness
    {
       public const string regexMarsulePostiCode = @"^[0-9]{10,20}$";
       public static bool ValidateMarsulePostiCode(string str)
       {
           return new Regex(regexMarsulePostiCode).IsMatch(str);
       }
    }
}
