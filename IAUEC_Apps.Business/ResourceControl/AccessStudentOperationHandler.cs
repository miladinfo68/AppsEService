using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.ResourceControl;
using IAUEC_Apps.DTO.ResourceControlClasses;

namespace IAUEC_Apps.Business.ResourceControl
{
 public   class AccessStudentOperationHandler
    {
        AccessStudentOperationDBA model = null;
        public AccessStudentOperationHandler()
        {
            model = new AccessStudentOperationDBA();
        }
        #region GET
        public List<AccessStudentOperationModel> GetAll(AccessStudentOperationModel access)
        {
            return model.Select(access);
        }
        public List<AccessStudentOperationModel> GetAllForSelectUnit(AccessStudentOperationModel access)
        {
            return GetAll(access).Where(c => c.FlagAllowSelectUnit == true).ToList();
        }
        public List<AccessStudentOperationModel> GetAllForFinancial(AccessStudentOperationModel access)
        {
            return GetAll(access).Where(c => c.FlagAllowFinancial == true).ToList();
        }
        public AccessStudentOperationModel GetAStudent(AccessStudentOperationModel access)
        {
            return model.Select(access).Where(c=>c.StudentCode==access.StudentCode).FirstOrDefault();
        }
        #endregion

        #region Update
        public bool UpdateAStudent(AccessStudentOperationModel access)
        {
            return model.Update(access);
        }

        #endregion

        #region Enter
        public long EnterAStudent(AccessStudentOperationModel access)
        {
              return model.Insert(access);
        }

        #endregion 

    }
}
