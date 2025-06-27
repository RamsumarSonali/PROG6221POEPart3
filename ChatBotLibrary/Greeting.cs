using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using POEChatbox;

namespace ChatBotLibrary
{
    public class Greeting
    {
        public string UserName { get; set; } = "";
        public bool UseGreeted { get; set; } = false;

        // Returns the initial greeting prompt (without reading input)
        public string GetGreetingPrompt()
        {
            return "🔐 Hello, I am CyberBot, what is your name?";
        }

        // Accept the username from GUI and return the personalized response
        public string SetUserName(string userName)
        {
            UserName = string.IsNullOrWhiteSpace(userName) ? "User" : userName;
            UseGreeted = true;

            return $"It's nice to meet you, {UserName}.";


        }
    }
}
    
