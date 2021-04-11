using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace ResourceControl.BLL
{
    public static class UtilityFunction
    {
        public static bool IsMasouleDaftarUser(int roleId)
        {
            return (roleId == 73 || roleId == 55 || roleId == 57 || roleId == 62 || roleId == 64 || roleId == 58 || roleId == 59 || roleId == 60 || roleId == 61 || roleId == 63 || roleId == 65 || roleId == 74 || roleId == 75);

        }

        public static int IsMasouleDorehKootahModat(int roleId)
        {
            if ((roleId > 40 && roleId < 49) || roleId == 13 || roleId == 14)
                return roleId;
            else
                return 0;
        }

        public static long TimeToTicks(this string time)
        {
          

            TimeSpan result;
            TimeSpan.TryParse(time, out result);
            return result.Ticks;
        }

        public static string StatuseCondition(this string status)
        {
            if (!string.IsNullOrWhiteSpace(status) && !string.IsNullOrEmpty(status) && status != 0.ToString())
            {
                if (status == 2.ToString())
                {
                    return "تایید";
                }
                else
                {
                    return "در گردش";
                }
            }
            else
            {
                return "";
            }
        }


        public static List<T> ConvertDataTableToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value && dr[column.ColumnName] != null)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }
        public static System.Drawing.Image  AddressToImage(string Address)
        {
            var img = new System.Drawing.Bitmap(Address);
            byte[] array1 = imageToByteArray(img);

            MemoryStream ms = new MemoryStream(array1);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
        public static byte[] AddressToByte(string Address)
        {
            var img = new System.Drawing.Bitmap(Address);
            byte[] array1 = imageToByteArray(img);

            MemoryStream ms = new MemoryStream(array1);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return array1;
        }
        public static string ConvertScoreToDegree(decimal score)
        {
            if (score < 14)
            {
                return " مردود " + " ( " + " کمتر از 14 " + " ) ";

            }
            else if (score >= 14 && score < 16)
            {
                return " متوسط " + " ( " + "15.99-14" + " ) " ;

            }
            else if (score >= 16 && score < 18)
            {
                return " خوب " + " ( " + "17.99-16" + " ) " ;

            }
            else if (score >= 18 && score < 19)
            {
                return " خیلی خوب "+"(" + "18.99-18" + ")" ;

            }
            else if (score >= 19 && score <= 20)
            {
                return " عالی " + "(" + "20-19" + ")"  ;
            }
            else return "";
        }

    }
}