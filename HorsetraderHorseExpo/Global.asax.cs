using System;
using System.Web.Routing;

namespace HorsetraderHorseExpo
{
    public class Global : System.Web.HttpApplication
    {
        void RegisterRoutes(RouteCollection routes)
        {
            //Schedule
            routes.MapPageRoute("schedule", "schedule", "~/index2.aspx");
            //Event Details
            routes.MapPageRoute("eventDetails", "event-details", "~/event-details2.aspx");
            //Exhibitor Listing
            routes.MapPageRoute("exhibitors", "exhibitors", "~/vendors-listing.aspx");
            //Vendor Details
            routes.MapPageRoute("exhibitorDetails", "exhibitor-details", "~/vendor-details.aspx");
            //Search Results
            routes.MapPageRoute("searchResults", "search", "~/results2.aspx");
            //Print Event List
            routes.MapPageRoute("printEventList", "print-event-list", "~/print-event-list.aspx");
            //Print Exhibitor List
            routes.MapPageRoute("printExhibitorList", "print-exhibitor-list", "~/print-exhibitor-list.aspx");
            //Alternative Print Event List 1
            routes.MapPageRoute("altprint1", "altprint1", "~/print-list-sample-1.aspx");
            //Alternative Print Event List 2
            routes.MapPageRoute("altprint2", "altprint2", "~/print-list-sample-2.aspx");
            //Email
            routes.MapPageRoute("email", "email", "~/send-email.aspx");
            //View Map
            routes.MapPageRoute("viewMap", "view-map", "~/view-map.aspx");
            //View Grounds Map
            routes.MapPageRoute("viewGroundsMap", "view-grounds-map", "~/view-grounds-map.aspx");
            //Register
            routes.MapPageRoute("register", "register", "~/email-opt-in.aspx");
        }

        void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
