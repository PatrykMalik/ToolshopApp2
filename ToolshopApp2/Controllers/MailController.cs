using System;
using System.Net.Mail;
using System.Windows;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public class MailController
    {
        private static void sendmail(string MailTo, string subject, string body)
        {
            if (MailTo != null)
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("10.26.10.85", 25);
                mail.From = new MailAddress("ToolShopApp2@electrolux.com");
                mail.To.Add(MailTo);
                mail.Subject = subject;
                mail.Body = body.Replace("\\n", Environment.NewLine);


                SmtpServer.Credentials = new System.Net.NetworkCredential("ToolShopApp@electrolux.com", "");
                SmtpServer.EnableSsl = false;

                SmtpServer.Send(mail);
            }
        }

        public static void SendConfirmation(Request request)
        {
            User user = UserController.GetUser(request.User);
            if (user != null)
            {
                sendmail(user.Emial,
                     "Task " + request.Id + " has been correcly created", 
                     request.Description);

                sendmail("konrad.szwech@electrolux.com",
                    "User" + request.User + " created task " + request.Id + "", 
                    request.Description);
            }
            else
            {
                MessageBox.Show("Nie można wysłać wiadomości Email-Dodaj adres Email");
            }
        }

        public static void SendNotification(Request request)
        {
            User user = UserController.GetUser(request.User);
            if (user != null)
            {
                sendmail(user.Emial,

                    "Task " + request.Id + " has changed status to: " + request.Status,

                    "Your task " + request.Project + " for " + request.Order + " has changed status to: " + request.Status
                    + Environment.NewLine + Environment.NewLine + request.Description);

                sendmail("konrad.szwech@electrolux.com",

                    "User" + request.User + "  task " + request.Id + "" + " has changed status to: " + request.Status,

                    "Task " + request.Project + "  for " + request.Order + " has changed status to: " + request.Status
                    + Environment.NewLine + Environment.NewLine + request.Description);
            }
            else
            {
                MessageBox.Show("Nie można wysłać wiadomości Email-Dodaj adres Email");
            }
        }

        public static void SendUpdateNotification(Request newRequest, Request oldRequest)
        {
            User user = UserController.GetUser(newRequest.User);
            if (user != null)
            {
                sendmail(user.Emial,
                     "Task " + newRequest.Id + " has been updated",
                     "Updated task: " + Environment.NewLine +
                     "Classyfy: " + newRequest.Classyfy + Environment.NewLine +
                     "Order: " + newRequest.Order + Environment.NewLine +
                     "Project: " + newRequest.Project + Environment.NewLine +
                     "Cost Center: " + newRequest.CostCenter + Environment.NewLine +
                     "Deadline: " + newRequest.Date.ToShortDateString() + Environment.NewLine +
                     "Description: " + newRequest.Description + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                     "Old task: " + Environment.NewLine +
                     "Classyfy: " + oldRequest.Classyfy + Environment.NewLine +
                     "Order: " + oldRequest.Order + Environment.NewLine +
                     "Project: " + oldRequest.Project + Environment.NewLine +
                     "Cost Center: " + oldRequest.CostCenter + Environment.NewLine +
                     "Deadline: " + oldRequest.Date.ToShortDateString() + Environment.NewLine +
                     "Description: " + oldRequest.Description
                     );

                sendmail("konrad.szwech@electrolux.com",
                    "Task " + newRequest.Id + " has been updated",
                     "Updated task: " + Environment.NewLine +
                     "Classyfy: " + newRequest.Classyfy + Environment.NewLine +
                     "Order: " + newRequest.Order + Environment.NewLine +
                     "Project: " + newRequest.Project + Environment.NewLine +
                     "Cost Center: " + newRequest.CostCenter + Environment.NewLine +
                     "Deadline: " + newRequest.Date.ToShortDateString() + Environment.NewLine +
                     "Description: " + newRequest.Description + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                     "Old task: " + Environment.NewLine +
                     "Classyfy: " + oldRequest.Classyfy + Environment.NewLine +
                     "Order: " + oldRequest.Order + Environment.NewLine +
                     "Project: " + oldRequest.Project + Environment.NewLine +
                     "Cost Center: " + oldRequest.CostCenter + Environment.NewLine +
                     "Deadline: " + oldRequest.Date.ToShortDateString() + Environment.NewLine +
                     "Description: " + oldRequest.Description
                     );
            }
            else
            {
                MessageBox.Show("Nie można wysłać wiadomości Email-Dodaj adres Email");
            }
        }
    }
}
