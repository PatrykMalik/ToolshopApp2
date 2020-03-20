using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolshopApp2.View;
using ToolshopApp2.Data;
using System.Data;
using ToolshopApp2.Model;
using System.Windows;

namespace ToolshopApp2.Controllers
{
    public static class TaskWindowController
    {
        static Request _request = null;
        public static void InitializeTaskWindow()
        {
            var taskWindow = new TaskWindow();
            LoadComboboxItems(taskWindow);
            SetTaskWindowView(taskWindow);

        }        
        public static void InitializeTaskWindow(Object row)
        {
            _request = row as Request;
            var taskWindow = new TaskWindow();

            LoadComboboxItems(taskWindow);

            taskWindow._TaskControlersUserControl._AddRequest.Content = "Update Request";

            taskWindow._SimpleTaskUserControl._TextBoxId.Text = _request.Id.ToString() + " - " + (RequestController.GetStatus(_request.RequestStatusesId) == null ? "" : RequestController.GetStatus(_request.RequestStatusesId).Status);
            taskWindow._SimpleTaskUserControl._ComboBoxClassyfy.Text = _request.Classyfy;
            taskWindow._SimpleTaskUserControl._ComboBoxProject.Text = _request.Project;
            taskWindow._SimpleTaskUserControl._ComboboxTask.Text = _request.Order;
            taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Text = _request.CostCenter;
            taskWindow._SimpleTaskUserControl._DatePickerDeadline.Text = _request.Date.ToShortDateString();
            taskWindow._SimpleTaskUserControl._TextBoxDescription.Text = _request.Description;

            taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked = _request.Attachment;

            taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Text = _request.ContactPerson;
            taskWindow._ShipmentTaskUserControl._TextBoxEmail.Text = _request.Email;
            taskWindow._ShipmentTaskUserControl._TextBoxAdress.Text = _request.Address;
            taskWindow._ShipmentTaskUserControl._TextBoxSrzBegin.Text = _request.BeginigSrz;
            taskWindow._ShipmentTaskUserControl._TextBoxSrzEnd.Text = _request.EndingSrz;
            taskWindow._ShipmentTaskUserControl._ComboBoxTransport.Text = _request.Transpot;
            taskWindow._ShipmentTaskUserControl._ComboBoxInsurance.IsChecked = _request.Insurance;
            taskWindow._ShipmentTaskUserControl._TextBoxInsuranceCost.Text = _request.InsuranceCost;

            taskWindow._ToolshopPartUserControl._TextBoxSrz_1.Text = _request.Srz1;
            taskWindow._ToolshopPartUserControl._TextBoxSrz_2.Text = _request.Srz2;
            taskWindow._ToolshopPartUserControl._TextBoxSrz_3.Text = _request.Srz3;
            taskWindow._ToolshopPartUserControl._TextBoxSrz_4.Text = _request.Srz4;
            taskWindow._ToolshopPartUserControl._TextBoxSrz_5.Text = _request.Srz5;
            taskWindow._ToolshopPartUserControl._TextBoxSwz_1.Text = _request.Swz1;
            taskWindow._ToolshopPartUserControl._TextBoxSwz_2.Text = _request.Swz2;
            taskWindow._ToolshopPartUserControl._TextBoxSwz_3.Text = _request.Swz3;
            taskWindow._ToolshopPartUserControl._TextBoxSwz_4.Text = _request.Swz4;
            taskWindow._ToolshopPartUserControl._TextBoxSwz_5.Text = _request.Swz5;

            SetTaskWindowView(taskWindow);
        }
        public static bool AddRequest()
        {
            var context = new DatabaseConnectionContext();
            if (DateManagingController.IsDateAvaiable(TaskWindow.task._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value) && context != null)
            {
                if (_request == null)
                {
                    context.Add(RequestController.CreateRequest());
                    context.SaveChanges();
                    return true;
                }
                else if (_request.User == Environment.UserName.ToLower() || UserController.IsUserToolshopMember() || UserController.IsUserAdministartor())
                {
                    var request = RequestController.CreateRequest();
                    request.Id = _request.Id;
                    context.Update(request);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public static bool AddAddress()
        {
            var context = new DatabaseConnectionContext();
            var address = new Address()
            {
                ContactPerson = TaskWindow.task._ShipmentTaskUserControl._ComboBoxContactPerson.Text,
                Email = TaskWindow.task._ShipmentTaskUserControl._TextBoxEmail.Text,
                Adress = TaskWindow.task._ShipmentTaskUserControl._TextBoxAdress.Text
            };
            if (context != null)
            {
                context.Add(address);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        private static void SetTaskWindowView(TaskWindow taskWindow)
        {
            if (!(UserController.IsUserToolshopMember() || UserController.IsUserAdministartor()))
            {
                taskWindow._ToolshopPartUserControl.Visibility = Visibility.Hidden;
                taskWindow._ToolshopPartUserControl.Height = 0;
            }
            taskWindow.ShowDialog();
        }
        private static void LoadComboboxItems(TaskWindow taskWindow)
        {
            foreach (var item in ComboboxListController.GetTaskLists())
            {
                taskWindow._SimpleTaskUserControl._ComboboxTask.Items.Add(item.Name);
            }
            foreach (var item in ComboboxListController.GetClassyfyLists())
            {
                taskWindow._SimpleTaskUserControl._ComboBoxClassyfy.Items.Add(item.Name);
            }
            foreach (var item in ComboboxListController.GetProjectLists())
            {
                taskWindow._SimpleTaskUserControl._ComboBoxProject.Items.Add(item.Name);
            }
            foreach (var item in ComboboxListController.GetCostCenterLists())
            {
                taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Items.Add(item.Name);
            }
            foreach (var item in ComboboxListController.GetAddresses())
            {
                taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Items.Add(item.ContactPerson);
            }
        }

    }
}
