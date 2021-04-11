using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.University.Request;
using System.Data;

namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutNaghsBusiness : IDisposable
    {
        private CheckOutNaghsDAO _naghsDAO = null;
        public CheckOutNaghsBusiness()
        {
            _naghsDAO = new CheckOutNaghsDAO();
        }

        #region Create

        public int InsertNewNaghs(CheckOutNaghsDTO oNaghs)
        {
            return _naghsDAO.InsertNewNaghs(oNaghs);
        }
        public int InsertOdat(CheckOutNaghsDTO oNaghs)
        {
            return _naghsDAO.InsertOdat(oNaghs);
        }
        #endregion
        public int InsertNaghs_Article(CheckOutNaghsDTO oNaghs)
        {
            return _naghsDAO.InsertNaghs_Article(oNaghs);
        }
        public int InsertNaghs(CheckOutNaghsDTO oNaghs)
        {
            return _naghsDAO.InsertNaghs(oNaghs);
        }
        public int UpdateStudentRequest(StudentRequest oSR)
        {
            return _naghsDAO.UpdateStudentRequest(oSR);
        }
        public void Delete_Naghs(string stcode)
        {
            _naghsDAO.Delete_Naghs(stcode);
        }

        #region Read
        public DataTable GetAllNaghsByReqId(int StudentRequestId)
        {
            return _naghsDAO.GetAllNaghsByReqId(StudentRequestId);
        }
        public DataTable GetallNotResolvedNaghsByReqId(int StudentRequestId)
        {
            return _naghsDAO.GetallNotResolvedNaghsByReqId(StudentRequestId);
        }

        public DataTable GetAllNaghsByStatusId(int currentStatus)
        {
            return _naghsDAO.GetAllNaghsByStatusId(currentStatus);
        }

        public bool HasNaghs(string issuerID)
        {
            int count = _naghsDAO.HasNaghs(issuerID);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public DataTable GetNaghsMessage(string issuerID)
        {
            DataTable naghsMsg = _naghsDAO.GetNaghsMessage(issuerID);

            return naghsMsg;


        }
        public DataTable GetNaghsByStCode(string issuerID)
        {
            return _naghsDAO.GetNaghsByStCode(issuerID);
        }
        //public DataTable GetAllNaghsViewModelByReqId(int StudentRequestId)
        //{
        //    return _naghsDAO.GetAllNaghsViewModelByReqId(StudentRequestId);
        //}
        #endregion

        #region Delete
        public void DeleteNaghs(int naghsId, int Erae_be, int reqLogId, int reqId)
        {
            _naghsDAO.DeleteNaghs(naghsId, Erae_be, reqLogId, reqId);
        }
        #endregion

        #region Update

        public bool AddResolveMessage(int naghsId, string message)
        {
            int count = _naghsDAO.AddResolveMessage(naghsId, message);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public int GetNaghsIdByReqId(int reqId)
        {
            int naghsId;
            if (_naghsDAO.GetNaghsIdByReqId(reqId).Rows.Count <= 0)
            {
                naghsId = 0;
            }
            else
            {
                naghsId = Convert.ToInt32(_naghsDAO.GetNaghsIdByReqId(reqId).Rows[0][0]);
            }
            return naghsId;

        }
        public bool ResolveNaghsByMessage(int RequestId, int RequestLogId, string message)
        {
            int count = _naghsDAO.ResolveNaghsByMessage(RequestId,RequestLogId, message);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ResolveNaghsById(int naghsId)
        {
            int count = _naghsDAO.ResolveNaghsById(naghsId);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public void Dispose()
        {
            if (_naghsDAO != null)
            {
                _naghsDAO.Dispose();
                _naghsDAO = null;
            }
        }


    }
}
