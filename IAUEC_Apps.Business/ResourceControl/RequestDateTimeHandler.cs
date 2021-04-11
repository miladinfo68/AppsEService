using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceControl.BLL
{
    public class RequestDateTimeHandler
    {
        RequestDateTimeDBAccess _dateTimeDB = null;

        public RequestDateTimeHandler()
        {
            _dateTimeDB = new RequestDateTimeDBAccess();
        }


        public void AddDateTimeOfRequest(List<Entity.RequestDateTime> dateTimeList)
        {
            _dateTimeDB.InsertListOfDateTime(dateTimeList);
        }
        public void AddDateTimeOfRequestV2(Entity.RequestDateTime dateTime , int collegeId)
        {
            _dateTimeDB.InsertNewDateTimeForDefenceV2(dateTime, collegeId);
        }

        public List<Entity.RequestDateTime> GetDateTimeListByRequestId(int requestId)
        {
            return _dateTimeDB.GetDateTimeListByRequestId(requestId);
        }
        public List<Entity.RequestDateTime> GetDateTimeListByRequestIdForStudent(int requestId)
        {
            return _dateTimeDB.GetDateTimeListByRequestIdForStudent(requestId);
        }

        public List<RequestDateTime> CheckDateTimeListWithResourceId(int RequestId, int ResourceId)
        {
            return _dateTimeDB.CheckDateTimeListWithResourceId(RequestId, ResourceId);
        }

        public int CheckOneDateTimeWithResourceId(int dateTimeId, int ResourceId)
        {
            return _dateTimeDB.CheckOneDateTimeWithResourceId(dateTimeId, ResourceId);
        }

        public bool UpdateOneDateTimeRequest(RequestDateTime reqDateTime)
        {
            int count = _dateTimeDB.UpdateOneDateTimeRequest(reqDateTime);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpdateOneDateTimeRequestForDefence(RequestDateTime reqDateTime)
        {
            int count = _dateTimeDB.UpdateOneDateTimeRequestForDefence(reqDateTime);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateAllDateTimeRequest(List<RequestDateTime> dateTimeList, int status, string userID)
        {
            try
            {
                int count = _dateTimeDB.UpdateListOfDateTime(dateTimeList);
                if (count != 0)
                {
                    RequestHandler reqH = new RequestHandler();
                    int result = reqH.UpdateRequestStatus(dateTimeList[0].RequestId, status, userID);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateAllDateTimeRequest(List<RequestDateTime> dateTimeList, int status, string userID, int roleId)
        {
            try
            {
                int count = _dateTimeDB.UpdateListOfDateTime(dateTimeList);
                if (count != 0)
                {
                    RequestHandler reqH = new RequestHandler();
                    int result = reqH.UpdateRequestStatus(dateTimeList[0].RequestId, status, userID, roleId);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool UpdateAllDateTimeRequestForDefence(List<RequestDateTime> dateTimeList, int status, string userID, int roleId)
        {
            try
            {
                _dateTimeDB.UpdateListOfDateTimeForDefence(dateTimeList);
 
                    RequestHandler reqH = new RequestHandler();
                    int result = reqH.UpdateRequestStatus(dateTimeList[0].RequestId, status, userID, roleId);
                    return true;
          

            }
            catch (Exception exception)
            {

                return false;
            }
        }


        public int CheckOneDateTimeWithResourceIdPlus(int dateTimeId, int resourceId)
        {
            return _dateTimeDB.CheckOneDateTimeWithResourceIdPlus(dateTimeId, resourceId);
        }
        public int CheckOneDateTimeWithResourceIdPlusForStudent(int dateTimeId, int resourceId)
        {
            var resualt= _dateTimeDB.CheckOneDateTimeWithResourceIdPlusForStudent(dateTimeId, resourceId);
            if (resualt != null && resualt.Rows.Count > 0)
                return resualt.Rows.Count;
            else
                return 0;
        }
    }
}
