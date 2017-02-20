using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BLL;

namespace HorsetraderHorseExpo
{
    public partial class vendors_listing : System.Web.UI.Page
    {
        #region Global Variables
        protected string ExpoVendorIDs
        {
            get
            {
                if (ViewState["ExpoVendorIDs"] != null)
                    return ViewState["ExpoVendorIDs"].ToString();
                else if (Request.Cookies["ExpoVendorIDs"] != null)
                    return Request.Cookies["ExpoVendorIDs"].Value;
                else
                    return string.Empty;
            }
            set
            {
                ViewState["ExpoVendorIDs"] = value;
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void LoadPage()
        {
            if (!IsPostBack)
            {
                LoadExpoVendorIDs();
                LoadVendors(false);
                LoadCategoryFilter();
            }

            AttachEventHandlerToUserControlButtonClick();
        }

        private void LoadExpoVendorIDs()
        {
            if (Request.Cookies["ExpoVendorIDs"] != null)
                ExpoVendorIDs = Request.Cookies["ExpoVendorIDs"].Value;
        }

        private void AttachEventHandlerToUserControlButtonClick()
        {
            ucVendorList.ibtnRemoveFromListClicked += new EventHandler(ucVendorList_IbtnRemoveFromListClicked);
        }

        private void LoadVendors(bool sortData)
        {
            try
            {
                //Get ID for Event
                int expoEventId = int.Parse(ConfigurationManager.AppSettings["ExpoEventID"]);

                DataView dvVendors = GetVendors(expoEventId);
                dvVendors = sortData ? SortData(dvVendors) : dvVendors;

                //Get only the events for the first day and bind them to repeater for Building A
                dvVendors.RowFilter = "Location = 'Bldg. A'";
                if(!string.IsNullOrEmpty(ddlVendorCategory.SelectedValue))
                    dvVendors.RowFilter += "AND ExpoVendorCategoryID = " + ddlVendorCategory.SelectedValue;
                repBuildingA.DataSource = dvVendors;
                repBuildingA.DataBind();

                //Get only the events for the second day and bind them to repeater for Building B
                dvVendors.RowFilter = "Location = 'Bldg. B'";
                if (!string.IsNullOrEmpty(ddlVendorCategory.SelectedValue))
                    dvVendors.RowFilter += "AND ExpoVendorCategoryID = " + ddlVendorCategory.SelectedValue;
                repBuildingB.DataSource = dvVendors;
                repBuildingB.DataBind();

                //Get only the events for the third day and bind them to repeater for Building C
                dvVendors.RowFilter = "Location = 'Bldg. C'";
                if (!string.IsNullOrEmpty(ddlVendorCategory.SelectedValue))
                    dvVendors.RowFilter += "AND ExpoVendorCategoryID = " + ddlVendorCategory.SelectedValue;
                repBuildingC.DataSource = dvVendors;
                repBuildingC.DataBind();

                //Get only the events for the third day and bind them to repeater for Building D
                dvVendors.RowFilter = "Location = 'Bldg. D'";
                if (!string.IsNullOrEmpty(ddlVendorCategory.SelectedValue))
                    dvVendors.RowFilter += "AND ExpoVendorCategoryID = " + ddlVendorCategory.SelectedValue;
                repBuildingD.DataSource = dvVendors;
                repBuildingD.DataBind();

                //Get only the events for the third day and bind them to repeater for All Buildings
                dvVendors.RowFilter = "";
                if (!string.IsNullOrEmpty(ddlVendorCategory.SelectedValue))
                    dvVendors.RowFilter = "ExpoVendorCategoryID = " + ddlVendorCategory.SelectedValue;
                repAllBuildings.DataSource = dvVendors;
                repAllBuildings.DataBind();
            }
            catch
            { ;}
        }

        private void LoadCategoryFilter()
        {
            ddlVendorCategory.DataSource = GetVendorCategories();
            ddlVendorCategory.DataTextField = "Description";
            ddlVendorCategory.DataValueField = "ExpoVendorCategoryID";
            ddlVendorCategory.DataBind();

            ddlVendorCategory.Items.Insert(0, new ListItem("Category - All",""));
        }
        #endregion

        #region Private Functions
        private DataView GetVendors(int expoEventId)
        {
            DataSet dsVendors = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.Add(new SqlParameter("@ExpoEventID", expoEventId));
                    cmd.CommandText = "sp_ht_list_vendorsForExpo";
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

            if (dsVendors.Tables.Count > 0)
                return new DataView(dsVendors.Tables[0]);

            return new DataView();
        }

        private DataSet GetVendorCategories()
        {
            DataSet dsVendorCategories = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "sp_ExpoVendorCategory_List";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    var adapter = new SqlDataAdapter(cmd);

                    try
                    {
                        adapter.Fill(dsVendorCategories);
                    }
                    catch (Exception e)
                    {
                        //manage remote db when working local
                    }
                }
            }

            return dsVendorCategories;
        }

        private DataView SortData(DataView dvAdList)
        {
            //try
            //{
            //    dvAdList.Sort = ddlSort.SelectedValue;
            //}
            //catch (Exception e)
            //{ }

            return dvAdList;
        }

        private void LoadVendorList()
        {
            try
            {
                Repeater repExhibitorList = (Repeater)ucVendorList.FindControl("repExhibitorList");

                if (!string.IsNullOrEmpty(ExpoVendorIDs))
                {
                    string[] expoVendorIDArray = ExpoVendorIDs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> eventList = new List<string>(expoVendorIDArray);
                    DataSet dsEventList = ShowdateBLL.ListByExpoVendorID(eventList);
                    DataView dvEventList = dsEventList.Tables.Count > 0 ? new DataView(dsEventList.Tables[0]) : new DataView();

                    repExhibitorList.DataSource = dvEventList;
                    repExhibitorList.DataBind();
                }
                else
                {
                    repExhibitorList.DataSource = new DataView();
                    repExhibitorList.DataBind();
                }
            }
            catch
            { ;}
        }
        #endregion

        #region Protected Functions
        protected string GetScheduleTime(string startTime, string endTime)
        {
            string scheduleTime = string.Empty;

            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
                scheduleTime = string.Format("{0} - {1}", startTime, endTime);
            else if (!string.IsNullOrEmpty(startTime))
                scheduleTime += string.Format("{0}", startTime);

            return scheduleTime;
        }

        protected string GetImageHTML(string imageFile, string vendorName)
        {
            string html = string.Empty;

            if (!string.IsNullOrEmpty(imageFile))
            {
                html = string.Format(
                            "<img class='img-thumbnail vendor-thumbnail' src='{0}/{1}' alt='{2}'>",
                            ConfigurationManager.AppSettings["ImagesURL"],
                            imageFile,
                            vendorName);
            }

            return html;
        }

        protected string GetAddToListButtonHTML(string expoVendorId)
        {
            string buttonHtml = "<a class='btn btn-default' href='#" + expoVendorId + "' add-to-list='" + expoVendorId + "'>" +
                                  "<span class='glyphicon glyphicon-unchecked'></span> Add to list" +
                                "</a>";

            if (ExpoVendorIDs.Contains(expoVendorId))
                buttonHtml = "<a class='btn btn-success' href='#" + expoVendorId + "' add-to-list='" + expoVendorId + "'>" +
                                "<span class='glyphicon glyphicon-check'></span> Added to list" +
                             "</a>";

            return buttonHtml;
        }

        protected string GetMoreDetailsButtonHTML(bool isAdvertiser, string vendorId)
        {
            string buttonHtml = string.Empty;
            if (isAdvertiser)
            {
                buttonHtml = "<a class='btn btn-default' href='exhibitor-details?id=" + vendorId + "' role='button'>" +
                                "More details <span class='glyphicon glyphicon-chevron-right'></span>" +
                             "</a>";
            }

            return buttonHtml;
        }
        #endregion

        #region Event Handlers
        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            string expoVendorIDList = string.Empty;
            //ImageButton ibtnAddToList = (ImageButton)sender;
            bool addedToList; //= ibtnAddToList.ImageUrl.Contains("on-my-list");
            string orderNumber = eventOrderNumber.Value; //ibtnAddToList.CommandArgument;

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
            ExpoVendorIDs = expoVendorIDList;
            Response.Cookies["ExpoVendorIDs"].Value = expoVendorIDList;
            Response.Cookies["ExpoVendorIDs"].Expires = DateTime.Now.AddDays(90);

            //Change ImageButton image to "on my list"
            //ibtnAddToList.ImageUrl = addedToList ? "~/assets/images/add-to-list.png" : "~/assets/images/on-my-list.png";
            LoadVendors(false);
            //Load EventList usercontrol
            LoadVendorList();
        }

        protected void ucVendorList_IbtnRemoveFromListClicked(object sender, EventArgs e)
        {
            //Remove order number from Cookie
            if (Session["ExpoVendorIDToRemove"] != null)
            {
                string expoVendorId = (string)Session["ExpoVendorIDToRemove"];
                string expoVendorIDs = string.Empty;

                if (Request.Cookies["ExpoVendorIDs"] != null)
                {
                    expoVendorIDs = Request.Cookies["ExpoVendorIDs"].Value;

                    if (expoVendorIDs.Contains(expoVendorId))
                        expoVendorIDs = expoVendorIDs.Replace(expoVendorId + "|", "");
                }

                ExpoVendorIDs = expoVendorIDs;
                Session["ExpoVendorIDToRemove"] = null;
            }

            bool sortData = false;//ddlSort.SelectedIndex != 0;
            LoadVendors(sortData);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent(string.Format("{0}/search?q={1}",
                ConfigurationManager.AppSettings["HorseExpoWebsiteURL"], tbxSearch.Text));
        }

        protected void ddlVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVendors(false);
        }

        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater repeater = (Repeater)sender;

            if (repeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = new Label();
                    for (int i = 0; i < e.Item.Controls.Count; i++)
                    {
                        if (e.Item.Controls[i].GetType() == lblFooter.GetType())
                        {
                            lblFooter = (Label)e.Item.Controls[i];
                            lblFooter.Visible = true;
                            lblFooter.Text = "Oops! No exhibitors here. Please choose another building or filter by a different category";

                        }
                    }
                }
            }
        }
        #endregion
    }
}