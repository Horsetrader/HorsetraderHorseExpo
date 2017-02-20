using System;
using System.Configuration;
namespace HorsetraderHorseExpo
{
    public partial class send_email_vendor_mobile : System.Web.UI.Page
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
                && !string.IsNullOrEmpty(tbxComments.Value)
                && !string.IsNullOrEmpty(tbxEmail.Text)
                && !string.IsNullOrEmpty(tbxName.Text))
            {
                bool success = false;

                if (Request["e"] != null && Request["t"] != null)
                {
                    string type = Request["t"].ToString() == "exhibitor" ? "exhibitor" : "event";
                    string url = string.Format("{0}/{1}-details?id={2}",
                        ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], type, Request["id"]);

                    string subject = type == "exhibitor" ? "Western States Horse Expo user inquiry" : "Inquiry about your event on Western States Horse Expo";
                    string body = string.Format(
                        "Contact name: {0} <br>" +
                        "Email: {1} <br>" +
                        "Phone: {2} <br>" +
                        "Message: {3}",
                        tbxName.Text, tbxEmail.Text, tbxPhone.Text, tbxComments.Value);

                    string fromAddress = tbxEmail.Text;
                    string toAddress = Request["e"];

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