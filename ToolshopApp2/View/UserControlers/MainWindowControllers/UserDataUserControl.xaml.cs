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
    /// Interaction logic for UserDataUserControl.xaml
    /// </summary>
    public partial class UserDataUserControl : UserControl
    {
        public UserDataUserControl()
        {
            InitializeComponent();
            FillData();
        }
        private void FillData()
        {
            var user = UserController.GetUser(Environment.UserName.ToLower());
            var userType = UserController.GetKindOfUsers();
            _UserNameLabel.Content = user.Name;
            _UserTypeLabel.Content = userType.Where(x => x.Id == user.KindOfUserId).FirstOrDefault().TypeOfUser;
            _UserEmailTextbox.Text = user.Emial;
        }

        private void _UpdateUserDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserController.UpdateUser(_UserEmailTextbox.Text))
            {
                MessageBox.Show("Your data was updated correctly.", "Updated user data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Your data wasn't updated correctly. \nTry again or contavt with administrator.","Update data Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
