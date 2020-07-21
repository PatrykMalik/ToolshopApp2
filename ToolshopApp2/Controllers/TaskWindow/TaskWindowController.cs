using System;
using System.Collections.Generic;
using ToolshopApp2.View;
using ToolshopApp2.Data;
using ToolshopApp2.Model;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using ToolshopApp2.Connection;

namespace ToolshopApp2.Controllers
{
    public static class TaskWindowController
    {
        private static Request _request = null;
        private static TaskWindow taskWindow;

        public static void InitializeTaskWindow()
        {
            _request = null;
            taskWindow = new TaskWindow();
            LoadComboboxItems(taskWindow);
            SetTaskWindowView(taskWindow);

        }
        public static void InitializeTaskWindow(Object row)
        {
            var request = row as Request;
            int id = request.Id;
            request = null;
            _request = RequestController.GetRequest(id);
            taskWindow = new TaskWindow();

            LoadComboboxItems(taskWindow);

            taskWindow._TaskControlersUserControl._AddRequest.Content = "Update Request";

            UpdateRequestStatusInWindow();
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

            //taskWindow._ToolshopPartUserControl._TextBoxSrz_1.Text = _request.Srz1;
            //taskWindow._ToolshopPartUserControl._TextBoxSrz_2.Text = _request.Srz2;
            //taskWindow._ToolshopPartUserControl._TextBoxSrz_3.Text = _request.Srz3;
            //taskWindow._ToolshopPartUserControl._TextBoxSrz_4.Text = _request.Srz4;
            //taskWindow._ToolshopPartUserControl._TextBoxSrz_5.Text = _request.Srz5;
            //taskWindow._ToolshopPartUserControl._TextBoxSwz_1.Text = _request.Swz1;
            //taskWindow._ToolshopPartUserControl._TextBoxSwz_2.Text = _request.Swz2;
            //taskWindow._ToolshopPartUserControl._TextBoxSwz_3.Text = _request.Swz3;
            //taskWindow._ToolshopPartUserControl._TextBoxSwz_4.Text = _request.Swz4;
            //taskWindow._ToolshopPartUserControl._TextBoxSwz_5.Text = _request.Swz5;

            SetTaskWindowView(taskWindow);
        }
        public static bool AddRequest(List<string> filePaths)
        {
            var context = new DatabaseConnectionContext();
            if (DateManagingController.IsDateAvaiable(TaskWindow.task._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value) && context != null)
            {
                if (_request == null)
                {
                    _request = RequestController.CreateRequest();
                    context.Add(_request);
                    context.SaveChanges();
                    if (taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value)
                    {
                        foreach(var filePath in filePaths)
                            SendAttachments(filePath);
                    }
                    //MailController.SendConfirmation(_request);
                    _request = null;
                    return true;
                }
                else if ((_request.User == Environment.UserName.ToLower() 
                    || UserController.IsUserToolshopMemberOrAdministator()) && _request.Status != "Closed")
                {
                    var oldRequest = _request;
                    _request = RequestController.UpdateRequest(_request.Id);
                    context.Update(_request);
                    context.SaveChanges();
                    //MailController.SendUpdateNotification(_request, oldRequest);
                    _request = null;
                    return true;
                }
                else if (_request.Status == "Closed")
                {
                    MessageBox.Show("Is imposible to update this task, becouse is in status: " +  _request.Status, "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                TaskWindow.task._ShipmentTaskUserControl._ComboBoxContactPerson.Items.Clear();
                foreach (var item in ComboboxListController.GetAddresses())
                {
                    TaskWindow.task._ShipmentTaskUserControl._ComboBoxContactPerson.Items.Add(item.ContactPerson);
                }
                return true;
            }
            return false;
        }
        public static void AddProject(string s)
        {
            ComboboxListController.AddProject(s);
            TaskWindow.task._SimpleTaskUserControl._ComboBoxProject.Items.Clear();
            foreach (var item in ComboboxListController.GetProjectLists())
            {
                TaskWindow.task._SimpleTaskUserControl._ComboBoxProject.Items.Add(item.Name);
            }
        }
        public static void AcceptTask()
        {
            var context = new DatabaseConnectionContext();
            if (context != null && _request != null)
            {
                if (_request.Status == "Open")
                {
                    _request.Status = "Accepted";
                    context.Update(_request);
                    context.SaveChanges();
                    UpdateRequestStatusInWindow();
                    MailController.SendNotification(_request);
                }
                else
                {
                    MessageBox.Show("Task wasn't accepted.", "Accepting warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public static void CloseTask()
        {
            var context = new DatabaseConnectionContext();
            if (context != null && _request != null)
            {
                if (_request.Status == "Accepted")
                {
                    _request.Status = "Closed";
                    context.Update(_request);
                    context.SaveChanges();
                    UpdateRequestStatusInWindow();
                    MailController.SendNotification(_request);
                }
                else
                {
                    MessageBox.Show("Task wasn't closed.", "Closing warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public static void ReopenTask()
        {
            var context = new DatabaseConnectionContext();
            if (context != null && _request != null)
            {
                if (_request.Status == "Accepted")
                {
                    _request.Status = "Open";
                    context.Update(_request);
                    context.SaveChanges();
                    UpdateRequestStatusInWindow();
                    MailController.SendNotification(_request);
                }
                else
                {
                    MessageBox.Show("Task wasn't reopened.", "Reopen warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        public static void AddCostCenter(string s)
        {
            ComboboxListController.AddCostCenter(s);
            TaskWindow.task._SimpleTaskUserControl._ComboBoxCostCenter.Items.Clear();
            foreach (var item in ComboboxListController.GetCostCenterLists())
            {
                TaskWindow.task._SimpleTaskUserControl._ComboBoxCostCenter.Items.Add(item.Name);
            }
        }
        public static void OpenAttachments()
        {
            if (_request != null)
                if (Directory.Exists(ConnectionString.Attachement_path + "\\" + _request.Id))
                    System.Diagnostics.Process.Start("explorer.exe", ConnectionString.Attachement_path + "\\" + _request.Id);
        }
        public static bool SendAttachments(string filePath)
        {
            if (_request != null)
            {
                if (_request.Status != "Closed")
                {
                    string att_dir = System.IO.Path.Combine(ConnectionString.Attachement_path, _request.Id.ToString());
                    System.IO.Directory.CreateDirectory(att_dir);
                    if (System.IO.Directory.Exists(att_dir))
                    {
                        try
                        {
                            System.IO.File.Copy(filePath, System.IO.Path.Combine(att_dir, System.IO.Path.GetFileName(filePath)), true);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Adding attachments to closed task is unavaiable", "Attachment Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }
            }
            return false;
        }
        
        private static void UpdateRequestStatusInWindow()
        {
            var context = new DatabaseConnectionContext();
            taskWindow._SimpleTaskUserControl._TextBoxId.Text = _request.Id.ToString()
                + " - " + _request.Status;//(_request.StatusId == null ? "" : context.RequestStatuses.Where(x => x.StatusId == _request.StatusId).FirstOrDefault().Status);
        }
        private static void SetTaskWindowView(TaskWindow taskWindow)
        {
            if (!UserController.IsUserToolshopMemberOrAdministator())
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
        public static void TextboxOnlyNumeric(TextBox textBox)
        {
            var tuple = IsStringOnlyNumeric(textBox.Text, textBox.CaretIndex);
            textBox.Text = tuple.Item1;
            textBox.CaretIndex = tuple.Item2;
        }
        private static Tuple<string, int> IsStringOnlyNumeric(string text, int caretIndex)
        {
            int i = 0;
            foreach (char c in text)
            {
                if (!Char.IsNumber(c))
                {
                    caretIndex--;
                    var temp = text.Substring(0, i == 0 ? 0 : i);
                    temp += i < text.Length ? text.Substring(i + 1) : String.Empty;
                    text = temp;
                    i--;
                }
                i++;
            }
            return Tuple.Create(text, caretIndex < 0 ? 0 : caretIndex);
        }
    }
}
