using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Helper
{
    public static class CustomHelper
    {
        public static DataTable ConvertDataGridToDataTable(RadGrid MyGrid)
        {
            DataTable dtable = new DataTable();

            int columncount = 0;

            foreach (GridColumn column in MyGrid.MasterTableView.Columns)
            {
                if (!string.IsNullOrEmpty(column.UniqueName) && !string.IsNullOrEmpty(column.HeaderText))
                {
                    columncount++;
                    dtable.Columns.Add(column.UniqueName, typeof(string));
                }
            }

            DataRow dr;
            foreach (GridDataItem item in MyGrid.MasterTableView.Items)
            {
                dr = dtable.NewRow();

                for (int i = 0; i < columncount; i++)
                {
                    dr[i] = item[MyGrid.MasterTableView.Columns[i].UniqueName].Text;
                }

                dtable.Rows.Add(dr);
            }
            return dtable;
        }
        public static void ExcuteFilter(RadGrid MyGrid)
        {
            GridFilterMenu menu = MyGrid.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                    }
                }
            }
        }

        public static void FilterPersian(RadGrid MyGrid)
        {
            GridFilterMenu menu = MyGrid.FilterMenu;
            foreach (RadMenuItem item in menu.Items)
            {  
                if (item.Text == "NoFilter")
                {
                    item.Text = "حذف فیلتر";
                }
                if (item.Text == "Contains")
                {
                    item.Text = "شامل";
                }
                if (item.Text == "EqualTo")
                {
                    item.Text = "مساوی با";
                }
            }
        }
    }
}