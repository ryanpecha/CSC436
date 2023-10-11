using GitVisualizer.UI.UI_Forms;
using SkiaSharp;
using System.Diagnostics;

namespace GitVisualizer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MainForm mainForm = new MainForm();
            Application.Run(mainForm);

            Debug.WriteLine("This is how we debug!");
        }


        private static void DrawSkiaWeb()
        {
            SKBitmap bmp = new(640, 480);
            using SKCanvas canvas = new(bmp);
            canvas.Clear(SKColor.Parse("#003366"));

            Random rand = new(0);
            SKPaint p1 = new() { Color = SKColors.FloralWhite };
            SKPaint p2 = new() { Color = SKColors.Aqua };

            SKPoint prev = new(0, 0);
            for (int i = 0; i < 100; i++)
            {
                SKPoint pot = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
                canvas.DrawCircle(pot, 5, p1);
                canvas.DrawLine(prev, pot, p2);
                prev = pot;
            }

            SKFileWStream fs = new("points.jpg");
            bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 100);
        }
    }
}