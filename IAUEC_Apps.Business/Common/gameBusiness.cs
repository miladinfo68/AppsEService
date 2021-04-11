using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IAUEC_Apps.Business.Common
{
    public class gameBusiness
    {
        public DataTable getActiveGames()
        {
            DAO.CommonDAO.gameDAO gdao = new DAO.CommonDAO.gameDAO();
            DataTable dt = gdao.getActiveGames();
            return dt;
        }
        public string getGameLink(int gameID)
        {
            DAO.CommonDAO.gameDAO gdao = new DAO.CommonDAO.gameDAO();
            DataTable dt = gdao.getActiveGames();
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("gameid=" + gameID);
                if (dr.Length == 1)
                    return dr[0]["gameLinkAddress"].ToString();
            }
            return "";
        }
        public void setGameLog(int gameID,string playerID,int playerType)
        {
            DAO.CommonDAO.gameDAO gdao = new DAO.CommonDAO.gameDAO();
            gdao.setPlayLog(gameID, playerID, playerType);
        }
    }
}
