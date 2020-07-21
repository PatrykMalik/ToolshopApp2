using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ToolshopApp2.Controllers;

namespace ToolshopApp2.View.UserControlers
{
    /// <summary>
    /// Interaction logic for TaskControlersUserControl.xaml
    /// </summary>
    public partial class TaskControlersUserControl : UserControl
    {
        //zmienna globalna - przechowuje adresy załączników
        private List<string> filePath;

        public TaskControlersUserControl()
        {
            InitializeComponent();
        }

        private void _AddRequestClick(object sender, RoutedEventArgs e)
        {
            if (TaskWindowController.AddRequest(filePath))
            {
                TaskWindow.task.Close();
            }
            else
            {

            }
        }

        private void _ButtonAttachFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\" + Environment.UserName + "\\Documents";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                // adding attachment to already exiting request
                if (TaskWindowController.SendAttachments(openFileDialog.FileName))
                {                 
                    _CheckBoxAttachement.IsChecked = true;
                    TaskWindowController.AddRequest(filePath);
                }
                // adding addreses to new request (without assigned ID)
                else
                {
                    filePath.Add(openFileDialog.FileName);
                    _CheckBoxAttachement.IsChecked = true;
                }
            }
        }

        private void _ButtonOpenAttachments_Click(object sender, RoutedEventArgs e)
        {
            if (_CheckBoxAttachement.IsChecked.Value)
            {
                TaskWindowController.OpenAttachments();
            }
            else
                MessageBox.Show("You can't open attachments if they don't exitst!", "Missing Attachment", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
