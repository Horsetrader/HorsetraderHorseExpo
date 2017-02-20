<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="HorsetraderHorseExpo.index2" %>
<%@ Register TagPrefix="uc" TagName="EventQueue" Src="~/usercontrols/event-queue.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleAdsCode" Src="~/usercontrols/google-ads-code.ascx" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>
<%@ Register TagPrefix="uc" TagName="BannerAds" Src="~/usercontrols/banner-ads.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="#">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <title>Western States Horse Expo: Schedule</title>

    <!-- Bootstrap core CSS -->
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="./assets/css/offcanvas.css" rel="stylesheet">

    <!--Facebook metadata-->
    <meta property="og:site_name" content="California Horsetrader" />
    <meta property="fb:admins" content="1411365756,776784509,1055847817" />
    <meta property="fb:app_id" content="115642931869437" />
    <meta property="og:type" content="Company" />
    <meta property="og:url" content="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>" />
    <meta property="og:title" content="2014 Western States Horse Expo Schedule" />
    <%--<meta property="og:image" content="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] + "/assets/images/horse-expo-pomona-logo.png" %>" />--%>
    <meta property="og:description" content="The largest and most comprehensive equestrian exposition in North America" />

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
            <li class="active"><a href="#">Schedule</a></li>
            <li><a href="exhibitors">Exhibitors</a></li>
            <li><a href="assets/pdf/ExpoMaps2014BW.pdf">Map</a></li>
            <li><a href="assets/pdf/2014WSHEcoupons.pdf">Coupons</a></li>
          </ul>
        </div><!-- /.nav-collapse -->
      </div><!-- /.container -->
    </div><!-- /.navbar -->

    <div class="container">
      
      <%--<uc:BannerAds ID="ucBannerAds" runat="server" />--%>

      <div class="row" style="margin-bottom: 10px;">
        <div class="col-xs-12 col-sm-12 col-md-7">
            <a class="visible-md visible-lg" href="https://docs.google.com/spreadsheet/ccc?key=0AslZcQFESyeGdExtejlWaGZlRDkzVDVOR1U3Q1J1OWc&usp=sharing#gid=0" target="_blank">
                <img src="../assets/images/spreadsheet-expo-calendar-banner.jpg" />
            </a>
            <a class="visible-xs visible-sm" href="https://docs.google.com/spreadsheet/ccc?key=0AslZcQFESyeGdExtejlWaGZlRDkzVDVOR1U3Q1J1OWc&usp=sharing#gid=0" target="_blank">
                <img src="../assets/images/spreadsheet-expo-calendar-mobile.jpg" />
            </a>
        </div>
        <div class="visible-sm visible-md visible-lg col-sm-5">
            <div id="action-item-right" style="width:205px;">
                <span>Powered by:</span>
                <a href="http://horsetrader.com">
                    <img src="../assets/images/horsetrader-logo.jpg">
                </a>
            </div>
            <div id="action-item-left">
                
            </div>
        </div>
      </div>

      <div class="row row-offcanvas row-offcanvas-right">

        <div class="col-xs-12 col-sm-8 col-md-9">
          <p class="pull-right visible-xs">
            <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
              View my list 
              <span class="glyphicon glyphicon-chevron-right"></span>
            </button>
          </p>

          <h2>2014 Western States Horse Expo Calendar</h2>

          <h4 class="visible-sm visible-md visible-lg" style="margin-top:-10px;">
            <a style="text-decoration:none;" href="register">
            <span style="font-size:30px;" class="glyphicon glyphicon-arrow-right"></span> 
            <span style="vertical-align:super;">Register - Get updates of Western States Horse Expo schedule changes or additions!</span>
            </a>
          </h4>
          <!-- Nav tabs -->
          <ul class="nav nav-tabs">
            <li class="active"><a href="#day1" data-toggle="tab">Day 1<br>Friday</a></li>
            <li class=""><a href="#day2" data-toggle="tab">Day 2<br>Saturday</a></li>
            <li class=""><a href="#day3" data-toggle="tab">Day 3<br>Sunday</a></li>
          </ul>
          <!-- Tab panes -->
          <div class="tab-content">
            <div class="tab-pane active" id="day1">
              <h2>June 13 2014</h2>
              <div class="row">
                <asp:Repeater ID="repScheduleDay1" runat="server" onitemdatabound="repeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-12 col-sm-12 col-lg-12 schedule-event">
                          <h3>
                            <%# GetScheduleTime(Eval("event_start_time").ToString(), Eval("event_end_time").ToString())%>
                          </h3>
                          <h4>
                            <%# Eval("event_name")%>
                          </h4>
                          <img class="img-thumbnail vendor-thumbnail" src='<%# ConfigurationManager.AppSettings["ImagesURL"] %>/<%# Eval("foto_file") %>' alt='<%# Eval("event_name") %>'>
                          <p>
                            <%# Eval("description")%>
                          </p>
                          <p class="schedule-event-action">
                            <a class="btn btn-default" href="event-details?id=<%# Eval("order_number") %>" role="button">
                              More details 
                              <span class="glyphicon glyphicon-chevron-right"></span>
                            </a>
                            <%# GetAddToListButtonHTML(Eval("order_number").ToString())%>
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
            <div class="tab-pane" id="day2">
              <h2>June 14 2014</h2>
              <div class="row">
                <asp:Repeater ID="repScheduleDay2" runat="server" onitemdatabound="repeater_ItemDataBound">
                    <ItemTemplate>
                        <div id='<%# Eval("order_number")%>' class="col-12 col-sm-12 col-lg-12 schedule-event">
                          <h3>
                            <%# GetScheduleTime(Eval("event_start_time").ToString(), Eval("event_end_time").ToString())%>
                          </h3>
                          <h4>
                            <%# Eval("event_name")%>
                          </h4>
                          <img class="img-thumbnail vendor-thumbnail" src='<%# ConfigurationManager.AppSettings["ImagesURL"] %>/<%# Eval("foto_file") %>' alt='<%# Eval("event_name") %>'>
                          <p>
                            <%# Eval("description")%>
                          </p>
                          <p class="schedule-event-action">
                            <a class="btn btn-default" href="event-details?id=<%# Eval("order_number") %>" role="button">
                              More details 
                              <span class="glyphicon glyphicon-chevron-right"></span>
                            </a>
                            <%# GetAddToListButtonHTML(Eval("order_number").ToString())%>
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

            <div class="tab-pane" id="day3">
                <h2>June 15 2014</h2>
              <div class="row">
                <asp:Repeater ID="repScheduleDay3" runat="server" onitemdatabound="repeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="col-12 col-sm-12 col-lg-12 schedule-event">
                          <h3>
                            <%# GetScheduleTime(Eval("event_start_time").ToString(), Eval("event_end_time").ToString())%>
                          </h3>
                          <h4>
                            <%# Eval("event_name")%>
                          </h4>
                          <img class="img-thumbnail vendor-thumbnail" src='<%# ConfigurationManager.AppSettings["ImagesURL"] %>/<%# Eval("foto_file") %>' alt='<%# Eval("event_name") %>'>
                          <p>
                            <%# Eval("description")%>
                          </p>
                          <p class="schedule-event-action">
                            <a class="btn btn-default" href="event-details?id=<%# Eval("order_number") %>" role="button">
                              More details 
                              <span class="glyphicon glyphicon-chevron-right"></span>
                            </a>
                            <%# GetAddToListButtonHTML(Eval("order_number").ToString())%>
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

        <div class="col-xs-6 col-sm-4 col-md-3 sidebar-offcanvas" id="sidebar" role="navigation">
          <uc:EventQueue ID="ucEventQueue" runat="server" />
        </div><!--/span-->
      </div><!--/row-->

      <footer>
        <p></p>
      </footer>

    </div><!--/.container-->

    <!-- Email Modal -->
    <div class="modal fade" id="emailModal" tabindex="-1" role="dialog" aria-labelledby="emailModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h4 class="modal-title" id="emailModalLabel"></h4>
          </div>  
          <iframe id="sendEmail" class="modal-iframe" frameborder="0"></iframe>
        </div><!-- /.modal-content -->
      </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->

    <asp:Button ID="btnAddToList" runat="server" onclick="btnAddToList_Click" />
    <input type="hidden" id="eventOrderNumber" name="eventOrderNumber" runat="server" />
    <asp:HiddenField ID="selectedTab" runat="server" Value="#day1" />

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="./assets/scripts/jquery-1.10.2.min.js"></script>
    <script src="./assets/scripts/bootstrap.min.js"></script>
    <script src="./assets/scripts/offcanvas.js"></script>
    <script>
        $(document).ready(function () {

            var tab = document.getElementById('<%= selectedTab.ClientID%>').value;

            $('.nav-tabs a').click(function (e) {
                tab = $(this).attr('href');
                document.getElementById('<%=selectedTab.ClientID %>').value = tab;
            })

            $('.nav-tabs a[href=' + tab + ']').tab('show');
        });
    </script>
    </form>
</body>
</html>