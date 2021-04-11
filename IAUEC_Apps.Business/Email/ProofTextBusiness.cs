using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.Email;
using IAUEC_Apps.DTO.EmailClasses;

namespace IAUEC_Apps.Business.Email
{
    public class ProofTextBusiness
    {
        ProofText prt = new ProofText();
        ProofTextDAO prtDAO = new ProofTextDAO();
        public void CreateProofTextByProofText(string ProofText)
        {
            prtDAO.CreateProofTextByProofText(ProofText);
        }
        public DataTable GiveAllProofText()
        {
            //List<ProofText> lstproof = new List<ProofText>();
            //DataTable dt = new DataTable();
            return prtDAO.GiveAllProofText();
            //for(int i=0;i<dt.Rows.Count;i++)
            //{
            //    prt.Id = int.Parse(dt.Rows[i]["Id"].ToString());
            //    prt.Prooftext = dt.Rows[i]["Prooftext"].ToString();
            //    //lstproof[i] = prt;
            //}
            //return lstproof;
        }
    }
}
