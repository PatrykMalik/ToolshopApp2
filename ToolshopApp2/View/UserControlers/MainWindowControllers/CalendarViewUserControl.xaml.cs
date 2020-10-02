using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ToolshopApp2.Controllers;

namespace ToolshopApp2.View.UserControlers.MainWindowControllers
{
    /// <summary>
    /// Interaction logic for CalendarViewUserControl.xaml
    /// </summary>
    public partial class CalendarViewUserControl : UserControl
    {
        public CalendarViewUserControl()
        {
            InitializeComponent();
            InitializeCalendar();
            SetDatesInLabels(int.Parse(_Textbox_Week.Text), int.Parse(_Textbox_Year_Selected.Text));
        }

        private void _DataGridAllRequestsMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            var row = dg.SelectedItem;
            if (row != null)
            {
                TaskWindowController.InitializeTaskWindow(row);
            }
        }

        private void SetDatesInLabels (int wk_selected, int year_selected)
        {
            DateTime data = new DateTime(year_selected, 1, 1);
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (weekNum == 1)
                weekNum = (wk_selected - 1) * 7;
            else
                weekNum = wk_selected * 7;
            data = data.AddDays(weekNum);

            switch (data.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    L_Pon.Content = data.ToString("D");
                    L_Wto.Content = data.AddDays(1).ToString("D");
                    L_Sro.Content = data.AddDays(2).ToString("D");
                    L_Czw.Content = data.AddDays(3).ToString("D");
                    L_Ptk.Content = data.AddDays(4).ToString("D");
                    break;
                case DayOfWeek.Tuesday:
                    L_Pon.Content = data.AddDays(-1).ToString("D");
                    L_Wto.Content = data.AddDays(0).ToString("D");
                    L_Sro.Content = data.AddDays(1).ToString("D");
                    L_Czw.Content = data.AddDays(2).ToString("D");
                    L_Ptk.Content = data.AddDays(3).ToString("D");
                    break;
                case DayOfWeek.Wednesday:
                    L_Pon.Content = data.AddDays(-2).ToString("D");
                    L_Wto.Content = data.AddDays(-1).ToString("D");
                    L_Sro.Content = data.AddDays(0).ToString("D");
                    L_Czw.Content = data.AddDays(1).ToString("D");
                    L_Ptk.Content = data.AddDays(2).ToString("D");
                    break;
                case DayOfWeek.Thursday:
                    L_Pon.Content = data.AddDays(-3).ToString("D");
                    L_Wto.Content = data.AddDays(-2).ToString("D");
                    L_Sro.Content = data.AddDays(-1).ToString("D");
                    L_Czw.Content = data.AddDays(0).ToString("D");
                    L_Ptk.Content = data.AddDays(1).ToString("D");
                    break;
                case DayOfWeek.Friday:
                    L_Pon.Content = data.AddDays(-4).ToString("D");
                    L_Wto.Content = data.AddDays(-3).ToString("D");
                    L_Sro.Content = data.AddDays(-2).ToString("D");
                    L_Czw.Content = data.AddDays(-1).ToString("D");
                    L_Ptk.Content = data.AddDays(0).ToString("D");
                    break;
                case DayOfWeek.Saturday:
                    L_Pon.Content = data.AddDays(-5).ToString("D");
                    L_Wto.Content = data.AddDays(-4).ToString("D");
                    L_Sro.Content = data.AddDays(-3).ToString("D");
                    L_Czw.Content = data.AddDays(-2).ToString("D");
                    L_Ptk.Content = data.AddDays(-1).ToString("D");
                    break;
                case DayOfWeek.Sunday:
                    L_Pon.Content = data.AddDays(-6).ToString("D");
                    L_Wto.Content = data.AddDays(-5).ToString("D");
                    L_Sro.Content = data.AddDays(-4).ToString("D");
                    L_Czw.Content = data.AddDays(-3).ToString("D");
                    L_Ptk.Content = data.AddDays(-2).ToString("D");
                    break;
            }
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Pon.Content.ToString())))
            {
                _MondayCheckbox.IsChecked = true;
                if (!UserController.IsUserAdministartor())
                {
                    _MondayCheckbox.IsEnabled = false;
                }
            }
            else
            {
                _MondayCheckbox.IsChecked = false;
            }
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Wto.Content.ToString())))
            {
                _TuesdayCheckbox.IsChecked = true;
                if (!UserController.IsUserAdministartor())
                {
                    _TuesdayCheckbox.IsEnabled = false;
                }
            }
            else
            {
                _TuesdayCheckbox.IsChecked = false;
            }
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Sro.Content.ToString())))
            {
                _WednesdayCheckbox.IsChecked = true;
                if (!UserController.IsUserAdministartor())
                {
                    _WednesdayCheckbox.IsEnabled = false;
                }
            }
            else
            {
                _WednesdayCheckbox.IsChecked = false;
            }
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Czw.Content.ToString())))
            {
                _ThursdayCheckbox.IsChecked = true;
                if (!UserController.IsUserAdministartor())
                {
                    _ThursdayCheckbox.IsEnabled = false;
                }
            }
            else
            {
                _ThursdayCheckbox.IsChecked = false;
            }
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Ptk.Content.ToString())))
            {
                _FridayCheckbox.IsChecked = true;
                if (!UserController.IsUserAdministartor())
                {
                    _FridayCheckbox.IsEnabled = false;
                }
            }
            else
            {
                _FridayCheckbox.IsChecked = false;
            }
        }

        private void InitializeCalendar()
        {
            DateTime data = DateTime.Now;
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            _Textbox_Year_Selected.Text = data.Year.ToString();
            _Textbox_Week.Text = weekNum.ToString();
            ciCurr.Calendar.GetDayOfWeek(data);
            if (UserController.IsUserToolshopMemberOrAdministator())
            {
                _MondayCheckbox.IsEnabled = true;
                _TuesdayCheckbox.IsEnabled = true;
                _WednesdayCheckbox.IsEnabled = true;
                _ThursdayCheckbox.IsEnabled = true;
                _FridayCheckbox.IsEnabled = true;
            }
        }

        private void _Button_Wk_down_Click(object sender, RoutedEventArgs e)
        {
            if (Only_num_in_tb(_Textbox_Week.Text) && Only_num_in_tb(_Textbox_Year_Selected.Text))
            {
                int wk;
                wk = Int32.Parse(_Textbox_Week.Text);
                if (wk > 1)
                    wk--;
                else
                {
                    _Textbox_Year_Selected.Text = (int.Parse(_Textbox_Year_Selected.Text) - 1).ToString();
                    wk = How_many_wk_in_year(int.Parse(_Textbox_Year_Selected.Text));
                }
                _Textbox_Week.Text = wk.ToString();
            }
        }

        private bool Only_num_in_tb(string tekst)
        {
            if (tekst == String.Empty)
                return false;
            else
            {
                foreach (char c in tekst)
                    if (!Char.IsNumber(c))
                        return false;
            }
            return true;
        }

        private int How_many_wk_in_year(int year)
        {
            DateTime data = new DateTime(year, 12, 31);
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) - 1;
                case DayOfWeek.Tuesday:
                    return ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) - 1;
                case DayOfWeek.Wednesday:
                    return ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) - 1;
                default:
                    return ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }

        }

        private void _Button_bt_actual_wk_Click(object sender, RoutedEventArgs e)
        {
            DateTime data = DateTime.Now;
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(data, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            _Textbox_Year_Selected.Text = data.Year.ToString();
            _Textbox_Week.Text = weekNum.ToString();
        }

        private bool OnlyNumInString(string text)
        {
            foreach (char c in text)
                if (!Char.IsNumber(c))
                    return false;
            return true;
        }

        private void _Textbox_Week_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            _Textbox_Week.SelectionStart = _Textbox_Week.Text.Length;
            _Textbox_Week.SelectionLength = 0;
            if (OnlyNumInString(_Textbox_Week.Text) && OnlyNumInString(_Textbox_Year_Selected.Text) && int.Parse(_Textbox_Week.Text) > 0 && int.Parse(_Textbox_Week.Text) <= How_many_wk_in_year(int.Parse(_Textbox_Year_Selected.Text)) && int.Parse(_Textbox_Year_Selected.Text) > 2018 && int.Parse(_Textbox_Year_Selected.Text) < 2099)
            {
                SetDatesInLabels(int.Parse(_Textbox_Week.Text), int.Parse(_Textbox_Year_Selected.Text));

                _DataGridMonday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Pon.Content.ToString()));
                _DataGridTuesday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Wto.Content.ToString()));
                _DataGridWednesday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Sro.Content.ToString()));
                _DataGridThursday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Czw.Content.ToString()));
                _DataGridFriday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Ptk.Content.ToString()));
            }
        }

        private void _Button_wk_up_Click(object sender, RoutedEventArgs e)
        {
            if (Only_num_in_tb(_Textbox_Week.Text) && Only_num_in_tb(_Textbox_Year_Selected.Text))
            {
                int wk;
                wk = Int32.Parse(_Textbox_Week.Text);
                if (wk < How_many_wk_in_year(int.Parse(_Textbox_Year_Selected.Text)))
                    wk++;
                else
                {
                    wk = 1;
                    _Textbox_Year_Selected.Text = (int.Parse(_Textbox_Year_Selected.Text) + 1).ToString();
                }
                _Textbox_Week.Text = wk.ToString();
            }
        }

        private void BlockDateIfTrue(bool block, DateTime date)
        {
            if (block)
            {
                DateManagingController.BlockSelectedDate(date);
            }
            else if(!block && UserController.IsUserAdministartor())
            {
                DateManagingController.UnblockSelectedDate(date);
            }
        }

        private void _MondayCheckbox_Click(object sender, RoutedEventArgs e)
        {
            BlockDateIfTrue(_MondayCheckbox.IsChecked.Value, DateTime.Parse(L_Pon.Content.ToString()));
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Pon.Content.ToString())) && !UserController.IsUserAdministartor())
            {
                _MondayCheckbox.IsEnabled = false;
            }
        }

        private void _TuesdayCheckbox_Click(object sender, RoutedEventArgs e)
        {
            BlockDateIfTrue(_TuesdayCheckbox.IsChecked.Value, DateTime.Parse(L_Wto.Content.ToString()));
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Wto.Content.ToString())) && !UserController.IsUserAdministartor())
            {
                _TuesdayCheckbox.IsEnabled = false;
            }
        }

        private void _WednesdayCheckbox_Click(object sender, RoutedEventArgs e)
        {
            BlockDateIfTrue(_WednesdayCheckbox.IsChecked.Value, DateTime.Parse(L_Sro.Content.ToString()));
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Sro.Content.ToString())) && !UserController.IsUserAdministartor())
            {
                _WednesdayCheckbox.IsEnabled = false;
            }
        }

        private void _ThursdayCheckbox_Click(object sender, RoutedEventArgs e)
        {
            BlockDateIfTrue(_ThursdayCheckbox.IsChecked.Value, DateTime.Parse(L_Czw.Content.ToString()));
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Czw.Content.ToString())) && !UserController.IsUserAdministartor())
            {
                _ThursdayCheckbox.IsEnabled = false;
            }
        }

        private void _FridayCheckbox_Click(object sender, RoutedEventArgs e)
        {
            BlockDateIfTrue(_FridayCheckbox.IsChecked.Value, DateTime.Parse(L_Ptk.Content.ToString()));
            if (DateManagingController.IsBlockedDateByToolshop(DateTime.Parse(L_Ptk.Content.ToString())) && !UserController.IsUserAdministartor())
            {
                _FridayCheckbox.IsEnabled = false;
            }
        }

        private void _Button_Refresh_Callendar_Click(object sender, RoutedEventArgs e)
        {
            _DataGridMonday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Pon.Content.ToString()));
            _DataGridTuesday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Wto.Content.ToString()));
            _DataGridWednesday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Sro.Content.ToString()));
            _DataGridThursday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Czw.Content.ToString()));
            _DataGridFriday.ItemsSource = RequestController.GetRequests(DateTime.Parse(L_Ptk.Content.ToString()));
        }
    }
}
