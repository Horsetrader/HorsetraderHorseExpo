using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HorsetraderHorseExpo
{
    public partial class email_opt_in : System.Web.UI.Page
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Event Handlers
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(string.Format("{0}/search?q={1}",
                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], tbxSearch.Text));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbxEmail.Text) && !string.IsNullOrEmpty(tbxName.Text))
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Parameters.Add(new SqlParameter("@Email", tbxEmail.Text));
                            cmd.Parameters.Add(new SqlParameter("@Name", tbxName.Text));
                            cmd.Parameters.Add(new SqlParameter("@RegisteredDate", DateTime.Now));
                            cmd.Parameters.Add(new SqlParameter("@EmailOptInTypeID", ConfigurationManager.AppSettings["EmailOptInTypeID"]));
                            cmd.CommandText = "SP_EmailOptInType_Insert";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = sqlConnection;
                            sqlConnection.Open();

                            cmd.ExecuteNonQuery();
                        }
                    }

                    //Show success alert, hide error alert
                    successAlert.Attributes.Add("style", "display:block");
                    errorAlert.Attributes.Add("style", "display:none");
                }
                catch
                {
                    //Show error alert, hide success alert
                    successAlert.Attributes.Add("style", "display:none");
                    errorAlert.Attributes.Add("style", "display:block");
                }
            }
        }
        #endregion
    }
}