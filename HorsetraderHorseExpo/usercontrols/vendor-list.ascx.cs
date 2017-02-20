using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo.usercontrols
{
    public partial class vendor_list : System.Web.UI.UserControl
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
                if (Request.Cookies["ExpoVendorIDs"] != null)
                {
                    string eventOrderNumbers = Request.Cookies["ExpoVendorIDs"].Value;
                    string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> eventList = new List<string>(eventOrderNumbersArray);
                    DataSet dsEventList = ShowdateBLL.ListByExpoVendorID(eventList);
                    DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();

                    repExhibitorList.DataSource = dvEventList;
                    repExhibitorList.DataBind();
                }
                else
                {
                    repExhibitorList.DataSource = new DataView();
                    repExhibitorList.DataBind();
                }
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
                    DataSet dsEventList = ShowdateBLL.ListByExpoVendorID(eventList);
                    DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();

                    repExhibitorList.DataSource = dvEventList;
                    repExhibitorList.DataBind();
                }
                else
                {
                    repExhibitorList.DataSource = new DataView();
                    repExhibitorList.DataBind();
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

            return dateRange;
        }

        protected string SetTimeRangeInfo(string startTime, string endTime)
        {
            string timeRange = string.Empty;

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                timeRange += string.Format("{0} - {1}", startTime, endTime);
            else if (!string.IsNullOrEmpty(startTime))
                timeRange += string.Format("{0}", startTime);

            return timeRange;
        }

        private void OnIbtnRemoveFromListClicked(string expoVendorId)
        {
            Session["ExpoVendorIDToRemove"] = expoVendorId;

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

        protected string SetURLRedirect(bool isAdvertiser, string ExpoVendorID)
        {
            return isAdvertiser ? string.Format("exhibitor-details?id={0}", ExpoVendorID) : "#";
        }
        #endregion

        #region Event Handlers
        protected void ibtnRemoveFromList_Click(object sender, EventArgs e)
        {
            string orderNumber = ((ImageButton)sender).CommandArgument;

            if (Request.Cookies["ExpoVendorIDs"] != null)
            {
                string eventOrderNumbers = Request.Cookies["ExpoVendorIDs"].Value;
                eventOrderNumbers = eventOrderNumbers.Replace(orderNumber + "|", "");
                Response.Cookies["ExpoVendorIDs"].Value = eventOrderNumbers;
                Response.Cookies["ExpoVendorIDs"].Expires = DateTime.Now.AddDays(90);

                LoadEventList(eventOrderNumbers);
                OnIbtnRemoveFromListClicked(orderNumber);
            }
        }

        protected void repExhibitorList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (repExhibitorList.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                    lblFooter.Text = "Create and print your custom list of exhibitors to visit... Just check the ones not to miss!";
                }
            }
        }

        protected void ibtnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string orderNumberList = string.Empty;

            if (repExhibitorList.Items.Count < 1)
            {
                ClientScriptManager csm = Page.ClientScript;
                csm.RegisterStartupScript(this.GetType(), "alert", "alert('Please add at least one event to your list before printing')", true);
                return;
            }

            try
            {
                foreach (RepeaterItem showdate in repExhibitorList.Items)
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

            Session["ExpoVendorIDList"] = orderNumberList;
            Response.RedirectPermanent("print-exhibitor-list");
        }
        #endregion
    }
}