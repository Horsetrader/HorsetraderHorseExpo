<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="event-details.aspx.cs" Inherits="HorsetraderHorseExpo.event_details" %>
<%@ Register TagPrefix="uc" TagName="GoogleAdsCode" Src="~/usercontrols/google-ads-code.ascx" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/usercontrols/header.ascx" %>
<%@ Register TagPrefix="uc" TagName="Search" Src="~/usercontrols/search.ascx" %>
<%@ Register TagPrefix="uc" TagName="BannerAds" Src="~/usercontrols/banner-ads.ascx" %>
<%@ Register TagPrefix="uc" TagName="Footer" Src="~/usercontrols/footer.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Western States Horse Expo: Schedule</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!--Page stylesheets, etc-->
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/bootstrap.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/<%= ConfigurationManager.AppSettings["HorseExpoPubNumber"] %>.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/colorbox.css" media="screen">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>
    <link href="http://fonts.googleapis.com/css?family=Nobile" rel="stylesheet" type="text/css">

    <!--Page script files-->
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jQuery-1.8.2.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jquery.colorbox.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jquery.watermark.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/general.js"></script>

    <!--Facebook metadata-->
    <meta property="og:site_name" content="California Horsetrader" />
    <meta property="fb:admins" content="1411365756,776784509,1055847817" />
    <meta property="fb:app_id" content="115642931869437" />
    <meta property="og:type" content="Company" />
    <meta property="og:url" content="<%= PageURL %>" />
    <meta property="og:title" content="<%= PageTitle %>" />
    <meta property="og:image" content="<%= PageThumbnail %>" />
    <meta property="og:description" content="<%= PageContent %>" />

    <uc:GoogleAdsCode ID="ucGoogleAds" runat="server" />
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc:Header ID="ucHeader" runat="server" />
    <uc:Search ID="ucSearch" runat="server" />
    <div class="container">
        <uc:BannerAds ID="ucBannerAds" runat="server" />
        <div class="row">
            <div id="breadcrumbs" class="span12">
                <div id="breadcrumbsleft" class="leftside">
                    <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/house.png" style="vertical-align: -3px; margin-right: 3px;">
                    <a href="<%= ConfigurationManager.AppSettings["HorseExpoHomepage"] %>">Home</a> »
                    <a href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>">Schedule</a> » Event Details
                </div>
            </div>
        </div>
        <div class="row">
            <div class="span12" style="margin-top: 10px; border: 1px solid #E6E6E6; padding: 10px;">
                <div class="row">
                    <div class="span6">
                        <span class="redTitle" style="float: left;">
                            <asp:Label ID="lblEventName" runat="server"></asp:Label>
                        </span>
                        <div id="divShowRange" runat="server" style="clear:both; font-size: 14px;"></div>
                    </div>
                    <div class="span6" style="text-align:right;">
                        <asp:Label ID="lblLocation" runat="server"></asp:Label>
                        <a id="aShowMap" runat="server" class="example55 cboxElement"></a>
                        <br />
                        <asp:ImageButton ID="ibtnAddToList" runat="server" onclick="ibtnAddToList_Click" CssClass="add-to-list" />
                        <%--
                        <div id="divAsSeenIn" runat="server"></div>
                        <div id="divCurrentIssue" runat="server"></div>
                        --%>
                    </div>
                </div>
                <hr style="clear:both;border-top: 2px solid #E6E6E6; border-bottom: none;" />
                <div class="row">
                    <div class="span6">
                        <div id="divImage" runat="server"></div>
                    </div>
                    <div class="span6">
                        <span class="redTitle">Event details</span>
                        <div class="row">
                            <div class="span6">
                                <div id="divVideo" runat="server"></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span2 stats">Event type:</div>
                            <div class="span4">
                                <asp:Label ID="lblEventType" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span2 stats">Location:</div>
                            <div class="span4">
                                <asp:Label ID="lblFacility" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span2 stats">Start Date:</div>
                            <div class="span4">
                                <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="span2 stats">End Date:</div>
                            <div class="span4">
                                <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <p>
                            <br />
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lblBoothNumber" runat="server"></asp:Label>
                        </p>
                        <!--Contact-->
                        <%--<span class="redTitle">Contact info</span>--%>
                        <%--<div class="row">
                            <div class="span6">
                                <asp:Label ID="lblContact" runat="server"></asp:Label>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div id="divURL" runat="server" class="span6"></div>
                        </div>
                    </div>
                </div>
                <hr style="clear:both;border-top: 2px solid #E6E6E6; border-bottom: none;" />
                <div class="row">
                    <div id="divButtons" runat="server" class="span12" style="text-align:center;"></div>
                    <div id="divFacebookComments" runat="server" class="span12" style="text-align:center;"></div>
                </div>
            </div>
        </div>
        <uc:Footer ID="ucFooter" runat="server" />
    </div>
    </form>
</body>
</html>
