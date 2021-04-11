
using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.ResourceControl
{
    public class DaneshKadeDBAccess
    {
        public List<Daneshkade> GetAllDaneshkade()
        {
            List<Daneshkade> danlist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[DaneshkadesSelectAll]", CommandType.StoredProcedure))
            {

                if (table.Rows.Count > 0)
                {
                    danlist = new List<Daneshkade>();

                    foreach (DataRow row in table.Rows)
                    {
                        Daneshkade danesh = new Daneshkade();
                        danesh.ID = Convert.ToInt32(row["ID"]);
                        danesh.NameDanesh = row["namedanesh"] as string;
                        danlist.Add(danesh);
                    }
                }
            }

            return danlist;
        }

    }
}
