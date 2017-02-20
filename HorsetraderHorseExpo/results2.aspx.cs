using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class results2 : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (!IsPostBack)
            {
                LoadMetaData();
                LoadEventSearchResult();
                LoadExhibitorSearchResult();
            }
        }

        private void LoadMetaData()
        {
            if (Request["q"] != null)
            {
                Page.MetaKeywords = Request["q"];
                resultsLabel.InnerHtml = string.Format("Results for \"{0}\"...", Request["q"]);
            }
        }

        private void LoadEventSearchResult()
        {
            int expoEventID = 2;
            String searchQuery = GetEventSearchQuery(expoEventID);
            DataSet dsResults = GetEventsBySearch(searchQuery, int.Parse(ConfigurationManager.AppSettings["HorseExpoPubNumber"]));
            DataTable dtEvents = dsResults.Tables.Count > 0 ? dsResults.Tables[0] : new DataTable();

            //Bind ad results data
            repEventResults.DataSource = dtEvents;
            repEventResults.DataBind();

            //Display result count
            tabEventsResults.InnerHtml = string.Format("Schedule ({0})", dtEvents.Rows.Count);
        }

        private void LoadExhibitorSearchResult()
        {
            String searchQuery = GetExhibitorSearchQuery();
            DataSet dsResults = GetExhibitorsBySearch(searchQuery);
            DataTable dtExhibitors = dsResults.Tables.Count > 0 ? dsResults.Tables[0] : new DataTable();

            //Bind ad results data
            repExhibitorResults.DataSource = dtExhibitors;
            repExhibitorResults.DataBind();

            //Display result count
            tabExhibitorsResults.InnerHtml = string.Format("Exhibitors ({0})", dtExhibitors.Rows.Count);
        }

        private String GetEventSearchQuery(int expoEventID)
        {
            String searchQuery = String.Empty;

            if (Request["q"] != null)
            {
                string searchText = Request["q"];

                if (!string.IsNullOrEmpty(searchText))
                {
                    int inetSearchValue = 0;

                    Boolean needAnd = false;
                    StringBuilder sb;

                    int expoEventId = int.Parse(ConfigurationManager.AppSettings["ExpoEventID"]);
                    sb = new StringBuilder("SELECT DISTINCT OrderNumber, AdText, sd.ExpoEventID " +
                    "FROM  " +
                    "AdText at, ShowDates sd " +
                    "WHERE " +
                    "at.OrderNumber = sd.Order_Number " +
                    "and sd.ExpoEventID = " + expoEventID);
                    needAnd = true;
                    string[] separators = { " " };
                    string[] searchTerms = searchText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string term in searchTerms)
                    {
                        if (needAnd) sb.Append(" AND ");
                        sb.Append("CONTAINS(AdText, '\"*" + term + "*\"')");
                        needAnd = true;
                    }

                    /*
                    sb = new StringBuilder("SELECT DISTINCT OrderNumber FROM AdText WHERE ");

                    if (inetSearchValue != 0)
                    {
                        sb.Append("InetType = " + inetSearchValue.ToString() + " ");
                        needAnd = true;
                    }
                    string[] separators = { " " };
                    string[] searchTerms = searchText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string term in searchTerms)
                    {
                        if (needAnd) sb.Append(" AND ");
                        sb.Append("CONTAINS(AdText, '\"*" + term + "*\"')");
                        needAnd = true;
                    }
                    */
                    sb.Append(" AND PubNumber = " + ConfigurationManager.AppSettings["HorseExpoPubNumber"]);

                    searchQuery = sb.ToString();
                }
            }

            return searchQuery;
        }

        private String GetExhibitorSearchQuery()
        {
            String searchQuery = String.Empty;

            if (Request["q"] != null)
            {
                string searchText = Request["q"];

                if (!string.IsNullOrEmpty(searchText))
                {
                    Boolean needAnd = false;
                    StringBuilder sb;

                    sb = new StringBuilder("SELECT ExpoVendorID, Name, Location, Booth, ImageFileName, IsAdvertiser FROM ExpoVendor WHERE ");

                    string[] separators = { " " };
                    string[] searchTerms = searchText.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string term in searchTerms)
                    {
                        if (needAnd) sb.Append(" AND ");
                        sb.Append("CONTAINS((Name,Headline,Description), '\"*" + term + "*\"')");
                        needAnd = true;
                    }

                    sb.Append(" AND ExpoEventID = " + ConfigurationManager.AppSettings["ExpoEventID"]);
                    sb.Append(" ORDER BY Name");

                    searchQuery = sb.ToString();
                }
            }

            return searchQuery;
        }

        private DataSet GetEventsBySearch(String searchQuery, int pubNumber)
        {
            DataSet dsOrders = new DataSet();
            if (!String.IsNullOrEmpty(searchQuery))
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Parameters.Add(new SqlParameter("SearchQuery", searchQuery));
                        cmd.Parameters.Add(new SqlParameter("PubNumber", pubNumber));
                        cmd.CommandText = "sp_ht_list_fullTextSearchResultsShowdates";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();

                        var adapter = new SqlDataAdapter(cmd);

                        try
                        {
                            adapter.Fill(dsOrders);
                        }
                        catch (Exception e)
                        {
                            //manage remote db when working local
                        }
                    }
                }
            }

            return dsOrders;
        }

        private DataSet GetExhibitorsBySearch(String searchQuery)
        {
            DataSet ds = new DataSet();
            if (!String.IsNullOrEmpty(searchQuery))
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = searchQuery;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();

                        var adapter = new SqlDataAdapter(cmd);

                        try
                        {
                            adapter.Fill(ds);
                        }
                        catch (Exception e)
                        {
                            //manage remote db when working local
                        }
                    }
                }
            }

            return ds;
        }
        #endregion

        #region Functions
        protected string GetImageHTML(string imageFile, string vendorName)
        {
            string html = string.Empty;

            if (!string.IsNullOrEmpty(imageFile))
            {
                html = string.Format(
                            "<img class='img-thumbnail vendor-thumbnail' src='{0}/{1}' alt='{2}'>",
                            ConfigurationManager.AppSettings["ImagesURL"],
                            imageFile,
                            vendorName);
            }

            return html;
        }

        protected string GetMoreDetailsButtonHTML(bool isAdvertiser, string vendorId)
        {
            string buttonHtml = string.Empty;
            if (isAdvertiser)
            {
                buttonHtml = "<a class='btn btn-default' href='exhibitor-details?id=" + vendorId + "' role='button'>" +
                                "More details <span class='glyphicon glyphicon-chevron-right'></span>" +
                             "</a>";
            }

            return buttonHtml;
        }

        protected string GetScheduleTime(string startDate, string startTime, string endTime)
        {
            string dateRange = string.Empty;

            DateTime date;
            DateTime.TryParse(startDate, out date);

            dateRange = date.ToString("ddddd MMMM d");

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                dateRange += string.Format(", {0} - {1}", startTime, endTime);
            else if (!string.IsNullOrEmpty(startTime))
                dateRange += string.Format(", {0}", startTime);

            return dateRange;
        }
        #endregion

        #region Event Handlers
        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater repeater = (Repeater)sender;

            if (repeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = new Label();
                    for (int i = 0; i < e.Item.Controls.Count; i++)
                    {
                        if (e.Item.Controls[i].GetType() == lblFooter.GetType())
                        {
                            lblFooter = (Label)e.Item.Controls[i];
                            lblFooter.Visible = true;
                            lblFooter.Text = "Hmm, no results for your search here... have you tried the other tab?";

                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(string.Format("{0}/search?q={1}",
                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], tbxSearch.Text));
        }
        #endregion
    }
}