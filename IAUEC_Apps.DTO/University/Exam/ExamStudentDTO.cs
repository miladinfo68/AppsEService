
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Exam
{
    public class ExamStudentDTO
    {
        //public ExamStudentDTO(string studentCode, string firstName, string lastName, string grade, string major)
        //{
        //    StudentCode = studentCode;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    StudentCode = studentCode;
        //    Grade = grade;
        //    Major = major;
        //}
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public string Major { get; set; }

        //=================================
        public string CourseTitle { get; set; }
        public string ProfossorFullName { get; set; }
        public string ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string Calculator { get; set; }
        public string Term { get; set; }
        public string Note { get; set; }
        public string ExamDuration { get; set; }
        public string TypeNimsal { get; set; }
        public string KeyCode { get; set; }
        public string LowBook { get; set; }
        public string ClassCode { get; set; }
        public string SeatHeader { get; set; }
        public int? SeatNumber { get; set; }  


    }
}
