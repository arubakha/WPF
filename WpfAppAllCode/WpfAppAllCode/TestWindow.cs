using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfAppAllCode
{
    public class TestWindow : Window
    {
        static int counter = 1;
        public TestWindow(string title, int height, int width)
        {
            if (counter > 1)
                this.Title = title + " " + counter.ToString();
            else
                this.Title = title;
            counter++;

            this.Height = height;
            this.Width = width;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Closing += (s, e) =>
            {
            };

            this.Closed += (s, e) =>
            {
            };

            this.MouseMove += (s, e) =>
            {
                this.Title = e.GetPosition(this).ToString();
            };

            this.KeyDown += (s, e) =>
            {
                this.Title = e.Key.ToString();
            };

            Button btnExit = new Button();
            btnExit.Content = "Exit App";
            btnExit.Height = 25;
            btnExit.Width = 100;

            this.Content = btnExit;
        }
    }
}
