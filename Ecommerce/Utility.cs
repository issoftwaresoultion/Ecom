using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce
{
    public static class Utility
    {
        public static decimal GetConvertedPrice(decimal amount, string fromCurrency, string toCurrency)
        {
            toCurrency = "USD";
            if (fromCurrency == "Naira")
            {
              fromCurrency  ="NGN";
            }
            else if (fromCurrency == "Kwanza")
            {
                fromCurrency="AOA";
            }
            else if (fromCurrency == "Cedi")
            {
                fromCurrency = "GHS";
            }
            WebClient web = new WebClient();
            string url = string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");
            decimal rate = System.Convert.ToDecimal(values[1]);
            return rate * amount;
        }
        public static void SendConfermationEmail(string ToEmail, String EmailMessage, String subject, string name)
        {
            MailMessage m = new MailMessage("ravikumar318@gmail.com", ToEmail);
            m.Subject = subject;
            m.Body = EmailMessage;
            m.IsBodyHtml = true;
            m.From = new MailAddress("ravikumar318@gmail.com");

            m.To.Add(new MailAddress(ToEmail));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            NetworkCredential authinfo = new NetworkCredential("ravikumar318@gmail.com", "s_022113");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = authinfo;
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
        public static void SendEmail(string ToEmail, String EmailMessage, String subject, string name)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("adam_022113@yahoo.com");
                mail.To.Add(ToEmail);
                mail.Subject = subject;
                mail.Body = EmailMessage;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.mail.yahoo.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("adam_022113@yahoo.com", "Seesharp#4.0");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

            //MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Ecommerce");//website name
            //MailAddress toAddress = new MailAddress(ToEmail, name);
            //string fromPassword = ConfigurationManager.AppSettings["Password"].ToString();
            //string body = "Test";
            //var smtp = new SmtpClient
            //{
            //    Host = ConfigurationManager.AppSettings["Host"],
            //    Port = 8889,
            //    EnableSsl = false,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
            //    Timeout = 20000
            //};
            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = body,
            //    IsBodyHtml = true

            //})
            //{
            //    smtp.Send(message);
            //}
        }

        public static string GetResponse(string url)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
