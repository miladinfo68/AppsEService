using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DAO.CommonDAO;

namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutReportBusiness
    {
         UserAccessSection uasDAO = new UserAccessSection();

         public DataTable GetSection()
         {
             return uasDAO.GetSection();
         }

         public DataTable GetRequest(UserAccessSectionDTO uasDTO)
        {
            return uasDAO.GetRequest(uasDTO);
        }
    }
}
