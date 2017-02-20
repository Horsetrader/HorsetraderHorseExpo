<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="search.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.search" %>
<div id="headerContainer">
    <div id="header">
        <div class="searchBoxContainer">
            <div style="float:right;">
                <asp:Button runat="server" ID="btnSearch" CssClass="searchButton" Text="Search" 
                    style="width:80px;" onclick="btnSearch_Click" />
                <div class="searchInnerContainer">
                    <asp:TextBox runat="server" ID="tbxSearch" CssClass="searchBox watermark" style="border:0; -webkit-box-shadow: none; box-shadow: none; font-size:12px;"></asp:TextBox>
                </div>
            </div>
            <div style="clear:both;"></div>
        </div>
    </div>
</div>