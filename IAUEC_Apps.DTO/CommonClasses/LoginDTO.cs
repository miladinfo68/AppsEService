using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        //public string Name { get; set; }
        public string LastName { get; set; }
        public string StReshte { get; set; }
        public bool Enable { get; set; }
        public int sectionId { get; set; }
        public int UserId { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
    }

    public class GroupManger
    {
        public int Id { get; set; }
        public string GroupManagerName { get; set; }
        public string GroupManagerUser { get; set; }
        public int? GroupID { get; set; }
        public string Term { get; set; }
        public bool? IsActive { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PicUrl { get; set; }
        public decimal? ProfessorCode { get; set; }
        public bool Type { get; set; }
        public bool? IsManager { get; set; }
    }
}

public static class sessionNames
{
    public const string userID_Karbar = "UserID";
    public const string userID_StudentOstad = "user";
    public const string userName_Karbar = "UserName";
    public const string userName_StudentOstad = "user_Name";
    public const string appID_Karbar = "AppID";
    public const string appID_StudentOstad = "stApp";
    public const string roleID = "RoleID";
    public const string roleText = "RoleName";
    public const string sectionID = "sectionID";
    public const string menuID = "MenuID";
    public const string user_Karbar = "UserLogin";
    public const string IdentityNumber = "IdentityNumber";
    public const string TypeSignature = "TypeSignature";
    public const string password_Karbar = "p";
    public const string password_StudentOstad = "Password";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";
    //public const string appID = "AppID";


}
