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
    public partial class results : System.Web.UI.Page
    {
        #region Global Variables
        protected string EventOrderNumbers
        {
            get
            {
                if (ViewState["EventOrderNumbers"] != null)
                    return ViewState["EventOrderNumbers"].ToString();
                else if (Request.Cookies["EventOrderNumbers"] != null)
                    return Request.Cookies["EventOrderNumbers"].Value;
                else
                    return string.Empty;
            }
            set
            {
                ViewState["EventOrderNumbers"] = value;
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (!IsPostBack)
            {
                LoadEventOrderNumbers();
                LoadMetaData();
                LoadAdSearchResult(false);
            }

            AttachEventHandlerToUserControlButtonClick();
        }

        private void LoadEventOrderNumbers()
        {
            if (Request.Cookies["EventOrderNumbers"] != null)
                EventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;
        }

        private void AttachEventHandlerToUserControlButtonClick()
        {
            ucEventQueue.ibtnRemoveFromListClicked += new EventHandler(UcEventQueue_IbtnRemoveFromListClicked);
        }

        private void LoadMetaData()
        {
            if (Request["q"] != null)
                Page.MetaKeywords = Request["q"];
        }

        private void LoadAdSearchResult(bool sortData)
        {
            int categoryGroupID = -1;
            if (Request["gid"] != null)
                int.TryParse(Request["gid"], out categoryGroupID);

            String searchQuery = GetSearchQuery();
            DataSet dsResults = GetAdsBySearch(searchQuery, int.Parse(ConfigurationManager.AppSettings["HorseExpoPubNumber"]));
            DataTable dtAds = dsResults.Tables.Count > 0 ? dsResults.Tables[0] : new DataTable();
            //DataTable dtCategories = dsResults.Tables.Count > 1 ? dsResults.Tables[1] : new DataTable();

            //Bind ad results data
            DataView dvAdList = new DataView(dtAds);
            dvAdList = sortData ? SortData(dvAdList) : dvAdList;

            repAds.DataSource = dvAdList;
            repAds.DataBind();

            if (Request["q"] != null)
                lblDisplayingFor.Text = string.Format("Results for '{0}'", Request["q"].Length > 50 ? Request["q"].Substring(0, 50) + "..." : Request["q"]);

            lblTotalRecords.Text = String.Format(" - {0} listings found", repAds.Items.Count);
        }

        private String GetSearchQuery()
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

                    sb.Append(" AND PubNumber = " + ConfigurationManager.AppSettings["HorseExpoPubNumber"]);

                    searchQuery = sb.ToString();
                }
            }

            return searchQuery;
        }

        private DataSet GetAdsBySearch(String searchQuery, int pubNumber)
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
        #endregion

        #region Functions
        protected string GenerateImageHTML(string imageFile)
        {
            string html = string.Empty;

            if (!string.IsNullOrEmpty(imageFile))
            {
                html = string.Format(
                            "<a href='{0}/{1}' class='example5 cboxElement'>" +
                                "<img src='{0}/{1}' width='100px'>" +
                            "</a>",
                            ConfigurationManager.AppSettings["ImagesURL"],
                            imageFile);
            }

            return html;
        }

        protected string SetDateRangeInfo(string startDate, string startTime, string endTime)
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

        protected string SetMap(string subFacilityMap)
        {
            string html = string.Empty;

            if (!string.IsNullOrEmpty(subFacilityMap))
            {
                html = string.Format(
                            "<a href='{0}/{1}'" +
                                "class='example55 sexybutton sexysimple sexysmall style='margin-right:-6px;'>" +
                                "<span class='map'>Show Map</span>" +
                            "</a>",
                            ConfigurationManager.AppSettings["ImagesURL"],
                            subFacilityMap);
            }

            return html;
        }

        private DataView SortData(DataView dvAdList)
        {
            try
            {
                dvAdList.Sort = ddlSort.SelectedValue;
            }
            catch (Exception)
            { }

            return dvAdList;
        }

        protected string SetAddToListImage(string orderNumber)
        {
            string imageURL = "~/assets/images/add-to-list.png";

            if (EventOrderNumbers.Contains(orderNumber))
                imageURL = "~/assets/images/on-my-list.png";

            return imageURL;
        }

        private void LoadEventList()
        {
            try
            {
                Repeater repEventList = (Repeater)ucEventQueue.FindControl("repEventList");

                if (!string.IsNullOrEmpty(EventOrderNumbers))
                {
                    string[] eventOrderNumbersArray = EventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> eventList = new List<string>(eventOrderNumbersArray);
                    DataSet dsEventList = ShowdateBLL.ListByOrderNumber(eventList);
                    DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();

                    repEventList.DataSource = dvEventList;
                    repEventList.DataBind();
                }
                else
                {
                    repEventList.DataSource = new DataView();
                    repEventList.DataBind();
                }
            }
            catch
            { ;}
        }
        #endregion

        #region Event Handlers
        protected void repAds_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item && e.Item.ItemIndex == 0)
            {
                HtmlGenericControl divResult = (HtmlGenericControl)e.Item.FindControl("divResult");
                divResult.Attributes.Add("class", "searchresultsingle_right first-item");
            }

            if (repAds.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                    lblFooter.Text = "Sorry, there are currently no events related to your search";
                }
            }
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAdSearchResult(true);
        }

        protected void ibtnAddToList_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string eventOrderNumbers = string.Empty;
            ImageButton ibtnAddToList = (ImageButton)sender;
            bool addedToList = ibtnAddToList.ImageUrl.Contains("on-my-list");
            string orderNumber = ibtnAddToList.CommandArgument;

            //Check if list is in Cookies
            if (Request.Cookies["EventOrderNumbers"] != null)
            {
                eventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;
                addedToList = eventOrderNumbers.Contains(orderNumber);

                //Add to string chain (validate if not in it already)
                if (!addedToList)
                    eventOrderNumbers += orderNumber + "|";
                else
                    //Remove from list if already in it
                    eventOrderNumbers = eventOrderNumbers.Replace(orderNumber + "|", "");
            }
            else
                eventOrderNumbers += orderNumber + "|";

            //Add string chain to Cookie
            EventOrderNumbers = eventOrderNumbers;
            Response.Cookies["EventOrderNumbers"].Value = eventOrderNumbers;
            Response.Cookies["EventOrderNumbers"].Expires = DateTime.Now.AddDays(90);

            //Change ImageButton image to "on my list"
            ibtnAddToList.ImageUrl = addedToList ? "~/assets/images/add-to-list.png" : "~/assets/images/on-my-list.png";
            //Load EventList usercontrol
            LoadEventList();
        }

        protected void UcEventQueue_IbtnRemoveFromListClicked(object sender, EventArgs e)
        {
            //Remove order number from Cookie
            if (Session["OrderNumberToRemove"] != null)
            {
                string orderNumber = (string)Session["OrderNumberToRemove"];
                string eventOrderNumbers = string.Empty;

                if (Request.Cookies["EventOrderNumbers"] != null)
                {
                    eventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;

                    if (eventOrderNumbers.Contains(orderNumber))
                        eventOrderNumbers = eventOrderNumbers.Replace(orderNumber + "|", "");
                }

                EventOrderNumbers = eventOrderNumbers;
                Session["OrderNumberToRemove"] = null;
            }

            bool sortData = ddlSort.SelectedIndex != 0;
            LoadAdSearchResult(sortData);
        }
        #endregion
    }
}