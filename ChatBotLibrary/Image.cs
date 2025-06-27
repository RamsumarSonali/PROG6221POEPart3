using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;

namespace ChatBotLibrary
{
    public class Image
    {
        private readonly string imagePath = @"C:\Users\lab_services_student\Desktop\ST10029895_PROG6211_Part2\PROG6221POE\ChatBotLibrary\files\image.png";
        private readonly int width = 60;
        private readonly int height = 30;

        public string GetAsciiArtString()
        {
            StringBuilder sb = new StringBuilder();

            using (Bitmap image = new Bitmap(imagePath))
            using (Bitmap resized = new Bitmap(image, new Size(width, height)))
            {
                string asciiChars = "@%#*+=-:. ";

                for (int y = 0; y < resized.Height; y++)
                {
                    for (int x = 0; x < resized.Width; x++)
                    {
                        Color pixelColor = resized.GetPixel(x, y);
                        int grayValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        int index = grayValue * (asciiChars.Length - 1) / 255;
                        sb.Append(asciiChars[index]);
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}
