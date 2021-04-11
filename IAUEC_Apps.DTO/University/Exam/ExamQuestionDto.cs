using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    [Serializable]
    public class ExamQuestionDto
    {
        public string HashedPass { get; set; }
        public string RelativePath { get; set; }
        public string Did { get; set; }
        public string QuestionId { get; set; }
        public bool IsExistQuestionFile { get; set; }
        public bool IsExistAttachFile { get; set; }
        public string UserId { get; set; }
        public string AttExt { get; set; }
    }
}
