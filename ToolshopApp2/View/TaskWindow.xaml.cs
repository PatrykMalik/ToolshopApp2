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
using System.Windows.Shapes;
using ToolshopApp2.Controllers;

namespace ToolshopApp2.View
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 
    public partial class TaskWindow : Window
    {
        public static TaskWindow task;
        
        public TaskWindow()
        {
            InitializeComponent();
            task = this;
            if (_ShipmentTaskUserControl.IsInitialized)
            {
                _ShipmentTaskUserControl.Visibility = Visibility.Hidden;
                _ShipmentTaskUserControl.Width = 0;
            }
            if (!(UserController.IsUserToolshopMember() || UserController.IsUserAdministartor()))
            {
                _ToolshopPartUserControl.Visibility = Visibility.Hidden;
                _ToolshopPartUserControl.Height = 0;
            }
            this.ShowDialog();
        }
    }
}
