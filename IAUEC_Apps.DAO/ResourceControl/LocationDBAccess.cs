using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResourceControl.DAL
{
    public class LocationDBAccess
    {
        public bool AddNewLocation(Location loc)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", loc.Name),
                new SqlParameter("@address",loc.Address)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_LocationsInsert]", CommandType.StoredProcedure, parameters); ;
        }

        public List<Location> GetLocationListByCatID(int catID)
        {
            List<Location> loclist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@catID", catID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_LocationsSelectByCatID]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    loclist = new List<Location>();

                    foreach (DataRow row in table.Rows)
                    {
                        Location loc = new Location();
                        loc.Id = Convert.ToInt32(row["location"]);
                        loc.Name = row["location_name"] as string;
                        //loc.Address = row["address"] as string;
                        loclist.Add(loc);
                    }
                }
            }

            return loclist;
        }

        public List<Location> GetAllLocation()
        {
            List<Location> loclist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_LocationsSelectAll]", CommandType.StoredProcedure))
            {

                if (table.Rows.Count > 0)
                {
                    loclist = new List<Location>();

                    foreach (DataRow row in table.Rows)
                    {
                        Location loc = new Location();
                        loc.Id = Convert.ToInt32(row["Id"]);
                        loc.Name = row["name"] as string;
                        loc.Address = row["address"] as string;
                        loclist.Add(loc);
                    }
                }
            }

            return loclist;
        }

        public bool UpdateLocation(Location loc,string oldname)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@oldname",oldname),
                new SqlParameter("@name", loc.Name),
                new SqlParameter("@address", loc.Address)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_LocationUpdate]", CommandType.StoredProcedure, parameters);
        }

        public List<Location> GetLocationByUserRoleId(int roleId)
        {
            List<Location> loclist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@roleId", roleId)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_LocationsSelectByRoleId]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    loclist = new List<Location>();

                    foreach (DataRow row in table.Rows)
                    {
                        Location loc = new Location();
                        loc.Name = row["location"] as string;
                        //loc.Address = row["address"] as string;
                        loclist.Add(loc);
                    }
                }
            }

            return loclist;
        }
    }
}