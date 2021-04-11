using System;
using System.Web.Services;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.CommonUI.Signature
{
    public partial class Signature : System.Web.UI.Page
    {
        private static CommonBusiness _business { get; set; }
        private static int _identityNumber { get; set; }
        private static int _appId { get; set; }
        private static int _type { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            _identityNumber = Convert.ToInt32(Session[sessionNames.IdentityNumber]);
            _appId = Convert.ToInt32(Session[sessionNames.appID_Karbar]);
            _type = Convert.ToInt32(Session[sessionNames.TypeSignature]);
            _business = new CommonBusiness();
        }
        [WebMethod(EnableSession = true)]
        public static void UploadImage(string imageData)
        {
            var data = Convert.FromBase64String(imageData);

            _business.AddSignature(
                data,
                _identityNumber,
                _appId, _type);
        }


    }
}