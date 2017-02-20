using System;
using System.Collections.Generic;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class send_email_list_vendor_mobile : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Private Functions
        private void ShowEmailSentConfirmation(bool success)
        {
            if (success)
            {
                successAlert.Attributes.Add("style", "display:block");
                errorAlert.Attributes.Add("style", "display:none");
            }
            else
            {
                successAlert.Attributes.Add("style", "display:none");
                errorAlert.Attributes.Add("style", "display:block");
            }

            emailForm.Attributes.Add("style", "display:none;");
        }
        #endregion

        #region Event Handlers
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (Page.IsValid
                && !string.IsNullOrEmpty(tbxFriendEmail.Text)
                && !string.IsNullOrEmpty(tbxEmail.Text)
                && !string.IsNullOrEmpty(tbxName.Text))
            {
                bool success = false;

                if (Request.Cookies["ExpoVendorIDs"] != null)
                {
                    string eventOrderNumbers = Request.Cookies["ExpoVendorIDs"].Value;
                    string[] eventOrderNumbersArray = eventOrderNumbers.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> eventList = new List<string>(eventOrderNumbersArray);

                    string subject = "Take a look at these exhibitors from Horseexpoevents.com!";
                    string body = "Hello! Your friend, " + tbxName.Text + ", thought that you would be interested in these exhibitors from Horseexpoevents.com";

                    if (tbxNote.Value != string.Empty)
                        body += ", with the following note:<br /><br />" + tbxNote.Value;

                    body += "<br /><br />";
                    body += EmailHelper.GetVendorListContent(ShowdateBLL.ListByOrderNumber(eventList));
                    string fromAddress = tbxFriendEmail.Text;
                    string toAddress = tbxEmail.Text;
                    string bccAddress = string.Empty;

                    success = EmailHelper.Send(fromAddress, toAddress, bccAddress, subject, body);
                }

                //Show confirmation message
                ShowEmailSentConfirmation(success);
            }
            else
                lblCaptchaError.Visible = true;
        }
        #endregion
    }
}