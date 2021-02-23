using System;
using System.Collections.Generic;
using ToolshopApp2.View;
using ToolshopApp2.Data;
using ToolshopApp2.Model;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using ToolshopApp2.Connection;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

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
            HideControls(taskWindow);
            SetTaskWindowView(taskWindow);
            taskWindow.ShowDialog();
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
            taskWindow._SimpleTaskUserControl._TextBoxDescription.IsReadOnly = true;
            taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked = _request.Attachment;

            taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Text = _request.ContactPerson;
            taskWindow._ShipmentTaskUserControl._TextBoxEmail.Text = _request.Email;
            taskWindow._ShipmentTaskUserControl._TextBoxAdress.Text = _request.Address;
            taskWindow._ShipmentTaskUserControl._TextBoxSrzBegin.Text = _request.BeginigSrz;
            taskWindow._ShipmentTaskUserControl._TextBoxSrzEnd.Text = _request.EndingSrz;
            taskWindow._ShipmentTaskUserControl._ComboBoxTransport.Text = _request.Transpot;
            taskWindow._ShipmentTaskUserControl._ComboBoxInsurance.IsChecked = _request.Insurance;
            taskWindow._ShipmentTaskUserControl._TextBoxInsuranceCost.Text = _request.InsuranceCost;

            SetTaskWindowView(taskWindow);
            taskWindow.ShowDialog();
        }
        public static void DuplicateTask(Object row)
        {
            var request = row as Request;
            int id = request.Id;
            request = RequestController.GetRequest(id);
            _request = null;
            taskWindow = new TaskWindow();

            LoadComboboxItems(taskWindow);

            taskWindow._SimpleTaskUserControl._ComboBoxClassyfy.Text = request.Classyfy;
            taskWindow._SimpleTaskUserControl._ComboBoxProject.Text = request.Project;
            taskWindow._SimpleTaskUserControl._ComboboxTask.Text = request.Order;
            taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Text = request.CostCenter;
            taskWindow._SimpleTaskUserControl._DatePickerDeadline.Text = request.Date.ToShortDateString();
            taskWindow._SimpleTaskUserControl._TextBoxDescription.Text = request.Description;

            taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked = request.Attachment;

            taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Text = request.ContactPerson;
            taskWindow._ShipmentTaskUserControl._TextBoxEmail.Text = request.Email;
            taskWindow._ShipmentTaskUserControl._TextBoxAdress.Text = request.Address;
            taskWindow._ShipmentTaskUserControl._TextBoxSrzBegin.Text = request.BeginigSrz;
            taskWindow._ShipmentTaskUserControl._TextBoxSrzEnd.Text = request.EndingSrz;
            taskWindow._ShipmentTaskUserControl._ComboBoxTransport.Text = request.Transpot;
            taskWindow._ShipmentTaskUserControl._ComboBoxInsurance.IsChecked = request.Insurance;
            taskWindow._ShipmentTaskUserControl._TextBoxInsuranceCost.Text = request.InsuranceCost;
            SetTaskWindowView(taskWindow);
            taskWindow.ShowDialog();
        }
        public static bool AddRequest(List<string> filePaths)
        {
            bool output = false;
            if (_request == null)
            {
                output = AddNewRequestToDatabase(filePaths);
                if (output)
                {
                    UpdateRequestStatusInWindow();
                }
            }
            else if ((_request.User == Environment.UserName.ToLower()
                || UserController.IsUserToolshopMemberOrAdministator()) && _request.Status != "Closed")
            {
                output = UpdateRequestInDatabase();
            }
            else if (_request.Status == "Closed")
            {
                MessageBox.Show("Is imposible to update this task, because is in status: " + _request.Status, "Update Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return output;
        }
        public static bool AddAddress()
        {
            var context = new DatabaseConnectionContext();
            var address = new Address()
            {
                ContactPerson = TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Text,
                Email = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxEmail.Text,
                Adress = TaskWindow.taskWindow._ShipmentTaskUserControl._TextBoxAdress.Text
            };
            if (context != null)
            {
                context.Add(address);
                context.SaveChanges();
                TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Items.Clear();
                foreach (var item in ComboboxListController.GetAddresses())
                {
                    TaskWindow.taskWindow._ShipmentTaskUserControl._ComboBoxContactPerson.Items.Add(item.ContactPerson);
                }
                return true;
            }
            return false;
        }
        public static void AddProject(string s)
        {
            if (!IsProjectEmptyOrDuplicated(s))
            {
                ComboboxListController.AddProject(s);
                TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxProject.Items.Clear();
                foreach (var item in ComboboxListController.GetProjectLists())
                {
                    TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxProject.Items.Add(item.Name);
                }
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
            if (!IsCostCenterEmptyOrDuplicated(s))
            {
                ComboboxListController.AddCostCenter(s);
                TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Items.Clear();
                foreach (var item in ComboboxListController.GetCostCenterLists())
                {
                    TaskWindow.taskWindow._SimpleTaskUserControl._ComboBoxCostCenter.Items.Add(item.Name);
                }
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
                    string att_dir = Path.Combine(ConnectionString.Attachement_path, _request.Id.ToString());
                    Directory.CreateDirectory(att_dir);
                    if (Directory.Exists(att_dir))
                    {
                        try
                        {
                            File.Copy(filePath, System.IO.Path.Combine(att_dir, Path.GetFileName(filePath)), true);
                            taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked = true;
                            SilentUpdateRequestInDatabase();
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
                }
            }
            return false;
        }
        public static void AddComment(string s)
        {
            _request.Description += Environment.NewLine + Environment.NewLine + " " + DateTime.Now + " " + Environment.UserName + Environment.NewLine + s;
            var context = new DatabaseConnectionContext();
            context.Update(_request);
            context.SaveChangesAsync();
            taskWindow._SimpleTaskUserControl._TextBoxDescription.Text = _request.Description;
            MailController.SendCommentNotification(s, Environment.UserName, _request);
        }
        public static void GeneratePIC()
        {
            if (_request == null)
            {
                CopyPICFile(CreateTempDir());
            }
            else if (_request != null)
            {
                string att_dir = Path.Combine(ConnectionString.Attachement_path, _request.Id.ToString());
                if (!Directory.Exists(att_dir))
                {
                    Directory.CreateDirectory(att_dir);
                }
                if (CopyPICFile(att_dir))
                {
                    SilentUpdateRequestInDatabase();
                }
            }
        }
        private static bool SilentUpdateRequestInDatabase()
        {
            var context = new DatabaseConnectionContext();
            if (DateManagingController.IsDateAvaiable(TaskWindow.taskWindow._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value) && context != null)
            {
                var oldRequest = _request;
                _request = RequestController.UpdateRequest(_request.Id, oldRequest.User);
                context.Update(_request);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        private static bool UpdateRequestInDatabase()
        {
            var context = new DatabaseConnectionContext();
            var oldRequest = _request;
            if (oldRequest.Date == taskWindow._SimpleTaskUserControl._DatePickerDeadline.SelectedDate)
            {
                _request = RequestController.UpdateRequest(_request.Id, oldRequest.User);
                context.Update(_request);
                context.SaveChanges();
                MailController.SendUpdateNotification(_request, oldRequest);
                return true;
            }
            if (DateManagingController.IsDateAvaiable(TaskWindow.taskWindow._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value) && context != null)
            {
                _request = RequestController.UpdateRequest(_request.Id, oldRequest.User);
                context.Update(_request);
                context.SaveChanges();
                MailController.SendUpdateNotification(_request, oldRequest);
                return true;
            }
            return false;
        }
        private static bool AddNewRequestToDatabase(List<string> filePaths)
        {
            var context = new DatabaseConnectionContext();
            if (DateManagingController.IsDateAvaiable(TaskWindow.taskWindow._SimpleTaskUserControl._DatePickerDeadline.SelectedDate.Value) && context != null)
            {
                _request = RequestController.CreateRequest();
                context.Add(_request);
                context.SaveChanges();
                if (taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked.Value)
                {
                    foreach (var filePath in filePaths)
                        SendAttachments(filePath);
                }
                MailController.SendConfirmation(_request);
                //_request = null;
                return true;
            }
            return false;
        }
        private static bool CopyPICFile(string dest_dir_path)
        {
            if (Directory.Exists(dest_dir_path))
            {
                int i = 1;
                string dest_file_path = Path.Combine(dest_dir_path, "PIC_" + i.ToString() + ".xlsb");
                while (File.Exists(dest_file_path))
                {
                    i++;
                    dest_file_path = Path.Combine(dest_dir_path, "PIC_" + i.ToString() + ".xlsb");
                }
                File.Copy(ConnectionString.PIC_source_path, dest_file_path, false);
                Application excel = new Application();
                Workbook wb = excel.Workbooks.Open(dest_file_path);
                excel.Visible = true;
                taskWindow._TaskControlersUserControl._CheckBoxAttachement.IsChecked = true;
                return true;
            }
            else
            {
                MessageBox.Show("Problem with destination directory, please try again", "Destination Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return false;
        }
        private static string CreateTempDir()
        {
            string temp_dir = Path.Combine(@"C:\Users", Environment.UserName, @"Documents\ToolshopAppTemp");
            if (Directory.Exists(temp_dir))
            {
                return temp_dir;
            }
            Directory.CreateDirectory(temp_dir);
            return temp_dir;
        }
        private static void UpdateRequestStatusInWindow()
        {
            taskWindow._SimpleTaskUserControl._TextBoxId.Text = _request.Id.ToString() + " - " + _request.Status;
        }
        private static void SetTaskWindowView(TaskWindow taskWindow)
        {
            if (!UserController.IsUserToolshopMemberOrAdministator())
            {
                taskWindow._ToolshopPartUserControl.Visibility = Visibility.Collapsed;
            }
            if (!(_request == null))
            {
                if (_request.Status == "Closed")
                {
                    taskWindow._TaskControlersUserControl.Visibility = Visibility.Collapsed;
                    //taskWindow._CommentPartUserControl.Visibility = Visibility.Collapsed;
                }
            }
        }
        private static void HideControls(TaskWindow taskWindow)
        {
            taskWindow._CommentPartUserControl.Visibility = Visibility.Collapsed;
            taskWindow._TaskControlersUserControl._ButtonAttachFile.Visibility = Visibility.Collapsed;
            taskWindow._TaskControlersUserControl._ButtonGeneratePIC.Visibility = Visibility.Collapsed;
            taskWindow._TaskControlersUserControl._ButtonOpenAttachments.Visibility = Visibility.Collapsed;
            taskWindow._TaskControlersUserControl._CheckBoxAttachement.Visibility = Visibility.Collapsed;
            taskWindow._TaskControlersUserControl._CloseWindowButton.Visibility = Visibility.Collapsed;
        }
        public static void ShowControls()
        {
            taskWindow._CommentPartUserControl.Visibility = Visibility.Visible;
            taskWindow._TaskControlersUserControl._ButtonAttachFile.Visibility = Visibility.Visible;
            taskWindow._TaskControlersUserControl._ButtonGeneratePIC.Visibility = Visibility.Visible;
            taskWindow._TaskControlersUserControl._ButtonOpenAttachments.Visibility = Visibility.Visible;
            taskWindow._TaskControlersUserControl._CheckBoxAttachement.Visibility = Visibility.Visible;
            taskWindow._TaskControlersUserControl._CloseWindowButton.Visibility = Visibility.Visible;
            taskWindow._TaskControlersUserControl._AddRequest.Content = "Update Request";
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
        public static void TextboxOnlyNumeric(System.Windows.Controls.TextBox textBox)
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
        private static bool IsProjectEmptyOrDuplicated(string project)
        {
            if (project.Trim() == String.Empty)
            {
                return true;
            }
            foreach (var item in ComboboxListController.GetProjectLists())
            {
                if (project == item.Name)
                    return true;
            }
            return false;
        }
        private static bool IsCostCenterEmptyOrDuplicated(string project)
        {
            if (project.Trim() == String.Empty)
            {
                return true;
            }
            foreach (var item in ComboboxListController.GetCostCenterLists())
            {
                if (project == item.Name)
                    return true;
            }
            return false;
        }
        private static void ClearTempDir()
        {
            string temp_dir = Path.Combine(@"C:\Users", Environment.UserName, @"Documents\ToolshopAppTemp");
            if (Directory.Exists(temp_dir))
            {
                List<string> temp_files = new List<string>();
                foreach (var file in Directory.GetFiles(temp_dir))
                {
                    temp_files.Add(file);
                }
                if (temp_files.Count > 0)
                {
                    if (MessageBox.Show("You have prepared PIC files. Do you want to store them? If No, then will be deleted!", "PIC folder not empty", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes)
                        == MessageBoxResult.No)
                    {
                        foreach (var file in temp_files)
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
        }
    }
}