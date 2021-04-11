using IAUEC_Apps.DAO.University.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.university.Exam
{
    public class RequestDistributionOfStudentBusiness
    {
        RequestDistributionOfStudentDAO RequestDistributionOfStudentDAO = new RequestDistributionOfStudentDAO();

        public List<string> GetAllProvince()
        {
            DataTable DT = RequestDistributionOfStudentDAO.GetAllProvince();
            List<string> ProvinceList = new List<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
                ProvinceList.Add(DT.Rows[i]["Province"].ToString());

            return ProvinceList;
        }


        public List<string> GetAllTerm()
        {
            DataTable DT = RequestDistributionOfStudentDAO.GetAllTerm();
            List<string> ProvinceList = new List<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
                ProvinceList.Add(DT.Rows[i]["tterm"].ToString());

            return ProvinceList;
        }


        public DataTable GetProvinceStudent( string Province)
        {
            return RequestDistributionOfStudentDAO.GetProvinceStudent( Province);
        }






    }
}
