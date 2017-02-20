<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HorsetraderHorseExpo.index" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>
<%@ Register TagPrefix="uc" TagName="GoogleAdsCode" Src="~/usercontrols/google-ads-code.ascx" %>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/usercontrols/header.ascx" %>
<%@ Register TagPrefix="uc" TagName="Search" Src="~/usercontrols/search.ascx" %>
<%@ Register TagPrefix="uc" TagName="BannerAds" Src="~/usercontrols/banner-ads.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventQueue" Src="~/usercontrols/event-queue.ascx" %>
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
    <link href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" rel="Stylesheet" type="text/css">

    <!--Page script files-->
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jQuery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jquery.colorbox.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jquery.watermark.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/general.js"></script>

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
                    <a href="<%= ConfigurationManager.AppSettings["HorseExpoHomepage"] %>">Home</a> » Schedule
                </div>
            </div>
        </div>
        <div class="row">
            <div class="span3">
                <uc:EventQueue ID="ucEventQueue" runat="server" />
            </div>
            <div class="span9">
                <div class="topSection">
                    <div class="titleSection">
                        <div class="redTitleSection">
                            <span id="spanTitle" runat="server" class="redTitle"></span>
                            <asp:Label runat="server" ID="lblTotalRecords"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="filters span9">
                        <asp:RadioButton ID="rbAllDays" runat="server" Text="ALL days" AutoPostBack="true"
                            GroupName="DateFilter" oncheckedchanged="rbDateFilter_CheckedChanged" Checked="true" />
                        <asp:RadioButton ID="rbFriday" runat="server" Text="Friday February 7" AutoPostBack="true"
                            GroupName="DateFilter" oncheckedchanged="rbDateFilter_CheckedChanged" />
                        <asp:RadioButton ID="rbSaturday" runat="server" Text="Saturday February 8" AutoPostBack="true"
                            GroupName="DateFilter" oncheckedchanged="rbDateFilter_CheckedChanged" />
                        <asp:RadioButton ID="rbSunday" runat="server" Text="Sunday February 9" AutoPostBack="true"
                            GroupName="DateFilter" oncheckedchanged="rbDateFilter_CheckedChanged" />
                        <br /><br />
                        <span style="margin-left:5px">Sort by:</span>
                        <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSort_SelectedIndexChanged">
                            <asp:ListItem Value="">Select...</asp:ListItem>
                            <asp:ListItem Value="event_name ASC, event_start_date ASC">
                                Clinician or event name
                            </asp:ListItem>
                            <asp:ListItem Value="event_start_date ASC">
                                Day
                            </asp:ListItem>
                            <asp:ListItem Value="nullEventStartTimeSort ASC, event_start_time ASC,
                                event_start_date ASC">
                                Time of day
                            </asp:ListItem>
                            <asp:ListItem Value="nullSubFacilitySort ASC, sub_facility ASC,
                                event_start_date ASC">
                                Arena or location
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="showsResults">
                    <asp:Repeater runat="server" ID="repShows" onitemdatabound="repAds_ItemDataBound">
                    <ItemTemplate>
                        <div class="searchresultsingle">
                            <div class="searchresultsingle_left">
                                <%# GenerateImageHTML(Eval("foto_file").ToString())%>
                            </div>
                            <div id="divResult" runat="server" class="searchresultsingle_right">
                                <div class="searchresultsbody_left">
                                    <p class="eventdate">
                                        <%# SetDateRangeInfo(Eval("event_start_date").ToString(), Eval("event_start_time").ToString(), Eval("event_end_time").ToString())%>
                                    </p>
                                    <p class="eventtitle">
                                        <%# Eval("event_name")%>
                                    </p>
                                    <a href="event-details?id=<%# Eval("order_number") %>" style="text-decoration:none; display:block">
                                        More details!...
                                    </a>
                                </div>
                                <div class="searchresultsbody_right">
                                    <div class="facility">
                                        <%# Eval("sub_facility")%>
                                        <br />
                                        <%# SetMap(Eval("sub_facility_map").ToString())%>
                                    </div>
                                    <div>
                                        <asp:ImageButton ID="ibtnAddToList" runat="server" onclick="ibtnAddToList_Click"
                                            ImageUrl='<%# SetAddToListImage(Eval("order_number").ToString())%>' CommandArgument='<%# Eval("order_number")%>' />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
                    </FooterTemplate>
                </asp:Repeater>
                </div>
            </div>
        </div>
        <uc:Footer ID="ucFooter" runat="server" />
    </div>
    </form>
</body>
</html>
