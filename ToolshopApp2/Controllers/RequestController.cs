using System;
using System.Collections.Generic;
using System.Linq;
using ToolshopApp2.Model;
using ToolshopApp2.View;
using ToolshopApp2.Data;
using Microsoft.Office.Interop.Excel;

namespace ToolshopApp2.Controllers
{
    public class RequestController
    {
        //private static DatabaseConnectionContext _context = new DatabaseConnectionContext();
        
        public static Request CreateRequest()
        {
            var request = new Request()
            {
                User = Environment.UserName.ToLower(),
                Classyfy = TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxClassyfy.Text,
                Project = TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxProject.Text,
                Order = TaskWindow.taskWindow._SimpleTaskUserControl._ComboboxTask.Text,
                Date = TaskWindow.taskWindow._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value,
                Description = TaskWindow.taskWindow._SimpleTaskUserControl._TextBoxDescription.Text,
                CostCenter = TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Text,
                Status = "Open",
                //Status = new RequestStatus { StatusId = 1 },
                //RequestStatus = new RequestStatus(),
                Attachment = TaskWindow.taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value,
                CreationTime = DateTime.UtcNow
            };
            if ((request.Order == "Shipping Dishwasher" || request.Order == "Shipping Components") && TaskWindow.taskWindow._ShipmentTaskUserControl.IsInitialized)
            {
                request.ContactPerson = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Text;
                request.Email = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxEmail.Text;
                request.Address = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxAdress.Text;
                request.BeginigSrz = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxSrzBegin.Text;
                request.EndingSrz = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxSrzEnd.Text;
                request.Transpot = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxTransport.Text;
                request.Insurance = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxInsurance.IsChecked.Value;
                request.InsuranceCost = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxInsuranceCost.Text;
            }
            if (UserController.IsUserToolshopMemberOrAdministator())
            {
                //request.Srz1 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_1.Text;
                //request.Srz2 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_2.Text;
                //request.Srz3 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_3.Text;
                //request.Srz4 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_4.Text;
                //request.Srz5 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_5.Text;
                //request.Swz1 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_1.Text;
                //request.Swz2 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_2.Text;
                //request.Swz3 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_3.Text;
                //request.Swz4 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_4.Text;
                //request.Swz5 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_5.Text;
                //request.DescpriptionToolshop = TaskWindow.task._ToolshopPartUserControl._TextBoxDescription.Text;
            }
            return request;
        }

        public static Request UpdateRequest(int id, string user)
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            
            var request = new Request()
            {
                Id = id,
                User = user,
                Classyfy = TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxClassyfy.Text,
                Project = TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxProject.Text,
                Order = TaskWindow.taskWindow._SimpleTaskUserControl._ComboboxTask.Text,
                Date = TaskWindow.taskWindow._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value,
                Description = TaskWindow.taskWindow._SimpleTaskUserControl._TextBoxDescription.Text,
                CostCenter = TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Text,
                Status = context.Requests.Where(x => x.Id == id).FirstOrDefault().Status,
                Attachment = TaskWindow.taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value,
                CreationTime = DateTime.UtcNow
            };
            if ((request.Order == "Shipping Dishwasher" || request.Order == "Shipping Components") && TaskWindow.taskWindow._ShipmentTaskUserControl.IsInitialized)
            {
                request.ContactPerson = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Text;
                request.Email = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxEmail.Text;
                request.Address = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxAdress.Text;
                request.BeginigSrz = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxSrzBegin.Text;
                request.EndingSrz = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxSrzEnd.Text;
                request.Transpot = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxTransport.Text;
                request.Insurance = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxInsurance.IsChecked.Value;
                request.InsuranceCost = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxInsuranceCost.Text;
            }
            if (UserController.IsUserToolshopMemberOrAdministator())
            {
                //request.Srz1 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_1.Text;
                //request.Srz2 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_2.Text;
                //request.Srz3 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_3.Text;
                //request.Srz4 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_4.Text;
                //request.Srz5 = TaskWindow.task._ToolshopPartUserControl._TextBoxSrz_5.Text;
                //request.Swz1 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_1.Text;
                //request.Swz2 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_2.Text;
                //request.Swz3 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_3.Text;
                //request.Swz4 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_4.Text;
                //request.Swz5 = TaskWindow.task._ToolshopPartUserControl._TextBoxSwz_5.Text;
                //request.DescpriptionToolshop = TaskWindow.task._ToolshopPartUserControl._TextBoxDescription.Text;
            }
            return request;
        }

        public static Request GetRequest(int id)
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            return context.Requests.Where(x => x.Id == id).FirstOrDefault();
        }

        public  static void AcceptRequest(Request request)
        {
            request.Status = "Accepted";
            var context = new DatabaseConnectionContext();
            context.Update(request);
            context.SaveChanges();
        }

        public static void AddComment(string comment)
        {

        }

        public static IEnumerable<Request> GetRequests()
        {
            var context = new DatabaseConnectionContext();
            return context.Requests.ToArray<Request>();
        }
        
        public static IEnumerable<Request> GetOpenRequests()
        {
            var context = new DatabaseConnectionContext();
            return context.Requests.Where<Request>(x => x.Status == "Open").ToList();
        }
        
        public static IEnumerable<Request> GetRequests(DateTime date)
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            return context.Requests.Where(p => p.Date == date).ToArray<Request>();
        }
        
        public static RequestStatus GetStatus(int id)
        {
            //return _context.RequestStatuses.Where(i => i.StatusId == id).FirstOrDefault<RequestStatus>();
            return null;
        }
    }
}
