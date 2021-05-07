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
using System.Windows.Shapes;
using ToolshopApp2.Controllers;
using ToolshopApp2.Model;

namespace ToolshopApp2.View
{
    /// <summary>
    /// Interaction logic for EditDishwaserWindow.xaml
    /// </summary>
    public partial class EditDishwaserWindow : Window
    {
        private DishwashersOnStock _dishwasher;
        public EditDishwaserWindow(DishwashersOnStock dishwasher)
        {
            _dishwasher = dishwasher;
            InitializeComponent();
            if (UserController.IsUserToolshopMemberOrAdministator())
            {
                EditDishwaher.Visibility = Visibility.Visible;
            }
        }

        private void EditDishwaher_Click(object sender, RoutedEventArgs e)
        {
            _dishwasher.Assigment = Assigment.Text;
            _dishwasher.BegingSrz = BegingSrz.Text;
            _dishwasher.EndingSrz = EndingSrz.Text;
            _dishwasher.Comments = Comment.Text;

            DishwasherOnStockController.UpdateDishwasherOnStock(_dishwasher);
            this.Close();
        }
    }
}
