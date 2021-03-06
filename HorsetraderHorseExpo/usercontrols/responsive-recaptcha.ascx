﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="responsive-recaptcha.ascx.cs" Inherits="HorsetraderHorseExpo.usercontrols.responsive_recaptcha" %>
<!-- Start Responsive reCAPTCHA -->

<div id="recaptcha_widget" style="display:none" class="recaptcha_widget">
        <div id="recaptcha_image"></div>
        <div class="recaptcha_only_if_incorrect_sol" style="color:red">Incorrect. Please try again.</div>

        <div class="recaptcha_input">
                <label class="recaptcha_only_if_image" for="recaptcha_response_field">Enter the words above:</label>
                <label class="recaptcha_only_if_audio" for="recaptcha_response_field">Enter the numbers you hear:</label>

                <input type="text" id="recaptcha_response_field" name="recaptcha_response_field">
        </div>

        <ul class="recaptcha_options">
                <li>
                        <a href="javascript:Recaptcha.reload()">
                                <i class="icon-refresh"></i>
                                <span class="captcha_hide">Get another CAPTCHA</span>
                        </a>
                </li>
                <li class="recaptcha_only_if_image">
                        <a href="javascript:Recaptcha.switch_type('audio')">
                                <i class="icon-volume-up"></i><span class="captcha_hide"> Get an audio CAPTCHA</span>
                        </a>
                </li>
                <li class="recaptcha_only_if_audio">
                        <a href="javascript:Recaptcha.switch_type('image')">
                                <i class="icon-picture"></i><span class="captcha_hide"> Get an image CAPTCHA</span>
                        </a>
                </li>
                <li>
                        <a href="javascript:Recaptcha.showhelp()">
                                <i class="icon-question-sign"></i><span class="captcha_hide"> Help</span>
                        </a>
                </li>
        </ul>
</div>

<!-- End Responsive reCAPTCHA-->