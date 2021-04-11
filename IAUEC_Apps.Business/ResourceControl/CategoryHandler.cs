using ResourceControl.DAL;
using ResourceControl.Entity;
using System.Collections.Generic;
using System.Linq;
using IAUEC_Apps.Business.Common;

namespace ResourceControl.BLL
{
    public class CategoryHandler
    {
        CategoryDBAccess CategoryDB = null;
        private CommonBusiness common = null;

        public CategoryHandler()
        {
            CategoryDB = new CategoryDBAccess();
            common = new CommonBusiness();
        }

        public List<Category> getCategoryListByLocId(int locId)
        {
            return CategoryDB.GetCategoryListByLocId(locId);
        }

        public List<Category> GetCategoryList()
        {
            return CategoryDB.GetCategoryList();
        }

        public bool UpdateCategory(Category category, int userId)
        {

            var resualt = CategoryDB.UpdateCategory(category);
            if (!resualt)
            {
                var myCategory =
                    CategoryDB.GetCategoryList()
                        .FirstOrDefault(c => c.Name == category.Name && c.Description == category.Description);
                if (myCategory == null) return false;
                var categoryId = myCategory.ID;
                common.InsertIntoUserLog(userId, "", 11, 135, "ویرایش دسته بندی جدید", categoryId);
                return false;
            }
            else
                return true;
        }

        public Category GetCategoryDetails(int catID)
        {
            return CategoryDB.GetCategoryDetails(catID);
        }
        //public bool DeleteCategory(int catID)
        //{
        //    return CategoryDB.DeleteCategory(catID);
        //}
        public bool AddNewCategory(Category category, int userId)
        {

            var resualt = CategoryDB.AddNewCategory(category);
            if (!resualt)
            {
                var myCategory =
                    CategoryDB.GetCategoryList()
                        .FirstOrDefault(c => c.Name == category.Name && c.Description == category.Description);
                if (myCategory == null) return false;
                var categoryId = myCategory.ID;
                common.InsertIntoUserLog(userId, "", 11, 136, "درج دسته بندی جدید", categoryId);
                return false;
            }
            else
                return true;

        }

        public List<Category> GetCategoriesByUserRoleId(int RoleId)
        {
            //categories availabel in locationId 1 (molasadra)
            if (RoleId == 37 || RoleId == 38)
            {
                return CategoryDB.GetCategoryListByLocationId(1);
            }
            //categories availabel in locationId 2 (Pasdaran)
            else if (RoleId == 39 || RoleId == 40)
            {
                return CategoryDB.GetCategoryListByLocationId(2);
            }
            else
            {
                return CategoryDB.GetCategoryList();
            }
        }
    }
}