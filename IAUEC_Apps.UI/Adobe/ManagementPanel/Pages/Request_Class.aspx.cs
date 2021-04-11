using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class Request_Class : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
           
            if (!IsPostBack)
            {
                // تمام نیمسال های قابل قبول را بر می گرداند
                List<string> TermList = StuPresentBusiness.GetActiveTerm();              
                foreach (var item in TermList)
                    ddl_Nimsal.Items.Add(item.ToString());

                // تعداد جلسات یک کلاس
                for (int i = 1; i < 17; i++)
                    ddl_MeetingCount.Items.Add(i.ToString());

                // روزهای کلاس
                ddl_Day.Items.Add("نامشخص"); // 0
                ddl_Day.Items.Add("شنبه"); // 1
                ddl_Day.Items.Add("یکشنبه"); // 2
                ddl_Day.Items.Add("دوشنبه"); // 3
                ddl_Day.Items.Add("سه شنبه"); // 4
                ddl_Day.Items.Add("چهارشنبه"); // 5
                ddl_Day.Items.Add("پنجشنبه"); // 6
                ddl_Day.Items.Add("جمعه"); // 7



                // نوع کلاس    
                ddl_ClassType.Items.Add("همایش");
                ddl_ClassType.Items.Add("سمینار");
                ddl_ClassType.Items.Add("کلاس درس");



            }
            
        }

        protected void btn_RegisterRequest_Click(object sender, EventArgs e)
        {
            ManagementPanelBusiness MPB = new ManagementPanelBusiness();

        //    if (!CommonBusiness.IsNumeric(txt_CountOfUser.Text))
        //        RadWindowManager1.RadAlert("لطفا تعداد دانشجویان را درست وارد نمایید", 500, 200, "پیام", "");
        //    else if (MPB.Insert_CourseClass(InsertData()) == false)
        //        RadWindowManager1.RadAlert("درخواست مورد را دوباره ثبت کنید", 500, 200, "پیام", "");
        //    else
        //        RadWindowManager1.RadAlert("درخواست مورد با موفقیت ثبت گردید", 500, 200, "پیام", "");


        }

        public ManagementPanelDTO InsertData()
        {
            ManagementPanelDTO MpClass = new ManagementPanelDTO();                    

            //MpClass.Id=0;
            //MpClass.Class=txt_ClassName.Text;
            //MpClass.Course = txt_CourseName.Text;
            //MpClass.MeetingCount = int.Parse(ddl_MeetingCount.SelectedValue);
            //MpClass.UserCount = int.Parse(txt_CountOfUser.Text);
            //MpClass.Tterm = ddl_Nimsal.SelectedValue;
            //MpClass.SaatStart = txt_TimeStart.Text;
            //MpClass.SaatEnd = txt_TimeEnd.Text;
            //MpClass.IdRoz = ddl_Day.SelectedIndex;
            //MpClass.idUniversity = 1;
            //MpClass.idType = ddl_ClassType.SelectedIndex;

            return MpClass;
        }







    }
}