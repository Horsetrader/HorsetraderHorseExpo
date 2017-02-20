using System;
using System.Configuration;

namespace HorsetraderHorseExpo
{
    public partial class send_email_friend_mobile : System.Web.UI.Page
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
                btnGoBack.Visible = true;
                btnTryAgain.Visible = false;
            }
            else
            {
                successAlert.Attributes.Add("style", "display:none");
                errorAlert.Attributes.Add("style", "display:block");
                btnGoBack.Visible = false;
                btnTryAgain.Visible = true;
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

                if (Request["id"] != null && Request["t"] != null)
                {
                    string type = Request["t"].ToString() == "exhibitor" ? "exhibitor" : "event";
                    string url = string.Format("{0}/{1}-details?id={2}",
                        ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], type, Request["id"]);

                    string subject = "Take a look at this " + type + " from Western States Horse Expo!";
                    string body = string.Format("Hello! Your friend, {0}, thought that you would be " +
                        "interested in this {1} from Western States Horse Expo with the following note:<br><br>" +
                        "{2}<br><br><a href='{3}'>Click here to view the details</a>",
                        tbxName.Text, type, tbxNote.Value, url);


                    string fromAddress = tbxEmail.Text;
                    string toAddress = tbxFriendEmail.Text;

                    success = EmailHelper.Send(fromAddress, toAddress, string.Empty, subject, body);
                }

                //Show confirmation message
                ShowEmailSentConfirmation(success);
            }
            else
                lblCaptchaError.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            if (Request["id"] != null && Request["t"] != null)
            {
                string type = Request["t"].ToString() == "exhibitor" ? "exhibitor" : "event";
                string url = string.Format("{0}/{1}-details?id={2}",
                    ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], type, Request["id"]);

                Response.Redirect(url);
            }
            else
                Response.Redirect("schedule");
        }

        protected void btnTryAgain_Click(object sender, EventArgs e)
        {
            emailForm.Attributes.Add("style", "display:block;");
            errorAlert.Attributes.Add("style", "display:none;");
            lblCaptchaError.Visible = false;
            btnTryAgain.Visible = false;
        }
        #endregion
    }
}