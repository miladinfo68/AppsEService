using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DAO.Adobe;

namespace IAUEC_Apps.Business.Adobe
{
    public class AdobeMasterBusiness
    {
        AdobeMasterDAO AMDAO = new AdobeMasterDAO();

        //This Need To Chenge and need To Optimize
        public bool CreateFolder(string FolderName)
        {
            AdobeConnectDTO ACDTO = new DTO.AdobeClasses.AdobeConnectDTO();
            ACDTO.FolderName = "T"+FolderName;
            ACDTO.FolderFolderId = "24531";
            ACDTO.FolderUrlPath = "00000-000";
            ACDTO.FolderDataBegin = new DateTime(2015, 01, 01);
            ACDTO.FolderDataEnd = new DateTime(2016, 12, 28);
            return AMDAO.CreateFolder(ACDTO);
            

            
        }








    }
}
