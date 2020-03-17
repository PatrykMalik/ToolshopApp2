using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolshopApp2.Data;
using ToolshopApp2.Model;

namespace ToolshopApp2.Controllers
{
    public static class TaskListController
    {
        public static void AddTask(string s)
        {
            var context = new DatabaseConnectionContext();
            var taskList = new TaskList()
            {
                Name = s
            };
            context.Add(taskList);
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

    }
}
