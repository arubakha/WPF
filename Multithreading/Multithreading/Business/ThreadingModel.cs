using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Multithreading.Application;

namespace Multithreading.Business
{
    class ThreadingModel
    {
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private int moduleLevel = 5000;
        private static int staticLevel = 888000;
        private string runningName;

        #region RaiseThreadStatusChanged
        public delegate void ThreadStatusChangedDelegate(ThreadingModel caller, ThreadStatusEventArgs args);
        public event ThreadStatusChangedDelegate ThreadStatusChanged;
        private void RaiseThreadStatusChanged(object value) 
        {
            if (ThreadStatusChanged != null)
            {
                //ThreadStatusChanged(this, new ThreadStatusEventArgs(value));
                //App.Current.Dispatcher.BeginInvoke(new Action(() => { ThreadStatusChanged(this, new ThreadStatusEventArgs(value)); }), null);
                App.Current.Dispatcher.BeginInvoke(ThreadStatusChanged, this, new ThreadStatusEventArgs(value));
            }
        }
        #endregion

        internal void StartConsequtiveTasks()
        {
            Task.Factory.StartNew(t =>
            {
                RaiseThreadStatusChanged(5);
            }
            , null)
            .ContinueWith(t => Thread.Sleep(2000))
            .ContinueWith(t =>
            {
                RaiseThreadStatusChanged(744);
            })
            .ContinueWith(t => Thread.Sleep(2000))
            .ContinueWith(t =>
            {
                RaiseThreadStatusChanged(81);
            });
        }

        public void StartWithExtensionMethodPost()
        {
            RaiseThreadStatusChanged(25);
            SynchronizationContext.Current.Post(TimeSpan.FromSeconds(2), () => RaiseThreadStatusChanged(632));
            SynchronizationContext.Current.Post(TimeSpan.FromSeconds(2), () => RaiseThreadStatusChanged(88899));
        }

        internal void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread th = new Thread(ThreadStarterName);
                th.Name = string.Format("Thread{0}", i);
                th.Start();
            }
        }

        private void ThreadStarterName()
        {
            string currentThread = Thread.CurrentThread.Name;

            DateTime startTime = DateTime.Now;
            Timer timer = null;
            timer = new Timer((o) =>
            {
                string temp = runningName;
                runningName = currentThread;
                RaiseThreadStatusChanged(string.Format("{0}: {1} -> {2}.", currentThread, temp, runningName));
                if (DateTime.Now > startTime.AddSeconds(5))
                    timer.Dispose();
            }, null, 0, 10);
        }

        private void ThreadStarter()
        {
            int local = 100;
            RaiseThreadStatusChanged(string.Format("{0} local {1}.", Thread.CurrentThread.Name, local));
            local++;

            RaiseThreadStatusChanged(string.Format("\t{0} moduleLevel {1}.", Thread.CurrentThread.Name, moduleLevel));
            moduleLevel++;

            RaiseThreadStatusChanged(string.Format("\t\t{0} staticLevel {1}.", Thread.CurrentThread.Name, staticLevel));
            staticLevel++;
            
            //InnerFunction();
        }

        private void InnerFunction()
        {
            int inner = 200;
            RaiseThreadStatusChanged(string.Format("\t{0} inner {1}.", Thread.CurrentThread.Name, inner));
            inner++;
        }
    }
}
