using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ResourceControl.DAL
{
    public class CategoryDBAccess
    {
        public bool AddNewCategory(Category category)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {                
                new SqlParameter("@name", category.Name),
                new SqlParameter("@description", category.Description)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_CategoryInsert]", CommandType.StoredProcedure, parameters); ;
        }
        public bool UpdateCategory(Category category)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID",category.ID),
                new SqlParameter("@name", category.Name),
                new SqlParameter("@description", category.Description)
            };
            return SqlDBHelper.ExecuteNonQuery("[Resource_Control].[sp_CategoryUpdate]", CommandType.StoredProcedure, parameters);
        }
        //public bool DeleteCategory(int catID)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ID", catID)
        //    };

        //    return SqlDBHelper.ExecuteNonQuery("sp_CategoryDelete", CommandType.StoredProcedure, parameters);
        //}
        public Category GetCategoryDetails(int catID)
        {
            Category category = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", catID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_CategorySelect]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];


                    category = new Category();

                    category.ID = Convert.ToInt32(row["ID"]);
                    category.Name = row["name"] as string;
                    category.Description = row["description"] as string;
                }
            }

            return category;
        }

        public List<Category> GetCategoryListByLocId(int locID)
        {
            List<Category> categorylist = null;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@location", locID)
            };

            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_CategorySelectBylocation]", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    categorylist = new List<Category>();

                    foreach (DataRow row in table.Rows)
                    {
                        Category category = new Category();
                        category.ID = Convert.ToInt32(row["CategoryID"]);
                        category.Name = row["CategoryName"] as string;
                        category.Description = row["description"] as string;

                        categorylist.Add(category);
                    }
                }
            }

            return categorylist;
        }

        public List<Category> GetCategoryList()
        {
            List<Category> categorylist = null;

            using (DataTable table = SqlDBHelper.ExecuteSelectCommand("[Resource_Control].[sp_CategorySelectAll]", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    categorylist = new List<Category>();

                    foreach (DataRow row in table.Rows)
                    {
                        Category category = new Category();
                        category.ID = Convert.ToInt32(row["ID"]);
                        category.Name = row["name"] as string;
                        category.Description = row["description"] as string;

                        categorylist.Add(category);
                    }
                }
            }

            return categorylist;
        }

        public List<Category> GetCategoryListByLocationId(int locId)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@locId", locId)
            };

            List<Category> catList = new List<Category>();
            using (DataTable table = SqlDBHelper.ExecuteParamerizedSelectCommand("[Resource_Control].[sp_GetCategoryListByLocationId]", CommandType.StoredProcedure, parameters))
            {

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Category category = new Category();

                        category.ID = Convert.ToInt32(row["ID"]);
                        category.Name = row["name"] as string;
                        category.Description = row["description"] as string;

                        catList.Add(category);
                    }

                    
                }
            }

            return catList;
        }
    }
}