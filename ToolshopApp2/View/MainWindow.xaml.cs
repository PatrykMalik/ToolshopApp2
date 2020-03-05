﻿using System;
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
using ToolshopApp2.View;
using ToolshopApp2.Controllers;


namespace ToolshopApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWelcomeWindow();
            if (!TaskWindowController.UserExistInDatabase())
            {
                this.Close();
            }
            if (!UserController.IsUserAdministartor())
            {
                _TabItemAdministrator.Visibility = Visibility.Hidden;
            }
            else
            {
                InitializeAdministratorTab();
            }
            
        }
        private void InitializeAdministratorTab()
        {
            _AdministratorAddValuesToDatabaseUserControl.Visibility = Visibility.Hidden;
            _AdministratorDataViewUserControl.Visibility = Visibility.Hidden;
        }
        private void _ButtonNewTaskClick(object sender, RoutedEventArgs e)
        {
            var taskWindow = new TaskWindow();
        }

        private void InitializeWelcomeWindow()
        {
            var welcomeWindow = new WelcomeWindow();
        }

        private void _ButtonAddValuesViewClick(object sender, RoutedEventArgs e)
        {
            InitializeAdministratorTab();
            _AdministratorAddValuesToDatabaseUserControl.Visibility = Visibility.Visible;
        }

        private void _ButtonDataViewClick(object sender, RoutedEventArgs e)
        {
            InitializeAdministratorTab();
            _AdministratorDataViewUserControl.Visibility = Visibility.Visible;
        }
    }
}