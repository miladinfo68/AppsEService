using System.Collections.Generic;


namespace IAUEC_Apps.DTO.University.Exam
{
    public class PollQuestionDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public bool NeedComment { get; set; }
        public int PollId { get; set; }
        public List<PollOptionDTO> PollOptions { get; set; }
    }
}
