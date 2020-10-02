using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ToolshopApp2.View;
using ToolshopApp2.Controllers;
using System.Globalization;
using System.Threading;
//using Microsoft.Office.Interop.Excel;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ToolshopApp2.Model;

namespace ToolshopApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;

        public MainWindow()
        {
            try
            {
                AutomaticAcceptanceController.CheckTasks();
                mainWindow = this;
                InitializeComponent();
                if (!UserController.UserExistInDatabase())
                {
                    InitializeWelcomeWindow();
                    if (!UserController.UserExistInDatabase())
                        this.Close();
                }
                SetDateTimeStandard();
                if (UserController.IsUserAdministartor())
                {
                    InitializeAdministratorTab();
                }
                else
                {
                    _TabItemListOfPCDishwashers.Visibility = Visibility.Hidden;
                    _TabItemAdministrator.Visibility = Visibility.Hidden;
                }

                SetComboboxes();
                RefrashDataGrid();
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                    _LabelVersionNumber.Content = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                else
                    _LabelVersionNumber.Content = "0.0.0.9";
            }
            catch (Exception ex)
            {
                LogSingleton.Instance.SaveLog(ex.Message);
                LogSingleton.Instance.SaveLog(ex.ToString());
                LogSingleton.Instance.SaveLog(ex.Source);
            }
        }

        private void SetComboboxes()
        {
            _ComboboxUser.Items.Add("All");
            _ComboboxClassyfy.Items.Add("All");
            _ComboboxOrder.Items.Add("All");
            _ComboboxProject.Items.Add("All");
            _ComboboxCostcenter.Items.Add("All");
            _ComboboxStatus.Items.Add("All");
            _ComboboxStatus.Items.Add("NotClosed");
            _ComboboxStatus.Items.Add("Open");
            _ComboboxStatus.Items.Add("Accepted");
            _ComboboxStatus.Items.Add("Closed");
            DownloadDataToComboboxes();
            if (UserController.IsUserToolshopMemberOrAdministator())
                _ComboboxUser.SelectedIndex = 0;
            else
                _ComboboxUser.SelectedItem = Environment.UserName.ToLower();
            _ComboboxClassyfy.SelectedIndex = 0;
            _ComboboxOrder.SelectedIndex = 0;
            _ComboboxProject.SelectedIndex = 0;
            _ComboboxCostcenter.SelectedIndex = 0;
            _ComboboxStatus.SelectedIndex = 1;
        }

        private void DownloadDataToComboboxes()
        {
            foreach (var item in UserController.GetUsers())
                _ComboboxUser.Items.Add(item.Name);
            foreach (var item in ComboboxListController.GetClassyfyLists())
                _ComboboxClassyfy.Items.Add(item.Name);
            foreach (var item in ComboboxListController.GetTaskLists())
                _ComboboxOrder.Items.Add(item.Name);
            foreach (var item in ComboboxListController.GetProjectLists())
                _ComboboxProject.Items.Add(item.Name);
            foreach (var item in ComboboxListController.GetCostCenterLists())
                _ComboboxCostcenter.Items.Add(item.Name);
        }

        private bool ComplexFilter(object obj)
        {
            Request request = obj as Request;
            if (request != null)
            {
                return (
                    (request.Id.ToString() == _TextboxId.Text || _TextboxId.Text == String.Empty) &&
                    (request.User.Contains(_ComboboxUser.Text) || _ComboboxUser.Text == "All") &&
                    (request.Classyfy == _ComboboxClassyfy.Text || _ComboboxClassyfy.Text == "All") &&
                    (request.Order == _ComboboxOrder.Text || _ComboboxOrder.Text == "All") &&
                    (request.Project == _ComboboxProject.Text || _ComboboxProject.Text == "All") &&
                    (request.CostCenter == _ComboboxCostcenter.Text || _ComboboxCostcenter.Text == "All") &&
                    (request.Status == _ComboboxStatus.Text || _ComboboxStatus.Text == "All" || (_ComboboxStatus.Text == "NotClosed" && request.Status != "Closed")) &&
                    (request.Date > _DatepickerFrom.SelectedDate || _DatepickerFrom.SelectedDate == null) &&
                    (request.Date < _DatepickerTo.SelectedDate || _DatepickerTo.SelectedDate == null));
            }
            return false;
        }

        private void RefrashDataGrid()
        {
            var ObColl = new ObservableCollection<Request>();
            foreach (var model in RequestController.GetRequests())
                ObColl.Add(model);
            var itemSourceTable = new CollectionViewSource() { Source = ObColl };
            ICollectionView ItemList = itemSourceTable.View;
            var filter = new Predicate<object>(ComplexFilter);

            ItemList.Filter = filter;

            _DataGridAllRequests.ItemsSource = ItemList;
        }

        private void InitializeAdministratorTab()
        {
            _AdministratorAddValuesToDatabaseUserControl.Visibility = Visibility.Hidden;
            _AdministratorDataViewUserControl.Visibility = Visibility.Hidden;
        }

        private void _ButtonNewTaskClick(object sender, RoutedEventArgs e)
        {
            TaskWindowController.InitializeTaskWindow();
        }

        private void InitializeWelcomeWindow()
        {
            var welcomeWindow = new WelcomeWindow();
        }

        private void _ButtonAddValuesViewClick(object sender, RoutedEventArgs e)
        {
            InitializeAdministratorTab();
            _AdministratorAddValuesToDatabaseUserControl.Visibility = Visibility.Visible;
        }

        private void _ButtonDataViewClick(object sender, RoutedEventArgs e)
        {
            InitializeAdministratorTab();
            _AdministratorDataViewUserControl.Visibility = Visibility.Visible;
        }

        private void _DataGridAllRequestsMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            var row = dg.SelectedItem;
            if (row != null)
            {
                TaskWindowController.InitializeTaskWindow(row);
                RefrashDataGrid();
            }
        }

        private void SetDateTimeStandard()
        {
            var newCulture = new CultureInfo("pl-PL");
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;

            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        private void _CalendarControlUserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void _ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefrashDataGrid();
        }

        private void _ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            _TextboxId.Text = String.Empty;
            _ComboboxUser.SelectedItem = Environment.UserName.ToLower();
            _ComboboxClassyfy.SelectedIndex = 0;
            _ComboboxOrder.SelectedIndex = 0;
            _ComboboxProject.SelectedIndex = 0;
            _ComboboxCostcenter.SelectedIndex = 0;
            _ComboboxStatus.SelectedIndex = 1;
        }

        private void _ButtonDuplicate_Click(object sender, RoutedEventArgs e)
        {
            var row = _DataGridAllRequests.SelectedItem;
            TaskWindowController.DuplicateTask(row);
        }
    }
}
