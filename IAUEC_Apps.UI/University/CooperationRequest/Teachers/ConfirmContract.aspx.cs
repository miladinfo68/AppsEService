using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class ConfirmContract : System.Web.UI.Page
    {
        Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
        Business.university.Faculty.FacultyReportsBusiness FRB = new Business.university.Faculty.FacultyReportsBusiness();
        Business.Adobe.ProfPresentBusiness PB = new Business.Adobe.ProfPresentBusiness();
        Business.Common.CommonBusiness commonBSN = new Business.Common.CommonBusiness();
        const string hrID = "hrID";
        string _typeOfContract;
        UserControls.Contract ucContract = new UserControls.Contract();
        UserControls.Contract_DeputyGroup ucContract_DeputyGroup = new UserControls.Contract_DeputyGroup();
        UserControls.Contract_HeadOfDepartment ucContract_HeadOfDepartment = new UserControls.Contract_HeadOfDepartment();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            _typeOfContract = Session["contractType"].ToString();


            switch (_typeOfContract)
            {
                case DTO.contract.educationContract:
                    ucContract = LoadControl("../UserControls/Contract.ascx") as UserControls.Contract;
                    ucContract.Attributes.Add("EnableViewState", "false");
                    plcUserControl.Controls.Add(ucContract);
                    loadPage_EducationContract();
                    break;
                case DTO.contract.HeadOfDepartment:
                    ucContract_HeadOfDepartment = LoadControl("../UserControls/Contract_HeadOfDepartment.ascx") as UserControls.Contract_HeadOfDepartment;
                    plcUserControl.Controls.Add(ucContract_HeadOfDepartment);
                    loadPage_HeadOfDepartmentContract();
                    break;
                case DTO.contract.DeputyGroup:
                    ucContract_DeputyGroup = LoadControl("../UserControls/Contract_DeputyGroup.ascx") as UserControls.Contract_DeputyGroup;
                    plcUserControl.Controls.Add(ucContract_DeputyGroup);
                    loadPage_DeputyContract();
                    break;
            }

        }

        private void loadPage_EducationContract()
        {
            bool hasContract = false;

            ucContract.TeacherCode = Convert.ToInt32(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            ucContract.signature = chbConfirm.Checked;
            ucContract.userType = 1;
            ucContract.term = Session["term"].ToString();
            if (!IsPostBack)
            {
                DataTable dtHR = FRB.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
                if (dtHR.Rows.Count == 1)
                {
                    ViewState[hrID] = dtHR.Rows[0]["ID"].ToString();
                    DataTable dtContract = bsn.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), ucContract.term);
                    if (dtContract.Rows.Count == 1)
                    {
                        if (dtContract.Rows[0]["contractFile"] != DBNull.Value)
                        {
                            hasContract = true;
                        }
                    }
                    if (hasContract)
                    {
                        showMessage("شما قرارداد این ترم را امضا کرده اید و امکان مشاهده دوباره قرارداد برای شما وجود ندارد.");
                        return;
                    }
                    DataTable dtSignature = bsn.getSignature(Convert.ToInt32(ViewState[hrID]), 1);
                    bool hasSignature = false;
                    if (dtSignature.Rows.Count == 1)
                    {
                        if (dtSignature.Rows[0]["signature"] != DBNull.Value)
                        {
                            hasSignature = true;
                        }
                    }
                    if (!hasSignature)
                    {
                        showMessage("استاد گرامی برای شما اسکن امضا در سامانه ثبت نشده است. لطفا با در اختیار داشتن نام کاربری خود، با کارگزینی هیئت علمی به شماره تماس (02142863288) تماس حاصل فرمایید. ");
                        return;
                    }

                }
                else
                {
                    showMessage("شما دسترسی به این قسمت را ندارید");
                    return;
                }
            }
        }
        private void loadPage_HeadOfDepartmentContract()
        {
            bool hasContract = false;

            ucContract_HeadOfDepartment.TeacherCode = Convert.ToInt32(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            ucContract_HeadOfDepartment.signature = chbConfirm.Checked;
            ucContract_HeadOfDepartment.userType = 1;
            ucContract_HeadOfDepartment.year = Session["year"].ToString();
            if (!IsPostBack)
            {
                DataTable dtHR = FRB.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
                if (dtHR.Rows.Count == 1)
                {
                    ViewState[hrID] = dtHR.Rows[0]["ID"].ToString();
                    DataTable dtContract =  bsn.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), ucContract_HeadOfDepartment.year);
                    if (dtContract.Rows.Count == 1)
                    {
                        if (dtContract.Rows[0]["contractFile"] != DBNull.Value)
                        {
                            hasContract = true;
                        }
                    }
                    if (hasContract)
                    {
                        showMessage("شما قرارداد این سال را امضا کرده اید و امکان مشاهده دوباره قرارداد برای شما وجود ندارد.");
                        return;
                    }
                    DataTable dtSignature = bsn.getSignature(Convert.ToInt32(ViewState[hrID]), 1);
                    bool hasSignature = false;
                    if (dtSignature.Rows.Count == 1)
                    {
                        if (dtSignature.Rows[0]["signature"] != DBNull.Value)
                        {
                            hasSignature = true;
                        }
                    }
                    if (!hasSignature)
                    {
                        showMessage("استاد گرامی برای شما اسکن امضا در سامانه ثبت نشده است. لطفا با در اختیار داشتن نام کاربری خود، با کارگزینی هیئت علمی به شماره تماس (02142863288) تماس حاصل فرمایید. ");
                        return;
                    }
                    var dtSalary = PB.getProfessorSalary(Convert.ToInt32(Session["year"].ToString()), Convert.ToInt64(Session[sessionNames.userID_StudentOstad]));
                    bool hasSalary = false;
                    if (dtSalary>0)
                    {
                            hasSalary = true;
                    }
                    if (!hasSalary)
                    {
                        showMessage("استاد گرامی برای شما مبلغ قرارداد برای سال جاری در سامانه خدمات ثبت نشده است. لطفا با در اختیار داشتن نام کاربری خود، با کارگزینی هیئت علمی به شماره تماس (02142863288) تماس حاصل فرمایید. ");
                        return;
                    }

                }
                else
                {
                    showMessage("شما دسترسی به این قسمت را ندارید");
                    return;
                }
            }
        }
        private void loadPage_DeputyContract()
        {
            bool hasContract = false;

            ucContract_DeputyGroup.TeacherCode = Convert.ToInt32(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            ucContract_DeputyGroup.signature = chbConfirm.Checked;
            ucContract_DeputyGroup.userType = 1;
            ucContract_DeputyGroup.year = Session["year"].ToString();
            if (!IsPostBack)
            {
                DataTable dtHR = FRB.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
                if (dtHR.Rows.Count == 1)
                {
                    ViewState[hrID] = dtHR.Rows[0]["ID"].ToString();
                    DataTable dtContract = bsn.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), ucContract_DeputyGroup.year);
                    if (dtContract.Rows.Count == 1)
                    {
                        if (dtContract.Rows[0]["contractFile"] != DBNull.Value)
                        {
                            hasContract = true;
                        }
                    }
                    if (hasContract)
                    {
                        showMessage("شما قرارداد این سال را امضا کرده اید و امکان مشاهده دوباره قرارداد برای شما وجود ندارد.");
                        return;
                    }
                    DataTable dtSignature = bsn.getSignature(Convert.ToInt32(ViewState[hrID]), 1);
                    bool hasSignature = false;
                    if (dtSignature.Rows.Count == 1)
                    {
                        if (dtSignature.Rows[0]["signature"] != DBNull.Value)
                        {
                            hasSignature = true;
                        }
                    }
                    if (!hasSignature)
                    {
                        showMessage("استاد گرامی برای شما اسکن امضا در سامانه ثبت نشده است. لطفا با در اختیار داشتن نام کاربری خود، با کارگزینی هیئت علمی به شماره تماس (02142863288) تماس حاصل فرمایید. ");
                        return;
                    }
                    var dtSalary = PB.getProfessorSalary(Convert.ToInt32(Session["year"].ToString()), Convert.ToInt64(Session[sessionNames.userID_StudentOstad]));
                    bool hasSalary = false;
                    if (dtSalary > 0)
                    {
                        hasSalary = true;
                    }
                    if (!hasSalary)
                    {
                        showMessage("استاد گرامی برای شما مبلغ قرارداد برای سال جاری در سامانه خدمات ثبت نشده است. لطفا با در اختیار داشتن نام کاربری خود، با کارگزینی هیئت علمی به شماره تماس (02142863288) تماس حاصل فرمایید. ");
                        return;
                    }

                }
                else
                {
                    showMessage("شما دسترسی به این قسمت را ندارید");
                    return;
                }
            }
        }
        private void Confirm_EducationContract()
        {
            if (!ucContract.canSign && !string.IsNullOrEmpty(ucContract.incompletedInf))
            {
                string msg = string.Format("{0} {1} {2}", "اطلاعات", ucContract.incompletedInf, "شما دارای نقص میباشد. لطفا با ورود به صفحه ویرایش اطلاعات فردی، اطلاعات خود را تکمیل نموده و سپس اقدام به امضای قرارداد فرمایید");
                showMessage(msg);
                return;
            }
            string s = ucContract.getContentOfContract();
            int contractId;
            if (Session[sessionNames.userID_StudentOstad] != null && Convert.ToInt32(Session[sessionNames.userID_StudentOstad]) != 0 &&
                bsn.insertTeacherContract(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), s, Convert.ToInt32(ViewState[hrID]), ucContract.term, out contractId))
            {
                setLog(contractId, 38, getLogDescription());
                showMessage("قرارداد شما جهت بررسی به کارگزینی فرستاده شد. شما میتوانید در صفحه اصلی قرارداد از مراحل ثبت قرارداد خود مطلع شوید");
            }
            else
            {
                showMessage("در ارسال قرارداد خطایی به وجود آمده است. لطفا مجددا تلاش فرمایید.");
            }
        }
        private void Confirm_HeadOfDepartmentContract()
        {
            if (!ucContract_HeadOfDepartment.canSign && !string.IsNullOrEmpty(ucContract_HeadOfDepartment.incompletedInf))
            {

                string msg = string.Format("{0} {1} {2}", "اطلاعات", ucContract_HeadOfDepartment.incompletedInf, "شما دارای نقص میباشد. لطفا با ورود به صفحه ویرایش اطلاعات فردی، اطلاعات خود را تکمیل نموده و سپس اقدام به امضای قرارداد فرمایید");
                showMessage(msg);
                return;
            }
            string s = ucContract_HeadOfDepartment.getContentOfContract();
            int contractId;
            if (Session[sessionNames.userID_StudentOstad] != null && Convert.ToInt32(Session[sessionNames.userID_StudentOstad]) != 0 &&
                bsn.insertTeacherContract(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), s, Convert.ToInt32(ViewState[hrID]), ucContract_HeadOfDepartment.year, out contractId))
            {
                setLog(contractId, 54, ucContract_HeadOfDepartment.year);
                showMessage("قرارداد شما جهت بررسی به کارگزینی فرستاده شد. شما میتوانید در صفحه اصلی قرارداد از مراحل ثبت قرارداد خود مطلع شوید");
            }
            else
            {
                showMessage("در ارسال قرارداد خطایی به وجود آمده است. لطفا مجددا تلاش فرمایید.");
            }
        }
        private void Confirm_DeputyContract()
        {

            if (!ucContract_DeputyGroup.canSign && !string.IsNullOrEmpty(ucContract_DeputyGroup.incompletedInf))
            {

                string msg = string.Format("{0} {1} {2}", "اطلاعات", ucContract_DeputyGroup.incompletedInf, "شما دارای نقص میباشد. لطفا با ورود به صفحه ویرایش اطلاعات فردی، اطلاعات خود را تکمیل نموده و سپس اقدام به امضای قرارداد فرمایید");
                showMessage(msg);
                return;
            }
            string s = ucContract_DeputyGroup.getContentOfContract();
            int contractId;
            if (Session[sessionNames.userID_StudentOstad] != null && Convert.ToInt32(Session[sessionNames.userID_StudentOstad]) != 0 &&
                bsn.insertTeacherContract(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), s, Convert.ToInt32(ViewState[hrID]), ucContract_DeputyGroup.year, out contractId))
            {
                setLog(contractId, 55, ucContract_DeputyGroup.year);
                showMessage("قرارداد شما جهت بررسی به کارگزینی فرستاده شد. شما میتوانید در صفحه اصلی قرارداد از مراحل ثبت قرارداد خود مطلع شوید");
            }
            else
            {
                showMessage("در ارسال قرارداد خطایی به وجود آمده است. لطفا مجددا تلاش فرمایید.");
            }
        }

        private void showMessage(string msg)
        {
            rwmMain.RadAlert(msg, 300, 100, "پیام سیستم", "RedirectToEditMain");

        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            switch (_typeOfContract)
            {
                case DTO.contract.educationContract:
                    Confirm_EducationContract();
                    break;
                case DTO.contract.HeadOfDepartment:
                    Confirm_HeadOfDepartmentContract();
                    break;
                case DTO.contract.DeputyGroup:
                    Confirm_DeputyContract();
                    break;
            }
        }

        protected void chbConfirm_CheckedChanged(object sender, EventArgs e)
        {
            btnConfirm.Enabled = chbConfirm.Checked;
        }

        private void setLog(int modifyId, int eventID, string description)
        {
            Business.Common.UniversityBusiness UB = new Business.Common.UniversityBusiness();
            string userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            userId = ViewState[hrID].ToString();// Session[sessionNames.userID_StudentOstad].ToString();
            appId = Convert.ToInt32(Session[sessionNames.appID_StudentOstad]);


            commonBSN.InsertIntoStudentLog(userId, DateTime.Now.ToString("HH:mm"), 13, eventID, description, modifyId);
        }


        private string getLogDescription()
        {
            DataTable dt = bsn.getTerm_Contract(ucContract.term);
            string description = "";
            if (dt.Rows.Count == 1)
                description = string.Format("نیم سال {0} سال تحصیلی {1}", dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString());
            return description;
        }

    }
}