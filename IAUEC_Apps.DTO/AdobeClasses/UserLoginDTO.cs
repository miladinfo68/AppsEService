using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
    public class UserLoginDTO
    {
      public int  UserId{get;set;} 
	  public string UserName {get;set;} 
	  public string Password {get;set;} 
	  public int  RoleId {get;set;}
      public string Name { get; set; }
    }
}
