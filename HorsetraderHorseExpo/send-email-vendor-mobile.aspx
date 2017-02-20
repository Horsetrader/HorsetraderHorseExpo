<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="send-email-vendor-mobile.aspx.cs" Inherits="HorsetraderHorseExpo.send_email_vendor_mobile" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Register TagPrefix="uc" TagName="ResponsiveRecaptcha" Src="~/usercontrols/responsive-recaptcha.ascx" %>
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

    <style>
        h3 {
            margin-left: 5px;
            margin-right: 5px;
        }
        .alert 
        {
            display: none;
        }
        
        #recaptcha_response_field {
            line-height:normal;
        }
        #emailForm {
            padding: 5px;
        }
        
        .recaptcha_widget{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;max-width:300px;border:4px solid #AF1500;-webkit-border-radius:4px;-moz-border-radius:4px;-ms-border-radius:4px;-o-border-radius:4px;border-radius:4px;background:#AF1500;margin:0 0 10px}
        #recaptcha_image{width:100% !important;height:auto !important}
        #recaptcha_image img{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;width:100%;height:auto;-webkit-border-radius:2px;-moz-border-radius:2px;-ms-border-radius:2px;-o-border-radius:2px;border-radius:2px;border:3px solid #FFF}
        .recaptcha_is_showing_audio embed{height:0;width:0;overflow:hidden}
        .recaptcha_is_showing_audio #recaptcha_image{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;width:100%;height:60px;background:#FFF;-webkit-border-radius:2px;-moz-border-radius:2px;-ms-border-radius:2px;-o-border-radius:2px;border-radius:2px;border:3px solid #FFF}
        .recaptcha_is_showing_audio #recaptcha_image br{display:none}
        .recaptcha_is_showing_audio #recaptcha_image #recaptcha_audio_download{display:block}
        .recaptcha_input{background:#FFDC73;color:#000;font:13px/1.5 "HelveticaNeue","Helvetica Neue",Helvetica,Arial,"Liberation Sans",FreeSans,sans-serif;margin:4px 0 0;padding:0 4px 4px;border:4px solid #FFDC73;-webkit-border-radius:2px;-moz-border-radius:2px;-ms-border-radius:2px;-o-border-radius:2px;border-radius:2px}
        .recaptcha_input label{margin:0 0 6px;-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box}
        .recaptcha_input input{width:100%}
        .recaptcha_options{list-style:none;margin:4px 0 0;height:18px}
        .recaptcha_options li{float:left;margin:0 4px 0 0}
        .recaptcha_options li a{text-decoration:none;text-shadow:0 1px 1px #000;font-size:16px;color:#FFF;display:block;width:20px;height:18px}
        .recaptcha_options li a:active{position:relative;top:1px;text-shadow:none}
        .captcha_hide{display:none}
    </style>

    <!-- Google Analytics -->
    <uc:Analytics ID="ucAnalytics" runat="server" />
</head>
<body>
    <form id="form1" runat="server" role="form">
        <h3>Email </h3>

        <div id="successAlert" runat="server" class="alert alert-success">
            Email sent successfully
        </div>
        <div id="errorAlert" runat="server" class="alert alert-danger">
            There was an error sending the email. Please try again.
        </div>
        <div id="emailForm" runat="server">

            <div class="form-group">
                <label class="control-label" for="fullname">Name:</label>
                <asp:TextBox ID="tbxName" runat="server" class="form-control" 
                    placeholder="Type in your name" name="fullname" required>
                </asp:TextBox>
            </div>
        
            <div class="form-group">
                <label class="control-label" for="email">Email:</label>
                <asp:TextBox ID="tbxEmail" runat="server" class="form-control" 
                    placeholder="Type in your email" type="email" name="email" required>
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label class="control-label" for="phone">Phone:</label>
                    <asp:TextBox ID="tbxPhone" runat="server" class="form-control" placeholder="Type in your phone" name="phone"></asp:TextBox>
            </div>

            <div class="form-group">
                <label class="control-label" for="comments">Questions / Comments:</label>
                <textarea id="tbxComments" class="form-control" runat="server" name="comments" cols="20" rows="3" required></textarea>
            </div>

            <div class="form-group">
                <label class="control-label" for="lastname">Please complete the following form to send your email:</label>
                <uc:ResponsiveRecaptcha ID="ucResponsiveRecaptcha" runat="server" />
                <recaptcha:RecaptchaControl
                    ID="recaptcha"
                    runat="server"
                    PublicKey="6LfSVN4SAAAAACnOXhW_Bp7E6a9KX9hZS6rZJDwG"
                    PrivateKey="6LfSVN4SAAAAAJWc82Bk2GS8BorIlmih5M1GMZmh"
                    Theme="custom"
                    CustomThemeWidget="recaptcha_widget"
                    />
                <p class="help-block">
                    <asp:Label ID="lblCaptchaError" runat="server"
                        Text="Please try typing the words in again..." ForeColor="Red" Visible="false">
                    </asp:Label>
                </p>
            </div>

            <div style="text-align:right;">
                <asp:Button ID="btnCancel" runat="server" class="btn btn-primary cancel" 
                    Text="Cancel" onclick="btnCancel_Click" />

                <asp:Button ID="btnSendEmail" runat="server" class="btn btn-primary" 
                    Text="Submit" onclick="btnSendEmail_Click" />
            </div>
        </div>

        <asp:Button ID="btnGoBack" runat="server" class="btn btn-primary cancel" 
                Text="Go Back" onclick="btnCancel_Click" Visible="false" />

        <asp:Button ID="btnTryAgain" runat="server" class="btn btn-primary cancel" 
                Text="Try Again" onclick="btnTryAgain_Click" Visible="false" />
    
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