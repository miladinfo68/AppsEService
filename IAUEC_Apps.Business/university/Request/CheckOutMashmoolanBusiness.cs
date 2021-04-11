using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Request;

namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutMashmoolanBusiness
    {
        CheckOutMashmoolanDAO MashmoolDao = null;
        public CheckOutMashmoolanBusiness()
        {
            MashmoolDao = new CheckOutMashmoolanDAO();
        }

        public System.Data.DataTable GetMashmoolFareghOk()
        {
            return MashmoolDao.GetMashmoolFareghOk();
        }

        public bool UpdateMashmoolStatusByReqId(int reqId)
        {
            int count = MashmoolDao.UpdateMashmoolStatusByReqId(reqId);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
