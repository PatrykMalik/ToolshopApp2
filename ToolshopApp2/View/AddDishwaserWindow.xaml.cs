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
    /// Interaction logic for AddDishwaserWindow.xaml
    /// </summary>
    public partial class AddDishwaserWindow : Window
    {
        public AddDishwaserWindow()
        {
            InitializeComponent();
            InitializeSubcomponents();
        }
        private void InitializeSubcomponents()
        {
            ScanDate.SelectedDate = DateTime.Today;
        }
        private void AddDishwaher_Click(object sender, RoutedEventArgs e)
        {
            DishwasherOnStockController.CreateDishwasherOnStockModels(Barcodes.Text, Assigment.Text, ScanDate.Text, BegingSrz.Text, EndingSrz.Text, Comment.Text);
            this.Close();
        }
    }
}
