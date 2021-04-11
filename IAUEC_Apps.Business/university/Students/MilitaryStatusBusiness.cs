using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Students;
using IAUEC_Apps.DTO.University.Students;
using System.Data;

namespace IAUEC_Apps.Business.university.Students
{
    public class MilitaryStatusBusiness
    {
        MilitaryStatusDAO RES = new MilitaryStatusDAO();
        
        public DataTable GetInfoSTUD(MilitaryStatusDTO msd)
        {
            return RES.GetInfoSTU(msd);
        }

        public void updateMashmulNumber(MilitaryStatusDTO msd)
        {
            RES.updateMashmulNumber(msd);
        }

        public void insertMashmulNumber(MilitaryStatusDTO msd)
        {
            RES.insertMashmulNumber(msd);
        }
    }
}
