using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Dynamic;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class ContractsMain : System.Web.UI.Page
    {
        readonly ProfessorRequestBusiness bsnRequest = new ProfessorRequestBusiness();
        readonly Business.university.CooperationRequest.CooperationRequestBusiness bsnCooperation = new Business.university.CooperationRequest.CooperationRequestBusiness();
        readonly Business.university.Faculty.FacultyReportsBusiness bsnFaculty = new Business.university.Faculty.FacultyReportsBusiness();
        const string hrID = "hrID";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //btnContract_HeadOfDepartment.Visible = false;
                divPendingBox.Visible = false;
                DataTable dtHR = bsnFaculty.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
                if (dtHR.Rows.Count == 1)
                {
                    ViewState[hrID] = dtHR.Rows[0]["ID"].ToString();
                    //if (dtHR.Rows[0]["nahveh_hamk_Sida"] != DBNull.Value)
                    //    btnContract_HeadOfDepartment.Visible = dtHR.Rows[0]["nahveh_hamk_Sida"].ToString() != "3";
                }
                else
                {
                    divPendingBox.Visible = true;
                    divPendingBox.InnerText = "شما اجازه دسترسی به این قسمت را ندارید. لطفا با کارگزینی تماس بگیرید.";
                    return;
                }
            }
        }

        #region Education Contract


        private void setDDlTermSource()
        {
            ddlTerm.DataSource = bsnCooperation.getTermsToContractWithTeachers(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            ddlTerm.DataValueField = "val";
            ddlTerm.DataTextField = "term";
            ddlTerm.DataBind();
        }

        /// <summary>
        /// to show status od contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContract_Click(object sender, EventArgs e)
        {

            if (canSignEducationContract())
            {
                Session[sessionNames.appID_StudentOstad] = 13;
                LoadPage_EducationContract();
                dvSelectContract.Visible = false;
                setDivVisible(DTO.contract.educationContract);
            }
        }

        private bool canSignEducationContract()
        {
            Business.university.Faculty.FacultyReportsBusiness bsnFaculty = new Business.university.Faculty.FacultyReportsBusiness();

            DataTable dtMartabe;
            dtMartabe = bsnFaculty.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));

            if (dtMartabe.Rows.Count == 1)
            {
                if (dtMartabe.Rows[0]["nahveh_hamk_Sida"] != DBNull.Value && Convert.ToInt32(dtMartabe.Rows[0]["nahveh_hamk_sida"]) != 0)
                {
                    if (Convert.ToInt32(dtMartabe.Rows[0]["nahveh_hamk_Sida"]) != 3)
                    {
                        showMessage("استاد گرامی، با توجه به عضویت شما در هیئت علمی دانشگاه، نیازی به تمدید قرارداد آموزشی به صورت الکترونیکی از طرف شما نمی باشد. ");

                        return false;
                    }
                }
                else
                {
                    showMessage("استاد گرامی نحوه همکاری شما با دانشگاه نامشخص است. لطفا با کارگزینی هیئت علمی تماس حاصل فرمایید. ");
                    return false;
                }
            }
            else
            {
                showMessage("استاد گرامی ورود شما به سامانه قرارداد با خطا همراه است. لطفا با کارگزینی هیئت علمی تماس حاصل فرمایید. ");
                return false;
            }
            return true;
        }

        private void LoadPage_EducationContract()
        {
            ddlTerm.Visible = true;
            btnShowContract.Visible = true;
            divPendingBox.Visible = false;
            setDDlTermSource();
            if (bsnRequest.HasPendingRequest(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), (int)DTO.University.Request.RequestTypeId.EditContactInfo) ||
                bsnRequest.HasPendingRequest(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), (int)DTO.University.Request.RequestTypeId.EditPersonalInfo))
            {
                string msg = "  شما به دلیل وجود درخواست درحال بررسی اطلاعات فردی و یا اطلاعات تماس، امکان مشاهده قرارداد را ندارید.";
                divPendingMessage.InnerText = msg;
                divPendingBox.Visible = true;
                btnShowContract.Visible = false;

                //return;
            }
            if (ddlTerm.Items.Count == 0)
            {
                divRejectMessage_Contract.InnerText = "استاد گرامی شما از ترم 1-97-96 تا کنون، درسی اخذ نکرده اید. این قرارداد صرفا برای اساتیدی است که در این بازه درس اخذ کرده اند.";
                divRejectBox_Contract.Visible = true;
                ddlTerm.Visible = false;
                btnShowContract.Visible = false;
                return;
            }
            LoadEducationContractStatus(ddlTerm.SelectedItem.Text);
        }


        private List<object> GetrptProgressContractSource(int step)
        {
            if (ddlTerm.SelectedItem.Value == "-1")
                return null;
            var dummyData = new List<object>();
            //for (int i = 0; i < 1; i++)
            //{
            DataTable dt = bsnCooperation.getTerm_Contract();
            string term = ddlTerm.SelectedItem.Text;
            string sal, nimsal = "نامشخص";
            sal = term.Substring(0, term.LastIndexOf("-"));
            switch (term.Substring(term.LastIndexOf("-") + 1))
            {
                case "1":
                    nimsal = "اول";
                    break;
                case "2":
                    nimsal = "دوم";
                    break;
                case "3":
                    nimsal = "سوم";
                    break;
            }
            if (dt.Rows.Count > 0)
            {
                string invertYear;//= string.Format("{0}-{1}", dt.Rows[0]["sal"].ToString().Substring(dt.Rows[0]["sal"].ToString().IndexOf("-")+1), dt.Rows[0]["sal"].ToString().Substring(0, dt.Rows[0]["sal"].ToString().IndexOf("-")));
                invertYear = string.Format("{0}-{1}", sal.Substring(dt.Rows[0]["sal"].ToString().IndexOf("-") + 1), sal.Substring(0, dt.Rows[0]["sal"].ToString().IndexOf("-")));
                dynamic data = new ExpandoObject();
                data.Progress = (step * 33.3) + 16.5;
                data.TermTitle = string.Format("{0} {1} {2} {3}", "نیم سال", nimsal, "سال تحصیلی", invertYear);// "ترم " + "96-97-1";// "ترم " + "96-97-" ;
                data.Step = step + 1;
                dummyData.Add(data);
            }
            //}
            return dummyData;
        }

        protected string GetCssClass_Contract(int step)
        {
            DataTable dt = bsnCooperation.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), ddlTerm.SelectedItem.Text);

            if (dt.Rows.Count > 0)
            {
                divPendingBox.Visible = false;
                int status = Convert.ToInt32(dt.Rows[0]["status"]);
                switch (status)
                {
                    case 0:
                        if (step == 1)
                        {

                            return "f1Contract-step active";
                        }
                        break;
                    case 1:
                    case 2:
                        if (step == 2)
                        {
                            return "f1Contract-step active";
                        }
                        else if (step < 2)
                            return "f1Contract-step activated";
                        break;
                    case 3:
                        if (step == 3)
                        {

                            return "f1Contract-step active";
                        }
                        else if (step < 3)
                            return "f1Contract-step activated";
                        break;
                }
            }


            //var currentStep = int.Parse(data.ToString());
            //if (currentStep == step)
            //    return "f1-step active";
            //if(step < currentStep)
            //    return "f1-step activated";
            return "f1Contract-step";

        }
        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEducationContractStatus(ddlTerm.SelectedItem.Text);
        }

        private void LoadEducationContractStatus(string term)
        {

            divRejectBox_Contract.Visible = false;
            btnShowContract.Visible = true && divPendingMessage.InnerText=="";
            DataTable dt = bsnCooperation.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), term);
            int status = 0;
            if (dt.Rows.Count > 0)
            {
                btnShowContract.Visible = false;
                status = Convert.ToInt32(dt.Rows[0]["status"]);
                if (status == 0)
                    status = 1;
                else if (status == 1 || status == 2)
                    status = 2;
            }
            else
            {
                DataTable dtReject = bsnCooperation.getContractByStatus(4, term);
                DataRow[] drReject = dtReject.Select("hrID=" + ViewState[hrID].ToString());

                divRejectBox_Contract.Visible = drReject.Length > 0;
                if (drReject.Length > 0)
                    divRejectMessage_Contract.InnerText += "علت رد: " + drReject[0]["description"].ToString();
            }
            rptProgressContract.DataSource = GetrptProgressContractSource(status);
            rptProgressContract.DataBind();

        }

        /// <summary>
        /// show text of contract to sign
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowContract_Click(object sender, EventArgs e)
        {
            if (ddlTerm.SelectedItem.Value == "-1")
                return;
            Session["term"] = ddlTerm.SelectedItem.Text;
            Session["contractType"] = DTO.contract.educationContract;
            Response.Redirect(url: "~/University/CooperationRequest/Teachers/confirmcontract.aspx");
        }


        #endregion Education Contract


        #region HOD Contract

        private void setDdlYearSource()
        {
            var dt=bsnCooperation.getYearToSigncontract_HOD(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            ddlYear.DataSource = dt;
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
        protected string GetCssClass_ContractHOD(int step)
        {
            DataTable dt = bsnCooperation.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), ddlYear.SelectedItem.Text);

            if (dt.Rows.Count > 0)
            {
                divPendingBox.Visible = false;
                int status = Convert.ToInt32(dt.Rows[0]["status"]);
                switch (status)
                {
                    case 0:
                        if (step == 1)
                        {

                            return "f1ContractHOD-step active";
                        }
                        break;
                    case 1:
                    case 2:
                        if (step == 2)
                        {
                            return "f1ContractHOD-step active";
                        }
                        else if (step < 2)
                            return "f1ContractHOD-step activated";
                        break;
                    case 3:
                        if (step == 3)
                        {

                            return "f1ContractHOD-step active";
                        }
                        else if (step < 3)
                            return "f1ContractHOD-step activated";
                        break;
                }
            }


            //var currentStep = int.Parse(data.ToString());
            //if (currentStep == step)
            //    return "f1-step active";
            //if(step < currentStep)
            //    return "f1-step activated";
            return "f1ContractHOD-step";

        }

        /// <summary>
        /// to show status of HOD Contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnContract_HeadOfDepartment_Click(object sender, EventArgs e)
        {
            if (canSignEducationContract())
            {
                Session[sessionNames.appID_StudentOstad] = 13;
                LoadPage_HODContract();
                dvSelectContract.Visible = false;
                setDivVisible(DTO.contract.HeadOfDepartment);
            }
        }

        private void LoadPage_HODContract()
        {
            ddlYear.Visible = true;
            btnShowContractHOD.Visible = true;
            divPendingBoxHOD.Visible = false;
            divRejectBox_ContractHOD.Visible = false;
            setDdlYearSource();
            if (bsnRequest.HasPendingRequest(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), (int)DTO.University.Request.RequestTypeId.EditContactInfo) ||
                bsnRequest.HasPendingRequest(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]), (int)DTO.University.Request.RequestTypeId.EditPersonalInfo))
            {
                string msg = "  شما به دلیل وجود درخواست درحال بررسی اطلاعات فردی و یا اطلاعات تماس، امکان مشاهده قرارداد را ندارید.";
                divPendingMessageHOD.InnerText = msg;
                divPendingBoxHOD.Visible = true;
                btnShowContractHOD.Visible = false;

                //return;
            }
            if (ddlYear.Items.Count == 0)
            {
                divRejectMessage_ContractHOD.InnerText = "استاد گرامی شما از سال 1399 تا کنون،کد کاربری شما به عنوان مدیر گروه یا معاون گروه ثبت نشده است . این قرارداد صرفا برای اساتیدی است که در این بازه مدیر گروه و یا معاون گروه بوده اند.";
                divRejectBox_ContractHOD.Visible = true;
                ddlYear.Visible = false;
                btnShowContractHOD.Visible = false;
                return;
            }
            /*بررسی مدیر یا معاون گروه بودن استاد
             * 
             */
            LoadHODContractStatus(ddlYear.SelectedItem.Text);
        }
        private void LoadHODContractStatus(string term)
        {

            divRejectBox_ContractHOD.Visible = false;
            btnShowContractHOD.Visible = true && divPendingMessage.InnerText=="";
            DataTable dt=bsnCooperation.getContractOfTeacher(Convert.ToInt32(ViewState[hrID]), term);
            int status = 0;
            if (dt.Rows.Count > 0)
            {
                btnShowContractHOD.Visible = false;
                status = Convert.ToInt32(dt.Rows[0]["status"]);
                if (status == 0)
                    status = 1;
                else if (status == 1 || status == 2)
                    status = 2;
            }
            else
            {
                DataTable dtReject = bsnCooperation.getContractByStatus(4, term);
                DataRow[] drReject = dtReject.Select("hrID=" + ViewState[hrID].ToString());

                divRejectBox_ContractHOD.Visible = drReject.Length > 0;
                if (drReject.Length > 0)
                    divRejectMessage_ContractHOD.InnerText += "علت رد: " + drReject[0]["description"].ToString();
            }
            rptProgressContractHOD.DataSource = GetrptProgressHODContractSource(status);
            rptProgressContractHOD.DataBind();

        }
        private List<object> GetrptProgressHODContractSource(int step)
        {
            if (ddlYear.SelectedItem.Value == "-1")
                return null;
            var dummyData = new List<object>();
            //for (int i = 0; i < 1; i++)
            //{
            DataTable dt = new DataTable();//bsnCooperation.getTerm_Contract();
            string year = ddlYear.SelectedItem.Text;
            
            //if (dt.Rows.Count > 0)
            {
                dynamic data = new ExpandoObject();
                data.Progress = (step * 33.3) + 16.5;
                data.TermTitle = string.Format("{0} {1}", "قرارداد سال", year);// "ترم " + "96-97-1";// "ترم " + "96-97-" ;
                data.Step = step + 1;
                dummyData.Add(data);
            }
            //}
            return dummyData;
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHODContractStatus(ddlYear.SelectedItem.Value);

        }

        protected void btnShowContractHOD_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(DateTime.Now.ToPeString().Substring(0, 4));
            Business.Adobe.ProfPresentBusiness pb = new Business.Adobe.ProfPresentBusiness();

            var HeyatElmi = pb.getActiveGroupManager(year, Convert.ToInt64(Session[sessionNames.userID_StudentOstad]));
            if (HeyatElmi.Rows.Count > 0)
            {
                bool isManager = Convert.ToBoolean(HeyatElmi.Rows[0]["isManager"]);
                Session["year"] = ddlYear.SelectedItem.Text;
                switch (isManager)
                {
                    case true:
                        Session["contractType"] = DTO.contract.HeadOfDepartment;

                        break;
                    case false:
                        Session["contractType"] = DTO.contract.DeputyGroup;

                        break;
                }
                Response.Redirect(url: "~/University/CooperationRequest/Teachers/confirmcontract.aspx");
            }
        }


        #endregion HOD Contract



        #region Agreement

        protected void btnAgreement_Click(object sender, EventArgs e)
        {
            if (canSignAgreement())
            {
                dvSelectContract.Visible = false;
                dvAgreement.Visible = true;
                dvContract.Visible = false;
                LoadPage_Agreement();
            }

        }
        private bool canSignAgreement()
        {
            var ostInf = bsnFaculty.getOstadInfoFromPortal(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            if (ostInf.codeOstad == 0)
            {
                showMessage("استاد گرامی، کد استادی که با آن به سامانه وارد شده اید در سامانه پرتال پژوهشی یافت نشد. ");
                return false;
            }

            DataTable dtMartabe;
            dtMartabe = bsnFaculty.GetOstadInfoFromHR(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));

            if (dtMartabe.Rows.Count == 1)
            {
                if (dtMartabe.Rows[0]["nahveh_hamk_Sida"] != DBNull.Value && Convert.ToInt32(dtMartabe.Rows[0]["nahveh_hamk_Sida"]) != 3)
                {
                    showMessage("استاد گرامی، با توجه به عضویت شما در هیئت علمی دانشگاه، نیازی به امضای تفاهم نامه پژوهشی به صورت الکترونیکی از طرف شما نمی باشد. ");
                    return false;

                }
            }
            return true;
        }

        private void LoadPage_Agreement()
        {
            DataTable dt = bsnCooperation.getAgreementOfTeacher(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));
            int status = 0;

            //
            if (dt.Rows.Count > 0)
            {
                divRejectBox_Agreement.Visible = false;
                status = Convert.ToInt32(dt.Rows[0]["status"]);
                btnShowAgreement.Visible = false;
            }

            else
            {
                DataTable dtReject = bsnCooperation.getAgreementByStatus(3);
                DataRow[] drReject = dtReject.Select("code_ostad=" + Session[sessionNames.userID_StudentOstad].ToString());

                divRejectBox_Agreement.Visible = drReject.Length > 0;
                btnShowAgreement.Visible = drReject.Length > 0;
                if (drReject.Length > 0)
                    divRejectMessage_Agreement.InnerText += "علت رد: " + drReject[0]["description"].ToString();
                else
                    Response.Redirect(url: "~/University/CooperationRequest/Teachers/confirmAgreement.aspx");

            }
            LoadAgreementStatus(status);
        }

        private void LoadAgreementStatus(int status)
        {
            if (status == 0)
                status = 1;
            else if (status == 1)
                status = 2;

            rptProgressAgreement.DataSource = GetrptProgressAgreementSource(status);
            rptProgressAgreement.DataBind();
        }

        protected string GetCssClass_Agreement(int step)
        {
            DataTable dt = bsnCooperation.getAgreementOfTeacher(Convert.ToInt32(Session[sessionNames.userID_StudentOstad]));

            if (dt.Rows.Count > 0)
            {
                int status = Convert.ToInt32(dt.Rows[0]["status"]);
                switch (status)
                {
                    case 0:
                        if (step == 1)
                        {

                            return "f1Agreement-step active";
                        }
                        break;
                    case 1:
                        if (step == 2)
                        {
                            return "f1Agreement-step active";
                        }
                        else if (step < 2)
                            return "f1Agreement-step activated";
                        break;
                    case 2:
                        if (step == 3)
                        {

                            return "f1Agreement-step active";
                        }
                        else if (step < 3)
                            return "f1Agreement-step activated";
                        break;
                }
            }


            //var currentStep = int.Parse(data.ToString());
            //if (currentStep == step)
            //    return "f1-step active";
            //if(step < currentStep)
            //    return "f1-step activated";
            return "f1Agreement-step";

        }

        private List<object> GetrptProgressAgreementSource(int step)
        {
            var dummyData = new List<object>();

            dynamic data = new ExpandoObject();
            data.Progress = (step * 33.3) + 16.5;
            data.TermTitle = "وضعیت تایید تفاهم نامه";
            data.Step = step;
            dummyData.Add(data);

            //}
            return dummyData;
        }

        protected void btnShowAgreement_Click(object sender, EventArgs e)
        {
            Response.Redirect(url: "~/University/CooperationRequest/Teachers/confirmAgreement.aspx");
        }


        #endregion Agreement


        private void showMessage(string msg)
        {
            rwm_message.RadAlert(msg, null, 100, "پیام", "");

        }

        private void setDivVisible(string type)
        {
            dvAgreement.Visible = type== DTO.contract.agreement;
            dvContract.Visible = type== DTO.contract.educationContract;
            dvContract_Department.Visible = type== DTO.contract.HeadOfDepartment;

        }

    
    }
}