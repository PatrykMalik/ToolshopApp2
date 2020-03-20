using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class ComboboxListController
    {
        public static void AddTask(string s)
        {
            var context = new DatabaseConnectionContext();
            var taskList = new TaskList()
            {
                Name = s
            };
            context.Add(taskList);
            context.SaveChanges();
        }
        public static void AddClassyfy(string s)
        {
            var context = new DatabaseConnectionContext();
            var classyfyList = new ClassyfyList()
            {
                Name = s
            };
            context.Add(classyfyList);
            context.SaveChanges();

        }
        public static void AddProject(string s)
        {
            var context = new DatabaseConnectionContext();
            var projectList = new ProjectList()
            {
                Name = s
            };
            context.Add(projectList);
            context.SaveChanges();
        }
        public static void AddCostCenter(string s)
        {
            var context = new DatabaseConnectionContext();
            var costCenter = new CostCenterList()
            {
                Name = s
            };
            context.Add(costCenter);
            context.SaveChanges();
        }

        public static IEnumerable<TaskList> GetTaskLists()
        {
            var context = new DatabaseConnectionContext();
            List<TaskList> taskList = new List<TaskList>();
            foreach (TaskList task in context.TaskLists)
            {
                taskList.Add(task);
            }
            return taskList;
        }
        public static IEnumerable<ClassyfyList> GetClassyfyLists()
        {
            var context = new DatabaseConnectionContext();
            List<ClassyfyList> taskList = new List<ClassyfyList>();
            foreach (var task in context.ClassyfyLists)
            {
                taskList.Add(task);
            }
            return taskList;
        }
        public static IEnumerable<ProjectList> GetProjectLists()
        {
            var context = new DatabaseConnectionContext();
            List<ProjectList> taskList = new List<ProjectList>();
            foreach (var task in context.ProjectLists)
            {
                taskList.Add(task);
            }
            return taskList;
        }
        public static IEnumerable<CostCenterList> GetCostCenterLists()
        {
            var context = new DatabaseConnectionContext();
            List<CostCenterList> taskList = new List<CostCenterList>();
            foreach (var task in context.CostCenterLists)
            {
                taskList.Add(task);
            }
            return taskList;
        }
        public static IEnumerable<Address> GetAddresses()
        {
            var context = new DatabaseConnectionContext();
            List<Address> addresses = new List<Address>();
            foreach (var address in context.Addresses)
            {
                addresses.Add(address);
            }
            return addresses;
        }
        public static Address GetAddress(string contactPerson)
        {
            var context = new DatabaseConnectionContext();
            var address = context.Addresses.Where(x => x.ContactPerson == contactPerson).First();
            return address;
        }

    }
}
