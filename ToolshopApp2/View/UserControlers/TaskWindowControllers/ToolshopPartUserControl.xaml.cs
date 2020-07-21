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
    /// Interaction logic for ToolshopPartUserControl.xaml
    /// </summary>
    public partial class ToolshopPartUserControl : UserControl
    {
        public ToolshopPartUserControl()
        {
            InitializeComponent();
            //_TextBoxSrz_5.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSrz_5.IsEnabled = false;
            //_TextBoxSrz_4.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSrz_4.IsEnabled = false;
            //_TextBoxSrz_3.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSrz_3.IsEnabled = false;
            //_TextBoxSrz_2.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSrz_2.IsEnabled = false;
            //_TextBoxSwz_5.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSwz_5.IsEnabled = false;
            //_TextBoxSwz_4.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSwz_4.IsEnabled = false;
            //_TextBoxSwz_3.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSwz_3.IsEnabled = false;
            //_TextBoxSwz_2.Visibility = System.Windows.Visibility.Hidden;
            //_TextBoxSwz_2.IsEnabled = false;
        }

        private void _ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Do you really want to accept the request?", "Accept request", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                TaskWindowController.AcceptTask();
            }
        }

        private void _ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Do you really want to close the request?", "Close request", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                TaskWindowController.CloseTask();
            }
        }

        private void _ButtonReopen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Do you really want to reopen the request?", "Reopen request", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                TaskWindowController.ReopenTask();
            }
        }

        //private void _ButtonSrzPlus_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!_TextBoxSrz_1.IsEnabled)
        //    {
        //        _TextBoxSrz_1.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSrz_1.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSrz_2.IsEnabled)
        //    {
        //        _TextBoxSrz_2.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSrz_2.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSrz_3.IsEnabled)
        //    {
        //        _TextBoxSrz_3.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSrz_3.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSrz_4.IsEnabled)
        //    {
        //        _TextBoxSrz_4.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSrz_4.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSrz_5.IsEnabled)
        //    {
        //        _TextBoxSrz_5.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSrz_5.IsEnabled = true;
        //    }
        //}

        //private void _ButtonSrzMinus_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_TextBoxSrz_5.IsEnabled)
        //    {
        //        _TextBoxSrz_5.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSrz_5.IsEnabled = false;
        //    }
        //    else if (_TextBoxSrz_4.IsEnabled)
        //    {
        //        _TextBoxSrz_4.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSrz_4.IsEnabled = false;
        //    }
        //    else if (_TextBoxSrz_3.IsEnabled)
        //    {
        //        _TextBoxSrz_3.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSrz_3.IsEnabled = false;
        //    }
        //    else if (_TextBoxSrz_2.IsEnabled)
        //    {
        //        _TextBoxSrz_2.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSrz_2.IsEnabled = false;
        //    }
        //    else if (_TextBoxSrz_1.IsEnabled)
        //    {
        //        _TextBoxSrz_1.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSrz_1.IsEnabled = false;
        //    }
        //}

        //private void _ButtonSwzPlus_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!_TextBoxSwz_1.IsEnabled)
        //    {
        //        _TextBoxSwz_1.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSwz_1.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSwz_2.IsEnabled)
        //    {
        //        _TextBoxSwz_2.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSwz_2.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSwz_3.IsEnabled)
        //    {
        //        _TextBoxSwz_3.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSwz_3.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSwz_4.IsEnabled)
        //    {
        //        _TextBoxSwz_4.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSwz_4.IsEnabled = true;
        //    }
        //    else if (!_TextBoxSwz_5.IsEnabled)
        //    {
        //        _TextBoxSwz_5.Visibility = System.Windows.Visibility.Visible;
        //        _TextBoxSwz_5.IsEnabled = true;
        //    }
        //}

        //private void _ButtonSwzMinus_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_TextBoxSwz_5.IsEnabled)
        //    {
        //        _TextBoxSwz_5.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSwz_5.IsEnabled = false;
        //    }
        //    else if (_TextBoxSwz_4.IsEnabled)
        //    {
        //        _TextBoxSwz_4.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSwz_4.IsEnabled = false;
        //    }
        //    else if (_TextBoxSwz_3.IsEnabled)
        //    {
        //        _TextBoxSwz_3.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSwz_3.IsEnabled = false;
        //    }
        //    else if (_TextBoxSwz_2.IsEnabled)
        //    {
        //        _TextBoxSwz_2.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSwz_2.IsEnabled = false;
        //    }
        //    else if (_TextBoxSwz_1.IsEnabled)
        //    {
        //        _TextBoxSwz_1.Visibility = System.Windows.Visibility.Hidden;
        //        _TextBoxSwz_1.IsEnabled = false;
        //    }
        //}
    }
}
