using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.DAO.Email;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.Business.Email
{
    public class Email_ConnectBusiness
    {
        Email_ConnectDAO emDAO = new Email_ConnectDAO();
        public void CreateEmailConnect(string SmsText, string EmailText, int status)
        {
            emDAO.CreateEmailConnect(SmsText, EmailText, status);
        }
        public List<Email_ConnectDTO> GetConnectTextByStatus(int status)
        {
            Email_ConnectDTO em = new Email_ConnectDTO();
            List<Email_ConnectDTO> emList = new List<Email_ConnectDTO>();
           
            DataTable dt = new DataTable();
            dt = emDAO.GetConnectTextByStatus(status);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                em.SmsText = dt.Rows[i]["SmsText"].ToString();
                em.EmailText = dt.Rows[i]["EmailText"].ToString();
                em.StatusId = int.Parse(dt.Rows[i]["StatusId"].ToString());
                emList.Add(em);
            }
            return emList;
        }
        public int GetConnectTypeByStcode(string stcode)
        {
            return emDAO.GetConnectTypeByStcode(stcode);
        }
    }

}
