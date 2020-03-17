using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class DateManagingController
    {
        public static bool IsDateAvaiable(DateTime dateToCheck)
        {
            if (IsSeletedDateOnWeekend(dateToCheck))
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to add task to weekend", "Seleded date is for weekend", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    return false;
                }
                else
                {
                    return true;
                }                    
            }
            if (IsBlockedDateByToolshop(dateToCheck))
            {
                MessageBox.Show("Please reschedule task.\nSelected date is blocked by toolshop.", "Blocked day", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (!IsAvaiableSpaceInSelectedDate(dateToCheck))
            {
                MessageBox.Show("Please reschedule task.\nSelected date is fully filled in toolshop.", "Full day", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        private static bool IsSeletedDateOnWeekend(DateTime dateToCheck)
        {
            if (dateToCheck.DayOfWeek == DayOfWeek.Saturday || dateToCheck.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }

        private static bool IsBlockedDateByToolshop(DateTime dateToCheck)
        {
            var context = new DatabaseConnectionContext();
            foreach (var date in context.BlockedDays)
            {
                if (dateToCheck == date.blockedDate)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsAvaiableSpaceInSelectedDate(DateTime dateToCheck)
        {
            var context = new DatabaseConnectionContext();
            var ts = context.Requests.Where(t => t.Date == dateToCheck).ToList();
            if (ts == null)
            {
                return true;
            }
            else if (ts.Count < 6)
            {
                return true;
            }
            else
                return false;
        }
    }
}

