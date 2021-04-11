using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class PaymentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<PaymentDTO> lstDto = new List<PaymentDTO>();
            PaymentBusiness PB = new PaymentBusiness();
            lstDto = PB.Get_Student_Payment(Session[sessionNames.userID_StudentOstad].ToString());
            grd_PaymentList.DataSource = lstDto;
            grd_PaymentList.DataBind();
        }
    }
}