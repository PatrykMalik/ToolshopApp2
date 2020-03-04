using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolshopApp2.View;
using ToolshopApp2.Model;
using ToolshopApp2.Data;

namespace ToolshopApp2.Controllers
{
    public static class TaskWindowController
    {
        public static void AddRequest()
        {
            var context = new DatabaseConnectionContext();

            var request = new Request()
            {
                User = Environment.UserName,
                Classyfy = TaskWindow.task._SimpleTaskUserControl._ComboBoxClassyfy.Text,
                Project = TaskWindow.task._SimpleTaskUserControl._ComboBoxProject.Text,
                Order = TaskWindow.task._SimpleTaskUserControl._ComboboxTask.Text,
                Date = TaskWindow.task._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value,
                Description = TaskWindow.task._SimpleTaskUserControl._TextBoxDescription.Text,
                CostCenter = TaskWindow.task._SimpleTaskUserControl._ComboBoxCostCenter.Text,
                RequestStatusesId = 1,
                Attachment = TaskWindow.task._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value,                
            };
            if ((request.Order == "Shipping Dishwasher" || request.Order == "Shipping Components") && TaskWindow.task._ShipmentTaskUserControl.IsInitialized)
            {
                request.ContactPerson = TaskWindow.task._ShipmentTaskUserControl._ComboBoxContactPerson.Text;
                request.Email = TaskWindow.task._ShipmentTaskUserControl._TextBoxEmail.Text;
                request.Address = TaskWindow.task._ShipmentTaskUserControl._TextBoxAdress.Text;
                request.BeginigSrz = TaskWindow.task._ShipmentTaskUserControl._TextBoxSrzBegin.Text;
                request.EndingSrz = TaskWindow.task._ShipmentTaskUserControl._TextBoxSrzEnd.Text;
                request.Transpot = TaskWindow.task._ShipmentTaskUserControl._ComboBoxTransport.Text;
                request.Insurance = TaskWindow.task._ShipmentTaskUserControl._ComboBoxInsurance.IsChecked.Value;
                request.InsuranceCost = TaskWindow.task._ShipmentTaskUserControl._TextBoxInsuranceCost.Text;
            }
            context.Add(request);
            context.SaveChanges();
        }

        public static bool AddUser()
        {
            if (!UserExistInDatabase())
            {
                var context = new DatabaseConnectionContext();
                var user = new User
                {
                    Name = Environment.UserName,
                    Emial = WelcomeWindow.welcomeWindow._NewUserEmailUserControl._TextboxEmail.Text,
                    KindOfUserId = 1
                };
                context.Add(user);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool UserExistInDatabase()
        {
            var context = new DatabaseConnectionContext();
            var user = context.Users
                .Where(u => u.Name == Environment.UserName)
                .FirstOrDefault();
            
            return user != null;
        }
    }
}
