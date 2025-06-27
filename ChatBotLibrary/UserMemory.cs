namespace ChatBotLibrary
{
    // Make this public so it can be used in public classes/methods
    public class UserMemory
    {
        public string UserName { get; set; } = "User";
        public string Interest { get; set; } = "";
        public string Mood { get; set; } = "";

        public bool WaitingForReminderResponse { get; set; } = false;
        public string LastAddedTaskTitle { get; set; } = "";

        public void UpdateMood(string input)
        {
            if (input.Contains("worried") || input.Contains("scared"))
                Mood = "worried";
            else if (input.Contains("curious"))
                Mood = "curious";
            else if (input.Contains("frustrated"))
                Mood = "frustrated";
            else
                Mood = "";
        }
    }
}
