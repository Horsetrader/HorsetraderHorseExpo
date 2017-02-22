using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HorsetraderHorseExpo
{
    public partial class view_grounds_map : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (Request["from"] != null)
            {
                goBack.Attributes.Add("href", Request["from"]);
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