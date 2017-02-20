using System;
using System.Configuration;

namespace HorsetraderHorseExpo.usercontrols
{
    public partial class search : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SetSearchValues();
        }

        private void SetSearchValues()
        {
            //Set textbox value
            try
            {
                if (Request["q"] != null)
                    tbxSearch.Text = Request["q"];
            }
            catch
            {
                tbxSearch.Text = string.Empty;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Response.RedirectPermanent(string.Format("Results.aspx?inet={0}&search={1}", ddlSearchCategories.SelectedValue, tbxSearch.Text));
            Response.RedirectPermanent(string.Format("{0}/search?q={1}",
                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], tbxSearch.Text));
        }
    }
}