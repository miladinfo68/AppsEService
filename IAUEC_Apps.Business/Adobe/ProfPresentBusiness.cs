
using IAUEC_Apps.DAO.Adobe;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.Adobe
{
    public class ProfPresentBusiness
    {

        //StuPresentDAO StuDAO = new StuPresentDAO();

        public List<string> GetAllDepartment()
        {
            ProfPresentDAO ProfDAO = new ProfPresentDAO();
            DataTable DT = ProfDAO.GetAllDepartment();
            List<string> TermList = new List<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
                TermList.Add(DT.Rows[i]["Department"].ToString());

            return TermList;
        }

        public DataTable GetAllTimeOfEachClass(int Leniency, string Nimsal, string Department)
        {
            DataTable sortedDT = SortedDataTableBothServer(Nimsal, Department);

            DataTable DT3 = new DataTable();
            DT3.Columns.Add("Department", typeof(string));
            DT3.Columns.Add("GroupName", typeof(string));
            DT3.Columns.Add("LOGIN", typeof(string));
            DT3.Columns.Add("TeacherName", typeof(string));
            DT3.Columns.Add("code", typeof(int));
            DT3.Columns.Add("LessonName", typeof(string));
            DT3.Columns.Add("SumOfTime", typeof(string));
            DT3.Columns.Add("TimeClass", typeof(string));
            DT3.Columns.Add("SessionCount", typeof(string));
            DT3.Columns.Add("Hozoor", typeof(string));
            DT3.Columns.Add("Leniency", typeof(string));
            DT3.Columns.Add("tterm", typeof(string));

            TimeOfEachClass TimeOfEachClass_ = new TimeOfEachClass();
            TimeOfEachClass_.SumOfTime = 0;
            string Code = "";
            long TotalTime = 0;
            long TotalClassTime = 0;

            for (int i = 0; i < sortedDT.Rows.Count; i++)
            {
                if (Code != sortedDT.Rows[i]["code"].ToString())
                {
                    if (i > 0)
                    {
                        DataRow row = DT3.NewRow();
                        row["Department"] = TimeOfEachClass_.Department;
                        row["GroupName"] = TimeOfEachClass_.GroupName;
                        row["LOGIN"] = TimeOfEachClass_.LOGIN;
                        row["TeacherName"] = TimeOfEachClass_.TeacherName;
                        row["code"] = TimeOfEachClass_.codeInt;
                        row["LessonName"] = TimeOfEachClass_.LessonName;
                        row["SumOfTime"] = TotalTime.ToString();
                        row["TimeClass"] = TotalClassTime.ToString();
                        row["SessionCount"] = TimeOfEachClass_.SessionCount.ToString();
                        row["Hozoor"] = TimeOfEachClass_.Hozoor.ToString();
                        row["Leniency"] = Leniency.ToString();
                        row["tterm"] = TimeOfEachClass_.tterm.ToString();
                        DT3.Rows.Add(row);
                        TotalTime = 0;
                        TotalClassTime = 0;
                    }

                    TimeOfEachClass_.Department = sortedDT.Rows[i]["Department"].ToString();
                    TimeOfEachClass_.GroupName = sortedDT.Rows[i]["GroupName"].ToString();
                    TimeOfEachClass_.LOGIN = sortedDT.Rows[i]["LOGIN"].ToString();
                    TimeOfEachClass_.TeacherName = sortedDT.Rows[i]["TeacherName"].ToString();
                    TimeOfEachClass_.codeInt = int.Parse(sortedDT.Rows[i]["code"].ToString());
                    TimeOfEachClass_.LessonName = sortedDT.Rows[i]["LessonName"].ToString();
                    TimeOfEachClass_.SumOfTime = int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());
                    TimeOfEachClass_.TimeClass = int.Parse(sortedDT.Rows[i]["TimeClass"].ToString());
                    TimeOfEachClass_.tterm = sortedDT.Rows[i]["tterm"].ToString();
                    TimeOfEachClass_.SessionCount = 1;

                    if ((TimeOfEachClass_.SumOfTime + Leniency) - TimeOfEachClass_.TimeClass >= 0)
                        TimeOfEachClass_.Hozoor = 1;
                    else
                        TimeOfEachClass_.Hozoor = 0;

                    TotalTime = TotalTime + TimeOfEachClass_.SumOfTime;
                    TotalClassTime = TotalClassTime + TimeOfEachClass_.TimeClass;
                    Code = TimeOfEachClass_.codeInt.ToString();
                }

                else if (!(sortedDT.Rows[i]["code"].ToString() == sortedDT.Rows[i - 1]["code"].ToString()
                    && sortedDT.Rows[i]["PersianDate"].ToString() == sortedDT.Rows[i - 1]["PersianDate"].ToString()
                    && sortedDT.Rows[i]["FirstLogin"].ToString() == sortedDT.Rows[i - 1]["FirstLogin"].ToString()
                    && sortedDT.Rows[i]["LastLogOut"].ToString() == sortedDT.Rows[i - 1]["LastLogOut"].ToString()
                    ))
                {
                    if (sortedDT.Rows[i]["SumOfTime"].ToString() == "")
                        TimeOfEachClass_.SumOfTime = 0;
                    else
                        TimeOfEachClass_.SumOfTime = int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());

                    TimeOfEachClass_.TimeClass = int.Parse(sortedDT.Rows[i]["TimeClass"].ToString());
                    TimeOfEachClass_.SessionCount = TimeOfEachClass_.SessionCount + 1;
                    if ((TimeOfEachClass_.SumOfTime + Leniency) - TimeOfEachClass_.TimeClass >= 0)
                        TimeOfEachClass_.Hozoor = TimeOfEachClass_.Hozoor + 1;

                    // اضافه کرن مجموع زمان حضور
                    TotalTime = TotalTime + TimeOfEachClass_.SumOfTime;
                    // اضافه کردن مجموع زمان کلاس                    
                    TotalClassTime = TotalClassTime + TimeOfEachClass_.TimeClass;

                    TimeOfEachClass_.SumOfTime = int.Parse(TotalTime.ToString());
                    TimeOfEachClass_.TimeClass = int.Parse(TotalClassTime.ToString());
                }
            }

            return DT3;
        }

        public DataTable SortedDataTableBothServer(string tterm, string Department)
        {
            ProfPresentDAO ProfDAO = new ProfPresentDAO();
            // گرفتن لیست اساتید براساس ترم و دپارتمان
            DataTable DTProf_Sida = new DataTable();
            DTProf_Sida = ProfDAO.GetProfessorListByDepartment_Term(Department, tterm);// Get ProfList By Departmenet And Term

            // Adobe آماده کردن داده ها (کد استاد و کد درس) جهت ارسال به 
            DataTable tvp = new DataTable();//Array List    
            tvp.Columns.Add("ProfID", typeof(String));
            tvp.Columns.Add("ClassID", typeof(String));
            DataRow workRow;
            for (int i = 0; i < DTProf_Sida.Rows.Count; i++)
            {
                //Get Data that Need in Adobe for Give Prof Info
                workRow = tvp.NewRow();
                workRow[0] = "ins" + DTProf_Sida.Rows[i]["code_ostad"].ToString();
                workRow[1] = DTProf_Sida.Rows[i]["did"].ToString();
                tvp.Rows.Add(workRow);
            }

            DataTable DT1 = ProfDAO.GetProfessorTimeByArrayList(tvp);




            //DataTable DT1 = new DataTable();
            //DataTable DT0 = new DataTable();

            ////if (tterm == "93-94-1")
            ////{
            ////    DT1 = GivaAll(Department);
            ////    DT0 = GivaAll_OLD(Department);
            ////    foreach (DataRow dr in DT0.Rows)
            ////        DT1.Rows.Add(dr.ItemArray);
            ////}
            ////else
            ////    DT1 = GivaAll93942(Department);

            DataTable DT11 = new DataTable();
            DT11.Columns.Add("Department", typeof(string));
            DT11.Columns.Add("GroupName", typeof(string));
            DT11.Columns.Add("LOGIN", typeof(string));
            DT11.Columns.Add("TeacherName", typeof(string));
            DT11.Columns.Add("code", typeof(int));
            DT11.Columns.Add("LessonName", typeof(string));
            DT11.Columns.Add("PersianDate", typeof(string));
            DT11.Columns.Add("SumOfTime", typeof(string));
            DT11.Columns.Add("TimeClass", typeof(string));
            DT11.Columns.Add("TimeStart", typeof(string));
            DT11.Columns.Add("TimeEnd", typeof(string));
            DT11.Columns.Add("FirstLogin", typeof(string));
            DT11.Columns.Add("LastLogOut", typeof(string));
            DT11.Columns.Add("Hozoor", typeof(string));
            DT11.Columns.Add("NAME", typeof(string));
            DT11.Columns.Add("tterm", typeof(string));

            for (int i = 0; i < DTProf_Sida.Rows.Count; i++)
            {
                for (int j = 0; j < DT1.Rows.Count; j++)// را یکی میکند Adobe اطلاعات دریافت شده از 
                {
                    if (DTProf_Sida.Rows[i]["Code"].ToString() == DT1.Rows[j]["NAME"].ToString())
                    {
                        DataRow row = DT11.NewRow();
                        row["Department"] = DTProf_Sida.Rows[i]["Department"].ToString();
                        row["GroupName"] = DTProf_Sida.Rows[i]["namegroup"].ToString();
                        row["LessonName"] = DTProf_Sida.Rows[i]["namedars"].ToString();
                        row["LOGIN"] = DT1.Rows[j]["LOGIN"].ToString();
                        row["TeacherName"] = DT1.Rows[j]["TeacherName"].ToString();
                        row["PersianDate"] = DT1.Rows[j]["PersianDate"].ToString();
                        row["SumOfTime"] = DT1.Rows[j]["SumOfTime"].ToString();
                        row["FirstLogin"] = DT1.Rows[j]["FirstLogin"].ToString();
                        row["LastLogOut"] = DT1.Rows[j]["LastLogOut"].ToString();
                        row["code"] = DTProf_Sida.Rows[i]["did"].ToString();
                        row["TimeClass"] = DTProf_Sida.Rows[i]["TimeClass"].ToString();
                        row["TimeStart"] = DTProf_Sida.Rows[i]["saatstart"].ToString();
                        row["TimeEnd"] = DTProf_Sida.Rows[i]["saatend"].ToString();
                        row["tterm"] = tterm;
                        int HozorCheck = 0;
                        if (DT1.Rows[j]["SumOfTime"].ToString() != "")
                            HozorCheck = int.Parse(DT1.Rows[j]["SumOfTime"].ToString());

                        int TimeClassCheck = 0;
                        if (DTProf_Sida.Rows[i]["TimeClass"].ToString() != "")
                            TimeClassCheck = int.Parse(DTProf_Sida.Rows[i]["TimeClass"].ToString());

                        if (HozorCheck > TimeClassCheck)
                            row["Hozoor"] = "1";
                        else
                            row["Hozoor"] = "0";
                        DT11.Rows.Add(row);

                    }
                }
            }

            DataView dv = DT11.DefaultView;
            dv.Sort = "code";
            DataTable sortedDT = dv.ToTable();

            int testRows1 = sortedDT.Rows.Count;
            int testRows2 = DTProf_Sida.Rows.Count;
            int testRows3 = DT1.Rows.Count;
            return sortedDT;
        }

        public DataTable GetProfessorListByDepartment_Term(string tterm, int DepartmentCode)
        {
            ProfPresentDAO ProfDAO = new ProfPresentDAO();
            // گرفتن لیست اساتید براساس ترم و دپارتمان
            DataTable DTProf_Sida = new DataTable();
            DTProf_Sida = ProfDAO.GetProfessorListByDepartment_Term(DepartmentCode, tterm);// Get ProfList By Departmenet And Term
            DataTable DTProfMeetings = ProfDAO.R_TeacherTotalTimeinMeetingsListByClassListAndTerm(DTProf_Sida, tterm);
            return DTProfMeetings;


        }

        public DataTable GetMGTimeByUserName_Term(string username, string term)
        {
            ProfessorPresentDAO pd = new ProfessorPresentDAO();
            return pd.GetMGTimeByUserName_Term(username, term);

        }
        public DataTable getActiveGroupManager(int year, Int64 professorCode)
        {
            ProfessorPresentDAO pd = new ProfessorPresentDAO();
            DataTable dt = pd.getActiveGroupManager(year);
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("professorCode=" + professorCode);
                if (dr.Length > 0)
                    return dr.CopyToDataTable();

                else
                    return new DataTable();
            }
            return dt;
        }

        public string getActiveGroupOfGroupManagers(int year, Int64 codeOstad)
        {
            ProfessorPresentDAO pd = new ProfessorPresentDAO();
            DataTable dt = pd.getActiveGroupOfGroupManagers(year, codeOstad);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value && dt.Rows[0][0].ToString() !="")
            {
                string groups = dt.Rows[0][0].ToString();
                return groups.Substring(0, groups.LastIndexOf(','));
            }
            return "";
        }

        public long getProfessorSalary(int year, Int64 professorCode)
        {
            ProfessorPresentDAO pd = new ProfessorPresentDAO();
            DataTable dt = pd.getProfessorSalary(year, professorCode);
            if(dt.Rows.Count>0)
            {
                return Convert.ToInt64(dt.Rows[0]["salary"]);
            }
            return 0;
            
        }

        public DataTable GetMGUsers(string term)
        {
            ProfessorPresentDAO pd = new ProfessorPresentDAO();
            return pd.GetMGUsers(term);
        }


    }
}
