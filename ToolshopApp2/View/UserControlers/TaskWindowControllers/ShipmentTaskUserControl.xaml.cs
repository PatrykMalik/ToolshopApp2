using Microsoft.EntityFrameworkCore.Internal;
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
using ToolshopApp2.Model;

namespace ToolshopApp2.View.UserControlers
{
    /// <summary>
    /// Interaction logic for ShipmentTaskUserControl.xaml
    /// </summary>
    public partial class ShipmentTaskUserControl : UserControl
    {
        public ShipmentTaskUserControl()
        {
            InitializeComponent();
        }

        private void _ButtonClearAdress_Click(object sender, RoutedEventArgs e)
        {
            _ComboBoxContactPerson.Text = String.Empty;
            _TextBoxEmail.Text = String.Empty;
            _TextBoxAdress.Text = String.Empty;
        }

        private void _ButtonAddAdress_Click(object sender, RoutedEventArgs e)
        {
            if (TaskWindowController.AddAddress())
            {
                MessageBox.Show("Address has been added successfully", "Address Added", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void _ButtonPorcia_Click(object sender, RoutedEventArgs e)
        {
            _ComboBoxContactPerson.Text = String.Empty;
            _TextBoxEmail.Text = String.Empty;
            _TextBoxAdress.Text = "VIA L. ZANUSSI 24" + Environment.NewLine + "PORCIA" + Environment.NewLine + "33080" + Environment.NewLine + "Italy";
        }

        private void _ButtonSolaro_Click(object sender, RoutedEventArgs e)
        {
            _ComboBoxContactPerson.Text = String.Empty;
            _TextBoxEmail.Text = String.Empty;
            _TextBoxAdress.Text = "Electrolux Italia SPA" + Environment.NewLine + "Corso Europa 63" + Environment.NewLine + "20020 Solaro, (MI)";
        }

        private void _ButtonStockholm_Click(object sender, RoutedEventArgs e)
        {
            _ComboBoxContactPerson.Text = String.Empty;
            _TextBoxEmail.Text = String.Empty;
            _TextBoxAdress.Text = "AB Electrolux" + Environment.NewLine + "S:t Göransgatan 143," + Environment.NewLine + "SE-105 45 Stockholm" + Environment.NewLine + "SWEDEN";
        }

        private void _ComboBoxInsurance_Unchecked(object sender, RoutedEventArgs e)
        {
            _TextBoxInsuranceCost.IsEnabled = false;
            _TextBoxInsuranceCost.Text = String.Empty;
        }

        private void _ComboBoxInsurance_Checked(object sender, RoutedEventArgs e)
        {
            _TextBoxInsuranceCost.IsEnabled = true;
        }

        private void _ComboBoxContactPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_ComboBoxContactPerson.SelectedItem != null)
            {
                var addres = ComboboxListController.GetAddress(_ComboBoxContactPerson.SelectedItem.ToString());
                _TextBoxEmail.Text = addres.Email;
                _TextBoxAdress.Text = addres.Adress;

            }
        }
        private void _TextBoxSrzBegin_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskWindowController.TextboxOnlyNumeric(_TextBoxSrzBegin);
        }

        private void _TextBoxSrzEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskWindowController.TextboxOnlyNumeric(_TextBoxSrzEnd);
        }
        
    }
}
