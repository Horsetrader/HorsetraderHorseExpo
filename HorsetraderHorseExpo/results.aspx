<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="results.aspx.cs" Inherits="HorsetraderHorseExpo.results" EnableEventValidation="false"%>
<%@ Register TagPrefix="uc" TagName="Header" Src="~/usercontrols/header.ascx" %>
<%@ Register TagPrefix="uc" TagName="Search" Src="~/usercontrols/search.ascx" %>
<%@ Register TagPrefix="uc" TagName="EventQueue" Src="~/usercontrols/event-queue.ascx" %>
<%@ Register TagPrefix="uc" TagName="Footer" Src="~/usercontrols/footer.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title>Western States Horse Expo: Schedule</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!--Page stylesheets, etc-->
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/bootstrap.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/<%= ConfigurationManager.AppSettings["HorseExpoPubNumber"] %>.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/colorbox.css" media="screen">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <!--Page script files-->
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jQuery-1.8.2.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jquery.colorbox.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jquery.watermark.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/general.js"></script>
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/adresults.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc:Header ID="ucHeader" runat="server" />
    <uc:Search ID="ucSearch" runat="server" />
    <div class="container">
        <div class="row">
            <div id="breadcrumbs" class="span12">
                <div id="breadcrumbsleft" class="leftside">
                    <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/house.png" style="vertical-align: -3px; margin-right: 3px;">
                    <a href="<%= ConfigurationManager.AppSettings["HorseExpoHomepage"] %>">Home</a> »
                    <a href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>">Schedule</a> » Search Results
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
                        <div class="titleSection">
                            <div class="redTitleSection">
                                <asp:Label runat="server" CssClass="redTitle" ID="lblDisplayingFor" Text="Results"></asp:Label>
                                <asp:Label runat="server" class="searcresulttitle" ID="lblTotalRecords"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="filters span9">
                        <span>Sort by:</span>
                        <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
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
                <div id="searchResults">
                    <asp:Repeater runat="server" ID="repAds" onitemdatabound="repAds_ItemDataBound">
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
                                            <%# SetMap(Eval("sub_facility_map").ToString())%>
                                        </div>
                                        <asp:ImageButton ID="ibtnAddToList" runat="server" onclick="ibtnAddToList_Click" CssClass="add-to-list"
                                            ImageUrl='<%# SetAddToListImage(Eval("order_number").ToString())%>' CommandArgument='<%# Eval("order_number")%>' />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblEmptyData" runat="server" Visible="false"></asp:Label>
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