using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class AutomaticAcceptanceController
    {
        public static void CheckTasks()
        {
            if (!WasTodayCheckedTasks())
            {
                var list = RequestController.GetOpenRequests();
                var date = SetAcceptanceDate();
                foreach (var request in list)
                {
                    if (DateTime.Compare(date, request.CreationTime) > 0)
                    {
                        RequestController.AcceptRequest(request);
                        MailController.SendNotification(request);
                    }
                }
                UpdateAcceptanceDate();
            }            
        }
        private static DateTime SetAcceptanceDate()
        {
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                return DateTime.Today.AddDays(-4);
            if (DateTime.Today.DayOfWeek == DayOfWeek.Tuesday)
                return DateTime.Today.AddDays(-3);
            return DateTime.Today.AddDays(-2);
        }
        private static bool WasTodayCheckedTasks()
        {
            var context = new DatabaseConnectionContext();
            var date = context.AcceptanceDates.FirstOrDefault();
            if (date == null)
            {
                AddAcceptanceDate();
                return false;
            }
            return DateTime.Compare(DateTime.Today, date.DateTime) == 0;
        }
        private static void UpdateAcceptanceDate()
        {
            var context = new DatabaseConnectionContext();
            var date = context.AcceptanceDates.FirstOrDefault();
            date.DateTime = DateTime.Today;
            context.Update(date);
            context.SaveChanges();
        }
        private static void AddAcceptanceDate()
        {
            var context = new DatabaseConnectionContext();
            var date = new AcceptanceDate() { DateTime = DateTime.Today };
            context.Add(date);
            context.SaveChanges();
        }
    }
}
