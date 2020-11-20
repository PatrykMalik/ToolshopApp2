using System;
using System.Windows;
using System.Windows.Controls;
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

        private void _DataGridDatabaseView_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
    }
}
