using System;
using System.Collections.Generic;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class event_details2 : System.Web.UI.Page
    {
        #region Global Variables
        protected string PageURL;
        protected string PageTitle;
        protected string PageThumbnail;
        protected string PageContent;
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            VerifyURLParams();

            if (!IsPostBack)
                LoadShowDetails();
        }

        private void VerifyURLParams()
        {
            if (Request["id"] != null)
                return;

            Response.RedirectPermanent(ConfigurationManager.AppSettings["HorseExpoWebsiteURL"]);
        }

        private void LoadFacebookMetadata(string orderNumber, string title, string thumbnail, string content)
        {
            PageURL = string.Format("{0}/event-details?id={1}",
                                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"],
                                Request["id"]);
            PageTitle = title;
            PageThumbnail = string.Format("{0}/{1}",
                                        ConfigurationManager.AppSettings["ImagesURL"], thumbnail);
            PageContent = content;
        }

        private void LoadShowDetails()
        {
            ShowdateBLL showdate = GetShow();

            eventName.InnerHtml = string.Format("{0}<br><small>{1}</small>", 
                showdate.Event_Name, 
                GetDateRangeInfo(showdate.Event_Start_Date, showdate.Event_Start_Time, showdate.Event_End_Time));

            eventType.InnerHtml = showdate.Event_Type;
            eventLocation.InnerHtml = showdate.Sub_Facility;
            eventBoothNumber.InnerHtml = string.IsNullOrEmpty(showdate.Booth_Number) ? string.Empty : string.Format("<strong>Booth number - {0}", showdate.Booth_Number);

            eventDescription.InnerHtml = GetImageHTML(showdate.Foto_File);
            eventDescription.InnerHtml += string.Format("<p>{0}</p>", showdate.Description);
            eventDescription.InnerHtml += GetURL(showdate.Url);

            eventVideo.InnerHtml = GetYoutubeVideoHTML(showdate.Youtube_Url);

            //Load map
            SetViewMapButtonsHTML(showdate.Order_Number, showdate.Sub_Facility_Map);

            SetAddToListButtonHTML(showdate.Order_Number.ToString());

            divFacebookComments.InnerHtml = GetFacebookCommentsHTML(showdate.Order_Number);

            //Facebook metadata
            LoadFacebookMetadata(showdate.Order_Number.ToString(), showdate.Event_Name,
                showdate.Foto_File, showdate.Description);

            //Send email
            sendEmailClinician.Attributes.Add("data-src", string.Format("send-email-vendor.aspx?e={0}&t=clinician", showdate.Contact_Email));
            sendEmailClinicianMobile.Attributes.Add("href", string.Format("send-email-vendor-mobile.aspx?id={0}&e={1}&t=clinician", showdate.Order_Number, showdate.Contact_Email));
            sendEmailFriend.Attributes.Add("data-src", string.Format("send-email-friend.aspx?id={0}&t=clinician", showdate.Order_Number));
            sendEmailFriendMobile.Attributes.Add("href", string.Format("send-email-friend-mobile.aspx?id={0}&t=clinician", showdate.Order_Number));

            //Grounds map link
            //mapLink.Attributes.Add("href", string.Format("view-grounds-map?from=event-details?id={0}", showdate.Order_Number));
        }
        #endregion

        #region Private Functions
        private ShowdateBLL GetShow()
        {
            ShowdateBLL showdate = new ShowdateBLL();

            try
            {
                int orderNumber = int.Parse(Request["id"].ToString());
                showdate = new ShowdateBLL(orderNumber);
            }
            catch
            { ; }

            return showdate;
        }

        private string GetDateRangeInfo(DateTime startDate, string startTime, string endTime)
        {
            string dateRange = string.Empty;
            dateRange = startDate.ToString("ddddd MMMM d");

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                dateRange += string.Format(", {0} - {1}", startTime, endTime);

            return dateRange;
        }

        private string GetImageHTML(string image)
        {
            string imageHTML = string.Empty;

            if (!string.IsNullOrEmpty(image))
            {
                imageHTML = string.Format("<img src='{0}/{1}' class='details-main-image img-thumbnail' />",
                        ConfigurationManager.AppSettings["ImagesURL"], image);

            }

            return imageHTML;
        }

        private string GetYoutubeVideoHTML(string youtubeURL)
        {
            string youtubeHTML = string.Empty;

            if (!string.IsNullOrEmpty(youtubeURL))
            {
                youtubeHTML = string.Format(
                        "<iframe id='Iframe1' type='text/html' width='420' height='315' " +
                            "src='{0}?rel=0' frameborder='0'>" +
                        "</iframe>",
                        youtubeURL);
            }
            //With no youtube video, hide container and expand description container
            else
            {
                eventDescription.Attributes.Add("class", "col-xs-12 col-sm-12 text-center");
                eventVideoContainer.Attributes.Add("style", "display:none");
            }

            return youtubeHTML;
        }

        private string GetURL(string url)
        {
            string urlInfo = string.Empty;

            if (!string.IsNullOrEmpty(url))
            {
                urlInfo = string.Format("<p>Visit our website! <a href='{0}' target='_blank'>{0}</a></p>", url);
            }

            return urlInfo;
        }

        private void SetAddToListButtonHTML(string orderNumber)
        {
            string buttonHTML = "<span class='glyphicon glyphicon-unchecked'></span> Add to list";
            addToList.Attributes.Add("class", "btn btn-primary");

            if (Request.Cookies["EventOrderNumbers"] != null)
            {
                string eventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;
                string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> eventList = new List<string>(eventOrderNumbersArray);

                if (eventList.Contains(orderNumber))
                {
                    buttonHTML = "<span class='glyphicon glyphicon-check'></span> Added to list";
                    addToList.Attributes.Add("class", "btn btn-success");
                }
            }

            addToList.Attributes.Add("add-to-list", orderNumber);
            addToList.InnerHtml = buttonHTML;
        }

        private void SetAddToListButtonHTML(string orderNumber, bool addedToList)
        {
            string buttonHTML = "<span class='glyphicon glyphicon-unchecked'></span> Add to list";
            addToList.Attributes.Add("class", "btn btn-primary");

            if (addedToList)
            {
                buttonHTML = "<span class='glyphicon glyphicon-check'></span> Added to list";
                addToList.Attributes.Add("class", "btn btn-success");
            }
            
            addToList.Attributes.Add("add-to-list", orderNumber);
            addToList.InnerHtml = buttonHTML;
        }

        private string GetFacebookCommentsHTML(int orderNumber)
        {
            string fbCommentsHTML = string.Empty;
            string pageURL = string.Format("{0}/event-details?id={1}",
                                            ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], orderNumber);

            fbCommentsHTML = string.Format("<div id='fb-root'></div>" +
                                        "<script src='http://connect.facebook.net/en_US/all.js#xfbml=1' type='text/javascript'></script>&nbsp;" +
                                        "<fb:comments href='{0}' num_posts='2' width='940' publish_feed='false' simple='false'>" +
                                        "</fb:comments>",
                                        pageURL);

            return fbCommentsHTML;
        }

        private void SetViewMapButtonsHTML(int orderNumber, string mapFilename)
        {
            viewMapMobile.Attributes.Add("href", string.Format("view-map?id={0}&f={1}&t=event", orderNumber, mapFilename));
            viewMap.Attributes.Add("href", string.Format("{0}/{1}", ConfigurationManager.AppSettings["ImagesURL"], mapFilename));
            viewMap.Attributes.Add("data-gallery", "");
        }
        #endregion

        #region Event Handlers
        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            string eventOrderNumbersList = string.Empty;
            bool addedToList = false;
            string orderNumber = eventOrderNumber.Value;

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
            Response.Cookies["EventOrderNumbers"].Value = eventOrderNumbersList;
            Response.Cookies["EventOrderNumbers"].Expires = DateTime.Now.AddDays(90);

            //Change ImageButton image to "on my list"
            SetAddToListButtonHTML(orderNumber, !addedToList);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(string.Format("{0}/search?q={1}",
                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], tbxSearch.Text));
        }
        #endregion
    }
}