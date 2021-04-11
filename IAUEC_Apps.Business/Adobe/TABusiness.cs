using IAUEC_Apps.DAO.Adobe;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace IAUEC_Apps.Business.Adobe
{
    public class TABusiness
    {
        TADAO TADAO = new TADAO();
        StuPresentDAO spDAO = new StuPresentDAO();

        
        public DataTable ReadExcelFile(string Path)
        {
            DataTable UsersTable = new DataTable();
            UsersTable.Columns.Add("LOGIN", typeof(string));
            UsersTable.Columns.Add("Name", typeof(string));
            UsersTable.Columns.Add("Family", typeof(string));
            UsersTable.Columns.Add("Email", typeof(string));
            UsersTable.Columns.Add("ClassCode", typeof(string));

            Excel.Application xlApp = new Excel.Application();  
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(Path);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
                       
            for (int i = 1; i <= 1000; i++)
            {              
                if (xlRange.Cells[i, 1] != null && xlRange.Cells[i, 1].Value2 != null)
                {
                    DataRow row = UsersTable.NewRow();
                    row["LOGIN"] = xlRange.Cells[i, 1].Value2.ToString();
                    row["Name"] = "";
                    row["Family"] = "";
                    row["Email"] = "";
                    row["ClassCode"] = "";

                    if (xlRange.Cells[i, 2] != null)
                        row["Name"] = xlRange.Cells[i, 2].Value2.ToString();

                    if (xlRange.Cells[i, 2] != null)
                        row["Family"] = xlRange.Cells[i, 3].Value2.ToString();

                    if (xlRange.Cells[i, 2] != null)
                        row["Email"] = xlRange.Cells[i, 4].Value2.ToString();

                    if (xlRange.Cells[i, 2] != null)
                        row["ClassCode"] = xlRange.Cells[i, 5].Value2.ToString();

                    UsersTable.Rows.Add(row);
                }
                else
                    break;
            }

            xlWorkbook.Close();     
            return UsersTable;
        }
        
        public DataTable GetTAUsers()
        {            
            return TADAO.GetTAUsers();
        }
                
        public void CheckTAUsers(DataTable ExcelUsers, DataTable TAUsers)
        {
            bool UserFinder = false;

            for (int i = 0; i < ExcelUsers.Rows.Count; i++)
            {
                UserFinder = false;

                for (int j = 0; j < TAUsers.Rows.Count; j++)
                {
                    if (ExcelUsers.Rows[i]["LOGIN"].ToString() == TAUsers.Rows[j]["LOGIN"].ToString()
                         && ExcelUsers.Rows[i]["ClassCode"].ToString() == TAUsers.Rows[j]["ClassCode"].ToString())
                    {
                        UserFinder = true;
                        break;
                    }
                }
                            
                if (UserFinder == false)                
                    TADAO.AddToTAUser(ExcelUsers.Rows[i]["LOGIN"].ToString(), ExcelUsers.Rows[i]["Name"].ToString(), ExcelUsers.Rows[i]["Family"].ToString(), ExcelUsers.Rows[i]["Email"].ToString(), ExcelUsers.Rows[i]["ClassCode"].ToString());
                    
            }

        }

         
        /// <summary>
        /// داده های نهایی را گرفته و 
        /// ارفاق و میزان زمان حضور استادیار را مشخص میکند
        /// </summary>
        /// <param name="Leniency"></param>
        /// <param name="Nimsal"></param>
        /// <param name="Department"></param>
        /// <returns></returns>
        public DataTable GetAllTimeOfEachClass(int Leniency, string Nimsal, string Department)
        { 
            // دریافت داده بصورت کامل و نهایی
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


            // اضافه کردن آخرین رکورد
            DataRow row2 = DT3.NewRow();
            row2["Department"] = TimeOfEachClass_.Department;
            row2["GroupName"] = TimeOfEachClass_.GroupName;
            row2["LOGIN"] = TimeOfEachClass_.LOGIN;
            row2["TeacherName"] = TimeOfEachClass_.TeacherName;
            row2["code"] = TimeOfEachClass_.codeInt;
            row2["LessonName"] = TimeOfEachClass_.LessonName;
            row2["SumOfTime"] = TotalTime.ToString();
            row2["TimeClass"] = TotalClassTime.ToString();
            row2["SessionCount"] = TimeOfEachClass_.SessionCount.ToString();
            row2["Hozoor"] = TimeOfEachClass_.Hozoor.ToString();
            row2["Leniency"] = Leniency.ToString();
            row2["tterm"] = Nimsal;
            DT3.Rows.Add(row2);



            return DT3;
        }

        public DataTable SortedDataTableBothServer(string tterm, string Department)
        {          
            // TA تمام کاربران ثبت شده در جدول 
            DataTable DT_TA_Users = TADAO.GetTAUsers();
            DataTable DTProf_SidaInfo = TADAO.GetTAAssistantListByDepartment_Term(Department, tterm);
         
             
            DataTable tvp = new DataTable();
            tvp.Columns.Add("ProfID", typeof(String));
            tvp.Columns.Add("ClassID", typeof(String));
            DataRow workRow;
            for (int i = 0; i < DT_TA_Users.Rows.Count; i++)
            {
                workRow = tvp.NewRow();
                workRow[0] = DT_TA_Users.Rows[i]["LOGIN"].ToString();
                workRow[1] = DT_TA_Users.Rows[i]["ClassCode"].ToString();
                tvp.Rows.Add(workRow);
            }






            DataTable DT_TA = new DataTable();
            DT_TA.Columns.Add("Department", typeof(string));
            DT_TA.Columns.Add("GroupName", typeof(string));
            DT_TA.Columns.Add("LessonName", typeof(string));
            DT_TA.Columns.Add("code", typeof(string));
            DT_TA.Columns.Add("TimeClass", typeof(string));
            DT_TA.Columns.Add("TimeStart", typeof(string));
            DT_TA.Columns.Add("TimeEnd", typeof(string));

            for (int i = 0; i < DT_TA_Users.Rows.Count; i++)
            {
                for (int j = 0; j < DTProf_SidaInfo.Rows.Count; j++)// را یکی میکند Adobe اطلاعات دریافت شده از 
                {
                    if (DT_TA_Users.Rows[i]["ClassCode"].ToString() == DTProf_SidaInfo.Rows[j]["did"].ToString())
                    {
                        DataRow row = DT_TA.NewRow();
                        row["Department"] = DTProf_SidaInfo.Rows[j]["Department"].ToString();
                        row["GroupName"] = DTProf_SidaInfo.Rows[j]["namegroup"].ToString();
                        row["LessonName"] = DTProf_SidaInfo.Rows[j]["namedars"].ToString();
                        row["code"] = DT_TA_Users.Rows[i]["ClassCode"].ToString();
                        row["TimeClass"] = DTProf_SidaInfo.Rows[j]["TimeClass"].ToString();
                        row["TimeStart"] = DTProf_SidaInfo.Rows[j]["saatstart"].ToString();
                        row["TimeEnd"] = DTProf_SidaInfo.Rows[j]["saatend"].ToString();
                        DT_TA.Rows.Add(row);

                        break;
                    }
                }
            }
            
            DataTable DT1 = TADAO.GetTAAssistantTimeByArrayList(tvp);                        
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

         

            for (int i = 0; i < DT_TA.Rows.Count; i++)
            {
                for (int j = 0; j < DT1.Rows.Count; j++)// را یکی میکند Adobe اطلاعات دریافت شده از 
                {
                    //10068-977
                    // وقتی فقط کد کلاس را داشته باشیم یعنی 977 را داریم پس مجبوریم برای مقایسه فیلتر  کنیم
                    // این فیلتر توسط دستورات ذیل انجام می گیرد
                    String Adobestr = DT1.Rows[j]["NAME"].ToString();
                    String TAstr = DT_TA.Rows[i]["code"].ToString();
                    int TAcount = TAstr.Count();
                    String Code_Filter = Adobestr.Substring(Adobestr.Length - TAcount);
                  
                    if (DT_TA.Rows[i]["code"].ToString() == Code_Filter)
                    {
                        
                        DataRow row = DT11.NewRow();
                        row["Department"] = DT_TA.Rows[i]["Department"].ToString();
                        row["GroupName"] = DT_TA.Rows[i]["GroupName"].ToString();
                        row["LessonName"] = DT_TA.Rows[i]["LessonName"].ToString();                    
                        row["LOGIN"] = DT1.Rows[j]["LOGIN"].ToString();
                        row["TeacherName"] = DT1.Rows[j]["TeacherName"].ToString();
                        row["PersianDate"] = DT1.Rows[j]["PersianDate"].ToString();
                        row["SumOfTime"] = DT1.Rows[j]["SumOfTime"].ToString();
                        row["FirstLogin"] = DT1.Rows[j]["FirstLogin"].ToString();
                        row["LastLogOut"] = DT1.Rows[j]["LastLogOut"].ToString();
                        row["code"] = DT_TA.Rows[i]["code"].ToString();
                        row["TimeClass"] = DT_TA.Rows[i]["TimeClass"].ToString();
                        row["TimeStart"] = DT_TA.Rows[i]["TimeStart"].ToString();
                        row["TimeEnd"] = DT_TA.Rows[i]["TimeEnd"].ToString();                     
                        row["tterm"] = tterm;
                        int HozorCheck = 0;
                        if (DT1.Rows[j]["SumOfTime"].ToString() != "")
                            HozorCheck = int.Parse(DT1.Rows[j]["SumOfTime"].ToString());

                        int TimeClassCheck = 0;
                        if (DT_TA.Rows[i]["TimeClass"].ToString() != "")
                            TimeClassCheck = int.Parse(DT_TA.Rows[i]["TimeClass"].ToString());  

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
            int testRows2 = DT_TA_Users.Rows.Count;
            int testRows3 = DT1.Rows.Count;
            return sortedDT;
        }





        /// <summary>
        ///  استفاده شده در کلاس اطلاعات بصورت جزئی
        ///  
        /// </summary>
        /// <param name="ProfCode"></param>
        /// <param name="ClassCode"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="tterm"></param>
        /// <returns></returns>
        public DataTable GetSortedDataTableByProfCodeClassCode(string ProfCode, float ClassCode, string BeginDate, string EndDate, string tterm)
        {
            DataTable DT1 = new DataTable();
            DataTable DT2 = new DataTable();

            //if (tterm == "93-94-1")
            //{
            //    DT1 = TADAO.TimeByUserName_ClassCode(ProfCode, ClassCode, BeginDate, EndDate);
            //    DT2 = TADAO.Old_TimeByUserName_ClassCode(ProfCode, ClassCode, BeginDate, EndDate);
            //}
            //else
            DT2 = TADAO.TimeByUserName_ClassCode93942(ProfCode, ClassCode, BeginDate, EndDate);

            DataTable DT3 = new DataTable();
            DT3.Columns.Add("PersianDate", typeof(string));
            DT3.Columns.Add("TimeStart", typeof(string));
            DT3.Columns.Add("TimeEND", typeof(string));
            DT3.Columns.Add("FirstLogin", typeof(string));
            DT3.Columns.Add("LastLogOut", typeof(string));
            DT3.Columns.Add("SumOfTime", typeof(string));
            DT3.Columns.Add("TimeClass", typeof(string));
            DT3.Columns.Add("Late", typeof(string));
            DT3.Columns.Add("Soon", typeof(string));
            DT3.Columns.Add("Status", typeof(string));

            //// یکی کردن جداول
            //foreach (DataRow dr in DT1.Rows)
            //    DT2.Rows.Add(dr.ItemArray);

            DataTable DT10 = spDAO.GetStudentClassInfo(ClassCode.ToString(), tterm);
            DataTable DT11 = new DataTable();
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

            for (int i = 0; i < DT2.Rows.Count; i++)
            {
                DataRow row = DT11.NewRow();
                row["LOGIN"] = DT2.Rows[i]["LOGIN"].ToString();
                row["TeacherName"] = DT2.Rows[i]["TeacherName"].ToString();                
                row["PersianDate"] = DT2.Rows[i]["PersianDate"].ToString();
                row["SumOfTime"] = DT2.Rows[i]["SumOfTime"].ToString();
                row["FirstLogin"] = DT2.Rows[i]["FirstLogin"].ToString();
                row["LastLogOut"] = DT2.Rows[i]["LastLogOut"].ToString();
                row["code"] = DT10.Rows[0]["did"].ToString();
                row["TimeClass"] = DT10.Rows[0]["TimeClass"].ToString();
                row["TimeStart"] = DT10.Rows[0]["saatstart"].ToString();
                row["TimeEnd"] = DT10.Rows[0]["saatend"].ToString();
                row["LessonName"] = DT10.Rows[0]["namedars"].ToString();
                int HozorCheck = int.Parse(DT2.Rows[i]["SumOfTime"].ToString());
                int TimeClassCheck = int.Parse(DT10.Rows[0]["TimeClass"].ToString());

                if (HozorCheck > TimeClassCheck)
                    row["Hozoor"] = "1";
                else
                    row["Hozoor"] = "0";
                DT11.Rows.Add(row);
            }

            // مرتب کردن
            DataView dv = DT11.DefaultView;
            dv.Sort = "PersianDate";
            dv.Sort = "code";
            return dv.ToTable();

        }






    }
}
