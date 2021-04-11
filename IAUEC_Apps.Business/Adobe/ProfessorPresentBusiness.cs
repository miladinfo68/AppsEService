using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.Business.Adobe
{
    public class ProfessorPresentBusiness
    {
        ProfessorPresentDAO ppDAO = new ProfessorPresentDAO();
        StuPresentDAO spDAO = new StuPresentDAO();

        public DataTable GivaAll()
        {
            // Interval Numbers of PRINCIPAL_ID
            DataTable DT1 = ppDAO.GiveListPrincipal();

            int MinPRINCIPAL = 0;
            int MaxPRINCIPAL = 0;
            int x = 0;
            int[,] xx = new int[2, 12];
            //int[,] xx = new int[2, 2];
            for (int i = 0; i < DT1.Rows.Count; i++)
            //for (int i = 0; i < 2; i++)
            {
                MinPRINCIPAL = int.Parse(DT1.Rows[i]["PRINCIPAL_ID"].ToString());
                xx[0, x] = MinPRINCIPAL;
                i = i + 49;

                if (i > DT1.Rows.Count)
                    MaxPRINCIPAL = int.Parse(DT1.Rows[DT1.Rows.Count - 1]["PRINCIPAL_ID"].ToString());
                else
                    MaxPRINCIPAL = int.Parse(DT1.Rows[i]["PRINCIPAL_ID"].ToString());

                xx[1, x] = MaxPRINCIPAL;
                x++;
            }

            DataTable DT2 = new DataTable();
            DT2.Columns.Add("LOGIN", typeof(string));
            DT2.Columns.Add("TeacherName", typeof(string));
            //DT2.Columns.Add("code", typeof(int));
            //DT2.Columns.Add("LessonName", typeof(string));
            DT2.Columns.Add("PersianDate", typeof(string));
            DT2.Columns.Add("SumOfTime", typeof(string));
            //DT2.Columns.Add("TimeClass", typeof(string));
            //DT2.Columns.Add("TimeStart", typeof(string));
            //DT2.Columns.Add("TimeEND", typeof(string));
            DT2.Columns.Add("FirstLogin", typeof(string));
            DT2.Columns.Add("LastLogOut", typeof(string));
            //DT2.Columns.Add("Hozoor", typeof(string));
            DT2.Columns.Add("NAME", typeof(string));

            for (int i = 0; i < 12; i++)
            {
                MinPRINCIPAL = xx[0, i];
                MaxPRINCIPAL = xx[1, i];
                DataTable DT3 = ppDAO.TimeByUserName_ClassCode(MinPRINCIPAL.ToString(), MaxPRINCIPAL.ToString());

                foreach (DataRow dr in DT3.Rows)
                    DT2.Rows.Add(dr.ItemArray);
            }

            DataView dv = DT2.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }

        public DataTable GivaAll_OLD()
        {
            // Interval Numbers of PRINCIPAL_ID
            DataTable DT1 = ppDAO.GiveListPrincipal_OLD();

            int MinPRINCIPAL = 0;
            int MaxPRINCIPAL = 0;
            int x = 0;
            int[,] xx = new int[2, 12];
            //int[,] xx = new int[2, 2];
            for (int i = 0; i < DT1.Rows.Count; i++)
            //for (int i = 0; i < 2; i++)
            {
                MinPRINCIPAL = int.Parse(DT1.Rows[i]["PRINCIPAL_ID"].ToString());
                xx[0, x] = MinPRINCIPAL;
                i = i + 49;

                if (i > DT1.Rows.Count)
                    MaxPRINCIPAL = int.Parse(DT1.Rows[DT1.Rows.Count - 1]["PRINCIPAL_ID"].ToString());
                else
                    MaxPRINCIPAL = int.Parse(DT1.Rows[i]["PRINCIPAL_ID"].ToString());

                xx[1, x] = MaxPRINCIPAL;
                x++;
            }


            DataTable DT2 = new DataTable();
            DT2.Columns.Add("LOGIN", typeof(string));
            DT2.Columns.Add("TeacherName", typeof(string));
            //DT2.Columns.Add("code", typeof(int));
            //DT2.Columns.Add("LessonName", typeof(string));
            DT2.Columns.Add("PersianDate", typeof(string));
            DT2.Columns.Add("SumOfTime", typeof(string));
            //DT2.Columns.Add("TimeClass", typeof(string));
            //DT2.Columns.Add("TimeStart", typeof(string));
            //DT2.Columns.Add("TimeEND", typeof(string));
            DT2.Columns.Add("FirstLogin", typeof(string));
            DT2.Columns.Add("LastLogOut", typeof(string));
            //DT2.Columns.Add("Hozoor", typeof(string));
            DT2.Columns.Add("NAME", typeof(string));

            for (int i = 0; i < 12; i++)
            {
                MinPRINCIPAL = xx[0, i];
                MaxPRINCIPAL = xx[1, i];
                DataTable DT3 = ppDAO.TimeByUserName_ClassCode_OLD(MinPRINCIPAL.ToString(), MaxPRINCIPAL.ToString());

                foreach (DataRow dr in DT3.Rows)
                    DT2.Rows.Add(dr.ItemArray);
            }

            DataView dv = DT2.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }

        public DataTable SortedDataTableBothServer(string tterm, int iddanesh)
        {
            DataTable DT1 = new DataTable();
            DataTable DT0 = new DataTable();

            if (tterm == "93-94-1")
            {
                DT1 = GivaAll();
                DT0 = GivaAll_OLD();
                foreach (DataRow dr in DT0.Rows)
                    DT1.Rows.Add(dr.ItemArray);
            }
            else if (tterm == "94-95-2")
            {
                DT1 = ppDAO.TimeByUserName_ClassCodeByTerm(tterm);
            }
            else
            {
                DT1 = GivaAllByTerm(tterm);
            }


            DataTable DT10 = spDAO.GetStudentClassInfo(tterm, iddanesh);
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
            DT11.Columns.Add("tterm", typeof(string));
            DT11.Columns.Add("namedanesh", typeof(string));
            DT11.Columns.Add("v_nazari", typeof(string));
            DT11.Columns.Add("sco_ID", typeof(string));
            DT11.Columns.Add("mahal_klas", typeof(string));
            for (int i = 0; i < DT10.Rows.Count; i++)
            {
                for (int j = 0; j < DT1.Rows.Count; j++)
                {
                    //if (DT10.Rows[i]["Code"].ToString() == "15221153-1775")
                    //{

                    //}
                    if (DT10.Rows[i]["Code"].ToString() == DT1.Rows[j]["NAME"].ToString() && "ins" + DT10.Rows[i]["idostad"].ToString() == DT1.Rows[j]["LOGIN"].ToString())

                    {
                        DataRow row = DT11.NewRow();
                        row["LOGIN"] = DT1.Rows[j]["LOGIN"].ToString();
                        row["TeacherName"] = DT1.Rows[j]["TeacherName"].ToString();
                        row["PersianDate"] = DT1.Rows[j]["PersianDate"].ToString();
                        row["SumOfTime"] = DT1.Rows[j]["SumOfTime"].ToString();
                        row["FirstLogin"] = DT1.Rows[j]["FirstLogin"].ToString();
                        row["LastLogOut"] = DT1.Rows[j]["LastLogOut"].ToString();
                        row["code"] = DT10.Rows[i]["did"].ToString();
                        row["TimeClass"] = DT10.Rows[i]["TimeClass"].ToString();
                        row["TimeStart"] = DT10.Rows[i]["saatstart"].ToString();
                        row["TimeEnd"] = DT10.Rows[i]["saatend"].ToString();
                        row["tterm"] = tterm;
                        row["LessonName"] = DT10.Rows[i]["namedars"].ToString();
                        row["namedanesh"] = DT10.Rows[i]["namedanesh"].ToString();
                        row["v_nazari"] = DT10.Rows[i]["v_nazari"].ToString();
                        row["mahal_klas"] = DT10.Rows[i]["mahal_klas"].ToString();
                        //  row["sco_ID"] = DT1.Rows[j]["SCO_ID"].ToString();
                        int HozorCheck = 0;
                        if (DT1.Rows[j]["SumOfTime"].ToString() != "")
                        {
                            HozorCheck = int.Parse(DT1.Rows[j]["SumOfTime"].ToString());
                        }

                        int TimeClassCheck = 0;
                        if (DT10.Rows[i]["TimeClass"].ToString() != "")
                        {
                            TimeClassCheck = int.Parse(DT10.Rows[i]["TimeClass"].ToString());
                        }


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
            return sortedDT;
        }
        //1':14"

        public DataTable GetAllTimeOfEachClass(DataTable sortedDT, int Leniency)
        {
            DataTable DT3 = new DataTable();
            DT3.Columns.Add("LOGIN", typeof(string));
            DT3.Columns.Add("TeacherName", typeof(string));
            // DT3.Columns.Add("code", typeof(string));
            DT3.Columns.Add("code", typeof(int));
            DT3.Columns.Add("LessonName", typeof(string));
            DT3.Columns.Add("SumOfTime", typeof(string));
            DT3.Columns.Add("TimeClass", typeof(string));
            DT3.Columns.Add("SessionCount", typeof(string));
            DT3.Columns.Add("Hozoor", typeof(string));
            DT3.Columns.Add("Leniency", typeof(string));
            DT3.Columns.Add("tterm", typeof(string));
            DT3.Columns.Add("namedanesh", typeof(string));
            DT3.Columns.Add("v_nazari", typeof(string));
            DT3.Columns.Add("mahal_klas", typeof(string));
            TimeOfEachClass TimeOfEachClass_ = new TimeOfEachClass();
            TimeOfEachClass_.SumOfTime = 0;
            string Code = "";
            string ScoID = "";
            long TotalTime = 0;
            long TotalClassTime = 0;

            for (int i = 0; i < sortedDT.Rows.Count; i++)
            {
                //if (sortedDT.Rows[i]["code"].ToString() == "1610")
                //{
                if (Code != sortedDT.Rows[i]["code"].ToString())
                {
                    if (i > 0)
                    {
                        DataRow row = DT3.NewRow();
                        row["LOGIN"] = TimeOfEachClass_.LOGIN;
                        row["TeacherName"] = TimeOfEachClass_.TeacherName;
                        row["code"] = TimeOfEachClass_.codeInt;
                        row["LessonName"] = TimeOfEachClass_.LessonName;
                        row["SumOfTime"] = TotalTime.ToString();
                        row["TimeClass"] = TotalClassTime.ToString();
                        row["SessionCount"] = TimeOfEachClass_.SessionCount.ToString();
                        row["Hozoor"] = TimeOfEachClass_.Hozoor.ToString();
                        row["Leniency"] = Leniency.ToString();
                        row["tterm"] = TimeOfEachClass_.tterm;
                        row["namedanesh"] = TimeOfEachClass_.Department;
                        row["mahal_klas"] = TimeOfEachClass_.mahalKlas;


                        DT3.Rows.Add(row);
                        TotalTime = 0;
                        TotalClassTime = 0;
                    }

                    TimeOfEachClass_.LOGIN = sortedDT.Rows[i]["LOGIN"].ToString();
                    TimeOfEachClass_.TeacherName = sortedDT.Rows[i]["TeacherName"].ToString();
                    // TimeOfEachClass_.code = sortedDT.Rows[i]["code"].ToString();
                    TimeOfEachClass_.codeInt = int.Parse(sortedDT.Rows[i]["code"].ToString());
                    TimeOfEachClass_.LessonName = sortedDT.Rows[i]["LessonName"].ToString();
                    TimeOfEachClass_.mahalKlas = sortedDT.Rows[i]["mahal_klas"].ToString();
                    if (sortedDT.Rows[i]["SumOfTime"].ToString() != "")
                    {
                        TimeOfEachClass_.SumOfTime = int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());
                    }
                    else
                    {
                        TimeOfEachClass_.SumOfTime = 0;
                    }
                    if (sortedDT.Rows[i]["TimeClass"].ToString() == "")
                    {
                        TimeOfEachClass_.TimeClass = 0;
                    }
                    else
                    {
                        TimeOfEachClass_.TimeClass = int.Parse(sortedDT.Rows[i]["TimeClass"].ToString());
                    }

                    TimeOfEachClass_.tterm = sortedDT.Rows[i]["tterm"].ToString();
                    TimeOfEachClass_.Department = sortedDT.Rows[i]["namedanesh"].ToString();
                    TimeOfEachClass_.SessionCount = 1;
                    if ((TimeOfEachClass_.SumOfTime + (Leniency * int.Parse(sortedDT.Rows[i]["v_nazari"].ToString()))) - TimeOfEachClass_.TimeClass >= 0)
                        TimeOfEachClass_.Hozoor = 1;
                    else
                        TimeOfEachClass_.Hozoor = 0;

                    TotalTime = TotalTime + TimeOfEachClass_.SumOfTime;
                    TotalClassTime = TotalClassTime + TimeOfEachClass_.TimeClass;

                    Code = TimeOfEachClass_.codeInt.ToString();
                    ScoID = TimeOfEachClass_.SCO_ID;
                }
                //--------------------
                else if (
                    !(sortedDT.Rows[i]["code"].ToString() == sortedDT.Rows[i - 1]["code"].ToString()
                    && sortedDT.Rows[i]["PersianDate"].ToString() == sortedDT.Rows[i - 1]["PersianDate"].ToString()
                    && sortedDT.Rows[i]["FirstLogin"].ToString() == sortedDT.Rows[i - 1]["FirstLogin"].ToString()
                    && sortedDT.Rows[i]["LastLogOut"].ToString() == sortedDT.Rows[i - 1]["LastLogOut"].ToString()
                    ) ||
                    (sortedDT.Rows[i]["code"].ToString() == sortedDT.Rows[i - 1]["code"].ToString() && sortedDT.Rows[i]["SCO_ID"].ToString() != sortedDT.Rows[i - 1]["SCO_ID"].ToString() &&
                     sortedDT.Rows[i]["PersianDate"].ToString() == sortedDT.Rows[i - 1]["PersianDate"].ToString()
                    )
                )
                {
                    if (sortedDT.Rows[i]["SumOfTime"].ToString() == "")
                    {
                        TimeOfEachClass_.SumOfTime = 0;
                    }
                    else
                    {
                        TimeOfEachClass_.SumOfTime = int.Parse(sortedDT.Rows[i]["SumOfTime"].ToString());
                    }
                    if (sortedDT.Rows[i]["TimeClass"].ToString() == "")
                    {
                        TimeOfEachClass_.TimeClass = 0;
                    }
                    else
                    {
                        TimeOfEachClass_.TimeClass = int.Parse(sortedDT.Rows[i]["TimeClass"].ToString());
                    }

                    TimeOfEachClass_.SessionCount = TimeOfEachClass_.SessionCount + 1;
                    if ((TimeOfEachClass_.SumOfTime + (Leniency * int.Parse(sortedDT.Rows[i]["v_nazari"].ToString()))) - TimeOfEachClass_.TimeClass >= 0)
                        TimeOfEachClass_.Hozoor = TimeOfEachClass_.Hozoor + 1;

                    // اضافه کرن مجموع زمان حضور
                    TotalTime = TotalTime + TimeOfEachClass_.SumOfTime;
                    // اضافه کردن مجموع زمان کلاس                    
                    TotalClassTime = TotalClassTime + TimeOfEachClass_.TimeClass;

                    TimeOfEachClass_.SumOfTime = int.Parse(TotalTime.ToString());
                    TimeOfEachClass_.TimeClass = int.Parse(TotalClassTime.ToString());
                }

                //}
            }
            return DT3;

        }

        //=======================================
        public DataTable GivaAllByTerm(string term)
        {
            //DataTable DT1 = ppDAO.GiveListPrincipal93942();
            DataTable DT1 = ppDAO.GiveListPrincipalbyTerm(term);
            int MinPRINCIPAL = 0;
            int MaxPRINCIPAL = 0;
            int RowNum = 0;
            if (DT1.Rows.Count % 50 == 0)
                RowNum = DT1.Rows.Count / 50;
            else
                RowNum = (DT1.Rows.Count / 50) + 1;

            int x = 0;
            int[,] xx = new int[2, RowNum];

            for (int i = 0; i < DT1.Rows.Count; i++)
            {
                MinPRINCIPAL = int.Parse(DT1.Rows[i]["PRINCIPAL_ID"].ToString());
                xx[0, x] = MinPRINCIPAL;
                i = i + 49;

                if (i > DT1.Rows.Count)
                    MaxPRINCIPAL = int.Parse(DT1.Rows[DT1.Rows.Count - 1]["PRINCIPAL_ID"].ToString());
                else
                    MaxPRINCIPAL = int.Parse(DT1.Rows[i]["PRINCIPAL_ID"].ToString());

                xx[1, x] = MaxPRINCIPAL;
                x++;
            }

            DataTable DT2 = new DataTable();
            DT2.Columns.Add("LOGIN", typeof(string));
            DT2.Columns.Add("TeacherName", typeof(string));
            DT2.Columns.Add("PersianDate", typeof(string));
            DT2.Columns.Add("SumOfTime", typeof(string));
            DT2.Columns.Add("FirstLogin", typeof(string));
            DT2.Columns.Add("LastLogOut", typeof(string));
            DT2.Columns.Add("NAME", typeof(string));
            DT2.Columns.Add("SCO_ID", typeof(int));
            for (int i = 0; i < RowNum; i++)
            {
                MinPRINCIPAL = xx[0, i];
                MaxPRINCIPAL = xx[1, i];

                DataTable DT3 = ppDAO.TimeByUserName_ClassCodeByTerm(MinPRINCIPAL.ToString(), MaxPRINCIPAL.ToString(), term);
                foreach (DataRow dr in DT3.Rows)
                    DT2.Rows.Add(dr.ItemArray);
            }

            DataView dv = DT2.DefaultView;
            dv.Sort = "PersianDate";
            DataTable sortedDT = dv.ToTable();
            return sortedDT;
        }





    }
}
