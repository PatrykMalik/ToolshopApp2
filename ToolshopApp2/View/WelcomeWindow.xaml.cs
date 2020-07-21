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

namespace ToolshopApp2.View
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public static WelcomeWindow welcomeWindow;
        public WelcomeWindow()
        {
            InitializeComponent();
            welcomeWindow = this;
            SetupLabel();     
        }
        private void SetupLabel()
        {            
            welcomeWindow._LabelWelcome.Content = "Welcome " + Environment.UserName;
            if (!UserController.UserExistInDatabase())
            {
                this._NewUserEmailUserControl._LabelInfo.Content = "You are new user of ToolshopApp. \nPlease add you email to get notifications.";
                this.ShowDialog();
            }
            else
            {
                this._NewUserEmailUserControl.Visibility = Visibility.Hidden;
                this._NewUserEmailUserControl.Height = 0;
                this.Close();
            }    
        }

        
    }
}
