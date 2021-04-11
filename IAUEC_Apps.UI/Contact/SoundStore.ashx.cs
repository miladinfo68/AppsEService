using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Web;

namespace IAUEC_Apps.UI.Contact
{
    /// <summary>
    /// Summary description for SoundStore
    /// </summary>
    public class SoundStore : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.Files.Count > 0)
            {

                const int typeFile = 2;
                string userID = context.Request.Params["userID"];
                string idGrp = context.Request.Params["idGrp"];
                string msg = context.Request.Params["msg"];
                string idOnChat = context.Request.Params["idOnChat"];
                string idMsgReplayed = context.Request.Params["idMsgReplayed"];
                //Fetch the Uploaded File.

                HttpPostedFile postedFile = context.Request.Files[0];

                //Set the Folder Path.
                string folderPath = context.Server.MapPath("~/Contact/SoundRecorder/");

                //Set the File Name.
                string fileName = Path.GetFileName(postedFile.FileName);
                // int index = fileName.LastIndexOf('.');

                // string format = fileName.Substring(index, fileName.Length - (index));
                string format = ".wav";
                var file = Functions.MessageJs.InsertMessage(userID, idOnChat, idGrp, msg + fileName, int.Parse(idMsgReplayed), typeFile, format);
                JObject jsonFile = JObject.Parse(file.Substring(1, file.Length - 2));

                //Save the File in Folder.
                //string json = new JavaScriptSerializer().Serialize(
                //new
                //{
                //    name = jsonFile["ChatID_P"] + format
                //});

                //Send File details in a JSON Response.
                postedFile.SaveAs(folderPath + jsonFile["ChatID_P"] + format);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = "text/json";
                context.Response.Write(file);
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}