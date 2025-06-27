using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotLibrary.Core
{
    
        
        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsCompleted { get; set; } = false;

            public DateTime? ReminderDate { get; set; } // nullable DateTime for optional reminder
        }
    }




