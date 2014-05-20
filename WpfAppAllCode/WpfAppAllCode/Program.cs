using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfAppAllCode
{
    class Program : Application
    {
        [STAThread]
        static void Main(string[] args)
        {
            Program app = new Program();


            app.Startup += OnStartup;

            //app.Startup += new StartupEventHandler(OnStartup);

            //app.Startup += delegate(object s, StartupEventArgs a)
            //{
            //    TestWindow window = new TestWindow("My WPF app", 200, 300);
            //    window.Show();
            //};

            //app.Startup += (s, a) =>
            //{
            //    TestWindow window = new TestWindow("My WPF app", 200, 300);
            //    window.Show();
            //};



            app.Exit += OnExit;
            

            app.Run();
        }

        
        private static void OnStartup(object s, StartupEventArgs a)
        {
            TestWindow window = new TestWindow("My WPF app", 200, 300);
            window.Show();
            window.Focus();
        }

        private static void OnExit(object s, ExitEventArgs a)
        {
            //MessageBox.Show("Exit!");
        }

    }
}
