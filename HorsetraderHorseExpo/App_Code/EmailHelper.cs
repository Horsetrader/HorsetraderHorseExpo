using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using System.Data;

public class EmailHelper
{

    public static bool Send(string FromAddress, string ToAddress, string BccAddress, string Subject, string Body)
    {

        bool result = false;
        SmtpClient client = new SmtpClient();
        client.Host = ConfigurationManager.AppSettings["SMTPHost"];
        client.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage message = new MailMessage(FromAddress, ToAddress, Subject, Body);
        if (BccAddress != "")
        {
            MailAddress mailBccAddress = new MailAddress(BccAddress);
            message.Bcc.Add(mailBccAddress);
        }
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.IsBodyHtml = true;

        //Send Message
        try
        {
            client.Send(message);
            result = true;
        }
        catch (Exception ex)
        {
            result = false;

        }
        return result;
    }

    #region Content Building
    public static string GetEventListContent(DataSet dsEventList)
    {
        StringBuilder emailBodyText = new StringBuilder();

        emailBodyText.Append("<!DOCTYPE html>");
        emailBodyText.Append("<html lang='en'>");
        emailBodyText.Append("<head></head>");
        emailBodyText.Append("<body>");
        emailBodyText.Append("<table cellpadding='0' cellspacing='0' border='0' style='width:940px;font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #333333;'>");
        emailBodyText.Append("<tr>");
        emailBodyText.Append("<td colspan='2' style='text-align:center;'><img src='http://horsexpo.horsetrader.com/assets/images/horse-expo-print-header.jpg' alt='Horse Expo Event List'/></td>");
        emailBodyText.Append("</tr>");
        
        //loop starts here
        foreach (DataRow dr in dsEventList.Tables[0].Rows)
        {
            emailBodyText.Append("<tr>");
            emailBodyText.Append("<td colspan='2' style='font-size:24.5px;line-height:20px;font-weight:bold; border-bottom:1px solid #eeeeee; padding-top:20px; padding-bottom:25px;'>");
            emailBodyText.Append(FormatDate(dr["event_start_date"].ToString()));
            emailBodyText.Append("</td>");
            emailBodyText.Append("</tr>");
            emailBodyText.Append("<tr>");
            emailBodyText.Append("<td colspan='2' style='font-size:17.5px;line-height:20px;font-weight:bold; padding-top:20px;'>");
            emailBodyText.Append(SetEventTimeInfo(dr["event_start_time"].ToString(), dr["event_end_time"].ToString()));
            emailBodyText.Append("</td>");
            emailBodyText.Append("</tr>");
            emailBodyText.Append("<tr>");
            emailBodyText.Append("<td colspan='2' style='font-size:14px;line-height:20px;font-weight:bold; padding-top:10px; padding-bottom:10px;'>");
            emailBodyText.Append(dr["sub_facility"].ToString());
            emailBodyText.Append("</td>");
            emailBodyText.Append("</tr>");
            emailBodyText.Append("<tr>");
            emailBodyText.Append("<td style='width:140px;'>");
            emailBodyText.Append(SetImage(dr["foto_file"].ToString()));
            emailBodyText.Append("</td>");
            emailBodyText.Append("<td style='vertical-align:top; padding-left: 15px;'>");
            emailBodyText.Append("<span style='font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;font-size: 14px;line-height: 20px;color: #333333;'>" + dr["event_name"].ToString() + "</span>");
            emailBodyText.Append("<p>" + dr["description"].ToString() + "</p>");
            emailBodyText.Append("</td>");
            emailBodyText.Append("</tr>");
            
        }
        emailBodyText.Append("<tr>");
        emailBodyText.Append("<td colspan='2' style='text-align:center;'><img src='http://horsexpo.horsetrader.com/assets/images/horse-expo-print-footer.jpg' alt='Horse Expo Event List'/></td>");
        emailBodyText.Append("</tr>");
        emailBodyText.Append("</table>");

        return emailBodyText.ToString();
    }

    public static string GetVendorListContent(DataSet dsVendorList)
    {
        StringBuilder emailBodyText = new StringBuilder();

        emailBodyText.Append("<!DOCTYPE html>");
        emailBodyText.Append("<html lang='en'>");
        emailBodyText.Append("<head></head>");
        emailBodyText.Append("<body>");
        emailBodyText.Append("<table cellpadding='0' cellspacing='0' border='0' style='width:460px;font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #333333;'>");
        emailBodyText.Append("<tr>");
        emailBodyText.Append("<td colspan='2' style='text-align:center;padding-bottom:20px;'><img src='http://horsexpo.horsetrader.com/assets/images/horse-expo-print-header.jpg' alt='Horse Expo Event List'/></td>");
        emailBodyText.Append("</tr>");

        //loop starts here
        foreach (DataRow dr in dsVendorList.Tables[0].Rows)
        {
            emailBodyText.Append("<tr>");
            emailBodyText.Append("<td colspan='2' style='font-size:24.5px;line-height:20px;font-weight:bold;padding-bottom:5px'>");
            emailBodyText.Append(dr["Name"].ToString());
            emailBodyText.Append("</td>");
            emailBodyText.Append("</tr>");
            emailBodyText.Append("<tr>");
            emailBodyText.Append("<td colspan='2' style='font-size:17.5px;line-height:20px;font-weight:bold;border-bottom:1px solid #eeeeee;padding-bottom:25px;'>");
            emailBodyText.Append(dr["Location"].ToString() + " - Booth " + dr["Booth"].ToString());
            emailBodyText.Append("</td>");
            emailBodyText.Append("</tr>");

        }
        emailBodyText.Append("<tr>");
        emailBodyText.Append("<td colspan='2' style='text-align:center;padding-top:20px;'><img src='http://horsexpo.horsetrader.com/assets/images/horse-expo-print-footer.jpg' alt='Horse Expo Event List'/></td>");
        emailBodyText.Append("</tr>");
        emailBodyText.Append("</table>");

        return emailBodyText.ToString();
    }
    #endregion

    #region Formatting
    private static string FormatDate(string startDate)
    {
        DateTime date;
        DateTime.TryParse(startDate, out date);

        startDate = date.ToString("ddddd MMMM d");

        return startDate;
    }

    private static string SetImage(string photoFile)
    {
        string imageHTML = string.Empty;

        if (!string.IsNullOrEmpty(photoFile))
        {
            string imageURL = string.Format("{0}/{1}",
                                    ConfigurationManager.AppSettings["ImagesURL"],
                                    photoFile);
            imageHTML = string.Format("<img style='width:121px;' src='{0}' />", imageURL);
        }

        return imageHTML;
    }

    private static string SetEventTimeInfo(string startTime, string endTime)
    {
        string timeInfo = string.Empty;

        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            timeInfo += string.Format("{0} - {1}", startTime, endTime);
        else if (!string.IsNullOrEmpty(startTime))
            timeInfo += string.Format("{0}", startTime);

        return timeInfo;
    }

    private static string SetBoothNumberInfo(string boothNumber)
    {
        if (!string.IsNullOrEmpty(boothNumber))
        {
            boothNumber = string.Format("Also, see their booth, #{0}!", boothNumber);
        }

        return boothNumber;
    }
    #endregion
}