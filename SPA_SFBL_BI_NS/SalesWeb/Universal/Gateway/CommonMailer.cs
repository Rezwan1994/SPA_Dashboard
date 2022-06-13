using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace SalesWeb.Universal.Gateway
{
    public class CommonMailer
    {
        static void Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639

            ServicePointManager.ServerCertificateValidationCallback =
                (s, certificate, chain, sslPolicyErrors) => true;
            //ServicePointManager.ServerCertificateValidationCallback =
                //delegate (
                //    object s,
                //    X509Certificate certificate,
                //    X509Chain chain,
                //    SslPolicyErrors sslPolicyErrors
                //) {
                //    return true;
                //};
        }

        public void SendMail(string mailTo, string subject, string body)
        {
            //SmtpClient smtpServer = new SmtpClient("172.16.128.39")
            SmtpClient smtpServer = new SmtpClient("172.16.128.41")
            {
                Port = 25,
                //Port = 587,
                Credentials = new NetworkCredential("maaz@squaregroup.com", "MaaZ737$"),
                EnableSsl = true
            };

            char[] splitter = { ';' };
            var mailList = mailTo.Split(splitter);

            for (int i = 0; i <= mailList.Length - 1; i++)
            {
                Disable_CertificateValidation();
                smtpServer.Send("maaz@squaregroup.com", mailList[i], subject, body);
            }
        }



    }
}