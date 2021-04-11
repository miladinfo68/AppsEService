using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DAO.Evaluation;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.DTO.Evaluation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IAUEC_Apps.Business.Common
{
    public class EvaluationBusiness
    {
        #region field
        private readonly EvaluationDAO _evaluation = new EvaluationDAO();
        #endregion

        private List<string> GetStCode(string mobile)
        {
            var _commonDAO = new CommonDAO();
            return _commonDAO.GetStcode(mobile);
        }
        private static bool PersonIdentity(string stCode)
        {
            string html = string.Empty;
            string url = "http://mobile.iauec.ac.ir/Api/MainApi/PersonIdentity?code=" + stCode;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
                var res = JsonConvert.DeserializeObject<ResponceAppSms>(html);
                return res.Result == "true";
            }
        }
        private static string SendContent(AppSmsDTO model)
        {
            using (var client = new WebClient())
            {
                var encodedJson = JsonConvert.SerializeObject(model);
                client.Headers.Add("Content-Type:application/json");
                client.Encoding = Encoding.UTF8;
                var response = client.UploadString("http://mobile.iauec.ac.ir/Api/MainApi/SendContent", encodedJson);
                var res = JsonConvert.DeserializeObject<ResponceAppSms>(response);
                return res.Result;
            }
        }
        private static void InsertLogAppSms(string stCode, string message, bool isSend, string mobile)
        {
            var _commonDAO = new CommonDAO();
            _commonDAO.InsertLogAppSms(stCode, message, isSend, mobile);
        }



        public bool IsQuestionExist()
        {
            return _evaluation.IsQuestionExist();
        }
        public bool IsUserevaluated(int userId)
        {
            return _evaluation.IsUserevaluated(userId);
        }
        public List<QuestionDTO> GetQuestions()
        {
            return _evaluation.GetQuestions();
        }
        public bool InsertStudentAnswer(StudentAnswerOfQuestionDTO studentAnswerOfQuestion)
        {
            return _evaluation.InsertStudentAnswer(studentAnswerOfQuestion);
        }
        public bool InsertStudentAnswers(List<StudentAnswerOfQuestionDTO> model)
        {
            var termJary = SellectTermJary();
            model.ForEach(x => x.Term = termJary);
            return _evaluation.InsertStudentAnswers(model);
        }
        public bool InsertStudentComment(string comment, int userId)
        {
            return _evaluation.InsertStudentComment(comment, userId);
        }

        public string SellectTermJary()
        {
            return _evaluation.SellectTermJary();
        }
        public bool CallAzmoonApi(int studentId)
        {
            WebRequest request = WebRequest.Create("https://azmoon.iauec.ac.ir/api/index.php?student_id=" + studentId);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var responseFromServer = reader.ReadToEnd();
            dataStream.Close();
            response.Close();
            return responseFromServer == "1";
        }
    }
}
