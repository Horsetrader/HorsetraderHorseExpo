using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class vendor_details : System.Web.UI.Page
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
                LoadVendorDetails();
        }

        private void VerifyURLParams()
        {
            if (Request["id"] != null)
                return;

            Response.RedirectPermanent(ConfigurationManager.AppSettings["HorseExpoWebsiteURL"]);
        }

        private void LoadFacebookMetadata(string title, string thumbnail, string content)
        {
            PageURL = string.Format("{0}/exhibitor-details?id={1}",
                                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"],
                                Request["id"]);
            PageTitle = title.Replace("'","&apos;");
            PageThumbnail = string.Format("{0}/{1}",
                                        ConfigurationManager.AppSettings["ImagesURL"], thumbnail);
            PageContent = content;
        }

        private void LoadVendorDetails()
        {
            DataSet dsVendor = GetVendor();
            string expoVendorId = string.Empty;

            if (dsVendor.Tables.Count > 0)
            {
                //Load vendor info
                DataTable dt = dsVendor.Tables[0];

                vendorTitle.InnerHtml = string.Format(
                    "{0}{1}<em class='hidden-xs'>{2}</em><small>{3} - Booth {4}</small>",
                    dt.Rows[0]["Name"],
                    GetSocialIcons(dt.Rows[0]["FacebookURL"].ToString(), dt.Rows[0]["TwitterURL"].ToString()),
                    dt.Rows[0]["Headline"],
                    dt.Rows[0]["Location"],
                    dt.Rows[0]["Booth"]
                );

                expoVendorId = dt.Rows[0]["ExpoVendorID"].ToString();
                vendorImage.Attributes.Add("src", ConfigurationManager.AppSettings["ImagesURL"]  + "/" + dt.Rows[0]["ImageFileName"]);
                vendorDescription.InnerHtml = dt.Rows[0]["Description"].ToString();
                vendorContactName.InnerHtml = dt.Rows[0]["ContactName"].ToString();
                vendorPhone.InnerHtml = dt.Rows[0]["Phone"].ToString();
                vendorUrl.InnerHtml = string.Format("<a href='{0}' target='_blank'>{0}</a>", dt.Rows[0]["URL"]);
                vendorVideo.InnerHtml = GetYoutubeVideoHTML(dt.Rows[0]["YoutubeURL"].ToString());

                SetViewMapButtonsHTML(dt.Rows[0]["ExpoVendorID"].ToString(), dt.Rows[0]["MapFileName"].ToString());

                sendEmailVendor.Attributes.Add("data-src", string.Format("send-email-vendor.aspx?e={0}&t=exhibitor", dt.Rows[0]["Email"]));
                sendEmailVendorMobile.Attributes.Add("href", string.Format("send-email-vendor-mobile.aspx?id={0}&e={1}&t=exhibitor", dt.Rows[0]["ExpoVendorID"], dt.Rows[0]["Email"]));
                sendEmailFriend.Attributes.Add("data-src", string.Format("send-email-friend.aspx?id={0}&t=exhibitor", dt.Rows[0]["ExpoVendorID"]));
                sendEmailFriendMobile.Attributes.Add("href", string.Format("send-email-friend-mobile.aspx?id={0}&t=exhibitor", dt.Rows[0]["ExpoVendorID"]));

                //Facebook metadata
                LoadFacebookMetadata(dt.Rows[0]["Name"].ToString(), dt.Rows[0]["ImageFileName"].ToString(), dt.Rows[0]["Description"].ToString());

                //Load vendor images
                dt = dsVendor.Tables[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vendorImages.InnerHtml += string.Format(
                        "<a href='{0}/{1}' data-gallery>" +
                            "<img src='{0}/{1}' class='img-thumbnail vendor-image' />" +
                        "</a>",
                        ConfigurationManager.AppSettings["ImagesURL"],
                        dt.Rows[i]["ImageFileName"]
                    );
                }
            }

            
            SetAddToListButtonHTML(expoVendorId);
            divFacebookComments.InnerHtml = GetFacebookCommentsHTML(expoVendorId);

            //Grounds map link
            //mapLink.Attributes.Add("href", string.Format("view-grounds-map?from=exhibitor-details?id={0}", expoVendorId));
        }
        #endregion

        #region Private Functions
        private DataSet GetVendor()
        {
            DataSet dsVendors = new DataSet();
            int expoVendorId = 0;
            int.TryParse(Request["id"], out expoVendorId);

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.Add(new SqlParameter("@ExpoVendorID", expoVendorId));
                    cmd.CommandText = "sp_ht_GetVendorByID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    var adapter = new SqlDataAdapter(cmd);

                    try
                    {
                        adapter.Fill(dsVendors);
                    }
                    catch (Exception e)
                    {
                        //manage remote db when working local
                    }
                }
            }

            return dsVendors;
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
                imageHTML = string.Format("<img src='{0}/{1}' class='img-thumbnail' />",
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
                        "<div class='video-container'>" +
                            "<iframe id='Iframe1' type='text/html' width='420' height='315' " +
                                "src='{0}?rel=0' frameborder='0'>" +
                            "</iframe>" +
                        "</div>",
                        youtubeURL);
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

        private void SetViewMapButtonsHTML(string expoVendorId, string mapFilename)
        {
            viewMapMobile.Attributes.Add("href",string.Format("view-map?id={0}&f={1}&t=exhibitor",expoVendorId,mapFilename));
            viewMap.Attributes.Add("href",string.Format("{0}/{1}", ConfigurationManager.AppSettings["ImagesURL"], mapFilename));
            viewMap.Attributes.Add("data-gallery","");
        }

        private void SetAddToListButtonHTML(string expoVendorId)
        {
            string buttonHTML = "<span class='glyphicon glyphicon-unchecked'></span> Add to list";
            addToList.Attributes.Add("class", "btn btn-primary");

            if (Request.Cookies["ExpoVendorIDs"] != null)
            {
                string eventOrderNumbers = Request.Cookies["ExpoVendorIDs"].Value;
                string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> eventList = new List<string>(eventOrderNumbersArray);

                if (eventList.Contains(expoVendorId))
                {
                    buttonHTML = "<span class='glyphicon glyphicon-check'></span> Added to list";
                    addToList.Attributes.Add("class", "btn btn-success");
                }
            }

            addToList.Attributes.Add("add-to-list", expoVendorId);
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

        private string GetFacebookCommentsHTML(string expoVendorId)
        {
            string fbCommentsHTML = string.Empty;
            string pageURL = string.Format("{0}/exhibitor-details?id={1}",
                                            ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], expoVendorId);

            fbCommentsHTML = string.Format("<div id='fb-root'></div>" +
                                        "<script src='http://connect.facebook.net/en_US/all.js#xfbml=1' type='text/javascript'></script>&nbsp;" +
                                        "<fb:comments href='{0}' num_posts='2' width='940' publish_feed='false' simple='false'>" +
                                        "</fb:comments>",
                                        pageURL);

            return fbCommentsHTML;
        }

        private string GetSocialIcons(string facebookUrl, string twitterUrl)
        {
            string socialIconsHtml = string.Empty;
            socialIconsHtml = string.IsNullOrEmpty(facebookUrl) ? string.Empty : string.Format("<a href='{0}'><img class='social-icons' src='assets/images/facebook.png' /></a>", facebookUrl);
            socialIconsHtml += string.IsNullOrEmpty(twitterUrl) ? string.Empty : string.Format("<a href='{0}'><img class='social-icons' src='assets/images/twitter.png' /></a>", twitterUrl);
            return socialIconsHtml;
        }
        #endregion

        #region Event Handlers
        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            string expoVendorIDList = string.Empty;
            bool addedToList = false;
            string orderNumber = eventOrderNumber.Value;

            //Check if list is in Cookies
            if (Request.Cookies["ExpoVendorIDs"] != null)
            {
                expoVendorIDList = Request.Cookies["ExpoVendorIDs"].Value;
                addedToList = expoVendorIDList.Contains(orderNumber);

                //Add to string chain (validate if not in it already)
                if (!addedToList)
                    expoVendorIDList += orderNumber + "|";
                else
                    //Remove from list if already in it
                    expoVendorIDList = expoVendorIDList.Replace(orderNumber + "|", "");
            }
            else
                expoVendorIDList += orderNumber + "|";

            //Add string chain to Cookie
            Response.Cookies["ExpoVendorIDs"].Value = expoVendorIDList;
            Response.Cookies["ExpoVendorIDs"].Expires = DateTime.Now.AddDays(90);

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