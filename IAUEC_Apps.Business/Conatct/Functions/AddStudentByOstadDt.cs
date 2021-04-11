
using System.Data;

namespace IAUEC_Apps.Business.Conatct.Functions
{
  public static class AddStudentByOstadDt
    {
        public static DataTable GetOstadAddStudent(DataTable dtSt, DataTable dtOS)
        {
            DataTable dt = new DataTable();
            if(dtOS!=null&&dtOS.Rows.Count>0)
            {
                dt = dtOS;
            }
            if(dtSt!=null&&dtSt.Rows.Count>0)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["ID"] = dtSt.Rows[0]["ID"].ToString();
                    dr["FullName"] = dtSt.Rows[0]["fullName"] != null ?
                                     dtSt.Rows[0]["fullName"] + "(دانشجو)" : "";
                    dr["Images"] = dtSt.Rows[0]["Images"];
                    dt.Rows.Add(dr);
                 
                }
            }
            return dt;
               
        }
    }
}
