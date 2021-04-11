using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.Business.Adobe
{
    public class StuPresentBusiness
    {
        //Test
        StuPresentDAO spDAO=new StuPresentDAO();
        UniversityDAO UD = new UniversityDAO();

        public DataTable GetStudentInfo(string StudentCode, string LastName, string FirstName, string tterm)
        {
            return spDAO.GetStudentInfo(StudentCode, LastName, FirstName, tterm);
        }

        public DataTable GetTotalTime(string StudentCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            return spDAO.GetTotalTime(StudentCode, TermTimeStart, TermTimeEnd);
        }

        public DataTable GetTotalTime_Old(string StudentCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            return spDAO.GetTotalTime_Old(StudentCode, TermTimeStart, TermTimeEnd);
        }

        public DataTable GetTime(string StudentCode, string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            return spDAO.GetTime(StudentCode, ClassCode, TermTimeStart, TermTimeEnd);
        }
       
        public DataTable GetTime_Old(string StudentCode, string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            return spDAO.GetTime_Old(StudentCode, ClassCode, TermTimeStart, TermTimeEnd);
        }

        public DataTable TotalTimeResult(string Stcode, DateTime TermTimeStart, DateTime TermTimeEnd, string tterm)
        {
            DataTable DT1=new DataTable();
            DataTable DT2 = new DataTable();

            if (tterm == "93-94-1")
            {
                DT1 = GetTotalTime_Old(Stcode, TermTimeStart, TermTimeEnd);
                DT2 = GetTotalTime(Stcode, TermTimeStart, TermTimeEnd);
                foreach (DataRow item in DT2.Rows)
                    DT1.Rows.Add(item.ItemArray);
            }
            if (tterm == "93-94-2")
            {
                DateTime Start = new DateTime(2015, 02, 01);
                DateTime End = new DateTime(2015, 08, 22);
                DT1 = spDAO.GetTotalTime(Stcode, Start, End, tterm);
            }
            if (tterm == "93-94-3")
            {
                  DateTime Start = new DateTime(2015, 07, 01);
                 DateTime End = new DateTime(2015, 09, 22);
                DT1 = spDAO.GetTotalTime(Stcode, Start, End, tterm);
            }
            if (tterm == "94-95-1")
            {
                DT1 = spDAO.GetTotalTime(Stcode, TermTimeStart, TermTimeEnd, tterm);
            }
            if (tterm == "94-95-2")
            {
                DT1 = spDAO.GetTotalTime(Stcode, TermTimeStart, TermTimeEnd, tterm);
            }
            //else
            //    DT1 = GetTotalTime93942(Stcode);

            //DataTable DT10 = GetStudentClassInfoByTerm(tterm,Stcode);
            DataTable DT11 = new DataTable();
            DT11.Columns.Add("LOGIN", typeof(string));
            DT11.Columns.Add("StudentName", typeof(string));
            DT11.Columns.Add("LessonName", typeof(string));
          //  DT11.Columns.Add("PersianDate", typeof(string));
            DT11.Columns.Add("SumOfTime", typeof(string));
          //  DT11.Columns.Add("PRINCIPAL_ID", typeof(string));
            DT11.Columns.Add("code", typeof(int));
            DT11.Columns.Add("TeacherName", typeof(string));
            DT11.Columns.Add("tterm", typeof(string));
            
            
            //for (int i = 0; i < DT10.Rows.Count; i++)
            //{
                for (int j = 0; j < DT1.Rows.Count; j++)
                {
                    //if (DT10.Rows[i]["Code"].ToString() == DT1.Rows[j]["code"].ToString())
                    //{
                    DataTable dtClassInfo = spDAO.GetClassInfoByClassCode(DT1.Rows[j]["code"].ToString(), tterm);
                    if (dtClassInfo.Rows.Count>0)
	{
		 
	
                        DataRow row = DT11.NewRow();
                        row["LOGIN"] = DT1.Rows[j]["stcode"].ToString();
                        row["StudentName"] = DT1.Rows[j]["StudentName"].ToString();

                        row["LessonName"] = dtClassInfo.Rows[0]["namedars"].ToString();
                       // row["LessonName"] = DT10.Rows[i]["namedars"].ToString();
                       // row["PersianDate"] = DT1.Rows[j]["PersianDate"].ToString();
                        row["SumOfTime"] = DT1.Rows[j]["SumOfTime"].ToString();
                       // row["PRINCIPAL_ID"] = DT1.Rows[j]["PRINCIPAL_ID"].ToString();
                        row["code"] = DT1.Rows[j]["code"].ToString();
                        row["TeacherName"] = dtClassInfo.Rows[0]["Ost_Name"].ToString();
                        row["tterm"] = tterm.ToString();
                        DT11.Rows.Add(row);
                    //}
               // }
            }
}


            if (DT11.Rows.Count > 0)
            {
                DataView dv = DT11.DefaultView;
                dv.Sort = "code";
                DataTable sortedDT = dv.ToTable();

                DataTable DT3 = new DataTable();
                DT3.Columns.Add("ClassCode", typeof(string));
                DT3.Columns.Add("LessonName", typeof(string));
                DT3.Columns.Add("TeacherName", typeof(string));
                DT3.Columns.Add("SumOfTime", typeof(string));
                DT3.Columns.Add("Stcode", typeof(string));
                DT3.Columns.Add("StName", typeof(string));
                DT3.Columns.Add("tterm", typeof(string));

                TimeOfEachClass TimeOfEachClass_ = new TimeOfEachClass();
                string Code = "";
                long TotalTime = 0;

                for (int i = 0; i <= sortedDT.Rows.Count; i++)
                {
                    if (i == sortedDT.Rows.Count)
                    {
                        DataRow row = DT3.NewRow();
                        row["ClassCode"] = TimeOfEachClass_.code;
                        row["LessonName"] = TimeOfEachClass_.LessonName;
                        row["TeacherName"] = TimeOfEachClass_.TeacherName;
                        row["Stcode"] = TimeOfEachClass_.Stcode;
                        row["StName"] = TimeOfEachClass_.StName;
                        row["SumOfTime"] = TotalTime.ToString();
                        row["tterm"] = tterm.ToString();

                        DT3.Rows.Add(row);
                        TotalTime = 0;
                    }
                    else if (Code != sortedDT.Rows[i]["code"].ToString())
                    {
                        if (i > 0)
                        {
                            DataRow row = DT3.NewRow();
                            row["ClassCode"] = TimeOfEachClass_.code;
                            row["LessonName"] = TimeOfEachClass_.LessonName;
                            row["TeacherName"] = TimeOfEachClass_.TeacherName;
                            row["Stcode"] = TimeOfEachClass_.Stcode;
                            row["StName"] = TimeOfEachClass_.StName;
                            row["SumOfTime"] = TotalTime.ToString();
                            row["tterm"] = tterm.ToString();
                            DT3.Rows.Add(row);
                            TotalTime = 0;
                        }

                        TimeOfEachClass_.code = sortedDT.Rows[i]["code"].ToString();
                        TimeOfEachClass_.LessonName = sortedDT.Rows[i]["LessonName"].ToString();
                        TimeOfEachClass_.TeacherName = sortedDT.Rows[i]["TeacherName"].ToString();
                        TimeOfEachClass_.Stcode = sortedDT.Rows[i]["LOGIN"].ToString();
                        TimeOfEachClass_.StName = sortedDT.Rows[i]["StudentName"].ToString();
                        TotalTime = TotalTime + int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());
                        TimeOfEachClass_.tterm = tterm.ToString();

                        Code = TimeOfEachClass_.code;
                    }
                    else
                        TotalTime = TotalTime + int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());
                }

                return DT3;
            }
            else
                return DT1;            
            
        }
            
        public DataTable TimeResult(string StudentCode, string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd,string tterm)
        {
            DataTable DT1=new DataTable();
            DataTable DT2 = new DataTable();

            if (tterm == "93-94-1")
            {
                DT1 = GetTime_Old(StudentCode, ClassCode, TermTimeStart, TermTimeEnd);
                DT2 = GetTime(StudentCode, ClassCode, TermTimeStart, TermTimeEnd);

                foreach (DataRow item in DT2.Rows)
                    DT1.Rows.Add(item.ItemArray);
            }
            if (tterm == "93-94-2")
            {
                DateTime Start = new DateTime(2015, 02, 01);
                DateTime End = new DateTime(2015, 08, 22);
                DT1 = spDAO.GetStudentTimeMeetingByClassCode(StudentCode, ClassCode, Start, End, "Live");
            }
            if (tterm == "93-94-3")
            {
                DateTime Start = new DateTime(2015, 07, 01);
                DateTime End = new DateTime(2015, 09, 22);
                DT1 = spDAO.GetStudentTimeMeetingByClassCode(StudentCode, ClassCode, Start, End, "Live"); 
            }
            if (tterm == "94-95-1")
            {
                DateTime Start = new DateTime(2015, 09, 12);
                DateTime End = new DateTime(2016, 01, 22);
                DT1 = spDAO.GetStudentTimeMeetingByClassCode(StudentCode, ClassCode, Start, End, "Class");
            }
            if (tterm == "94-95-2")
            {
                DateTime Start = new DateTime(2016, 02, 06);
                DateTime End = new DateTime(2016, 06, 22);
                DT1 = spDAO.GetStudentTimeMeetingByClassCode(StudentCode, ClassCode, Start, End, "vc_new");
            }

           // DataTable DT10 = spDAO.GetStudentClassInfo(ClassCode,tterm);
            DataTable DT11 = new DataTable();
            DT11.Columns.Add("LOGIN", typeof(string));
            DT11.Columns.Add("StudentName", typeof(string));
            DT11.Columns.Add("LessonName", typeof(string));
            DT11.Columns.Add("PersianDate", typeof(string));
            DT11.Columns.Add("SumOfTime", typeof(string));
            DT11.Columns.Add("SCO_ID", typeof(string));
            DT11.Columns.Add("PRINCIPAL_ID", typeof(string));
            DT11.Columns.Add("code", typeof(int));
            DT11.Columns.Add("TeacherName", typeof(string));
            DT11.Columns.Add("TimeStart", typeof(string));
            DT11.Columns.Add("TimeEND", typeof(string));
            DT11.Columns.Add("TimeClass", typeof(string));

            //for (int i = 0; i < DT10.Rows.Count; i++)
            //{
                for (int j = 0; j < DT1.Rows.Count; j++)
                {
                    DataTable dtClassInfo = spDAO.GetClassInfoByClassCode(DT1.Rows[j]["code"].ToString(), tterm);
                    if (dtClassInfo.Rows.Count > 0)
                    {
                   
                        DataRow row = DT11.NewRow();
                        row["LOGIN"] = DT1.Rows[j]["LOGIN"].ToString();
                        row["StudentName"] = DT1.Rows[j]["StudentName"].ToString();
                        row["LessonName"] = dtClassInfo.Rows[0]["namedars"].ToString();
                        row["PersianDate"] = DT1.Rows[j]["PersianDate"].ToString();
                        row["SumOfTime"] = DT1.Rows[j]["SumOfTime"].ToString();
                        row["SCO_ID"] = DT1.Rows[j]["SCO_ID"].ToString();
                        row["PRINCIPAL_ID"] = DT1.Rows[j]["PRINCIPAL_ID"].ToString();
                        row["code"] = DT1.Rows[j]["code"].ToString();
                        row["TeacherName"] = DT1.Rows[j]["TeacherName"].ToString();
                        //row["TimeStart"] = dtClassInfo.Rows[0]["saatstart"].ToString();
                        //row["TimeEND"] = dtClassInfo.Rows[0]["saatend"].ToString();
                        row["TimeClass"] = dtClassInfo.Rows[0]["Klas_DAy"].ToString() + " " + dtClassInfo.Rows[0]["ClassTime"].ToString();

                        DT11.Rows.Add(row);

                    }
                }
            
            return DT11;
        }

        public DataTable GetStudentClassInfoByTerm(string tterm,string stcode)
        {
            return spDAO.GetStudentClassInfoByTerm(tterm, stcode);
        }

        public DataTable GetStudentClassInfo(string ClassCode,string tterm)
        {
            return spDAO.GetStudentClassInfo(ClassCode,tterm);
        }
        public DataTable GetClassInfoByClassCode(string ClassCode, string tterm)
        {
            return spDAO.GetClassInfoByClassCode(ClassCode, tterm);
        }   
        public List<string> GetActiveTerm()
        {
            DataTable DT = spDAO.GetActiveTerm();
            List<string> TermList = new List<string>();
            for (int i = 0; i < DT.Rows.Count; i++)
                TermList.Add(DT.Rows[i]["tterm"].ToString());
            
            return TermList;
        }

        //public DataTable GetActiveTermDT()
        //{
        //    DataTable DT = spDAO.GetActiveTerm();
         
        //    return spDAO.GetActiveTerm();
        //}

        public DataTable GetTotalTime93942(string StudentCode)
        {
            return spDAO.GetTotalTime93942(StudentCode);
        }

        public DataTable GetTime93942(string StudentCode, string ClassCode)
        {
            return spDAO.GetTime93942(StudentCode, ClassCode);
        }

        //=================================================================================================


        public DataTable GetClassTime(string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            return spDAO.GetClassTime(ClassCode, TermTimeStart, TermTimeEnd);
        }

        public DataTable GetClassTime_Old(string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd)
        {
            return spDAO.GetClassTime_Old(ClassCode, TermTimeStart, TermTimeEnd);
        }

        public DataTable GetClassTime93942(string ClassCode)
        {
            return spDAO.GetClassTime93942(ClassCode);
        }
        
        public DataTable GetClassTimeResult(string ClassCode, DateTime TermTimeStart, DateTime TermTimeEnd, string tterm)
        {
            // اگر دانشجو در یک روز چند زمان داشته باشد
            // همه زمان ها را با هم جمع کرده و درآخر یکی می کند
            DataTable DT1 = new DataTable();
            DataTable DT2 = new DataTable();

            if (tterm == "93-94-1")
            {
                DT1 = GetClassTime_Old(ClassCode, TermTimeStart, TermTimeEnd);
                DT2 = GetClassTime( ClassCode, TermTimeStart, TermTimeEnd);

                foreach (DataRow item in DT2.Rows)
                    DT1.Rows.Add(item.ItemArray);
            }
            else
                DT1 = GetClassTime93942(ClassCode);

            DataTable DT10 = GetStudentClassInfo(ClassCode,tterm);
            DataTable DT11 = new DataTable();
            DT11.Columns.Add("LOGIN", typeof(string));
            DT11.Columns.Add("StudentName", typeof(string));
            DT11.Columns.Add("LessonName", typeof(string));
            DT11.Columns.Add("PersianDate", typeof(string));
            DT11.Columns.Add("SumOfTime", typeof(string));
            DT11.Columns.Add("SCO_ID", typeof(string));
            DT11.Columns.Add("PRINCIPAL_ID", typeof(string));
            DT11.Columns.Add("TeacherName", typeof(string));
            DT11.Columns.Add("code", typeof(int));
            
            DT11.Columns.Add("TimeStart", typeof(string));
            DT11.Columns.Add("TimeEND", typeof(string));
            DT11.Columns.Add("TimeClass", typeof(string));

            for (int i = 0; i < DT10.Rows.Count; i++)
            {
                for (int j = 0; j < DT1.Rows.Count; j++)
                {
                    if (DT10.Rows[i]["Code"].ToString() == DT1.Rows[j]["Code"].ToString())
                    {
                        DataRow row = DT11.NewRow();
                        row["LOGIN"] = DT1.Rows[j]["LOGIN"].ToString();
                        row["StudentName"] = DT1.Rows[j]["StudentName"].ToString();
                        row["LessonName"] = DT10.Rows[i]["namedars"].ToString();
                        row["PersianDate"] = DT1.Rows[j]["PersianDate"].ToString();
                        row["SumOfTime"] = DT1.Rows[j]["SumOfTime"].ToString();
                        row["SCO_ID"] = DT1.Rows[j]["SCO_ID"].ToString();
                        row["PRINCIPAL_ID"] = DT1.Rows[j]["PRINCIPAL_ID"].ToString();
                        row["TeacherName"] = DT1.Rows[j]["TeacherName"].ToString();
                        row["code"] = DT10.Rows[i]["did"].ToString();
                        
                        row["TimeStart"] = DT10.Rows[i]["saatstart"].ToString();
                        row["TimeEND"] = DT10.Rows[i]["saatend"].ToString();
                        row["TimeClass"] = DT10.Rows[i]["TimeClass"].ToString();

                        DT11.Rows.Add(row);

                    }
                }
            }
            

            if (DT11.Rows.Count > 0)
            {
                DataView dv = DT11.DefaultView;
                dv.Sort = "PersianDate";
                dv.Sort = "LOGIN";                
                DataTable sortedDT = dv.ToTable();

                DataTable DT3 = new DataTable();
                DT3.Columns.Add("LOGIN", typeof(string));
                DT3.Columns.Add("StudentName", typeof(string));
                DT3.Columns.Add("LessonName", typeof(string));
                DT3.Columns.Add("PersianDate", typeof(string));
                DT3.Columns.Add("SumOfTime", typeof(string));
                DT3.Columns.Add("SCO_ID", typeof(string));
                DT3.Columns.Add("PRINCIPAL_ID", typeof(string));
                DT3.Columns.Add("TeacherName", typeof(string));
                DT3.Columns.Add("code", typeof(int));
                DT3.Columns.Add("TimeStart", typeof(string));
                DT3.Columns.Add("TimeEND", typeof(string));
                DT3.Columns.Add("TimeClass", typeof(string));

                TimeOfEachClass TimeOfEachClass_ = new TimeOfEachClass();
                for (int i = 0; i  < sortedDT.Rows.Count; i++)
                {
                    if( i==0)
                    {
                        // First Row - ADD To Class
                        TimeOfEachClass_.LOGIN = sortedDT.Rows[i]["LOGIN"].ToString(); 
                        TimeOfEachClass_.StudentName = sortedDT.Rows[i]["StudentName"].ToString(); 
                        TimeOfEachClass_.LessonName = sortedDT.Rows[i]["LessonName"].ToString(); 
                        TimeOfEachClass_.PersianDate = sortedDT.Rows[i]["PersianDate"].ToString(); 
                        TimeOfEachClass_.SCO_ID = sortedDT.Rows[i]["SCO_ID"].ToString(); 
                        TimeOfEachClass_.PRINCIPAL_ID = sortedDT.Rows[i]["PRINCIPAL_ID"].ToString(); 
                        TimeOfEachClass_.TeacherName = sortedDT.Rows[i]["TeacherName"].ToString(); 
                        TimeOfEachClass_.code = sortedDT.Rows[i]["code"].ToString(); 
                        TimeOfEachClass_.TimeStart = sortedDT.Rows[i]["TimeStart"].ToString(); 
                        TimeOfEachClass_.TimeEND = sortedDT.Rows[i]["TimeEND"].ToString(); ;
                        TimeOfEachClass_.TimeClass = int.Parse(sortedDT.Rows[i]["TimeClass"].ToString());
                        TimeOfEachClass_.SumOfTime = int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString()); 
                    }
                    else if (i==sortedDT.Rows.Count-1)
                    {
                        //Last Row
                        if (sortedDT.Rows[i]["LOGIN"].ToString() == sortedDT.Rows[i - 1]["LOGIN"].ToString()
                        && sortedDT.Rows[i]["PersianDate"].ToString() == sortedDT.Rows[i - 1]["PersianDate"].ToString())
                        {
                            // اگر باقبلی یکی بود
                            // مجموع ساعت حضور
                            // DataTable سپس اضافه شود به 
                            TimeOfEachClass_.SumOfTime = TimeOfEachClass_.SumOfTime + int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());

                            DataRow row = DT3.NewRow();
                            row["LOGIN"] = TimeOfEachClass_.LOGIN;
                            row["StudentName"] = TimeOfEachClass_.StudentName;
                            row["LessonName"] = TimeOfEachClass_.LessonName;
                            row["PersianDate"] = TimeOfEachClass_.PersianDate;
                            row["SCO_ID"] = TimeOfEachClass_.SCO_ID;
                            row["PRINCIPAL_ID"] = TimeOfEachClass_.PRINCIPAL_ID;
                            row["TeacherName"] = TimeOfEachClass_.TeacherName;
                            row["code"] = TimeOfEachClass_.code;
                            row["TimeStart"] = TimeOfEachClass_.TimeStart;
                            row["TimeEND"] = TimeOfEachClass_.TimeEND;
                            row["TimeClass"] = TimeOfEachClass_.TimeClass;
                            row["SumOfTime"] = TimeOfEachClass_.SumOfTime;
                            DT3.Rows.Add(row);
                        }
                        else
                        {
                            // اگر باقبلی یکی نبود
                            // ابتدا تابع قبلی اضافه شود                            
                            DataRow row = DT3.NewRow();
                            row["LOGIN"] = TimeOfEachClass_.LOGIN;
                            row["StudentName"] = TimeOfEachClass_.StudentName;
                            row["LessonName"] = TimeOfEachClass_.LessonName;
                            row["PersianDate"] = TimeOfEachClass_.PersianDate;
                            row["SCO_ID"] = TimeOfEachClass_.SCO_ID;
                            row["PRINCIPAL_ID"] = TimeOfEachClass_.PRINCIPAL_ID;
                            row["TeacherName"] = TimeOfEachClass_.TeacherName;
                            row["code"] = TimeOfEachClass_.code;
                            row["TimeStart"] = TimeOfEachClass_.TimeStart;
                            row["TimeEND"] = TimeOfEachClass_.TimeEND;
                            row["TimeClass"] = TimeOfEachClass_.TimeClass;
                            row["SumOfTime"] = TimeOfEachClass_.SumOfTime;
                            DT3.Rows.Add(row);

                            // سپس آخرین ردیف اضافه شود
                            DataRow row1 = DT3.NewRow();
                            row1["LOGIN"] = sortedDT.Rows[i]["LOGIN"].ToString();
                            row1["StudentName"] = sortedDT.Rows[i]["StudentName"].ToString();
                            row1["LessonName"] = sortedDT.Rows[i]["LessonName"].ToString();
                            row1["PersianDate"] = sortedDT.Rows[i]["PersianDate"].ToString();
                            row1["SCO_ID"] = sortedDT.Rows[i]["SCO_ID"].ToString();
                            row1["PRINCIPAL_ID"] = sortedDT.Rows[i]["PRINCIPAL_ID"].ToString();
                            row1["TeacherName"] = sortedDT.Rows[i]["TeacherName"].ToString();
                            row1["code"] = sortedDT.Rows[i]["code"].ToString();
                            row1["TimeStart"] = sortedDT.Rows[i]["TimeStart"].ToString();
                            row1["TimeEND"] = sortedDT.Rows[i]["TimeEND"].ToString();
                            row1["TimeClass"] = sortedDT.Rows[i]["TimeClass"].ToString();
                            row1["SumOfTime"] = sortedDT.Rows[i]["SumOfTime"].ToString();
                            DT3.Rows.Add(row1);                  
                        }
                    }
                    else if (sortedDT.Rows[i]["LOGIN"].ToString() == sortedDT.Rows[i - 1]["LOGIN"].ToString()
                        && sortedDT.Rows[i]["PersianDate"].ToString() == sortedDT.Rows[i - 1]["PersianDate"].ToString())
                    {
                        // اگر باقبلی یکی نبود
                        TimeOfEachClass_.SumOfTime = TimeOfEachClass_.SumOfTime + int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());
                    }                    
                    else
                    {
                        //   اضافه شود  DataTable ابتدا ردیف قبل به 
                        DataRow row = DT3.NewRow();
                        row["LOGIN"] = TimeOfEachClass_.LOGIN;
                        row["StudentName"] = TimeOfEachClass_.StudentName;
                        row["LessonName"] = TimeOfEachClass_.LessonName;
                        row["PersianDate"] = TimeOfEachClass_.PersianDate;
                        row["SCO_ID"] = TimeOfEachClass_.SCO_ID;
                        row["PRINCIPAL_ID"] = TimeOfEachClass_.PRINCIPAL_ID;
                        row["TeacherName"] = TimeOfEachClass_.TeacherName;
                        row["code"] = TimeOfEachClass_.code;
                        row["TimeStart"] = TimeOfEachClass_.TimeStart;
                        row["TimeEND"] = TimeOfEachClass_.TimeEND;
                        row["TimeClass"] = TimeOfEachClass_.TimeClass;
                        row["SumOfTime"] = TimeOfEachClass_.SumOfTime;
                        DT3.Rows.Add(row);

                        // سپس ردیف جدید در کلاس قرار گیرد 
                        TimeOfEachClass_.LOGIN = sortedDT.Rows[i]["LOGIN"].ToString();
                        TimeOfEachClass_.StudentName = sortedDT.Rows[i]["StudentName"].ToString();
                        TimeOfEachClass_.LessonName = sortedDT.Rows[i]["LessonName"].ToString();
                        TimeOfEachClass_.PersianDate = sortedDT.Rows[i]["PersianDate"].ToString();
                        TimeOfEachClass_.SCO_ID = sortedDT.Rows[i]["SCO_ID"].ToString();
                        TimeOfEachClass_.PRINCIPAL_ID = sortedDT.Rows[i]["PRINCIPAL_ID"].ToString();
                        TimeOfEachClass_.TeacherName = sortedDT.Rows[i]["TeacherName"].ToString();
                        TimeOfEachClass_.code = sortedDT.Rows[i]["code"].ToString();
                        TimeOfEachClass_.TimeStart = sortedDT.Rows[i]["TimeStart"].ToString();
                        TimeOfEachClass_.TimeEND = sortedDT.Rows[i]["TimeEND"].ToString(); ;
                        TimeOfEachClass_.TimeClass = int.Parse(sortedDT.Rows[i]["TimeClass"].ToString());
                        TimeOfEachClass_.SumOfTime = int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString()); 
                    }

                }
                

                // Final Sort
                DataView dv2 = DT3.DefaultView;
                dv2.Sort = "PersianDate";          
                DataTable sortedDT2 = dv2.ToTable();
                
                return sortedDT2;
            }
            else
                return DT1;    

            
        }


        public DataTable GetTotalTime(string StudentCode, DateTime TermTimeStart, DateTime TermTimeEnd, string term)
        {
            return spDAO.GetTotalTime(StudentCode, TermTimeStart, TermTimeEnd,term);
        }
        public DataTable GetTotalTimeInAllClassesByStcode(string StudentCode, string con)
        {
            return spDAO.GetTotalTimeInAllClassesByStcode(StudentCode, con);
        }
        public DataTable GetTotalTimeInAllClassesByStcode(string StudentCode)
        {
        
            SettingBusiness setb = new SettingBusiness();
            StudentDAO s= new StudentDAO();
            string term = s.GetLastTermBystcode(StudentCode);
            DataTable dtTermJari = UD.GetTermJary();
            string termJari = dtTermJari.Rows[0][0].ToString();
            int salStudent = 0;
            if (term!="")
             salStudent=Convert.ToInt32( term.Split('-')[0]);

            int jariSal = 0;
            if(termJari!="")
                jariSal = Convert.ToInt32( termJari.Split('-')[0]);
            
            if (term==""|| salStudent<(jariSal-2))
                return new DataTable() ;
            SettingDTO setdto = setb.GetSettingByTermC(term);
            if (setdto.ConName!="")
            {
                 return spDAO.GetTotalTimeInAllClassesByStcode(StudentCode, term);
            }
            else
            {
                return new DataTable();
            }
           
        }

    }
}
