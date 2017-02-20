<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="google-ads-code.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.google_ads_code" %>
<!--Google Ads-->

<%--Western States Horse Expo--%>

<script async src="//www.googletagservices.com/tag/js/gpt.js"> </script>
<script>
    googletag = window.googletag || { cmd: [] };
    googletag.cmd.push(function () {
        var mapping = googletag.sizeMapping().
    addSize([992, 200], [575, 70]). // Medium and large size devices 992px and up
    addSize([600, 200], [575, 70]). // Small size devices 768px - 991px
    addSize([0, 0], [[320, 70], [300, 70]]). // Devices less than 768px - accepts both common mobile banner formats
    build();
        googletag.defineSlot('/1330450/Horsexpo_AllPages_Top_575x70', [575, 70], 'action-item').
    defineSizeMapping(mapping).
    addService(googletag.pubads());
        googletag.enableServices();
    });
</script>

<%--Horse Expo Pomona--%>
<%--<script async src="//www.googletagservices.com/tag/js/gpt.js"> </script>
<script>
    googletag = window.googletag || { cmd: [] };
    googletag.cmd.push(function () {
        var mapping = googletag.sizeMapping().
    addSize([992, 200], [575, 70]). // Medium and large size devices 992px and up
    addSize([600, 200], [575, 70]). // Small size devices 768px - 991px
    addSize([0, 0], [[320, 70], [300, 70]]). // Devices less than 768px - accepts both common mobile banner formats
    build();
        googletag.defineSlot('/1330450/HorseExpoEvents_Responsive', [575, 70], 'action-item').
    defineSizeMapping(mapping).
    addService(googletag.pubads());
        googletag.enableServices();
    });
</script>--%>
