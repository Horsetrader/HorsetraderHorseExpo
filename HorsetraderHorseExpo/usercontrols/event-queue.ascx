<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="event-queue.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.event_queue" %>
<p class="pull-left visible-xs">
    <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
        <span class="glyphicon glyphicon-chevron-left"></span> 
        View schedule
    </button>
</p>

<div class="well well-sm">
    <div class="nav-list-icons">
        <asp:ImageButton ID="ibtnPrint" runat="server" onclick="ibtnPrint_Click" ImageURL="~/assets/images/print.jpg" />
        <a class="modalButton visible-xs" href="#" data-toggle="modal" data-target="#emailModal" data-title="Email List to a Friend" data-src="send-email-list-mobile.aspx">
            <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/emailShowList.png" />
        </a>
        <a class="modalButton visible-sm visible-md visible-lg" href="#" data-toggle="modal" data-target="#emailModal" data-title="Email List to a Friend" data-src="send-email-list.aspx">
            <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/emailShowList.png" />
        </a>
    </div>
    
    <ul id="event-list" class="nav nav-list">
        <li class="divider"></li>
        <li class="nav-header">
            <div class="dont-miss-list-header">
                <img id="dont-miss-list-title" src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/dont-miss-list.jpg" />
            </div>
        </li>
        <asp:Repeater ID="repEventList" runat="server" onitemdatabound="repEventList_ItemDataBound">
            <ItemTemplate>
                <li>
                    <a href='event-details?id=<%# Eval("order_number")%>'>
                        <div class="row">
                            <div class="event-image">
                                <%# SetImage(Eval("foto_file").ToString())%>
                            </div>
                            <div class="event-details">
                                <span class="event-title"><%# TrimEventName(Eval("event_name").ToString())%></span>
                                <span class="event-date">
                                    <%# SetDateRangeInfo(Eval("event_start_date").ToString())%>
                                </span>
                                <span class="event-date">
                                    <%# SetTimeRangeInfo(Eval("event_start_time").ToString(),
                                            Eval("event_end_time").ToString())%>
                                </span>
                                <span><%# Eval("sub_facility")%></span>
                            </div>
                        </div>
                    </a>
                    <div style="clear:left; text-align: right;">
                        <asp:ImageButton ID="ibtnRemoveFromList" runat="server" CommandArgument='<%# Eval("order_number")%>'
                            onclick="ibtnRemoveFromList_Click" ImageUrl="~/assets/images/remove.png">
                        </asp:ImageButton> 
                    </div>
                </li>
                <li class="divider"></li>
            </ItemTemplate>
            <FooterTemplate>
                <asp:Label ID="lblEmptyData" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
            </FooterTemplate>
        </asp:Repeater>
    </ul>
</div>