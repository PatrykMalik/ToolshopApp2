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
            //ResizeWindowBasedOnTask();
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
                    TaskWindow.task._ShipmentTaskUserControl.Visibility = Visibility.Visible;
                    TaskWindow.task._ShipmentTaskUserControl.Width = TaskWindow.task._SimpleTaskUserControl.Width;
                }
                else if (TaskWindow.task._ShipmentTaskUserControl.IsInitialized)
                {
                    TaskWindow.task._ShipmentTaskUserControl.Visibility = Visibility.Hidden;
                    TaskWindow.task._ShipmentTaskUserControl.Width = 0;
                }
            }
        }

        private void _DatePickerDeadlineSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateManagingController.IsSeletedDateOnWeekend(_DatePickerDeadline.SelectedDate.Value))
            {
                _DatePickerDeadline.IsDropDownOpen = true;
            }
            else if (!DateManagingController.IsDateAvaiable(_DatePickerDeadline.SelectedDate.Value))
            {
                MessageBox.Show("Selected date is unavaiable.\nPlease reschedule task.", "Selected date is unavaiable", MessageBoxButton.OK, MessageBoxImage.Warning);
                _DatePickerDeadline.IsDropDownOpen = true;
            }
        }

        private void _ButtonAddProjectClick(object sender, RoutedEventArgs e)
        {
            ComboboxListController.AddProject(_ComboBoxProject.Text);
        }

        private void _ButtonAddCostCenterClick(object sender, RoutedEventArgs e)
        {
            ComboboxListController.AddCostCenter(_ComboBoxCostCenter.Text);
        }
    }
}
