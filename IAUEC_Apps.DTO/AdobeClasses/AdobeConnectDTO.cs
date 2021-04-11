using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
    public class AdobeConnectDTO
    {
        // For Login         
        public string DomainAddress { set; get; }
        public string DomainLogin { set; get; }
        public string DomainPassword { set; get; }
        public Cookie DomainCookies { set; get; }


        // Create Meetings
        public string MeetingName { set; get; }
        public string MeetingFolderId { set; get; }
        public string MeetingUrlPath { set; get; }
        public DateTime MeetingDataBegin { set; get; }
        public DateTime MeetingDataEnd { set; get; }


        // Create Folders
        public string FolderName { set; get; }
        public string FolderFolderId { set; get; }
        public string FolderUrlPath { set; get; }
        public DateTime FolderDataBegin { set; get; }
        public DateTime FolderDataEnd { set; get; }


        // Create Users
        public string UserFirstName { set; get; }
        public string UserLastName { set; get; }
        public string UserLogin { set; get; }
        public string UserPassWord { set; get; }
        public string UserEmail { set; get; }


        // Add/Update Users In Meetings
        public string PrincipalID { set; get; }
        public string ScoId { set; get; }
        public string MeetingPermissionType { set; get; }

        public void SetValueDefult(string userLogin="",string userPass= "4sx0pvauo4nleowu5ugvkkx9l0bpsbe"
            , string firstName="",string LastName="")
        {
            //DomainAddress = "stc.iauec.ac.ir";
            DomainAddress = "vadafavc.iauec.ac.ir";
            ////admin
            //DomainLogin = "m_saryazdi";
            //DomainPassword = "M3312@M_";
            DomainLogin = "vcadmin";
            DomainPassword = "VCadmin@1399@";
            //cur user
            UserLogin = userLogin;
            UserPassWord = userPass;
            UserFirstName = firstName;
            UserLastName = LastName;


        }




        public enum MeetingPermission
        {
            host,
            Presenter,
            Particepent
        }




      



    }
}
