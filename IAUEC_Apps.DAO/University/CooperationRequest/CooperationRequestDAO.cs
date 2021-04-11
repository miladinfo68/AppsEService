using IAUEC_Apps.DAC.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.CooperationRequest
{
    public class CooperationRequestDAO
    {
        SqlConnection con = new SqlConnection(new HrConnection().HR_con);
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        #region Read

        public DataTable getEmploymentActionHistory()
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "sp_getEmploymentActionHistory";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

            }
            return dt;
        }
        public DataTable getProfessorsByModifyType(int eventID,string fromDate,string toDate)
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "sp_getProfessorsByModifyType";
            cmd.Parameters.AddWithValue("@event", eventID);
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@toDate", toDate);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

            }
            return dt;
        }


        public DataTable getTeachersHaveNotPersonalImage()
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "sp_getTeacherHaveNotPersonalImage";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

            }
            return dt;
        }

        public DataTable getTeachersHaveNotPersonalImage_CantUpload()
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandText = "sp_GetCantUploadPersonalyImage";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

            }
            return dt;
        }

        public DataTable GetRequestAccept()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ReportRequestAccept]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetResearchRequestAccept()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ReportResearchRequestAccept]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetResearchRequestReject()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ReportResearchRequsetReject]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetResearchRequestPending()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_ReportResearchRequestPending";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetNameMadarek()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_DocName]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetRequestReject()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_ReportRequsetReject]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetRequestEditing()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_RequestEditing]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetRequestPending()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_RequestPending";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetRequestScanDoc(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_ScanDocID";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@ID", ID);
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetNameScanDoc(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_DocNameByID]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@ID", ID);
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetInfoPeoByCodeMeli(string codemeli)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_GetInfoPeoByCodeMeli";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@codemeli", codemeli);
            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetInfoPeoByCodeMeliAndFamily(string codemeli, string family)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[HR].[SP_GetInfoPeoByCodeMeliAndName]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@codemeli", codemeli);
            cmd.Parameters.AddWithValue("@family", family);

            try
            {
                SqlDataReader rdr;
                con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch
            {

                if (con.State == ConnectionState.Open)
                    con.Close();
                throw;
            }
            if (con.State == ConnectionState.Open)
                con.Close();
            return dt;
        }

        public DataTable getAllHokmInfo()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "hr.SP_getAllHokmInfo";
            cmd.Connection = con;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader sdr;
                con.Open();
                sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw;
            }
            return dt;
        }

        public void updateHokmSeenStatus(int requestId, int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "hr.SP_updateSeenStatusHokmInfo";
            cmd.Parameters.AddWithValue("@reqId", requestId);
            cmd.Parameters.AddWithValue("@status", status);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                throw;
            }
        }

        public DataTable GetAllControlToSidaFields()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetAllControlToSidaFields";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();

            try
            {
                SqlDataReader rdr;
                conn.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        //public DataTable GetOstadInfoFromSida(int codeostad)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandText = "HR.SP_GetInfoPeoByCodeMeli";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    DataTable dt = new DataTable();
        //    cmd.Parameters.AddWithValue("@codemeli", codemeli);
        //    try
        //    {
        //        SqlDataReader rdr;
        //        con.Open();
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        con.Close();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return dt;
        //}

        public DataTable getTeacherSignature(int hrID)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_getTeacherSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@hrID", hrID);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public DataTable getSignaturesStatus()
        {
            DataTable dt = new DataTable();
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.sp_getAllSignatureStatus";
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public DataTable getAllContractStatus(string term)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.sp_getAllContractsStatus";
            cmn.Parameters.AddWithValue("@term", term);

            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();


            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }
        public DataTable getAllContractStatus_HOD(string year)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.sp_getAllHODContractsStatus";
            cmn.Parameters.AddWithValue("@year", year);

            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();


            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getAllAgreementsStatus()
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.sp_getAllAgreementsStatus";

            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();


            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getTermsToContractWithTeachers(int code_Ostad)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[Faculty].[getTermsToContractWithTeachers]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@codeOstad", code_Ostad);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return dt;
        }
        public DataTable getYearToSigncontract_HOD(int codeOstad)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[Faculty].[sp_getYearOfSignedContracts_Teacher]";
            cmn.Parameters.AddWithValue("@code_ostad", codeOstad);
            cmn.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return dt;
        }

        public DataTable getYearsOfHodContract()
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[Faculty].[sp_getYearOfSignedContracts]";
            cmn.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return dt;
        }

        public DataTable getbeginAndEndWorkTimeHOD(Int64 codeOstad ,int year)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[Faculty].[sp_getBeginAndEndWorkTimeHOD]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@codeOstad", codeOstad);
            cmn.Parameters.AddWithValue("@year", year);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

            }
            return dt;
        }

        public DataTable getUserSignature(int userRole)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_getUserSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@userRole", userRole);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = comm.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getTeacherContract(int codeOstad, string term)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.SP_getContracts";
            cmn.Parameters.AddWithValue("@term", term);
            cmn.Parameters.AddWithValue("@teacherCode", codeOstad);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getTeacherAgreement(Int64 codeOstad)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.sp_getTeacherAgreement";
            cmn.Parameters.AddWithValue("@codeOstad", codeOstad);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getContractByStatus(string term, int status)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.SP_getContractsByStatus";
            cmn.Parameters.AddWithValue("@term", term);
            cmn.Parameters.AddWithValue("@status", status);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public DataTable getAgreementByStatus(int status)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.SP_getAgreementsByStatus";
            cmn.Parameters.AddWithValue("@status", status);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getBlacklistTeachers()
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_getblacklistTeacher";
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

            }
            return dt;

        }
        #endregion Read

        #region Write

        public bool insertTeacherContract(int codeOstad, string contractFile, int hrID, string term,out int contractId)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.SP_insertNewContract";
            cmn.Parameters.AddWithValue("@contract", contractFile);
            cmn.Parameters.AddWithValue("@teacherCode", codeOstad);
            cmn.Parameters.AddWithValue("@hrID", hrID);
            cmn.Parameters.AddWithValue("@term", term);
            bool successful = false;
            contractId = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                contractId = Convert.ToInt32(cmn.ExecuteScalar());
                conn.Close();
                successful = contractId > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return successful;
        }

        public bool insertTeacherAgreement(int codeOstad, string agreementFile, int hrID,  out int agreementId)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.SP_insertNewAgreement";
            cmn.Parameters.AddWithValue("@agreement", agreementFile);
            cmn.Parameters.AddWithValue("@teacherCode", codeOstad);
            cmn.Parameters.AddWithValue("@hrID", hrID);
            bool successful = false;
            agreementId = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                agreementId = Convert.ToInt32(cmn.ExecuteScalar());
                conn.Close();
                successful = agreementId > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return successful;
        }

        public bool UpdateTeacherContractAfterReject(string term, string contractFile, int hrID, out int contractId)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.UpdateContractOnRejectedContract";
            cmn.Parameters.AddWithValue("@contractFile", contractFile);
            cmn.Parameters.AddWithValue("@term", term);
            cmn.Parameters.AddWithValue("@hrID", hrID);
            bool successful = false;
            contractId = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                contractId =Convert.ToInt32( cmn.ExecuteScalar());
                conn.Close();
                successful = contractId > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return successful;
        }

        public bool UpdateTeacherAgreementAfterReject(string AgreementFile, int hrID, out int AgreementId)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.UpdateAgreementOnRejectedAgreement";
            cmn.Parameters.AddWithValue("@AgreementFile", AgreementFile);
            cmn.Parameters.AddWithValue("@hrID", hrID);
            bool successful = false;
            AgreementId = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                AgreementId = Convert.ToInt32(cmn.ExecuteScalar());
                conn.Close();
                successful = AgreementId > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return successful;
        }

        public string getTerm_Contract(string term)
        {
            string retTerm = term;
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "select faculty.Func_GetTermJary_Contract(@term)";
            cmn.Parameters.AddWithValue("@term", term);
            cmn.CommandType = CommandType.Text;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                retTerm = cmn.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return retTerm;
        }

        public bool updateTeacherContractStatus(string term, int codeOstad, int newStatus, string contractFile, string description = "")
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "faculty.SP_UpdateContractStatus";
            cmn.Parameters.AddWithValue("@term", term);
            cmn.Parameters.AddWithValue("@teacherCode", codeOstad);
            cmn.Parameters.AddWithValue("@contractFile", contractFile);
            cmn.Parameters.AddWithValue("@status", newStatus);
            cmn.Parameters.AddWithValue("@description", description);
            bool successful = false;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                successful = cmn.ExecuteNonQuery() > 0;
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return successful;
        }

        public bool updateTeacherAgreementStatus(Int64 codeOstad, int newStatus, string agreementFile,out int agreementID, string description = "")
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.CommandText = "Faculty.SP_UpdateAgreementStatus";
            cmn.Parameters.AddWithValue("@teacherCode", codeOstad);
            cmn.Parameters.AddWithValue("@agreementFile", agreementFile);
            cmn.Parameters.AddWithValue("@status", newStatus);
            cmn.Parameters.AddWithValue("@description", description);
            bool successful = false;
            agreementID = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                agreementID = Convert.ToInt32(cmn.ExecuteScalar());
                conn.Close();
                successful = agreementID > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return successful;
        }

        public bool InsertTeacherSignature(int codeOstad, string signature, int hrID)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_InsertTeacherSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@codeOstad", codeOstad);
            comm.Parameters.AddWithValue("@signature", signature);
            comm.Parameters.AddWithValue("@hrID", hrID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }

        public bool InsertUserSignature(int userCode, string signature, int userRole)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_InsertUserSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@userCode", userCode);
            comm.Parameters.AddWithValue("@signature", signature);
            comm.Parameters.AddWithValue("@userRole", userRole);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }

        public bool updateTeacherSignature(int hrID, string signature)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_UpdateTeacherSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@hrID", hrID);
            comm.Parameters.AddWithValue("@signature", signature);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }

        public bool updateUserSignature(int userCode, string signature, int userRole)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_UpdateUserSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@userCode", userCode);
            comm.Parameters.AddWithValue("@signature", signature);
            comm.Parameters.AddWithValue("@userRole", userRole);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }
        public bool deleteSignature(Int64 userCode, int userRole)
        {
            SqlCommand comm = new SqlCommand("", conn);
            comm.CommandText = "faculty.SP_deleteSignature";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@userCode", userCode);
            comm.Parameters.AddWithValue("@userRole", userRole);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            return true;
        }

        public void UpdateInfoPeopleStatus(string UserName, string Status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "HR.SP_UpdateInfoPeopleStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Status", Status);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
                throw;
            }
        }

        public int updateBlacklistTeacher(string idd_meli, bool inBlacklist)
        {
            SqlCommand cmd = new SqlCommand("", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_updateTeacherBlacklist";
            cmd.Parameters.AddWithValue("@idd_meli", idd_meli);
            cmd.Parameters.AddWithValue("@blacklist", inBlacklist);
            int id = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var result = cmd.ExecuteScalar();
                id = result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

            }
            return id;
        }

        #endregion



    }
}
