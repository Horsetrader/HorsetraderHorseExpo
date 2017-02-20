<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="vendor-list.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.vendor_list" %>
<p class="pull-left visible-xs">
    <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
        <span class="glyphicon glyphicon-chevron-left"></span> 
        View exhibitors
    </button>
</p>

<div class="well well-sm">
    <div class="nav-list-icons">
        <asp:ImageButton ID="ibtnPrint" runat="server" onclick="ibtnPrint_Click" ImageURL="~/assets/images/print.jpg" />
        <a class="modalButton visible-xs" href="#" data-toggle="modal" data-target="#emailModal" data-title="Email List to a Friend" data-src="send-email-list-vendor-mobile.aspx">
            <img src="<%= ConfigurationManager.AppSettings["HorseExpoWebsiteURL"] %>/assets/images/emailShowList.png" />
        </a>
        <a class="modalButton visible-sm visible-md visible-lg" href="#" data-toggle="modal" data-target="#emailModal" data-title="Email List to a Friend" data-src="send-email-list-vendor.aspx">
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
        <asp:Repeater ID="repExhibitorList" runat="server" onitemdatabound="repExhibitorList_ItemDataBound">
            <ItemTemplate>
                <li>
                    <a href='<%# SetURLRedirect((bool)Eval("IsAdvertiser"), Eval("ExpoVendorID").ToString()) %>' booth-booster='<%# Eval("IsAdvertiser") %>'>
                        <div class="row">
                            <div class="event-image">
                                <%# SetImage(Eval("ImageFileName").ToString())%>
                            </div>
                            <div class="event-details">
                                <span class="event-title"><%# Eval("Name")%></span>
                                <span class="event-date">
                                    <%# Eval("Location")%>
                                </span>
                                <span class="event-date">
                                    Booth: <%# Eval("Booth") %>
                                </span>
                            </div>
                        </div>
                    </a>
                    <div style="clear:left; text-align: right;">
                        <asp:ImageButton ID="ibtnRemoveFromList" runat="server" CommandArgument='<%# Eval("ExpoVendorID")%>'
                            onclick="ibtnRemoveFromList_Click" ImageUrl="~/assets/images/remove.png">
                        </asp:ImageButton>
                    </div>
                </li>
                <li class="divider"></li>
            </ItemTemplate>
            <FooterTemplate>
                <div class="no-data">
                <asp:Label ID="lblEmptyData" runat="server" Visible="false" style="margin-top:15px;"></asp:Label>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </ul>
</div>