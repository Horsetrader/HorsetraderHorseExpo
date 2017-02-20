<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-grounds-map.aspx.cs" Inherits="HorsetraderHorseExpo.view_grounds_map" %>
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
	
	<!-- Bootstrap core CSS -->
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet">
	
    <!-- Google Analytics -->
    <uc:Analytics ID="ucAnalytics" runat="server" />

	<style>
		#goBack {
			position: absolute;
			top: 5pt;
			left: 5pt;
			opacity: 0.7;
		}
		
		#download-pdf {
			position: absolute;
			top: 30pt;
			left: 5pt;
			opacity: 0.7;
		}
		
		@media (max-width: 400px) {
			.map-image {
				display:none;
			}
			.map-image-90 {
			   width: auto;
			   max-height: 100%;
			   margin: 0 auto;
			   display: block;
			}
		}
		
		@media (min-width: 401px) {
			.map-image-90 {
				display:none;
			}
			.map-image {
			   height: auto;
			   max-width: 100%;
			   margin: 0 auto;
			   display: block;
			}
		}
	</style>
</head>
<body>
	<a id="goBack" runat="server" href="index2.aspx" class="btn btn-xs btn-primary" role="button">
		<span class="glyphicon glyphicon-chevron-left"></span> Go back
	</a>
    <a id="download-pdf" href="assets/pdf/fairplex-grounds-map.pdf" class="btn btn-xs btn-success" role="button">
		<span class="glyphicon glyphicon-download"></span> Download PDF
	</a>
	<img id="verticalMap" runat="server" class="map-image-90" />
	<img id="horizontalMap" runat="server" class="map-image" />
</body>
