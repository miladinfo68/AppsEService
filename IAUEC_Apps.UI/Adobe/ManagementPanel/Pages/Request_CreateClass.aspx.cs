using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IAUEC_Apps.UI.Adobe.ManagementPanel.Pages
{
    public partial class Request_CreateClass : System.Web.UI.Page
    {
        StuPresentBusiness StuPresentBusiness = new StuPresentBusiness();
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // تمام نیمسال های قابل قبول را بر می گرداند
                List<string> TermList = StuPresentBusiness.GetActiveTerm();
                foreach (var item in TermList)
                    ddl_Nimsal.Items.Add(item.ToString());

                // تعداد جلسات یک کلاس
                for (int i = 1; i < 17; i++)
                    ddl_MeetingCount.Items.Add(i.ToString());

                // تعداد دانشجویان یک کلاس
                for (int i = 1; i < 200; i++)
                    ddl_CountOfUser.Items.Add(i.ToString());

                // نوع کلاس
                DataTable DT0 = MPB.Get_CourseType();
                ddl_ClassType.DataTextField = "CourseType";
                ddl_ClassType.DataValueField = "id";
                ddl_ClassType.DataSource = DT0;
                ddl_ClassType.DataBind(); 
                
                // زمان کلاس
                DataTable DT1 = MPB.GetCourseTimeClass();
                ddl_TimeClass.DataTextField = "Time";
                ddl_TimeClass.DataValueField = "id";
                ddl_TimeClass.DataSource = DT1;
                ddl_TimeClass.DataBind();

                // قابلیت های آداب
                DataTable DT2 = MPB.GetGetAdobe_Ability();
                CheckBoxList1.DataTextField = "Ability";
                CheckBoxList1.DataValueField = "id";
                CheckBoxList1.DataSource = DT2;
                CheckBoxList1.DataBind();

            }
        }

        protected void btn_RegisterRequest_Click(object sender, EventArgs e)
        {

            MPB.Insert_CourseClass(InsertData());
        }


        public ManagementPanelDTO InsertData()
        {
            ManagementPanelDTO MpClass = new ManagementPanelDTO();        
        
            //public int IdUniversity { set; get; }
            //public int IdCourseTimeClass { set; get; }
            //public int IdCourseType { set; get; }
            
            MpClass.Id = 0;
            MpClass.Class = txt_ClassName.Text;
            MpClass.Course = txt_CourseName.Text;
            MpClass.MeetingCount = int.Parse(ddl_MeetingCount.SelectedValue);
            MpClass.UserCount = int.Parse(ddl_CountOfUser.SelectedValue);
            MpClass.Tterm = ddl_Nimsal.SelectedValue;            
            MpClass.IdUniversity = 1;           
            MpClass.IdCourseTimeClass = int.Parse(ddl_TimeClass.SelectedValue);
            MpClass.IdCourseType = int.Parse(ddl_ClassType.SelectedValue);

            List<long> AdobeAbility = new List<long>();

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if(CheckBoxList1.Items[i].Selected==true)
                    AdobeAbility.Add(int.Parse(CheckBoxList1.Items[i].Value));            
            }

            MpClass.List_Id_AdobeAbility = AdobeAbility;

            long x = MpClass.List_Id_AdobeAbility.ElementAt(1);

            return MpClass;
        }




    }
}