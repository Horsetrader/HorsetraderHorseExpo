<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="banner-ads.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.banner_ads" %>
<%--<div id="headerspace">
    <div id="headerspaceleft">
        <script type='text/javascript'>
            GA_googleFillSlot("Horsexpo_AllPages_Top_575x70");
        </script>
    </div>
    <div style="float:left; background-color:white; width:170px; height:70px; margin-left:17px;">
        <a href="https://docs.google.com/spreadsheet/ccc?key=0AslZcQFESyeGdExtejlWaGZlRDkzVDVOR1U3Q1J1OWc#gid=2">
            <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/expo-calendar.jpg" />
        </a>
    </div>
    <div id="headerspaceright" style="background-color:white;">
        <a href="http://horsetrader.com/media/expo_coupons.pdf">
            <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/expo-coupons.jpg" />
        </a>
    </div>
</div>--%>


<div class="row" style="margin-bottom: 10px;">
    <div class="col-xs-12 col-sm-12 col-md-7">
        <div id="action-item">
            <script>
                googletag.cmd.push(function () {
                    googletag.display('action-item')
                });
            </script>
        </div>
    </div>
    <div class="visible-md visible-lg col-md-5">
        <div id="action-item-right" style="width:205px;">
            <span>Powered by:</span>
            <a href="http://horsetrader.com">
                <img src="../assets/images/horsetrader-logo.jpg">
            </a>
        </div>
        <%--<div id="action-item-left">
            <a href="https://docs.google.com/spreadsheet/ccc?key=0AslZcQFESyeGdHlTTUtSUU1GaC1VUGRFQXFETEZXb1E">
                <img src="../assets/images/expo-calendar.jpg" />
            </a>
        </div>--%>
        
    </div>
</div>