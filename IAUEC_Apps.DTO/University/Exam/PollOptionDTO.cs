using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    public class PollOptionDTO
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public int Point { get; set; }
        public int PollQuestionId { get; set; }
    }
}
