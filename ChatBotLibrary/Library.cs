using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChatBotLibrary
{
    public class Library
    {
         public List<ChatBotData> data = new List<ChatBotData>();

        public Dictionary<string, List<string>> KeywordResponses = new Dictionary<string, List<string>>();

        public UserMemory Memory { get; set; }
        public string CurrentTopic { get; set; }

        private readonly Random random = new Random();

        public void LoadData()
        {
            KeywordResponses.Clear();  // clear previous data to avoid duplicates

            string filePath = "C:\\Users\\lab_services_student\\Desktop\\ST10029895_PROG6211_Part2\\PROG6221POE\\ChatBotLibrary\\files\\chatbot.txt";

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        if (parts[0].Trim().ToLower() == "invalid") continue;

                        var subject = parts[0].Trim();
                        var keywords = parts[1].Split(',').Select(k => k.Trim().ToLower()).ToList();
                        var responses = parts[2].Split('~').Select(r => r.Trim()).ToList();

                        data.Add(new ChatBotData
                        {
                            Subject = subject,
                            Keywords = keywords,
                            Content = string.Join(" ~ ", responses)
                        });

                        foreach (string keyword in keywords)
                        {
                            if (!KeywordResponses.ContainsKey(keyword))
                            {
                                KeywordResponses[keyword] = new List<string>();
                            }

                            foreach (string response in responses)
                            {
                                KeywordResponses[keyword].Add(response);
                            }
                        }
                    }
                }
            }
            else
            {
                // fallback manual data if file not found
                KeywordResponses["password"] = new List<string>
                {
                    "Use a mix of letters, numbers, and symbols.",
                    "Avoid using personal information in your passwords.",
                    "Do not use consecutive numbers or letters."
                };
                KeywordResponses["phishing"] = new List<string>
                {
                    "Be cautious of emails asking for personal information.",
                    "Check the sender's email address carefully.",
                    "Do not click on suspicious links or attachments."
                };
                KeywordResponses["privacy"] = new List<string>
                {
                    "Review your privacy settings on social media.",
                    "Be cautious about sharing personal information online.",
                    "Use strong passwords and enable two-factor authentication."
                };
                KeywordResponses["scam"] = new List<string>
                {
                    "Be wary of offers that seem too good to be true.",
                    "Do not share personal information with unknown people.",
                    "Report suspicious messages to the appropriate authorities."
                };
            }
        }

        public string GetResponse(string input)
        {
            input = input.ToLower();

            // split input into words for exact matching
            var inputWords = input.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var keyword in KeywordResponses.Keys)
            {
                if (inputWords.Contains(keyword))
                {
                    CurrentTopic = keyword;

                    var responses = KeywordResponses[keyword];
                    string baseResponse = responses[random.Next(responses.Count)];

                    if (Memory != null && !string.IsNullOrEmpty(Memory.Interest) && keyword == Memory.Interest)
                    {
                        return $"Since you're interested in {Memory.Interest}, here's something for you: {baseResponse}";
                    }

                    return baseResponse;
                }
            }

            // no keyword matched
            return "";
        }

        public string GetFollowUp(string topic)
        {
            if (KeywordResponses.ContainsKey(topic))
            {
                var responses = KeywordResponses[topic];
                return responses[random.Next(responses.Count)];
            }
            return "I’d love to help, but I’m not sure what topic we’re discussing.";
        }
    }
}