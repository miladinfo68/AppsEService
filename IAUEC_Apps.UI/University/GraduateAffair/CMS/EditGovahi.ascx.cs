using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class EditGovahi : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.DataBinding += new System.EventHandler(this.NewsDetail_DataBinding);

        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        protected void NewsDetail_DataBinding(object sender, System.EventArgs e)
        {
            txt_Date.Text = DataBinder.Eval(DataItem, "date_namehaz").ToString();
            txt_Koja.Text = DataBinder.Eval(DataItem, "name_bekoja").ToString();
            ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک گواهینامه موقت", "4"));
            ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک دانشنامه", "6"));
            ddl_Govahi.Items.Insert(0, new ListItem("انتخاب کنید"));
            ddl_Govahi.SelectedValue = DataBinder.Eval(DataItem, "type_govahi").ToString();
            txt_LtterNo.Text = DataBinder.Eval(DataItem, "num_namehaz").ToString();
        }
    }
}