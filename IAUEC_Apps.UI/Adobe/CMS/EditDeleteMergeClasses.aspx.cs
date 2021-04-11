using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class EditDeleteMergeClasses : System.Web.UI.Page
    {
        ClassBusiness clsB = new ClassBusiness();
        DataTable dt = new DataTable();
        DataTable term = new DataTable();
        UniversityBusiness unvB = new UniversityBusiness();
        ClassListDTO mrgClass = new ClassListDTO();

        string nimsal;
        int MergeCode;

        protected void Page_Load(object sender, EventArgs e)
        {
            RegPcal();
            if (IsPostBack)
            {
                rdbDelete.Visible = true;
                rdbEdit.Visible = true;
            }
            else
            {
                term = unvB.GetNimsalJary();
                nimsal = term.Rows[0]["nimsal"].ToString();
                ViewState.Add("nimsal", nimsal);
                txtmergeCode.Focus();
            }
            if (rdbEdit.Checked)
            {
                divDelete.Visible = false;
                divEdit.Visible = true;
            }
            else if (rdbDelete.Checked)
            {
                divDelete.Visible = true;
                divEdit.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtmergeCode.Text != string.Empty)
            {
                MergeCode = Convert.ToInt32(txtmergeCode.Text);
                hide();
                ViewState.Add("MergeCode", MergeCode);

                bindGrid();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            mrgClass.Semester = ViewState["nimsal"].ToString();
            mrgClass.MergeCode = ViewState["MergeCode"].ToString();
            mrgClass.CourseCode = txtCourseCode.Text;
            mrgClass.SessionCount = Convert.ToInt32(txtSessionCount.Text);
            mrgClass.FirstSession = txtFirstSession.Text;
            mrgClass.ClassStartTime = RadTimePicker1.DateInput.Text.Substring(11, 5).Replace('-', ':');
            mrgClass.ClassEndTime = RadTimePicker2.DateInput.Text.Substring(11, 5).Replace('-', ':');
            mrgClass.ProfID = Convert.ToInt32(cmbProf.SelectedValue);
            mrgClass.ClassDay = Convert.ToInt32(drpDay.SelectedValue);

            clsB.EditMergeClass(mrgClass);

            hide();
            bindGrid();
                    
            string msg = "کلاس های ادغام شده با موفقیت به روز رسانی شدند";
            RadWindowManager1.RadAlert(msg, 0, 100, "پیام سیستم", "");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            dt = (DataTable)ViewState["dt"];
            if (dt.Rows.Count != 0)
            {
                nimsal = ViewState["nimsal"].ToString();
                MergeCode = Convert.ToInt32(ViewState["MergeCode"]);
                clsB.DeleteMergeClasses(nimsal, MergeCode);
                bindGrid();
                string msg = "تمامی کلاس های کد ادغامی مورد نظر با موفقیت حذف شدند";
                RadWindowManager1.RadAlert(msg, 0, 100, "پیام سیستم", "");
                hide();
                bindGrid();
            }
            else
            {
                string msg = "کلاسی با کد ادغامی مورد نظر موجود نیست";
                RadWindowManager1.RadAlert(msg, 0, 100, "پیام سیستم", "");
            }
        }

        protected void rdbEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEdit.Checked)
            {
                BindTextBoxes();
            }
        }

        protected void vldTimes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RadTimePicker2.SelectedTime <= RadTimePicker1.SelectedTime)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        private void bindGrid()
        {
            MergeCode = Convert.ToInt32(ViewState["MergeCode"]);
            nimsal = ViewState["nimsal"].ToString();
            dt = clsB.getMergeCodeClasses(nimsal, MergeCode);
            grdClassList.DataSource = dt;
            grdClassList.DataBind();

            ViewState.Add("dt", dt);

            divGrid.Visible = true;

            if (dt.Rows.Count != 0)
            {
                rdbDelete.Visible = true;
                rdbEdit.Visible = true;
            }
            else
            {
                rdbDelete.Visible = false;
                rdbEdit.Visible = false;
            }
        }

        private void hide()
        {
            divDelete.Visible = false;
            divEdit.Visible = false;
            divGrid.Visible = false;
            rdbDelete.Visible = false;
            rdbEdit.Visible = false;
            rdbDelete.Checked = false;
            rdbEdit.Checked = false;
        }

        private void clear()
        {
            hide();
            txtmergeCode.Text = string.Empty;
            txtCourseCode.Text = string.Empty;
            txtFirstSession.Text = string.Empty;
            txtSessionCount.Text = string.Empty;
            drpDay.SelectedIndex = 0;
            cmbProf.SelectedIndex = 0;
            rdbDelete.Checked = false;
            rdbEdit.Checked = false;
        }

        private void BindTextBoxes()
        {
            List<string> dateTime = new List<string>();

            dt = (DataTable)ViewState["dt"];

            txtCourseCode.Text = dt.Rows[dt.Rows.Count - 1]["codedars"].ToString();
            txtFirstSession.Text = dt.Rows[dt.Rows.Count - 1]["date_first_session"].ToString();
            txtSessionCount.Text = dt.Rows[dt.Rows.Count - 1]["count_sessions"].ToString();
            string s = dt.Rows[dt.Rows.Count - 1]["saatklass"].ToString();
            dateTime = SeprateDateAndTime(s);

            drpDay.SelectedValue = getDayValue(dateTime[0]).ToString();

            TimeSpan Stime = TimeSpan.Parse(dateTime[1]);
            TimeSpan Etime = TimeSpan.Parse(dateTime[2]);

            RadTimePicker1.SelectedTime = Stime;
            RadTimePicker2.SelectedTime = Etime;

            bindCmb();
            cmbProf.SelectedValue = dt.Rows[dt.Rows.Count - 1]["idostad"].ToString();
        }

        private void bindCmb()
        {
            DataTable prof = new DataTable();
            prof = clsB.getProfMergeName();
            cmbProf.DataSource = prof;
            cmbProf.DataTextField = "MergeName";
            cmbProf.DataValueField = "code_ostad";
            cmbProf.DataBind();
        }

        private int getDayValue(string str)
        {
            int value = 0;
            switch (str)
            {
                case "شنبه":
                    value = 1;
                    break;
                case "یکشنبه":
                    value = 2;
                    break;
                case "دوشنبه":
                    value = 3;
                    break;
                case "سه شنبه":
                    value = 4;
                    break;
                case "چهار شنبه":
                    value = 5;
                    break;
                case "پنج شنبه":
                    value = 6;
                    break;
                case "جمعه":
                    value = 7;
                    break;
                default:
                    value = 0;
                    break;
            }
            return value;
        }

        private List<string> SeprateDateAndTime(string s)
        {
            List<string> dateTime = new List<string>();

            s = s.Replace(' ', '-');
            s = s.Replace('-', '$');
            int dollar = 0;
            int count = 0;
            int stime = 0;
            int day = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '$')
                {
                    dollar++;
                }
            }

            if (dollar == 3)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '$')
                    {
                        count++;
                        if (count <= 2)
                        {
                            day = i;
                        }
                        else if (count == 3)
                        {
                            stime = i;
                        }
                    }
                }
            }
            else if (dollar == 2)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == '$')
                    {
                        count++;
                        if (count <= 1)
                        {
                            day = i;
                        }
                        else if (count == 2)
                        {
                            stime = i;
                        }
                    }
                }
            }

            string dayStr = s.Substring(0, day);
            dayStr = dayStr.Replace('$', ' ');
            string stimeStr = s.Substring(day + 1, stime - day - 1);
            string etimeStr = s.Substring(stime + 1, s.Length - stime - 1);

            dateTime.Add(dayStr);
            dateTime.Add(stimeStr);
            dateTime.Add(etimeStr);

            return dateTime;
        }
        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtFirstSession', {extraInputID: 'ContentPlaceHolder1_txtFirstSession',extraInputFormat: 'yyyy/mm/dd'}); ";

            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp1, true);
        }
    }
}