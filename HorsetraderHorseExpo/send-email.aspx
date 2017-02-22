<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="send-email.aspx.cs" Inherits="HorsetraderHorseExpo.send_email" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <link rel="stylesheet" href="assets/stylesheets/aspstyle.css" type="text/css" media="screen" />
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>
    <title>Email Event List to Friend</title>

    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="emailcontainer" runat="server">
		<h1>
			<img src="assets/images/email.png" style="vertical-align: -3px; margin-right: 3px;">
			Send your event list to a friend via email:
		</h1>
		<div id="emaildescription">
			Simply fill out the form below to send your friend your list
        </div>
		<div id="emailcontainerbody">
			<div id="emailcontainerleft">
				Your Friend's Email:
			</div>
			<div id="emailcontainerright">
                <asp:TextBox ID="tbxFriendEmail" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfvFriendEmail" runat="server"
                    ControlToValidate="tbxFriendEmail" Text="*" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revFriendEmail" runat="server"
                    ControlToValidate="tbxFriendEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Text="Please verify this email" Display="Dynamic">
                </asp:RegularExpressionValidator>
			</div>
		</div>
		<div id="emailcontainerbody">
			<div id="emailcontainerleft">
				Your Name:
			</div>
			<div id="emailcontainerright">
                <asp:TextBox ID="tbxName" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfvName" runat="server"
                    ControlToValidate="tbxName" Text="*" Display="Dynamic">
                </asp:RequiredFieldValidator>
			</div>
		</div>
		<div id="emailcontainerbody">
			<div id="emailcontainerleft">
				Your Email:
			</div>
			<div id="emailcontainerright">
                <asp:TextBox ID="tbxEmail" runat="server"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="tbxEmail" Text="*" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                    ControlToValidate="tbxEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Text="Please verify this email" Display="Dynamic">
                </asp:RegularExpressionValidator>
			</div>
		</div>
		<div id="emailcontainerbody">
			<div id="emailcontainerleft">
				Note:
            </div>
			<div id="emailcontainerright">
				<textarea id="tbxComments" runat="server" name="comments" cols="20" rows="5"></textarea>
			</div>
		</div>
		<div id="emailcontainerbody">
			<div id="emailcontainerleft">
				Please complete the form (at right) to verify your email. Thank you!
				<br />
                <asp:Label ID="lblCaptchaError" runat="server"
                    Text="Please try typing the words in again..." ForeColor="Red" Visible="false">
                </asp:Label>
			</div>
			<div id="emailcontainerright">
			    <recaptcha:RecaptchaControl
                    ID="recaptcha"
                    runat="server"
                    PublicKey="6LfSVN4SAAAAACnOXhW_Bp7E6a9KX9hZS6rZJDwG"
                    PrivateKey="6LfSVN4SAAAAAJWc82Bk2GS8BorIlmih5M1GMZmh"
                    />
			</div>
		</div>
		<div id="emailcontainerbody">
			<div id="emailcontainerleft">
			</div>
			<div id="emailcontainerright">
				<input type="hidden" value="873807" name="ordernumber">
				<input type="hidden" value="" name="PubNumber">
				<asp:Button ID="btnSendEmail" runat="server" Text="Send" onclick="btnSendEmail_Click" />
			</div>
		</div>
	</div>
    <div id="confirmationmessage" style="display:none" runat="server">
        <h1>
            <img src="assets/images/accept.png" style="vertical-align: -3px; margin-right: 3px;">
            Your event list has been emailed to your friend!
        </h1>
        <p>        
            Click on the "X" in the bottom right of this window to close it.
        </p>
    </div>
    </form>
</body>
</html>
