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
using ToolshopApp2.Controllers;
using ToolshopApp2.View;

namespace ToolshopApp2.View.UserControlers
{
    /// <summary>
    /// Interaction logic for NewUserEmailUserControl.xaml
    /// </summary>
    public partial class NewUserEmailUserControl : UserControl
    {
        public NewUserEmailUserControl()
        {
            InitializeComponent();
        }
        private void _ButtonAddEmailClick(object sender, RoutedEventArgs e)
        {
            if (UserController.AddUser(ToolshopApp2.View.WelcomeWindow.welcomeWindow._NewUserEmailUserControl._TextboxEmail.Text))
            {
                MessageBox.Show("Email has been correcly added to database","Add Email",MessageBoxButton.OK,MessageBoxImage.Information);
                ToolshopApp2.View.WelcomeWindow.welcomeWindow.Close();
            }
            else
            {
                MessageBox.Show("Email hasn't been correcly added to database", "Add Email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
