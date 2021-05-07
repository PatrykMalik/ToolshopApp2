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

namespace ToolshopApp2.View.UserControlers.MainWindowControllers
{
    /// <summary>
    /// Interaction logic for DishwasersOnStockUserControl.xaml
    /// </summary>
    public partial class DishwasersOnStockUserControl : UserControl
    {
        public DishwasersOnStockUserControl()
        {
            InitializeComponent();
            if (UserController.IsUserToolshopMemberOrAdministator())
            {
                ScanPnc.Visibility = Visibility.Visible;
            }
        }

        private void ScanPnc_Click(object sender, RoutedEventArgs e)
        {
            DishwasherOnStockController.OpenAddDishwasherWindow();
        }

        //private void _EditPnc_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void LoadListOfPncs_Click(object sender, RoutedEventArgs e)
        {
            TabOfDishwasers.ItemsSource = DishwasherOnStockController.GetDishwashersOnStocks();
        }

        private void ClearListOfPncs_Click(object sender, RoutedEventArgs e)
        {
            TabOfDishwasers.ItemsSource = null;
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            DishwasherOnStockController.ExportTabToExcel();
        }

        private void TabOfDishwasers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.DataGrid dg = (System.Windows.Controls.DataGrid)sender;
            var row = dg.SelectedItem;
            if (row != null)
            {
                DishwasherOnStockController.OpenEditDishwasherWindow(row);                
            }
        }
    }
}
