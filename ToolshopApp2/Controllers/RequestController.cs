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
                Classyfy = TaskWindow.task._SimpleTaskUserControl._ComboBoxClassyfy.Text,
                Project = TaskWindow.task._SimpleTaskUserControl._ComboBoxProject.Text,
                Order = TaskWindow.task._SimpleTaskUserControl._ComboboxTask.Text,
                Date = TaskWindow.task._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value,
                Description = TaskWindow.task._SimpleTaskUserControl._TextBoxDescription.Text,
                CostCenter = TaskWindow.task._SimpleTaskUserControl._ComboBoxCostCenter.Text,
                Status = "Open",
                //Status = new RequestStatus { StatusId = 1 },
                //RequestStatus = new RequestStatus(),
                Attachment = TaskWindow.task._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value,
                CreationTime = DateTime.UtcNow
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
                request.DescpriptionToolshop = TaskWindow.task._ToolshopPartUserControl._TextBoxDescription.Text;
            }
            return request;
        }

        public static Request UpdateRequest(int id)
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            var request = new Request()
            {
                Id = id,
                User = Environment.UserName.ToLower(),
                Classyfy = TaskWindow.task._SimpleTaskUserControl._ComboBoxClassyfy.Text,
                Project = TaskWindow.task._SimpleTaskUserControl._ComboBoxProject.Text,
                Order = TaskWindow.task._SimpleTaskUserControl._ComboboxTask.Text,
                Date = TaskWindow.task._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value,
                Description = TaskWindow.task._SimpleTaskUserControl._TextBoxDescription.Text,
                CostCenter = TaskWindow.task._SimpleTaskUserControl._ComboBoxCostCenter.Text,
                Status = context.Requests.Where(x => x.Id == id).FirstOrDefault().Status,
                Attachment = TaskWindow.task._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value,
                CreationTime = DateTime.UtcNow
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
                request.DescpriptionToolshop = TaskWindow.task._ToolshopPartUserControl._TextBoxDescription.Text;
            }
            return request;
        }

        public static Request GetRequest(int id)
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            return context.Requests.Where(x => x.Id == id).FirstOrDefault();
        }

        public static IEnumerable<Request> GetRequests()
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            return context.Requests.ToArray<Request>();
        }
        //public static List<Request> GetRequests()
        //{
        //    return _context.Requests.ToList();
        //}
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
