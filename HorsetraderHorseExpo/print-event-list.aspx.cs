using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class print_event_list : System.Web.UI.Page
    {
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

        private void LoadEventList()
        {
            List<string> eventList = LoadArgsFromSession();

            DataSet dsEventList = ShowdateBLL.ListByOrderNumber(eventList);
            DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();

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
        #endregion
    }
}