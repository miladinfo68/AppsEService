using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.DAO.University.Request;

namespace IAUEC_Apps.Business.university.Request
{
    public class MadarekReportBusiness
    {
        MadarekReportDAO mrDAO = new MadarekReportDAO();

        public DataTable GetInfo(MadarekReportDTO dto)
        {
            return mrDAO.GetInfo(dto);
        }
    }
}
