using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using ChatBotLibrary;
using ChatBotLibrary.Core;  // Make sure this matches your class library namespace
//repo test
namespace POEWinForms
{
    public partial class MainForms : Form
    {
        private ChatBotEngine _chatBotEngine;
        private Greeting _greeting;
        private AudioPlayer _audioPlayer;
        private ChatBotLibrary.Image _asciiImage;

        public MainForms()
        {
            InitializeComponent();

            // Initialize chatbot components
            var library = new Library();
            var memory = new UserMemory();
            var quiz = new QuizManager();
            var tasks = new TaskManager();

            _chatBotEngine = new ChatBotEngine(library, quiz, tasks, memory);
            _greeting = new Greeting();
            _audioPlayer = new AudioPlayer();
            _asciiImage = new ChatBotLibrary.Image();

            // Hook form load event
            this.Load += MainForms_Load;
        }

        private void MainForms_Load(object sender, EventArgs e)
        {
            // Play greeting audio
            _audioPlayer.Play();

            // Show ASCII art in the conversation box
            string asciiArt = _asciiImage.GetAsciiArtString();
            rtbConversation.AppendText(asciiArt + Environment.NewLine);

            // Ask for the user name
            string name = Interaction.InputBox("What is your name?", "CyberBot Greeting", "User");
            if (!string.IsNullOrWhiteSpace(name))
            {
                _greeting.SetUserName(name);
                rtbConversation.AppendText($"Bot: Nice to meet you, {name}!{Environment.NewLine}");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtUserInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            rtbConversation.AppendText($"You: {userInput}{Environment.NewLine}");

            string response = _chatBotEngine.ProcessInput(userInput);
            rtbConversation.AppendText($"Bot: {response}{Environment.NewLine}");

            txtUserInput.Clear();
        }
    }
}