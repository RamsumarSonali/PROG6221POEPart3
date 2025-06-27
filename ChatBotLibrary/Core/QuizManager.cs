using System;
using System.Collections.Generic;
using System.IO;

 namespace ChatBotLibrary.Core
{
    public class QuizManager
    {
        private bool quizActive = false;
        private int currentQuestionIndex = 0;
        private int score = 0;

        private readonly List<QuizQuestion> questions;

        public bool IsQuizActive => quizActive;

        public QuizManager()
        {
            // Initialize quiz questions here or load from file
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What does 'phishing' mean?",
                    Options = new List<string> { "A fishing technique", "An online scam", "A type of virus", "A password" },
                    CorrectOptionIndex = 1
                },
                new QuizQuestion
                {
                    Question = "What should you do if you receive a suspicious email?",
                    Options = new List<string> { "Click the link", "Ignore it", "Report it", "Reply to ask for details" },
                    CorrectOptionIndex = 2
                },
                new QuizQuestion
                {
                    Question = "Which is the safest way to manage passwords?",
                    Options = new List<string> { "Write them on paper", "Use a password manager", "Use the same password everywhere", "Share with friends" },
                    CorrectOptionIndex = 1
                }
                // Add more questions if you want
            };
        }

        public string HandleQuiz(string input, string userName)
        {
            input = input.Trim().ToLower();

            if (!quizActive)
            {
                if (input == "quiz")
                {
                    quizActive = true;
                    currentQuestionIndex = 0;
                    score = 0;
                    return $"Starting the cybersecurity quiz, {userName}!\n" + GetCurrentQuestionText();
                }
                else
                {
                    return "Type 'quiz' to start the cybersecurity quiz!";
                }
            }
            else
            {
                if (input == "exit" || input == "quit")
                {
                    quizActive = false;
                    return $"Quiz exited. Your final score was {score} out of {questions.Count}.";
                }

                // Validate answer: expecting option like "1", "2", "3", or "a", "b", "c"
                int selectedOption = -1;

                if (int.TryParse(input, out int numericChoice))
                {
                    selectedOption = numericChoice - 1; // User options shown as 1-based, index is 0-based
                }
                else if (input.Length == 1)
                {
                    // If input is a letter a/b/c/d, convert to index
                    char c = input[0];
                    if (c >= 'a' && c < 'a' + questions[currentQuestionIndex].Options.Count)
                    {
                        selectedOption = c - 'a';
                    }
                }

                if (selectedOption < 0 || selectedOption >= questions[currentQuestionIndex].Options.Count)
                {
                    return "Please answer by typing the option number or letter (e.g., 1 or a). Type 'exit' to quit the quiz.";
                }

                // Check answer correctness
                bool isCorrect = selectedOption == questions[currentQuestionIndex].CorrectOptionIndex;
                if (isCorrect) score++;

                string feedback = isCorrect ? "Correct! 🎉" : $"Wrong! The correct answer was: {GetCorrectAnswerText()}";

                currentQuestionIndex++;

                if (currentQuestionIndex >= questions.Count)
                {
                    quizActive = false;
                    return $"{feedback}\n\nQuiz complete! Your final score: {score} out of {questions.Count}. Thanks for playing!";
                }
                else
                {
                    return $"{feedback}\n\nNext question:\n{GetCurrentQuestionText()}";
                }
            }
        }

        private string GetCurrentQuestionText()
        {
            var q = questions[currentQuestionIndex];
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(q.Question);

            for (int i = 0; i < q.Options.Count; i++)
            {
                // Show options as letters a), b), c)...
                char optionLetter = (char)('a' + i);
                sb.AppendLine($"{optionLetter}) {q.Options[i]}");
            }

            sb.AppendLine("Please type the letter (a, b, c, etc.) or number (1, 2, 3, etc.) of your answer.");
            sb.AppendLine("Or type 'exit' to quit the quiz.");

            return sb.ToString();
        }

        private string GetCorrectAnswerText()
        {
            var q = questions[currentQuestionIndex];
            return q.Options[q.CorrectOptionIndex];
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
    }
}