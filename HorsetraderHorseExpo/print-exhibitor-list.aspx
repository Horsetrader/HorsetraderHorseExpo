<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-exhibitor-list.aspx.cs" Inherits="HorsetraderHorseExpo.print_exhibitor_list" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!--Page stylesheets, etc-->
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/bootstrap.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/print.css" media="print">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <!-- Google Analytics -->
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="span12" style="text-align:center;">
                <img src="assets/images/horse-expo-print-header.jpg" alt="Western States Horse Expo Event List" />
            </div>
            <div class="span12">
                <asp:Repeater ID="repExhibitorList" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="span3"></div>
                            <div class="span6">
                                <h3>
                                    <%# Eval("Name") %>
                                </h3>
                                <h4>
                                    <%# Eval("Location") %> - Booth <%# Eval("Booth")%>
                                </h4>
                                <hr />
                            </div>
                            <div class="span3"></div>
                        </div>        
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="span12" style="text-align:center; margin-top:20px;">
                <img src="assets/images/horse-expo-print-footer.jpg" alt="Western States Horse Expo Event List" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
