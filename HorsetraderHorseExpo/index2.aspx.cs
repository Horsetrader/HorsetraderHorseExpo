using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class index2 : System.Web.UI.Page
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
        int expoid = 0;

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
                LoadShows(false);
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

        public void LoadShows(bool sortData)
        {
            try
            {
                //Get ID for Event to obtain start and end dates
                int expoEventId = int.Parse(ConfigurationManager.AppSettings["ExpoEventID"]);
                expoid = expoEventId;           // save for query generation

                //Get event start and end dates
                DataTable dtEventDates = GetEventDates(expoEventId);
                DateTime eventStartDate = DateTime.Parse(dtEventDates.Rows[0]["EventStartDate"].ToString());
                DateTime eventEndDate = DateTime.Parse(dtEventDates.Rows[0]["EventEndDate"].ToString());

                //Get PubNumber
                int pubNumber = int.Parse(ConfigurationManager.AppSettings["HorseExpoPubNumber"]);

                DataView dvShows = GetShows(eventStartDate, eventEndDate, expoEventId, pubNumber);
                dvShows = sortData ? SortData(dvShows) : dvShows;

                //Get only the events for the first day and bind them to repeater for Day 1
                dvShows.RowFilter = "event_start_date >= #" + eventStartDate.ToString("MM/dd/yyyy") + "# AND event_start_date <= #" + eventStartDate.AddDays(1).ToString("MM/dd/yyyy") + "#";
                repScheduleDay1.DataSource = dvShows;
                repScheduleDay1.DataBind();

                //Get only the events for the second day and bind them to repeater for Day 2
                dvShows.RowFilter = "event_start_date >= #" + eventStartDate.AddDays(1).ToString("MM/dd/yyyy") + "# AND event_start_date <= #" + eventStartDate.AddDays(2).ToString("MM/dd/yyyy") + "#";
                repScheduleDay2.DataSource = dvShows;
                repScheduleDay2.DataBind();

                //Get only the events for the third day and bind them to repeater for Day 3
                dvShows.RowFilter = "event_start_date >= #" + eventStartDate.AddDays(2).ToString("MM/dd/yyyy") + "# AND event_start_date <= #" + eventStartDate.AddDays(3).ToString("MM/dd/yyyy") + "#";
                repScheduleDay3.DataSource = dvShows;
                repScheduleDay3.DataBind();
            }
            catch (Exception e) {
                String msg = e.Message;
            }
        }
        #endregion

        #region Private Functions
        private DataTable GetEventDates(int expoEventId)
        {
            DataSet dsEventDates = new DataSet();

            try {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString)) {
                    using (SqlCommand cmd = new SqlCommand()) {
                        cmd.Parameters.Add(new SqlParameter("@ExpoEventID", expoEventId));
                        cmd.CommandText = "sp_ht_get_ExpoEventDates";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();

                        var adapter = new SqlDataAdapter(cmd);

                        try {
                            adapter.Fill(dsEventDates);
                        } catch (Exception e) {
                            //manage remote db when working local
                        }
                    }
                }

                if (dsEventDates.Tables.Count > 0)
                    return dsEventDates.Tables[0];

                return new DataTable();
            }
            catch (Exception e) {
                String msg = e.Message;
                return new DataTable();
            }
        }

        private DataView GetShows(DateTime eventStartDate, DateTime eventEndDate, int expoEventId, int pubNumber)
        {
            DataSet dsShows = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.Add(new SqlParameter("@EventStartDate", eventStartDate));
                    cmd.Parameters.Add(new SqlParameter("@EventEndDate", eventEndDate));
                    cmd.Parameters.Add(new SqlParameter("@ExpoEventID", expoEventId));
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
            //try
            //{
            //    dvAdList.Sort = ddlSort.SelectedValue;
            //}
            //catch (Exception e)
            //{ }

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
        #endregion

        #region Protected Functions
        protected string GetScheduleTime(string startTime, string endTime)
        {
            string scheduleTime = string.Empty;

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                scheduleTime = string.Format("{0} - {1}", startTime, endTime);
            else if (!string.IsNullOrEmpty(startTime))
                scheduleTime += string.Format("{0}", startTime);

            return scheduleTime;
        }

        protected string GetAddToListButtonHTML(string orderNumber)
        {
            string buttonHtml = "<a class='btn btn-default' href='#" + orderNumber + "' add-to-list='" + orderNumber + "'>" +
                                  "<span class='glyphicon glyphicon-unchecked'></span> Add to list" +
                                "</a>";

            if (EventOrderNumbers.Contains(orderNumber))
                buttonHtml = "<a class='btn btn-success' href='#" + orderNumber + "' add-to-list='" + orderNumber + "'>" +
                                "<span class='glyphicon glyphicon-check'></span> Added to list" +
                             "</a>";

            return buttonHtml;
        }
        #endregion

        #region Event Handlers
        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            string eventOrderNumbersList = string.Empty;
            //ImageButton ibtnAddToList = (ImageButton)sender;
            bool addedToList; //= ibtnAddToList.ImageUrl.Contains("on-my-list");
            string orderNumber = eventOrderNumber.Value; //ibtnAddToList.CommandArgument;

            //Check if list is in Cookies
            if (Request.Cookies["EventOrderNumbers"] != null)
            {
                eventOrderNumbersList = Request.Cookies["EventOrderNumbers"].Value;
                addedToList = eventOrderNumbersList.Contains(orderNumber);

                //Add to string chain (validate if not in it already)
                if (!addedToList)
                    eventOrderNumbersList += orderNumber + "|";
                else
                    //Remove from list if already in it
                    eventOrderNumbersList = eventOrderNumbersList.Replace(orderNumber + "|", "");
            }
            else
                eventOrderNumbersList += orderNumber + "|";

            //Add string chain to Cookie
            EventOrderNumbers = eventOrderNumbersList;
            Response.Cookies["EventOrderNumbers"].Value = eventOrderNumbersList;
            Response.Cookies["EventOrderNumbers"].Expires = DateTime.Now.AddDays(90);

            //Change ImageButton image to "on my list"
            //ibtnAddToList.ImageUrl = addedToList ? "~/assets/images/add-to-list.png" : "~/assets/images/on-my-list.png";
            LoadShows(false);
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

            bool sortData = false;//ddlSort.SelectedIndex != 0;
            LoadShows(sortData);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(string.Format("{0}/search?q={1}",
                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], tbxSearch.Text));
        }

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
                            lblFooter.Text = "No events have been added at the moment. If you'd like to get updates of Western States Horse Expo schedule changes or additions click <a href='register'>here</a>";

                        }
                    }
                }
            }
        }
        #endregion
    }
}