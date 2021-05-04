using System;
using System.Collections.Generic;
using System.Linq;
using ToolshopApp2.Model;
using ToolshopApp2.View;
using ToolshopApp2.Data;
using System.Data;
using System.Reflection;
//using Microsoft.Office.Interop.Excel;

namespace ToolshopApp2.Controllers
{
    public class RequestController
    {
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
                request.Time = TaskWindow.taskWindow._ToolshopPartUserControl._TextBoxTime.Text;
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
                request.Time = TaskWindow.taskWindow._ToolshopPartUserControl._TextBoxTime.Text;
            }
            return request;
        }
        public static Request GetRequest(int id)
        {
            DatabaseConnectionContext context = new DatabaseConnectionContext();
            return context.Requests.Where(x => x.Id == id).FirstOrDefault();
        }
        public static void AcceptRequest(Request request)
        {
            request.Status = "Accepted";
            var context = new DatabaseConnectionContext();
            context.Update(request);
            context.SaveChanges();
        }
        public static IEnumerable<Request> GetRequests()
        {
            var context = new DatabaseConnectionContext();
            return context.Requests.ToArray<Request>();
        }
        public static DataTable GetRequestsTable()
        {
            var context = new DatabaseConnectionContext();
            var requestTable = new DataTable(typeof(Request).Name);
            var list = context.Requests.ToArray<Request>();
            PropertyInfo[] properties = typeof(Request).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                requestTable.Columns.Add(prop.Name);
            }
            foreach (var item in list)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                requestTable.Rows.Add(values);
            }
            return requestTable;
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
    }
}
