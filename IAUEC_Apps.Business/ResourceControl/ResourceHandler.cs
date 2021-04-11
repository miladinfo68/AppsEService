using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAUEC_Apps.Business.Common;
using System.Data;

namespace ResourceControl.BLL
{
    public class ResourceHandler
    {
        ResourceDBAccess ResourceDB = null;
        private CommonBusiness common = null;




        public ResourceHandler()
        {
            ResourceDB = new ResourceDBAccess();
            common = new CommonBusiness();
        }
        public List<Resource> GetResourceList()
        {
            return ResourceDB.GetResourceList();
        }
        public List<Resource> GetResourceListByCatID(int catID)
        {
            return ResourceDB.GetResourceListByCatID(catID);
        }

        public List<Resource> GetResourceListByCatIDandLocation(int catID, int location)
        {
            return ResourceDB.GetResourceListByCatIDandLocation(catID, location);
        }

        public List<Resource> GetResourceListByCatLocation(int location)
        {
            return ResourceDB.GetResourceListByCatLocation(location);
        }

        public bool UpdateResource(Resource resource, int userId)
        {
            var resualt = ResourceDB.UpdateResource(resource);
            return ResourceLog(resualt, userId, resource.ID);
        }
        public bool UpdateResource(Resource resource, List<Option> optlist, int userId)
        {
            try
            {
                foreach (var item in optlist)
                {
                    Res_Opt_Junc res_opt = new Res_Opt_Junc();
                    res_opt.Res_id = resource.ID;
                    res_opt.Opt_id = item.ID;
                    res_opt.IsActive = item.IsActive;
                    Res_Opt_JuncHandler rsopt = new Res_Opt_JuncHandler();
                    rsopt.UpdateRes_Opt_Junc(res_opt);
                }
            }
            catch (Exception)
            {

                throw;
            }
            var resualt = ResourceDB.UpdateResource(resource);
            return ResourceLog(resualt, userId, resource.ID);
        }

        private bool ResourceLog(bool resualt, int userId, int id)
        {
            CommonBusiness common = new CommonBusiness();

            if (!resualt)
            {
                var myResourc =
                   ResourceDB.GetResourceList()
                        .FirstOrDefault(c => c.ID == id);
                if (myResourc == null) return false;
                var resourcId = myResourc.ID;
                common.InsertIntoUserLog(userId, "", 11, 142, "ویرایش کلاس موجود", resourcId);
                return false;
            }
            else
                return true;

        }
        public Resource GetResourceDetails(int resID)
        {
            return ResourceDB.GetResourceDetails(resID);
        }

        // public bool DeleteResource(int catID)
        //{
        //    return ResourceDB.DeleteResource(catID);
        //}
        public int AddNewResource(Resource resource, List<Option> optlist, int userId)
        {
            int id = ResourceDB.AddNewResource(resource);

            foreach (var item in optlist)
            {
                Res_Opt_Junc res_opt = new Res_Opt_Junc();
                res_opt.Res_id = id;
                res_opt.Opt_id = item.ID;
                Res_Opt_JuncHandler rsopt = new Res_Opt_JuncHandler();
                rsopt.AddNewRes_Opt_Junc(res_opt);
            }


            ;
            if (id != 0)
            {
                var myResource =
                    ResourceDB.GetResourceList()
                        .FirstOrDefault(c => c.ID == id);
                if (myResource == null) return id;
                var resourceId = myResource.ID;
                common.InsertIntoUserLog(userId, "", 11, 141, "ثبت کلاس جدید", resourceId);
                return id;
            }
            else
                return id;
        }

        public List<Resource> GetResourceListByReqID(int reqID)
        {
            return ResourceDB.GetResourceListByReqID(reqID);
        }
        public List<Resource> ResourceSelectByReqStudentID(int reqID)
        {
            return ResourceDB.ResourceSelectByReqStudentID(reqID);
        }

        public List<Resource> GetResourceListByReqIDForAfterAccept(int reqID)
        {
            return ResourceDB.GetResourceListByReqIDForAfterAccept(reqID);
        }
        public List<Resource> GetResourceListByStudentReqIDForAfterAccept(int reqID)
        {
            return ResourceDB.GetResourceListByStudentReqIDForAfterAccept(reqID);
        }
        public List<Resource> GetResourceListByCatIDandRoleId(int RoleId, int catId)
        {
            if (RoleId == 37 || RoleId == 38)
            {
                return ResourceDB.GetResourceListByLocationIdandCatId(1, catId);
            }
            //categories availabel in locationId 2 (Pasdaran)
            else if (RoleId == 39 || RoleId == 40)
            {
                return ResourceDB.GetResourceListByLocationIdandCatId(2, catId);
            }
            else
            {
                return ResourceDB.GetResourceList();
            }
        }

        public bool IsSourceUsed(int resourceId)
        {
            var resualt = ResourceDB.IsSourceUsed(resourceId);
            return resualt > 0;
        }

        public void DeleteResource(int resourceId, int userId)
        {
            ResourceDB.Delete(resourceId);
            common.InsertIntoUserLog(userId, "", 11, 143, "حذف منبع", resourceId);
        }
    }
}