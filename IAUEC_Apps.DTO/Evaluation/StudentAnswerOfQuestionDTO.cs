using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.Evaluation
{
    public class StudentAnswerOfQuestionDTO
    {
        public int QuestionId { get; set; }
        public int SubQuestionId { get; set; }
        public int AnswerId { get; set; }
        public int UserId { get; set; }
        public string Term { get; set; }
    }
    public static class ExchangeModel
    {
        public static List<StudentAnswerOfQuestionDTO> ToModel(this string[] answers,int userId)
        {
            var studentAnswerOfQuestionList = new List<StudentAnswerOfQuestionDTO>();
            if (answers.Length == 0)
                return null;
            foreach (var answer in answers)
            {
                studentAnswerOfQuestionList.Add(new StudentAnswerOfQuestionDTO
                {
                    QuestionId = Convert.ToInt32(answer.Split(',')[0]),
                    SubQuestionId = Convert.ToInt32(answer.Split(',')[1]),
                    AnswerId = Convert.ToInt32(answer.Split(',')[2]),
                    UserId = userId
                });
            }
            return studentAnswerOfQuestionList;
        }
    }
}
