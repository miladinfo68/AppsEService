using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using System.ComponentModel;

namespace IAUEC_Apps.Business.Adobe
{
   public class ProfessorPresentDetailsBusiness
    { 
       ProfessorPresentDetailsDAO pdao = new ProfessorPresentDetailsDAO();
       StuPresentDAO spDAO = new StuPresentDAO();
//E:\TFS\IAUEC_Apps\New IAUEC_Apps\IAUEC_Apps\IAUEC_Apps.Business\Adobe\ProfessorPresentDetailsBusiness.cs
       public DataTable GetSortedDataTableByProfCodeClassCode(string ProfCode, int ClassCode, string BeginDate, string EndDate,string tterm)
       {
           DataTable DT1 = new DataTable();
           DataTable DT2 = new DataTable();

           if (tterm == "93-94-1")
           {
               DT1 = pdao.TimeByUserName_ClassCode(ProfCode, ClassCode, BeginDate, EndDate);
               DT2 = pdao.Old_TimeByUserName_ClassCode(ProfCode, ClassCode, BeginDate, EndDate);             
           }
           else
               DT2 = pdao.TimeByUserName_ClassCode(ProfCode, ClassCode, BeginDate, EndDate, tterm);
           
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
                      
           // یکی کردن جداول
           foreach (DataRow dr in DT1.Rows)
               DT2.Rows.Add(dr.ItemArray);

           DataTable DT10 = spDAO.GetStudentClassInfo(ClassCode.ToString(),tterm); 
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
           DT11.Columns.Add("v_nazari", typeof(string));
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
               row["TimeStart"] = DT2.Rows[i]["start_time"].ToString();
               row["TimeEnd"] = DT2.Rows[i]["end_time"].ToString();
               row["v_nazari"] = DT10.Rows[0]["v_nazari"].ToString();
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

       public DataTable ConvertToDataTable<T>(IList<T> data)
       {
           PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
           DataTable table = new DataTable();
           foreach (PropertyDescriptor prop in properties)
               table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
           foreach (T item in data)
           {
               DataRow row = table.NewRow();
               foreach (PropertyDescriptor prop in properties)
                   row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
               table.Rows.Add(row);
           }
           return table;
       }



          
    }
}
