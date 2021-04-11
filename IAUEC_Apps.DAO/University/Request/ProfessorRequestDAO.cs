using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.University.Request
{
    public class ProfessorRequestDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection hrConnection = new SqlConnection(new HrConnection().HR_con);

        #region write
        public int AddNewEditRequest(ProfessorEditRequestDTO oReqDto)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_AddNewProfessorEditRequest";
            cmds.Parameters.AddWithValue("@Code_Ostad", oReqDto.Code_Ostad);
            cmds.Parameters.AddWithValue("@RequestTypeID", oReqDto.RequestTypeID);
            cmds.Parameters.AddWithValue("@RequestLogID", oReqDto.RequestLogID);
            cmds.Parameters.AddWithValue("@CreateDate", oReqDto.Createdate);
            cmds.Parameters.AddWithValue("@Note", oReqDto.Note);
            cmds.Parameters.AddWithValue("@ProfessorMessage", oReqDto.ProfessorMessage);
            cmds.Parameters.AddWithValue("@Erae_Be", oReqDto.Erae_Be);
            cmds.Parameters.AddWithValue("@ChangeSet", oReqDto.ChangeSet);
            cmds.Parameters.AddWithValue("@isdeleted", oReqDto.isDeleted);
            cmds.Parameters.AddWithValue("@ScanImageUrl", oReqDto.ScanImageUrl);
            cmds.Parameters.AddWithValue("@HR_InfoPeople_Id", oReqDto.HR_InfoPeople_Id);

            try
            {
                conn.Open();
                int reqId = int.Parse(cmds.ExecuteScalar().ToString());
                conn.Close();
                return reqId;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddNewEditRequestScan(int docType, byte[] scanImage, string ScanUrl, int code_Ostad, int reqId)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_AddNewProfessorEditRequest_Doc";
            cmds.Parameters.AddWithValue("DocType", docType);
            cmds.Parameters.AddWithValue("ScanImage", scanImage);
            cmds.Parameters.AddWithValue("ScanUrl", ScanUrl);
            cmds.Parameters.AddWithValue("codeOstad", code_Ostad);
            cmds.Parameters.AddWithValue("reqId", reqId);

            try
            {
                conn.Open();
               cmds.ExecuteScalar();
                conn.Close();
            }
            catch (Exception exx)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public DataTable getScanOfProfessor(int codeOstad,int docStatus,int reqID)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_GetProfessorRequests_Scan";
            cmds.Parameters.AddWithValue("@docStatus", docStatus);
            cmds.Parameters.AddWithValue("@codeOstad", codeOstad);
            cmds.Parameters.AddWithValue("@reqID", reqID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmds.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception exx)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        public DataTable getScanOfProfessor(int requestId)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_GetProfessorRequestsByReqId_Scan";
            cmds.Parameters.AddWithValue("@reqId", requestId);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmds.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception exx)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }



        public void AddChangeList(int reqId, IList<ChangedInfoDTO> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ProfessorRequestId = reqId;
                list[i].State = FieldChangeState.Submitted;
            }

            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_AddNewChangeList";
            DataTable table = ConvertListToDataTable(list);
            SqlParameter param = new SqlParameter("@ChangeList", table);
            param.SqlDbType = SqlDbType.Structured;
            cmds.Parameters.Add(param);

            try
            {
                conn.Open();
                int a = cmds.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddNewHokm(ProfessorHokmDTO oProfessorHokmDTO)
        {

            SqlCommand cmds = new SqlCommand();
            cmds.Connection = hrConnection;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "HR.SP_InsertHokmInfo";
            cmds.Parameters.AddWithValue("@InfoPeopleId", oProfessorHokmDTO.InfoPeopleId);
            cmds.Parameters.AddWithValue("@code_ostad", oProfessorHokmDTO.Code_Ostad);
            cmds.Parameters.AddWithValue("@HokmUrl", (object)oProfessorHokmDTO.HokmUrl ?? DBNull.Value);
            cmds.Parameters.AddWithValue("@MablaghHokm", oProfessorHokmDTO.MablaghHokm);
            cmds.Parameters.AddWithValue("@number_hokm", oProfessorHokmDTO.Number_Hokm);
            cmds.Parameters.AddWithValue("@date_runhokm", oProfessorHokmDTO.Date_RunHokm);
            cmds.Parameters.AddWithValue("@date_hokm", oProfessorHokmDTO.Date_Hokm);
            cmds.Parameters.AddWithValue("@payeh", oProfessorHokmDTO.Payeh);
            cmds.Parameters.AddWithValue("@type_estekhdam", oProfessorHokmDTO.Type_Estekhdam);
            cmds.Parameters.AddWithValue("@uni_khedmat", oProfessorHokmDTO.Uni_Khedmat);
            cmds.Parameters.AddWithValue("@uni_khedmatType", oProfessorHokmDTO.Uni_KhedmatType);
            cmds.Parameters.AddWithValue("@nahveh_hamk", oProfessorHokmDTO.Nahveh_Hamk);
            cmds.Parameters.AddWithValue("@IsRetired", oProfessorHokmDTO.IsRetired);
            cmds.Parameters.AddWithValue("@DateUpload", oProfessorHokmDTO.DateUpload);
            cmds.Parameters.AddWithValue("@State", oProfessorHokmDTO.State);
            cmds.Parameters.AddWithValue("@Martabeh", oProfessorHokmDTO.Martabeh);
            cmds.Parameters.AddWithValue("@EditRequestId", oProfessorHokmDTO.EditRequestId);
            cmds.Parameters.AddWithValue("@BoundHour", oProfessorHokmDTO.BoundHour);

            try
            {
                hrConnection.Open();
                int reqId = cmds.ExecuteNonQuery();
                hrConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ApproveNewHokm(ProfessorHokmDTO oHokm)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = hrConnection;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "HR.SP_ApproveNewHokm";
            cmds.Parameters.AddWithValue("@HokmId", oHokm.HokmId);
            cmds.Parameters.AddWithValue("@MablaghHokm", oHokm.MablaghHokm);
            cmds.Parameters.AddWithValue("@number_hokm", oHokm.Number_Hokm);
            cmds.Parameters.AddWithValue("@date_runhokm", oHokm.Date_RunHokm);
            cmds.Parameters.AddWithValue("@date_hokm", oHokm.Date_Hokm);
            cmds.Parameters.AddWithValue("@payeh", oHokm.Payeh);
            cmds.Parameters.AddWithValue("@type_estekhdam", oHokm.Type_Estekhdam);
            cmds.Parameters.AddWithValue("@uni_khedmat", oHokm.Uni_Khedmat);
            cmds.Parameters.AddWithValue("@uni_khedmatType", oHokm.Uni_KhedmatType);
            cmds.Parameters.AddWithValue("@nahveh_hamk", oHokm.Nahveh_Hamk);
            cmds.Parameters.AddWithValue("@IsRetired", oHokm.IsRetired);
            cmds.Parameters.AddWithValue("@State", oHokm.State);
            cmds.Parameters.AddWithValue("@Martabeh", oHokm.Martabeh);
            cmds.Parameters.AddWithValue("@DateRunHokmHere", oHokm.DateRunHokmHere);
            cmds.Parameters.AddWithValue("@BoundHour", oHokm.BoundHour?1:0);

            try
            {
                hrConnection.Open();
                int reqId = cmds.ExecuteNonQuery();
                hrConnection.Close();
                return reqId;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public bool updateHokmInThreeTables(ProfessorHokmDTO oHokm)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = hrConnection;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "dbo.sp_UpdateHokmInThreeTables";
            cmds.Parameters.AddWithValue("@MablaghHokm", oHokm.MablaghHokm);
            cmds.Parameters.AddWithValue("@number_hokm", oHokm.Number_Hokm);
            cmds.Parameters.AddWithValue("@date_runhokm", oHokm.Date_RunHokm);
            cmds.Parameters.AddWithValue("@date_hokm", oHokm.Date_Hokm);
            cmds.Parameters.AddWithValue("@payeh", oHokm.Payeh);
            cmds.Parameters.AddWithValue("@type_estekhdam", oHokm.Type_Estekhdam);
            cmds.Parameters.AddWithValue("@uni_khedmat", oHokm.Uni_Khedmat);
            cmds.Parameters.AddWithValue("@uni_khedmatType", oHokm.Uni_KhedmatType);
            cmds.Parameters.AddWithValue("@nahveh_hamk", oHokm.Nahveh_Hamk);
            cmds.Parameters.AddWithValue("@IsRetired", oHokm.IsRetired);
            cmds.Parameters.AddWithValue("@Martabeh", oHokm.Martabeh);
            cmds.Parameters.AddWithValue("@BoundHour", oHokm.BoundHour ? 1 : 0);
            cmds.Parameters.AddWithValue("@hrID", oHokm.InfoPeopleId);
            cmds.Parameters.AddWithValue("@codeOstad", oHokm.Code_Ostad);

            try
            {
                hrConnection.Open();
                int count = cmds.ExecuteNonQuery();
                hrConnection.Close();
                return count>0;
            }
            catch (Exception ex)
            {
                if (hrConnection.State == ConnectionState.Open)
                    hrConnection.Close();
                return false;
            }

        }

        #endregion

        #region Read

        public DataTable GetAllRequestsByProfCode(int code_ostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorRequests";
            cmd.Parameters.AddWithValue("@code_ostad", code_ostad);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }

        public DataTable GetAllRequestsByHRCode(int code_ostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorRequestsByHrID";
            cmd.Parameters.AddWithValue("@code_ostad", code_ostad);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }

        public DataTable GetAllRequestDocsByProfCode(int codeOstad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorRequestDocs";
            cmd.Parameters.AddWithValue("@codeOstad", codeOstad);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetChangeListByReqId(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetChangeListByReqId";
            cmd.Parameters.AddWithValue("@reqId", reqId);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }

        public ProfessorHokmDTO GetNewHokmInfo(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorNewHokmByReqId";
            cmd.Parameters.AddWithValue("@reqId", reqId);
            DataTable dt = new DataTable();
            ProfessorHokmDTO oHokm = new ProfessorHokmDTO();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                if (dt.Rows.Count > 0)

                {
                    oHokm.HokmId = Convert.ToInt32(dt.Rows[0]["HokmId"]);
                    oHokm.InfoPeopleId = Convert.ToInt32(dt.Rows[0]["InfoPeopleId"]);
                    oHokm.Code_Ostad = Convert.ToInt32(dt.Rows[0]["code_ostad"]);
                    oHokm.HokmUrl = dt.Rows[0]["hokmurl"].ToString();
                    oHokm.MablaghHokm = Convert.ToInt64(dt.Rows[0]["MablaghHokm"]);
                    oHokm.Number_Hokm = dt.Rows[0]["number_hokm"].ToString();
                    oHokm.Date_RunHokm = dt.Rows[0]["date_runhokm"].ToString();
                    oHokm.Date_Hokm = dt.Rows[0]["date_hokm"].ToString();
                    oHokm.Payeh = Convert.ToInt32(dt.Rows[0]["payeh"]);
                    oHokm.Type_Estekhdam = Convert.ToInt32(dt.Rows[0]["type_estekhdam"]);
                    oHokm.Uni_Khedmat = Convert.ToInt32(dt.Rows[0]["uni_khedmat"]);
                    oHokm.Uni_KhedmatType = Convert.ToInt32(dt.Rows[0]["uni_khedmatType"]);
                    oHokm.Nahveh_Hamk = Convert.ToInt32(dt.Rows[0]["nahveh_hamk"]);
                    if (dt.Rows[0]["isRetired"] != DBNull.Value)
                        oHokm.IsRetired = Convert.ToBoolean(dt.Rows[0]["isRetired"]);
                    oHokm.DateUpload = dt.Rows[0]["dateupload"].ToString();
                    oHokm.State = Convert.ToInt32(dt.Rows[0]["state"]);
                    if (dt.Rows[0]["DateRunHokmHere"] != DBNull.Value)
                    {
                        oHokm.DateRunHokmHere = dt.Rows[0]["DateRunHokmHere"].ToString();
                    }
                    oHokm.Martabeh = Convert.ToInt32(dt.Rows[0]["Martabeh"]);
                    oHokm.EditRequestId = Convert.ToInt32(dt.Rows[0]["EditRequestId"]);
                    if(dt.Rows[0]["BoundHour"]!=DBNull.Value)
                    oHokm.BoundHour = Convert.ToBoolean(dt.Rows[0]["BoundHour"]);
                }
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return oHokm;
        }

        public ProfessorHokmDTO GetLastMartabe(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorNewHokmByReqId";
            cmd.Parameters.AddWithValue("@reqId", reqId);
            DataTable dt = new DataTable();
            ProfessorHokmDTO oHokm = new ProfessorHokmDTO();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                if (dt.Rows.Count > 0)
                {
                    oHokm.HokmId = Convert.ToInt32(dt.Rows[0]["HokmId"]);
                    oHokm.InfoPeopleId = Convert.ToInt32(dt.Rows[0]["InfoPeopleId"]);
                    oHokm.Code_Ostad = Convert.ToInt32(dt.Rows[0]["code_ostad"]);
                    oHokm.HokmUrl = dt.Rows[0]["hokmurl"].ToString();
                    oHokm.MablaghHokm = Convert.ToInt64(dt.Rows[0]["MablaghHokm"]);
                    oHokm.Number_Hokm = dt.Rows[0]["number_hokm"].ToString();
                    oHokm.Date_RunHokm = dt.Rows[0]["date_runhokm"].ToString();
                    oHokm.Date_Hokm = dt.Rows[0]["date_hokm"].ToString();
                    oHokm.Payeh = Convert.ToInt32(dt.Rows[0]["payeh"]);
                    oHokm.Type_Estekhdam = Convert.ToInt32(dt.Rows[0]["type_estekhdam"]);
                    oHokm.Uni_Khedmat = Convert.ToInt32(dt.Rows[0]["uni_khedmat"]);
                    oHokm.Uni_KhedmatType = Convert.ToInt32(dt.Rows[0]["uni_khedmatType"]);
                    oHokm.Nahveh_Hamk = Convert.ToInt32(dt.Rows[0]["nahveh_hamk"]);
                    if (dt.Rows[0]["isRetired"] != DBNull.Value)
                        oHokm.IsRetired = Convert.ToBoolean(dt.Rows[0]["isRetired"]);
                    oHokm.DateUpload = dt.Rows[0]["dateupload"].ToString();
                    oHokm.State = Convert.ToInt32(dt.Rows[0]["state"]);
                    if (dt.Rows[0]["DateRunHokmHere"] != DBNull.Value)
                    {
                        oHokm.DateRunHokmHere = dt.Rows[0]["DateRunHokmHere"].ToString();
                    }
                    oHokm.Martabeh = Convert.ToInt32(dt.Rows[0]["Martabeh"]);
                    oHokm.EditRequestId = Convert.ToInt32(dt.Rows[0]["EditRequestId"]);
                    if (dt.Rows[0]["BoundHour"] != DBNull.Value)
                        oHokm.BoundHour = Convert.ToBoolean(dt.Rows[0]["BoundHour"]);
                }
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return oHokm;
        }
        

        public DataTable GetLastHokmInfoByInfoPeopleID(int infoPeopleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = hrConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.SP_GetHokmByInfoPeopleId";
            cmd.Parameters.AddWithValue("@InfoPeopleId", infoPeopleId);
            DataTable dt = new DataTable();
            try
            {
                hrConnection.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                
                hrConnection.Close();
            }
            catch (Exception e)
            {
                throw;
            }
            return dt;
        }

        public List<ProfessorEditRequestDTO> GetRequestByTypeAndStatus(string reqType, string reqStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetRequestByTypeAndStatus";
            cmd.Parameters.AddWithValue("@reqType", reqType);
            cmd.Parameters.AddWithValue("@reqStatus", reqStatus);

            List<ProfessorEditRequestDTO> oReqList = null;
            using (DataTable dt = new DataTable())
            {

                try
                {
                    if(conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        oReqList = new List<ProfessorEditRequestDTO>();
                        foreach (DataRow row in dt.Rows)
                        {
                            ProfessorEditRequestDTO oEdit = new ProfessorEditRequestDTO();

                            oEdit.Id = (int)row["ProfessorRequestID"];
                            oEdit.Code_Ostad = (int)row["Code_Ostad"];
                            oEdit.Createdate = row["CreateDate"].ToString();
                            oEdit.Erae_Be = row["Erae_Be"].ToString();
                            oEdit.ScanImageUrl = row["ScanImageUrl"].ToString();
                            oEdit.isDeleted = (bool)row["isdeleted"];
                            oEdit.ProfessorMessage = row["ProfessorMessage"].ToString();
                            oEdit.RequestLogID = Convert.ToInt32(row["RequestLogID"]);
                            oEdit.RequestTypeID = Convert.ToInt32(row["RequestTypeID"]);
                            oEdit.Term = row["term"].ToString();
                            oEdit.Note = row["Note"].ToString();
                            oEdit.ChangeSet = (int)row["ChangeSet"];
                            oEdit.FullName = row["name"].ToString();
                            oEdit.HR_InfoPeople_Id =(int) row["hr_infopeople_id"];
                            oEdit.hdn_fullName = oEdit.FullName;
                            oReqList.Add(oEdit);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                return oReqList;
            }
        }

        public List<ProfessorEditRequestDTO> GetProfessorRequestsByIdAndStatus(int code_Ostad, string state)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetRequestByCodeOstadAndStatus";
            cmd.Parameters.AddWithValue("@code_Ostad", code_Ostad);
            cmd.Parameters.AddWithValue("@state", state);

            List<ProfessorEditRequestDTO> oReqList = null;
            using (DataTable dt = new DataTable())
            {

                try
                {
                    conn.Open();
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        oReqList = new List<ProfessorEditRequestDTO>();
                        foreach (DataRow row in dt.Rows)
                        {
                            ProfessorEditRequestDTO oEdit = new ProfessorEditRequestDTO();

                            oEdit.Id = (int)row["ProfessorRequestID"];
                            oEdit.Code_Ostad = (int)row["Code_Ostad"];
                            oEdit.Createdate = row["CreateDate"].ToString();
                            oEdit.Erae_Be = row["Erae_Be"].ToString();
                            oEdit.ScanImageUrl = row["ScanImageUrl"].ToString();
                            oEdit.isDeleted = (bool)row["isdeleted"];
                            oEdit.ProfessorMessage = row["ProfessorMessage"].ToString();
                            oEdit.RequestLogID = Convert.ToInt32(row["RequestLogID"]);
                            oEdit.RequestTypeID = Convert.ToInt32(row["RequestTypeID"]);
                            oEdit.Term = row["term"].ToString();
                            oEdit.Note = row["Note"].ToString();
                            oEdit.ChangeSet = (int)row["ChangeSet"];
                            oEdit.FullName = row["Name"].ToString();
                            oEdit.HR_InfoPeople_Id = (int)row["hr_infopeople_id"];
                            oEdit.hdn_fullName = oEdit.FullName;
                            oReqList.Add(oEdit);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                return oReqList;
            }
        }

        public bool HasPendingRequest(int codeostad, int RequestTypeId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_ProfessorHasRequestBefore";
            cmd.Parameters.AddWithValue("@codeostad", codeostad);
            cmd.Parameters.AddWithValue("@RequestTypeId", RequestTypeId);

            try
            {
                int x;
                conn.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return Convert.ToBoolean(x);

            }
            catch (Exception)
            {

                throw;

            }
        }
        public DataTable GetProfessorFromResearchByCode(int codeOstad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorFromResearchByCode";
            cmd.Parameters.AddWithValue("@CodeOstad", codeOstad);
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetProfessorEditInfoFieldsByProfessorRequestId(int requestId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetProfessorEditInfoFieldsByProfessorRequestId";
            cmd.Parameters.AddWithValue("@reqId", requestId);
            try
            {
                DataTable dt = new DataTable();
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetDocByInfoIdAndType(int infoId, int typeId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = hrConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.SP_GetDocByInfoIdAndType";
            cmd.Parameters.AddWithValue("@infoId", infoId);
            cmd.Parameters.AddWithValue("@typeId", typeId);
            try
            {
                DataTable dt = new DataTable();
                hrConnection.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                hrConnection.Close();
                return dt;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Update

        public int UpdateOneChangeById(int reqId, string NewValue, int state)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateOneProfessorChangeById";
            cmd.Parameters.AddWithValue("@reqId", reqId);
            cmd.Parameters.AddWithValue("@NewValue", NewValue);
            cmd.Parameters.AddWithValue("@state", state);
            int count = 0;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }

        public int UpdateProfessorRequestStatus(int reqId, int RequestLogId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateProfessorRequestStatus";
            cmd.Parameters.AddWithValue("@reqId", reqId);
            cmd.Parameters.AddWithValue("@RequestLogId", RequestLogId);
            int count = 0;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }

        public bool UpdateOstadInformation_AfterApprove(int requestId)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateOstadInformation_AfterApprove_ThreeTables";
            cmd.Parameters.AddWithValue("@RequestId", requestId);
            int count = 0;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                result = true;
                conn.Close();


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
                throw;
            }
            return result;
        }


        public bool InsertDocumentToHr(int hrId,byte[] image,int docType,int status,string extention)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = hrConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "hr.SP_InsertUpdateScanAndDoc";
            cmd.Parameters.AddWithValue("@id", hrId);
            cmd.Parameters.Add("@doc", SqlDbType.Image);
            cmd.Parameters["@doc"].Value = image;
            cmd.Parameters.AddWithValue("@type", docType);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@ext", extention);
            int count = 0;

            try
            {
                hrConnection.Open();
                count = cmd.ExecuteNonQuery();
                result = true;
                hrConnection.Close();


            }
            catch (Exception ex)
            {
                if (hrConnection.State == ConnectionState.Open)
                    hrConnection.Close();
                return false;
                throw;
            }
            return result;
        }

        public bool updateProffessorScanStatus(Int64 codeOstad,int ProfessorReqId,int DocType,int status)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateProfessorRequestStatus_Doc";
            cmd.Parameters.AddWithValue("@reqId", ProfessorReqId);
            cmd.Parameters.AddWithValue("@codeOstad", codeOstad);
            cmd.Parameters.AddWithValue("@doctype", DocType);
            cmd.Parameters.AddWithValue("@status", status);
            int count = 0;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                result = true;
                conn.Close();


            }
            catch (Exception ext)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
                throw;
            }
            return result;
        }

        public int InsertMessageToRequest(int reqId, string message)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_InsertMessageToProfessorRequest";
            cmd.Parameters.AddWithValue("@reqId", reqId);
            cmd.Parameters.AddWithValue("@message", message);
            int count = 0;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }

        #endregion /Update

        #region Delete
        public int DeleteProfessorRequest(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_DeleteProfessorRequest";
            cmd.Parameters.AddWithValue("@reqId", reqId);

            try
            {
                int x;
                conn.Open();
                x = cmd.ExecuteNonQuery();
                conn.Close();

                return x;

            }
            catch (Exception)
            {

                throw;

            }
        }
        
        public bool DeleteProfessorRequest_Doc(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_DeleteProfessorRequestStatus_Doc";
            cmd.Parameters.AddWithValue("@reqId", reqId);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
                throw;

            }
            return true;
        }

        #endregion

        #region Helpers
        private static DataTable ConvertListToDataTable(IList<ChangedInfoDTO> list)
        {
            DataTable table = new DataTable();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(ChangedInfoDTO));
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType == typeof(FieldChangeState))
                {
                    table.Columns.Add(prop.Name, typeof(int));
                }
                else
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

            }
            object[] values = new object[props.Count];
            foreach (var item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion



    }
}
