namespace ResourceControl.Entity
{
    public class Teacher
    {
        public string text { get; set; }
        public string Value { get; set; }
    }

    public class ProfessorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CertainTimesDto
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
        public bool IsForStudent { get; set; }
        public bool IsForEmployee { get; set; }
    }

    public class AttendanceProfessor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Place { get; set; }
        public string Kind { get; set; }
        public string Role { get; set; }
    }
}