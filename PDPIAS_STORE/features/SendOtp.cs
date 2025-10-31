using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace PDPIAS_management.Features
{
    public class SendOtp
    {
        private static readonly string FromEmail = "theofficialcinevision@gmail.com"; // your email
        private static readonly string FromPassword = "vmfsemrezwvczcdj";  // your app password (or SMTP password)
        private static readonly string SmtpHost = "smtp.gmail.com";
        private static readonly int SmtpPort = 587;

        public static string GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);  // 6-digit OTP
            return otp.ToString();
        }

        public static void SendOtpEmail(string toEmail, string otp)
        {
            var fromAddress = new MailAddress(FromEmail, "PDPIAS Store");
            var toAddress = new MailAddress(toEmail);
            string subject = "Your OTP Code";
            string body = $"Your OTP is: {otp}";

            var smtp = new SmtpClient
            {
                Host = SmtpHost,
                Port = SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, FromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}