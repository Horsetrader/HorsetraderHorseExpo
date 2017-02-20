using System;
using System.Data;
using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class ShowdateBAL
    {
        #region Properties
        public int Order_Number { get; set; }
        public string Advertiser { get; set; }
        public string Contact_First_Name { get; set; }
        public string Contact_Last_Name { get; set; }
        public string Contact_Phone { get; set; }
        public string Contact_Email { get; set; }
        public string Description { get; set; }
        public string Distribution { get; set; }
        public string Distribution_Info { get; set; }
        public string Editorial { get; set; }
        public string Editorial_Info { get; set; }
        public int Facility_ID { get; set; }
        public string Facility { get; set; }
        public string Event_Name { get; set; }
        public int Event_Type_ID { get; set; }
        public int Alliance_Event_Type_ID { get; set; }
        public string Event_Type { get; set; }
        public DateTime Event_Start_Date { get; set; }
        public string Event_Start_Time { get; set; }
        public DateTime Event_End_Date { get; set; }
        public string Event_End_Time { get; set; }
        public string Youtube_Url { get; set; }
        public string Url { get; set; }
        public List<ApprovalDTO> Approvals { get; set; }
        public int Association_ID { get; set; }
        public string Association { get; set; }
        public string Breed { get; set; }
        public int Display_Ad { get; set; }
        public string Ad_On_Page { get; set; }
        public string Sub_Facility { get; set; }
        public string Sub_Facility_Map { get; set; }
        public string Booth_Number { get; set; }
        #endregion

        #region Constructors
        public ShowdateBAL() { ; }

        public ShowdateBAL(int orderNumber)
        {
            loadObject(ShowdateDAL.GetByOrderNumber(orderNumber));
        }
        #endregion

        #region Public Methods
        public static DataSet ListByOrderNumber(List<string> orderNumbers)
        {
            string orderNumberList = string.Empty;

            //Format list to comma separated values string
            foreach (string orderNumber in orderNumbers)
            {
                orderNumberList += orderNumber + ",";
            }
            //Remove last comma in string
            if (!string.IsNullOrEmpty(orderNumberList))
                orderNumberList = orderNumberList.Substring(0, orderNumberList.Length - 1);

            return ShowdateDAL.ListByOrderNumber(orderNumberList);
        }
        #endregion

        #region Private Methods
        private void loadObject(DataSet dsShowdate)
        {
            if (dsShowdate.Tables.Count > 0)
            {
                if (dsShowdate.Tables[0].Rows.Count > 0)
                {
                    DataRow drShowdate = dsShowdate.Tables[0].Rows[0];
                    Order_Number = string.IsNullOrEmpty(drShowdate["Order_Number"].ToString()) ? -1 : int.Parse(drShowdate["Order_Number"].ToString());
                    Advertiser = string.IsNullOrEmpty(drShowdate["Advertiser"].ToString()) ? string.Empty : drShowdate["Advertiser"].ToString();
                    Contact_First_Name = string.IsNullOrEmpty(drShowdate["Contact_First_Name"].ToString()) ? string.Empty : drShowdate["Contact_First_Name"].ToString();
                    Contact_Last_Name = string.IsNullOrEmpty(drShowdate["Contact_Last_Name"].ToString()) ? string.Empty : drShowdate["Contact_Last_Name"].ToString();
                    Contact_Phone = string.IsNullOrEmpty(drShowdate["Contact_Phone"].ToString()) ? string.Empty : drShowdate["Contact_Phone"].ToString();
                    Contact_Email = string.IsNullOrEmpty(drShowdate["Contact_Email"].ToString()) ? string.Empty : drShowdate["Contact_Email"].ToString();
                    Description = string.IsNullOrEmpty(drShowdate["Description"].ToString()) ? string.Empty : drShowdate["Description"].ToString();
                    Distribution = string.IsNullOrEmpty(drShowdate["Distribution"].ToString()) ? string.Empty : drShowdate["Distribution"].ToString();
                    Distribution_Info = string.IsNullOrEmpty(drShowdate["Distribution_Info"].ToString()) ? string.Empty : drShowdate["Distribution_Info"].ToString();
                    Editorial = string.IsNullOrEmpty(drShowdate["Editorial"].ToString()) ? string.Empty : drShowdate["Editorial"].ToString();
                    Editorial_Info = string.IsNullOrEmpty(drShowdate["Editorial_Info"].ToString()) ? string.Empty : drShowdate["Editorial_Info"].ToString();
                    Facility_ID = string.IsNullOrEmpty(drShowdate["Facility_ID"].ToString()) ? -1 : int.Parse(drShowdate["Facility_ID"].ToString());
                    Facility = string.IsNullOrEmpty(drShowdate["Facility"].ToString()) ? string.Empty : drShowdate["Facility"].ToString();
                    Event_Name = string.IsNullOrEmpty(drShowdate["Event_Name"].ToString()) ? string.Empty : drShowdate["Event_Name"].ToString();
                    Event_Type_ID = string.IsNullOrEmpty(drShowdate["Event_Type_ID"].ToString()) ? -1 : int.Parse(drShowdate["Event_Type_ID"].ToString());
                    Event_Type = string.IsNullOrEmpty(drShowdate["Event_Type"].ToString()) ? string.Empty : drShowdate["Event_Type"].ToString();
                    Event_Start_Date = string.IsNullOrEmpty(drShowdate["Event_Start_Date"].ToString()) ? DateTime.MinValue : DateTime.Parse(drShowdate["Event_Start_Date"].ToString());
                    Event_Start_Time = string.IsNullOrEmpty(drShowdate["Event_Start_Time"].ToString()) ? string.Empty : drShowdate["Event_Start_Time"].ToString();
                    Event_End_Date = string.IsNullOrEmpty(drShowdate["Event_End_Date"].ToString()) ? DateTime.MinValue : DateTime.Parse(drShowdate["Event_End_Date"].ToString());
                    Event_End_Time = string.IsNullOrEmpty(drShowdate["Event_End_Time"].ToString()) ? string.Empty : drShowdate["Event_End_Time"].ToString();
                    Youtube_Url = string.IsNullOrEmpty(drShowdate["Youtube_Url"].ToString()) ? string.Empty : drShowdate["Youtube_Url"].ToString();
                    Url = string.IsNullOrEmpty(drShowdate["Url"].ToString()) ? string.Empty : drShowdate["Url"].ToString();
                    Approvals = new List<ApprovalDTO>();
                    Association_ID = string.IsNullOrEmpty(drShowdate["Association_ID"].ToString()) ? -1 : int.Parse(drShowdate["Association_ID"].ToString());
                    Association = string.IsNullOrEmpty(drShowdate["Association"].ToString()) ? string.Empty : drShowdate["Association"].ToString();
                    Breed = string.IsNullOrEmpty(drShowdate["Breed"].ToString()) ? string.Empty : drShowdate["Breed"].ToString();
                    Display_Ad = string.IsNullOrEmpty(drShowdate["Display_Ad"].ToString()) ? -1 : int.Parse(drShowdate["Display_Ad"].ToString());
                    Ad_On_Page = string.IsNullOrEmpty(drShowdate["Ad_On_Page"].ToString()) ? string.Empty : drShowdate["Ad_On_Page"].ToString();
                    Sub_Facility = string.IsNullOrEmpty(drShowdate["Sub_Facility"].ToString()) ? string.Empty : drShowdate["Sub_Facility"].ToString();
                    Sub_Facility_Map = string.IsNullOrEmpty(drShowdate["Sub_Facility_Map"].ToString()) ? string.Empty : drShowdate["Sub_Facility_Map"].ToString();
                    Booth_Number = string.IsNullOrEmpty(drShowdate["Booth_Number"].ToString()) ? string.Empty : drShowdate["Booth_Number"].ToString();
                }
                if(dsShowdate.Tables[1].Rows.Count > 0)
                    loadApprovals(dsShowdate.Tables[1]);
            }
        }

        private void loadApprovals(DataTable dtApprovals)
        {
            List<ApprovalDTO> approvalList = new List<ApprovalDTO>();
            foreach (DataRow dr in dtApprovals.Rows)
            {
                ApprovalDTO approvalDTO = new ApprovalDTO();
                approvalDTO.Approval_Number = string.IsNullOrEmpty(dr["Approval_Number"].ToString()) ? -1 : int.Parse(dr["Approval_Number"].ToString());
                approvalDTO.Approval_Description = dr["Approval_Description"].ToString();
                approvalList.Add(approvalDTO);
            }

            Approvals = approvalList;
        }

        private string GetApprovalNumbersList()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (BLL.ApprovalDTO approval in this.Approvals)
            {
                sb.AppendFormat("{0},", approval.Approval_Number);
            }

            if (sb.Length > 0)
                sb.Remove((sb.Length - 1), 1);

            return sb.ToString();
        }

        private int GetAllianceEventType()
        {
            DataTable dtAllianceEventType = ShowdateDAL.GetAllianceEventType(this.Event_Type_ID);
            int allianceEventTypeID = -1;

            if (dtAllianceEventType.Rows.Count > 0)
                int.TryParse(dtAllianceEventType.Rows[0]["Alliance_Event_Type_ID"].ToString(), out allianceEventTypeID);

            return allianceEventTypeID;
        }
        #endregion
    }
}
