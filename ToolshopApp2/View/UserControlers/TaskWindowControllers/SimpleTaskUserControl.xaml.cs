using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolshopApp2.Controllers;
using System.Linq.Expressions;

namespace ToolshopApp2.View.UserControlers
{
    /// <summary>
    /// Interaction logic for SimpleTaskUserControl.xaml
    /// </summary>
    public partial class SimpleTaskUserControl : UserControl
    {
        public SimpleTaskUserControl()
        {
            InitializeComponent();
            _DatePickerDeadline.SelectedDate = DateTime.Today.AddDays(7);            
        }
        private void _ComboboxTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResizeWindowBasedOnTask();
        }
        private void ResizeWindowBasedOnTask()
        {
            if (_ComboboxTask.IsInitialized)
            {
                if (_ComboboxTask.SelectedIndex == 5 || _ComboboxTask.SelectedIndex == 6)
                {
                    TaskWindow.taskWindow._ShipmentTaskUserControl.Visibility = Visibility.Visible;
                }
                else if (TaskWindow.taskWindow._ShipmentTaskUserControl.IsInitialized)
                {
                    TaskWindow.taskWindow._ShipmentTaskUserControl.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void _ButtonAddProjectClick(object sender, RoutedEventArgs e)
        {
            TaskWindowController.AddProject(_ComboBoxProject.Text);
        }
        private void _ButtonAddCostCenterClick(object sender, RoutedEventArgs e)
        {
            TaskWindowController.AddCostCenter(_ComboBoxCostCenter.Text);
        }
    }
}
