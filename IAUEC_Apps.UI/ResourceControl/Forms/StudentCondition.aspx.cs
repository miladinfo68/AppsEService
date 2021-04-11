using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.DTO.ResourceControlClasses;
using ResourceControl.BLL;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentCondition : System.Web.UI.Page
    {
        private readonly RequestHandler _requestHandler;
        public static StudentConditionViewModel ViewModel;



        private static DataTable _studentDefenceRequests;

        public StudentCondition()
        {
            _requestHandler = new RequestHandler();
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad].ToString());
            _studentDefenceRequests = _requestHandler.GetStudentDefenceRequest(userId);

            ViewModel= new StudentConditionViewModel(0, 25, ".25",20,60, "تایید دانشکده",false);
        }
    }
}