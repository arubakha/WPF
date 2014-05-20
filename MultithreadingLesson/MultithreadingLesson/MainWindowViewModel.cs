using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using MultithreadingLesson.Business;
using System.ComponentModel;
using System.Windows.Controls;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using CoreLibrary;

namespace MultithreadingLesson
{
    public class MainWindowViewModel : BaseViewModel
    {
        private AutoResetEvent waitHandle = new AutoResetEvent(false);

        #region Properties
        private string result;
        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    RaisePropertyChanged("Result");
                }
            }
        }

        private Mode mode;
        public Mode Mode
        {
            get { return mode; }
            set
            {
                if (mode != value)
                {
                    mode = value;
                    RaisePropertyChanged("Mode");
                }
            }
        }
        #endregion

        #region ctor
        public MainWindowViewModel()
        {
            Mode = Mode.LongCall;
            ClickMeCommand = new DelegateCommand(ExecuteClickMeCommand);

            PropertyChanged += (s, e) => { if (e.PropertyName == "Mode") ClearResult(); };
        }
        #endregion

        private void ClearResult()
        {
            Result = string.Empty;
        }

        public ICommand ClickMeCommand { get; private set; }
        private void ExecuteClickMeCommand(object parameter)
        {
            ClearResult();

            Operations ops = new Operations();

            if (Mode == Mode.LongCall)
            {
                //1. Direct
                Result = ops.VeryLongOperation("LONG");
            }
            else if (Mode == Mode.Delegate)
            {
                //2. Local Delegate
                VeryLongOperationDelegate longOp = new VeryLongOperationDelegate(ops.VeryLongOperation);
                longOp.BeginInvoke("DELEGATE", DelegateCallback, null);
            }
            else if (Mode == Mode.Thread)
            {
                //3. Thread
                Thread thread = new Thread(ThreadStart);
                thread.Start(ops);
            }
            else if (Mode == Mode.AutoResetEvent)
            {
                //4. Thread
                Thread thread2 = new Thread(ThreadStart2);
                thread2.Start(ops);
                waitHandle.WaitOne();
            }

        }

        private delegate string VeryLongOperationDelegate(string parameter);
        private void DelegateCallback(IAsyncResult ar)
        {
            object returned = ((VeryLongOperationDelegate)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
            Result = returned.ToString();
        }

        private void ThreadStart(object param)
        {
            Operations ops = param as Operations;
            if (ops != null)
            {
                Result = ops.VeryLongOperation("THREAD");
            }
        }

        private void ThreadStart2(object param)
        {
            Operations ops = param as Operations;
            if (ops != null)
            {
                Result = ops.VeryLongOperation("AUTORESET");
            }
            waitHandle.Set();
        }


    }
}
