using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotLibrary.Core
{

    public class TaskManager
    {

        private List<TaskItem> tasks = new();

        public void AddTask(TaskItem task)
        {
            tasks.Add(task);
        }

        public List<TaskItem> GetAllTasks() => tasks;

        public TaskItem? GetTaskByTitle(string title)
        {
            return tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public void SetReminder(string title, DateTime reminderDate)
        {
            var task = GetTaskByTitle(title);
            if (task != null)
            {
                task.ReminderDate = reminderDate;
            }
        }
    }
}
    


