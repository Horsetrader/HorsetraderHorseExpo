<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-list-sample-1.aspx.cs" Inherits="HorsetraderHorseExpo.print_list_sample_1" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <!--Page stylesheets, etc-->
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/bootstrap.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/print.css" media="screen">
    <link rel="stylesheet" type="text/css" href="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/stylesheets/print.css" media="print">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <!--Page script files-->
    <script type="text/javascript" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/scripts/jQuery-1.8.2.js"></script>

    <script>
        $(document).ready(function () {
            $('#ddlView').change(function () {
                if ($('#ddlView').val() == 'Compact') {
                    $('.event-image-print').hide();
                    $('.event-description-print').attr('class', 'span10 event-description-print')
                }
                else {
                    $('.event-description-print').attr('class', 'span8 event-description-print')
                    $('.event-image-print').show();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div id="printing-properties" class="span12" style="margin-bottom: 15px; background-color: whiteSmoke; padding: 10px 20px; border-bottom: 1px solid #CCCCCC;">
                <div class="row">
                    <div class="span9">
                        <span style="font-size: 17px; font-weight: bold;">Print Options:</span>
                    </div>
                    <div class="span3" style="text-align: right;">
                        <a href='<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>'
                            class="sexybutton sexysimple sexyred">
                            Cancel
                        </a>
                        <a href="javascript:window.print()"
                            class="sexybutton sexysimple sexygreen">
                            Print List
                        </a>
                    </div>
                </div>
                <div style="border-bottom: 1px solid #CCCCCC; margin: 10px 0;"></div>
                <div class="row">
                    <div class="span4">
                        <span>View:</span>
                        <asp:DropDownList ID="ddlView" runat="server">
                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                            <asp:ListItem Text="Compact" Value="Compact"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="span4">
                        <span>Sort by:</span>
                        <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddlSort_SelectedIndexChanged">
                            <asp:ListItem Value="event_start_date ASC" Selected="True">
                                Day
                            </asp:ListItem>
                            <asp:ListItem Value="nullSubFacilitySort ASC, sub_facility ASC,
                                event_start_date ASC">
                                Arena or location
                            </asp:ListItem>
                            <asp:ListItem Value="event_name ASC, event_start_date ASC">
                                Clinician or event name
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="span12" style="text-align:center;">
                <img src="assets/images/horse-expo-print-header.jpg" alt="Horse Expo Event List" />
            </div>
            <div class="span12">
                <asp:Repeater ID="repEventList" runat="server" 
                    onitemdatabound="repEventList_ItemDataBound">
                    <ItemTemplate>
                        <div class="row" style="margin-top: 20px;">
                            <div class="span12">
                                <span style="font-size:20px; font-weight: bold">
                                    <%# FormatDate(Eval("event_start_date").ToString())%> - 
                                    <%# SetEventTimeInfo(Eval("event_start_time").ToString(), Eval("event_end_time").ToString())%>
                                </span>
                                <div style="border-bottom: 1px solid #CCCCCC; margin: 10px 0;"></div>
                            </div>
                            <div id="divImage" runat="server" class="span2 event-image-print">
                                <%# SetImage(Eval("foto_file").ToString())%>
                            </div>
                            <div id="divDescription" runat="server" class="span8 event-description-print">
                                <span style="font-size:18px; font-weight: bold;">
                                    <%# Eval("event_name")%>
                                </span>
                                <p>
                                    <%# Eval("description")%>
                                </p>
                                <p>
                                    <%# SetBoothNumberInfo(Eval("booth_number").ToString())%>
                                </p>
                            </div>
                            <div class="span2">
                                <span style="font-size: 12px;">
                                    <%# Eval("sub_facility")%>
                                </span>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="span12" style="text-align:center; margin-top:20px;">
                <img src="assets/images/horse-expo-print-footer.jpg" alt="Horse Expo Event List" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
