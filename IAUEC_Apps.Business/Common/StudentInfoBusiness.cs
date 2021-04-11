using IAUEC_Apps.DAO.StudentInfo;
using IAUEC_Apps.DTO.StudentInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.Common
{
    public class StudentInfoBusiness
    {
        #region Fields
        private readonly StudentInformationDAO _studentInformationDAO = new StudentInformationDAO();
        private readonly CommonBusiness _commonBusiness = new CommonBusiness();
        #endregion

        #region Methods
        public bool IsStudentUpdate(string stCode)
        {
            return _studentInformationDAO.IsStudentUpdate(stCode);
        }
        public string GetMobileByStudentCode(string stCode)
        {
            return _studentInformationDAO.GetStudentByStudentCode(stCode);
        }
        public List<StateDTO> GetStates()
        {
            var res = new List<StateDTO>();
            var dtState = _commonBusiness.GetOstan(0);
            foreach (DataRow row in dtState.Rows)
            {
                res.Add(new StateDTO
                {
                    STATE_CODE = Convert.ToInt32(row["ID"]),
                    STATE_NAME = row["Title"].ToString()
                });
            }
            return res;
        }
        public List<CityDTO> GetCityDTO(int stateId)
        {
            var res = new List<CityDTO>();
            var dtCity = _commonBusiness.getShahrestan(stateId);
            foreach (DataRow row in dtCity.Rows)
            {
                res.Add(new CityDTO
                {
                    CITY_CODE = Convert.ToInt32(row["ID"]),
                    CITY_NAME = row["Title"].ToString()
                });
            }
            return res;
        }
        public List<MilitryStatusDTO> GetMilitryStatusDTO()
        {
            var res = new List<MilitryStatusDTO>();
            var dtMilitryStatus = _commonBusiness.GetStatusMilitary_fcoding();
            foreach (DataRow row in dtMilitryStatus.Rows)
            {
                res.Add(new MilitryStatusDTO
                {
                    Id = Convert.ToInt32(row["id"]),
                    Title = row["namecoding"].ToString()
                });
            }
            return res;
        }
        public bool UpdateStudent(StudentDTO model)
        {
            return _studentInformationDAO.UpdateStudent(model);
        }
        #endregion
    }
}
