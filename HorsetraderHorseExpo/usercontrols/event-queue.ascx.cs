using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo.usercontrols
{
    public partial class event_queue : System.Web.UI.UserControl
    {
        public EventHandler ibtnRemoveFromListClicked;

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (!IsPostBack)
                LoadEventList();
        }

        private void LoadEventList()
        {
            try
            {
                if (Request.Cookies["EventOrderNumbers"] != null)
                {
                    string eventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;
                    string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
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
                //if (Cache["EventList"] != null)
                //{
                //    List<string> eventList = (List<string>)Cache["EventList"];
                //    DataSet dsEventList = ShowdateBLL.ListByOrderNumber(eventList);
                //    DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();

                //    repEventList.DataSource = dvEventList;
                //    repEventList.DataBind();
                //}
                //else
                //{
                //    repEventList.DataSource = new DataView();
                //    repEventList.DataBind();
                //}
            }
            catch
            { ;}
        }

        private void LoadEventList(string eventOrderNumbers)
        {
            try
            {
                if (!string.IsNullOrEmpty(eventOrderNumbers))
                {
                    string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
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

        #region Functions
        protected string TrimEventName(string eventName)
        {
            if (eventName.Length > 14)
                eventName = eventName.Substring(0, 14) + "...";

            return eventName;
        }

        protected string SetDateRangeInfo(string startDate)
        {
            string dateRange = string.Empty;

            DateTime date;
            DateTime.TryParse(startDate, out date);

            dateRange = date.ToString("ddddd MMMM d");

            //if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            //    dateRange += string.Format(", {0} - {1}", startTime, endTime);
            //else if (!string.IsNullOrEmpty(startTime))
            //    dateRange += string.Format(", {0}", startTime);

            return dateRange;
        }

        protected string SetTimeRangeInfo(string startTime, string endTime)
        {
            string timeRange = string.Empty;

            //DateTime date;
            //DateTime.TryParse(startDate, out date);

            //dateRange = date.ToString("ddddd MMMM d");

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                timeRange += string.Format("{0} - {1}", startTime, endTime);
            else if (!string.IsNullOrEmpty(startTime))
                timeRange += string.Format("{0}", startTime);

            return timeRange;
        }

        private void OnIbtnRemoveFromListClicked(string orderNumber)
        {
            Session["OrderNumberToRemove"] = orderNumber;

            if (ibtnRemoveFromListClicked != null)
                ibtnRemoveFromListClicked(this, EventArgs.Empty);
        }

        protected string SetImage(string photoFile)
        {
            string imageHTML = string.Empty;

            if (!string.IsNullOrEmpty(photoFile))
            {
                string imageURL = string.Format("{0}/{1}",
                                        ConfigurationManager.AppSettings["ImagesURL"],
                                        photoFile);
                imageHTML = string.Format("<img src='{0}' />", imageURL);
            }

            return imageHTML;
        }
        #endregion

        #region Event Handlers
        protected void ibtnRemoveFromList_Click(object sender, EventArgs e)
        {
            string orderNumber = ((ImageButton)sender).CommandArgument;

            if (Request.Cookies["EventOrderNumbers"] != null)
            {
                string eventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;
                eventOrderNumbers = eventOrderNumbers.Replace(orderNumber + "|", "");
                Response.Cookies["EventOrderNumbers"].Value = eventOrderNumbers;
                Response.Cookies["EventOrderNumbers"].Expires = DateTime.Now.AddDays(90);

                LoadEventList(eventOrderNumbers);
                OnIbtnRemoveFromListClicked(orderNumber);
            }

            //if (Cache["EventList"] != null)
            //{
            //    List<string> eventList = (List<string>)Cache["EventList"];
            //    eventList.Remove(orderNumber);
            //    Cache["EventList"] = eventList;

            //    LoadEventList();
            //    OnIbtnRemoveFromListClicked();
            //}
        }

        protected void repEventList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (repEventList.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                    lblFooter.Text = "Create and print your custom list of events to attend... Just check the ones not to miss!";
                }
            }
        }

        protected void ibtnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string orderNumberList = string.Empty;

            if (repEventList.Items.Count < 1)
            {
                ClientScriptManager csm = Page.ClientScript;
                csm.RegisterStartupScript(this.GetType(), "alert", "alert('Please add at least one event to your list before printing')", true);
                return;
            }

            try
            {
                foreach (RepeaterItem showdate in repEventList.Items)
                {
                    if (showdate.ItemType == ListItemType.Item
                        || showdate.ItemType == ListItemType.AlternatingItem)
                    {
                        ImageButton ibtn = (ImageButton)showdate.FindControl("ibtnRemoveFromList");
                        orderNumberList += ibtn.CommandArgument + "|";
                    }
                }
            }
            catch
            { ;}

            Session["EventOrderNumberList"] = orderNumberList;
            Response.RedirectPermanent("print-event-list");
        }
        #endregion
    }
}