using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class print_exhibitor_list : System.Web.UI.Page
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
            if (Session["ExpoVendorIDList"] != null)
            {
                string orderNumbers = (string)Session["ExpoVendorIDList"];
                string[] splitCharacter = new string[] { "|" };
                orderNumberList = orderNumbers.Split(splitCharacter, StringSplitOptions.RemoveEmptyEntries);
            }

            return orderNumberList.ToList<string>(); ;
        }

        private void LoadEventList()
        {
            List<string> eventList = LoadArgsFromSession();

            DataSet dsExhibitorList = ShowdateBLL.ListByExpoVendorID(eventList);
            DataView dvExhibitorList = dsExhibitorList.Tables.Count > 0 ? new DataView(dsExhibitorList.Tables[0]) : new DataView();

            repExhibitorList.DataSource = dvExhibitorList;
            repExhibitorList.DataBind();
        }
        #endregion

        #region Functions
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