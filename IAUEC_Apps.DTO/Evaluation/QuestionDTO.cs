using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.Evaluation
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Term { get; set; }
        public bool IsLastQuestion { get; set; }
        public List<AnswerOfQuestionDTO> AnswerOfQuestions { get; set; }
        public List<SubQuestionDTO> SubQuestionDTOs { get; set; }
    }
}
