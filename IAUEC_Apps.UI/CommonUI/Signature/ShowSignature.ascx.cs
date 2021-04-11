using System;
using System.Linq;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.CommonUI.Signature
{
    public partial class ShowSignature : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var identityNumber = Convert.ToInt32(Session[sessionNames.IdentityNumber]);
            var appId = Convert.ToInt32(Session[sessionNames.appID_Karbar]);
            var type = Convert.ToInt32(Session[sessionNames.TypeSignature]);

            var business = new CommonBusiness();

            var data = business.GetSignature(
                identityNumber,
                appId, type);
            var signature = data.FirstOrDefault();
            if (signature != null)
                hdn.Value = Convert.ToBase64String(signature.Image);
        }
    }
}