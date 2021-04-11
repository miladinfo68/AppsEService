using System.Data;
namespace IAUEC_Apps.Business.Conatct.Functions
{
    class RowDataTableOstad
    {
        #region Get
        public static DataTable GetDataTableOstad(DataTable dt)
        {
            DataTable dtOstad = new DataTable();
            dtOstad.Columns.Add("FullName");
            dtOstad.Columns.Add("ID");
            DataRow dr;

            if (dt!=null&&dt.Rows.Count>0)
            {
                if (dt.Rows[0]["Moshaver1"] != null&& dt.Rows[0]["Moshaver1"].ToString()!="0"&&
                    dt.Rows[0]["fullNameMoshaver1"] != null)
                {
                    dr = dtOstad.NewRow();
                    dr["ID"] = dt.Rows[0]["Moshaver1"].ToString();
                    dr["FullName"] = dt.Rows[0]["fullNameMoshaver1"] + "(مشاور اول)";
                    dtOstad.Rows.Add(dr);
                }
                if (dt.Rows[0]["Moshaver2"] != null && dt.Rows[0]["Moshaver2"].ToString() != "0" &&
                    dt.Rows[0]["fullNameMoshaver2"] != null) 
               {
                    dr = dtOstad.NewRow();
                    dr["ID"] = dt.Rows[0]["Moshaver2"].ToString();
                    dr["FullName"] =  dt.Rows[0]["fullNameMoshaver2"] + "(مشاور دوم)" ;
                    dtOstad.Rows.Add(dr);
                }
                if (dt.Rows[0]["Rahnama1"] != null && dt.Rows[0]["Rahnama1"].ToString() != "0" &&
                    dt.Rows[0]["fullNameRahnama1"] != null)
                {
                    dr = dtOstad.NewRow();
                    dr["ID"] = dt.Rows[0]["Rahnama1"].ToString();
                    dr["FullName"] = dt.Rows[0]["fullNameRahnama1"] != null ?
                                    dt.Rows[0]["fullNameRahnama1"] + "(راهنما اول)" : "";
                    dtOstad.Rows.Add(dr);
                }
                if (dt.Rows[0]["Rahnama2"] != null && dt.Rows[0]["Rahnama2"].ToString() != "0" &&
                         dt.Rows[0]["fullNameRahnama2"] != null)
                {
                    dr = dtOstad.NewRow();
                    dr["ID"] = dt.Rows[0]["Rahnama2"].ToString();
                    dr["FullName"] = dt.Rows[0]["fullNameRahnama2"] != null ?
                                    dt.Rows[0]["fullNameRahnama2"] + "(راهنما دوم)" : "";
                    dtOstad.Rows.Add(dr);
                }
                if (dt.Rows[0]["DavarIn"] != null && dt.Rows[0]["DavarIn"].ToString() != "0" &&
                        dt.Rows[0]["fullNameDavarIn"] != null)
                {
                    dr = dtOstad.NewRow();
                    dr["ID"] = dt.Rows[0]["DavarIn"].ToString();
                    dr["FullName"] = dt.Rows[0]["fullNameDavarIn"] != null ?
                                    dt.Rows[0]["fullNameDavarIn"] + "(داور داخلی)" : "";
                    dtOstad.Rows.Add(dr);
                }
                if (dt.Rows[0]["DavarOUT"] != null && dt.Rows[0]["DavarOUT"].ToString() != "0" &&
                    dt.Rows[0]["fullNameDavarOUT"] != null)
                {
                    dr = dtOstad.NewRow();
                    dr["ID"] = dt.Rows[0]["DavarOUT"].ToString();
                    dr["FullName"] = dt.Rows[0]["fullNameDavarOUT"] != null ?
                                    dt.Rows[0]["fullNameDavarOUT"] + "(داور خارجی)" : "";
                    dtOstad.Rows.Add(dr);
                }

            }
            return dtOstad;


        }
        #endregion
    }
}
