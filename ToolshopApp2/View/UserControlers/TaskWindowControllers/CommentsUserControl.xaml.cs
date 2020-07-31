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

namespace ToolshopApp2.View.UserControlers.TaskWindowControllers
{
    /// <summary>
    /// Interaction logic for CommentsUserControl.xaml
    /// </summary>
    public partial class CommentsUserControl : UserControl
    {
        public CommentsUserControl()
        {
            InitializeComponent();
        }

        private void _ButtonAddComment_Click(object sender, RoutedEventArgs e)
        {
            TaskWindowController.AddComment(_TextboxComment.Text);
        }
    }
}
