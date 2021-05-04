using System;
using System.Linq;
using System.Windows;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class DateManagingController
    {
        public static bool IsDateAvaiable(DateTime dateToCheck)
        {
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
        public static bool IsSelectedDateOnWeekend(DateTime dateToCheck)
        {
            if (dateToCheck.DayOfWeek == DayOfWeek.Saturday || dateToCheck.DayOfWeek == DayOfWeek.Sunday)
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to add task to weekend", "Seleded date is for weekend", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static void BlockSelectedDate(DateTime dateToBlock)
        {
            BlockedDay blockedDay = new BlockedDay { blockedDate = dateToBlock };
            var context = new DatabaseConnectionContext();
            context.BlockedDays.Add(blockedDay);
            context.SaveChanges();
        }

        public static void UnblockSelectedDate(DateTime date)
        {
            var context = new DatabaseConnectionContext();
            var unblockedDate = context.BlockedDays.Where(x => x.blockedDate == date).FirstOrDefault();
            if (unblockedDate != null)
            {
                context.BlockedDays.Attach(unblockedDate);
                context.BlockedDays.Remove(unblockedDate);
                context.SaveChanges();
            }
        }

        public static bool IsBlockedDateByToolshop(DateTime dateToCheck)
        {
            var context = new DatabaseConnectionContext();
            return (context.BlockedDays.Where(x => x.blockedDate == dateToCheck).FirstOrDefault() != null) ;
        }

        private static bool IsAvaiableSpaceInSelectedDate(DateTime dateToCheck)
        {
            var context = new DatabaseConnectionContext();
            return (context.Requests.Where(t => t.Date == dateToCheck).Count() < 6);
        }
    }
}

