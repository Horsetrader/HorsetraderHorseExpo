using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class event_details : System.Web.UI.Page
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
            PageURL = string.Format("{0}/event-details.aspx?id={1}",
                                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"],
                                Request["id"]);
            PageTitle = title;
            PageThumbnail = string.Format("{0}/{1}'>",
                                        ConfigurationManager.AppSettings["ImagesURL"], thumbnail);
            PageContent = content;
        }

        private void LoadShowDetails()
        {
            ShowdateBLL showdate = GetShow();
            lblEventName.Text = showdate.Event_Name;
            lblLocation.Text = lblFacility.Text = showdate.Sub_Facility;
            SetMap(showdate.Sub_Facility_Map);
            SetAddToListButton(showdate.Order_Number.ToString());
            divShowRange.InnerHtml = SetDateRangeInfo(showdate.Event_Start_Date, 
                                        showdate.Event_Start_Time, showdate.Event_End_Time);
            //divAsSeenIn.InnerHtml = string.IsNullOrEmpty(showdate.Ad_On_Page) ? string.Empty : SetAsSeenInInfo();
            //divCurrentIssue.InnerHtml = SetCurrentIssueInfo(showdate.Ad_On_Page);
            lblEventType.Text = showdate.Event_Type;
            lblStartDate.Text = showdate.Event_Start_Date > DateTime.MinValue ?
                string.Format("{0} {1}",
                showdate.Event_Start_Date.ToShortDateString(),
                showdate.Event_Start_Time)
                :
                string.Empty;
            lblEndDate.Text = showdate.Event_End_Date > DateTime.MinValue ?
                string.Format("{0} {1}",
                showdate.Event_End_Date.ToShortDateString(),
                showdate.Event_End_Time)
                :
                string.Empty;
            //lblApprovals.Text = SetApprovalsInfo(showdate.Approvals);
            lblDescription.Text = showdate.Description;
            lblBoothNumber.Text = SetBoothNumberInfo(showdate.Booth_Number);
            //lblContact.Text = string.Format("{0} {1} {2}",
            //    showdate.Contact_First_Name,
            //    showdate.Contact_Last_Name,
            //    showdate.Contact_Phone);
            divImage.InnerHtml = SetImage(showdate.Foto_File);
            divVideo.InnerHtml = SetYoutubeVideo(showdate.Youtube_Url);
            divURL.InnerHtml = SetURL(showdate.Url);
            divButtons.InnerHtml = SetButtons(showdate.Order_Number);
            divFacebookComments.InnerHtml = SetFacebookComments(showdate.Order_Number);

            //Facebook metadata
            LoadFacebookMetadata(showdate.Order_Number.ToString(), showdate.Event_Name, 
                showdate.Foto_File, showdate.Description);
        }
        #endregion

        #region Functions
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

        private void SetMap(string subFacilityMap)
        {
            if (!string.IsNullOrEmpty(subFacilityMap))
            {
                aShowMap.Attributes.Add("href", ConfigurationManager.AppSettings["ImagesURL"] + "/" + subFacilityMap);
                aShowMap.InnerHtml = "<button class='sexybutton sexysimple sexysmall' style='margin-left: 5px; margin-top: -2px;'>" +
                                        "<span class='map'>Show Map</span>" +
                                      "</button>";
            }
        }

        private string SetDateRangeInfo(DateTime startDate, string startTime, string endTime)
        {
            string dateRange = string.Empty;
            dateRange = startDate.ToString("ddddd MMMM d");
            
            if(!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                dateRange += string.Format(", {0} - {1}", startTime, endTime);
            
            return dateRange;
        }

        private string SetAsSeenInInfo()
        {
            return "<span>As seen in:</span><img src='assets/images/horseexpologo.jpg' />";
        }

        private string SetCurrentIssueInfo(string pageNumber)
        {
            string currentIssueHTML = string.Empty;

            if (!string.IsNullOrEmpty(pageNumber))
            {
                currentIssueHTML = string.Format(
                    "On Page {0}, " +
                    "<a class='blueLink' href='http://content.yudu.com/Aoy9u/Horsetrader/resources/{0}.htm'>" +
                        "Go to Page Now!" +
                    "</a>",
                    pageNumber);
            }

            return currentIssueHTML;
        }

        private string SetApprovalsInfo(List<ApprovalDTO> approvalList)
        {
            string approvals = string.Empty;

            if (approvalList != null)
            {
                foreach (ApprovalDTO approval in approvalList)
                {
                    approvals += approval.Approval_Description + ", ";
                }

                if (approvals.Length > 0)
                    approvals = approvals.Substring(0, approvals.Length - 2);
            }

            return approvals;
        }

        private string SetImage(string image)
        {
            string imageHTML = string.Empty;

            if (!string.IsNullOrEmpty(image))
            {
                imageHTML = string.Format("<img src='{0}/{1}'>",
                        ConfigurationManager.AppSettings["ImagesURL"], image);
                
            }
            else
            {
                imageHTML = "<img src='" + ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] + "/assets/images/noimage.jpg' width='400'>";
            }

            return imageHTML;
        }

        private string SetYoutubeVideo(string youtubeURL)
        {
            string youtubeHTML = string.Empty;

            if (!string.IsNullOrEmpty(youtubeURL))
            {
                youtubeHTML = string.Format(
                        "<iframe id='Iframe1' type='text/html' width='420' height='315' " +
                            "src='{0}' frameborder='0'>" +
                        "</iframe>",
                        youtubeURL);
            }
            
            return youtubeHTML;
        }

        private string SetURL(string url)
        {
            string urlInfo = string.Empty;

            if (!string.IsNullOrEmpty(url))
            {
                urlInfo = string.Format("Visit our website! <a href='{0}' target='_blank'>{0}</a>", url);
            }

            return urlInfo;
        }

        private string SetBoothNumberInfo(string boothNumber)
        {
            string boothInfo = string.Empty;

            if (!string.IsNullOrEmpty(boothNumber))
            {
                boothInfo = string.Format("Come visit us in booth {0}", boothNumber);
            }

            return boothInfo;
        }

        private string SetButtons(int orderNumber)
        {
            string buttonsHTML = string.Empty;

            buttonsHTML = string.Format("<a href='emailclinician.asp?fastad={0}' class='exampleemail'>" +
                                            "<button class='sexybutton sexysimple'>" +
                                                "<span class='email'>Email clinician</span>" +
                                            "</button>" +
                                        "</a>",
                                        orderNumber);


            buttonsHTML += string.Format("<a href='emailfriend.asp?fastad={0}&PubNumber={1}' class='exampleemail cboxElement small-offset1'>" +
                                            "<button class='sexybutton sexysimple'>" +
                                                "<span class='user'>Email ad to a friend</span>" +
                                            "</button>" +
                                        "</a>",
                                        orderNumber,
                                        ConfigurationManager.AppSettings["HorseExpoPubNumber"]);

            return buttonsHTML;
        }

        private string SetFacebookComments(int orderNumber)
        {
            string buttonsHTML = string.Empty;
            string pageURL = string.Format("{0}/event-details.aspx?id={1}",
                                            ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], orderNumber);

            buttonsHTML = string.Format("<div id='fb-root'></div>" +
                                        "<script src='http://connect.facebook.net/en_US/all.js#xfbml=1' type='text/javascript'></script>&nbsp;" +
                                        "<fb:comments href='{0}' num_posts='2' width='940' publish_feed='false' simple='false'>" +
                                        "</fb:comments>",
                                        pageURL);

            return buttonsHTML;
        }

        protected void SetAddToListButton(string orderNumber)
        {
            string imageURL = "~/assets/images/add-to-list.png";

            if (Request.Cookies["EventOrderNumbers"] != null)
            {
                string eventOrderNumbers = Request.Cookies["EventOrderNumbers"].Value;
                string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> eventList = new List<string>(eventOrderNumbersArray);

                if (eventList.Contains(orderNumber))
                    imageURL = "~/assets/images/on-my-list.png";
            }

            ibtnAddToList.ImageUrl = imageURL;
            ibtnAddToList.CommandArgument = orderNumber;
        }
        #endregion

        #region Event Handlers
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
            Response.Cookies["EventOrderNumbers"].Value = eventOrderNumbers;
            Response.Cookies["EventOrderNumbers"].Expires = DateTime.Now.AddDays(90);

            //Change ImageButton image to "on my list"
            ibtnAddToList.ImageUrl = addedToList ? "~/assets/images/add-to-list.png" : "~/assets/images/on-my-list.png";
        }
        #endregion

    }
}