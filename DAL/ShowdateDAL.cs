using System;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class ShowdateDAL : BaseDAL
    {
        public static DataSet GetByOrderNumber(int orderNumber)
        {   
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString);
            sql.AddParameter("@OrderNumber", orderNumber);
            sql.AddParameter("@PubNumber", ConfigurationManager.AppSettings["HorseExpoPubNumber"]);

            DataSet dsResult = sql.Execute("sp_ht_GetShowdateByPub", CommandType.StoredProcedure);
            return dsResult;
        }

        public static DataSet ListByOrderNumber(string orderNumberList)
        {
            DataSet dsResult = new DataSet();

            if (!string.IsNullOrEmpty(orderNumberList))
            {
                SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString);
                sql.AddParameter("@OrderNumberList", orderNumberList);

                dsResult = sql.Execute("sp_ht_ListShowdatesByOrderNumber", CommandType.StoredProcedure);
            }
            
            return dsResult;
        }

        public static DataSet ListExpoVendorID(string expoVendorIdList)
        {
            DataSet dsResult = new DataSet();

            if (!string.IsNullOrEmpty(expoVendorIdList))
            {
                SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["HorseExpoConnectionString"].ConnectionString);
                sql.AddParameter("@ExpoVendorIDList", expoVendorIdList);

                dsResult = sql.Execute("sp_ht_ListExpoVendorsByID", CommandType.StoredProcedure);
            }

            return dsResult;
        }

        public static DataTable GetAllianceEventType(int eventTypeID)
        {
            SQLHelper sql = new SQLHelper(ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString);
            sql.AddParameter("@EventTypeID", eventTypeID);
            DataSet dsShowdateSections = sql.Execute("Usp_GetAllianceEventType", CommandType.StoredProcedure);

            if (dsShowdateSections.Tables.Count > 0)
                return dsShowdateSections.Tables[0];
            else
                return new DataTable();
        }
    }
}
