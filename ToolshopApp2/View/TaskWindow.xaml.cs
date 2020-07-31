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
        }

        private void _TaskWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _ColumnSecond.Width = GridLength.Auto;
            _RowSecond.Height = GridLength.Auto;
            _RowThird.Height = GridLength.Auto;
            _RowFourth.Height = GridLength.Auto;
            _SimpleTaskUserControl.Height = task.Height - _TaskControlersUserControl.Height - _CommentPartUserControl.Height - _ToolshopPartUserControl.Height;           
        }
    }
}
