using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class send_email : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        { }
        #endregion

        #region Functions
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
        #endregion

        #region Event Handlers
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && !string.IsNullOrEmpty(tbxEmail.Text) 
                && !string.IsNullOrEmpty(tbxFriendEmail.Text)
                && !string.IsNullOrEmpty(tbxName.Text))
            {
                if (Cache["EventList"] != null)
                {
                    List<string> eventList = (List<string>)Cache["EventList"];

                    string subject = "Take a look at these events from Horseexpoevents.com!";
                    string body = "Hello! Your friend, " + tbxName.Text + ", thought that you would be interested in these events from Horseexpoevents.com";

                    if (tbxComments.InnerText != string.Empty)
                        body += ", with the following note:<br /><br />" + tbxComments.InnerText;

                    body += "<br /><br />";
                    body += EmailHelper.GetEventListContent(ShowdateBLL.ListByOrderNumber(eventList));
                    string fromAddress = tbxFriendEmail.Text;
                    string toAddress = tbxEmail.Text;
                    string bccAddress = string.Empty;

                    EmailHelper.Send(fromAddress, toAddress, bccAddress, subject, body);

                    emailcontainer.Attributes.Add("style", "display:none");
                    confirmationmessage.Attributes.Add("style", "display:block");
                }
            }
            else
                lblCaptchaError.Visible = true;
        }
        #endregion
    }
}