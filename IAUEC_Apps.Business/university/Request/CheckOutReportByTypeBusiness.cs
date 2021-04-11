using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DAO.University.Request;


namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutReportByTypeBusiness
    {
        CheckOutReportByTypeDAO reqDao = new CheckOutReportByTypeDAO();

        public DataTable getAllRequestBytype(int isOnline, int reqType, string sDate, string eDate)
        {
            return reqDao.getAllRequestBytype(isOnline, reqType, sDate, eDate);
        }
        public DataTable GetAllRequestByPayment(string sDate, string eDate)
        {
            return reqDao.GetAllRequestByPayment( sDate, eDate);
        }
    }
}
