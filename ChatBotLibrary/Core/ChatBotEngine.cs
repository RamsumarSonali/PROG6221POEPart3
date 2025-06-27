using System;
//using POEChatbox.Core;

namespace ChatBotLibrary.Core
{
    public class ChatBotEngine
    {
        private readonly Library library;
        private readonly QuizManager quizManager;
        private readonly TaskManager taskManager;
        private readonly UserMemory memory;

        public ChatBotEngine(Library lib, QuizManager quiz, TaskManager tasks, UserMemory userMemory)
        {
            library = lib;
            quizManager = quiz;
            taskManager = tasks;
            memory = userMemory;

            // Link UserMemory to Library for personalized responses
            library.Memory = memory;
        }

        // This method processes user input and returns the chatbot response
        public string ProcessInput(string input)
        {
            input = input.ToLower().Trim();

            // 1. Handle quiz-related input
            if (input == "quiz" || quizManager.IsQuizActive)
            {
                return quizManager.HandleQuiz(input, memory.UserName);
            }

            // 2. Handle reminder confirmation (yes/no)
            if (memory.WaitingForReminderResponse)
            {
                if (input == "yes")
                {
                    memory.WaitingForReminderResponse = false;
                    memory.Interest = "awaiting reminder date";
                    return $"When would you like me to remind you about '{memory.LastAddedTaskTitle}'? Please specify a date (e.g., 2025-07-01):";
                }
                else if (input == "no")
                {
                    memory.WaitingForReminderResponse = false;
                    memory.LastAddedTaskTitle = "";
                    return "No reminder set.";
                }
                else
                {
                    return "Please answer 'yes' or 'no' to set a reminder.";
                }
            }

            // 3. Handle reminder date input
            if (memory.Interest == "awaiting reminder date")
            {
                if (DateTime.TryParse(input, out DateTime reminderDate))
                {
                    taskManager.SetReminder(memory.LastAddedTaskTitle, reminderDate);
                    string msg = $"Reminder set for '{memory.LastAddedTaskTitle}' on {reminderDate.ToShortDateString()}.";
                    memory.LastAddedTaskTitle = "";
                    memory.Interest = "";
                    return msg;
                }
                else
                {
                    return "Sorry, I couldn't understand the date. Please enter it in YYYY-MM-DD format.";
                }
            }

            // 4. Add task commands
            if (input.StartsWith("add task") || input.StartsWith("remind me to"))
            {
                string taskTitle = ExtractTaskTitle(input);
                var task = new TaskItem { Title = taskTitle, Description = "User-defined task" };
                taskManager.AddTask(task);

                memory.LastAddedTaskTitle = taskTitle;
                memory.WaitingForReminderResponse = true;

                return $"Task added: '{taskTitle}'. Would you like to set a reminder? (yes/no)";
            }

            // 5. Default: try to get a chatbot response from the library
            string response = library.GetResponse(input);
            if (!string.IsNullOrEmpty(response))
                return response;

            // 6. Fallback message
            return "I'm here to help with cybersecurity tasks, reminders, or quizzes.";
        }

        // Helper method to extract task title from input string
        private string ExtractTaskTitle(string input)
        {
            if (input.Contains("to "))
                return input.Substring(input.IndexOf("to ") + 3).Trim();

            if (input.StartsWith("add task "))
                return input.Substring("add task ".Length).Trim();

            return "Unnamed Task";
        }
    }
}