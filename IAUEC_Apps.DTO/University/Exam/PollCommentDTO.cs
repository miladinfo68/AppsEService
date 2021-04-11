using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    public class PollCommentDTO
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public int UserId { get; set; }
        public string TargetObject { get; set; }
        public string Comment { get; set; }
    }
}
