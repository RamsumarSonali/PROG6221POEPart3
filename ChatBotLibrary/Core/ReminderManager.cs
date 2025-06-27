using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotLibrary.Core
{
    
        public class ReminderManager
        {
        private List<Reminder> reminders = new List<Reminder>();

        public string SetReminder(string taskTitle, string dateInput)
        {
            if (DateTime.TryParse(dateInput, out DateTime reminderDate))
            {
                reminders.Add(new Reminder
                {
                    TaskTitle = taskTitle,
                    ReminderDate = reminderDate
                });

                return $"Reminder set for '{taskTitle}' on {reminderDate.ToShortDateString()}.";
            }
            else
            {
                return "Invalid date format. Please try again using format YYYY-MM-DD (e.g., 2025-07-01).";
            }
        }

        public List<Reminder> GetAllReminders()
        {
            return reminders;
        }
    }

    // ✅ Make this class public to avoid accessibility error
    public class Reminder
    {
        public string TaskTitle { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}


