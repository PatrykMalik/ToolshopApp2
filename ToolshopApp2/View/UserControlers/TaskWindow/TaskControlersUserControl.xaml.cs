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
    /// Interaction logic for TaskControlersUserControl.xaml
    /// </summary>
    public partial class TaskControlersUserControl : UserControl
    {
        public TaskControlersUserControl()
        {
            InitializeComponent();
        }

        private void _AddRequestClick(object sender, RoutedEventArgs e)
        {
            if (TaskWindowController.AddRequest())
            {
                TaskWindow.task.Close();
            }
            else
            {
               
            }
        }
    }
}
