using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Request.Pages
{
    public partial class GovahiTazakoratUI : System.Web.UI.Page
    {
        /// <summary>
        /// این متد ایونت لود شدن صفحه را انجام می دهد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// برای تایید تذکرات، دانشجو این کلید را فشرده و مرحله درخواست ارسال گواهی رغا ادامه می دهد.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>چنانچه دانشجو مطالب را تایید نکرده باشد، یک پیغام به او نمایش داده می شود</remarks>
        protected void btn_taeid_Click(object sender, EventArgs e)
        {

            Response.Redirect("RequestGovahiUI.aspx");

        }

    }
}