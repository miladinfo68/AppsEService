using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
 using System.Web.UI.WebControls;
namespace IAUEC_Apps.UI.Contact.Functions
{
    public class AddDefualt
    {
        public static DataTable GetDefualt(DataTable dt)
        {
          
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["images"] == null || row["images"].ToString() == "")
                    {
                        var imgDef = System.Web.HttpContext.Current.Server.MapPath("../Img/avatarContact.jpg");

                        byte[] b = System.IO.File.ReadAllBytes(imgDef);

                        row["images"] = b;
                    }
                    if (dt.Columns.Contains("IsDeleted") && row["IsDeleted"].ToString().Trim() == "True")  
                    {
                        row["Message"] = "پیام پاک شده است";
                    }
                    if (dt.Columns.Contains("FlagReplayed") && row["FlagReplayed"].ToString().Trim()== "True"
                        &&(( (row["RplyMsg"]==null||row["RplyMsg"].ToString().Trim()==""))
                        ||(( row["IsDeletedReplay"].ToString().Trim() == "True"))))
                    {
                        row["RplyMsg"] = "پیام پاک شده است";
                    }

                }

            }
            return dt;
        }

    }
}