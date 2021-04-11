using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.university.Request
{
    public class RecordViolationBusiness
    {
        #region Fileds
        private readonly RecordViolationDAO _recordViolationDAO = new RecordViolationDAO();
        #endregion
        public DataTable GetRecordViolationList()
        {
            return _recordViolationDAO.GetRecordViolationList();
        }
        public bool InsertViolation(RecordViolationDTO recordViolationDTO)
        {
            return _recordViolationDAO.InsertViolation(recordViolationDTO);
        }
        public RecordViolationStudentInfo GetStudentInfo(RecordViolationDTO recordViolationDTO)
        {
            return _recordViolationDAO.GetStudentInfo(recordViolationDTO);
        }
        public bool SubmitDeleteDate(int id)
        {
            return _recordViolationDAO.SubmitDeleteDate(id);
        }
        public bool UnSubmitDeleteDate(int id)
        {
            return _recordViolationDAO.UnSubmitDeleteDate(id);
        }
    }
}
