using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    public class ExamQuestionInfo
    {
        public long ID { get; set; }
        public string Term { get; set; }
        public int CityId { get; set; }
        public string Did { get; set; }
        public string ExamDate { get; set; }
        public string ExamTime { get; set; }
        public decimal ProfCode { get; set; }
        public string ProfName { get; set; }
        public int CourseCode { get; set; }
        public string CourseName { get; set; }
        public string NewExamDate { get; set; }
        public string NewExamTime { get; set; }
        public byte Q2Status { get; set; }
        public string Q2Address { get; set; }
        public string Q2Password{ get; set; }
        public bool Calculator { get; set; }
        public bool Note { get; set; }
        public int MinuteExamTime { get; set; }
        public bool TemplateDownloaded { get; set; }
        public bool AnswerSheet1 { get; set; }
        public bool AnswerSheet2 { get; set; }
        public bool AnswerSheet3 { get; set; }
        public bool LowBook { get; set; }
        public string BookName { get; set; }
        public string SaveDate { get; set; }
        public string LastModifyDate { get; set; }
        public string FirstUploadDate { get; set; }
        public string ReciveDateExamSheet { get; set; }
        public string KeyCode { get; set; }
        public bool ApproveNewHeader { get; set; }
        public string RejectText { get; set; }
        public string TraceNumber { get; set; }

        public byte Status { get; set; }   
        public string ProffosorMobile { get; set; }
        public int ExamQuestionID { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
