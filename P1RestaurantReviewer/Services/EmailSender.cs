using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using P1RestaurantReviewer.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Services
{
    /// <summary>
    /// This handles the email confirmation when a user registers and needs a new email confirmation
    /// </summary>
    public class EmailSender : IEmailSender

    {

        private readonly GoogleSMTPDetails _googleSMTPDetails;
        // I’ve injected GoogleSMTPDetails into the constructor

        public EmailSender(IOptions<GoogleSMTPDetails> googleSMTPDetails)
        {
            // We want to know if googleSMTPDetails is null so we throw an exception if it is           
            _googleSMTPDetails = googleSMTPDetails.Value ?? throw new ArgumentException(nameof(googleSMTPDetails));
        }
        /// <summary>
        /// Collects user info and sends them the confirmation email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="htmlMessage"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(email, email));
                message.From = new MailAddress(_googleSMTPDetails.email, _googleSMTPDetails.email);
                message.Subject = subject;
                message.Body = htmlMessage;
                message.IsBodyHtml = true;

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_googleSMTPDetails.email, _googleSMTPDetails.password);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
            return Task.CompletedTask;

            throw new NotImplementedException();

       



        }
        
    }
}
