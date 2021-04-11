using System;
using System.Data;
using IAUEC_Apps.Business.university.Request;


namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class HomePageUI : System.Web.UI.Page
    {
        RequestStudentCartBusiness DAO = new RequestStudentCartBusiness();
        DataTable dt = new DataTable();
        /// <summary>
        ///با لود شدن صفحه، اطلاعات دانشجو از طریق شماره دانشجویی استخراج می گردد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = DAO.GetStudentsInfo(Session[sessionNames.userID_StudentOstad].ToString());
                lbl_NamePrev.Text = dt.Rows[0]["firstName"].ToString();
                lbl_LastNamePrev.Text = dt.Rows[0]["lastName"].ToString();
                lbl_SalVorudPrev.Text = dt.Rows[0]["enterYear"].ToString();
                lbl_ReshtePrev.Text = dt.Rows[0]["nameresh"].ToString();
                lbl_MaghtaPrev.Text = dt.Rows[0]["magh"].ToString();
                lbl_ShomareDaneshjuPrev.Text = Session[sessionNames.userID_StudentOstad].ToString();



            }
        }
    }
}