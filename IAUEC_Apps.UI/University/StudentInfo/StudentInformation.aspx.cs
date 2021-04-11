using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.StudentInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.StudentInfo
{
    public partial class StudentInformation : System.Web.UI.Page
    {
        private static readonly StudentInfoBusiness _studentInfoBusiness = new StudentInfoBusiness();
        static string  stCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            stCode = Session[sessionNames.userID_StudentOstad]?.ToString();
            mobile.Value = _studentInfoBusiness.GetMobileByStudentCode(stCode);
        }
        [WebMethod]
        public static List<StateDTO> GetStates()
        {
            return _studentInfoBusiness.GetStates();
        }
        [WebMethod]
        public static List<CityDTO> GetCityDTO(int stateCode)
        {
            return _studentInfoBusiness.GetCityDTO(stateCode);
        }
        [WebMethod]
        public static List<MilitryStatusDTO> GetMilitryStatusDTO()
        {
            return _studentInfoBusiness.GetMilitryStatusDTO();
        }
        [WebMethod]
        public static bool UpdateStudent(StudentDTO studentDTO)
        {
            studentDTO.StudentCode = stCode;
            return _studentInfoBusiness.UpdateStudent(studentDTO);
        }
    }
}