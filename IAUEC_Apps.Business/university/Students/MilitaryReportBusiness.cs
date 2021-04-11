using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Students;
using IAUEC_Apps.DTO.University.Students;
using System.Data;

namespace IAUEC_Apps.Business.university.Students
{
    public class MilitaryReportBusiness
    {
        MilitaryReportDAO RES = new MilitaryReportDAO();

        public DataTable GetInfoSTUD(MilitaryReportDTO msd)
        {
            return RES.GetInfoSTU(msd);
        }

    }
}
