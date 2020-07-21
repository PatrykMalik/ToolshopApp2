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
    /// Interaction logic for AdministratorAddValuesToDatabaseUserControl.xaml
    /// </summary>
    public partial class AdministratorAddValuesToDatabaseUserControl : UserControl
    {
        public AdministratorAddValuesToDatabaseUserControl()
        {
            InitializeComponent();
        }

        private void _ButtonAddTaskClick(object sender, RoutedEventArgs e)
        {
            ComboboxListController.AddTask(_TextBoxAddTask.Text);
            _TextBoxAddTask.Text = String.Empty;
        }

        private void _ButtonAddClassyfyClick(object sender, RoutedEventArgs e)
        {
            ComboboxListController.AddClassyfy(_TextBoxAddClassyfy.Text);
            _TextBoxAddClassyfy.Text = String.Empty;
        }
    }
}
