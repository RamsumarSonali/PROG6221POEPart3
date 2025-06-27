using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotLibrary
{
    public class AudioPlayer
    {
        public void Play()
        {
            string filePath = "C:\\Users\\lab_services_student\\Desktop\\ST10029895_PROG6211_Part2\\PROG6221POE\\ChatBotLibrary\\files\\Bot Greeting.wav"; // Ensure the file is in the correct directory

            if (System.IO.File.Exists(filePath))
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync(); // Use Play() for async playback
                }
            }
            else
            {
                Console.WriteLine("Error: File not found at " + filePath);
            }
        }
    }
}