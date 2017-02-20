<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="results2.aspx.cs" Inherits="HorsetraderHorseExpo.results2" %>
<%@ Register TagPrefix="uc" TagName="EventQueue" Src="~/usercontrols/event-queue.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleAdsCode" Src="~/usercontrols/google-ads-code.ascx" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>
<%@ Register TagPrefix="uc" TagName="BannerAds" Src="~/usercontrols/banner-ads.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head id="head1" runat="server">
	<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="#">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <title>Western States Horse Expo: Search Results</title>

    <!-- Bootstrap core CSS -->
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="./assets/css/offcanvas.css" rel="stylesheet">

    <!-- Google Ads -->
    <uc:GoogleAdsCode ID="ucGoogleAds" runat="server" />

    <!-- Google Analytics -->
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar navbar-inverse" role="navigation">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <div class="navbar-brand">
            <a href="<%= ConfigurationManager.AppSettings["HorseExpoHomepage"] %>">
                <img src="assets/images/logo.png" style="width: 130px;" />
            </a>
          </div>
        </div>
        <div class="collapse navbar-collapse">
          <div class="navbar-form navbar-right" role="search">
            <div class="right-inner-addon ">
                <span class="glyphicon glyphicon-search"></span>
                <asp:TextBox ID="tbxSearch" runat="server" placeholder="Type here to search..." CssClass="form-control"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearch" onclick="btnSearch_Click" />
            </div>
          </div>
          <ul class="nav navbar-nav">
            <li class="active"><a href="schedule">Schedule</a></li>
            <li><a href="exhibitors">Exhibitors</a></li>
            <%--<li><a href="view-grounds-map?from=search">Map</a></li>
            <li><a href="assets/pdf/expo-pomona-coupons.pdf">Coupons</a></li>--%>
          </ul>
        </div><!-- /.nav-collapse -->
      </div><!-- /.container -->
    </div><!-- /.navbar -->

    <div class="container">

      <uc:BannerAds ID="ucBannerAds" runat="server" />

      <div class="row">

        <div class="col-xs-12">

          <h2 id="resultsLabel" runat="server"></h2>

          <!-- Nav tabs -->
          <ul class="nav nav-tabs">
            <li class="active"><a id="tabExhibitorsResults" runat="server" href="#exhibitorResults" data-toggle="tab">Exhibitors</a></li>
            <li class=""><a id="tabEventsResults" runat="server" href="#eventResults" data-toggle="tab">Scheduled Events</a></li>
          </ul>
          <!-- Tab panes -->
          <div class="tab-content">
            <div class="tab-pane active" id="exhibitorResults">
              <div class="row">
                <asp:Repeater ID="repExhibitorResults" runat="server" onitemdatabound="repeater_ItemDataBound">
                    <ItemTemplate>
                        <div class='col-12 col-sm-12 col-lg-12 <%# Container.ItemIndex % 2 == 0 ? "vendor-listing" : "vendor-listing-alt" %>'>
                            <h3>
                                <%# Eval("Name") %>
                                <br>
                                <small><%# Eval("Location") %> - Booth <%# Eval("Booth") %></small>
                            </h3>
                            <div class="row">
                                <div class="col-xs-6 col-sm-8">
                                    <%# GetImageHTML(Eval("ImageFileName").ToString(), Eval("Name").ToString())%>
                                </div>
                                <!--/span-->
                                <div class="col-xs-6 col-sm-4">
                                    <div class="vendor-listing-action">
                                        <%# GetMoreDetailsButtonHTML((bool)Eval("IsAdvertiser"), Eval("ExpoVendorID").ToString())%>
                                    </div>
                                </div><!--/span-->

                            </div><!--/row-->

                        </div><!--/span-->
                    </ItemTemplate>
                    <FooterTemplate>
                        <p class="no-data">
                            <asp:Label ID="lblEmptyData" runat="server" Visible="false"></asp:Label>
                        </p>
                    </FooterTemplate>
                </asp:Repeater>
              </div><!--/row-->
            </div><!--/tab pane-->

            <div class="tab-pane" id="eventResults">
              <div class="row">
                <asp:Repeater ID="repEventResults" runat="server" onitemdatabound="repeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-12 col-sm-12 col-lg-12 <%# Container.ItemIndex % 2 == 0 ? "schedule-event" : "schedule-event-alt" %>">
                          <h3>
                            <%# GetScheduleTime(Eval("event_start_date").ToString(), Eval("event_start_time").ToString(), Eval("event_end_time").ToString())%>
                          </h3>
                          <h4>
                            <%# Eval("event_name")%>
                          </h4>
                          <img class="img-thumbnail vendor-thumbnail" src='<%#ConfigurationManager.AppSettings["ImagesURL"] %>/<%# Eval("foto_file") %>' alt='<%# Eval("event_name") %>'>
                          <p>
                            <%# Eval("description")%>
                          </p>
                          <p class="schedule-event-action">
                            <a class="btn btn-default" href="event-details?id=<%# Eval("order_number") %>" role="button">
                              More details 
                              <span class="glyphicon glyphicon-chevron-right"></span>
                            </a>
                          </p>
                        </div><!--/span-->
                    </ItemTemplate>
                    <FooterTemplate>
                        <p class="no-data">
                            <asp:Label ID="lblEmptyData" runat="server" Visible="false"></asp:Label>
                        </p>
                    </FooterTemplate>
                </asp:Repeater>

              </div><!--/row-->
            </div><!--/tab pane-->
          </div>
        </div><!--/span-->

      </div><!--/row-->

      <footer>
        <p></p>
      </footer>

    </div><!--/.container-->

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="./assets/scripts/jquery-1.10.2.min.js"></script>
    <script src="./assets/scripts/bootstrap.min.js"></script>
    <script src="./assets/scripts/offcanvas.js"></script>
    </form>
</body>
</html>