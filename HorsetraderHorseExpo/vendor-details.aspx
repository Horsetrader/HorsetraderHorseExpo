<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vendor-details.aspx.cs" Inherits="HorsetraderHorseExpo.vendor_details" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>

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

    <title>Western States Horse Expo: Exhibitors</title>

    <!-- Bootstrap core CSS -->
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="./assets/css/offcanvas.css" rel="stylesheet">

    <!-- Blueimp gallery CSS -->
    <link rel="stylesheet" href="assets/css/blueimp-gallery.min.css">

    <!--Facebook metadata-->
    <meta property="og:site_name" content="California Horsetrader" />
    <meta property="fb:admins" content="1411365756,776784509,1055847817" />
    <meta property="fb:app_id" content="115642931869437" />
    <meta property="og:type" content="Company" />
    <meta property="og:url" content="<%= PageURL %>" />
    <meta property="og:title" content="<%= PageTitle %>" />
    <meta property="og:image" content="<%= PageThumbnail %>" />
    <meta property="og:description" content="<%= PageContent %>" />

    <!-- Google Analytics -->
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" class="form-horizontal" runat="server">
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
            <li><a href="schedule">Schedule</a></li>
            <li class="active"><a href="exhibitors">Exhibitors</a></li>
            <li><a href="assets/pdf/ExpoMaps2014BW.pdf">Map</a></li>
            <li><a href="assets/pdf/2014WSHEcoupons.pdf">Coupons</a></li>
          </ul>
        </div><!-- /.nav-collapse -->
      </div><!-- /.container -->
    </div><!-- /.navbar -->

    <div class="container">

      <div class="row">

        <div class="col-xs-12">
          <h2 id="vendorTitle" runat="server"></h2>
          <script type="text/javascript">
              function fbs_clickAdv() {
                  u = '<%= PageURL %>';
                  t = '<%= PageTitle %>';
                  window.open('http://www.facebook.com/sharer.php?u=' + encodeURIComponent(u) + '&t=' + encodeURIComponent(t),
                                                'sharer', 'toolbar=0,status=0,width=626,height=436');
                  return false;
              }
          </script>
          <a rel="nofollow" href='http://www.facebook.com/share.php?u=<;url>&t=<%= PageTitle %>'
            onclick="return fbs_clickAdv()" target="_blank">
            <img src="http://horsetrader.com/images/share_icon.gif" alt="Share on Facebook" />
          </a>
        </div><!--/span-->

        <div class="col-xs-12 col-sm-12">
          <div class="event-details-container">
            <div class="row">

              <div class="col-xs-12 col-sm-6 text-center">
                <img id="vendorImage" runat="server" src="" class="details-main-image img-thumbnail" />
                <p id="vendorDescription" runat="server"></p>

                <div id="vendorImages" runat="server" class="text-center" style="width:100%">
                </div>

              </div><!--/span-->

              <div class="col-xs-12 col-sm-6 text-center">
                <div id="vendorVideo" runat="server">
                </div>

                <dl>
                  <dt>Contact</dt>
                  <dd id="vendorContactName" runat="server"></dd>
                  <dt>Phone</dt>
                  <dd id="vendorPhone" runat="server"></dd>
                  <dt>URL</dt>
                  <dd id="vendorUrl" runat="server"></dd>
                </dl>
              </div><!--/span-->

              <div class="col-xs-12 col col-sm-12 text-center">
                <div class="button-container">
                  <a id="viewMapMobile" runat="server" class="btn btn-primary visible-xs" role="button">
                    <span class="glyphicon glyphicon-map-marker"></span> View map
                  </a>
                  <a id="viewMap" runat="server" class="btn btn-primary visible-sm visible-md visible-lg" role="button">
                    <span class="glyphicon glyphicon-map-marker"></span> View map
                  </a>
                  <a id="addToList" runat="server" class="btn btn-primary" role="button">
                  </a>
                  <a id="sendEmailVendor" runat="server" class="btn btn-primary modalButton visible-sm visible-md visible-lg" role="button" 
                    data-toggle="modal" data-target="#emailModal" data-title="Email Exhibitor">
                    <span class="glyphicon glyphicon-envelope"></span> Email exhibitor
                  </a>
                  <a id="sendEmailVendorMobile" runat="server" class="btn btn-primary visible-xs" role="button" >
                    <span class="glyphicon glyphicon-envelope"></span> Email exhibitor
                  </a>
                  <a id="sendEmailFriend" runat="server" class="btn btn-primary modalButton visible-sm visible-md visible-lg" role="button"
                    data-toggle="modal" data-target="#emailModal" data-title="Email Info to a Friend">
                    <span class="glyphicon glyphicon-user"></span> Email info to a friend
                  </a>
                  <a id="sendEmailFriendMobile" runat="server" class="btn btn-primary visible-xs" role="button">
                    <span class="glyphicon glyphicon-user"></span> Email info to a friend
                  </a>
                </div>
              </div><!--/span-->

            </div><!--/row-->
          </div>
        </div><!--/span-->

        <div id="divFacebookComments" runat="server" class="col-xs-12 col-sm-12">
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

    <!-- Blueimp Gallery markup -->
    <div id="blueimp-gallery" class="blueimp-gallery blueimp-gallery-controls">
        <!-- The container for the modal slides -->
        <div class="slides"></div>
        <!-- Controls for the borderless lightbox -->
        <h3 class="title"></h3>
        <a class="prev">‹</a>
        <a class="next">›</a>
        <a class="close">×</a>
        <a class="play-pause"></a>
        <ol class="indicator"></ol>
    </div>

    <asp:Button ID="btnAddToList" runat="server" onclick="btnAddToList_Click" />
    <input type="hidden" id="eventOrderNumber" name="eventOrderNumber" runat="server" />

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <script src="./assets/scripts/jquery-1.10.2.min.js"></script>
    <script src="./assets/scripts/bootstrap.min.js"></script>
    <script src="./assets/scripts/offcanvas.js"></script>
    
    <!-- Blueimp Gallery JavaScript
    ================================================== -->
    <script src="assets/scripts/jquery.blueimp-gallery.min.js"></script>
    </form>
</body>
</html>
