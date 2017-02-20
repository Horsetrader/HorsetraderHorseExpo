<%
dim mediaUrl, photosUrl,photosMain, couponPdf
'DEVELOPMENT SETTINGS
'mediaUrl = "http://206.251.236.13/fotoshow/view.aspx?order="
'photosUrl = "http://localhost/beta/foto/"
'photosMain = "http://206.251.236.13/documentManager/"
'couponPdf = "http://localhost/beta/media/1102A_SSavers_PrintCoupons.pdf"

'PRODUCTION SETTINGS
mediaUrl = "http://98.158.164.153/fotoshow/view.aspx?order="
photosUrl = "http://horsetrader.com/foto/"
photosMain = "http://98.158.164.153/documentManager/"
couponPdf = "http://horsetrader.com/media/store_saver_coupons.pdf"

function getImageServerUrl()
    getImageServerUrl = photosUrl
end function

function getConnectionString()
    'DEVELOPMENT
    'getConnectionString = "Provider=SQLOLEDB;User ID=ht_user;password=123abc;Initial Catalog=Horsetrader_Advantage;Network=DBMSSOCN;Data Source=DSILuis\DsiSQL"
    'PRODUCTION
	getConnectionString = "Provider=SQLOLEDB;User ID=cht_search;password=CHTsearch2011;Initial Catalog=AdvantageProd;Network=DBMSSOCN;Data Source=192.168.4.10"
end function

function getCouponPdfUrl()

    getCouponPdfUrl = "http://horsetrader.com/media/store_saver_coupons.pdf"

end function

function showDebugInformation()
    showDebugInformation = false
end function
%>