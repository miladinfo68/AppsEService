using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAUEC_Apps.Business.Common;

namespace ResourceControl.BLL
{
    public class LocationHandler
    {
        LocationDBAccess LocationDB = null;
        private CommonBusiness common = null;
        public LocationHandler()
        {
            LocationDB = new LocationDBAccess();
            common = new CommonBusiness();
        }
        public bool AddNewLocation(Location loc, int userId)
        {
            var resualt = LocationDB.AddNewLocation(loc);
            if (!resualt)
            {
                var myLocation =
                    LocationDB.GetAllLocation()
                        .FirstOrDefault(c => c.Name == loc.Name && c.Address == loc.Address);
                if (myLocation == null) return false;
                var locationId = myLocation.Id;
                common.InsertIntoUserLog(userId, "", 11, 139, "ایجاد محل جدید", locationId);
                return false;
            }
            else
                return true;

        }
        public List<Location> GetLocationListByCatID(int catID)
        {
            return LocationDB.GetLocationListByCatID(catID);
        }

        public List<Location> GetAllLocation()
        {
            return LocationDB.GetAllLocation();
        }

        public bool UpdateLocation(Location loc, string oldname)
        {
            return LocationDB.UpdateLocation(loc, oldname);
        }

        public List<Location> GetLocationByUserRoleId(int roleId)
        {
            Location loc = new Location();
            if (roleId == 37 || roleId == 38)
            {
                loc.Id = 1;
                loc.Name = "ملاصدرا";
                return new List<Location>() { loc };
            }
            else if (roleId == 39 || roleId == 40)
            {
                loc.Id = 2;
                loc.Name = "ساختمان رام";
                return new List<Location>() { loc };
            }
            else
            {
                throw new Exception("Invalid RoleId");
            }
            //return LocationDB.GetLocationByUserRoleId(roleId);
        }
    }
}