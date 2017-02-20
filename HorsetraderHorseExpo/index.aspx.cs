using System;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class index : System.Web.UI.Page
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
                //LoadFilters();
                LoadShows(false);
            }

            LoadPageDisplay();
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

        private void LoadPageDisplay()
        {
            //Load current year for title
            int year = DateTime.Now.Year;
            //DateTime startDate;
            //DateTime endDate;
            //DateTime.TryParse(tbxAltStartDate.Text, out startDate);
            //DateTime.TryParse(tbxAltEndDate.Text, out endDate);
            //If user filters by date, take the highest Year value
            //if (startDate.Year != year || endDate.Year != year)
                //year = endDate.Year > startDate.Year ? endDate.Year : startDate.Year;

            spanTitle.InnerText = string.Format("2014 Western States Horse Expo Calendar", year);
        }

        //private void LoadFilters()
        //{
        //    //Initialize date filters
        //    DateTime startDate = GetFilterStartDate();
        //    DateTime endDate = GetFilterEndDate();
        //    tbxStartDate.Text = startDate.ToString("ddd. MMMM d"); //DateTime.Today.ToString("ddd. MMMM d");
        //    tbxEndDate.Text = endDate.ToString("ddd. MMMM d"); //DateTime.Today.AddDays(90).ToString("ddd. MMMM d");
        //    tbxAltStartDate.Text = startDate.ToString("MM/dd/yyyy");
        //    tbxAltEndDate.Text = endDate.ToString("MM/dd/yyyy");

        //    //Configuration specific to Windows with different date setting (day/month/year)
        //    if (bool.Parse(ConfigurationManager.AppSettings["IsInDevelopmentEnvironment"]))
        //    {
        //        tbxAltStartDate.Text = startDate.ToString("dd/MM/yyyy");
        //        tbxAltEndDate.Text = endDate.ToString("dd/MM/yyyy");
        //    }
        //}

        public void LoadShows(bool sortData)
        {
            try
            {
                //Apply date range filter
                DateTime startDate = GetFilterStartDate();
                DateTime endDate = GetFilterEndDate();

                //if (DateTime.TryParse(tbxAltStartDate.Text, out startDate) ||
                //    DateTime.TryParse(tbxAltEndDate.Text, out endDate))
                //{
                //    tbxStartDate.Text = startDate.ToString("ddd. MMMM d");
                //    tbxEndDate.Text = endDate.ToString("ddd. MMMM d");
                //}

                //Get PubNumber
                int pubNumber = int.Parse(ConfigurationManager.AppSettings["HorseExpoPubNumber"]);

                DataView dvShows = GetShows(startDate, endDate, pubNumber);
                dvShows = sortData ? SortData(dvShows) : dvShows;
                repShows.DataSource = dvShows;
                repShows.DataBind();

                lblTotalRecords.Text = String.Format(" - {0} listings found", repShows.Items.Count);
            }
            catch
            { ;}
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
                                "class='example55 sexybutton sexysimple sexysmall'>" +
                                "<span class='map'>Show Map</span>" +
                            "</a>",
                            ConfigurationManager.AppSettings["ImagesURL"],
                            subFacilityMap);
            }

            return html;
        }

        private DataView GetShows(DateTime startDate, DateTime endDate, int pubNumber)
        {
            DataSet dsShows = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.Add(new SqlParameter("@StartDate", startDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", endDate));
                    cmd.Parameters.Add(new SqlParameter("@PubNumber", pubNumber));
                    cmd.CommandText = "sp_ht_list_showsForExpo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    var adapter = new SqlDataAdapter(cmd);

                    try
                    {
                        adapter.Fill(dsShows);
                    }
                    catch (Exception e)
                    {
                        //manage remote db when working local
                    }
                }
            }

            if (dsShows.Tables.Count > 0)
                return new DataView(dsShows.Tables[0]);

            return new DataView();
        }

        private DataView SortData(DataView dvAdList)
        {
            try
            {
                dvAdList.Sort = ddlSort.SelectedValue;
            }
            catch (Exception e)
            { }

            return dvAdList;
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

        protected string SetAddToListImage(string orderNumber)
        {
            string imageURL = "~/assets/images/add-to-list.png";

            if (EventOrderNumbers.Contains(orderNumber))
                    imageURL = "~/assets/images/on-my-list.png";

            return imageURL;
        }

        private DateTime GetFilterStartDate()
        {
            int startingDay = 7;
            int startingMonth = 2;
            int startingYear = 2014;
            rbAllDays.Attributes.Add("style", "color:#000");
            rbFriday.Attributes.Add("style", "color:#000");
            rbSaturday.Attributes.Add("style", "color:#000");
            rbSunday.Attributes.Add("style", "color:#000");

            if (rbAllDays.Checked)
                rbAllDays.Attributes.Add("style", "color:#df263c");
            else if (rbFriday.Checked)
                rbFriday.Attributes.Add("style", "color:#df263c");
            else if (rbSaturday.Checked)
            {
                startingDay = 8;
                rbSaturday.Attributes.Add("style", "color:#df263c");
            }
            else if (rbSunday.Checked)
            {
                startingDay = 9;
                rbSunday.Attributes.Add("style", "color:#df263c");
            }

            //Get default date filters from Web.config
            //int startingDay = int.Parse(ConfigurationManager.AppSettings["DateFilterStartingDay"].ToString());
            //int startingMonth = int.Parse(ConfigurationManager.AppSettings["DateFilterStartingMonth"].ToString());
            //int startingYear = int.Parse(ConfigurationManager.AppSettings["DateFilterStartingYear"].ToString());

            return new DateTime(startingYear, startingMonth, startingDay);
        }

        private DateTime GetFilterEndDate()
        {
            int endingDay = 9;
            int endingMonth = 2;
            int endingYear = 2014;

            if (rbFriday.Checked)
                endingDay = 7;
            else if (rbSaturday.Checked)
                endingDay = 8;
            else if (rbSunday.Checked)
                endingDay = 9;

            //Get default date filters from Web.config
            //int endingDay = int.Parse(ConfigurationManager.AppSettings["DateFilterEndingDay"].ToString());
            //int endingMonth = int.Parse(ConfigurationManager.AppSettings["DateFilterEndingMonth"].ToString());
            //int endingYear = int.Parse(ConfigurationManager.AppSettings["DateFilterEndingYear"].ToString());

            return new DateTime(endingYear, endingMonth, endingDay);
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

            if (repShows.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                    lblFooter.Text = "Sorry, we were not able to find any shows";
                }
            }
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadShows(true);
        }

        protected void btnApplyDateRange_Click(object sender, EventArgs e)
        {
            LoadShows(false);
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
                    
                    if(eventOrderNumbers.Contains(orderNumber))
                        eventOrderNumbers = eventOrderNumbers.Replace(orderNumber + "|", "");
                }

                EventOrderNumbers = eventOrderNumbers;
                Session["OrderNumberToRemove"] = null;
            }

            bool sortData = ddlSort.SelectedIndex != 0;
            LoadShows(sortData);
        }
        #endregion

        protected void rbDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            bool sortData = ddlSort.SelectedIndex != 0;
            LoadShows(sortData);
        }
    }
}