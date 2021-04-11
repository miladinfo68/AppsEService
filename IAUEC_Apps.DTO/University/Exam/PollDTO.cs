using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    public class PollDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Term { get; set; }
        public string Description { get; set; }
        public bool NeedComment { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PollType { get; set; }
    }
}
