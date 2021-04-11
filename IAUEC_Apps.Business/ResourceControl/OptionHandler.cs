using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAUEC_Apps.Business.Common;

namespace ResourceControl.BLL
{
    public class OptionHandler
    {
        OptionDBAccess OptionDB = null;
        private CommonBusiness common = null;
        public OptionHandler()
        {
            OptionDB = new OptionDBAccess();
            common = new CommonBusiness();
        }

        public List<Option> GetOptionList()
        {
            return OptionDB.GetOptionList();
        }

        public List<Option> GetOptionListByResID(int resID)
        {
            return OptionDB.GetOptionListByResID(resID);
        }
        public List<Option> GetOptionListByCatID(int catID)
        {
            return OptionDB.GetOptionListByCatID(catID);
        }
        public bool UpdateOption(Option option, int userId)
        {
            var resualt = OptionDB.UpdateOption(option);
            if (!resualt)
            {
                var myOption =
                    OptionDB.GetOptionList()
                        .FirstOrDefault(c => c.ID == option.ID);
                if (myOption == null) return false;
                var optionId = myOption.ID;
                common.InsertIntoUserLog(userId, "", 11, 138, "ویرایش امکان موجود", optionId);
                return false;
            }
            else
                return true;

        }

        // public Option GetOptionDetails(int optID)
        //{
        //    return OptionDB.GetOptionDetails(optID);
        //}

        public List<Option> GetOptionListByReqID(int reqID)
        {
            return OptionDB.GetOptionListByReqIDID(reqID);
        }

        public bool DeleteOption(int optID, int userId)
        {
            var optionId = 0;
            if (OptionDB.GetOptionList().Any(c => c.ID == optID))
            {
                optionId = optID;
            }
            var resualt = OptionDB.DeleteOption(optID);
            if (!resualt && optionId != 0)
            {

                common.InsertIntoUserLog(userId, "", 11, 139, "حذف امکان موجود", optionId);
                return false;
            }
            else
                return true;
        }
        public bool AddNewOption(Option option, int userId)
        {
            var resualt = OptionDB.AddNewOption(option);
            if (!resualt)
            {
                var myOption =
                    OptionDB.GetOptionList()
                        .FirstOrDefault(c => c.Name == option.Name && c.IsActive == option.IsActive && c.IsDeleted == option.IsDeleted);
                if (myOption == null) return false;
                var optionId = myOption.ID;
                common.InsertIntoUserLog(userId, "", 11, 137, "درج امکان جدید", optionId);
                return false;
            }
            else
                return true;

        }
    }
}