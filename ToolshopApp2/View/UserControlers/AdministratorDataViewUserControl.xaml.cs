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
    /// Interaction logic for AdministratorDataViewUserControl.xaml
    /// </summary>
    public partial class AdministratorDataViewUserControl : UserControl
    {
        public AdministratorDataViewUserControl()
        {
            InitializeComponent();
        }

        private void _ButtonShowUsersClick(object sender, RoutedEventArgs e)
        {
            var users = UserController.GetUsers();
            _DataGridDatabaseView.ItemsSource = users;
        }

        private void _ButtonShowProjectClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
