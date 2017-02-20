<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email-opt-in.aspx.cs" Inherits="HorsetraderHorseExpo.email_opt_in" %>
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

    <title>Western States Horse Expo: Register</title>

    <!-- Bootstrap core CSS -->
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="./assets/css/offcanvas.css" rel="stylesheet">

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
            <li><a href="schedule">Schedule</a></li>
            <li><a href="exhibitors">Exhibitors</a></li>
            <%--<li><a href="view-grounds-map?from=schedule">Map</a></li>
            <li><a href="assets/pdf/expo-pomona-coupons.pdf">Coupons</a></li>--%>
          </ul>
        </div><!-- /.nav-collapse -->
      </div><!-- /.container -->
    </div><!-- /.navbar -->
    
    <!--Sucess alert-->
    <div id="successAlert" runat="server" class="alert alert-success text-center">
        Thank you for registering! We'll keep you updated with schedule changes or additions
    </div>
    <!--Error alert-->
    <div id="errorAlert" runat="server" class="alert alert-danger text-center">
        There was an error registering your information. If error persists, please let us know by sending an email to: expoemail@horsetrader.com
    </div>

    <div style="margin: 10px auto; max-width: 515px; padding:10px;">
            <p class="lead text-justify">Thank your for your interest in Western States Horse Expo 2014!</p>
            <p class="lead text-justify">If you like to plan in advance, use the form below to keep updated on the latest additions or changes to the schedule, vendor or money-saving coupon lists.</p>
    </div>

    <!--Opt In Form-->
    <div class="input-form">
        <div class="form-group">
            <label for="name">Name:</label>
            <asp:TextBox ID="tbxName" runat="server" class="form-control" 
                name="name" required>
            </asp:TextBox>
        </div>

        <div class="form-group">
            <label for="email">Email:</label>
            <asp:TextBox ID="tbxEmail" runat="server" class="form-control" 
                name="email" type="email" required>
            </asp:TextBox>
        </div>

        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" 
            Text="Submit" onclick="btnSubmit_Click" />
    </div>

    <!-- Bootstrap core JavaScript
        ================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->
        <script src="./assets/scripts/jquery-1.10.2.min.js"></script>
        <script src="./assets/scripts/jquery.validate.min.js"></script>
        <script>
            $('form').validate({
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-error');
                },
                errorElement: 'span',
                errorClass: 'help-block',
                errorPlacement: function (error, element) {
                    if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    } else {
                        error.insertAfter(element);
                    }
                }
            });
        </script>
    </form>
</body>
</html>
