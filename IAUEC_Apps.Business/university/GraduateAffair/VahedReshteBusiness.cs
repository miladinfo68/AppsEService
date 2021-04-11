using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Graduate;
using IAUEC_Apps.DAO.University.GraduateAffair;

namespace IAUEC_Apps.Business.university.GraduateAffair
{
    public class VahedReshteBusiness
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        VahedReshteDAO vrDAO = new VahedReshteDAO();

        #region get
        public DataTable getVahed()
        {
            return vrDAO.GetVahed();
        }

        public DataTable getSemat()
        {
            return vrDAO.GetSemat();
        }

        public DataTable GetReshteVahed()
        {
            return vrDAO.GetReshteVahed();
        }

        public DataTable GetSematVahed()
        {
            return vrDAO.GetSematVahed();
        }
        #endregion

        #region set
        public int setvahedReshte(VahedReshteDTO vrd)
        {
            return vrDAO.setVahedReshte(vrd);            
        }

        public int setSematvahed(VahedReshteDTO vrd)
        {
            return vrDAO.setSematVahed(vrd);
        }

        public int setVahedInfo(VahedReshteDTO vrd)
        {
            return vrDAO.setVahedInfo(vrd);
        }

        public int setSematInfo(VahedReshteDTO vrd)
        {
            return vrDAO.setSematInfo(vrd);
        }

        #endregion

        #region update

        public void updateVahedInfo(VahedReshteDTO vrd)
        {
            vrDAO.updateVahedInfo(vrd);
        }

        public void updateSematInfo(VahedReshteDTO vrd)
        {
            vrDAO.updateSematInfo(vrd);
        }

        public void updateReshteVahedInfo(VahedReshteDTO vrd)
        {
            vrDAO.updateReshteVahedInfo(vrd);
        }

        public void updateSematVahedInfo(VahedReshteDTO vrd)
        {
            vrDAO.updateSematVahedInfo(vrd);
        }

        #endregion
    }
}
