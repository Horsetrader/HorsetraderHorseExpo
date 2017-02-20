using System;
using System.Configuration;

namespace HorsetraderHorseExpo
{
    public partial class view_map : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (Request["id"] != null && Request["f"] != null && Request["t"] != null)
            {
                string type = Request["t"].ToString() == "exhibitor" ? "exhibitor" : "event";
                string url = string.Format("{0}/{1}-details?id={2}",
                    ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], type, Request["id"]);

                goBack.Attributes.Add("href", url);
                verticalMap.Attributes.Add("src", string.Format("{0}/{1}", ConfigurationManager.AppSettings["ImagesURL"], FormatFileName(Request["f"])));
                horizontalMap.Attributes.Add("src", string.Format("{0}/{1}", ConfigurationManager.AppSettings["ImagesURL"], Request["f"]));
            }
        }
        #endregion

        #region Private Methods
        private string FormatFileName(string mapFileName)
        {
            try
            {
                mapFileName = mapFileName.Insert(mapFileName.IndexOf("."), "-90");
            }
            catch { }
            
            return mapFileName;
        }
        #endregion
    }
}