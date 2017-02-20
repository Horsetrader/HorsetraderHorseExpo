using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class print_list_sample_1 : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (!IsPostBack)
                LoadEventList(false);
        }

        private List<string> LoadArgsFromSession()
        {
            string[] orderNumberList = new string[] { };
            if (Session["EventOrderNumberList"] != null)
            {
                string orderNumbers = (string)Session["EventOrderNumberList"];
                string[] splitCharacter = new string[] { "|" };
                orderNumberList = orderNumbers.Split(splitCharacter, StringSplitOptions.RemoveEmptyEntries);
            }

            return orderNumberList.ToList<string>(); ;
        }

        private void LoadEventList(bool sortData)
        {
            //List<string> eventList = LoadArgsFromSession();
            List<string> eventList = new List<string>(4);
            eventList.Add("873811");
            eventList.Add("873816");
            eventList.Add("873815");
            eventList.Add("873848");


            DataSet dsEventList = ShowdateBLL.ListByOrderNumber(eventList);
            DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();
            dvEventList = sortData ? SortData(dvEventList) : dvEventList;

            repEventList.DataSource = dvEventList;
            repEventList.DataBind();
        }
        #endregion

        #region Functions
        protected string FormatDate(string startDate)
        {
            DateTime date;
            DateTime.TryParse(startDate, out date);

            startDate = date.ToString("ddddd MMMM d");

            return startDate;
        }

        protected string SetImage(string photoFile)
        {
            string imageHTML = string.Empty;

            if (!string.IsNullOrEmpty(photoFile))
            {
                string imageURL = string.Format("{0}/{1}",
                                        ConfigurationManager.AppSettings["ImagesURL"],
                                        photoFile);
                imageHTML = string.Format("<img src='{0}' width='100px' />", imageURL);
            }

            return imageHTML;
        }

        protected string SetEventTimeInfo(string startTime, string endTime)
        {
            string timeInfo = string.Empty;

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                timeInfo += string.Format("{0} - {1}", startTime, endTime);
            else if (!string.IsNullOrEmpty(startTime))
                timeInfo += string.Format("{0}", startTime);

            return timeInfo;
        }

        protected string SetBoothNumberInfo(string boothNumber)
        {
            if (!string.IsNullOrEmpty(boothNumber))
            {
                boothNumber = string.Format("Also, see their booth, #{0}!", boothNumber);
            }

            return boothNumber;
        }

        private DataView SortData(DataView dvEventList)
        {
            try
            {
                dvEventList.Sort = ddlSort.SelectedValue;
            }
            catch (Exception e)
            { }

            return dvEventList;
        }
        #endregion

        #region Event Handlers
        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEventList(true);
        }

        protected void repEventList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ddlView.SelectedValue.Equals("Compact"))
                {
                    HtmlGenericControl divImage = (HtmlGenericControl)e.Item.FindControl("divImage");
                    HtmlGenericControl divDescription = (HtmlGenericControl)e.Item.FindControl("divDescription");
                    divImage.Attributes.Add("style", "display:none;");
                    divDescription.Attributes.Add("class", "span10 event-description-print");
                }
                else
                {
                    HtmlGenericControl divImage = (HtmlGenericControl)e.Item.FindControl("divImage");
                    HtmlGenericControl divDescription = (HtmlGenericControl)e.Item.FindControl("divDescription");
                    divImage.Attributes.Add("style", "display:block;");
                    divDescription.Attributes.Add("class", "span8 event-description-print");
                }
            }
        }
        #endregion
    }
}