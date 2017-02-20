<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.header" %>
<div id="topnav">
    <div id="topnavbox">
        <div id="topnavleft">
            <a href="<%= ConfigurationManager.AppSettings["HorseExpoHomepage"] %>">HOME</a>&nbsp;&nbsp;|
            <a href="https://docs.google.com/spreadsheet/ccc?key=0AslZcQFESyeGdExtejlWaGZlRDkzVDVOR1U3Q1J1OWc#gid=2">OLD-STYLE CALENDAR</a>&nbsp;&nbsp;|&nbsp;
            <a href="<%= ConfigurationManager.AppSettings["HorseExpoTwitterURL"] %>"><img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/twitterIcon.png" alt="twitter"/></a> 
            <a href="<%= ConfigurationManager.AppSettings["HorseExpoFacebookURL"] %>"><img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/facebook_logo.gif" alt="facebook" /></a>
        </div>
        <div id="topnavright">
            powered by
            <a href="http://www.horsetrader.com">
                <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/horsetrader-logo.jpg" alt="horsetrader.com" />
            </a>
        </div>
    </div>
</div>
