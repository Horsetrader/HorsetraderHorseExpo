<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="send-email-friend.aspx.cs" Inherits="HorsetraderHorseExpo.send_email_friend" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Register TagPrefix="uc" TagName="Analytics" Src="~/usercontrols/google-analytics.ascx" %>

<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="#">
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>

    <title>Western States Horse Expo: Send Email</title>

    <!-- Bootstrap core CSS -->
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet">

    <!-- Google Analytics -->
    <uc:Analytics ID="ucAnalytics" runat="server" />

    <style>
        .alert 
        {
            display: none;
        }
        
        #recaptcha_response_field {
            line-height:normal;
        }
        #emailForm {
            padding: 19px;
            text-align: right;
        }
        
        @media (max-width:320px) {
            #recaptcha_widget_div {
                margin-left: -19px;
            }
            .well, .alert {
                margin-left: 2px;
                margin-right: 2px;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" class="form-horizontal" role="form">

        <div id="successAlert" runat="server" class="alert alert-success">
            Email sent successfully
        </div>
        <div id="errorAlert" runat="server" class="alert alert-danger">
            There was an error sending the email. Please try again.
        </div>
        <div id="emailForm" runat="server">
            
            <div class="form-group">
                <label class="col-xs-3 control-label" for="friendemail">Your Friend's Email:</label>
                <div class="col-xs-7">
                    <asp:TextBox ID="tbxFriendEmail" runat="server" class="form-control" 
                        placeholder="Type in your friend's email" name="friendemail" type="email" required>
                    </asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-3 control-label" for="fullname">Your Name:</label>
                <div class="col-xs-7">
                    <asp:TextBox ID="tbxName" runat="server" class="form-control" 
                        placeholder="Type in your name" name="fullname" required>
                    </asp:TextBox>
                </div>
            </div>
        
            <div class="form-group">
                <label class="col-xs-3 control-label" for="email">Your Email:</label>
                <div class="col-xs-7">
                    <asp:TextBox ID="tbxEmail" runat="server" class="form-control" 
                        placeholder="Type in your email" type="email" name="email" required>
                    </asp:TextBox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-3 control-label" for="comments">Note:</label>
                <div class="col-xs-7">
                    <textarea id="tbxNote" class="form-control" runat="server" name="comments" cols="20" rows="3"></textarea>
                </div>
            </div>

            <div class="form-group">
                <label class="col-xs-3 control-label" for="lastname">Please complete the following form to send your email:</label>
                <div class="col-xs-7">
                    <recaptcha:RecaptchaControl
                        ID="recaptcha"
                        runat="server"
                        PublicKey="6LfSVN4SAAAAACnOXhW_Bp7E6a9KX9hZS6rZJDwG"
                        PrivateKey="6LfSVN4SAAAAAJWc82Bk2GS8BorIlmih5M1GMZmh"
                        />
                    <p class="help-block">
                        <asp:Label ID="lblCaptchaError" runat="server"
                            Text="Please try typing the words in again..." ForeColor="Red" Visible="false">
                        </asp:Label>
                    </p>
                </div>
            </div>

            <asp:Button ID="btnSendEmail" runat="server" class="btn btn-primary" 
                Text="Submit" onclick="btnSendEmail_Click" />

        </div>
    
        <!-- Bootstrap core JavaScript
        ================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->
        <script src="./assets/scripts/jquery-1.10.2.min.js"></script>
        <script src="./assets/scripts/jquery.validate.min.js"></script>
        <script>
            $('form').validate({
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-error');
                },
                errorElement: 'span',
                errorClass: 'help-block',
                errorPlacement: function (error, element) {
                    if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    } else {
                        error.insertAfter(element);
                    }
                }
            });
        </script>
    </form>
</body>
</html>