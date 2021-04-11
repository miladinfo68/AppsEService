using IAUEC_Apps.Business.Conatct;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace IAUEC_Apps.UI
{
    public class SendDynamicMsg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            if (context.Request.Form.Count > 0)
            {


                string userID = context.Request.Params["userID"];
                string idOnChat = context.Request.Params["idOnChat"];
                string idGrp = context.Request.Params["idGrp"];
                string idChatLast = context.Request.Params["idChatLast"];

                //Fetch the Uploaded File.
                DataTable dt = null;
                if (userID.Trim() != "" && userID != "undefined" && idChatLast.Trim() != ""
                    && idChatLast != "undefined" && idGrp != "undefined" && idOnChat != "undefined")
                {
                    if (idGrp.Trim() != "")
                        dt = MesageGroupBuisnes.GetMessageGroup(userID, idGrp, idChatLast);
                    else if (idOnChat.Trim() != "")
                        dt = MessagePersonalBuisnes.GetMessagePersonal(userID, idOnChat, idChatLast);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                                if (col.ColumnName == "Images")
                                    row["Images"] = System.Convert.ToBase64String((byte[])row["Images"]);
                            }
                            rows.Add(row);

                        }

                        string daresult = serializer.Serialize(rows);
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        context.Response.ContentType = "text/json";


                        if (daresult != null && daresult.Length > 0)
                        {
                            context.Response.Write(daresult);
                        }
                        else
                        {
                            context.Response.Write("");
                        }
                        context.Response.End();

                    }
                }

                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "text/json";
                    context.Response.Write("");
                    context.Response.End();
                }


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